using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Test.SqlCopy.Objects;
using Test.SqlCopy.Data;

namespace Test.SqlCopy
{
    public partial class SqlCopyForm : Form
    {
        public List<CopyObject> list { get; set; }
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
                CopyObject obj = this.CurrentObj;
                obj.Name = this.txtName.Text;
                obj.Dbms = this.comboBox1.SelectedItem as string;
                obj.BatchSize = this.BatchSize;
                obj.BulkCopyTimeout = this.BulkCopyTimeout;
                obj.CheckConstraints = this.cbxCheckConstraints.Checked;
                obj.DeleteRows = this.cbxDeleteRows.Checked;
                //obj.DeleteSql = "";
                obj.Destination = this.Destination;
                //obj.DestinationPartitionName = "";
                //obj.DestinationTableName = "";
                obj.FireTriggers = this.cbxFireTriggers.Checked;
                obj.KeepIdentity = this.cbxKeepIdentity.Checked;
                obj.KeepNulls = this.cbxKeepNulls.Checked;
                //obj.ListSql = "";
                obj.NotifyAfter = 0;
                //obj.PostCopySql = "";
                //obj.PreCopySql = "";
                //obj.SelectSql = "";
                obj.Source = this.Source;
                obj.TableLock = cbxTableLock.Checked;
                obj.UseInternalTransaction = false;
                obj.Tables = this.Tables;   
                return obj;
            }
            set
            {
                this.CurrentObj = value;
                this.txtName.Text = value.Name;
                this.comboBox1.SelectedItem = value.Dbms;
                this.txtBatchSize.Text = value.BatchSize.ToString();
                this.txtTimeout.Text = value.BulkCopyTimeout.ToString();
                this.cbxCheckConstraints.Checked = value.CheckConstraints;
                this.cbxFireTriggers.Checked = value.FireTriggers;
                this.cbxKeepIdentity.Checked = value.KeepIdentity;
                this.cbxKeepNulls.Checked = value.KeepNulls;
                this.cbxTableLock.Checked = value.TableLock;
                this.cbxDeleteRows.Checked = value.DeleteRows;
                this.btnSql.Enabled = Properties.Settings.Default.DeleteRows;
                this.txtSource.Text = value.Source;
                this.txtDestination.Text = value.Destination;
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
        }


        private void bttnRefresh_Click(object sender, EventArgs e)
        {
            CopyObject copy = this.Settings;

            try
            {
                CopyManager manager = null;

                switch (this.Settings.Dbms)
                {
                    case "Oracle":
                        manager = new CopyManager(copy, new OracleData(copy));
                        break;
                    default:
                        manager = new CopyManager(copy, new SqlData(copy));
                        break;
                }

                copy.Tables = manager.List();

                this.Settings = copy;

                // Save Objects to disk
                this.SaveList();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Connecting to Source");
            }
        }

        public void SaveList()
        {
            // Save Objects to disk
            SerializationHelper.Serialize<List<CopyObject>>(this.list, "list.xml");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.CurrentObj = this.Settings;

            // Save Objects to disk
            SerializationHelper.Serialize<List<CopyObject>>(this.list, "list.xml");

            this.CopyTablesAsc();
        }

 
        public void CopyTablesAsc()
		{
            this.backgroundWorker1.RunWorkerAsync();
		}

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.progressBar1.Visible = false;
        }

        public void ShowProgress(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

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
            this.dataGridView1.Refresh();
        }

        // Background Version
        public void CopyTables(object sender, DoWorkEventArgs e) 
        {
            CopyObject settings = this.Settings;

            CopyManager manager = null;

            switch (this.Settings.Dbms)
            {
                case "Oracle":
                    manager = new CopyManager(settings, new OracleData(settings));
                    break;
                default:
                    manager = new CopyManager(this.Settings, new SqlData(settings));
                    break;
            }

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

            foreach (TableObject obj in settings.Tables)
            {
                if (worker.CancellationPending)
                {
                    break;
                }
                try
                {
                    if (obj.Selected)
                    {
                        manager.Copy(obj.Name);
                        worker.ReportProgress(0);
                    }
                }
                catch (Exception er)
                {
                    obj.Status = er.Message;
                    worker.ReportProgress(0, er);
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


        //public void GetSqlTables()
        //{
        //    CopyManager manager = new CopyManager(this.Settings, new SqlData(this.Settings));

        //    //this.dataGridView1.Rows.Clear();
        //    this.dataGridView1.AutoGenerateColumns = false;
        //    this.dataGridView1.DataSource = manager.List();

        //    //using (IDataReader dr = manager.List())
        //    //{
        //    //    while (dr.Read())
        //    //    {
        //    //        this.dataGridView1.Rows.Add(true, dr["table_name"].ToString(), "");
        //    //    }
        //    //}

        //    //string sql = @"SELECT '[' + table_schema + '].[' + table_name + ']' as table_name FROM information_schema.tables";

        //    //using (SqlConnection source = new SqlConnection(this.Source))
        //    //{
        //    //    SqlCommand command = new SqlCommand(sql, source);
        //    //    source.Open();
        //    //    IDataReader dr = command.ExecuteReader();

        //    //    while (dr.Read())
        //    //    {
        //    //        this.dataGridView1.Rows.Add(true, dr["table_name"].ToString(), "");
        //    //    }
        //    //    dr.Close();
        //    //}
        //}

        //public void GetOracleTables()
        //{
        //    CopyManager manager = new CopyManager(this.Settings, new OracleData(this.Settings));

        //    //this.dataGridView1.Rows.Clear();

        //    this.dataGridView1.AutoGenerateColumns = false;
        //    this.Settings.Tables = manager.List();
        //    this.dataGridView1.DataSource = manager.List();
            

        //    //using (IDataReader dr = manager.List())
        //    //{
        //    //    while (dr.Read())
        //    //    {
        //    //        this.dataGridView1.Rows.Add(true, dr["table_name"].ToString(), "");
        //    //    }
        //    //}

        //    //string sql = @"SELECT '[' + table_schema + '].[' + table_name + ']' as table_name FROM information_schema.tables";

        //    //using (SqlConnection source = new SqlConnection(this.Source))
        //    //{
        //    //    SqlCommand command = new SqlCommand(sql, source);
        //    //    source.Open();
        //    //    IDataReader dr = command.ExecuteReader();

        //    //    while (dr.Read())
        //    //    {
        //    //        this.dataGridView1.Rows.Add(true, dr["table_name"].ToString(), "");
        //    //    }
        //    //    dr.Close();
        //    //}
        //}

        
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

        //private void cboSource_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Delete)
        //    {
        //        if (Properties.Settings.Default.sourcelist.Contains(this.cboSource.Text))
        //        {
        //            if (MessageBox.Show(this.cboSource.Text, "Remove this entry?", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //            {
        //                // Remove the item
        //                Properties.Settings.Default.sourcelist.Remove(this.cboSource.Text);

        //                // Reload the drop down
        //                this.cboSource.DataSource = null;
        //                this.cboSource.DataSource = Properties.Settings.Default.sourcelist;
        //            }

        //            // Save users settings
        //            Properties.Settings.Default.Save();
        //        }
        //    }
        //}

        //private void cboDestination_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Delete)
        //    {
        //        if (Properties.Settings.Default.destinationlist.Contains(this.cboDestination.Text))
        //        {
        //            if (MessageBox.Show(this.cboDestination.Text, "Remove this entry?", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //            {
        //                // Remove the item
        //                Properties.Settings.Default.destinationlist.Remove(this.cboDestination.Text);

        //                // Reload the drop down
        //                this.cboDestination.DataSource = null;
        //                this.cboDestination.DataSource = Properties.Settings.Default.destinationlist;
        //            }

        //            // Save users settings
        //            Properties.Settings.Default.Save();
        //        }
        //    }
        //}


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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  this.CurrentObj = this.list[this.cboSource.SelectedIndex];
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bttnSave_Click(object sender, EventArgs e)
        {
            this.SaveList();
            this.Close();
        }

        private void SqlCopyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveList();
        }

        //private void cboSource_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //this.CurrentObj = this.list[this.cboSource.SelectedIndex];

        //    this.Settings = this.list[this.cboSource.SelectedIndex];
        //}
    }
}