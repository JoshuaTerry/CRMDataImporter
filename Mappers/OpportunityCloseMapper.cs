using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
    public class OpportunityCloseMapper : MapperBase<OpportunityClose>
    {
        public OpportunityCloseMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        { 
           
                this.Query = @"select
                                        oc.OpportunityId,
                                        'opportunity' as 'OpportunityIdLogicalName',
                                        oc.ActivityId, 
                                        oc.ActualRevenue,
                                        oc.ActualEnd,
                                        oc.Subject,
                                        oc.ScheduledEnd,
                                        oc.CompetitorId,
                                        'competitor' as 'CompetitorIdLogicalName',
                                        oc.[Description]
                                        from OpportunityClose oc"; 
        }
          
        public override bool IsImportable(OpportunityClose entity)
        {
            return DestinationKeyExists(entity.OpportunityId.Id,"Opportunity") && entity.ActivityId.HasValue && !DestinationKeyExists(entity.ActivityId.Value,"OpportunityClose");
        } 
    }
}

