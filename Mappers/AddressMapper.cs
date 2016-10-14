using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
	public class AddressMapper : MapperBase<CustomerAddress>
	{
        public AddressMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
		{            
                this.Query = @"select  
									ca.CustomerAddressId,
									ca.AddressNumber,
									ca.Name,
									ca.ParentId,
									'contact' as 'ParentIdLogicalName', 
									'contact' as 'ObjectTypeCode',
									--ca.AddressTypeCode,
									ca.Line1,
									ca.Line2,
									ca.Line3,
									ca.City,
									ca.StateOrProvince,
									ca.PostalCode,
									case 
											when LTRIM(RTRIM(ca.Country)) = 'USA' then 'US'
											when LTRIM(RTRIM(ca.Country)) = 'U.S.A.' then 'US'
											when LTRIM(RTRIM(ca.Country)) = 'UNITED STATES' then 'US'
											when LTRIM(RTRIM(ca.Country)) = '' then 'US'
											when ca.Country is null then 'US'
											else ca.Country
										end as 'Country', 
									ca.Telephone1,
									ca.Telephone2,
									ca.Telephone3,
									ca.Fax,
									a.AccountNumber as 'allgnt_CustomerNumber', 
									c.FirstName as 'osv_FirstName',
									c.LastName as 'osv_LastName',
									c.Suffix as 'osv_SuffixName', 
									a.Name as 'osv_Company'
									from CustomerAddress ca
									inner join Contact c 
										on c.ContactId = ca.ParentId
									inner join Account a
										on c.AccountId = a.AccountId 
										where a.Name is not null ";
		}

		public override bool IsImportable(CustomerAddress entity)
		{  
            return (!Project.Dictionaries.ImportedAddressIds.ContainsKey(entity.CustomerAddressId.Value) &&
                DestinationKeyExists(entity.ParentId.Id,"Contact") &&
                !Project.Dictionaries.ContactAddress1Ids.ContainsKey(entity.Id));
		} 
	}
}

