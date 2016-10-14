using Osv.Crm.Entities;
using System;

namespace CRMDataImport.Mappers
{
    public class IncidentResolutionMapper : MapperBase<IncidentResolution>
	{
        public IncidentResolutionMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
		{
            this.Query = @"select  
                                    ir.ActivityId,
                                    ir.IncidentId, 
                                    ir.Subject,
                                    'incident' as 'IncidentIdLogicalName',
                                    ir.ScheduledDurationMinutes,
                                    ir.TimeSpent,
                                    ir.IsBilled,
                                    ir.[Description],
                                    ir.ActualEnd
                                    from IncidentResolution ir";
		}

        public override bool IsImportable(IncidentResolution entity)
		{  
            return (DestinationKeyExists(entity.IncidentId.Id, "Incident") && !DestinationKeyExists(entity.ActivityId.Value,"IncidentResolution"));
		} 
	}
}

