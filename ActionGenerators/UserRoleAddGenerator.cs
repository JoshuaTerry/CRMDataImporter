using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Osv.Crm.Entities;
using System;
using System.Linq;

namespace CRMDataImport.ActionGenerators
{
    public class UserRoleAddGenerator : ImportQueryActionGenerator
    {
        public UserRoleAddGenerator()
            : base(SourceDatabaseEnum.CRM3)
        {
            Notes = @"BEFORE IMPORT
Source must be set to an instance of CRM2011 that contains the users you want to copy.";
            Query = @"select u.DomainName as 'User', r.Name as 'Role', bu.Name as 'BusinessUnit'
                        from SystemUserRoles v --_ValidRoles v
                        join Role r on r.RoleId = v.RoleId
                        join BusinessUnit bu on bu.BusinessUnitId = r.BusinessUnitId
                        join SystemUser u on u.SystemUserId = v.SystemUserId";
            AllowParallelism = false;
        }

        protected override Action<IOrganizationService, CrmContext> MakeRowAction(System.Data.IDataReader reader)
        {
            string domainName = reader.GetTypedValue<string>("User");
            string roleName = reader.GetTypedValue<string>("Role");
            string businessUnit = reader.GetTypedValue<string>("BusinessUnit");

            return new Action<IOrganizationService, CrmContext>((service, context) =>
            { 
                //we need to find this user by domain name in the destination system
                var user = (from u in context.SystemUserSet where u.DomainName == domainName select u).SingleOrDefault();

                if (user != null)
                {
                    //use the destination business unit dictionary to find this business unit by name
                    var bu = Project.Dictionaries.BusinessUnits[businessUnit];

                    //find the role by name and business unit in the destination system
                    var role = (from r in context.RoleSet where r.Name == roleName && r.BusinessUnitId.Id==bu  select r).Single();


                    //associate the found user with the found role
                    AssociateRequest associate = new AssociateRequest()
                    {
                        Target = new EntityReference(SystemUser.EntityLogicalName, user.SystemUserId.Value),
                        RelatedEntities = new EntityReferenceCollection()
                        {
                            new EntityReference(Role.EntityLogicalName, role.RoleId.Value)
                        },
                        Relationship = new Relationship("systemuserroles_association")
                    };

                    try
                    {
                        service.Execute(associate);
                    }
                    catch (System.ServiceModel.FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                    {
                        // This exception would only occur when the item has already been added.
                        if (ex.Message != "Cannot insert duplicate key.")
                            throw ex;
                    }
                }
                else
                {
                    Log.Warn(string.Format("Unable to find user {0} in destination.", domainName));
                }
            });
        }
    }
}
