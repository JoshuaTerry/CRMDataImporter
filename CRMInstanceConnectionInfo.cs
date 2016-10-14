using System;
using System.Windows.Forms;

namespace CRMDataImport
{
    public partial class CRMInstanceConnectionInfo : Form
    {
        public CRMInstanceConnectionInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Save Settings to Current Project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        { 
            DataMigration.CurrentProject.UpdateCRMSettings(DiscoveryAddress.Text,
                OrganizationName.Text,
                Username.Text,
                Password.Text,
                Domain.Text);
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
        private void CRMInstanceConnectionInfo_Load(object sender, EventArgs e)
        {
            DiscoveryAddress.Text = DataMigration.CurrentProject.DiscoveryAddress;
            Domain.Text = DataMigration.CurrentProject.Domain;
            OrganizationName.Text = DataMigration.CurrentProject.OrganizationName;
            Username.Text = DataMigration.CurrentProject.Username;
            Password.Text = DataMigration.CurrentProject.Password;
        }
    }
}
