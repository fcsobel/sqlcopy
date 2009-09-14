using System;
using System.Collections.Generic;
using System.Text;

namespace Test.SqlCopy.Objects
{
    [Serializable()]
    public class CopyObject
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public int BatchSize { get; set;}
        public int BulkCopyOptions { get; set; }
        public int BulkCopyTimeout { get; set; }
        public int NotifyAfter { get; set; }
        public string DestinationTableName { get; set; }
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
        public string PreCopySql { get; set; }
        public string PostCopySql { get; set; }

        //@"SELECT '[' + table_schema + '].[' + table_name + ']' as table_name FROM information_schema.tables";
        public string GetTableSql { get; set; }

        //delete from {0};"
        public string DeleteSql { get; set; }
    }
}
