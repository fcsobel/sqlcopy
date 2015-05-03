using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
//using System.Data.SqlClient;
using c3o.SqlCopy.Objects;
using IndiansInc;
using MySql.Data.MySqlClient;
using IndiansInc.Internals;

namespace c3o.SqlCopy.Data
{
	// MySql
	// Date Time Conversion issues
	//http://stackoverflow.com/questions/5754822/unable-to-convert-mysql-date-time-value-to-system-datetime
	//You must add Convert Zero Datetime=True to your connection string, for example:
	// added both "Convert Zero Datetime=True" & "Allow Zero Datetime=True" and it works fine

	// Use Datetime2 in sql server
	//http://stackoverflow.com/questions/1334143/sql-server-datetime2-vs-datetime

	public class MySqlData : IDbData 
	{
		//public event RowsCopiedEventHandler OnRowsCopied;
		
		public string ConnectionString { get; set; }
		public CopyObject CopySettings { get; set; }

		
		public MySqlData(string connectionString, CopyObject settings)
		{
			this.ConnectionString = connectionString;
			this.CopySettings = settings;
		}

		public System.Data.SqlClient.SqlBulkCopyOptions Options
		{
			get
			{
				System.Data.SqlClient.SqlBulkCopyOptions option = System.Data.SqlClient.SqlBulkCopyOptions.Default;
				if (CopySettings.KeepIdentity) option = option | System.Data.SqlClient.SqlBulkCopyOptions.KeepIdentity;
				if (CopySettings.KeepNulls) option = option | System.Data.SqlClient.SqlBulkCopyOptions.KeepNulls;
				if (CopySettings.CheckConstraints) option = option | System.Data.SqlClient.SqlBulkCopyOptions.CheckConstraints;
				if (CopySettings.FireTriggers) { option = option | System.Data.SqlClient.SqlBulkCopyOptions.FireTriggers; }
				if (CopySettings.TableLock) { option = option | System.Data.SqlClient.SqlBulkCopyOptions.TableLock; }
				return option;                
			}
		}

		public IDataReader List()
		{
			return this.ExecuteReader(this.CopySettings.ListSql);
		}

		// Return Database formatted select 
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

		
//		public string GetSelectSql(TableObject table)
//		{
//			if (!string.IsNullOrEmpty(table.Sql))
//			{
//				return table.Sql.Replace("\r\n", "\n").Replace("\n", "\r\n");
//			}
//			else
//			{
//				List<string> columns = new List<string>();

//				if (settings.IncludeSchema)
//				{
//					//				                            where   '[' + table_schema + '].[' + table_name + ']' = '[{0}].[{1}]'
//					string sql = @"select column_name 
//							from INFORMATION_SCHEMA.COLUMNS 
//							where   table_schema  = '{0}' 
//									and table_name = '{1}'
//									and not columnproperty(object_id(table_name), COLUMN_NAME, 'IsComputed') = 1";


//					using (IDataReader dr = this.ExecuteReader(this.settings.Source, string.Format(sql, table.Schema, table.Name)))
//					{
//						while (dr.Read())
//						{
//							columns.Add(string.Format("[{0}]", dr["COLUMN_NAME"]));
//						}
//					}
//				}
//				else
//				{
//					string sql = @"select column_name 
//							from INFORMATION_SCHEMA.COLUMNS 
//							where   table_name  = '{0}' 
//									and not columnproperty(object_id(table_name), COLUMN_NAME, 'IsComputed') = 1";

//					using (IDataReader dr = this.ExecuteReader(this.settings.Source, string.Format(sql, table.Name)))
//					{
//						while (dr.Read())
//						{
//							columns.Add(string.Format("[{0}]", dr["COLUMN_NAME"]));
//						}
//					}
//				}

//				string select = string.Format("select \r\n{1} \r\nfrom {0}", this.FullTableName(table), string.Join(",\r\n", columns.ToArray()));

//				return select;
//			}
//		}


		public IDataReader Select(TableObject table)
		{
			string sql = this.GetSelectSql(table);

			//return this.ExecuteReader(this.settings.Source, string.Format(settings.SelectSql, table));
			return this.ExecuteReader(string.Format(sql));
		}

		public void Delete(TableObject table)
		{
			this.ExecuteNonQuery(string.Format(CopySettings.DeleteSql, table.Name));
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

		//	using (IDataReader dr = (IDataReader)this.Select(table))
		//	{
		//		MySqlBulkCopy copy = new MySqlBulkCopy(CopySettings.Destination, this.Options);
		//		//MySqlBulkCopy copy = new MySqlBulkCopy();
		//		//copy.BatchSize = settings.BatchSize;
		//		copy.DestinationTableName = this.FullTableName(table);
		//		copy.DestinationTableName = copy.DestinationTableName.Replace("[", "");
		//		copy.DestinationTableName = copy.DestinationTableName.Replace("]", "");

		//		//// map all items
		//		//ColumnMapItemCollection collection = new ColumnMapItemCollection();
		//		//for (int i = 0; i < dr.FieldCount; i++ )
		//		//{
		//		//	dr.GetDataTypeName(i);
		//		//	ColumnMapItem item = new ColumnMapItem();
		//		//	item.DataType = dr.GetDataTypeName(i);
		//		//	item.DestinationColumn = dr.GetName(i);
		//		//	item.SourceColumn = dr.GetName(i);
		//		//	collection.Add(item);
		//		//}				
		//		//copy.ColumnMapItems = collection;

		//		//copy.DestinationDbConnection = connection;

		//		//copy.OnBatchSizeCompleted +=		
		//		//copy.OnBatchSizeCompleted = new IndiansInc.MySqlBulkCopy.OnBatchSizeCompletedDelegate(this.copy_SqlRowsCopied);

		//		//OnBatchSizeCompletedDelegate test = copy_SqlRowsCopied;

		//		//copy.OnBatchSizeCompleted += copy_OnBatchSizeCompleted;

		//		copy.SqlRowsCopied += table.OnRowsCopied;

		//		//copy.BulkCopyTimeout = settings.BulkCopyTimeout;

		//		copy.NotifyAfter = CopySettings.NotifyAfter;
		//		//copy.SqlRowsCopied += copy_SqlRowsCopied;					 
		//		//copy.WriteToServer(dr);
		//		copy.WriteToServer(dr);
		//	}
		//}

		//void copy_SqlRowsCopied(System.Data.SqlClient.SqlRowsCopiedEventArgs e)
		//{
		//	throw new NotImplementedException();
		//}

		//void copy_OnBatchSizeCompleted(BatchSizeCompletedEventArgs e)
		//{
		//	if (OnRowsCopied != null) { OnRowsCopied(this, new RowsCopiedEventArgs(e)); }
		//}

		//void copy_SqlRowsCopied(object sender, BatchSizeCompletedEventArgs e)
		//{
		//	if (OnRowsCopied != null) { OnRowsCopied(this, new RowsCopiedEventArgs(e)); }
		//}


		// Copy from source to destination (this = destination)
		public void Copy(TableObject table, IDbData source)
		{
			// delete from destination (this)
			if (CopySettings.DeleteRows) this.Delete(table);

			// seletc from source
			using (IDataReader dr = source.Select(table))
			{
				// copy to destination this
				using (MySqlBulkCopy copy = new MySqlBulkCopy(this.ConnectionString, this.Options))
				{

					//MySqlBulkCopy copy = new MySqlBulkCopy();
					copy.BatchSize = CopySettings.BatchSize;

					copy.DestinationTableName = this.FullTableName(table);
					copy.DestinationTableName = copy.DestinationTableName.Replace("[", "");
					copy.DestinationTableName = copy.DestinationTableName.Replace("]", "");
					
					// map all items
					ColumnMapItemCollection collection = new ColumnMapItemCollection();
					for (int i = 0; i < dr.FieldCount; i++)
					{
						dr.GetDataTypeName(i);
						ColumnMapItem item = new ColumnMapItem();
						item.DataType = dr.GetDataTypeName(i);
						item.DestinationColumn = dr.GetName(i);
						item.SourceColumn = dr.GetName(i);
						collection.Add(item);
					}
					copy.ColumnMappings = collection;

					//copy.DestinationDbConnection = connection;
					//copy.BulkCopyTimeout = settings.BulkCopyTimeout;
					copy.NotifyAfter = CopySettings.NotifyAfter;
					//copy.SqlRowsCopied += table.OnRowsCopied;


					//copy.WriteToServer(dr);
					copy.WriteToServer(dr);
				}
			}
		}


		public IDataReader ExecuteReader(string sql)
		{            
			MySqlConnection connection = new MySqlConnection(this.ConnectionString);
			MySqlCommand command = new MySqlCommand(sql, connection);
			connection.Open();
			return command.ExecuteReader(CommandBehavior.CloseConnection);
		}


		public int ExecuteNonQuery(string sql)
		{
			using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
			{
				MySqlCommand command = new MySqlCommand(sql, connection);
				connection.Open();
				return command.ExecuteNonQuery();
			}
		}


		public object ExecuteScalar(string sql)
		{
			using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
			{
				MySqlCommand command = new MySqlCommand(sql, connection);
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
