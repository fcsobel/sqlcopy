using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

namespace Test.SqlCopy.Data
{
    public class OracleData
    {
        public IDataReader SelectTable(string db, string table)
        {
            OracleConnection source = new OracleConnection(db);

            string sql = string.Format("select * from {0}", table);

            OracleCommand command = new OracleCommand(sql, source);

            source.Open();
            IDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            return dr;
        }

        public void DeleteTable(string db, string table)
        {
            OracleConnection destination = new OracleConnection(db);
            string sql = string.Format("delete from {0};", table);

            OracleCommand command = new OracleCommand(sql, destination);

            destination.Open();
            command.ExecuteNonQuery();
        }


        public IDataReader GetTables(string db)
        {
            string sql = @"SELECT '[' + table_schema + '].[' + table_name + ']' as table_name FROM information_schema.tables";

            OracleConnection source = new OracleConnection(db);
            OracleCommand command = new OracleCommand(sql, source);
            source.Open();
            IDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            return dr;
        }


        //Disable Constraints for all tables
        //exec sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
        //exec sp_msforeachtable "ALTER TABLE ? DISABLE TRIGGER all"
        public void PreCopySql(string db)
        {
            using (OracleConnection destination = new OracleConnection(db))
            {
                //string sql = "exec sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; exec sp_msforeachtable 'ALTER TABLE ? DISABLE TRIGGER all'; ";
                string sql = Properties.Settings.Default.PreCopySql;

                OracleCommand command = new OracleCommand(sql, destination);

                destination.Open();
                command.ExecuteNonQuery();
            }
        }

        //Turn constraints and triggers back on
        //exec sp_msforeachtable @command1="print '?'", @command2="ALTER TABLE ? CHECK CONSTRAINT all"
        //exec sp_msforeachtable @command1="print '?'", @command2="ALTER TABLE ? ENABLE TRIGGER all"
        public void PostCopySql(string db)
        {
            using (OracleConnection destination = new OracleConnection(db))
            {
                //string sql = "exec sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all'; exec sp_msforeachtable 'ALTER TABLE ? ENABLE TRIGGER all'; ";
                string sql = Properties.Settings.Default.PostCopySql;

                OracleCommand command = new OracleCommand(sql, destination);

                destination.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
