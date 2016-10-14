using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRMDataImport.Mappers
{
    public class UoMScheduleMapper: MapperBase<UoMSchedule>
    {
        Dictionary<Guid, string> uomschedules = null;

        public UoMScheduleMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        { 
            this.Query = @"select 
                                UoMScheduleId, 
                                [Name], 
                                'Default Base Unit' as 'BaseUoMName',
                                [Description]
                                from UoMSchedule"; 
        }

        public override bool IsImportable(UoMSchedule entity)
        {
            if (uomschedules == null)
            {
                Project.ExecuteInContext((context, server) =>
                {
                    uomschedules = (from u in context.UoMScheduleSet
                                    select new { u.Id, string.Empty }).ToDictionary(t => t.Id, t => string.Empty);
                }); 
            }

            return !uomschedules.ContainsKey(entity.UoMScheduleId.Value); 
        } 
    }
}

