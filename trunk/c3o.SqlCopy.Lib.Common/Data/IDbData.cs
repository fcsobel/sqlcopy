using System;
using System.Data;
using System.Data.SqlClient;
using c3o.SqlCopy.Objects;

namespace c3o.SqlCopy.Data
{
    public interface IDbData
    {
        void Copy(TableObject obj);
        void Copy(TableObject table, IDbData source);
        void Delete(TableObject table);
        int ExecuteNonQuery(string db, string sql);
        IDataReader ExecuteReader(string db, string sql);
        IDataReader List();
        void PostCopy();
        void PreCopy();
        System.Data.IDataReader Select(TableObject table);
        CopyObject settings { get; set; }
        string GetSelectSql(TableObject table);
    }
}
