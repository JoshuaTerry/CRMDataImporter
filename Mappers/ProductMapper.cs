using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
    public class ProductMapper : MapperBase<Product>
    {
        public ProductMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        { 
            this.Query = @"select 
                                p.ProductId,
                                p.ProductNumber,
                                p.Name,
                                p.SubjectId,
                                'subject' as 'SubjectIdLogicalName',
                                p.DefaultUoMId,
                                'uom' as 'DefaultUoMIdLogicalName',
                                p.DefaultUoMScheduleId,
                                'uomschedule' as 'DefaultUoMScheduleIdLogicalName',
                                p.PriceLevelId,
                                'pricelevel' as 'PriceLevelIdLogicalName',
                                p.QuantityDecimal,
                                p.Price,
                                p.[Description],
                                --p.CreatedBy,
                                --cb.DomainName as 'CreatedByDomainName',
                                p.CreatedOn as 'OverriddenCreatedOn'
                                from Product p
                                inner join SystemUser cb
	                                on cb.SystemUserId = p.CreatedBy
                                left join PriceLevel pl
									on pl.PriceLevelId = p.PriceLevelId";
        }
         
        public override bool IsImportable(Product entity)
        {
            return (!DestinationKeyExists(entity.ProductId.Value,"Product"));
        }
         
    }
}