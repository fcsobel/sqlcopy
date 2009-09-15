using System;
namespace Test.SqlCopy.Data
{
    interface IOracleData
    {
        void Copy(string table);
        void Delete(string table);
        int ExecuteNonQuery(string db, string sql);
        System.Data.IDataReader ExecuteReader(string db, string sql);
        System.Data.IDataReader List();
        Oracle.DataAccess.Client.OracleBulkCopyOptions Options { get; }
        void PostCopy();
        void PreCopy();
        System.Data.IDataReader Select(string table);
        Test.SqlCopy.Objects.CopyObject settings { get; set; }
    }
}
