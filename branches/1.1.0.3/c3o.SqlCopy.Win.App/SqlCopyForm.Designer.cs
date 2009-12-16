namespace c3o.SqlCopy
{
    partial class SqlCopyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlCopyForm));
            this.btnCopy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bttnRefresh = new System.Windows.Forms.Button();
            this.cbxKeepIdentity = new System.Windows.Forms.CheckBox();
            this.cbxKeepNulls = new System.Windows.Forms.CheckBox();
            this.bttnSelectAll = new System.Windows.Forms.Button();
            this.bttnDeselectAll = new System.Windows.Forms.Button();
            this.bttnFlipSelect = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBatchSize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxCheckConstraints = new System.Windows.Forms.CheckBox();
            this.cbxFireTriggers = new System.Windows.Forms.CheckBox();
            this.cbxTableLock = new System.Windows.Forms.CheckBox();
            this.cbxDeleteRows = new System.Windows.Forms.CheckBox();
            this.btnSql = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Copy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.table_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(650, 652);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 17;
            this.btnCopy.Text = "Copy Data";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 658);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destination:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tables:";
            // 
            // bttnRefresh
            // 
            this.bttnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bttnRefresh.Location = new System.Drawing.Point(650, 71);
            this.bttnRefresh.Name = "bttnRefresh";
            this.bttnRefresh.Size = new System.Drawing.Size(75, 23);
            this.bttnRefresh.TabIndex = 2;
            this.bttnRefresh.Text = "Refresh";
            this.bttnRefresh.UseVisualStyleBackColor = true;
            this.bttnRefresh.Click += new System.EventHandler(this.bttnRefresh_Click);
            // 
            // cbxKeepIdentity
            // 
            this.cbxKeepIdentity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxKeepIdentity.AutoSize = true;
            this.cbxKeepIdentity.Checked = true;
            this.cbxKeepIdentity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxKeepIdentity.Location = new System.Drawing.Point(327, 581);
            this.cbxKeepIdentity.Name = "cbxKeepIdentity";
            this.cbxKeepIdentity.Size = new System.Drawing.Size(88, 17);
            this.cbxKeepIdentity.TabIndex = 8;
            this.cbxKeepIdentity.Text = "Keep Identity";
            this.cbxKeepIdentity.UseVisualStyleBackColor = true;
            // 
            // cbxKeepNulls
            // 
            this.cbxKeepNulls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxKeepNulls.AutoSize = true;
            this.cbxKeepNulls.Checked = true;
            this.cbxKeepNulls.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxKeepNulls.Location = new System.Drawing.Point(327, 604);
            this.cbxKeepNulls.Name = "cbxKeepNulls";
            this.cbxKeepNulls.Size = new System.Drawing.Size(77, 17);
            this.cbxKeepNulls.TabIndex = 9;
            this.cbxKeepNulls.Text = "Keep Nulls";
            this.cbxKeepNulls.UseVisualStyleBackColor = true;
            // 
            // bttnSelectAll
            // 
            this.bttnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bttnSelectAll.Location = new System.Drawing.Point(79, 578);
            this.bttnSelectAll.Name = "bttnSelectAll";
            this.bttnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.bttnSelectAll.TabIndex = 4;
            this.bttnSelectAll.Text = "Select All";
            this.bttnSelectAll.UseVisualStyleBackColor = true;
            this.bttnSelectAll.Click += new System.EventHandler(this.bttnSelectAll_Click);
            // 
            // bttnDeselectAll
            // 
            this.bttnDeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bttnDeselectAll.Location = new System.Drawing.Point(160, 578);
            this.bttnDeselectAll.Name = "bttnDeselectAll";
            this.bttnDeselectAll.Size = new System.Drawing.Size(75, 23);
            this.bttnDeselectAll.TabIndex = 5;
            this.bttnDeselectAll.Text = "Deselect All";
            this.bttnDeselectAll.UseVisualStyleBackColor = true;
            this.bttnDeselectAll.Click += new System.EventHandler(this.bttnDeselectAll_Click);
            // 
            // bttnFlipSelect
            // 
            this.bttnFlipSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bttnFlipSelect.Location = new System.Drawing.Point(241, 578);
            this.bttnFlipSelect.Name = "bttnFlipSelect";
            this.bttnFlipSelect.Size = new System.Drawing.Size(65, 23);
            this.bttnFlipSelect.TabIndex = 6;
            this.bttnFlipSelect.Text = "Flip";
            this.bttnFlipSelect.UseVisualStyleBackColor = true;
            this.bttnFlipSelect.Click += new System.EventHandler(this.bttnFlipSelect_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CopyTables);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ShowProgress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Copy,
            this.table_name,
            this.status});
            this.dataGridView1.Location = new System.Drawing.Point(79, 102);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(646, 463);
            this.dataGridView1.TabIndex = 3;
            // 
            // txtTimeout
            // 
            this.txtTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimeout.Location = new System.Drawing.Point(623, 579);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(51, 20);
            this.txtTimeout.TabIndex = 14;
            this.txtTimeout.Text = "30";
            this.txtTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(577, 582);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Timeout:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(675, 582);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "seconds";
            // 
            // txtBatchSize
            // 
            this.txtBatchSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBatchSize.Location = new System.Drawing.Point(623, 605);
            this.txtBatchSize.Name = "txtBatchSize";
            this.txtBatchSize.Size = new System.Drawing.Size(51, 20);
            this.txtBatchSize.TabIndex = 15;
            this.txtBatchSize.Text = "0";
            this.txtBatchSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(563, 608);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Batch Size:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(675, 608);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "rows";
            // 
            // cbxCheckConstraints
            // 
            this.cbxCheckConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxCheckConstraints.AutoSize = true;
            this.cbxCheckConstraints.Location = new System.Drawing.Point(426, 581);
            this.cbxCheckConstraints.Name = "cbxCheckConstraints";
            this.cbxCheckConstraints.Size = new System.Drawing.Size(112, 17);
            this.cbxCheckConstraints.TabIndex = 11;
            this.cbxCheckConstraints.Text = "Check Constraints";
            this.cbxCheckConstraints.UseVisualStyleBackColor = true;
            this.cbxCheckConstraints.Click += new System.EventHandler(this.cbxDeleteRows_Click);
            // 
            // cbxFireTriggers
            // 
            this.cbxFireTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxFireTriggers.AutoSize = true;
            this.cbxFireTriggers.Location = new System.Drawing.Point(426, 604);
            this.cbxFireTriggers.Name = "cbxFireTriggers";
            this.cbxFireTriggers.Size = new System.Drawing.Size(84, 17);
            this.cbxFireTriggers.TabIndex = 12;
            this.cbxFireTriggers.Text = "Fire Triggers";
            this.cbxFireTriggers.UseVisualStyleBackColor = true;
            this.cbxFireTriggers.Click += new System.EventHandler(this.cbxDeleteRows_Click);
            // 
            // cbxTableLock
            // 
            this.cbxTableLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxTableLock.AutoSize = true;
            this.cbxTableLock.Location = new System.Drawing.Point(426, 627);
            this.cbxTableLock.Name = "cbxTableLock";
            this.cbxTableLock.Size = new System.Drawing.Size(80, 17);
            this.cbxTableLock.TabIndex = 13;
            this.cbxTableLock.Text = "Table Lock";
            this.cbxTableLock.UseVisualStyleBackColor = true;
            // 
            // cbxDeleteRows
            // 
            this.cbxDeleteRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxDeleteRows.AutoSize = true;
            this.cbxDeleteRows.Location = new System.Drawing.Point(327, 627);
            this.cbxDeleteRows.Name = "cbxDeleteRows";
            this.cbxDeleteRows.Size = new System.Drawing.Size(87, 17);
            this.cbxDeleteRows.TabIndex = 10;
            this.cbxDeleteRows.Text = "Delete Rows";
            this.cbxDeleteRows.UseVisualStyleBackColor = true;
            this.cbxDeleteRows.Click += new System.EventHandler(this.cbxDeleteRows_Click);
            // 
            // btnSql
            // 
            this.btnSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSql.Enabled = false;
            this.btnSql.Location = new System.Drawing.Point(241, 623);
            this.btnSql.Name = "btnSql";
            this.btnSql.Size = new System.Drawing.Size(65, 23);
            this.btnSql.TabIndex = 7;
            this.btnSql.Text = "SQL";
            this.btnSql.UseVisualStyleBackColor = true;
            this.btnSql.Click += new System.EventHandler(this.btnSql_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "DBMS:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Sql Server",
            "Oracle"});
            this.comboBox1.Location = new System.Drawing.Point(79, 43);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(147, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.Location = new System.Drawing.Point(79, 73);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(565, 20);
            this.txtSource.TabIndex = 1;
            // 
            // txtDestination
            // 
            this.txtDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestination.Location = new System.Drawing.Point(79, 654);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(565, 20);
            this.txtDestination.TabIndex = 16;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(79, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(270, 20);
            this.txtName.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Name:";
            // 
            // Copy
            // 
            this.Copy.DataPropertyName = "Selected";
            this.Copy.HeaderText = "";
            this.Copy.Name = "Copy";
            this.Copy.Width = 30;
            // 
            // table_name
            // 
            this.table_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.table_name.DataPropertyName = "Name";
            this.table_name.HeaderText = "Table Name";
            this.table_name.Name = "table_name";
            this.table_name.ReadOnly = true;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.status.DataPropertyName = "Status";
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // SqlCopyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 689);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnSql);
            this.Controls.Add(this.cbxDeleteRows);
            this.Controls.Add(this.cbxTableLock);
            this.Controls.Add(this.cbxFireTriggers);
            this.Controls.Add(this.cbxCheckConstraints);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBatchSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTimeout);
            this.Controls.Add(this.bttnFlipSelect);
            this.Controls.Add(this.bttnDeselectAll);
            this.Controls.Add(this.bttnSelectAll);
            this.Controls.Add(this.cbxKeepNulls);
            this.Controls.Add(this.cbxKeepIdentity);
            this.Controls.Add(this.bttnRefresh);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SqlCopyForm";
            this.Text = "Copy Window";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SqlCopyForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bttnRefresh;
        private System.Windows.Forms.CheckBox cbxKeepIdentity;
        private System.Windows.Forms.CheckBox cbxKeepNulls;
        private System.Windows.Forms.Button bttnSelectAll;
        private System.Windows.Forms.Button bttnDeselectAll;
        private System.Windows.Forms.Button bttnFlipSelect;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBatchSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbxCheckConstraints;
        private System.Windows.Forms.CheckBox cbxFireTriggers;
        private System.Windows.Forms.CheckBox cbxTableLock;
        private System.Windows.Forms.CheckBox cbxDeleteRows;
        private System.Windows.Forms.Button btnSql;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Copy;
        private System.Windows.Forms.DataGridViewTextBoxColumn table_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
    }
}

