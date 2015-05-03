using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using c3o.SqlCopy.Objects;


namespace c3o.SqlCopy.Data
{
	public class SqlData : IDbData 
	{
		//public event RowsCopiedEventHandler OnRowsCopied;
	
		public string ConnectionString { get; set; }
		public CopyObject CopySettings { get; set; }

		public SqlData(string connectionString, CopyObject settings)
		{
			this.ConnectionString = connectionString;
			this.CopySettings = settings;
		}

		public SqlBulkCopyOptions Options
		{
			get
			{
				SqlBulkCopyOptions option = SqlBulkCopyOptions.Default;
				if (CopySettings.KeepIdentity) option = option | SqlBulkCopyOptions.KeepIdentity;
				if (CopySettings.KeepNulls) option = option | SqlBulkCopyOptions.KeepNulls;
				if (CopySettings.CheckConstraints) option = option | SqlBulkCopyOptions.CheckConstraints;
				if (CopySettings.FireTriggers) { option = option | SqlBulkCopyOptions.FireTriggers; }
				if (CopySettings.TableLock) { option = option | SqlBulkCopyOptions.TableLock; }
				return option;                
			}
		}

		public IDataReader List()
		{
			return this.ExecuteReader(this.CopySettings.ListSql);
		}

		
		public string GetSelectSql(TableObject table)
		{
			if (!string.IsNullOrEmpty(table.Sql))
			{
				return table.Sql.Replace("\r\n", "\n").Replace("\n", "\r\n");
			}
			else
			{
				List<string> columns = new List<string>();

				if (CopySettings.IncludeSchema)
				{
					//				                            where   '[' + table_schema + '].[' + table_name + ']' = '[{0}].[{1}]'
					string sql = @"select column_name 
							from INFORMATION_SCHEMA.COLUMNS 
							where   table_schema  = '{0}' 
									and table_name = '{1}'
									and not columnproperty(object_id(table_name), COLUMN_NAME, 'IsComputed') = 1";


					using (IDataReader dr = this.ExecuteReader(string.Format(sql, table.Schema, table.Name)))
					{
						while (dr.Read())
						{
							columns.Add(string.Format("[{0}]", dr["COLUMN_NAME"]));
						}
					}
				}
				else
				{
					string sql = @"select column_name 
							from INFORMATION_SCHEMA.COLUMNS 
							where   table_name  = '{0}' 
									and not columnproperty(object_id(table_name), COLUMN_NAME, 'IsComputed') = 1";

					using (IDataReader dr = this.ExecuteReader(string.Format(sql, table.Name)))
					{
						while (dr.Read())
						{
							columns.Add(string.Format("[{0}]", dr["COLUMN_NAME"]));
						}
					}
				}

				string select = string.Format("select \r\n{1} \r\nfrom {0}", this.FullTableName(table), string.Join(",\r\n", columns.ToArray()));

				return select;
			}
		}


		public IDataReader Select(TableObject table)
		{
			string sql = this.GetSelectSql(table);

			//return this.ExecuteReader(this.settings.Source, string.Format(settings.SelectSql, table));
			return this.ExecuteReader(string.Format(sql));
		}

		public void Delete(TableObject table)
		{
			this.ExecuteNonQuery(string.Format(CopySettings.DeleteSql, this.FullTableName(table)));
		}

		public long Count(TableObject table)
		{
			return System.Convert.ToInt64(this.ExecuteScalar(string.Format(CopySettings.CountSql, this.FullTableName(table))));
		}


		public void PreCopy()
		{
			if (!string.IsNullOrEmpty(this.CopySettings.PreCopySql))
			{
				this.ExecuteNonQuery(this.CopySettings.PreCopySql);
			}
		}


		public void PostCopy()
		{
			if (!string.IsNullOrEmpty(this.CopySettings.PostCopySql))
			{
				this.ExecuteNonQuery(this.CopySettings.PostCopySql);
			}
		}


		//public void Copy(TableObject table)
		//{
		//	if (CopySettings.DeleteRows) this.Delete(table);

		//	using (IDataReader dr = this.Select(table))
		//	{
		//		using (SqlBulkCopy copy = new SqlBulkCopy(CopySettings.Destination, this.Options))
		//		{
		//			copy.BulkCopyTimeout = CopySettings.BulkCopyTimeout;
		//			copy.BatchSize = CopySettings.BatchSize;
		//			copy.DestinationTableName = this.FullTableName(table);
		//			copy.NotifyAfter = CopySettings.NotifyAfter;
		//			copy.SqlRowsCopied += copy_SqlRowsCopied;
		//			copy.WriteToServer(dr);
		//		}
		//	}
		//}

		//void copy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
		//{
		//	if (OnRowsCopied != null) { OnRowsCopied(this, new RowsCopiedEventArgs(e)); }
		//}

		// Copy from source to this destination
		public void Copy(TableObject table, IDbData source)
		{
			// delete from destination (this)
			if (CopySettings.DeleteRows) this.Delete(table);

			// select from source
			using (IDataReader dr = source.Select(table))
			{
				// copy to destination (this)
				using (SqlBulkCopy copy = new SqlBulkCopy(this.ConnectionString, this.Options))
				{
					copy.BulkCopyTimeout = CopySettings.BulkCopyTimeout;
					copy.BatchSize = CopySettings.BatchSize;
					copy.DestinationTableName = this.FullTableName(table);
					copy.NotifyAfter = CopySettings.NotifyAfter;					
					//copy.SqlRowsCopied += copy_SqlRowsCopied;
					copy.SqlRowsCopied += table.OnRowsCopied;
					copy.WriteToServer(dr);
				}
			}
		}


		public IDataReader ExecuteReader(string sql)
		{            
			SqlConnection connection = new SqlConnection(this.ConnectionString);
			SqlCommand command = new SqlCommand(sql, connection);
			connection.Open();
			return command.ExecuteReader(CommandBehavior.CloseConnection);
		}


		public int ExecuteNonQuery(string sql)
		{
			using (SqlConnection connection = new SqlConnection(this.ConnectionString))
			{
				SqlCommand command = new SqlCommand(sql, connection);
				connection.Open();
				return command.ExecuteNonQuery();
			}
		}


		public object ExecuteScalar(string sql)
		{
			using (SqlConnection connection = new SqlConnection(this.ConnectionString))
			{
				SqlCommand command = new SqlCommand(sql, connection);
				connection.Open();
				return command.ExecuteScalar();
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
