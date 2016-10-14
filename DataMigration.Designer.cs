namespace CRMDataImport
{
    partial class DataMigration
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.connectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDatabaseConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCRMConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenProjectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveProjectFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.VerifyRequiredEntities = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.MapperNotes = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NumberOfThreads = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OverrideQuery = new System.Windows.Forms.CheckBox();
            this.Query = new System.Windows.Forms.TextBox();
            this.Stop = new System.Windows.Forms.Button();
            this.ImportEntities = new System.Windows.Forms.Button();
            this.EntitiesToImport = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnImportEmailAttachments = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.OrgName = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Yes = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectionsToolStripMenuItem,
            this.loggingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1004, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.toolStripSeparator1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.openProjectToolStripMenuItem.Text = "&Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.saveProjectToolStripMenuItem.Text = "&Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // connectionsToolStripMenuItem
            // 
            this.connectionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setDatabaseConnectionToolStripMenuItem,
            this.setCRMConnectionToolStripMenuItem});
            this.connectionsToolStripMenuItem.Name = "connectionsToolStripMenuItem";
            this.connectionsToolStripMenuItem.Size = new System.Drawing.Size(102, 24);
            this.connectionsToolStripMenuItem.Text = "&Connections";
            // 
            // setDatabaseConnectionToolStripMenuItem
            // 
            this.setDatabaseConnectionToolStripMenuItem.Name = "setDatabaseConnectionToolStripMenuItem";
            this.setDatabaseConnectionToolStripMenuItem.Size = new System.Drawing.Size(245, 24);
            this.setDatabaseConnectionToolStripMenuItem.Text = "&Set Database Connection";
            this.setDatabaseConnectionToolStripMenuItem.Click += new System.EventHandler(this.setDatabaseConnectionToolStripMenuItem_Click);
            // 
            // setCRMConnectionToolStripMenuItem
            // 
            this.setCRMConnectionToolStripMenuItem.Name = "setCRMConnectionToolStripMenuItem";
            this.setCRMConnectionToolStripMenuItem.Size = new System.Drawing.Size(245, 24);
            this.setCRMConnectionToolStripMenuItem.Text = "S&et CRM Connection";
            this.setCRMConnectionToolStripMenuItem.Click += new System.EventHandler(this.setCRMConnectionToolStripMenuItem_Click);
            // 
            // loggingToolStripMenuItem
            // 
            this.loggingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.loggingToolStripMenuItem.Name = "loggingToolStripMenuItem";
            this.loggingToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.loggingToolStripMenuItem.Text = "Logging";
            this.loggingToolStripMenuItem.Visible = false;
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(131, 24);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // OpenProjectFileDialog
            // 
            this.OpenProjectFileDialog.Filter = "XML Files | *.xml";
            // 
            // SaveProjectFileDialog
            // 
            this.SaveProjectFileDialog.Filter = "XML Files | *.xml";
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.tabPage1);
            this.Tabs.Controls.Add(this.tabPage3);
            this.Tabs.Controls.Add(this.tabPage5);
            this.Tabs.Enabled = false;
            this.Tabs.Location = new System.Drawing.Point(12, 69);
            this.Tabs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(631, 502);
            this.Tabs.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Yes);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.VerifyRequiredEntities);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(623, 473);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Required Entities";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // VerifyRequiredEntities
            // 
            this.VerifyRequiredEntities.Enabled = false;
            this.VerifyRequiredEntities.Location = new System.Drawing.Point(353, 63);
            this.VerifyRequiredEntities.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VerifyRequiredEntities.Name = "VerifyRequiredEntities";
            this.VerifyRequiredEntities.Size = new System.Drawing.Size(75, 32);
            this.VerifyRequiredEntities.TabIndex = 1;
            this.VerifyRequiredEntities.Text = "Verify";
            this.VerifyRequiredEntities.UseVisualStyleBackColor = true;
            this.VerifyRequiredEntities.Click += new System.EventHandler(this.VerifyRequiredEntities_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add and verify that dependent entities exist.";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.MapperNotes);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.NumberOfThreads);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.OverrideQuery);
            this.tabPage3.Controls.Add(this.Query);
            this.tabPage3.Controls.Add(this.Stop);
            this.tabPage3.Controls.Add(this.ImportEntities);
            this.tabPage3.Controls.Add(this.EntitiesToImport);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(623, 473);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "CRM 3.0 Data Import";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // MapperNotes
            // 
            this.MapperNotes.Location = new System.Drawing.Point(239, 56);
            this.MapperNotes.Name = "MapperNotes";
            this.MapperNotes.Size = new System.Drawing.Size(366, 159);
            this.MapperNotes.TabIndex = 10;
            this.MapperNotes.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Notes";
            // 
            // NumberOfThreads
            // 
            this.NumberOfThreads.Location = new System.Drawing.Point(372, 221);
            this.NumberOfThreads.Name = "NumberOfThreads";
            this.NumberOfThreads.Size = new System.Drawing.Size(23, 22);
            this.NumberOfThreads.TabIndex = 7;
            this.NumberOfThreads.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(401, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "# of Threads";
            // 
            // OverrideQuery
            // 
            this.OverrideQuery.AutoSize = true;
            this.OverrideQuery.Location = new System.Drawing.Point(239, 223);
            this.OverrideQuery.Name = "OverrideQuery";
            this.OverrideQuery.Size = new System.Drawing.Size(128, 21);
            this.OverrideQuery.TabIndex = 5;
            this.OverrideQuery.Text = "Override Query";
            this.OverrideQuery.UseVisualStyleBackColor = true;
            this.OverrideQuery.CheckedChanged += new System.EventHandler(this.OverrideQuery_CheckedChanged);
            // 
            // Query
            // 
            this.Query.Location = new System.Drawing.Point(239, 250);
            this.Query.Multiline = true;
            this.Query.Name = "Query";
            this.Query.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Query.Size = new System.Drawing.Size(366, 210);
            this.Query.TabIndex = 4;
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(18, 22);
            this.Stop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(85, 30);
            this.Stop.TabIndex = 3;
            this.Stop.Text = "STOP";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // ImportEntities
            // 
            this.ImportEntities.Location = new System.Drawing.Point(109, 22);
            this.ImportEntities.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ImportEntities.Name = "ImportEntities";
            this.ImportEntities.Size = new System.Drawing.Size(111, 30);
            this.ImportEntities.TabIndex = 2;
            this.ImportEntities.Text = "Import Entities";
            this.ImportEntities.UseVisualStyleBackColor = true;
            this.ImportEntities.Click += new System.EventHandler(this.ImportEntities_Click);
            // 
            // EntitiesToImport
            // 
            this.EntitiesToImport.FormattingEnabled = true;
            this.EntitiesToImport.ItemHeight = 16;
            this.EntitiesToImport.Items.AddRange(new object[] {
            ""});
            this.EntitiesToImport.Location = new System.Drawing.Point(18, 56);
            this.EntitiesToImport.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.EntitiesToImport.Name = "EntitiesToImport";
            this.EntitiesToImport.Size = new System.Drawing.Size(202, 404);
            this.EntitiesToImport.TabIndex = 1;
            this.EntitiesToImport.SelectedIndexChanged += new System.EventHandler(this.EntitiesToImport_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Select an Entity to Import";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnImportEmailAttachments);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage5.Size = new System.Drawing.Size(623, 473);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Attachments";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnImportEmailAttachments
            // 
            this.btnImportEmailAttachments.Location = new System.Drawing.Point(39, 42);
            this.btnImportEmailAttachments.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportEmailAttachments.Name = "btnImportEmailAttachments";
            this.btnImportEmailAttachments.Size = new System.Drawing.Size(461, 28);
            this.btnImportEmailAttachments.TabIndex = 0;
            this.btnImportEmailAttachments.Text = "Import Email Attachments";
            this.btnImportEmailAttachments.UseVisualStyleBackColor = true;
            this.btnImportEmailAttachments.Click += new System.EventHandler(this.btnImportEmailAttachments_Click);
            // 
            // Status
            // 
            this.Status.Location = new System.Drawing.Point(653, 94);
            this.Status.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Status.Multiline = true;
            this.Status.Name = "Status";
            this.Status.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Status.Size = new System.Drawing.Size(327, 454);
            this.Status.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(649, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "CRM 2011 Instance:";
            // 
            // OrgName
            // 
            this.OrgName.AutoSize = true;
            this.OrgName.Location = new System.Drawing.Point(220, 42);
            this.OrgName.Name = "OrgName";
            this.OrgName.Size = new System.Drawing.Size(55, 17);
            this.OrgName.TabIndex = 6;
            this.OrgName.Text = "Not Set";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(852, 39);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Test Code";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(650, 554);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Rows affected: ";
            // 
            // lblRowCount
            // 
            this.lblRowCount.AutoSize = true;
            this.lblRowCount.Location = new System.Drawing.Point(763, 554);
            this.lblRowCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(16, 17);
            this.lblRowCount.TabIndex = 9;
            this.lblRowCount.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(235, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "Have you Activated the Plugins yet?";
            // 
            // Yes
            // 
            this.Yes.Location = new System.Drawing.Point(353, 21);
            this.Yes.Name = "Yes";
            this.Yes.Size = new System.Drawing.Size(75, 32);
            this.Yes.TabIndex = 3;
            this.Yes.Text = "Yes";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // DataMigration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 606);
            this.Controls.Add(this.lblRowCount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.OrgName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DataMigration";
            this.Text = "Data Migration";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDatabaseConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCRMConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenProjectFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveProjectFileDialog;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button VerifyRequiredEntities;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label OrgName;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button ImportEntities;
        private System.Windows.Forms.ListBox EntitiesToImport;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem loggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnImportEmailAttachments;
        private System.Windows.Forms.TextBox Query;
        private System.Windows.Forms.CheckBox OverrideQuery;
        private System.Windows.Forms.TextBox NumberOfThreads;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox MapperNotes;
        private System.Windows.Forms.Button Yes;
        private System.Windows.Forms.Label label7;
    }
}