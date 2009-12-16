using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using c3o.SqlCopy.Objects;


namespace c3o.SqlCopy.Data
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
            if (!string.IsNullOrEmpty(this.settings.PreCopySql))
            {
                this.ExecuteNonQuery(this.settings.Destination, this.settings.PreCopySql);
            }
        }


        public void PostCopy()
        {
            if (!string.IsNullOrEmpty(this.settings.PostCopySql))
            {

                this.ExecuteNonQuery(this.settings.Destination, this.settings.PostCopySql);
            }
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
                    copy.DestinationTableName = table;
                    copy.WriteToServer(dr);
                }
            }
        }


        public void Copy(string table, IDbData source)
        {
            if (settings.DeleteRows) this.Delete(table);

            using (IDataReader dr = source.Select(table))
            {
                using (SqlBulkCopy copy = new SqlBulkCopy(settings.Destination, this.Options))
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
