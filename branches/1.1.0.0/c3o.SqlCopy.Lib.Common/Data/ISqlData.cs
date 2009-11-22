using System;
namespace c3o.SqlCopy.Data
{
    interface ISqlData
    {
        void CopyTable(string table);
        void DeleteTable(string db, string table);
        System.Data.IDataReader GetTables(string db);
        System.Data.SqlClient.SqlBulkCopyOptions Options { get; }
        void PostCopySql(string db);
        void PreCopySql(string db);
        System.Data.IDataReader SelectTable(string db, string table);
        c3o.SqlCopy.Objects.CopyObject settings { get; set; }
    }
}
