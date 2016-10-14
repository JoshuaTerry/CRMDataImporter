using Microsoft.Xrm.Sdk.Client;
using Osv.Crm.Entities;
using System.Linq;

namespace CRMDataImport.Mappers
{
    public class PriceListMapper : MapperBase<PriceLevel>
	{
        public PriceListMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
		{ 
            this.Query = @"select 
                                    pl.PriceLevelId,
                                    pl.Name,
                                    pl.[Description],
                                    pl.BeginDate,
                                    pl.EndDate, 
                                    pl.CreatedOn as 'OverriddenCreatedOn'
                                    from PriceLevel pl
                                    inner join SystemUser cb
	                                    on cb.SystemUserId = pl.CreatedBy";
		}

        public override bool IsImportable(PriceLevel entity)
		{
            bool result = false;
            Project.ExecuteInContext(
                delegate(CrmContext context, OrganizationServiceProxy proxy)
                {
                   result = ((from pl in context.PriceLevelSet
                             where pl.PriceLevelId == entity.PriceLevelId
                             select pl).FirstOrDefault()) == null;
                });
            return result;
		} 
	}
}

