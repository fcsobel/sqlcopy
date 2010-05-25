using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace c3o.SqlCopy
{
    public partial class SqlEditForm : Form
    {
        public SqlEditForm()
        {
            InitializeComponent();
        }

        private void SqlEditForm_Load(object sender, EventArgs e)
        {
            //this.txtPre.Text = Properties.Settings.Default.PreCopySql;
            //this.txtPost.Text = Properties.Settings.Default.PostCopySql;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.PreCopySql = this.txtPre.Text;
            //Properties.Settings.Default.PostCopySql = this.txtPost.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}