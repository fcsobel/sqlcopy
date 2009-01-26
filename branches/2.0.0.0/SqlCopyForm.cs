using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using System.Threading;
using Test.SqlCopy.Properties;
using System.Diagnostics;


namespace Test.SqlCopy
{
    public partial class SqlCopyForm : Form, INotifyPropertyChanged 
    {
        public string Source { get { return this.cboSource.Text; } }
        public string Destination { get { return this.cboDestination.Text; } }
        public int BulkCopyTimeout { get { return int.Parse(this.txtTimeout.Text); } }
        public int BatchSize { get { return int.Parse(this.txtBatchSize.Text); } }
        private ManualResetEvent quitEvent = new ManualResetEvent(false);
        private CopyThread _copyThread;

        private bool _quit;
        private bool _busy;

        public bool Busy
        {
            get { return _busy;}
            set {

                if (!this.InvokeRequired)
                {
                    _busy = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Busy"));
                }
                else
                {
                    this.BeginInvoke(new MethodInvoker(delegate() { Busy = value; }));
                }
            }
        }

        private SqlBulkCopyOptions Options
        {
            get
            {
                SqlBulkCopyOptions option = SqlBulkCopyOptions.Default;
                if (cbxKeepIdentity.Checked) option = option | SqlBulkCopyOptions.KeepIdentity;
                if (cbxKeepNulls.Checked) option = option | SqlBulkCopyOptions.KeepNulls;
                if (cbxCheckConstraints.Checked) option = option | SqlBulkCopyOptions.CheckConstraints;
                if (cbxFireTriggers.Checked) { option = option | SqlBulkCopyOptions.FireTriggers; }
                if (cbxTableLock.Checked) { option = option | SqlBulkCopyOptions.TableLock; }
                return option;
            }
        }

        public SqlCopyForm()
        {
            InitializeComponent();

            this.btnCancel.DataBindings.Add(new Binding("Enabled", this, "Busy"));
            
            this.cboDestination.Text = Properties.Settings.Default.destination;
            this.txtBatchSize.Text = Properties.Settings.Default.BatchSize.ToString();
            this.txtTimeout.Text = Properties.Settings.Default.Timeout.ToString();
            this.btnSql.Enabled = Properties.Settings.Default.DeleteRows;

            if (Properties.Settings.Default.sourcelist == null)
            {
                Properties.Settings.Default.sourcelist = new System.Collections.Specialized.StringCollection();
            }
            this.cboSource.DataSource = Properties.Settings.Default.sourcelist;

            if (Properties.Settings.Default.destinationlist == null)
            {
                Properties.Settings.Default.destinationlist = new System.Collections.Specialized.StringCollection();
            }
            this.cboDestination.DataSource = Properties.Settings.Default.destinationlist;

            this.cboSource.Text = Properties.Settings.Default.source;
            this.cboDestination.Text = Properties.Settings.Default.destination;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Busy)
                return;

            Busy = true;

            Properties.Settings.Default.destination = this.cboDestination.Text;
            Properties.Settings.Default.Timeout = this.BulkCopyTimeout;
            Properties.Settings.Default.BatchSize = this.BatchSize;

            if (!Properties.Settings.Default.destinationlist.Contains(Properties.Settings.Default.destination))
            {
                Properties.Settings.Default.destinationlist.Add(Properties.Settings.Default.destination);
                this.cboDestination.DataSource = null;
                this.cboDestination.DataSource = Properties.Settings.Default.destinationlist;

                this.cboDestination.Text = Properties.Settings.Default.destination;
            }

            Properties.Settings.Default.Save();

            this.CopyTablesAsc();
        }


        public void CopyTablesAsc()
        {
            // clear the status fields
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.Cells[3].Value = string.Empty;
                row.Cells[4].Value = 0;
            }
            quitEvent.Reset();
            _quit = false;

            List<string> tableNames = new List<string>();
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                bool status = (bool)row.Cells[0].Value;
                if (status)
                {
                    tableNames.Add(Convert.ToString(row.Cells[1].Value));
                }
            }

            _copyThread = new CopyThread(
                this.cbxDeleteRows.Checked
                , this.Destination
                , this.Source
                , quitEvent
                , (int)numThreadCount.Value
                , this.Options
                , this.BulkCopyTimeout
                , this.BatchSize
                , tableNames.ToArray());
            _copyThread.CopyDone += new EventHandler<CopyDoneEventArgs>(_copyThread_CopyDone);
            Thread tr = new Thread(_copyThread.CopyTables);
            _copyThread.CopyProgress += copy_CopyProgress;
            tr.Start();
        }

        void _copyThread_CopyDone(object sender, CopyDoneEventArgs e)
        {
            Busy = false;
            if (e != null && e.Ex != null)
            {
                Trace.TraceError("Error occurred while processing: {0}", e.Ex.Message);
            }
        }

        /// <summary>
        /// handler for the copy progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void copy_CopyProgress(object sender, CopyProgressEventArgs e)
        {
            SqlBulkCopy copy = sender as SqlBulkCopy;
            if (copy != null)
            {
                if (_quit)
                    e.SqlArgs.Abort = true;

                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    if ((string)row.Cells[1].Value == copy.DestinationTableName)
                    {
                        row.Cells[3].Value = string.Format("{0}: {1} rows copied", e.Total == e.Current ? "Done" : "Busy", e.SqlArgs.RowsCopied);
                        row.Cells[4].Value = e.Total == 0 ? 0 : e.Current * 100 / e.Total; // percentage done
                    }
                }
            }
            else
            {
                if (e.Exception != null && !string.IsNullOrEmpty(e.TableName))
                {
                    foreach (DataGridViewRow row in this.dataGridView1.Rows)
                    {
                        if ((string)row.Cells[1].Value == e.TableName)
                        {
                            row.Cells[3].Value = e.Exception.Message;
                            row.Cells[4].Value = 0; // reset
                        }
                    }
                }
            }
        }

        public void GetTables()
        {
            this.dataGridView1.Rows.Clear();

            string sql = @"SELECT '[' + table_schema + '].[' + table_name + ']' as table_name, TABLE_TYPE FROM information_schema.tables WHERE table_name <> 'sysdiagrams' ORDER BY TABLE_TYPE, TABLE_NAME";

            using (SqlConnection source = new SqlConnection(this.Source))
            {
                using (SqlCommand command = new SqlCommand(sql, source))
                {
                    command.CommandTimeout = this.BulkCopyTimeout;
                    source.Open();
                    using (IDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            this.dataGridView1.Rows.Add(true, dr["table_name"].ToString(), dr["table_type"], "");
                        }
                    }
                }
            }
        }


        private void bttnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetTables();
                Properties.Settings.Default.source = this.cboSource.Text;

                if (!Properties.Settings.Default.sourcelist.Contains(Properties.Settings.Default.source))
                {
                    this.cboSource.DataSource = null;
                    Properties.Settings.Default.sourcelist.Add(Properties.Settings.Default.source);
                    this.cboSource.DataSource = Properties.Settings.Default.sourcelist;

                    this.cboSource.Text = Properties.Settings.Default.source;
                }

                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
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
                bool status = (bool)row.Cells[0].Value;
                row.SetValues(!status);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Trace.Listeners.Add(new ListboxTrace(lstLog));
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

        private void btnSelectTables_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.SetValues(((string)row.Cells[2].Value).Equals("BASE TABLE", StringComparison.OrdinalIgnoreCase));
            }

        }

        private void btnSelectViews_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.SetValues(((string)row.Cells[2].Value).Equals("VIEW", StringComparison.OrdinalIgnoreCase));
            }
        }

        private void SqlCopyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Busy)
            {
                if (MessageBox.Show("Copy session active, abort?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CancelProcess();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                _quit = true;
                quitEvent.Set();
            }
        }

        /// <summary>
        /// cancels the copy process
        /// </summary>
        private void CancelProcess()
        {
            Trace.TraceWarning("Cancelled the copy on request of user");
            _quit = true;
            quitEvent.Set();
           
            Busy = false;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelProcess();
        }
    }
}