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

namespace c3o.SqlCopy
{
    public partial class SqlCopyForm : Form
    {
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
                DBMS dbms = (DBMS)this.comboBox1.SelectedItem;

                if (this.CurrentObj == null)
                {
                    string templateFile = string.Format(@"{0}\config\template.xml", new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName );

                    if (File.Exists(templateFile))
                    {
                        List<CopyObject> templates = SerializationHelper.Deserialize<List<CopyObject>>(templateFile);

                        if (templates != null && templates.Count > 0)
                        {
                            this.CurrentObj = templates.Find(hit => hit.SourceType == dbms);

                            //CopyObject temmp = templates.Find(hit => hit.SourceType == obj.SourceType);

                            //if (temmp != null)
                            //{
                            //    if (string.IsNullOrEmpty(obj.DeleteSql)) obj.DeleteSql = temmp.DeleteSql;
                            //    if (string.IsNullOrEmpty(obj.ListSql)) obj.ListSql = temmp.ListSql;
                            //    if (string.IsNullOrEmpty(obj.PostCopySql)) obj.PostCopySql = temmp.PostCopySql;
                            //    if (string.IsNullOrEmpty(obj.PreCopySql)) obj.PreCopySql = temmp.PreCopySql;
                            //    if (string.IsNullOrEmpty(obj.SelectSql)) obj.SelectSql = temmp.SelectSql;
                            //}
                        }
                    }
                }

                CopyObject obj = this.CurrentObj;

                if (obj != null)
                {

                    //obj.FileName = this.FileName;
                    obj.SourceType = (DBMS)this.comboBox1.SelectedItem;
                    obj.DestinationType = (DBMS)this.cboDestintaion.SelectedItem;
                    obj.BatchSize = this.BatchSize;
                    obj.BulkCopyTimeout = this.BulkCopyTimeout;
                    obj.CheckConstraints = this.cbxCheckConstraints.Checked;
                    obj.DeleteRows = this.cbxDeleteRows.Checked;

                    obj.Destination = this.Destination;
                    //obj.DestinationPartitionName = "";
                    //obj.DestinationTableName = "";

                    obj.FireTriggers = this.cbxFireTriggers.Checked;
                    obj.KeepIdentity = this.cbxKeepIdentity.Checked;
                    obj.KeepNulls = this.cbxKeepNulls.Checked;
                    obj.NotifyAfter = 0;

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
                this.comboBox1.SelectedItem = value.SourceType;
                this.cboDestintaion.SelectedItem = value.DestinationType;
                this.txtBatchSize.Text = value.BatchSize.ToString();
                this.txtTimeout.Text = value.BulkCopyTimeout.ToString();
                this.cbxCheckConstraints.Checked = value.CheckConstraints;
                this.cbxFireTriggers.Checked = value.FireTriggers;
                this.cbxKeepIdentity.Checked = value.KeepIdentity;
                this.cbxKeepNulls.Checked = value.KeepNulls;
                this.cbxTableLock.Checked = value.TableLock;
                this.cbxDeleteRows.Checked = value.DeleteRows;
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

            this.comboBox1.DataSource = System.Enum.GetValues(typeof(DBMS));
            this.cboDestintaion.DataSource = System.Enum.GetValues(typeof(DBMS));
        }


        private void bttnRefresh_Click(object sender, EventArgs e)
        {
            CopyObject copy = this.Settings;

            try
            {
                CopyManager manager = new CopyManager(copy);

                copy.Tables = manager.List();

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

        private void button1_Click(object sender, EventArgs e)
        {
            //this.CurrentObj = this.Settings;

            CopyObject settings = this.Settings;

            foreach(TableObject obj in settings.Tables)
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
            this.backgroundWorker1.RunWorkerAsync();
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
                   // this.dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                    //this.dataGridView1.Refresh();
                    
                    this.dataGridView1.CurrentCell = row.Cells[2];
                    this.dataGridView1.UpdateCellValue(2, row.Index);
                }
            }

            //this.dataGridView1.FirstDisplayedScrollingRowIndex = e.ProgressPercentage;

            //if (e.UserState == null)
            //{
            //    this.dataGridView1.Rows[e.ProgressPercentage].Cells[2].Value = "Success";
            //}
            //else
            //{
            //    Exception er = (Exception) e.UserState;

            //    this.dataGridView1.Rows[e.ProgressPercentage].Cells[2].Value = er.Message;
            //}

            //this.dataGridView1.Show();
            //this.dataGridView1.Refresh();
        }

        // Background Version
        public void CopyTables(object sender, DoWorkEventArgs e) 
        {
            CopyObject settings = this.Settings;

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
                        manager.Copy(obj.Name);
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

            this.Settings = SerializationHelper.Deserialize<CopyObject>(this.FileName);

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
    }
}