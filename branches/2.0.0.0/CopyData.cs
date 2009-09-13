using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Test.SqlCopy
{
    /// <summary>
    /// Copy progress event args
    /// </summary>
    public class CopyProgressEventArgs : EventArgs
    {
        public SqlRowsCopiedEventArgs SqlArgs { get; private set; }
        public long Total { get; private set; }
        public Exception Exception { get; private set; }
        public string TableName { get; private set; }
        public long Current
        {
            get
            {
                if (SqlArgs != null)
                    return SqlArgs.RowsCopied;
                else
                    return Total;
            }
        }

        public CopyProgressEventArgs(SqlRowsCopiedEventArgs args, string tableName, long total)
        {
            TableName = tableName;
            SqlArgs = args;
            Total = total;
        }

        public CopyProgressEventArgs(string tableName, Exception ex)
        {
            Exception = ex;
            TableName = tableName;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="count">Nr of rows copied</param>
        /// <param name="done">Copy is complete</param>
        public CopyProgressEventArgs(string tableName, long count, long total) : this(new SqlRowsCopiedEventArgs(count), tableName, total)
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
            _source = sourceConnectionString;
            _destination = destinationConnectionString;
            _options = options;
            _batchSize = batchSize;
            _timeout = timeout;
        }

        public CopyData(string sourceConnectionString, string destinationConnectionString, SqlBulkCopyOptions options, int timeout, int batchSize, bool deleteOption)        
        {
            _source = sourceConnectionString;
            _destination = destinationConnectionString;
            _options = options;
            _batchSize = batchSize;
            _timeout = timeout;
            _deleteoption = deleteOption;
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
            if (_deleteoption) DeleteTable(table);

            using (SqlConnection source = new SqlConnection(_source))
            {
                // get the count of source records, 
                // so we can report on it in the progress overview
                string sql = string.Format("select count(*) from {0}", table);
                source.Open();

                long rowCount = 0;
                using (SqlCommand command = new SqlCommand(sql, source))
                {
                    command.CommandTimeout = _timeout;
                    rowCount = (int)command.ExecuteScalar();
                }

                Trace.TraceInformation("Starting to copy {0} rows to table {1}", rowCount, table);

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
                    command.CommandTimeout = _timeout;

                    using (IDataReader dr = command.ExecuteReader())
                    {

                        using (SqlBulkCopy copy = new SqlBulkCopy(_destination, _options))
                        {
                            copy.BulkCopyTimeout = _timeout;
                            copy.BatchSize = _batchSize;

                            // explicitly map column names
                            foreach (string column in columns)
                            {
                                copy.ColumnMappings.Add(column, column);
                            }

                            // setup notification
                            copy.NotifyAfter = 1000;        
                            copy.DestinationTableName = table;
                            // attach event handler
                            copy.SqlRowsCopied +=
                                delegate(object sender, SqlRowsCopiedEventArgs e)
                                {
                                    if (CopyProgress != null)
                                    {
                                        CopyProgress.Invoke(sender, new CopyProgressEventArgs(e,table, rowCount));
                                    }
                                };
                            copy.WriteToServer(dr);

                            // anyone out there?
                            if (CopyProgress != null)
                            {
                                // invoke the progress with the final count
                                CopyProgress.Invoke(copy, new CopyProgressEventArgs(table, rowCount, rowCount));
                            }
                        }
                    }
                }
            }
        }
    }
}