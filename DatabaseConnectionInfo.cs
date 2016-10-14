using System;
using System.Windows.Forms;

namespace CRMDataImport
{
    public partial class DatabaseConnectionInfo : Form
    { 
        public DatabaseConnectionInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Save settings to current project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {
            DataMigration.CurrentProject.UpdateConnectionStrings(CRM3ConnectionString.Text, CRM2011ConnectionString.Text, ACTConnectionString.Text); 
             
            this.Close();
        }

        /// <summary>
        /// Cancel changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Load settings from current project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseConnectionInfo_Load(object sender, EventArgs e)
        {
            CRM2011ConnectionString.Text = DataMigration.CurrentProject.CRM2011ConnectionString;
            CRM3ConnectionString.Text = DataMigration.CurrentProject.CRM3ConnectionString;
            ACTConnectionString.Text = DataMigration.CurrentProject.ACTConnectionString;
        }
    }
}
