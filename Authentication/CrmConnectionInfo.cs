
namespace CRMDataImport.Authentication
{
    public class CrmConnectionInfo
    {
        /// To get discovery service address and organization unique name, 
        /// Sign in to your CRM org and click Settings, Customization, Developer Resources.
        /// On Developer Resource page, find the discovery service address under Service Endpoints and organization unique name under Your Organization Information.
        public string DiscoveryServiceAddress { get; set; }
        public string OrganizationUniqueName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserDomain { get; set; }
    }
}
