using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CRMDataImport.Mappers
{
    public class OpportunityMapper : MapperBase<Opportunity>
    {
        public OpportunityMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        {
            LoadOpportunityTypes();

            if (update)
            {
                this.Query = @"
                                    select 
                                        o.OpportunityId,     
                                        o.CFBsamplesent as 'allgnt_SampleSent'                            
                                        from Opportunity o 
											where o.CFBsamplesent = 1
";
            }
            else
            {
                this.Query = @"
                                     select  
                                        o.OpportunityId,
                                        a.AccountNumber,
                                        a.AccountNumber as 'allgnt_CustomerNumber',
                                        o.[name] as 'Name', 
                                        case 
	                                        when o.CustomerIdType = 1 then o.CustomerId
	                                        when o.CustomerIdType = 2 then c.AccountId
                                        end as 'CustomerId',
                                        'account' as 'CustomerIdLogicalName',
                                        case 
	                                        when o.CustomerIdType = 1 then null
	                                        when o.CustomerIdType = 2 then o.CustomerId
                                        end as 'allgnt_Contact',
                                        'contact' as 'allgnt_ContactLogicalName', 
                                        su.DomainName as 'OwnerId', 
                                        o.EstimatedValue, 
                                        o.EstimatedCloseDate, 
                                        -- Ensure Opportunity Type Entities are Loaded
                                        o.CFPopportunitytype as 'allgnt_OpportunityType',  
                                        'allgnt_opportunity_type' as 'allgnt_OpportunityTypeLogicalName',
                                        o.IsRevenueSystemCalculated, 
                                        o.CFBsamplesent as 'allgnt_SampleSent',
                                        case
	                                        when o.CFPopportunitysource = 10 then 739280001
	                                        when o.CFPopportunitysource = 9 then 739280002
	                                        when o.CFPopportunitysource = 4 then 739280003
	                                        when o.CFPopportunitysource = 11 then 739280004
	                                        when o.CFPopportunitysource = 7 then 739280005
	                                        when o.CFPopportunitysource = 5 then 739280006
	                                        when o.CFPopportunitysource = 6 then 739280007
	                                        when o.CFPopportunitysource = 2 then 739280000
	                                        when o.CFPopportunitysource = 8 then 739280008
	                                        when o.CFPopportunitysource = 3 then 739280009
	                                        when o.CFPopportunitysource = 1 then 739280010
	                                        when o.CFPopportunitysource = 12 then 739280013
                                        end as 'allgnt_Source',  
                                        sm.Value as 'allgnt_PromotionCode', 
                                        o.CloseProbability,
                                        o.CloseProbability as 'allgnt_CloseProbability',
                                        o.OpportunityRatingCode, 
                                        o.PriceLevelId, 
                                        'pricelevel' as 'PriceLevelIdLogicalName', 
                                        o.[Description], 
                                        o.CreatedOn as 'OverriddenCreatedOn' 
                                        from Opportunity o
                                        inner join SystemUser su 
	                                        on o.OwnerId = su.SystemUserId
                                        left join Account a
	                                        on a.AccountId = o.AccountId
                                        left join Contact c 
	                                        on c.ContactId = o.ContactId 
	                                    left join StringMap sm
											on sm.AttributeValue = o.CFPpromotioncode and
												sm.AttributeName = 'CFPpromotioncode'";
            } 
        }

        public override bool IsImportable(Opportunity entity)
        { 
            return (!DestinationKeyExists(entity.OpportunityId.Value,"Opportunity") && entity.CustomerId != null && DestinationKeyExists(entity.CustomerId.Id,"Account"));
        }

        public override bool IsUpdateable(Opportunity entity)
        {
            return (DestinationKeyExists(entity.OpportunityId.Value,"Opportunity"));
        }

        protected override bool MapField(string name, IDataReader reader, NewEntityModel model)
        {
            // Item 189 - Also reference Item 71.  Determining if OwnerId should default to nhartley when user is not found.
            if (name == "OwnerId")
            {
                string domainname = reader.GetTypedValue<string>("OwnerId").ToLower();
                string defaultCRM3owner = Common.MissingCRM3DefaultOwner;

                Guid ownerid;

                if (DataMigration.CurrentProject.Dictionaries.SystemUsers.ContainsKey(domainname))
                    ownerid = DataMigration.CurrentProject.Dictionaries.SystemUsers[domainname];
                else
                    ownerid = DataMigration.CurrentProject.Dictionaries.SystemUsers[defaultCRM3owner];

                model.Entity.OwnerId = new EntityReference(SystemUser.EntityLogicalName, ownerid);

                return true;
            }
            // What is this?
            else if (name == "OLD-allgnt_PromotionCode")
            {
                string promocode = reader.GetTypedValue<string>("allgnt_PromotionCode");
                if (!string.IsNullOrEmpty(promocode) && Project.Dictionaries.PromotionCodes.ContainsKey(promocode))
                {
                    //model.Entity.allgnt_PromotionCode = new OptionSetValue(Project.Dictionaries.PromotionCodes[promocode]);
                }

                return true;
            }
            else if (name == "allgnt_OpportunityType")
            {
                int? osvalue = reader.GetTypedValue<int?>("allgnt_OpportunityType");
                
                if (osvalue.HasValue)
                {
                    Guid id = Guid.Empty;

                    foreach (var ot in _opportunitytypes)
                    {
                        if (ot.OptionSetValues.Contains(osvalue.Value))
                        {
                            id = ot.Id;
                            break;
                        }
                    }

                    if (id != Guid.Empty)
                        model.Entity.allgnt_OpportunityType = new EntityReference(allgnt_opportunity_type.EntityLogicalName, id);
                    else
                        throw new Exception(string.Format("Source Option Set Value {0} does not exist in mapping", osvalue.Value));
                }
                return true;
            }
            else if (name == "CompetitorId")
            {
                Guid? cId = reader.GetTypedValue<Guid?>("CompetitorId");

                if (cId.HasValue)
                {
                    Competitor c = new Competitor();
                    c.Id = cId.Value;
                    List<Competitor> list = new List<Competitor>();
                    list.Add(c);
                    model.Entity.opportunitycompetitors_association = list;
                }
                return true;
            }
            return false;
        }

        protected override Action<IOrganizationService, CrmContext> MakeFirstAction()
        {
            // Create the requried Opportunity Types
            return new Action<IOrganizationService, CrmContext>((service, context) =>
            {
                foreach (var ot in _opportunitytypes)
                {
                    try
                    {
                        service.Create(new allgnt_opportunity_type { allgnt_name = ot.Name, allgnt_opportunity_typeId = ot.Id, allgnt_opp_product_division = new OptionSetValue(ot.Division) });
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != "Cannot insert duplicate key.")
                            throw;
                    }
                }
            });
        }

        public struct OpportunityType
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Division { get; set; }
            public int[] OptionSetValues { get; set; }
        }

        private void LoadOpportunityTypes()
        {
           
            _opportunitytypes = new List<OpportunityType>();  
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("6a9831df-c265-49db-adb6-fe7b8e55639a"), Name = "CNV - Conversion", Division = 739280000, OptionSetValues = new int[] { 3 } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("c616711c-1d07-4ca6-a881-b5fd4729948e"), Name = "IOP - IOP", Division = 739280000, OptionSetValues = new int[] { 17 } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("71c83fa4-1427-4bbd-aed8-b51bf0727d3e"), Name = "BS - Box Set", Division = 739280000, OptionSetValues = new int[] { 18 } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("3a977910-551c-4576-8942-6b6fe52389ab"), Name = "BK - Bulk", Division = 739280000, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("130f2be2-77b2-43e4-9420-409537556691"), Name = "CHLD - Children's", Division = 739280000, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("1012ff40-3f93-4325-a439-8772be079994"), Name = "PM - Periodic Mailing", Division = 739280000, OptionSetValues = new int[] { 19, 2 } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("d3bf0197-3d66-4ba7-b124-6f2fadfa9d57"), Name = "OLG - Online Giving", Division = 739280000, OptionSetValues = new int[] { 13 } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("49268248-22ee-4cf1-a29b-b352bfc99d28"), Name = "OLGK - Online Giving Kit", Division = 739280000, OptionSetValues = new int[] { 20 } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("7b53f3fa-8735-43e4-9c67-c027deee8730"), Name = "WEB - Website", Division = 739280000, OptionSetValues = new int[] { 16 } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("4a813f37-b75e-403a-b2da-fbe8da934110"), Name = "SOPR - SOPR", Division = 739280000, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("c028588b-310e-4f7c-b4e3-97d77a642c2e"), Name = "NEWS - Newsletter", Division = 739280000, OptionSetValues = new int[] { 15 } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("8ffb73e9-7630-487a-8358-6677a2e1c0f3"), Name = "XMAS - Christmas Mailing", Division = 739280000, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("0c5e0065-0937-4f02-b099-ecda2309e053"), Name = "EA - Easter Mailing", Division = 739280000, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("618ed1da-8027-424d-904b-2bbbcb9c4f92"), Name = "MB - Mail Back", Division = 739280000, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("24d51cfd-be02-4104-8d1f-7799acd4b0f7"), Name = "APL - Appeal", Division = 739280000, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("83d39b6a-540a-4fe0-84f0-9de4744ab420"), Name = "ROCC - ROCC", Division = 739280000, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("9f6511d6-a95a-49ad-8c52-fa6bf6cd988c"), Name = "OTH - Other", Division = 739280000, OptionSetValues = new int[] { 14, 9, 11, 10, 1, 12 } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("8e1d802c-36af-4d83-8f17-b9a77dd8366d"), Name = "BAS - Basal", Division = 739280001, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("2386c397-c058-46c9-8ee0-5a1757914fed"), Name = "ECHLD - Early Childhood", Division = 739280001, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("65b595bc-b851-4569-882c-0b82e89c2808"), Name = "HS - High School", Division = 739280001, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("faece46a-60b6-432b-9905-d2ca6898a3f5"), Name = "OTH - Other", Division = 739280001, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("cfa9c2e8-e22d-435c-9b58-c1f3d702f1b3"), Name = "PER - Periodicals", Division = 739280001, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("e4a7f923-e027-40f8-a4ba-850900c00bb9"), Name = "RES - Resources", Division = 739280001, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("876e44e0-4011-4c17-98eb-5e5e131905ac"), Name = "SAC - Sacraments", Division = 739280001, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("e7f52552-5418-4038-9227-e9745fac273c"), Name = "TRD - Trade", Division = 739280001, OptionSetValues = new int[] { } });
            _opportunitytypes.Add(new OpportunityType { Id = Guid.Parse("d2a5efdd-785b-4628-84da-611e6e2e8f34"), Name = "VBS - VBS", Division = 739280001, OptionSetValues = new int[] { } });
        }

        private List<OpportunityType> _opportunitytypes = null; 
        
    }
}

