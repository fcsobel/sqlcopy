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
using System.Threading.Tasks;

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
				//ShowP();
			}
		}


		//public CopyObject GetDbms(DBMS dbms)
		//{
		//    string templateFile = string.Format(@"{0}\config\template.xml", new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName);
		//    if (File.Exists(templateFile))
		//    {
		//        List<CopyObject> templates = SerializationHelper.Deserialize<List<CopyObject>>(templateFile);
				
		//        if (templates != null && templates.Count > 0)
		//        {
		//            return templates.Find(hit => hit.SourceType == dbms);
		//        }
		//        else
		//        {
		//            throw new Exception(string.Format("DBMS: {0} not found in {1}", dbms, templateFile));
		//        }
		//    }
		//    else
		//    {
		//        throw new Exception(string.Format("File not found: {0}", templateFile));
		//    }
		//}


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
					obj.NotifyAfter = 1;

					//obj.ListSql = "";
					//obj.PostCopySql = "";
					//obj.PreCopySql = "";
					//obj.SelectSql = "";
					//obj.DeleteSql = "";

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

			//DataGridViewProgressColumn column = new DataGridViewProgressColumn();

			////this.dataGridView1.ColumnCount = this.dataGridView1.ColumnCount + 1;
			//this.dataGridView1.Columns.Add(column);
			////this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			//column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; 
			//column.HeaderText = "Progress";
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

		//public void SaveList()
		//{
		//    // Save Objects to disk
		//    SerializationHelper.Serialize<List<CopyObject>>(this.list, @"config\list.xml");

		//}

		private void bttnCopy_Click(object sender, EventArgs e)
		{
			CopyObject settings = this.Settings;

			foreach (TableObject obj in settings.Tables)
			{
				//obj.Status = "";
				obj.CopyStatus = null;
			}

			this.dataGridView1.FirstDisplayedScrollingRowIndex = 0;
			this.Tables = settings.Tables;

			// this.dataGridView1.Refresh();

			// Copy tables  
			//this.CopyTablesAsc();

			this.CopyTablesAsync(settings);
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

		//public void ShowP()
		//{
		//	foreach (DataGridViewRow row in this.dataGridView1.Rows)
		//	{
		//		TableObject obj = row.DataBoundItem as TableObject;
		//		row.Cells[4].Value = obj.Perce;
		//		var parent = row.DataGridView;
		//		parent.CurrentCell = row.Cells[3];
		//		parent.UpdateCellValue(2, row.Index);

		//	}
		//}

		public void ShowProgress(object sender, ProgressChangedEventArgs e)
		{
			//BackgroundWorker worker = (BackgroundWorker)sender;

			if (e.UserState != null)
			{
				TableObject table = e.UserState as TableObject;
				//this.ShowProgress(table);
				table.ShowProgress();
			}
		}

		//public void ShowProgress(TableObject table)
		//{
		//	var Row = table.Row;
		//	//table.ShowProgress();

		//	//DataGridViewRow row = e.UserState as DataGridViewRow;

		//	//if (row != null)
		//	//{
		//	//   // this.dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
		//	//	//this.dataGridView1.Refresh();					
		//	//	this.dataGridView1.CurrentCell = row.Cells[3];
		//	//	this.dataGridView1.UpdateCellValue(2, row.Index);
		//	//}
		//	if (Row != null)
		//	{
		//		//int i = (int) Math.Round((decimal) this.Copied / this.Count, 0) * 100;
		//		//Row.Cells[4].Value = e.ProgressPercentage;
		//		Row.Cells[4].Value = table.Percentage;
		//		var parent = Row.DataGridView;
		//		parent.CurrentCell = Row.Cells[3];
		//		parent.UpdateCellValue(2, Row.Index);
		//	}
		//}


		public async void CopyTablesAsync(CopyObject settings)
		{
			CopyManager manager = new CopyManager(settings);

			manager.OnRowsCopied +=manager_OnRowsCopied;

			try
			{
				if (this.cbxDeleteRows.Checked)
				{
					await Task.Run(() => manager.PreCopy());
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

				// prepare
				obj.Row = row;
				//obj.Worker = worker;
				//worker.ReportProgress(0, obj);

				//if (worker.CancellationPending)
				//{
				//	break;
				//}
				try
				{
					if (obj.Selected)
					{
						await Task.Run(() => manager.Copy(obj));
						//manager.Copy(obj);
						//obj.Status = "Success";
						obj.CopyStatus = CopyStatusEnum.Success;
						//this.ShowProgress(obj);
						
						//worker.ReportProgress(0, row);
						//worker.ReportProgress(100, obj);
						//obj.ShowProgress();
					}
					else
					{
						obj.CopyStatus = CopyStatusEnum.Skipped;
					}
					obj.ShowProgress();
				}
				catch (Exception er)
				{
					obj.CopyStatus = CopyStatusEnum.Error;
					//obj.Status = er.Message;
					obj.Message = er.Message;
					//this.ShowProgress(obj);
					obj.ShowProgress();
					//worker.ReportProgress(0, er);
					//worker.ReportProgress(0, row);
					//worker.ReportProgress(0, obj);
					//obj.ShowProgress();
				}
				finally
				{
				}


			}

			//foreach (TableObject obj in settings.Tables)
			//{
			//    if (worker.CancellationPending)
			//    {
			//        break;
			//    }
			//    try
			//    {
			//        if (obj.Selected)
			//        {
			//            manager.Copy(obj.Name);
			//            obj.Status = "Success";
			//            worker.ReportProgress(0);
			//        }
			//    }
			//    catch (Exception er)
			//    {
			//        obj.Status = er.Message;
			//        worker.ReportProgress(0, er);
			//    }
			//    finally
			//    {
			//    }
			//}

			try
			{
				if (this.cbxDeleteRows.Checked)
				{
					await Task.Run(() => manager.PostCopy());
					//manager.PostCopy();
				}
			}
			catch (Exception er)
			{
				MessageBox.Show(er.Message, "Post SQL Error");
				return;
			}
		}


		// Background Version
		public void CopyTables(object sender, DoWorkEventArgs e) 
		{
			//CopyObject settings = this.Settings;

			CopyObject settings = e.Argument as CopyObject;

			CopyManager manager = new CopyManager(settings);
		
			BackgroundWorker worker = (BackgroundWorker)sender;

			manager.OnRowsCopied +=manager_OnRowsCopied;
			

			//ProgressChanged
			//worker.ReportProgress()

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

				// prepare
				obj.Row = row;
				obj.Worker = worker;
				//worker.ReportProgress(0, obj);

				if (worker.CancellationPending)
				{
					break;
				}
				try
				{
					if (obj.Selected)
					{
						manager.Copy(obj);
						//obj.Status = "Success";
						obj.CopyStatus = CopyStatusEnum.Success;
						//worker.ReportProgress(0, row);
						worker.ReportProgress(100, obj);
						//obj.ShowProgress();
					}
				}
				catch (Exception er)
				{
					obj.CopyStatus = CopyStatusEnum.Error;
					obj.Message = er.Message;
					//obj.Status = er.Message;
					//worker.ReportProgress(0, er);
					//worker.ReportProgress(0, row);
					worker.ReportProgress(0, obj);
					//obj.ShowProgress();
				}
				finally
				{
				}


			}

			//foreach (TableObject obj in settings.Tables)
			//{
			//    if (worker.CancellationPending)
			//    {
			//        break;
			//    }
			//    try
			//    {
			//        if (obj.Selected)
			//        {
			//            manager.Copy(obj.Name);
			//            obj.Status = "Success";
			//            worker.ReportProgress(0);
			//        }
			//    }
			//    catch (Exception er)
			//    {
			//        obj.Status = er.Message;
			//        worker.ReportProgress(0, er);
			//    }
			//    finally
			//    {
			//    }
			//}

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

		void manager_OnRowsCopied(object sender, RowsCopiedEventArgs e)
		{
			string test = "tt";
			//throw new NotImplementedException();
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


		private void CopyForm_Load(object sender, EventArgs e)
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


		//private void bttnClose_Click(object sender, EventArgs e)
		//{
		//	this.Close();
		//}

		//private void bttnSave_Click(object sender, EventArgs e)
		//{
		//	//this.SaveList();
		//	this.Close();
		//}

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
						

		/// <summary>
		/// Save current data to xml file
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bttnSave_Click(object sender, EventArgs e)
		{
			//this.Save();
			if (string.IsNullOrEmpty(this.FileName))
			{
				this.saveFileDialog1.ShowDialog();
			}
			else
			{
				this.SaveAs(this.FileName);
				//SerializationHelper.Serialize<CopyObject>(this.Settings, this.FileName);        
			}
		}


		///// <summary>
		///// Save current data to xml file
		///// </summary>
		//private void Save()
		//{
		//	if (string.IsNullOrEmpty(this.FileName))
		//	{
		//		this.saveFileDialog1.ShowDialog();
		//	}
		//	else
		//	{
		//		this.SaveAs(this.FileName);
		//		//SerializationHelper.Serialize<CopyObject>(this.Settings, this.FileName);        
		//	}
		//}


		/// <summary>
		/// Save As XML dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bttnSaveAs_Click(object sender, EventArgs e)
		{
			this.saveFileDialog1.ShowDialog();
		}

		
		private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			this.SaveAs(this.saveFileDialog1.FileName);
			// set file name 
			//this.FileName = this.saveFileDialog1.FileName;

			// save 
			//SerializationHelper.Serialize<CopyObject>(this.Settings, this.saveFileDialog1.FileName);

			// set file in settings
			//Properties.Settings.Default.FileName = this.FileName;
			//Properties.Settings.Default.Save();
		}


		/// <summary>
		/// Save as file and remember settings
		/// </summary>
		/// <param name="filename"></param>
		private void SaveAs(string filename)
		{ 
			// set file name 
			this.FileName = filename;

			// save 
			SerializationHelper.Serialize<CopyObject>(this.Settings, filename);

			// set file in settings
			Properties.Settings.Default.FileName = this.FileName;
			Properties.Settings.Default.Save();
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


		/// <summary>
		/// Switch Source and Destination
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		/// <summary>
		/// Load source on dropdown selection
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.RefreshSource();
		}

		/// <summary>
		/// Load Destination on dropdown selection
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboDestintaion_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.RefreshDestination();
		}

		/// <summary>
		/// Load Source from XML for current selection
		/// </summary>
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

		/// <summary>
		/// Load Destination from XML based on current selection
		/// </summary>
		public void RefreshDestination()
		{
			DBMS dbms = (DBMS)this.cboDestintaion.SelectedItem;

			// load dbms template values from xml
			CopyObject template = CopyObject.GetTemplate(dbms);

			if (template != null && this.CurrentObj != null)
			{
				this.CurrentObj.PostCopySql = template.PostCopySql;
				this.CurrentObj.PreCopySql = template.PreCopySql;
				this.CurrentObj.DeleteSql = template.DeleteSql;
				this.CurrentObj.CountSql = template.CountSql;
			}
		
		}


	}
}