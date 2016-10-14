using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;
using System;

namespace CRMDataImport.Authentication
{
    /// <summary>
    ///     Demonstrate how to do basic authentication using IServiceManagement and SecurityTokenResponse.
    /// </summary>
    public class AuthenticationHelper
    { 
        private readonly CrmConnectionInfo crmConnectionInfo;
          
        public AuthenticationHelper(CrmConnectionInfo crmConnectionInfo)
        {
            this.crmConnectionInfo = crmConnectionInfo;
        }

        
        /// <summary>
        /// </summary>
        public OrganizationServiceProxy GetOrganizationProxy()
        {
            var serviceManagement = ServiceConfigurationFactory.CreateManagement<IDiscoveryService>(new Uri(crmConnectionInfo.DiscoveryServiceAddress));
            var endpointType = serviceManagement.AuthenticationType;

            // Set the credentials.
            var authCredentials = GetCredentials(endpointType);            
            var organizationUri = String.Empty;

            // Get the discovery service proxy.
            using (var discoveryProxy = GetProxy<IDiscoveryService, DiscoveryServiceProxy>(serviceManagement, authCredentials))
            {
                // Obtain organization information from the Discovery service. 
                if (discoveryProxy != null)
                {
                    // Obtain information about the organizations that the system user belongs to.
                    var orgs = DiscoverOrganizations(discoveryProxy);
                    // Obtains the Web address (Uri) of the target organization.
                    organizationUri = FindOrganization(crmConnectionInfo.OrganizationUniqueName, orgs.ToArray()).Endpoints[EndpointType.OrganizationService];
                }
            }

            if (!String.IsNullOrWhiteSpace(organizationUri))
            {
                var orgServiceManagement = ServiceConfigurationFactory.CreateManagement<IOrganizationService>(new Uri(organizationUri));

                // Set the credentials.
                var credentials = GetCredentials(endpointType);

                // Get the organization service proxy.
                var organizationProxy = GetProxy<IOrganizationService, OrganizationServiceProxy>(orgServiceManagement, credentials);
                organizationProxy.EnableProxyTypes();

                return organizationProxy; 
            }

            return null;
        }

        /// <summary>
        ///     Obtain the AuthenticationCredentials based on AuthenticationProviderType.
        /// </summary>
        /// <param name="endpointType">An AuthenticationProviderType of the CRM environment.</param>
        /// <returns>Get filled credentials.</returns>
        private AuthenticationCredentials GetCredentials(AuthenticationProviderType endpointType)
        {
            var authCredentials = new AuthenticationCredentials();
            switch (endpointType)
            {
                case AuthenticationProviderType.ActiveDirectory:
                    authCredentials.ClientCredentials.Windows.ClientCredential =
                        new System.Net.NetworkCredential(crmConnectionInfo.UserName,
                                                         crmConnectionInfo.Password,
                                                         crmConnectionInfo.UserDomain);
                    break;
                case AuthenticationProviderType.LiveId:
                    authCredentials.ClientCredentials.UserName.UserName = crmConnectionInfo.UserName;
                    authCredentials.ClientCredentials.UserName.Password = crmConnectionInfo.Password;
                    authCredentials.SupportingCredentials = new AuthenticationCredentials();
                    authCredentials.SupportingCredentials.ClientCredentials =
                         CRMDataImport.Authentication.DeviceIdManager.LoadOrRegisterDevice();
                    break;
                default: // For Federated and OnlineFederated environments.                    
                    authCredentials.ClientCredentials.UserName.UserName = crmConnectionInfo.UserName;
                    authCredentials.ClientCredentials.UserName.Password = crmConnectionInfo.Password;
                    // For OnlineFederated single-sign on, you could just use current UserPrincipalName instead of passing user name and password.
                    // authCredentials.UserPrincipalName = UserPrincipal.Current.UserPrincipalName;  //Windows/Kerberos
                    break;
            }

            return authCredentials;
        }

        /// <summary>
        ///     Discovers the organizations that the calling user belongs to.
        /// </summary>
        /// <param name="service">A Discovery service proxy instance.</param>
        /// <returns>
        ///     Array containing detailed information on each organization that
        ///     the user belongs to.
        /// </returns>
        public OrganizationDetailCollection DiscoverOrganizations(
            IDiscoveryService service)
        {
            if (service == null) throw new ArgumentNullException("service");
            var orgRequest = new RetrieveOrganizationsRequest();
            var orgResponse =
                (RetrieveOrganizationsResponse)service.Execute(orgRequest);

            return orgResponse.Details;
        }

        /// <summary>
        ///     Finds a specific organization detail in the array of organization details
        ///     returned from the Discovery service.
        /// </summary>
        /// <param name="orgUniqueName">The unique name of the organization to find.</param>
        /// <param name="orgDetails">Array of organization detail object returned from the discovery service.</param>
        /// <returns>Organization details or null if the organization was not found.</returns>
        /// <seealso cref="DiscoveryOrganizations" />
        public OrganizationDetail FindOrganization(string orgUniqueName,
                                                   OrganizationDetail[] orgDetails)
        {
            if (String.IsNullOrWhiteSpace(orgUniqueName))
                throw new ArgumentNullException("orgUniqueName");
            if (orgDetails == null)
                throw new ArgumentNullException("orgDetails");
            OrganizationDetail orgDetail = null;

            foreach (OrganizationDetail detail in orgDetails)
            {
                if (String.Compare(detail.UniqueName, orgUniqueName,
                                   StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    orgDetail = detail;
                    break;
                }
            }
            return orgDetail;
        }

        /// <summary>
        ///     Generic method to obtain discovery/organization service proxy instance.
        /// </summary>
        /// <typeparam name="TService">
        ///     Set IDiscoveryService or IOrganizationService type to request respective service proxy instance.
        /// </typeparam>
        /// <typeparam name="TProxy">
        ///     Set the return type to either DiscoveryServiceProxy or OrganizationServiceProxy type based on TService type.
        /// </typeparam>
        /// <param name="serviceManagement">An instance of IServiceManagement</param>
        /// <param name="authCredentials">The user's Microsoft Dynamics CRM logon credentials.</param>
        /// <returns></returns>
        private TProxy GetProxy<TService, TProxy>(
            IServiceManagement<TService> serviceManagement,
            AuthenticationCredentials authCredentials)
            where TService : class
            where TProxy : ServiceProxy<TService>
        {
            var classType = typeof(TProxy);

            if (serviceManagement.AuthenticationType !=
                AuthenticationProviderType.ActiveDirectory)
            {
                var tokenCredentials =
                    serviceManagement.Authenticate(authCredentials);
                // Obtain discovery/organization service proxy for Federated, LiveId and OnlineFederated environments. 
                // Instantiate a new class of type using the 2 parameter constructor of type IServiceManagement and SecurityTokenResponse.
                return (TProxy)classType
                                    .GetConstructor(new[] { typeof(IServiceManagement<TService>), typeof(SecurityTokenResponse) })
                                    .Invoke(new object[] { serviceManagement, tokenCredentials.SecurityTokenResponse });
            }

            // Obtain discovery/organization service proxy for ActiveDirectory environment.
            // Instantiate a new class of type using the 2 parameter constructor of type IServiceManagement and ClientCredentials.
            return (TProxy)classType
                                .GetConstructor(new[] { typeof(IServiceManagement<TService>), typeof(System.ServiceModel.Description.ClientCredentials) })
                                .Invoke(new object[] { serviceManagement, authCredentials.ClientCredentials });
        }
         
    }
}
