using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
    public class QuoteMapper : MapperBase<Quote>
	{
        public QuoteMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
		{
            this.Query = @"select  
            q.QuoteId,
            q.QuoteNumber,
            q.[Name], 
            a.AccountNumber,
            case 
	            when q.CustomerIdType = 1 then q.CustomerId
	            when q.CustomerIdType = 2 then c.AccountId
            end as 'CustomerId',
            'account' as 'CustomerIdLogicalName',
            case 
	            when q.CustomerIdType = 1 then null
	            when q.CustomerIdType = 2 then q.CustomerId
            end as 'allgnt_Contact',
            'contact' as 'allgnt_ContactLogicalName', 
            a.AccountNumber as 'allgnt_CustomerNumber',
            su.DomainName as 'OwnerId', 
            q.OpportunityId, 
            'opportunity' as 'OpportunityIdLogicalName',
            q.PriceLevelId, 
            'pricelevel' as 'PriceLevelIdLogicalName', 
            q.DiscountPercentage,
            q.DiscountAmount, 
            q.FreightAmount, 
            q.[Description],
            q.CreatedOn as 'OverriddenCreatedOn',
            q.New_QuoteCity as 'allgnt_QuoteCity',
            q.New_QuoteState as 'allgnt_QuoteState'
            from Quote q   
            left join Account a
	            on a.AccountId = q.AccountId
	        left join Contact c 
	                on c.ContactId = q.ContactId 
            inner join SystemUser su 
	            on q.OwnerId = su.SystemUserId
            inner join SystemUser cb 
	            on q.CreatedBy = cb.SystemUserId 
	        inner join (select  
				        QuoteNumber, MAX(revisionnumber) as rn
		        from	Quote
		        group by QuoteNumber ) revisions
	        on q.QuoteNumber = revisions.QuoteNumber
	        and q.revisionnumber = revisions.rn  
";
           
		}

        public override bool IsImportable(Quote entity)
		{ 
            return (!DestinationKeyExists(entity.QuoteId.Value,"Quote") && entity.OpportunityId != null && DestinationKeyExists(entity.OpportunityId.Id,"Opportunity"));             
		} 
	}
}

