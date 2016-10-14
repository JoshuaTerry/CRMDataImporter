using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;

namespace CRMDataImport.ActionGenerators
{ 
    public class AccountStatusChangeGenerator : StatusChangeGeneratorBase
    {
        public AccountStatusChangeGenerator()
            : base(SourceDatabaseEnum.CRM2011)
        { 
            Query = @"select AccountId
                        from Account
                        where allgnt_PostMigrationDeactivate = 1";
            AllowParallelism = true;
        }

        protected override Action<IOrganizationService, CrmContext> MakeRowAction(System.Data.IDataReader reader)
        {
            Guid id = reader.GetGuid(reader.GetOrdinal("AccountId"));

            return new Action<IOrganizationService, CrmContext>((service, context) =>
            { 
                // This Deactivates an Account
                SetEntityState(service, id, Account.EntityLogicalName, 1, 2); 
            }); 
        }
    }
 }
