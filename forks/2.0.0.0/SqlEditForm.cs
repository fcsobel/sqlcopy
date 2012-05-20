using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Test.SqlCopy
{
    public partial class SqlEditForm : Form
    {
        public SqlEditForm()
        {
            InitializeComponent();
        }

        private void SqlEditForm_Load(object sender, EventArgs e)
        {
            txtPre.Text = Properties.Settings.Default.PreCopySql;
            txtPost.Text = Properties.Settings.Default.PostCopySql;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PreCopySql = txtPre.Text;
            Properties.Settings.Default.PostCopySql = txtPost.Text;
            Properties.Settings.Default.Save();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}