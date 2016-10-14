using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Osv.Crm.Entities;
using System;
using System.Linq;

namespace CRMDataImport.ActionGenerators
{
    public class UserImportActionGenerator : ImportQueryActionGenerator
    {
        public UserImportActionGenerator()
            : base(SourceDatabaseEnum.CRM3)
        {
            Notes = @"BEFORE IMPORT
Run Required Entities!
Update the Representatives list
Source must be set to an instance of CRM2011 that contains the users you want to copy.";
            Query = @"select FirstName, LastName, InternalEMailAddress, DomainName, BusinessUnitIdName from SystemUser where DomainName <> '' and DomainName is not null";
            AllowParallelism = false;
        }

        protected override Action<IOrganizationService, CrmContext> MakeRowAction(System.Data.IDataReader reader)
        {
            string domainName = reader.GetTypedValue<string>("DomainName");
            string firstName = reader.GetTypedValue<string>("FirstName");
            string lastName = reader.GetTypedValue<string>("LastName");
            string buName = reader.GetTypedValue<string>("BusinessUnitIdName");

            return new Action<IOrganizationService, CrmContext>((service, context) =>
            {

                if (!Project.Dictionaries.SystemUsers.ContainsKey(domainName))
                {
                    var su = new Entity("systemuser");
                    su.Attributes.Add("domainname", domainName);
                    su.Attributes.Add("firstname", firstName);
                    su.Attributes.Add("lastname", lastName);

                    //try to find the bu to use
                    EntityReference bu;
                    if (Project.Dictionaries.BusinessUnits.ContainsKey(buName))
                    {
                        //we found one by name, use it
                        bu = new EntityReference("businessunit", Project.Dictionaries.BusinessUnits[buName]);
                    }
                    else
                    {
                        //we couldn't find the exact one, so use the first one in the dictionary
                        bu = new EntityReference("businessunit", Project.Dictionaries.BusinessUnits.First().Value);
                    }

                    su.Attributes.Add("businessunitid", bu);

                    CreateRequest cr = new CreateRequest();
                    cr.Target = su;

                    service.Execute(cr);
                } 
            });
        }
    }
}
