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

        private void SqlEditForm_Load(object sender, EventArgs e)
        {
            if (this.Table != null)
            {
                this.Text = string.Format("Table: {0}", this.Table.Name);

                CopyManager manager = new CopyManager(this.Settings);

                this.txtPre.Text = manager.GetSelectSql(Table);
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.txtPre.ReadOnly = false;
        }

        private void bttnReset_Click(object sender, EventArgs e)
        {
            this.Table.Sql = null;

            CopyManager manager = new CopyManager(this.Settings);

            this.txtPre.Text = manager.GetSelectSql(Table);            

            this.txtPre.ReadOnly = true;
        }

    }
}