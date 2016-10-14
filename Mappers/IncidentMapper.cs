using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
    public class IncidentMapper : MapperBase<Incident>
	{
        public IncidentMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
		{
            Notes = @"BEFORE IMPORT
Make sure that Case Dollar Mappings and Subject Mappings have been completed.

Ensure that all Currency Fields have the precision set to 2 BEFORE the import.";
            if (update)
            {
                this.Query = @"select 
                                        i.IncidentId, 
                                         COALESCE(cdm.NewSubjectId, csm.NewSubjectId) as 'SubjectId', 
                                        'subject' as 'SubjectIdLogicalName', 
                                        cdm.Credits as 'allgnt_Credit',
                                        cdm.[Bulk] as 'allgnt_Bulk',
                                        cdm.Reruns as 'allgnt_Rerun',
                                        cdm.StarterSets as 'allgnt_StarterSets',
                                        cdm.AdminTime as 'allgnt_AdminTime'  
                                        from Incident i  
										left join CaseSubjectMapping csm
											on csm.OldSubjectId = i.SubjectId
										left join CaseDollarMappings cdm
											on cdm.IncidentId = i.IncidentId 
 "; 
            }
            else
            {
                this.Query = @"select 
                                        i.IncidentId, 
                                        su.DomainName as 'OwnerId', 
                                        case 
	                                        when i.CustomerIdType = 1 then i.CustomerId
	                                        when i.CustomerIdType = 2 then c.AccountId
                                        end as 'CustomerId',
                                        'account' as 'CustomerIdLogicalName',
                                        case 
	                                        when i.CustomerIdType = 1 then null
	                                        when i.CustomerIdType = 2 then i.CustomerId
                                        end as 'allgnt_Contact',
                                        'contact' as 'allgnt_ContactLogicalName', 
                                        a.AccountNumber as allgnt_CustomerNumber,
                                        i.Title,
                                        COALESCE(cdm.NewSubjectId, csm.NewSubjectId) as 'SubjectId', 
                                        'subject' as 'SubjectIdLogicalName',
                                        i.FollowupBy,
                                        i.New_OrderNumber as 'allgnt_OrderNumber',
                                        i.New_NoofNonDeliverySets as 'allgnt_NumberofNonDeliverySets',
                                         case
                                            when i.New_IssueReportedBy = 1 then 739280001
                                            when i.New_IssueReportedBy = 2 then 739280000
                                            else null
                                        end as 'allgnt_reportedby',
                                        i.[Description],
                                        i.CreatedOn as 'OverriddenCreatedOn', 
                                        i.KbArticleId,
                                        'kbarticle' as 'KbArticleIdLogicalName' ,
                                        cdm.Credits as 'allgnt_Credit',
                                        cdm.[Bulk] as 'allgnt_Bulk',
                                        cdm.Reruns as 'allgnt_Rerun',
                                        cdm.StarterSets as 'allgnt_StarterSets',
                                        cdm.AdminTime as 'allgnt_AdminTime'  
                                        from Incident i 
                                        inner join SystemUser su 
	                                        on i.OwnerId = su.SystemUserId
                                        left join Account a
	                                        on a.AccountId = i.AccountId
	                                    left join Contact c
											on c.ContactId = i.ContactId
										left join CaseSubjectMapping csm
											on csm.OldSubjectId = i.SubjectId
										left join CaseDollarMappings cdm
											on cdm.IncidentId = i.IncidentId  ";
            }
		}

		public override bool IsImportable(Incident entity)
		{
            return (!DestinationKeyExists(entity.IncidentId.Value,"Incident") && entity.CustomerId != null && DestinationKeyExists(entity.CustomerId.Id, "Account"));
		}

        public override bool IsUpdateable(Incident entity)
        {
            return (DestinationKeyExists(entity.IncidentId.Value,"Incident"));
        }
	}
}

