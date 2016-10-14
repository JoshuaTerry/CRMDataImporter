using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
    public class DiscountMapper : MapperBase<Discount>
    {
        public DiscountMapper(bool update) : base(SourceDatabaseEnum.CRM3, update) 
        { 
            this.Query = @"select
                            d.DiscountId,
                            d.DiscountTypeId,
                            'discount' as 'DiscountTypeIdLogicalName',
                            d.LowQuantity,
                            d.HighQuantity,
                            d.Percentage,
                            d.Amount,
                            d.CreatedOn as 'OverriddenCreatedOn'
                            from Discount d";
        }

        public override bool IsImportable(Discount entity)
        {
            return true;
        }  
    }
}

