using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Test.SqlCopy.Data
{
    public class SqlData
    {
        public IDataReader SelectTable(string db, string table)
        {
            SqlConnection source = new SqlConnection(db);

            string sql = string.Format("select * from {0}", table);

            SqlCommand command = new SqlCommand(sql, source);

            source.Open();
            IDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            return dr;
        }

        public void DeleteTable(string db, string table)
        {
            using (SqlConnection destination = new SqlConnection(db))
            {
                string sql = string.Format("delete from {0};", table);

                SqlCommand command = new SqlCommand(sql, destination);

                destination.Open();
                command.ExecuteNonQuery();
            }
        }


        public IDataReader GetTables(string db)
        {
            string sql = @"SELECT '[' + table_schema + '].[' + table_name + ']' as table_name FROM information_schema.tables";

            SqlConnection source = new SqlConnection(db);
            SqlCommand command = new SqlCommand(sql, source);
            source.Open();
            IDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            return dr;
        }


        //Disable Constraints for all tables
        //exec sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
        //exec sp_msforeachtable "ALTER TABLE ? DISABLE TRIGGER all"
        public void PreCopySql(string db)
        {
            using (SqlConnection destination = new SqlConnection(db))
            {
                //string sql = "exec sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; exec sp_msforeachtable 'ALTER TABLE ? DISABLE TRIGGER all'; ";
                string sql = Properties.Settings.Default.PreCopySql;

                SqlCommand command = new SqlCommand(sql, destination);

                destination.Open();
                command.ExecuteNonQuery();
            }
        }

        //Turn constraints and triggers back on
        //exec sp_msforeachtable @command1="print '?'", @command2="ALTER TABLE ? CHECK CONSTRAINT all"
        //exec sp_msforeachtable @command1="print '?'", @command2="ALTER TABLE ? ENABLE TRIGGER all"
        public void PostCopySql(string db)
        {
            using (SqlConnection destination = new SqlConnection(db))
            {
                //string sql = "exec sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all'; exec sp_msforeachtable 'ALTER TABLE ? ENABLE TRIGGER all'; ";
                string sql = Properties.Settings.Default.PostCopySql;

                SqlCommand command = new SqlCommand(sql, destination);

                destination.Open();
                command.ExecuteNonQuery();
            }
        }


    }
}
