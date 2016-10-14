using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System.Data;

namespace CRMDataImport.Mappers
{
    public class QuoteDetailMapper : MapperBase<QuoteDetail>
    {
        public QuoteDetailMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        {
            if (update)
            {
                this.Query = @"select  
                                    qd.QuoteDetailId, 
                                    qd.ProductDescription 
                                    from QuoteDetail qd 
                                    where qd.IsProductOverridden = 1";
            }
            else
            {
                this.Query = @"select  
                                    qd.QuoteDetailId,
                                    qd.QuoteId,
                                    'quote' as 'QuoteIdLogicalName',
                                    qd.ProductId,
                                    'product' as 'ProductIdLogicalName',
                                    qd.UoMId,
                                    'uom' as 'UoMIdLogicalName',
                                    qd.CFIparishioners_qty as 'allgnt_NumberofMembers',
                                    qd.CFIparishioners_qty as 'allgnt_ParishionerQuantity',
                                    qd.CFIspecials_qty as 'allgnt_NumberofSpecials',
                                    qd.CFIspecials_qty as 'allgnt_SpecialsQuantity',
                                    qd.[Description],
                                    qd.PricePerUnit, 
                                    qd.Quantity,
                                    qd.ProductDescription,
                                    qd.CFCcycleamount as 'allgnt_CycleAmount',
                                    qd.ManualDiscountAmount,
                                    qd.Tax, 
                                    qd.IsPriceOverridden,
                                    qd.IsProductOverridden 
                                    from QuoteDetail qd ";
            }
        }

        protected override bool MapField(string name, IDataReader reader, NewEntityModel model)
        {
            if (name == "allgnt_CycleAmount")
            {
                decimal? amt = reader.GetTypedValue<decimal?>("allgnt_CycleAmount");
                if (amt.HasValue && amt.Value > 0)
                {
                    model.Entity.allgnt_CycleAmount = new Money(amt.Value);
                }
                else if (amt.HasValue)
                {
                    model.Entity.allgnt_CycleAmount = new Money(0);
                }
                return true;
            }

            return base.MapField(name, reader, model);
        }
        public override bool IsImportable(QuoteDetail entity)
        { 
            return (DestinationKeyExists(entity.QuoteId.Id,"Quote") && !DestinationKeyExists(entity.QuoteDetailId.Value,"QuoteDetail"));
        }

        public override bool IsUpdateable(QuoteDetail entity)
        {
            return DestinationKeyExists(entity.QuoteDetailId.Value,"QuoteDetail");
        }
    }
}

