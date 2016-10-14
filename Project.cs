using CRMDataImport.Authentication;
using Microsoft.Xrm.Sdk.Client;
using Osv.Crm.Entities;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CRMDataImport
{
    [DataContract(Namespace="")]
    [XmlRootAttribute("Project", Namespace = "", IsNullable = false)] 
    public class Project 
    {
        [DataMember()]
        public string FilePath { get; set; }

        [XmlIgnore()]
        public CommonDictionaries Dictionaries { get; protected set; }

        // Database Connection Settings
        [DataMember()]
        public string CRM3ConnectionString { get; private set; }
        [DataMember()]
        public string CRM2011ConnectionString { get; private set; }
        [DataMember()]
        public string ACTConnectionString { get; private set; }

        public void UpdateConnectionStrings(string crm3, string crm2011, string act)
        {
            CRM3ConnectionString = crm3;
            CRM2011ConnectionString = crm2011;
            ACTConnectionString = act;
            ResetDictionaries();
        }

        /// <summary>
        /// Reset dictionaries whenever a source for their data has changed
        /// </summary>
        public void ResetDictionaries()
        {
            Dictionaries = new CommonDictionaries(this); 
        }

        public void UpdateCRMSettings(string address, string org, string username, string password, string domain)
        {
            DiscoveryAddress = address;
            OrganizationName = org;
            Username = username;
            Password = password;
            Domain = domain;
            ResetDictionaries();
        }

        // CRM Settings
        [DataMember()]
        public string DiscoveryAddress { get; private set; }
        [DataMember()]
        public string OrganizationName { get; private set; }
        [DataMember()]
        public string Username { get; private set; }
        [DataMember()]
        public string Password { get; private set; }
        [DataMember()]
        public string Domain { get; private set; }

        /// <summary>
        /// Executes an action within the context of the CRM environment specified by the project settings
        /// </summary>
        /// <param name="method"></param>
        public void ExecuteInContext(Action<CrmContext, OrganizationServiceProxy> method)
        {
            using (var orgService = AuthHelper.GetOrganizationProxy())
            {
                orgService.EnableProxyTypes();

                using (var context = new CrmContext(orgService))
                {
                    method(context, orgService);
                }
            }
        }

        public void ExecuteInContext(Action<ActionContext> method)
        {
            using (var orgService = AuthHelper.GetOrganizationProxy())
            {
                orgService.EnableProxyTypes();

                using (var context = new CrmContext(orgService))
                { 
                    method(new ActionContext(orgService, context));
                }
            }
        }

        private CrmConnectionInfo connInfo = null;
        public CrmConnectionInfo ConnectionInfo
        {
            get
            {
                if (connInfo == null)
                {
                    connInfo = new CrmConnectionInfo()
                    {
                        DiscoveryServiceAddress = this.DiscoveryAddress,
                        OrganizationUniqueName = this.OrganizationName,
                        UserName = this.Username,
                        Password = this.Password,
                        UserDomain = this.Domain
                    };
                }
                return connInfo;
            }
        }

        private AuthenticationHelper authHelper = null;
        public  AuthenticationHelper AuthHelper
        {
            get
            {
                if (authHelper == null)
                    authHelper = new AuthenticationHelper(ConnectionInfo);

                return authHelper;
            }
        }           
    }

    [XmlRootAttribute("Projects", Namespace = "", IsNullable = false)] 
    public class RecentProject
    {
        public string FilePath { get; set; }
    }
}
