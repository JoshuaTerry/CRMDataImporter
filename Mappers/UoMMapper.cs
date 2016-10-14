using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
    class UoMMapper : MapperBase<UoM>
    {
        public UoMMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        { 
            this.Query = @"select 
                                        UoMId, 
                                        [Name], 
                                        UoMScheduleId,
                                        'uomschedule' as 'UoMScheduleIdLogicalName',
                                        cast(case
		                                    when [Name] = 'Bi-Monthly' then 6
		                                    when [Name] = 'Annually' then 1
		                                    when [Name] = 'ea' then 1
		                                    when [Name] = 'Monthly' then 12
		                                    when [Name] = 'Quarterly' then 4 
	                                    end as decimal) as 'Quantity' 
                                    from UoM  --where UoMScheduleId = 'AED76F18-A674-4B6F-95F4-E0133637E378' 
";
        }

        public override bool IsImportable(UoM entity)
        {
            return !(this.DestinationKeyExists(entity.UoMId.Value,"UoM"));
        } 
    }
}

