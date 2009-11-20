using System;
using System.Collections.Generic;
using System.Text;

namespace Test.SqlCopy.Objects
{
    [Serializable()]
    public class CopyObject
    {
        public string Dbms { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }
        public int BatchSize { get; set;}
        public int BulkCopyOptions { get; set; }
        public int BulkCopyTimeout { get; set; }
        public int NotifyAfter { get; set; }
        //public string DestinationTableName { get; set; }
        public bool Default { get; set; }
        public bool UseInternalTransaction { get; set; }  

        // Oracle only        
        public string DestinationPartitionName { get; set; }        

        // Sql server only
        public bool CheckConstraints { get; set; }        
        public bool FireTriggers { get; set; }
        public bool KeepIdentity { get; set; }
        public bool KeepNulls { get; set; }
        public bool TableLock { get; set; }

        // Custom
        public bool DeleteRows { get; set; }

        // Sql
        //Disable Constraints for all tables
        //exec sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
        //exec sp_msforeachtable "ALTER TABLE ? DISABLE TRIGGER all"
        //string sql = "exec sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; exec sp_msforeachtable 'ALTER TABLE ? DISABLE TRIGGER all'; ";
        public string PreCopySql { get; set; }

        //Turn constraints and triggers back on
        //exec sp_msforeachtable @command1="print '?'", @command2="ALTER TABLE ? CHECK CONSTRAINT all"
        //exec sp_msforeachtable @command1="print '?'", @command2="ALTER TABLE ? ENABLE TRIGGER all"
        //string sql = "exec sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all'; exec sp_msforeachtable 'ALTER TABLE ? ENABLE TRIGGER all'; ";
        public string PostCopySql { get; set; }

        //@"SELECT '[' + table_schema + '].[' + table_name + ']' as table_name FROM information_schema.tables";
        public string ListSql { get; set; }

        //delete from {0};"
        public string DeleteSql { get; set; }

        //"select * from {0}"
        public string SelectSql { get; set; }
    }

}
