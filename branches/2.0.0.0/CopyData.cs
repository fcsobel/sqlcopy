using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;

namespace Test.SqlCopy
{
    /// <summary>
    /// Copy progress event args
    /// </summary>
    public class CopyProgressEventArgs : EventArgs
    {
        public SqlRowsCopiedEventArgs SqlArgs { get; private set; }
        public bool Done { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="count">Nr of rows copied</param>
        public CopyProgressEventArgs(long count) : this(count, false)
        {
        }

        public CopyProgressEventArgs(SqlRowsCopiedEventArgs args, bool done)
        {
            this.SqlArgs = args; 
            this.Done = done;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="count">Nr of rows copied</param>
        /// <param name="done">Copy is complete</param>
        public CopyProgressEventArgs(long count, bool done) : this(new SqlRowsCopiedEventArgs(count), done)
        {
        }
    }

    /// <summary>
    /// The actual copy class
    /// </summary>
    public class CopyData
    {
        public event EventHandler<CopyProgressEventArgs> CopyProgress; 

        private string _source;
        private string _destination;
        private int _timeout;
        private int _batchSize;
        private SqlBulkCopyOptions _options;
        private bool _deleteoption = false;

        public CopyData(string sourceConnectionString, string destinationConnectionString, SqlBulkCopyOptions options, int timeout, int batchSize)
        {
            this._source = sourceConnectionString;
            this._destination = destinationConnectionString;
            this._options = options;
            this._batchSize = batchSize;
            this._timeout = timeout;
        }

        public CopyData(string sourceConnectionString, string destinationConnectionString, SqlBulkCopyOptions options, int timeout, int batchSize, bool deleteOption)        
        {
            this._source = sourceConnectionString;
            this._destination = destinationConnectionString;
            this._options = options;
            this._batchSize = batchSize;
            this._timeout = timeout;
            this._deleteoption = deleteOption;
        }


        public void DeleteTable(string table)
        {
            using (SqlConnection destination = new SqlConnection(_destination))
            {
                string sql = string.Format("delete from {0};", table);

                Trace.TraceInformation("Deleting data from table {0}", table);

                using (SqlCommand command = new SqlCommand(sql, destination))
                {
                    command.CommandTimeout = _timeout;
                    destination.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void CopyTable(string table)
        {
            // delete destination rows first
            if (this._deleteoption) this.DeleteTable(table);

            using (SqlConnection source = new SqlConnection(_source))
            {
                // get the count of source records, 
                // so we can report on it in the progress overview
                string sql = string.Format("select count(*) from {0}", table);
                source.Open();

                int count = 0;
                using (SqlCommand command = new SqlCommand(sql, source))
                {
                    command.CommandTimeout = this._timeout;
                    count = (int)command.ExecuteScalar();
                }

                // get the columns for this table
                // filter computed columns
                sql = string.Format(@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                 WHERE NOT COLUMNPROPERTY(OBJECT_ID(table_name) ,COLUMN_NAME,'IsComputed') = 1
                 AND '[' + table_schema + '].[' + table_name + ']' = '{0}'", table);

                List<string> columns = new List<string>();
                using (SqlCommand  command = new SqlCommand(sql, source))
                {
                    using (SqlDataReader sr = command.ExecuteReader())
                    {
                        while (sr.Read())
                        {
                            columns.Add(string.Format("[{0}]", sr["COLUMN_NAME"]));
                        }
                    }
                }
                
                sql = string.Format("select {1} from {0}", table, string.Join(",", columns.ToArray()));

                using (SqlCommand command = new SqlCommand(sql, source))
                {
                    command.CommandTimeout = this._timeout;

                    using (IDataReader dr = command.ExecuteReader())
                    {

                        using (SqlBulkCopy copy = new SqlBulkCopy(_destination, _options))
                        {
                            copy.BulkCopyTimeout = this._timeout;
                            copy.BatchSize = this._batchSize;
                            // setup notification
                            copy.NotifyAfter = 1000;        
                            copy.DestinationTableName = table;
                            // attach event handler
                            copy.SqlRowsCopied += new SqlRowsCopiedEventHandler(copy_SqlRowsCopied);
                            copy.WriteToServer(dr);

                            // anyone out there?
                            if (CopyProgress != null)
                            {
                                // invoke the progress with the final count
                                CopyProgress.Invoke(copy, new CopyProgressEventArgs(count, true));
                            }
                        }
                    }
                }
                
            }
        }

        /// <summary>
        /// event handler for the SqlCopy event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void copy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            if (CopyProgress != null)
            {
                   CopyProgress.Invoke(sender, new CopyProgressEventArgs(e, false));
            }
        }

    }
}