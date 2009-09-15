using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Test.SqlCopy.Objects;


namespace Test.SqlCopy.Data
{
    public class SqlData : IDbData 
    {
        public CopyObject settings { get; set; }

        public SqlData(CopyObject settings)
        {
            this.settings = settings;
        }

        public SqlBulkCopyOptions Options
        {
            get
            {
                SqlBulkCopyOptions option = SqlBulkCopyOptions.Default;
                if (settings.KeepIdentity) option = option | SqlBulkCopyOptions.KeepIdentity;
                if (settings.KeepNulls) option = option | SqlBulkCopyOptions.KeepNulls;
                if (settings.CheckConstraints) option = option | SqlBulkCopyOptions.CheckConstraints;
                if (settings.FireTriggers) { option = option | SqlBulkCopyOptions.FireTriggers; }
                if (settings.TableLock) { option = option | SqlBulkCopyOptions.TableLock; }
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
            this.ExecuteNonQuery(this.settings.Destination, Properties.Settings.Default.PreCopySql);
        }


        public void PostCopy()
        {
            this.ExecuteNonQuery(this.settings.Destination, Properties.Settings.Default.PostCopySql);
        }


        public void Copy(string table)
        {
            if (settings.DeleteRows) this.Delete(table);

            using (IDataReader dr = this.Select(table))
            {
                using (SqlBulkCopy copy = new SqlBulkCopy(settings.Destination, this.Options))
                {
                    copy.BulkCopyTimeout = settings.BulkCopyTimeout;
                    copy.BatchSize = settings.BatchSize;
                    copy.DestinationTableName = settings.DestinationTableName;
                    copy.WriteToServer(dr);
                }
            }
        }


        public IDataReader ExecuteReader(string db, string sql)
        {            
            SqlConnection connection = new SqlConnection(db);
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }


        public int ExecuteNonQuery(string db, string sql)
        {
            using (SqlConnection connection = new SqlConnection(db))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}
