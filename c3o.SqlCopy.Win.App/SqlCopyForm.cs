using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using c3o.SqlCopy.Objects;
using c3o.SqlCopy.Data;
using System.IO;
using System.Linq;

namespace c3o.SqlCopy
{
	public partial class SqlCopyForm : Form
	{
		bool sortName { get; set; }
		bool sortStatus { get; set; }
		bool sortChecked { get; set; }

		public string _FileName;
		public string FileName 
		{
			get 
			{
				return this._FileName;
			}
			set 
			{
				this._FileName = value;
				this.Text = string.Format("Copy Window - {0}", value); 
			}
		}

		
		//public List<CopyObject> list { get; set; }
		private CopyObject CurrentObj { get; set; }

		private List<TableObject> Tables 
		{
			get
			{
				List<TableObject> list = new List<TableObject>();
				foreach (DataGridViewRow row in this.dataGridView1.Rows)
				{
					TableObject obj = (TableObject) row.DataBoundItem;
					obj.Selected = (bool)row.Cells[0].Value;
					list.Add(obj);
				}
				return list;
			}
			set
			{
				this.dataGridView1.AutoGenerateColumns = false;
				this.dataGridView1.DataSource = value;
			}
		}

		public CopyObject Settings
		{
			get
			{
				DBMS dbms = (DBMS)this.cboSource.SelectedItem;

				if (this.CurrentObj == null)
				{
					this.CurrentObj = CopyObject.GetTemplate(dbms);
				}

				CopyObject obj = this.CurrentObj;

				if (obj != null)
				{

					//obj.FileName = this.FileName;
					obj.SourceType = (DBMS)this.cboSource.SelectedItem;
					obj.DestinationType = (DBMS)this.cboDestintaion.SelectedItem;
					obj.BatchSize = this.BatchSize;
					obj.BulkCopyTimeout = this.BulkCopyTimeout;
					obj.CheckConstraints = this.cbxCheckConstraints.Checked;
					obj.DeleteRows = this.cbxDeleteRows.Checked;
					obj.IncludeSchema = this.cbxSchema.Checked;
					obj.Destination = this.Destination;
					//obj.DestinationPartitionName = "";
					//obj.DestinationTableName = "";

					obj.FireTriggers = this.cbxFireTriggers.Checked;
					obj.KeepIdentity = this.cbxKeepIdentity.Checked;
					obj.KeepNulls = this.cbxKeepNulls.Checked;
					obj.NotifyAfter = 0;

					obj.Source = this.Source;
					obj.TableLock = cbxTableLock.Checked;
					obj.UseInternalTransaction = false;
					obj.Tables = this.Tables;
				}
				return obj;
			}
			set
			{
				this.CurrentObj = value;
				//this.FileName = value.FileName;
				this.cboSource.SelectedItem = value.SourceType;
				this.cboDestintaion.SelectedItem = value.DestinationType;
				this.txtBatchSize.Text = value.BatchSize.ToString();
				this.txtTimeout.Text = value.BulkCopyTimeout.ToString();
				this.cbxCheckConstraints.Checked = value.CheckConstraints;
				this.cbxFireTriggers.Checked = value.FireTriggers;
				this.cbxKeepIdentity.Checked = value.KeepIdentity;
				this.cbxKeepNulls.Checked = value.KeepNulls;
				this.cbxTableLock.Checked = value.TableLock;
				this.cbxDeleteRows.Checked = value.DeleteRows;
				this.cbxSchema.Checked = value.IncludeSchema;
			   // this.btnSql.Enabled = Properties.Settings.Default.DeleteRows;
				this.txtSource.Text = value.Source;
				this.txtDestination.Text = value.Destination;

				//this.comboBox1.SelectedItem = obj.SourceType = (DBMS)this.comboBox1.SelectedItem;
				//this.comboBox1.DataSource = System.Enum.GetValues(typeof(DBMS));

				this.Tables = value.Tables;

			}
		}

		public string Source { get { return this.txtSource.Text; } }
		public string Destination { get { return this.txtDestination.Text; } }
		public int BulkCopyTimeout { get { return int.Parse(this.txtTimeout.Text); } }
		public int BatchSize { get { return int.Parse(this.txtBatchSize.Text); } }

		public SqlCopyForm()
		{
			InitializeComponent();

			this.cboSource.DataSource = System.Enum.GetValues(typeof(DBMS));
			this.cboDestintaion.DataSource = System.Enum.GetValues(typeof(DBMS));
		}

		

		private void bttnRefresh_Click(object sender, EventArgs e)
		{
			CopyObject copy = this.Settings;

			try
			{
				CopyManager manager = new CopyManager(copy);

				if (this.sortName)
				{
					copy.Tables = manager.List().OrderBy(x => x.Schema).OrderBy(x => x.Name).ToList();
				}
				else
				{
					copy.Tables = manager.List().OrderByDescending(x => x.Schema).OrderByDescending(x => x.Name).ToList();
				}
				this.sortName = !this.sortName;

				this.Settings = copy;

				// Save Objects to disk
				//this.SaveList();                
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Connecting to Source");
			}
		}

		
		private void bttnCopy_Click(object sender, EventArgs e)
		{
			//this.CurrentObj = this.Settings;

			CopyObject settings = this.Settings;

			foreach (TableObject obj in settings.Tables)
			{
				obj.Status = "";
			}
			
			this.dataGridView1.FirstDisplayedScrollingRowIndex = 0;
			this.Tables = settings.Tables;

			// this.dataGridView1.Refresh();

			// Copy tables  
			this.CopyTablesAsc();
		}

 
		public void CopyTablesAsc()
		{
			this.backgroundWorker1.RunWorkerAsync(this.Settings);
		}

		void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			// Save Objects to disk
			//SerializationHelper.Serialize<List<CopyObject>>(this.list, @"config\list.xml");
		}

		public void ShowProgress(object sender, ProgressChangedEventArgs e)
		{
			BackgroundWorker worker = (BackgroundWorker)sender;

			if (e.UserState != null)
			{
				DataGridViewRow row = e.UserState as DataGridViewRow;

				if (row != null)
				{
					this.dataGridView1.CurrentCell = row.Cells[3];
					this.dataGridView1.UpdateCellValue(2, row.Index);
				}
			}
		}

		// Background Version
		public void CopyTables(object sender, DoWorkEventArgs e) 
		{
			//CopyObject settings = this.Settings;

			CopyObject settings = e.Argument as CopyObject;

			CopyManager manager = new CopyManager(settings); ;

			BackgroundWorker worker = (BackgroundWorker)sender;

			try
			{
				if (this.cbxDeleteRows.Checked)
				{
					manager.PreCopy();
				}
			}
			catch (Exception er)
			{
				MessageBox.Show(er.Message, "Pre SQL Error");
				return;
			}

			foreach (DataGridViewRow row in this.dataGridView1.Rows)
			{
				TableObject obj = (TableObject)row.DataBoundItem;

				if (worker.CancellationPending)
				{
					break;
				}
				try
				{
					if (obj.Selected)
					{
						manager.Copy(obj);
						obj.Status = "Success";
						worker.ReportProgress(0, row);
					}
				}
				catch (Exception er)
				{
					obj.Status = er.Message;
					//worker.ReportProgress(0, er);
					worker.ReportProgress(0, row);
				}
				finally
				{
				}
			}

			try
			{
				if (this.cbxDeleteRows.Checked)
				{
					manager.PostCopy();
				}
			}
			catch (Exception er)
			{
				MessageBox.Show(er.Message, "Post SQL Error");
				return;
			}
		}
		

		
		private void bttnSelectAll_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in this.dataGridView1.Rows)
			{
				row.SetValues(true);
			}

		}

		private void bttnDeselectAll_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in this.dataGridView1.Rows)
			{
				row.SetValues(false);
			}
		}

		private void bttnFlipSelect_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in this.dataGridView1.Rows)
			{
				bool status = (bool) row.Cells[0].Value;
				row.SetValues(!status);
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			string filename = Properties.Settings.Default.FileName;

			if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
			{
				this.FileName = filename;
				this.Settings = CopyObject.Read(this.FileName);

				// get latest template values
				this.RefreshSource();
				this.RefreshDestination();
			}
		}

		private void cbxDeleteRows_Click(object sender, EventArgs e)
		{
			this.btnSql.Enabled = this.cbxDeleteRows.Checked;
			
			if (cbxDeleteRows.Checked)
			{
				string message = "This option will attempt to delete ALL rows from the tables you have selected!";

				if (!cbxFireTriggers.Checked || !cbxCheckConstraints.Checked)
				{
					if (!cbxFireTriggers.Checked) 
					{
						message += "\n\r\n\rThe following SQL will be executed to disable triggers:";
						message += "\n\r\n\r\texec sp_msforeachtable 'ALTER TABLE ? DISABLE TRIGGER all'";
					}

					if (!cbxCheckConstraints.Checked)
					{
						message += "\n\r\n\rThe following SQL will be executed to disable constraints:";
						message += "\n\r\n\r\texec sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'";
					}
					MessageBox.Show(message, "Warning!");
				}
			}
		}

		private void btnSql_Click(object sender, EventArgs e)
		{
			EditSql();
		}

		private void EditSql()
		{
			SqlEditForm form = new SqlEditForm();
			form.ShowDialog(this);
		}


		private void button1_Click_1(object sender, EventArgs e)
		{
			this.Close();
		}

		private void bttnSave_Click(object sender, EventArgs e)
		{
			//this.SaveList();
			this.Close();
		}

		private void SqlCopyForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			//this.CurrentObj = this.Settings;
			//this.SaveList();
		}

		private void button1_Click_2(object sender, EventArgs e)
		{
			this.openFileDialog1.ShowDialog();
		}

		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			this.FileName = this.openFileDialog1.FileName;

			this.Settings = CopyObject.Read(this.FileName);

			// get latest template values
			this.RefreshSource();
			this.RefreshDestination();

			Properties.Settings.Default.FileName = this.FileName;
			Properties.Settings.Default.Save();

			//SerializationHelper.Serialize<List<CopyObject>>(this.list, @"config\list.xml");
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Save();
		}


		private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			this.FileName = this.saveFileDialog1.FileName;

			SerializationHelper.Serialize<CopyObject>(this.Settings, this.saveFileDialog1.FileName);

			Properties.Settings.Default.FileName = this.FileName;
			Properties.Settings.Default.Save();

		}

		private void Save()
		{
			if (string.IsNullOrEmpty(this.FileName))
			{
				this.saveFileDialog1.ShowDialog();
			}
			else
			{
				SerializationHelper.Serialize<CopyObject>(this.Settings, this.FileName);        
			}
		}

		private void bttnSaveAs_Click(object sender, EventArgs e)
		{
			this.saveFileDialog1.ShowDialog();
		}

			

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
			{				
			}
			else
			{
				if (e.ColumnIndex == 4)
				{
					DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

					TableObject obj = (TableObject)row.DataBoundItem;

					if (obj != null)
					{
						TableEditForm form = new TableEditForm();
						form.Table = obj;
						form.Settings = this.Settings;
						form.ShowDialog(this);
					}
				}
			}
		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
			{
				CopyObject obj = this.Settings;

				switch (e.ColumnIndex)
				{
					case 0: // checked
						if (this.sortChecked) { obj.Tables = obj.Tables.OrderByDescending(x => x.Selected).ToList(); }
						else { obj.Tables = obj.Tables.OrderBy(x => x.Selected).ToList(); }
						this.sortChecked = !this.sortChecked;
						break;
					case 1:	// schema
						if (this.sortName) { obj.Tables = obj.Tables.OrderByDescending(x => x.Schema).ToList(); }
						else { obj.Tables = obj.Tables.OrderBy(x => x.Schema).ToList(); }
						this.sortName = !this.sortName;
						break;
					case 2:	// name
						if (this.sortName) { obj.Tables = obj.Tables.OrderByDescending(x => x.Name).ToList(); }
						else { obj.Tables = obj.Tables.OrderBy(x => x.Name).ToList(); }
						this.sortName = !this.sortName;
						break;
					case 3: // status
						if (this.sortStatus) { obj.Tables = obj.Tables.OrderByDescending(x => x.Status).ToList(); }
						else { obj.Tables = obj.Tables.OrderBy(x => x.Status).ToList(); }
						this.sortStatus = !this.sortStatus;
						break;
				}
				this.Settings = obj;
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.RefreshSource();
		}

		private void cboDestintaion_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.RefreshDestination();
		}

		public void RefreshSource()
		{
			DBMS dbms = (DBMS)this.cboSource.SelectedItem;

			// load dbms tenplate values from xml
			CopyObject template = CopyObject.GetTemplate(dbms);

			if (template != null && this.CurrentObj != null)
			{
				this.CurrentObj.SelectSql = template.SelectSql;
				this.CurrentObj.ListSql = template.ListSql;
				this.CurrentObj.SchemaFormat = template.SchemaFormat;
			}
		}

		public void RefreshDestination()
		{
			DBMS dbms = (DBMS)this.cboDestintaion.SelectedItem;

			// load dbms tenplate values from xml
			CopyObject template = CopyObject.GetTemplate(dbms);

			if (template != null && this.CurrentObj != null)
			{
				this.CurrentObj.PostCopySql = template.PostCopySql;
				this.CurrentObj.PreCopySql = template.PreCopySql;
				this.CurrentObj.DeleteSql = template.DeleteSql;
			}
		
		}

		private void bttnSwitch_Click(object sender, EventArgs e)
		{
			// get sources
			string source = this.txtSource.Text;
			DBMS dbms = (DBMS)this.cboSource.SelectedItem;

			// set source 
			this.txtSource.Text = this.txtDestination.Text;
			this.cboSource.SelectedItem = this.cboDestintaion.SelectedItem;

			// set dest
			this.txtDestination.Text = source;
			this.cboDestintaion.SelectedItem = dbms;
		}
			

		//private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		//{
		//     DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
			 
		//    TableObject obj = (TableObject)row.DataBoundItem;

		//    if (obj != null)
		//    {
		//        MessageBox.Show(obj.Name);
		//    }			
		//}
	}
}