namespace c3o.SqlCopy
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
            this.txtPre = new System.Windows.Forms.TextBox();
            this.txtPost = new System.Windows.Forms.TextBox();
            this.ppp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPre
            // 
            this.txtPre.AcceptsReturn = true;
            this.txtPre.AcceptsTab = true;
            this.txtPre.Location = new System.Drawing.Point(15, 25);
            this.txtPre.Multiline = true;
            this.txtPre.Name = "txtPre";
            this.txtPre.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPre.Size = new System.Drawing.Size(650, 181);
            this.txtPre.TabIndex = 0;
            this.txtPre.WordWrap = false;
            // 
            // txtPost
            // 
            this.txtPost.AcceptsReturn = true;
            this.txtPost.AcceptsTab = true;
            this.txtPost.Location = new System.Drawing.Point(12, 227);
            this.txtPost.Multiline = true;
            this.txtPost.Name = "txtPost";
            this.txtPost.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPost.Size = new System.Drawing.Size(650, 184);
            this.txtPost.TabIndex = 1;
            this.txtPost.WordWrap = false;
            // 
            // ppp
            // 
            this.ppp.AutoSize = true;
            this.ppp.Location = new System.Drawing.Point(12, 9);
            this.ppp.Name = "ppp";
            this.ppp.Size = new System.Drawing.Size(77, 13);
            this.ppp.TabIndex = 2;
            this.ppp.Text = "Pre Copy SQL:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Post Copy SQL";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(506, 429);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(587, 429);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SqlEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 464);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ppp);
            this.Controls.Add(this.txtPost);
            this.Controls.Add(this.txtPre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SqlEditForm";
            this.Text = "Edit Pre/Post SQL";
            this.Load += new System.EventHandler(this.SqlEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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