using System;
using System.Data;
using System.Data.SqlClient;

namespace Test.SqlCopy
{
    public class CopyData
    {
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

                SqlCommand command = new SqlCommand(sql, destination);

                destination.Open();
                command.ExecuteNonQuery();
            }
        }

        public void CopyTable(string table)
        {
            // delete destination rows first
            if (this._deleteoption) this.DeleteTable(table);

            using (SqlConnection source = new SqlConnection(_source))
            {
                string sql = string.Format("select * from {0}", table);

                SqlCommand command = new SqlCommand(sql, source);

                source.Open();
                IDataReader dr = command.ExecuteReader();

                using (SqlBulkCopy copy = new SqlBulkCopy(_destination, _options))
                {   
                    copy.BulkCopyTimeout = this._timeout;
                    copy.BatchSize = this._batchSize;
                    copy.DestinationTableName = table;                    
                    copy.WriteToServer(dr);                    
                }
            }
        }

    }
}