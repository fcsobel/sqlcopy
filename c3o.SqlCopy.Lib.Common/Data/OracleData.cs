using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using c3o.SqlCopy.Objects;
using c3o.SqlCopy.Data;

namespace c3o.SqlCopy
{
	public class OracleData : IDbData
	{
		public event RowsCopiedEventHandler OnRowsCopied;
		
		public CopyObject settings { get; set; }

		public OracleData(CopyObject settings)
		{
			this.settings = settings;
		}

		public OracleBulkCopyOptions Options
		{
			get
			{
				OracleBulkCopyOptions option = OracleBulkCopyOptions.Default;
				if (settings.UseInternalTransaction) option = option | OracleBulkCopyOptions.UseInternalTransaction;
				return option;
			}
		}

		public IDataReader List()
		{
			return this.ExecuteReader(this.settings.Source, this.settings.ListSql);
		}

		public IDataReader Select(TableObject table)
		{
			return this.ExecuteReader(this.settings.Source, string.Format(settings.SelectSql, table.FullName));
		}

		public void Delete(TableObject table)
		{
			this.ExecuteNonQuery(this.settings.Destination, string.Format(settings.DeleteSql, table.FullName));
		}

		public long Count(TableObject table)
		{
			return (long) this.ExecuteScalar(this.settings.Source, string.Format(settings.CountSql, table.FullName));
		}

		public void PreCopy()
		{
			if (!String.IsNullOrEmpty(this.settings.PreCopySql))
			{
				this.ExecuteNonQuery(this.settings.Destination, this.settings.PreCopySql);
			}
		}
		
		public void PostCopy()
		{
			if (!String.IsNullOrEmpty(this.settings.PostCopySql))
			{
				this.ExecuteNonQuery(this.settings.Destination, this.settings.PostCopySql);
			}
		}

		public void Copy(TableObject table)
		{
			// Delete data
			if (settings.DeleteRows) this.Delete(table);


			using (IDataReader dr = this.Select(table))
			{
				using (OracleBulkCopy copy = new OracleBulkCopy(settings.Destination, this.Options))
				{
					copy.BulkCopyTimeout = settings.BulkCopyTimeout;
					copy.BatchSize = settings.BatchSize;
					copy.DestinationTableName = table.FullName;
					copy.NotifyAfter = settings.NotifyAfter;
					copy.OracleRowsCopied += copy_OracleRowsCopied;
					copy.WriteToServer(dr);
				}
			}
		}


		public void Copy(TableObject table, IDbData source)
		{
			// Delete data
			if (settings.DeleteRows) this.Delete(table);

			using (IDataReader dr = source.Select(table))
			{
				using (OracleBulkCopy copy = new OracleBulkCopy(settings.Destination, this.Options))
				{
					copy.BulkCopyTimeout = settings.BulkCopyTimeout;
					copy.BatchSize = settings.BatchSize;
					copy.DestinationTableName = table.FullName;
					copy.NotifyAfter = settings.NotifyAfter;
					copy.OracleRowsCopied += copy_OracleRowsCopied;
					copy.WriteToServer(dr);
				}
			}
		}

		void copy_OracleRowsCopied(object sender, OracleRowsCopiedEventArgs eventArgs)
		{
			if (OnRowsCopied != null) { OnRowsCopied(this, new RowsCopiedEventArgs(eventArgs)); }
		}


		public IDataReader ExecuteReader(string db, string sql)
		{
			OracleConnection connection = new OracleConnection(db);
			OracleCommand command = new OracleCommand(sql, connection);
			connection.Open();
			return command.ExecuteReader(CommandBehavior.CloseConnection);
		}


		public int ExecuteNonQuery(string db, string sql)
		{
			using (OracleConnection connection = new OracleConnection(db))
			{
				OracleCommand command = new OracleCommand(sql, connection);
				connection.Open();
				return command.ExecuteNonQuery();
			}
		}

		public object ExecuteScalar(string db, string sql)
		{
			using (OracleConnection connection = new OracleConnection(db))
			{
				OracleCommand command = new OracleCommand(sql, connection);
				connection.Open();
				return command.ExecuteScalar();
			}
		}


		public string GetSelectSql(TableObject table)
		{
			if (!string.IsNullOrEmpty(table.Sql))
			{                
				return table.Sql.Replace("\r\n", "\n").Replace("\n", "\r\n");
			}
			else
			{
				return string.Format(settings.SelectSql, table.FullName);
			}
		}
	}
}
