using System;
using System.Data;
using System.Data.SqlClient;
using c3o.SqlCopy.Objects;

namespace c3o.SqlCopy.Data
{
    public interface IDbData
    {
        void Copy(string table);
        void Delete(string table);
        int ExecuteNonQuery(string db, string sql);
        System.Data.IDataReader ExecuteReader(string db, string sql);
        System.Data.IDataReader List();
        void PostCopy();
        void PreCopy();
        System.Data.IDataReader Select(string table);
        c3o.SqlCopy.Objects.CopyObject settings { get; set; }
    }
}
