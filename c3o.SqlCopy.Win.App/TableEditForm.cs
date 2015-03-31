using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using c3o.SqlCopy.Objects;
using c3o.SqlCopy.Data;

namespace c3o.SqlCopy
{
    public partial class TableEditForm : Form
    {
        public CopyObject Settings { get; set; }
        public TableObject Table { get; set; }

        public TableEditForm()
        {
            InitializeComponent();
        }

		// load table
        private void SqlEditForm_Load(object sender, EventArgs e)
        {
            if (this.Table != null)
            {
				// set title
                this.Text = string.Format("Table: {0}", this.Table.Name);

				// load sql from dbms
				this.LoadSql();
            }
        }

		// Save & Close
        private void bttnOk_Click(object sender, EventArgs e)
        {
            if (!this.txtPre.ReadOnly)
            {
                if (this.Table != null)
                {
                    this.Table.Sql = this.txtPre.Text;
                }
            }
            this.Close();
        }

		
		// Reset table sql 
        private void bttnReset_Click(object sender, EventArgs e)
        {
			if (this.Table != null)
			{
				this.Table.Sql = null;

				//CopyManager manager = new CopyManager(this.Settings);
				//this.txtPre.Text = manager.GetSelectSql(Table);

				// load sql from dbms
				this.LoadSql();

				this.txtPre.ReadOnly = true;

				//this.Text = string.Format("Table: {0}", this.Table.Name);
			}

        }

		// Get Select SQL from DBMS
		private void LoadSql()
		{
            CopyManager manager = new CopyManager(this.Settings);
            this.txtPre.Text = manager.GetSelectSql(this.Table);	
		}


		// Close
        private void bttnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		// Allow sql to be edited
        private void bttnzEdit_Click(object sender, EventArgs e)
        {
            this.txtPre.ReadOnly = false;
        }



    }
}