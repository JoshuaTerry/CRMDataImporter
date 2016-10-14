namespace CRMDataImport
{
    partial class DatabaseConnectionInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CRM3ConnectionString = new System.Windows.Forms.TextBox();
            this.ACTConnectionString = new System.Windows.Forms.TextBox();
            this.Save = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.CRM2011ConnectionString = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "CRM 3.0 Connection String";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "ACT Connection String";
            // 
            // CRM3ConnectionString
            // 
            this.CRM3ConnectionString.Location = new System.Drawing.Point(222, 80);
            this.CRM3ConnectionString.Name = "CRM3ConnectionString";
            this.CRM3ConnectionString.Size = new System.Drawing.Size(523, 22);
            this.CRM3ConnectionString.TabIndex = 2;
            // 
            // ACTConnectionString
            // 
            this.ACTConnectionString.Location = new System.Drawing.Point(222, 108);
            this.ACTConnectionString.Name = "ACTConnectionString";
            this.ACTConnectionString.Size = new System.Drawing.Size(523, 22);
            this.ACTConnectionString.TabIndex = 3;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(668, 148);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(77, 29);
            this.Save.TabIndex = 4;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(564, 148);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(77, 30);
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // CRM2011ConnectionString
            // 
            this.CRM2011ConnectionString.Location = new System.Drawing.Point(222, 52);
            this.CRM2011ConnectionString.Name = "CRM2011ConnectionString";
            this.CRM2011ConnectionString.Size = new System.Drawing.Size(523, 22);
            this.CRM2011ConnectionString.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "CRM 2011 Connection String";
            // 
            // DatabaseConnectionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 256);
            this.Controls.Add(this.CRM2011ConnectionString);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.ACTConnectionString);
            this.Controls.Add(this.CRM3ConnectionString);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DatabaseConnectionInfo";
            this.Text = "Database Connection Info";
            this.Load += new System.EventHandler(this.DatabaseConnectionInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CRM3ConnectionString;
        private System.Windows.Forms.TextBox ACTConnectionString;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.TextBox CRM2011ConnectionString;
        private System.Windows.Forms.Label label3;
    }
}