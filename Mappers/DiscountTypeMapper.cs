using Osv.Crm.Entities;
using System;

namespace CRMDataImport.Mappers
{
    public class DiscountTypeMapper : MapperBase<DiscountType>
	{
        public DiscountTypeMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
		{ 
            this.Query = @"select
                                dt.DiscountTypeId,
                                dt.Name,
                                dt.[Description],
                                dt.IsAmountType, 
                                dt.CreatedOn as 'OverriddenCreatedOn'
                                from DiscountType dt
								inner join SystemUser su
									on su.SystemUserId = dt.CreatedBy";
		}

        public override bool IsImportable(DiscountType entity)
		{  
            return true;
		} 
	}
}

