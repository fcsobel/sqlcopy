namespace Test.SqlCopy
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
            btnCopy = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            bttnRefresh = new System.Windows.Forms.Button();
            bttnSelectAll = new System.Windows.Forms.Button();
            bttnDeselectAll = new System.Windows.Forms.Button();
            bttnFlipSelect = new System.Windows.Forms.Button();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            Copy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            table_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            TableType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colProgress = new Test.SqlCopy.DataGridViewProgressColumn();
            txtTimeout = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            txtBatchSize = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            cboSource = new System.Windows.Forms.ComboBox();
            cboDestination = new System.Windows.Forms.ComboBox();
            btnSql = new System.Windows.Forms.Button();
            btnSelectTables = new System.Windows.Forms.Button();
            btnSelectViews = new System.Windows.Forms.Button();
            label8 = new System.Windows.Forms.Label();
            numThreadCount = new System.Windows.Forms.NumericUpDown();
            cbxDeleteRows = new System.Windows.Forms.CheckBox();
            cbxTableLock = new System.Windows.Forms.CheckBox();
            cbxFireTriggers = new System.Windows.Forms.CheckBox();
            cbxCheckConstraints = new System.Windows.Forms.CheckBox();
            cbxKeepNulls = new System.Windows.Forms.CheckBox();
            cbxKeepIdentity = new System.Windows.Forms.CheckBox();
            lstLog = new System.Windows.Forms.ListBox();
            dataGridViewProgressColumn1 = new Test.SqlCopy.DataGridViewProgressColumn();
            btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numThreadCount)).BeginInit();
            SuspendLayout();
            // 
            // btnCopy
            // 
            btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnCopy.Location = new System.Drawing.Point(574, 602);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new System.Drawing.Size(75, 23);
            btnCopy.TabIndex = 11;
            btnCopy.Text = "Copy Data";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += new System.EventHandler(button1_Click);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(28, 13);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(44, 13);
            label1.TabIndex = 3;
            label1.Text = "Source:";
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(11, 606);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(63, 13);
            label2.TabIndex = 4;
            label2.Text = "Destination:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(28, 37);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(42, 13);
            label3.TabIndex = 6;
            label3.Text = "Tables:";
            // 
            // bttnRefresh
            // 
            bttnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            bttnRefresh.Location = new System.Drawing.Point(650, 7);
            bttnRefresh.Name = "bttnRefresh";
            bttnRefresh.Size = new System.Drawing.Size(75, 23);
            bttnRefresh.TabIndex = 2;
            bttnRefresh.Text = "Refresh";
            bttnRefresh.UseVisualStyleBackColor = true;
            bttnRefresh.Click += new System.EventHandler(bttnRefresh_Click);
            // 
            // bttnSelectAll
            // 
            bttnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            bttnSelectAll.Location = new System.Drawing.Point(79, 502);
            bttnSelectAll.Name = "bttnSelectAll";
            bttnSelectAll.Size = new System.Drawing.Size(75, 23);
            bttnSelectAll.TabIndex = 3;
            bttnSelectAll.Text = "Select All";
            bttnSelectAll.UseVisualStyleBackColor = true;
            bttnSelectAll.Click += new System.EventHandler(bttnSelectAll_Click);
            // 
            // bttnDeselectAll
            // 
            bttnDeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            bttnDeselectAll.Location = new System.Drawing.Point(160, 502);
            bttnDeselectAll.Name = "bttnDeselectAll";
            bttnDeselectAll.Size = new System.Drawing.Size(75, 23);
            bttnDeselectAll.TabIndex = 4;
            bttnDeselectAll.Text = "Deselect All";
            bttnDeselectAll.UseVisualStyleBackColor = true;
            bttnDeselectAll.Click += new System.EventHandler(bttnDeselectAll_Click);
            // 
            // bttnFlipSelect
            // 
            bttnFlipSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            bttnFlipSelect.Location = new System.Drawing.Point(241, 502);
            bttnFlipSelect.Name = "bttnFlipSelect";
            bttnFlipSelect.Size = new System.Drawing.Size(65, 23);
            bttnFlipSelect.TabIndex = 5;
            bttnFlipSelect.Text = "Flip";
            bttnFlipSelect.UseVisualStyleBackColor = true;
            bttnFlipSelect.Click += new System.EventHandler(bttnFlipSelect_Click);
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            Copy,
            table_name,
            TableType,
            status,
            colProgress});
            dataGridView1.Location = new System.Drawing.Point(79, 37);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size(646, 377);
            dataGridView1.TabIndex = 16;
            // 
            // Copy
            // 
            Copy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            Copy.FillWeight = 81.21828F;
            Copy.HeaderText = "";
            Copy.MinimumWidth = 20;
            Copy.Name = "Copy";
            Copy.Width = 20;
            // 
            // table_name
            // 
            table_name.FillWeight = 106.2606F;
            table_name.HeaderText = "Table Name";
            table_name.Name = "table_name";
            table_name.ReadOnly = true;
            // 
            // TableType
            // 
            TableType.FillWeight = 106.2606F;
            TableType.HeaderText = "Type";
            TableType.Name = "TableType";
            TableType.ReadOnly = true;
            // 
            // status
            // 
            status.FillWeight = 106.2606F;
            status.HeaderText = "Status";
            status.Name = "status";
            status.ReadOnly = true;
            // 
            // colProgress
            // 
            colProgress.HeaderText = "Progress";
            colProgress.Name = "colProgress";
            colProgress.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            colProgress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // txtTimeout
            // 
            txtTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            txtTimeout.Location = new System.Drawing.Point(623, 503);
            txtTimeout.Name = "txtTimeout";
            txtTimeout.Size = new System.Drawing.Size(51, 20);
            txtTimeout.TabIndex = 8;
            txtTimeout.Text = "30";
            txtTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(569, 505);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(48, 13);
            label4.TabIndex = 18;
            label4.Text = "Timeout:";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(675, 506);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(47, 13);
            label5.TabIndex = 19;
            label5.Text = "seconds";
            // 
            // txtBatchSize
            // 
            txtBatchSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            txtBatchSize.Location = new System.Drawing.Point(623, 529);
            txtBatchSize.Name = "txtBatchSize";
            txtBatchSize.Size = new System.Drawing.Size(51, 20);
            txtBatchSize.TabIndex = 9;
            txtBatchSize.Text = "0";
            txtBatchSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(556, 532);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(61, 13);
            label6.TabIndex = 21;
            label6.Text = "Batch Size:";
            // 
            // label7
            // 
            label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(675, 532);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(29, 13);
            label7.TabIndex = 22;
            label7.Text = "rows";
            // 
            // cboSource
            // 
            cboSource.AllowDrop = true;
            cboSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            cboSource.FormattingEnabled = true;
            cboSource.Location = new System.Drawing.Point(79, 9);
            cboSource.Name = "cboSource";
            cboSource.Size = new System.Drawing.Size(565, 21);
            cboSource.TabIndex = 23;
            cboSource.KeyDown += new System.Windows.Forms.KeyEventHandler(cboSource_KeyDown);
            // 
            // cboDestination
            // 
            cboDestination.AllowDrop = true;
            cboDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            cboDestination.FormattingEnabled = true;
            cboDestination.Location = new System.Drawing.Point(79, 602);
            cboDestination.Name = "cboDestination";
            cboDestination.Size = new System.Drawing.Size(484, 21);
            cboDestination.TabIndex = 24;
            cboDestination.KeyDown += new System.Windows.Forms.KeyEventHandler(cboDestination_KeyDown);
            // 
            // btnSql
            // 
            btnSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            btnSql.Enabled = false;
            btnSql.Location = new System.Drawing.Point(241, 545);
            btnSql.Name = "btnSql";
            btnSql.Size = new System.Drawing.Size(65, 23);
            btnSql.TabIndex = 29;
            btnSql.Text = "SQL";
            btnSql.UseVisualStyleBackColor = true;
            btnSql.Click += new System.EventHandler(btnSql_Click);
            // 
            // btnSelectTables
            // 
            btnSelectTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            btnSelectTables.Location = new System.Drawing.Point(79, 545);
            btnSelectTables.Name = "btnSelectTables";
            btnSelectTables.Size = new System.Drawing.Size(75, 23);
            btnSelectTables.TabIndex = 31;
            btnSelectTables.Text = "Tables";
            btnSelectTables.UseVisualStyleBackColor = true;
            btnSelectTables.Click += new System.EventHandler(btnSelectTables_Click);
            // 
            // btnSelectViews
            // 
            btnSelectViews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            btnSelectViews.Location = new System.Drawing.Point(160, 545);
            btnSelectViews.Name = "btnSelectViews";
            btnSelectViews.Size = new System.Drawing.Size(75, 23);
            btnSelectViews.TabIndex = 32;
            btnSelectViews.Text = "Views";
            btnSelectViews.UseVisualStyleBackColor = true;
            btnSelectViews.Click += new System.EventHandler(btnSelectViews_Click);
            // 
            // label8
            // 
            label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(543, 555);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(74, 13);
            label8.TabIndex = 34;
            label8.Text = "Thread count:";
            // 
            // numThreadCount
            // 
            numThreadCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            numThreadCount.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Test.SqlCopy.Properties.Settings.Default, "ThreadCount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            numThreadCount.Location = new System.Drawing.Point(623, 555);
            numThreadCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            numThreadCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            numThreadCount.Name = "numThreadCount";
            numThreadCount.Size = new System.Drawing.Size(51, 20);
            numThreadCount.TabIndex = 33;
            numThreadCount.Value = global::Test.SqlCopy.Properties.Settings.Default.ThreadCount;
            // 
            // cbxDeleteRows
            // 
            cbxDeleteRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            cbxDeleteRows.AutoSize = true;
            cbxDeleteRows.Checked = global::Test.SqlCopy.Properties.Settings.Default.DeleteRows;
            cbxDeleteRows.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Test.SqlCopy.Properties.Settings.Default, "DeleteRows", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            cbxDeleteRows.Location = new System.Drawing.Point(327, 551);
            cbxDeleteRows.Name = "cbxDeleteRows";
            cbxDeleteRows.Size = new System.Drawing.Size(87, 17);
            cbxDeleteRows.TabIndex = 28;
            cbxDeleteRows.Text = "Delete Rows";
            cbxDeleteRows.UseVisualStyleBackColor = true;
            cbxDeleteRows.Click += new System.EventHandler(cbxDeleteRows_Click);
            // 
            // cbxTableLock
            // 
            cbxTableLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            cbxTableLock.AutoSize = true;
            cbxTableLock.Checked = global::Test.SqlCopy.Properties.Settings.Default.TableLock;
            cbxTableLock.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Test.SqlCopy.Properties.Settings.Default, "TableLock", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            cbxTableLock.Location = new System.Drawing.Point(426, 551);
            cbxTableLock.Name = "cbxTableLock";
            cbxTableLock.Size = new System.Drawing.Size(80, 17);
            cbxTableLock.TabIndex = 27;
            cbxTableLock.Text = "Table Lock";
            cbxTableLock.UseVisualStyleBackColor = true;
            // 
            // cbxFireTriggers
            // 
            cbxFireTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            cbxFireTriggers.AutoSize = true;
            cbxFireTriggers.Checked = global::Test.SqlCopy.Properties.Settings.Default.FireTriggers;
            cbxFireTriggers.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Test.SqlCopy.Properties.Settings.Default, "FireTriggers", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            cbxFireTriggers.Location = new System.Drawing.Point(426, 528);
            cbxFireTriggers.Name = "cbxFireTriggers";
            cbxFireTriggers.Size = new System.Drawing.Size(84, 17);
            cbxFireTriggers.TabIndex = 26;
            cbxFireTriggers.Text = "Fire Triggers";
            cbxFireTriggers.UseVisualStyleBackColor = true;
            cbxFireTriggers.Click += new System.EventHandler(cbxDeleteRows_Click);
            // 
            // cbxCheckConstraints
            // 
            cbxCheckConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            cbxCheckConstraints.AutoSize = true;
            cbxCheckConstraints.Checked = global::Test.SqlCopy.Properties.Settings.Default.CheckConstraints;
            cbxCheckConstraints.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Test.SqlCopy.Properties.Settings.Default, "CheckConstraints", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            cbxCheckConstraints.Location = new System.Drawing.Point(426, 505);
            cbxCheckConstraints.Name = "cbxCheckConstraints";
            cbxCheckConstraints.Size = new System.Drawing.Size(112, 17);
            cbxCheckConstraints.TabIndex = 25;
            cbxCheckConstraints.Text = "Check Constraints";
            cbxCheckConstraints.UseVisualStyleBackColor = true;
            cbxCheckConstraints.Click += new System.EventHandler(cbxDeleteRows_Click);
            // 
            // cbxKeepNulls
            // 
            cbxKeepNulls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            cbxKeepNulls.AutoSize = true;
            cbxKeepNulls.Checked = global::Test.SqlCopy.Properties.Settings.Default.KeepNulls;
            cbxKeepNulls.CheckState = System.Windows.Forms.CheckState.Checked;
            cbxKeepNulls.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Test.SqlCopy.Properties.Settings.Default, "KeepNulls", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            cbxKeepNulls.Location = new System.Drawing.Point(327, 528);
            cbxKeepNulls.Name = "cbxKeepNulls";
            cbxKeepNulls.Size = new System.Drawing.Size(77, 17);
            cbxKeepNulls.TabIndex = 7;
            cbxKeepNulls.Text = "Keep Nulls";
            cbxKeepNulls.UseVisualStyleBackColor = true;
            // 
            // cbxKeepIdentity
            // 
            cbxKeepIdentity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            cbxKeepIdentity.AutoSize = true;
            cbxKeepIdentity.Checked = global::Test.SqlCopy.Properties.Settings.Default.KeepIdentity;
            cbxKeepIdentity.CheckState = System.Windows.Forms.CheckState.Checked;
            cbxKeepIdentity.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Test.SqlCopy.Properties.Settings.Default, "KeepIdentity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            cbxKeepIdentity.Location = new System.Drawing.Point(327, 505);
            cbxKeepIdentity.Name = "cbxKeepIdentity";
            cbxKeepIdentity.Size = new System.Drawing.Size(88, 17);
            cbxKeepIdentity.TabIndex = 6;
            cbxKeepIdentity.Text = "Keep Identity";
            cbxKeepIdentity.UseVisualStyleBackColor = true;
            // 
            // lstLog
            // 
            lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            lstLog.FormattingEnabled = true;
            lstLog.Location = new System.Drawing.Point(79, 421);
            lstLog.Name = "lstLog";
            lstLog.Size = new System.Drawing.Size(646, 69);
            lstLog.TabIndex = 35;
            // 
            // dataGridViewProgressColumn1
            // 
            dataGridViewProgressColumn1.HeaderText = "Progress";
            dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
            dataGridViewProgressColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            dataGridViewProgressColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            dataGridViewProgressColumn1.Width = 139;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnCancel.Enabled = false;
            btnCancel.Location = new System.Drawing.Point(655, 602);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 36;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new System.EventHandler(btnCancel_Click);
            // 
            // SqlCopyForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(742, 636);
            Controls.Add(btnCancel);
            Controls.Add(lstLog);
            Controls.Add(label8);
            Controls.Add(numThreadCount);
            Controls.Add(btnSelectViews);
            Controls.Add(btnSelectTables);
            Controls.Add(btnSql);
            Controls.Add(cbxDeleteRows);
            Controls.Add(cbxTableLock);
            Controls.Add(cbxFireTriggers);
            Controls.Add(cbxCheckConstraints);
            Controls.Add(cboDestination);
            Controls.Add(cboSource);
            Controls.Add(label7);
            Controls.Add(txtBatchSize);
            Controls.Add(label5);
            Controls.Add(txtTimeout);
            Controls.Add(bttnFlipSelect);
            Controls.Add(bttnDeselectAll);
            Controls.Add(bttnSelectAll);
            Controls.Add(cbxKeepNulls);
            Controls.Add(cbxKeepIdentity);
            Controls.Add(bttnRefresh);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCopy);
            Controls.Add(dataGridView1);
            Controls.Add(label4);
            Controls.Add(label6);
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "SqlCopyForm";
            Text = "Copy Window";
            Load += new System.EventHandler(Form1_Load);
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(SqlCopyForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numThreadCount)).EndInit();
            ResumeLayout(false);
            PerformLayout();

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
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBatchSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboSource;
        private System.Windows.Forms.ComboBox cboDestination;
        private System.Windows.Forms.CheckBox cbxCheckConstraints;
        private System.Windows.Forms.CheckBox cbxFireTriggers;
        private System.Windows.Forms.CheckBox cbxTableLock;
        private System.Windows.Forms.CheckBox cbxDeleteRows;
        private System.Windows.Forms.Button btnSql;
        private System.Windows.Forms.Button btnSelectTables;
        private System.Windows.Forms.Button btnSelectViews;
        private System.Windows.Forms.NumericUpDown numThreadCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Copy;
        private System.Windows.Forms.DataGridViewTextBoxColumn table_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableType;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private DataGridViewProgressColumn colProgress;
        private DataGridViewProgressColumn dataGridViewProgressColumn1;
        private System.Windows.Forms.Button btnCancel;
    }
}

