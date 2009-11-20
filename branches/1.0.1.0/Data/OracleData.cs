using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using Test.SqlCopy.Objects;
using Test.SqlCopy.Data;

namespace Test.SqlCopy
{
    public class OracleData : IDbData
    {
        public CopyObject settings { get; set; }

        public OracleData(CopyObject settings)
        {
            this.settings = settings;
        }

        public OracleBulkCopyOptions Options
        {
            get
            {
                OracleBulkCopyOptions option = OracleBulkCopyOptions.Default;
                if (settings.UseInternalTransaction) option = option | OracleBulkCopyOptions.UseInternalTransaction;
                return option;
            }
        }

        public IDataReader List()
        {
            return this.ExecuteReader(this.settings.Source, this.settings.ListSql);
        }

        public IDataReader Select(string table)
        {
            return this.ExecuteReader(this.settings.Source, string.Format(settings.SelectSql, table));
        }

        public void Delete(string table)
        {
            this.ExecuteNonQuery(this.settings.Destination, string.Format(settings.DeleteSql, table));
        }

        public void PreCopy()
        {
            if (!String.IsNullOrEmpty(this.settings.PreCopySql))
            {
                this.ExecuteNonQuery(this.settings.Destination, this.settings.PreCopySql);
            }
        }
        
        public void PostCopy()
        {
            if (!String.IsNullOrEmpty(this.settings.PostCopySql))
            {
                this.ExecuteNonQuery(this.settings.Destination, this.settings.PostCopySql);
            }
        }

        public void Copy(string table)
        {
            // Delete data
            if (settings.DeleteRows) this.Delete(table);


            using (IDataReader dr = this.Select(table))
            {
                using (OracleBulkCopy copy = new OracleBulkCopy(settings.Destination, this.Options))
                {
                    copy.BulkCopyTimeout = settings.BulkCopyTimeout;
                    copy.BatchSize = settings.BatchSize;
                    copy.DestinationTableName = table;
                    copy.WriteToServer(dr);
                }
            }
        }


        public IDataReader ExecuteReader(string db, string sql)
        {
            OracleConnection connection = new OracleConnection(db);
            OracleCommand command = new OracleCommand(sql, connection);
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }


        public int ExecuteNonQuery(string db, string sql)
        {
            using (OracleConnection connection = new OracleConnection(db))
            {
                OracleCommand command = new OracleCommand(sql, connection);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}
