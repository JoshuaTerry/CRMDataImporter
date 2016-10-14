using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;

namespace CRMDataImport.ActionGenerators
{ 
    public class OwnerAssignmentGenerator : StatusChangeGeneratorBase
    {
        public OwnerAssignmentGenerator()
            : base(SourceDatabaseEnum.CRM2011)
        {
            // Override query and change entity to update a different entity
            Query = @"select  
		ActivityId as 'EntityId',
        OwnerId,
		'email' as 'entitylogicalname' 
from Email with(nolock)";
            AllowParallelism = true;
        }

        protected override Action<IOrganizationService, CrmContext> MakeRowAction(System.Data.IDataReader reader)
        {
            // SystemUserId for jterry
            Guid tempId = Guid.Parse("4FF85AC9-6FB0-E311-BBE1-00155D02C71B");
            Guid ownerId = reader.GetGuid(reader.GetOrdinal("OwnerId"));
            Guid entityId = reader.GetGuid(reader.GetOrdinal("EntityId"));
            string logicalname = reader.GetString(reader.GetOrdinal("entitylogicalname"));

            return new Action<IOrganizationService, CrmContext>((service, context) =>
            {
                // assign to jterry
                AssignRequest assign1 = new AssignRequest
                {
                    Assignee = new EntityReference(SystemUser.EntityLogicalName,
                        tempId),
                    Target = new EntityReference(logicalname,
                        entityId)
                };
                service.Execute(assign1);

                // assign back to original owner
                AssignRequest assign2 = new AssignRequest
                {
                    Assignee = new EntityReference(SystemUser.EntityLogicalName,
                        ownerId),
                    Target = new EntityReference(logicalname,
                        entityId)
                };
                service.Execute(assign2);
            });
        }
    }
}
