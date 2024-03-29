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
            this.Copy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bttnSql = new System.Windows.Forms.DataGridViewImageColumn();
            this.table_Schema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.table_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percentage = new c3o.SqlCopy.DataGridViewProgressColumn();
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
            this.cboSource = new System.Windows.Forms.ComboBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.bttnOpen = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bttnSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cboDestintaion = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.bttnSaveAs = new System.Windows.Forms.Button();
            this.cbxSchema = new System.Windows.Forms.CheckBox();
            this.bttnSwitch = new System.Windows.Forms.Button();
            this.dataGridViewProgressColumn1 = new c3o.SqlCopy.DataGridViewProgressColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(975, 870);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(112, 35);
            this.btnCopy.TabIndex = 17;
            this.btnCopy.Text = "Copy Data";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.bttnCopy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 109);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 880);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destination:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 155);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tables:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bttnRefresh
            // 
            this.bttnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bttnRefresh.Location = new System.Drawing.Point(975, 109);
            this.bttnRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bttnRefresh.Name = "bttnRefresh";
            this.bttnRefresh.Size = new System.Drawing.Size(112, 35);
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
            this.cbxKeepIdentity.Location = new System.Drawing.Point(490, 762);
            this.cbxKeepIdentity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxKeepIdentity.Name = "cbxKeepIdentity";
            this.cbxKeepIdentity.Size = new System.Drawing.Size(128, 24);
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
            this.cbxKeepNulls.Location = new System.Drawing.Point(490, 797);
            this.cbxKeepNulls.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxKeepNulls.Name = "cbxKeepNulls";
            this.cbxKeepNulls.Size = new System.Drawing.Size(110, 24);
            this.cbxKeepNulls.TabIndex = 9;
            this.cbxKeepNulls.Text = "Keep Nulls";
            this.cbxKeepNulls.UseVisualStyleBackColor = true;
            // 
            // bttnSelectAll
            // 
            this.bttnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bttnSelectAll.Location = new System.Drawing.Point(118, 755);
            this.bttnSelectAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bttnSelectAll.Name = "bttnSelectAll";
            this.bttnSelectAll.Size = new System.Drawing.Size(112, 35);
            this.bttnSelectAll.TabIndex = 4;
            this.bttnSelectAll.Text = "Select All";
            this.bttnSelectAll.UseVisualStyleBackColor = true;
            this.bttnSelectAll.Click += new System.EventHandler(this.bttnSelectAll_Click);
            // 
            // bttnDeselectAll
            // 
            this.bttnDeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bttnDeselectAll.Location = new System.Drawing.Point(240, 755);
            this.bttnDeselectAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bttnDeselectAll.Name = "bttnDeselectAll";
            this.bttnDeselectAll.Size = new System.Drawing.Size(112, 35);
            this.bttnDeselectAll.TabIndex = 5;
            this.bttnDeselectAll.Text = "Deselect All";
            this.bttnDeselectAll.UseVisualStyleBackColor = true;
            this.bttnDeselectAll.Click += new System.EventHandler(this.bttnDeselectAll_Click);
            // 
            // bttnFlipSelect
            // 
            this.bttnFlipSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bttnFlipSelect.Location = new System.Drawing.Point(361, 755);
            this.bttnFlipSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bttnFlipSelect.Name = "bttnFlipSelect";
            this.bttnFlipSelect.Size = new System.Drawing.Size(98, 35);
            this.bttnFlipSelect.TabIndex = 6;
            this.bttnFlipSelect.Text = "Flip";
            this.bttnFlipSelect.UseVisualStyleBackColor = true;
            this.bttnFlipSelect.Click += new System.EventHandler(this.bttnFlipSelect_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CopyTables);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ShowProgress);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
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
            this.bttnSql,
            this.table_Schema,
            this.table_name,
            this.status,
            this.Percentage});
            this.dataGridView1.Location = new System.Drawing.Point(118, 158);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 30;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(969, 580);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Copy
            // 
            this.Copy.DataPropertyName = "Selected";
            this.Copy.Frozen = true;
            this.Copy.HeaderText = "";
            this.Copy.MinimumWidth = 8;
            this.Copy.Name = "Copy";
            this.Copy.Width = 30;
            // 
            // bttnSql
            // 
            this.bttnSql.FillWeight = 30F;
            this.bttnSql.Frozen = true;
            this.bttnSql.HeaderText = "SQL";
            this.bttnSql.Image = global::c3o.SqlCopy.Properties.Resources.pencil;
            this.bttnSql.MinimumWidth = 8;
            this.bttnSql.Name = "bttnSql";
            this.bttnSql.Width = 40;
            // 
            // table_Schema
            // 
            this.table_Schema.DataPropertyName = "Schema";
            this.table_Schema.Frozen = true;
            this.table_Schema.HeaderText = "Schema";
            this.table_Schema.MinimumWidth = 8;
            this.table_Schema.Name = "table_Schema";
            this.table_Schema.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.table_Schema.Width = 80;
            // 
            // table_name
            // 
            this.table_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.table_name.DataPropertyName = "Name";
            this.table_name.HeaderText = "Table";
            this.table_name.MinimumWidth = 8;
            this.table_name.Name = "table_name";
            this.table_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.status.DataPropertyName = "Status";
            this.status.HeaderText = "Status";
            this.status.MinimumWidth = 8;
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 200;
            // 
            // Percentage
            // 
            this.Percentage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Percentage.DataPropertyName = "Percentage";
            this.Percentage.HeaderText = "Progress";
            this.Percentage.MinimumWidth = 8;
            this.Percentage.Name = "Percentage";
            // 
            // txtTimeout
            // 
            this.txtTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimeout.Location = new System.Drawing.Point(935, 757);
            this.txtTimeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(75, 26);
            this.txtTimeout.TabIndex = 14;
            this.txtTimeout.Text = "30";
            this.txtTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(865, 761);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Timeout:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1012, 761);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "seconds";
            // 
            // txtBatchSize
            // 
            this.txtBatchSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBatchSize.Location = new System.Drawing.Point(935, 797);
            this.txtBatchSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBatchSize.Name = "txtBatchSize";
            this.txtBatchSize.Size = new System.Drawing.Size(75, 26);
            this.txtBatchSize.TabIndex = 15;
            this.txtBatchSize.Text = "0";
            this.txtBatchSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(845, 801);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Batch Size:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1012, 801);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 20);
            this.label7.TabIndex = 22;
            this.label7.Text = "rows";
            // 
            // cbxCheckConstraints
            // 
            this.cbxCheckConstraints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxCheckConstraints.AutoSize = true;
            this.cbxCheckConstraints.Location = new System.Drawing.Point(639, 762);
            this.cbxCheckConstraints.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxCheckConstraints.Name = "cbxCheckConstraints";
            this.cbxCheckConstraints.Size = new System.Drawing.Size(165, 24);
            this.cbxCheckConstraints.TabIndex = 11;
            this.cbxCheckConstraints.Text = "Check Constraints";
            this.cbxCheckConstraints.UseVisualStyleBackColor = true;
            this.cbxCheckConstraints.Click += new System.EventHandler(this.cbxDeleteRows_Click);
            // 
            // cbxFireTriggers
            // 
            this.cbxFireTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxFireTriggers.AutoSize = true;
            this.cbxFireTriggers.Location = new System.Drawing.Point(639, 797);
            this.cbxFireTriggers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxFireTriggers.Name = "cbxFireTriggers";
            this.cbxFireTriggers.Size = new System.Drawing.Size(123, 24);
            this.cbxFireTriggers.TabIndex = 12;
            this.cbxFireTriggers.Text = "Fire Triggers";
            this.cbxFireTriggers.UseVisualStyleBackColor = true;
            this.cbxFireTriggers.Click += new System.EventHandler(this.cbxDeleteRows_Click);
            // 
            // cbxTableLock
            // 
            this.cbxTableLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxTableLock.AutoSize = true;
            this.cbxTableLock.Location = new System.Drawing.Point(639, 832);
            this.cbxTableLock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxTableLock.Name = "cbxTableLock";
            this.cbxTableLock.Size = new System.Drawing.Size(112, 24);
            this.cbxTableLock.TabIndex = 13;
            this.cbxTableLock.Text = "Table Lock";
            this.cbxTableLock.UseVisualStyleBackColor = true;
            // 
            // cbxDeleteRows
            // 
            this.cbxDeleteRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxDeleteRows.AutoSize = true;
            this.cbxDeleteRows.Location = new System.Drawing.Point(490, 832);
            this.cbxDeleteRows.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxDeleteRows.Name = "cbxDeleteRows";
            this.cbxDeleteRows.Size = new System.Drawing.Size(126, 24);
            this.cbxDeleteRows.TabIndex = 10;
            this.cbxDeleteRows.Text = "Delete Rows";
            this.cbxDeleteRows.UseVisualStyleBackColor = true;
            this.cbxDeleteRows.Click += new System.EventHandler(this.cbxDeleteRows_Click);
            // 
            // btnSql
            // 
            this.btnSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSql.Enabled = false;
            this.btnSql.Location = new System.Drawing.Point(361, 824);
            this.btnSql.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSql.Name = "btnSql";
            this.btnSql.Size = new System.Drawing.Size(98, 35);
            this.btnSql.TabIndex = 7;
            this.btnSql.Text = "SQL";
            this.btnSql.UseVisualStyleBackColor = true;
            this.btnSql.Click += new System.EventHandler(this.btnSql_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(46, 71);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 20);
            this.label8.TabIndex = 30;
            this.label8.Text = "DBMS:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSource
            // 
            this.cboSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSource.FormattingEnabled = true;
            this.cboSource.Location = new System.Drawing.Point(118, 66);
            this.cboSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboSource.Name = "cboSource";
            this.cboSource.Size = new System.Drawing.Size(219, 28);
            this.cboSource.TabIndex = 0;
            this.cboSource.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.Location = new System.Drawing.Point(118, 112);
            this.txtSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(846, 26);
            this.txtSource.TabIndex = 1;
            this.txtSource.Text = "Data Source=.\\sqlexpress;Initial Catalog=<source>;Integrated Security=True";
            // 
            // txtDestination
            // 
            this.txtDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestination.Location = new System.Drawing.Point(118, 874);
            this.txtDestination.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(846, 26);
            this.txtDestination.TabIndex = 16;
            this.txtDestination.Text = "Data Source=.\\sqlexpress;Initial Catalog=<destination>;Integrated Security=True";
            // 
            // bttnOpen
            // 
            this.bttnOpen.Location = new System.Drawing.Point(118, 19);
            this.bttnOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bttnOpen.Name = "bttnOpen";
            this.bttnOpen.Size = new System.Drawing.Size(112, 35);
            this.bttnOpen.TabIndex = 33;
            this.bttnOpen.Text = "Open...";
            this.bttnOpen.UseVisualStyleBackColor = true;
            this.bttnOpen.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // bttnSave
            // 
            this.bttnSave.Location = new System.Drawing.Point(238, 19);
            this.bttnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bttnSave.Name = "bttnSave";
            this.bttnSave.Size = new System.Drawing.Size(112, 35);
            this.bttnSave.TabIndex = 34;
            this.bttnSave.Text = "Save";
            this.bttnSave.UseVisualStyleBackColor = true;
            this.bttnSave.Click += new System.EventHandler(this.bttnSave_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // cboDestintaion
            // 
            this.cboDestintaion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboDestintaion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDestintaion.FormattingEnabled = true;
            this.cboDestintaion.Location = new System.Drawing.Point(118, 827);
            this.cboDestintaion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboDestintaion.Name = "cboDestintaion";
            this.cboDestintaion.Size = new System.Drawing.Size(219, 28);
            this.cboDestintaion.TabIndex = 35;
            this.cboDestintaion.SelectedIndexChanged += new System.EventHandler(this.cboDestintaion_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 830);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 20);
            this.label10.TabIndex = 36;
            this.label10.Text = "DBMS:";
            // 
            // bttnSaveAs
            // 
            this.bttnSaveAs.Location = new System.Drawing.Point(360, 19);
            this.bttnSaveAs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bttnSaveAs.Name = "bttnSaveAs";
            this.bttnSaveAs.Size = new System.Drawing.Size(112, 35);
            this.bttnSaveAs.TabIndex = 38;
            this.bttnSaveAs.Text = "Save As...";
            this.bttnSaveAs.UseVisualStyleBackColor = true;
            this.bttnSaveAs.Click += new System.EventHandler(this.bttnSaveAs_Click);
            // 
            // cbxSchema
            // 
            this.cbxSchema.AutoSize = true;
            this.cbxSchema.Location = new System.Drawing.Point(364, 72);
            this.cbxSchema.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxSchema.Name = "cbxSchema";
            this.cbxSchema.Size = new System.Drawing.Size(150, 24);
            this.cbxSchema.TabIndex = 39;
            this.cbxSchema.Text = "Include Schema";
            this.cbxSchema.UseVisualStyleBackColor = true;
            // 
            // bttnSwitch
            // 
            this.bttnSwitch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bttnSwitch.Image = ((System.Drawing.Image)(resources.GetObject("bttnSwitch.Image")));
            this.bttnSwitch.Location = new System.Drawing.Point(6, 400);
            this.bttnSwitch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bttnSwitch.Name = "bttnSwitch";
            this.bttnSwitch.Size = new System.Drawing.Size(102, 61);
            this.bttnSwitch.TabIndex = 40;
            this.bttnSwitch.UseVisualStyleBackColor = true;
            this.bttnSwitch.Click += new System.EventHandler(this.bttnSwitch_Click);
            // 
            // dataGridViewProgressColumn1
            // 
            this.dataGridViewProgressColumn1.HeaderText = "Progress";
            this.dataGridViewProgressColumn1.MinimumWidth = 8;
            this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
            this.dataGridViewProgressColumn1.Width = 150;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.FillWeight = 30F;
            this.dataGridViewImageColumn1.HeaderText = "SQL";
            this.dataGridViewImageColumn1.Image = global::c3o.SqlCopy.Properties.Resources.pencil;
            this.dataGridViewImageColumn1.MinimumWidth = 8;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 30;
            // 
            // SqlCopyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 934);
            this.Controls.Add(this.bttnSwitch);
            this.Controls.Add(this.cbxSchema);
            this.Controls.Add(this.bttnSaveAs);
            this.Controls.Add(this.cboDestintaion);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.bttnSave);
            this.Controls.Add(this.bttnOpen);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.cboSource);
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
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SqlCopyForm";
            this.Text = "Simple Sql Copy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SqlCopyForm_FormClosing);
            this.Load += new System.EventHandler(this.CopyForm_Load);
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
        private System.Windows.Forms.ComboBox cboSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Button bttnOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button bttnSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ComboBox cboDestintaion;
        private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button bttnSaveAs;
		private System.Windows.Forms.CheckBox cbxSchema;
		private System.Windows.Forms.Button bttnSwitch;
		private DataGridViewProgressColumn dataGridViewProgressColumn1;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Copy;
		private System.Windows.Forms.DataGridViewImageColumn bttnSql;
		private System.Windows.Forms.DataGridViewTextBoxColumn table_Schema;
		private System.Windows.Forms.DataGridViewTextBoxColumn table_name;
		private System.Windows.Forms.DataGridViewTextBoxColumn status;
		private DataGridViewProgressColumn Percentage;
    }
}

