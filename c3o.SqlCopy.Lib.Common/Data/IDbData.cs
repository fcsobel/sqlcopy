using System;
using System.Data;
using System.Data.SqlClient;
using c3o.SqlCopy.Objects;
using Oracle.DataAccess.Client;

namespace c3o.SqlCopy.Data
{
	public delegate void RowsCopiedEventHandler(object sender, RowsCopiedEventArgs e);

    public interface IDbData
    {
		string ConnectionString { get; set; }
        //void Copy(TableObject obj);
        void Copy(TableObject table, IDbData source);
		long Count(TableObject table);
        void Delete(TableObject table);
        int ExecuteNonQuery(string sql);
        IDataReader ExecuteReader(string sql);
        IDataReader List();
        void PostCopy();
        void PreCopy();
        System.Data.IDataReader Select(TableObject table);
        CopyObject CopySettings { get; set; }
        string GetSelectSql(TableObject table);
		//event RowsCopiedEventHandler OnRowsCopied;		
    }

	public interface IDbDataCore
    {
        int ExecuteNonQuery(string db, string sql);
        IDataReader ExecuteReader(string db, string sql);
    }



	// Summary:
	//     Represents the set of arguments passed to the System.Data.SqlClient.SqlRowsCopiedEventHandler.
	public class RowsCopiedEventArgs : EventArgs
	{
		private OracleRowsCopiedEventArgs _oracleArgs { get; set; }
		private SqlRowsCopiedEventArgs _sqlArgs { get; set; }
		
		public bool Abort
		{
			set
			{
				if (this._oracleArgs != null) this._oracleArgs.Abort = value;
				if (this._sqlArgs != null) this._sqlArgs.Abort = value;
				//if (this._sqlArgs != null) this._mySqlArgs.Abort = value;
			}
		}
		public long RowsCopied
		{
			get
			{
				if (this._oracleArgs != null) return this._oracleArgs.RowsCopied;
				if (this._sqlArgs != null) return this._sqlArgs.RowsCopied;		
				return 0;
			}
		}
		public RowsCopiedEventArgs(SqlRowsCopiedEventArgs e) { this._sqlArgs = e; }
		public RowsCopiedEventArgs(OracleRowsCopiedEventArgs e) { this._oracleArgs = e; }		

	}
}
