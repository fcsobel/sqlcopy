
/* 
 * IndiansInc.MySqlBulklCopy
 * Helpful to copy a huge data set from one Mysql table to another.
 * Requirements: .NET Framework 3.5/Mono
 * Copyright (c) 2012 IndiansInc.MySqlBulkcopy (http://code.google.com/p/mysqlbulkcopy/)
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; version 2 of the license.
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 */


namespace IndiansInc
{
    /*
     * MySqlBulkCopy
     * This class represents a base class that should be used to copy data 
     * @author   Albert Arul Prakash<albertarulprakash@gmail.com>  
     */
    /*
     * Version Information:
     * Version 0.1: Base version of upload is implemented
     * Version 0.2: Issue 2: Does not support Batch sizes like in SqlBulkCopy is completed
     */
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using MySql.Data.MySqlClient;
    using IndiansInc.Internals;

    public class MySqlBulkCopy : IDisposable
    {
		protected SqlBulkCopyOptions copyOptions { get; set; }

		protected bool _manageConnection { get; set; }

        /// <summary>
        /// Delegate to subscribe notification from assembly
        /// </summary>
        /// <param name="e">Event arguments </param>
        //public delegate void OnBatchSizeCompletedDelegate(BatchSizeCompletedEventArgs e);
		public delegate void MySqlRowsCopiedEventHandler(SqlRowsCopiedEventArgs e);
		
		public ColumnMapItemCollection ColumnMappings { get; set; }
		protected MySqlConnection DestinationDbConnection { get; set; }
		protected string _connectionString { get; set; }
		public string DestinationTableName { get; set; }
		public int BatchSize { get; set; }

		//
		// Summary:
		//     Defines the number of rows to be processed before generating a notification
		//     event.
		//
		// Returns:
		//     The integer value of the System.Data.SqlClient.SqlBulkCopy.NotifyAfter property,
		//     or zero if the property has not been set.
		public int NotifyAfter { get; set; }

		// Summary:
		//     Occurs every time that the number of rows specified by the System.Data.SqlClient.SqlBulkCopy.NotifyAfter
		//     property have been processed.
		//public event SqlRowsCopiedEventHandler SqlRowsCopied;


        /// <summary>
        /// Delegate that need to invoked once the assembly uploads the specified BatchSize
        /// </summary>
        //public OnBatchSizeCompletedDelegate OnBatchSizeCompleted { get; set; }
		//public event OnBatchSizeCompletedDelegate OnBatchSizeCompleted;		
		public event MySqlRowsCopiedEventHandler SqlRowsCopied;		
		
		public MySqlBulkCopy(MySqlConnection connection)
		{
			this.DestinationDbConnection = connection;
		}
		public MySqlBulkCopy(string connectionString)
		{
			this._connectionString = connectionString;
		}

		public MySqlBulkCopy(string connectionString, SqlBulkCopyOptions copyOptions)
		{ 
			this._connectionString = connectionString;
			this.copyOptions = copyOptions;
		}


        /// <summary>
        /// Method that uploads the data from the MySqlDataReader that contains the data.
        /// </summary>
        /// <param name="reader">Data reader that contains the source data that to be uploaded</param>
        public void WriteToServer(System.Data.IDataReader reader)
        {
            //if (reader.HasRows)
            //{
                System.Data.DataTable table = new System.Data.DataTable();
                table.Load(reader);
                WriteToServer(table);
            //}
        }

        /// <summary>
        /// Method that uploads the data from the <see cref="System.Data.DataTable">DataTable</see> that contains the data.
        /// </summary>
        /// <param name="table">Data table that contains source data that to be uploaded</param>
		public void WriteToServer(System.Data.DataTable table)
		{
			if (this.DestinationDbConnection == null)
			{
				this._manageConnection = true;
				this.DestinationDbConnection = new MySqlConnection(this._connectionString);
				this.DestinationDbConnection.Open();
			}

			CommonFunctions functions = new CommonFunctions();
			int counter = 0;

			//eventArgs.ErrorDataRows = new List<System.Data.DataRow>();
			foreach (System.Data.DataRow item in table.Rows)
			{
				// build sql
				string sql = functions.ConstructSql(DestinationTableName, item, ColumnMappings);
				Console.WriteLine(sql);

				// Insert data
				MySqlCommand command = new MySqlCommand(sql, DestinationDbConnection);
				command.ExecuteNonQuery();

				counter++;

				if (counter >= NotifyAfter && counter > 0)
				{
					if (SqlRowsCopied != null)
					{
						SqlRowsCopiedEventArgs eventArgs = new SqlRowsCopiedEventArgs(counter);

						// invoke the delegate
						SqlRowsCopied(eventArgs);
					}
					counter = 0;
				}
			}

			// A final raise from the code. this is to catch the arbitary values that does not meet the batch size limit
			if (counter > 0)
			{
				// batch size is completed. invoke the SqlRowsCopied Delegate to alert the caller
				if (SqlRowsCopied != null)
				{
					SqlRowsCopiedEventArgs eventArgs = new SqlRowsCopiedEventArgs(counter);
					SqlRowsCopied(eventArgs);
				}
			}
		}     



		public void Dispose()
		{
			if (_manageConnection)
			{
				this.DestinationDbConnection.Dispose();
			}
			//throw new NotImplementedException();
		}
	}
}
