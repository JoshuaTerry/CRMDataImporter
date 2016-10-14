using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
 

namespace CRMDataImport.Mappers
{
    public class OpportunityCompetitorMapper : MapperBase<OpportunityCompetitors>
    {
        public OpportunityCompetitorMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        { 
             
                this.Query = @"SELECT [OpportunityId]
      ,[CompetitorId] 
      ,[OpportunityCompetitorId]
  FROM [OpportunityCompetitors] "; 
        }

        protected override bool MapField(string name, IDataReader reader, NewEntityModel model)
        {
            //if (name == "OpportunityCompetitorId")
            //{
            //    if (!reader.IsDBNull(reader.GetOrdinal("Website")) && reader.GetTypedValue<bool>("Website"))
            //    {
            //        Guid accountId = reader.GetTypedValue<Guid>("AccountId");
            //        var olgId = Project.Dictionaries.OSProducts["Websites"];
            //        model.Subactions.Add(new Action<CrmContext, IOrganizationService>((context, service) =>
            //        {
            //            var productReference = new EntityReference(allgnt_offertorysolutionsproduct.EntityLogicalName, olgId);

            //            EntityReferenceCollection entities = new EntityReferenceCollection();
            //            entities.Add(productReference);

            //            Relationship relationship = new Relationship("allgnt_account_allgnt_offertorysolutionsproduct");

            //            try
            //            {
            //                service.Associate(Account.EntityLogicalName, accountId, relationship, entities);
            //            }
            //            catch (System.ServiceModel.FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
            //            {
            //                // This exception would only occur when the item has already been added
            //                if (ex.Message != "Cannot insert duplicate key.")
            //                    throw ex;
            //            }
            //        }));
            //    }
            //    return true;
            //}
            return false;
        }
        public override bool IsImportable(OpportunityCompetitors entity)
        { 
            return (!DestinationKeyExists(entity.OpportunityCompetitorId.Value, "OpportunityCompetitor"));
        }

    }
}

