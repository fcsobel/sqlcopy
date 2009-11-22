using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Test.SqlCopy.Objects;
using Test.SqlCopy.Data;

namespace Test.SqlCopy
{
    public partial class CopyManagerForm : Form
    {
        List<CopyObject> list { get; set; }
        List<CopyObject> template { get; set; }

        public CopyManagerForm()
        {
            InitializeComponent();
        }

        private void CopyManagerForm_Load(object sender, EventArgs e)
        {
            this.list = SerializationHelper.Deserialize<List<CopyObject>>("list.xml");
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = list;

            this.template = SerializationHelper.Deserialize<List<CopyObject>>("template.xml");
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.DataSource = this.template;


        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            SqlCopyForm form = new SqlCopyForm();

            form.ShowDialog(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
