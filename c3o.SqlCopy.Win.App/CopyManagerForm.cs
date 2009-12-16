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
            this.list = SerializationHelper.Deserialize<List<CopyObject>>(@"config\list.xml");
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = this.list;

            this.template = SerializationHelper.Deserialize<List<CopyObject>>(@"config\template.xml");
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.DataSource = this.template;


        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            SqlCopyForm form = new SqlCopyForm();

            //form.Settings = this.list[this.dataGridView1.CurrentRow.Index];
            form.Settings = (CopyObject) this.dataGridView1.CurrentRow.DataBoundItem;
            //form.list = this.list;
            //form.Show();
            form.ShowDialog(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bttnAdd_Click(object sender, EventArgs e)
        {
            CopyObject obj = ObjectCopier.Clone<CopyObject>(this.template[this.comboBox1.SelectedIndex]);

            this.list.Add(obj);

            //this.dataGridView1.Rows.Clear();
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = list;

            //this.dataGridView1.Refresh();

            

            //this.dataGridView1.Rows.Add(obj);
            //this.dataGridView1.AutoGenerateColumns = false;
            //this.dataGridView1.DataSource = list;


            this.Save();
        }

        public void Save()
        {
            SerializationHelper.Serialize<List<CopyObject>>(this.list, @"config\list.xml");
        }

        private void bttnCopy_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                CopyObject obj = (CopyObject)row.DataBoundItem;
                bool selected = (bool)row.Cells[0].Value;

                if (selected)
                {
                    CopyManager manager = new CopyManager(obj);
                    manager.Copy();
                    manager.Log();
                }

                this.Save();
            }
        }

        private void bttnDelete_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.DataSource = null;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                bool? selected = row.Cells[0].Value as bool?;
                CopyObject obj = (CopyObject)row.DataBoundItem;
                obj.Selected = selected.HasValue && selected.Value;

            }

            this.dataGridView1.DataSource = null;
            
            this.list.RemoveAll(item => item.Selected == true);            

            this.dataGridView1.DataSource = list;

            this.Save();

        }
    }
}
