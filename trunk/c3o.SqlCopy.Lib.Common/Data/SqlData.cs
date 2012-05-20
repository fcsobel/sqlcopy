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
		public CopyObject settings { get; set; }

		public SqlData(CopyObject settings)
		{
			this.settings = settings;
		}

		public SqlBulkCopyOptions Options
		{
			get
			{
				SqlBulkCopyOptions option = SqlBulkCopyOptions.Default;
				if (settings.KeepIdentity) option = option | SqlBulkCopyOptions.KeepIdentity;
				if (settings.KeepNulls) option = option | SqlBulkCopyOptions.KeepNulls;
				if (settings.CheckConstraints) option = option | SqlBulkCopyOptions.CheckConstraints;
				if (settings.FireTriggers) { option = option | SqlBulkCopyOptions.FireTriggers; }
				if (settings.TableLock) { option = option | SqlBulkCopyOptions.TableLock; }
				return option;                
			}
		}

		public IDataReader List()
		{
			return this.ExecuteReader(this.settings.Source, this.settings.ListSql);
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

				if (settings.IncludeSchema)
				{
					//				                            where   '[' + table_schema + '].[' + table_name + ']' = '[{0}].[{1}]'
					string sql = @"select column_name 
							from INFORMATION_SCHEMA.COLUMNS 
							where   table_schema  = '{0}' 
									and table_name = '{1}'
									and not columnproperty(object_id(table_name), COLUMN_NAME, 'IsComputed') = 1";


					using (IDataReader dr = this.ExecuteReader(this.settings.Source, string.Format(sql, table.Schema, table.Name)))
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

					using (IDataReader dr = this.ExecuteReader(this.settings.Source, string.Format(sql, table.Name)))
					{
						while (dr.Read())
						{
							columns.Add(string.Format("[{0}]", dr["COLUMN_NAME"]));
						}
					}
				}

				string select = string.Format("select \r\n{1} \r\nfrom {0}", table.FullName, string.Join(",\r\n", columns.ToArray()));

				return select;
			}
		}


		public IDataReader Select(TableObject table)
		{
			string sql = this.GetSelectSql(table);

			//return this.ExecuteReader(this.settings.Source, string.Format(settings.SelectSql, table));
			return this.ExecuteReader(this.settings.Source, string.Format(sql));
		}

		public void Delete(TableObject table)
		{
			this.ExecuteNonQuery(this.settings.Destination, string.Format(settings.DeleteSql, table.FullName));
		}

		public void PreCopy()
		{
			if (!string.IsNullOrEmpty(this.settings.PreCopySql))
			{
				this.ExecuteNonQuery(this.settings.Destination, this.settings.PreCopySql);
			}
		}


		public void PostCopy()
		{
			if (!string.IsNullOrEmpty(this.settings.PostCopySql))
			{

				this.ExecuteNonQuery(this.settings.Destination, this.settings.PostCopySql);
			}
		}


		public void Copy(TableObject table)
		{
			if (settings.DeleteRows) this.Delete(table);

			using (IDataReader dr = this.Select(table))
			{
				using (SqlBulkCopy copy = new SqlBulkCopy(settings.Destination, this.Options))
				{
					copy.BulkCopyTimeout = settings.BulkCopyTimeout;
					copy.BatchSize = settings.BatchSize;
					copy.DestinationTableName = table.FullName;
					copy.WriteToServer(dr);
				}
			}
		}


		public void Copy(TableObject table, IDbData source)
		{
			if (settings.DeleteRows) this.Delete(table);

			using (IDataReader dr = source.Select(table))
			{
				using (SqlBulkCopy copy = new SqlBulkCopy(settings.Destination, this.Options))
				{
					copy.BulkCopyTimeout = settings.BulkCopyTimeout;
					copy.BatchSize = settings.BatchSize;
					copy.DestinationTableName = table.FullName;
					copy.WriteToServer(dr);
				}
			}
		}


		public IDataReader ExecuteReader(string db, string sql)
		{            
			SqlConnection connection = new SqlConnection(db);
			SqlCommand command = new SqlCommand(sql, connection);
			connection.Open();
			return command.ExecuteReader(CommandBehavior.CloseConnection);
		}


		public int ExecuteNonQuery(string db, string sql)
		{
			using (SqlConnection connection = new SqlConnection(db))
			{
				SqlCommand command = new SqlCommand(sql, connection);
				connection.Open();
				return command.ExecuteNonQuery();
			}
		}
	}
}
