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
    public partial class SqlCopyFormOld : Form
    {
        public List<CopyObject> list { get; set; }
        public CopyObject CurrentObj { get; set; }

        public CopyObject Settings
        {
            get
            {
                CopyObject obj = this.CurrentObj;
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
                return obj;
            }
            set
            {
                this.CurrentObj = value;
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
                this.cboSource.Text = value.Source;                
                this.cboDestination.Text = value.Destination;
            }
        }
        
        public string Source { get { return this.cboSource.Text; } }
        public string Destination { get { return this.cboDestination.Text; } }
        public int BulkCopyTimeout { get { return int.Parse(this.txtTimeout.Text); } }
        public int BatchSize { get { return int.Parse(this.txtBatchSize.Text); } }

        //private SqlBulkCopyOptions Options
        //{
        //    get
        //    {
        //        SqlBulkCopyOptions option = SqlBulkCopyOptions.Default;
        //        if (cbxKeepIdentity.Checked)  option = option | SqlBulkCopyOptions.KeepIdentity;
        //        if (cbxKeepNulls.Checked)    option = option | SqlBulkCopyOptions.KeepNulls;
        //        if (cbxCheckConstraints.Checked) option = option | SqlBulkCopyOptions.CheckConstraints;
        //        if (cbxFireTriggers.Checked) { option = option | SqlBulkCopyOptions.FireTriggers; }
        //        if (cbxTableLock.Checked) { option = option | SqlBulkCopyOptions.TableLock; }
        //        return option;
        //    }
        //}


        public SqlCopyFormOld()
        {
            InitializeComponent();

            ////this.txtSource.Text = Properties.Settings.Default.source;
            //this.cboDestination.Text = Properties.Settings.Default.destination;
            //this.txtBatchSize.Text = Properties.Settings.Default.BatchSize.ToString();
            //this.txtTimeout.Text = Properties.Settings.Default.Timeout.ToString();
            //this.cbxCheckConstraints.Checked = Properties.Settings.Default.CheckConstraints;
            //this.cbxFireTriggers.Checked = Properties.Settings.Default.FireTriggers;
            //this.cbxKeepIdentity.Checked = Properties.Settings.Default.KeepIdentity;
            //this.cbxKeepNulls.Checked = Properties.Settings.Default.KeepNulls;
            //this.cbxTableLock.Checked = Properties.Settings.Default.TableLock;
            //this.cbxDeleteRows.Checked = Properties.Settings.Default.DeleteRows;
            //this.btnSql.Enabled = Properties.Settings.Default.DeleteRows;
            
            if (Properties.Settings.Default.sourcelist == null)
            {
                Properties.Settings.Default.sourcelist = new System.Collections.Specialized.StringCollection();
            }
            //this.cboSource.DataSource = Properties.Settings.Default.sourcelist;

            if (Properties.Settings.Default.destinationlist == null)
            {
                Properties.Settings.Default.destinationlist = new System.Collections.Specialized.StringCollection();
            }
            this.cboDestination.DataSource = Properties.Settings.Default.destinationlist;

            //this.cboSource.Text = Properties.Settings.Default.source;
            //this.cboDestination.Text = Properties.Settings.Default.destination;

            this.list = SerializationHelper.Deserialize<List<CopyObject>>("list.xml");

            if (this.list == null)
            {
                this.list = new List<CopyObject>();
            }

            if (this.list.Count > 0)
            {
                this.Settings = list[0];
                //this.CurrentObj = list[0];
            }
            else
            {
                this.Settings = new CopyObject();
                this.list.Add(this.Settings);
            }
            ////this.list = new List<CopyObject>();
            //this.CurrentObj = new CopyObject();
            //this.CurrentObj.Source = "test";
            //this.list.Add(CurrentObj);

            this.cboSource.DisplayMember = "Source";
            this.cboSource.DataSource = this.list;            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            this.CurrentObj = this.Settings;

            // Save Objects to disk
            SerializationHelper.Serialize<List<CopyObject>>(this.list, "list.xml");


            ////Properties.Settings.Default.destination = this.cboDestination.Text;
            ////Properties.Settings.Default.Timeout = this.BulkCopyTimeout;
            ////Properties.Settings.Default.BatchSize = this.BatchSize;
            ////Properties.Settings.Default.CheckConstraints = this.cbxCheckConstraints.Checked;
            ////Properties.Settings.Default.FireTriggers  = this.cbxFireTriggers.Checked;
            ////Properties.Settings.Default.KeepIdentity = this.cbxKeepIdentity.Checked;
            ////Properties.Settings.Default.KeepNulls = this.cbxKeepNulls.Checked ;
            ////Properties.Settings.Default.TableLock = this.cbxTableLock.Checked ;
            ////Properties.Settings.Default.DeleteRows = this.cbxDeleteRows.Checked;
            
            //if (!Properties.Settings.Default.destinationlist.Contains(Properties.Settings.Default.destination))
            //{
            //    Properties.Settings.Default.destinationlist.Add(Properties.Settings.Default.destination);
            //    //this.cboDestination.Items.Add(Properties.Settings.Default.destination);
            //    this.cboDestination.DataSource = null;
            //    this.cboDestination.DataSource = Properties.Settings.Default.destinationlist;

            //    this.cboDestination.Text = Properties.Settings.Default.destination;
            //}

            //Properties.Settings.Default.Save();

            this.CopyTablesAsc();
        }

        ////Disable Constraints for all tables
        ////exec sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
        ////exec sp_msforeachtable "ALTER TABLE ? DISABLE TRIGGER all"
        //public void PreCopySql()
        //{
        //    using (SqlConnection destination = new SqlConnection(this.Destination))
        //    {
        //        //string sql = "exec sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; exec sp_msforeachtable 'ALTER TABLE ? DISABLE TRIGGER all'; ";
        //        string sql = Properties.Settings.Default.PreCopySql;

        //        SqlCommand command = new SqlCommand(sql, destination);

        //        destination.Open();
        //        command.ExecuteNonQuery();
        //    }
        //}

        ////Turn constraints and triggers back on
        ////exec sp_msforeachtable @command1="print '?'", @command2="ALTER TABLE ? CHECK CONSTRAINT all"
        ////exec sp_msforeachtable @command1="print '?'", @command2="ALTER TABLE ? ENABLE TRIGGER all"
        //public void PostCopySql()
        //{
        //    using (SqlConnection destination = new SqlConnection(this.Destination))
        //    {
        //        //string sql = "exec sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all'; exec sp_msforeachtable 'ALTER TABLE ? ENABLE TRIGGER all'; ";
        //        string sql = Properties.Settings.Default.PostCopySql;
                
        //        SqlCommand command = new SqlCommand(sql, destination);

        //        destination.Open();
        //        command.ExecuteNonQuery();
        //    }
        //}

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

            this.dataGridView1.FirstDisplayedScrollingRowIndex = e.ProgressPercentage;

            if (e.UserState == null)
            {
                this.dataGridView1.Rows[e.ProgressPercentage].Cells[2].Value = "Success";
            }
            else
            {
                Exception er = (Exception) e.UserState;

                this.dataGridView1.Rows[e.ProgressPercentage].Cells[2].Value = er.Message;
            }
        }

        // Background Version
        public void CopyTables(object sender, DoWorkEventArgs e) 
        {
            //CopyManager manager = new CopyManager(this.Settings, new SqlData(Settings));

            CopyManager manager = null;

            switch (this.Settings.Dbms)
            {
                case "Oracle":
                    manager = new CopyManager(this.Settings, new OracleData(this.Settings));
                    break;
                default:
                    manager = new CopyManager(this.Settings, new SqlData(this.Settings));
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
            
            //CopyData copy = new CopyData(this.Source, this.Destination, this.Options, this.BulkCopyTimeout, this.BatchSize, this.cbxDeleteRows.Checked);

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (worker.CancellationPending)
                {
                    break;
                }
                try
                {
                    bool status = (bool) row.Cells[0].Value;
                    if (status)
                    {
                        manager.Copy((string)row.Cells[1].Value);
                        worker.ReportProgress(row.Index);
                    }                    
                }
                catch (Exception er)
                {
                    worker.ReportProgress(row.Index, er);
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


        public void GetSqlTables()
        {
            CopyManager manager = new CopyManager(this.Settings, new SqlData(this.Settings));

            //this.dataGridView1.Rows.Clear();

            this.dataGridView1.DataSource = manager.List();

            //using (IDataReader dr = manager.List())
            //{
            //    while (dr.Read())
            //    {
            //        this.dataGridView1.Rows.Add(true, dr["table_name"].ToString(), "");
            //    }
            //}

            //string sql = @"SELECT '[' + table_schema + '].[' + table_name + ']' as table_name FROM information_schema.tables";

            //using (SqlConnection source = new SqlConnection(this.Source))
            //{
            //    SqlCommand command = new SqlCommand(sql, source);
            //    source.Open();
            //    IDataReader dr = command.ExecuteReader();

            //    while (dr.Read())
            //    {
            //        this.dataGridView1.Rows.Add(true, dr["table_name"].ToString(), "");
            //    }
            //    dr.Close();
            //}
        }

        public void GetOracleTables()
        {
            CopyManager manager = new CopyManager(this.Settings, new OracleData(this.Settings));

           // this.dataGridView1.Rows.Clear();

            this.dataGridView1.DataSource = manager.List();

            //using (IDataReader dr = manager.List())
            //{
            //    while (dr.Read())
            //    {
            //        this.dataGridView1.Rows.Add(true, dr["table_name"].ToString(), "");
            //    }
            //}

            //string sql = @"SELECT '[' + table_schema + '].[' + table_name + ']' as table_name FROM information_schema.tables";

            //using (SqlConnection source = new SqlConnection(this.Source))
            //{
            //    SqlCommand command = new SqlCommand(sql, source);
            //    source.Open();
            //    IDataReader dr = command.ExecuteReader();

            //    while (dr.Read())
            //    {
            //        this.dataGridView1.Rows.Add(true, dr["table_name"].ToString(), "");
            //    }
            //    dr.Close();
            //}
        }



        private void bttnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.Settings.Dbms)
                {
                    case "Oracle":
                        this.GetOracleTables();
                        break;
                    default:
                        this.GetSqlTables();
                        break;
                }
                               
                

                Properties.Settings.Default.source = this.cboSource.Text;

                if (!Properties.Settings.Default.sourcelist.Contains(Properties.Settings.Default.source))
                {
                    this.cboSource.DataSource = null;
                    Properties.Settings.Default.sourcelist.Add(Properties.Settings.Default.source);
                    this.cboSource.DataSource = Properties.Settings.Default.sourcelist;

                    this.cboSource.Text = Properties.Settings.Default.source;
                    //this.cboSource.Items.Add(Properties.Settings.Default.source);
                }

                Properties.Settings.Default.Save();
            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Connecting to Source");
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

        private void cboSource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (Properties.Settings.Default.sourcelist.Contains(this.cboSource.Text))
                {
                    if (MessageBox.Show(this.cboSource.Text, "Remove this entry?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Remove the item
                        Properties.Settings.Default.sourcelist.Remove(this.cboSource.Text);

                        // Reload the drop down
                        this.cboSource.DataSource = null;
                        this.cboSource.DataSource = Properties.Settings.Default.sourcelist;
                    }

                    // Save users settings
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void cboDestination_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (Properties.Settings.Default.destinationlist.Contains(this.cboDestination.Text))
                {
                    if (MessageBox.Show(this.cboDestination.Text, "Remove this entry?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Remove the item
                        Properties.Settings.Default.destinationlist.Remove(this.cboDestination.Text);

                        // Reload the drop down
                        this.cboDestination.DataSource = null;
                        this.cboDestination.DataSource = Properties.Settings.Default.destinationlist;
                    }

                    // Save users settings
                    Properties.Settings.Default.Save();
                }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  this.CurrentObj = this.list[this.cboSource.SelectedIndex];
        }

        private void cboSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.CurrentObj = this.list[this.cboSource.SelectedIndex];

            this.Settings = this.list[this.cboSource.SelectedIndex];
        }
    }
}