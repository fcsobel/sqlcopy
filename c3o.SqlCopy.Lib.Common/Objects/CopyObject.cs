using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;
using c3o.SqlCopy.Data;
using Oracle.DataAccess.Client;
using System.Windows.Forms;
using System.ComponentModel;

namespace c3o.SqlCopy.Objects
{
	public enum CopyStatusEnum { Copying, Success, Error, Skipped }

	[Serializable()]
	public class TableObject
	{
		[XmlIgnore]
		public CopyObject Parent { get; set; }
		public string Schema { get; set; }
		public string Name { get; set; }
		public string Message { get; set; }
		public bool Selected { get; set; }

		[XmlIgnore]
		public string Status
		{
			get
			{
				if (CopyStatus.HasValue)
				{
					switch (this.CopyStatus.Value)
					{
						case CopyStatusEnum.Copying:
							return string.Format("{0} {1} out of {2}", this.CopyStatus, this.Copied, this.Count);
						case CopyStatusEnum.Success:
							return string.Format("{0} {1} out of {2}", this.CopyStatus, this.Copied, this.Count);
						case CopyStatusEnum.Error:
							return string.Format("{0} {1} out of {2} - {3}", this.CopyStatus, this.Copied, this.Count, this.Message);
						default:
							return string.Format("{0}", this.CopyStatus.Value);
					}
				}
				return "";
			}
		}

		public string Sql { get; set; }
		public long Count { get; set; }
		public long Copied { get; set; }

		public CopyStatusEnum? CopyStatus { get; set; }

		[XmlIgnore]
		public DataGridViewRow Row { get; set; }
		[XmlIgnore]
		public BackgroundWorker Worker { get; set; }

		[XmlIgnore]
		public int Percentage 
		{ 
			get
			{ 
				return this.CopyStatus == CopyStatusEnum.Success ? 100 : this.Copied == 0 || this.Count == 0 ? 0 : (int) Math.Round((decimal) (((decimal)this.Copied / (decimal)this.Count) * 100), 0); 
			} 
		}

		//public int Percentage { get; set; }
		//public string Percentage2 { get; set; }

		public void ShowProgress()
		{
			//this.Status = string.Format("{0} {1} out of {2} - {3}", this.CopyStatus, this.Copied, this.Count, this.Percentage);

			if (Worker != null)
			{
				Worker.ReportProgress(this.Percentage, this);
			}
			else
			{
				if (Row != null)
				{


					//this.Status = this.Status + this.Percentage.ToString();
					var parent = Row.DataGridView;
					parent.CurrentCell = Row.Cells[3];
					parent.UpdateCellValue(3, Row.Index);
					//Row.Cells[4].Value = this.Percentage;
					parent.CurrentCell = Row.Cells[4];
					parent.UpdateCellValue(4, Row.Index);
				}
			}
		}

		//public void OnShowProgress()
		//{
		//	if (Row != null)
		//	{
		//		int i = (int) Math.Round((decimal) this.Copied / this.Count, 0) * 100;
		//		Row.Cells[5].Value = i;
		//		var parent = Row.DataGridView;
		//		parent.CurrentCell = Row.Cells[3];
		//		parent.UpdateCellValue(2, Row.Index);		
		//	}
		//}

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

		public void OnRowsCopied(object sender, OracleRowsCopiedEventArgs e)
		{
			this.Copied = e.RowsCopied;
			//this.Status = string.Format("Copying {0} out of {1} - {2}" , this.Copied, this.Count, this.Percentage);
			this.ShowProgress();
		}

		public void OnRowsCopied(object sender, SqlRowsCopiedEventArgs e)
		{
			this.Copied = e.RowsCopied;
			//this.Status = string.Format("Copying {0} out of {1} - {2}" , this.Copied, this.Count, this.Percentage);
			this.ShowProgress();
		}

		public void OnRowsCopied(object sender, RowsCopiedEventArgs e)
		{
			this.Copied = e.RowsCopied;
			//this.Status = "Copying " + this.Copied.ToString();
			this.ShowProgress();
		}

		public void OnSuccess(object sender)
		{
			//this.Status = string.Format("Success - Copied {0} rows out of {1}", this.Copied, this.Count);
			this.ShowProgress();
		}

		public TableObject() {
		
		}
		public TableObject(CopyObject parent) { 
			this.Parent = parent; 
		}



	}


	[Serializable()]
	public class CopyObject
	{
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
		public string CountSql { get; set; }

		//"select * from {0}"
		public string SelectSql { get; set; }

		public List<TableObject> Tables { get; set; }

		public static CopyObject Read(string filename)
		{
			CopyObject obj = SerializationHelper.Deserialize<CopyObject>(filename);

			if (obj.Tables != null && obj.Tables.Count > 0)
			{
				foreach (TableObject t in obj.Tables) { t.Parent = obj; }
			}
			return obj;
		}


		public static CopyObject GetTemplate(DBMS dbms)
		{
			string templateFile = string.Format(@"{0}\config\template.xml", new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName);
			if (File.Exists(templateFile))
			{
				List<CopyObject> templates = SerializationHelper.Deserialize<List<CopyObject>>(templateFile);

				if (templates != null && templates.Count > 0)
				{
					return templates.Find(hit => hit.SourceType == dbms);
				}
				else
				{
					throw new Exception(string.Format("DBMS: {0} not found in {1}", dbms, templateFile));
				}
			}
			else
			{
				throw new Exception(string.Format("File not found: {0}", templateFile));
			}
		}
	}

	public enum DBMS
	{
		SqlServer,
		Oracle
	}




}
