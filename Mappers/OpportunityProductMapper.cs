using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
    public class OpportunityProductMapper : MapperBase<OpportunityProduct>
    {
        public OpportunityProductMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        {
             
            this.Query = @"select
                                    op.OpportunityId,
                                    'opportunity' as 'OpportunityIdLogicalName',
                                    op.OpportunityProductId,
                                    op.ProductId, 
                                    'product' as 'ProductIdLogicalName',
                                    op.IsProductOverridden,
                                    op.IsPriceOverridden,
                                    op.ProductDescription,
                                    op.PricePerUnit, 
                                    op.Quantity,
                                    op.ManualDiscountAmount, 
                                    op.Tax,
                                    op.[Description],
                                    op.UoMId,
                                    'uom' as 'UoMIdLogicalName'
                                    from OpportunityProduct op";
        }

        public override bool IsImportable(OpportunityProduct entity)
        { 
            return (!DestinationKeyExists(entity.OpportunityProductId.Value,"OpportunityProduct") &&
                DestinationKeyExists(entity.OpportunityId.Id,"Opportunity"));
        } 
    }
}