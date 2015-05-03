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
		//public event RowsCopiedEventHandler OnRowsCopied;
		
		public string ConnectionString { get; set; }
		public CopyObject CopySettings { get; set; }

		public OracleData(string connectionString, CopyObject settings)
		{
			this.ConnectionString = connectionString;
			this.CopySettings = settings;
		}

		public OracleBulkCopyOptions Options
		{
			get
			{
				OracleBulkCopyOptions option = OracleBulkCopyOptions.Default;
				if (CopySettings.UseInternalTransaction) option = option | OracleBulkCopyOptions.UseInternalTransaction;
				return option;
			}
		}

		public IDataReader List()
		{
			return this.ExecuteReader(this.CopySettings.ListSql);
		}

		public IDataReader Select(TableObject table)
		{
			return this.ExecuteReader(string.Format(CopySettings.SelectSql, this.FullTableName(table)));
		}

		public void Delete(TableObject table)
		{
			this.ExecuteNonQuery(string.Format(CopySettings.DeleteSql, this.FullTableName(table)));
		}

		public long Count(TableObject table)
		{
			return (long) this.ExecuteScalar(string.Format(CopySettings.CountSql, this.FullTableName(table)));
		}

		public void PreCopy()
		{
			if (!String.IsNullOrEmpty(this.CopySettings.PreCopySql))
			{
				this.ExecuteNonQuery(this.CopySettings.PreCopySql);
			}
		}
		
		public void PostCopy()
		{
			if (!String.IsNullOrEmpty(this.CopySettings.PostCopySql))
			{
				this.ExecuteNonQuery(this.CopySettings.PostCopySql);
			}
		}

		//public void Copy(TableObject table)
		//{
		//	// Delete data
		//	if (CopySettings.DeleteRows) this.Delete(table);


		//	using (IDataReader dr = this.Select(table))
		//	{
		//		using (OracleBulkCopy copy = new OracleBulkCopy(CopySettings.Destination, this.Options))
		//		{
		//			copy.BulkCopyTimeout = CopySettings.BulkCopyTimeout;
		//			copy.BatchSize = CopySettings.BatchSize;
		//			copy.DestinationTableName = this.FullTableName(table);
		//			copy.NotifyAfter = CopySettings.NotifyAfter;
		//			copy.OracleRowsCopied += copy_OracleRowsCopied;
		//			copy.WriteToServer(dr);
		//		}
		//	}
		//}

		// Copy from source to destination this
		public void Copy(TableObject table, IDbData source)
		{
			// Delete data from destination (this)
			if (CopySettings.DeleteRows) this.Delete(table);

			// read from source
			using (IDataReader dr = source.Select(table))
			{
				// copy to destination (this)
				using (OracleBulkCopy copy = new OracleBulkCopy(this.ConnectionString, this.Options))
				{
					copy.BulkCopyTimeout = CopySettings.BulkCopyTimeout;
					copy.BatchSize = CopySettings.BatchSize;
					copy.DestinationTableName = this.FullTableName(table);
					copy.NotifyAfter = CopySettings.NotifyAfter;
					//copy.OracleRowsCopied += copy_OracleRowsCopied;
					copy.WriteToServer(dr);
				}
			}
		}

		//void copy_OracleRowsCopied(object sender, OracleRowsCopiedEventArgs eventArgs)
		//{
		//	if (OnRowsCopied != null) { OnRowsCopied(this, new RowsCopiedEventArgs(eventArgs)); }
		//}


		public IDataReader ExecuteReader(string sql)
		{
			OracleConnection connection = new OracleConnection(this.ConnectionString);
			OracleCommand command = new OracleCommand(sql, connection);
			connection.Open();
			return command.ExecuteReader(CommandBehavior.CloseConnection);
		}


		public int ExecuteNonQuery(string sql)
		{
			using (OracleConnection connection = new OracleConnection(this.ConnectionString))
			{
				OracleCommand command = new OracleCommand(sql, connection);
				connection.Open();
				return command.ExecuteNonQuery();
			}
		}

		public object ExecuteScalar(string sql)
		{
			using (OracleConnection connection = new OracleConnection(this.ConnectionString))
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
				return string.Format(CopySettings.SelectSql, this.FullTableName(table));
			}
		}

				/// <summary>
		/// Return DB formatted table name
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public string FullTableName(TableObject table)
		{
			if (this.CopySettings.IncludeSchema)
			{
				if (string.IsNullOrEmpty(this.CopySettings.SchemaFormat))
				{
					return string.Format("{0}.{1}", table.Schema, table.Name);
				}
				else
				{
					return string.Format(this.CopySettings.SchemaFormat, table.Schema, table.Name);
				}
			}
			else
			{
				if (string.IsNullOrEmpty(this.CopySettings.TableFormat))
				{
					return table.Name;
				}
				else
				{
					return string.Format(this.CopySettings.TableFormat, table.Name);
				}
			}
		}
	}
}
