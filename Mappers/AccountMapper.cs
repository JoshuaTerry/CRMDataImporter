using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Data;

namespace CRMDataImport.Mappers
{
    public class AccountMapper : MapperBase<Account>
    {
        public AccountMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update)
        {
            // 2/3/14 - Added feature to map Description from CRM 3.0 Account to a Note in 2011.
            this.Query = @"
                                        select  
                                            a.AccountId,
                                            a.Description,
                                            a.WebSiteURL , 
                                            a.PrimaryContactId,
                                            'contact' as 'PrimaryContactIdLogicalName',
                                            case 
                                                when a.IndustryCode = 1 then 863940000
                                                when a.IndustryCode = 2 then 863940001
                                                when a.IndustryCode = 3 then 863940002
                                                when a.IndustryCode = 4 then 863940003
                                                when a.IndustryCode = 5 then 863940004
                                                when a.IndustryCode = 6 then 863940005
                                                when a.IndustryCode = 7 then 863940006
                                                when a.IndustryCode = 8 then 863940007
                                                when a.IndustryCode = 9 then 863940008
                                                when a.IndustryCode = 33 then 863940009
                                                else null
                                            end as 'osv_offertoryreligiontype',  
                                            case
                                                when a.AccountCategoryCode = 1 then 739280000
                                                when a.AccountCategoryCode = 2 then null
                                                when a.AccountCategoryCode = 3 then 739280004
                                                else null
                                            end as 'allgnt_OffertoryCustomerGrouping', 
                                            cast(
                                            case 
                                                when a.New_Top25Customer = 1 or a.New_PurpleBox = 1 then 739280000
                                                else null
                                            end as int) as 'allgnt_OffertoryCustomerTier',
                                            a.New_ProductSpecialist as 'allgnt_ProductSpecialist',
                                            a.New_SrCampaignConsultant as 'allgnt_OffertoryConsultant', 
                                            case
                                                when a.TerritoryCode = 2 then 863940007
                                                when a.TerritoryCode = 3 then 863940000
                                                when a.TerritoryCode = 4 then 863940002
                                                when a.TerritoryCode = 5 then 863940003
                                                when a.TerritoryCode = 6 then 863940004
                                                when a.TerritoryCode = 7 then 863940005
                                                when a.TerritoryCode = 8 then 863940001
                                                when a.TerritoryCode = 9 then 863940008
                                                when a.TerritoryCode = 10 then 863940010
                                                when a.TerritoryCode = 11 then 863940011
                                                when a.TerritoryCode = 12 then 863940012
                                                when a.TerritoryCode = 13 then 863940013
                                                when a.TerritoryCode = 14 then 863940006
                                                when a.TerritoryCode = 15 then 863940009
                                                else null
                                            end as 'osv_offertorycensusprogram', 
                                            --a.New_CensusVersion as 'allgnt_OffertoryCensusProgramVersion',  -- There are no records with a value for this field
                                            a.New_OnlineGiving as 'OnlineGiving', 
                                            a.New_Website as 'Website',  
                                            cast(a.CFBbilingual as int) as 'allgnt_bilingual', 
											cast(case	
		                                        when a.New_OLGCompetitor = 1 then '26A0469C-2577-E011-A404-000BCDCD8B77'
		                                        when a.New_OLGCompetitor = 2 then 'F64D606A-2777-E011-A404-000BCDCD8B77'
		                                        when a.New_OLGCompetitor = 3 then 'FD8BC275-273E-E111-8536-000BCDCD8B77' 
		                                        else null
	                                        end as uniqueidentifier) as 'competitor1', 
                                            cast(case 
	                                            when a.CFPcompetitorcode = 1 then 'C6065044-E6AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 2 then '531CB8AF-E4AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 3 then '1C7AB319-E7AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 4 then '4C528632-E4AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 5 then '9E31DC6F-E6AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 6 then '839BA1D0-E4AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 34 then 'D18875FA-2677-E011-A404-000BCDCD8B77'
	                                            when a.CFPcompetitorcode = 7 then '56BE9F27-E7AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 8 then '942FD6F5-E4AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 11 then '542C5C10-E5AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 12 then '9065AB9C-E5AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 15 then 'EAC38ABF-E6AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 17 then 'D06A2158-E4AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 18 then '366B22C5-E5AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 19 then 'F7918C37-E5AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 20 then 'fdfd8576-aefa-4907-a60c-723af61c5dbd'
	                                            when a.CFPcompetitorcode = 21 then 'db384190-fb12-4b2d-8fee-e4cbeb6f8fdb'
	                                            when a.CFPcompetitorcode = 22 then '0AC429AC-0FB0-E111-8536-000BCDCD8B77'
	                                            when a.CFPcompetitorcode = 23 then 'E076814E-E7AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 24 then 'AAD065FC-E5AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 25 then '44d10f67-d63d-41b0-8a30-5e30c2aa095f'
	                                            when a.CFPcompetitorcode = 26 then '4858DE5E-E5AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 27 then '86836BF7-E6AB-DB11-81D9-000BCDCD88A8'
	                                            when a.CFPcompetitorcode = 28 then '561D7180-E5AB-DB11-81D9-000BCDCD88A8' 
	                                            else null
                                            end as uniqueidentifier) as 'competitor2',
                                            c.ParentCustomerId,
                                            CFInumberofparishioners as 'allgnt_NumberofMembers',
                                            c.Emailaddress1 as 'allgnt_OffertoryPrimaryContactEmail'
                                            from Account a
                                            left join Contact c
												on a.PrimaryContactId = c.ContactId 
                                             ";

        }

        public override bool IsUpdateable(Account entity)
        { 
            return DestinationKeyExists(entity.Id,"Account");// && (entity.PrimaryContactId == null || entity.PrimaryContactId != null && Project.Dictionaries.ImportedContactIds.ContainsKey(entity.PrimaryContactId.Id));
        }

        protected override bool MapField(string name, IDataReader reader, NewEntityModel model)
        {
            if (name == "Website")
            {
                if (!reader.IsDBNull(reader.GetOrdinal("Website")) && reader.GetTypedValue<bool>("Website"))
                {
                    Guid accountId = reader.GetTypedValue<Guid>("AccountId"); 
                    var olgId = Project.Dictionaries.OSProducts["Websites"];
                    model.Subactions.Add(new Action<CrmContext, IOrganizationService>((context, service) =>
                    { 
                        var productReference = new EntityReference(allgnt_offertorysolutionsproduct.EntityLogicalName, olgId);

                        EntityReferenceCollection entities = new EntityReferenceCollection();
                        entities.Add(productReference);

                        Relationship relationship = new Relationship("allgnt_account_allgnt_offertorysolutionsproduct");

                        try
                        {
                            service.Associate(Account.EntityLogicalName, accountId, relationship, entities);
                        }
                        catch (System.ServiceModel.FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                        {
                            // This exception would only occur when the item has already been added
                            if (ex.Message != "Cannot insert duplicate key.")
                                throw ex;
                        }
                    }));
                }
                return true;
            }
            else if (name == "OnlineGiving")
            {
                if (!reader.IsDBNull(reader.GetOrdinal("OnlineGiving")) && reader.GetTypedValue<bool>("OnlineGiving"))
                {
                    Guid accountId = reader.GetTypedValue<Guid>("AccountId");
                    var olgId = Project.Dictionaries.OSProducts["OLG"];
                    model.Subactions.Add((context, service) =>
                    { 
                        var productReference = new EntityReference(allgnt_offertorysolutionsproduct.EntityLogicalName, olgId);

                        EntityReferenceCollection entities = new EntityReferenceCollection();
                        entities.Add(productReference);

                        Relationship relationship = new Relationship("allgnt_account_allgnt_offertorysolutionsproduct");

                        try
                        {
                            service.Associate(Account.EntityLogicalName, accountId, relationship, entities);
                        }
                        catch (System.ServiceModel.FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                        {
                            // This exception would only occur when the item has already been added
                            if (ex.Message != "Cannot insert duplicate key.")
                                throw ex;
                        }
                    });
                }
                return true;
            }
            else if (name == "competitor1" || name=="competitor2")
            {
                Guid? competitorid = reader.GetTypedValue<Guid?>(name);
                if (competitorid.HasValue)
                {
                    Guid accountId = reader.GetTypedValue<Guid>("AccountId");
                    model.Subactions.Add((context, service) =>
                    { 
                        var competitorReference = new EntityReference(Competitor.EntityLogicalName, competitorid.Value);

                        EntityReferenceCollection entities = new EntityReferenceCollection();
                        entities.Add(competitorReference);
                        Relationship relationship = new Relationship("allgnt_account_competitor");

                        try
                        {
                            service.Associate(Account.EntityLogicalName, accountId, relationship, entities);
                        }
                        catch (System.ServiceModel.FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                        {
                            // This exception would only occur when the item has already been added
                            if (ex.Message != "Cannot insert duplicate key.")
                                throw ex;
                        }
                    });
                }
                return true;
            }
            else if (name == "PrimaryContactId")
            {
                Guid? contactid = reader.GetTypedValue<Guid?>("PrimaryContactId");
                Guid? parentCustomerId = reader.GetTypedValue<Guid?>("ParentCustomerId");
                Guid accountId = reader.GetTypedValue<Guid>("AccountId");
                
                if (contactid.HasValue && parentCustomerId.HasValue && DestinationKeyExists(contactid.Value,"Contact") && accountId == parentCustomerId.Value)
                    model.Entity.PrimaryContactId = new EntityReference("contact", contactid.Value);
                else 
                    Log.Info(string.Format("Primary Contact not set for Account: {0} Contact {1} Contact ParentCustomerId {2}.", model.Entity.Id, contactid, parentCustomerId));

                return true;
            }
            else if (name == "ParentCustomerId")
            {
                // Used to map PrimaryContactId
                return true;
            }
            else if (name == "Description" && !string.IsNullOrEmpty(reader.GetTypedValue<string>("Description")))
            {
                Guid accountid = reader.GetTypedValue<Guid>("AccountId");
                string description = reader.GetTypedValue<string>("Description");
                model.Subactions.Add((context, service) =>
                    {
                        Annotation note = new Annotation();
                        note.ObjectId = new EntityReference(Account.EntityLogicalName, accountid);
                        note.NoteText = description;

                        service.Create(note);
                    });
                return true;
            } 
            else if (name == "allgnt_ProductSpecialist" && reader.GetTypedValue<int?>("allgnt_ProductSpecialist").HasValue)
            {
                int? ps = reader.GetTypedValue<int?>("allgnt_ProductSpecialist").Value;
                Guid psid = Guid.Empty;
                switch (ps)
                {
                    case 1:
                        psid = Project.Dictionaries.RepresentativeIds["E31"];   
                        break;
                    case 2:
                        psid = Project.Dictionaries.RepresentativeIds["E62"];  
                        break;
                    case 3:
                        psid = Project.Dictionaries.RepresentativeIds["E56"];   
                        break;
                    case 4:
                        psid = Project.Dictionaries.RepresentativeIds["E65"];   
                        break;
                    case 5:
                        psid = Project.Dictionaries.RepresentativeIds["E60"];   
                        break;
                    default:
                        throw new Exception("No representative found");
                }

                model.Entity.allgnt_ProductSpecialist = new EntityReference(allgnt_representative.EntityLogicalName, psid);
                return true;
            }
            else if (name == "allgnt_OffertoryConsultant" && reader.GetTypedValue<int?>("allgnt_OffertoryConsultant").HasValue)
            {
                int? ps = reader.GetTypedValue<int?>("allgnt_OffertoryConsultant").Value;
                Guid psid = Guid.Empty;
                switch (ps)
                {
                    case 1:
                        psid = Project.Dictionaries.RepresentativeIds["E23"];  
                        break;
                    case 2:
                        psid = Project.Dictionaries.RepresentativeIds["E34"];   
                        break;
                    case 3:
                        psid = Project.Dictionaries.RepresentativeIds["E68"];   
                        break;
                    case 4:
                        psid = Project.Dictionaries.RepresentativeIds["E62"];   
                        break;
                    case 5:
                        psid = Project.Dictionaries.RepresentativeIds["E65"];  
                        break;
                    case 6:
                        psid = Project.Dictionaries.RepresentativeIds["E77"];   
                        break;
                    default:
                        throw new Exception("No representative found");
                }

                if (psid != Guid.Empty)
                    model.Entity.allgnt_OffertoryConsultant = new EntityReference(allgnt_representative.EntityLogicalName, psid);
               
                return true;
            }
            return false; 
        }
    }
}
