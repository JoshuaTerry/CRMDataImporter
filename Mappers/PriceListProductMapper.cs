using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
    public class PriceListProductMapper: MapperBase<ProductPriceLevel>
    {
        public PriceListProductMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        { 
            this.Query = @"select ppl.ProductPriceLevelId, 'pricelevel' as 'PriceLevelIdLogicalName', ppl.PriceLevelId, ppl.ProductId, 'product' as 'ProductIdLogicalName', ppl.UoMScheduleId, 'uomschedule' as 'UoMScheduleIdLogicalName', ppl.UoMId, 'uom' as 'UoMIdLogicalName', ppl.DiscountTypeId, 'discounttype' as 'DiscountTypeIdLogicalName', ppl.Percentage, ppl.Amount from ProductPriceLevel ppl";
        }

        public override bool IsImportable(ProductPriceLevel entity)
        {
            return (!DestinationKeyExists(entity.ProductPriceLevelId.Value,"ProductPriceLevel"));
        }

        public override bool IsUpdateable(ProductPriceLevel entity)
        {
            return DestinationKeyExists(entity.ProductPriceLevelId.Value, "ProductPriceLevel");
        }
    }
}