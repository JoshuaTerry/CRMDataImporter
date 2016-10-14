using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;

namespace CRMDataImport.ActionGenerators
{
    public class ContactStatusChangeGenerator : StatusChangeGeneratorBase
    {
        public ContactStatusChangeGenerator()
            : base(SourceDatabaseEnum.CRM3)
        {
            Query = @"select 
                            c.ContactId 
                            from Contact c
                            where c.StateCode <> 0";

            AllowParallelism = true;
        }
         
        protected override Action<IOrganizationService, CrmContext> MakeRowAction(System.Data.IDataReader reader)
        {
            //we must read these outside of the function delegate because we aren't sure what state the reader will
            //be in when the action below is executed
            Guid id = reader.GetGuid(reader.GetOrdinal("ContactId")); 

            return new Action<IOrganizationService, CrmContext>((service, context) =>
            {
                if (Project.Dictionaries.HasKeyBeenImported(id, "Contact")) 
                 { 
                       SetEntityState(service, id, Contact.EntityLogicalName, 1, 2); 
                 }
            }); 
        }
    }
}
