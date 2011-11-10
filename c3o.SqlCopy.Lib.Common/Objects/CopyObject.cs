using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace c3o.SqlCopy.Objects
{
	[Serializable()]
	public class TableObject
	{
		[XmlIgnore]
		public CopyObject Parent { get; set; }
		public string Schema { get; set; }
		public string Name { get; set; }
		public bool Selected { get; set; }
		public string Status { get; set; }
		public string Sql { get; set; }

		[XmlIgnore]
		public string FullName 
		{
			get
			{
				if (this.Parent.IncludeSchema)
				{
					if (string.IsNullOrEmpty(this.Parent.SchemaFormat))
					{
						return string.Format("{0}.{1}", this.Schema, this.Name);
					}
					else
					{
						return string.Format(this.Parent.SchemaFormat, this.Schema, this.Name);
					}
				}
				else
				{
					return this.Name;

				}
			}
		}
		
		public TableObject() { }
		public TableObject(CopyObject parent) { this.Parent = parent; }

	}


	[Serializable()]
	public class CopyObject
	{
		//public string GetTableName(string schema, string table)
		//{
		//    if (this.IncludeSchema)
		//    {
		//        if (string.IsNullOrEmpty(this.SchemaFormat))
		//        {
		//            return string.Format("{0}.{1}", schema, table);
		//        }
		//        else
		//        {
		//            return string.Format(this.SchemaFormat, schema, table);
		//        }
		//    }
		//    else
		//    {
		//        return table;
		//    }
		//}

		public bool Selected { get; set; }
		//public string FileName { get; set; }

		public DBMS SourceType { get; set; }
		public string Source { get; set; }

		public DBMS DestinationType { get; set; }
		public string Destination { get; set; }

		public int BatchSize { get; set;}
		public int BulkCopyOptions { get; set; }
		public int BulkCopyTimeout { get; set; }
		public int NotifyAfter { get; set; }
		public int LogLimit { get; set; }

		//public string DestinationTableName { get; set; }
		public bool Default { get; set; }
		public bool UseInternalTransaction { get; set; }  

		// Oracle only        
		public string DestinationPartitionName { get; set; }        

		// Sql server only
		public bool CheckConstraints { get; set; }        
		public bool FireTriggers { get; set; }
		public bool KeepIdentity { get; set; }
		public bool KeepNulls { get; set; }
		public bool TableLock { get; set; }

		public bool IncludeSchema { get; set; }
		public string SchemaFormat { get; set; }

		// Custom
		public bool DeleteRows { get; set; }

		// Sql
		//Disable Constraints for all tables
		//exec sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
		//exec sp_msforeachtable "ALTER TABLE ? DISABLE TRIGGER all"
		//string sql = "exec sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; exec sp_msforeachtable 'ALTER TABLE ? DISABLE TRIGGER all'; ";
		public string PreCopySql { get; set; }
		public string PreCopyStatus { get; set; }

		//Turn constraints and triggers back on
		//exec sp_msforeachtable @command1="print '?'", @command2="ALTER TABLE ? CHECK CONSTRAINT all"
		//exec sp_msforeachtable @command1="print '?'", @command2="ALTER TABLE ? ENABLE TRIGGER all"
		//string sql = "exec sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all'; exec sp_msforeachtable 'ALTER TABLE ? ENABLE TRIGGER all'; ";
		public string PostCopySql { get; set; }
		public string PostCopyStatus { get; set; }

		//@"SELECT '[' + table_schema + '].[' + table_name + ']' as table_name FROM information_schema.tables";
		public string ListSql { get; set; }

		//delete from {0};"
		public string DeleteSql { get; set; }

		//"select * from {0}"
		public string SelectSql { get; set; }

		public List<TableObject> Tables { get; set; }
	}

	public enum DBMS
	{
		SqlServer,
		Oracle
	}

}
