namespace Test.SqlCopy
{
    partial class SqlEditForm
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
            txtPre = new System.Windows.Forms.TextBox();
            txtPost = new System.Windows.Forms.TextBox();
            ppp = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // txtPre
            // 
            txtPre.AcceptsReturn = true;
            txtPre.AcceptsTab = true;
            txtPre.Location = new System.Drawing.Point(15, 25);
            txtPre.Multiline = true;
            txtPre.Name = "txtPre";
            txtPre.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtPre.Size = new System.Drawing.Size(650, 181);
            txtPre.TabIndex = 0;
            txtPre.WordWrap = false;
            // 
            // txtPost
            // 
            txtPost.AcceptsReturn = true;
            txtPost.AcceptsTab = true;
            txtPost.Location = new System.Drawing.Point(12, 227);
            txtPost.Multiline = true;
            txtPost.Name = "txtPost";
            txtPost.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtPost.Size = new System.Drawing.Size(650, 184);
            txtPost.TabIndex = 1;
            txtPost.WordWrap = false;
            // 
            // ppp
            // 
            ppp.AutoSize = true;
            ppp.Location = new System.Drawing.Point(12, 9);
            ppp.Name = "ppp";
            ppp.Size = new System.Drawing.Size(77, 13);
            ppp.TabIndex = 2;
            ppp.Text = "Pre Copy SQL:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 211);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(79, 13);
            label1.TabIndex = 3;
            label1.Text = "Post Copy SQL";
            // 
            // button1
            // 
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            button1.Location = new System.Drawing.Point(506, 429);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(button1_Click);
            // 
            // button2
            // 
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            button2.Location = new System.Drawing.Point(587, 429);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 5;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new System.EventHandler(button2_Click);
            // 
            // SqlEditForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(677, 464);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(ppp);
            Controls.Add(txtPost);
            Controls.Add(txtPre);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "SqlEditForm";
            Text = "Edit Pre/Post SQL";
            Load += new System.EventHandler(SqlEditForm_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPre;
        private System.Windows.Forms.TextBox txtPost;
        private System.Windows.Forms.Label ppp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}