using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Osv.Crm.Entities;
using System;
using System.Linq;

namespace CRMDataImport.ActionGenerators
{
    public class UserUpdateActionGenerator : ImportQueryActionGenerator
    {
        public UserUpdateActionGenerator()
            : base(SourceDatabaseEnum.CRM3)
        {
            Notes = @"BEFORE IMPORT
Source must be set to an instance of CRM2011 that contains the users you want to copy.";
            Query = @"select FirstName, LastName, InternalEMailAddress, DomainName, BusinessUnitIdName, address1_telephone1, address1_fax, allgnt_representative from SystemUser where DomainName <> '' and DomainName is not null";
            AllowParallelism = false;
        }

        protected override Action<IOrganizationService, CrmContext> MakeRowAction(System.Data.IDataReader reader)
        {
            string domainName = reader.GetTypedValue<string>("DomainName"); 
            string email = reader.GetTypedValue<string>("InternalEMailAddress");
            string telephone = reader.GetTypedValue<string>("address1_telephone1");
            string fax = reader.GetTypedValue<string>("address1_fax");
           Guid? rep = reader.GetTypedValue<Guid?>("allgnt_representative"); 

            return new Action<IOrganizationService, CrmContext>((service, context) =>
            {
                var user = (from u in context.SystemUserSet
                            where u.DomainName == domainName
                            select u).FirstOrDefault();

                if (user != null)
                {
                    user.Address1_Telephone1 = telephone;
                    user.Address1_Fax = fax;
                    user.InternalEMailAddress = email;

                    //if (rep.HasValue)
                    //{
                    //    user.allgnt_Representative = new EntityReference(allgnt_representative.EntityLogicalName, rep.Value);
                    //}
                }

                if (!context.IsAttached(user))
                    context.Attach(user);

                context.UpdateObject(user);
                context.SaveChanges();
            });
        }
    }
}
