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
            this.dataGridView1.DataSource = this.list;

            this.template = SerializationHelper.Deserialize<List<CopyObject>>("template.xml");
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.DataSource = this.template;


        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            SqlCopyForm form = new SqlCopyForm();

            //form.Settings = this.list[this.dataGridView1.CurrentRow.Index];
            form.Settings = (CopyObject) this.dataGridView1.CurrentRow.DataBoundItem;
            form.list = this.list;
            form.Show();
            //form.ShowDialog(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bttnAdd_Click(object sender, EventArgs e)
        {
            CopyObject obj = ObjectCopier.Clone<CopyObject>(this.template[this.comboBox1.SelectedIndex]);

            this.list.Add(obj);

            this.dataGridView1.DataSource = list;
            this.dataGridView1.Refresh();
            //this.dataGridView1.AutoGenerateColumns = false;
            //this.dataGridView1.DataSource = list;


            this.Save();
        }

        public void Save()
        {
            SerializationHelper.Serialize<List<CopyObject>>(this.list, "list.xml");
        }

    }
}
