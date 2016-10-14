using System.Collections.Generic;

namespace CRMDataImport
{
    /// <summary>
    /// This is for dictionaries that are loaded once and used throughout the application
    /// </summary>
    public static class StaticDictionaries
    {

        static StaticDictionaries()
        {
            LoadEntityTypeCodes();
            LoadStates();
        }

        public static Dictionary<string, string> StateAbbreviationToFullName = new Dictionary<string, string>();
        public static Dictionary<string, string> StateFullNameToAbbreviation = new Dictionary<string, string>();
        public static void LoadStates()
        {
            StateAbbreviationToFullName.Add("AL", "Alabama");
            StateAbbreviationToFullName.Add("AK", "Alaska");
            StateAbbreviationToFullName.Add("AZ", "Arizona");
            StateAbbreviationToFullName.Add("AR", "Arkansas");
            StateAbbreviationToFullName.Add("CA", "California");
            StateAbbreviationToFullName.Add("CO", "Colorado");
            StateAbbreviationToFullName.Add("CT", "Connecticut");
            StateAbbreviationToFullName.Add("DE", "Delaware");
            StateAbbreviationToFullName.Add("DC", "District of Columbia");
            StateAbbreviationToFullName.Add("FL", "Florida");
            StateAbbreviationToFullName.Add("GA", "Georgia");
            StateAbbreviationToFullName.Add("HI", "Hawaii");
            StateAbbreviationToFullName.Add("ID", "Idaho");
            StateAbbreviationToFullName.Add("IL", "Illinois");
            StateAbbreviationToFullName.Add("IN", "Indiana");
            StateAbbreviationToFullName.Add("IA", "Iowa");
            StateAbbreviationToFullName.Add("KS", "Kansas");
            StateAbbreviationToFullName.Add("KY", "Kentucky");
            StateAbbreviationToFullName.Add("LA", "Louisiana");
            StateAbbreviationToFullName.Add("ME", "Maine");
            StateAbbreviationToFullName.Add("MD", "Maryland");
            StateAbbreviationToFullName.Add("MA", "Massachusetts");
            StateAbbreviationToFullName.Add("MI", "Michigan");
            StateAbbreviationToFullName.Add("MN", "Minnesota");
            StateAbbreviationToFullName.Add("MS", "Mississippi");
            StateAbbreviationToFullName.Add("MO", "Missouri");
            StateAbbreviationToFullName.Add("MT", "Montana");
            StateAbbreviationToFullName.Add("NE", "Nebraska");
            StateAbbreviationToFullName.Add("NV", "Nevada");
            StateAbbreviationToFullName.Add("NH", "New Hampshire");
            StateAbbreviationToFullName.Add("NJ", "New Jersey");
            StateAbbreviationToFullName.Add("NM", "New Mexico");
            StateAbbreviationToFullName.Add("NY", "New York");
            StateAbbreviationToFullName.Add("NC", "North Carolina");
            StateAbbreviationToFullName.Add("ND", "North Dakota");
            StateAbbreviationToFullName.Add("OH", "Ohio");
            StateAbbreviationToFullName.Add("OK", "Oklahoma");
            StateAbbreviationToFullName.Add("OR", "Oregon");
            StateAbbreviationToFullName.Add("PA", "Pennsylvania");
            StateAbbreviationToFullName.Add("RI", "Rhode Island");
            StateAbbreviationToFullName.Add("SC", "South Carolina");
            StateAbbreviationToFullName.Add("SD", "South Dakota");
            StateAbbreviationToFullName.Add("TN", "Tennessee");
            StateAbbreviationToFullName.Add("TX", "Texas");
            StateAbbreviationToFullName.Add("UT", "Utah");
            StateAbbreviationToFullName.Add("VT", "Vermont");
            StateAbbreviationToFullName.Add("VA", "Virginia");
            StateAbbreviationToFullName.Add("WA", "Washington");
            StateAbbreviationToFullName.Add("WV", "West Virginia");
            StateAbbreviationToFullName.Add("WI", "Wisconsin");
            StateAbbreviationToFullName.Add("WY", "Wyoming");

            StateFullNameToAbbreviation = new Dictionary<string, string>();
            foreach (var i in StateAbbreviationToFullName)
            {
                StateFullNameToAbbreviation.Add(i.Value, i.Key);
            } 
        }

        public static Dictionary<int, string> EntityTypeCodes { get; set; }
        private static void LoadEntityTypeCodes()
        {
            EntityTypeCodes = new Dictionary<int, string>();
            EntityTypeCodes.Add(0, "unknown".ToLower());
            EntityTypeCodes.Add(1, "Account".ToLower());
            EntityTypeCodes.Add(16, "AccountLeads".ToLower());
            EntityTypeCodes.Add(1001, "ActivityMimeAttachment".ToLower());
            EntityTypeCodes.Add(135, "ActivityParty".ToLower());
            EntityTypeCodes.Add(4603, "ActivityPartyRollupByAccount".ToLower());
            EntityTypeCodes.Add(4604, "ActivityPartyRollupByContact".ToLower());
            EntityTypeCodes.Add(4200, "ActivityPointer".ToLower());
            EntityTypeCodes.Add(5, "Annotation".ToLower());
            EntityTypeCodes.Add(2000, "AnnualFiscalCalendar".ToLower());
            EntityTypeCodes.Add(4201, "Appointment".ToLower());
            EntityTypeCodes.Add(4601, "AttributeMap".ToLower());
            EntityTypeCodes.Add(4407, "BulkImport".ToLower());
            EntityTypeCodes.Add(4406, "BulkOperation".ToLower());
            EntityTypeCodes.Add(4405, "BulkOperationLog".ToLower());
            EntityTypeCodes.Add(10, "BusinessUnit".ToLower());
            EntityTypeCodes.Add(6, "BusinessUnitMap".ToLower());
            EntityTypeCodes.Add(132, "BusinessUnitNewsArticle".ToLower());
            EntityTypeCodes.Add(4003, "Calendar".ToLower());
            EntityTypeCodes.Add(4004, "CalendarRule".ToLower());
            EntityTypeCodes.Add(4400, "Campaign".ToLower());
            EntityTypeCodes.Add(4402, "CampaignActivity".ToLower());
            EntityTypeCodes.Add(4404, "CampaignActivityItem".ToLower());
            EntityTypeCodes.Add(4403, "CampaignItem".ToLower());
            EntityTypeCodes.Add(4401, "CampaignResponse".ToLower());
            EntityTypeCodes.Add(4215, "Commitment".ToLower());
            EntityTypeCodes.Add(123, "Competitor".ToLower());
            EntityTypeCodes.Add(1004, "CompetitorAddress".ToLower());
            EntityTypeCodes.Add(1006, "CompetitorProduct".ToLower());
            EntityTypeCodes.Add(26, "CompetitorSalesLiterature".ToLower());
            EntityTypeCodes.Add(4007, "ConstraintBasedGroup".ToLower());
            EntityTypeCodes.Add(2, "Contact".ToLower());
            EntityTypeCodes.Add(17, "ContactInvoices".ToLower());
            EntityTypeCodes.Add(22, "ContactLeads".ToLower());
            EntityTypeCodes.Add(19, "ContactOrders".ToLower());
            EntityTypeCodes.Add(18, "ContactQuotes".ToLower());
            EntityTypeCodes.Add(1010, "Contract".ToLower());
            EntityTypeCodes.Add(1011, "ContractDetail".ToLower());
            EntityTypeCodes.Add(2011, "ContractTemplate".ToLower());
            EntityTypeCodes.Add(1071, "CustomerAddress".ToLower());
            EntityTypeCodes.Add(4503, "CustomerOpportunityRole".ToLower());
            EntityTypeCodes.Add(4502, "CustomerRelationship".ToLower());
            EntityTypeCodes.Add(1013, "Discount".ToLower());
            EntityTypeCodes.Add(1080, "DiscountType".ToLower());
            EntityTypeCodes.Add(126, "DocumentIndex".ToLower());
            EntityTypeCodes.Add(4202, "Email".ToLower());
            EntityTypeCodes.Add(4600, "EntityMap".ToLower());
            EntityTypeCodes.Add(4000, "Equipment".ToLower());
            EntityTypeCodes.Add(4204, "Fax".ToLower());
            EntityTypeCodes.Add(30, "FilterTemplate".ToLower());
            EntityTypeCodes.Add(2004, "FixedMonthlyFiscalCalendar".ToLower());
            EntityTypeCodes.Add(4408, "ImportConfig".ToLower());
            EntityTypeCodes.Add(112, "Incident".ToLower());
            EntityTypeCodes.Add(4206, "IncidentResolution".ToLower());
            EntityTypeCodes.Add(3000, "IntegrationStatus".ToLower());
            EntityTypeCodes.Add(1003, "InternalAddress".ToLower());
            EntityTypeCodes.Add(1090, "Invoice".ToLower());
            EntityTypeCodes.Add(1091, "InvoiceDetail".ToLower());
            EntityTypeCodes.Add(127, "KbArticle".ToLower());
            EntityTypeCodes.Add(1082, "KbArticleComment".ToLower());
            EntityTypeCodes.Add(1016, "KbArticleTemplate".ToLower());
            EntityTypeCodes.Add(4, "Lead".ToLower());
            EntityTypeCodes.Add(1017, "LeadAddress".ToLower());
            EntityTypeCodes.Add(24, "LeadCompetitors".ToLower());
            EntityTypeCodes.Add(27, "LeadProduct".ToLower());
            EntityTypeCodes.Add(4207, "Letter".ToLower());
            EntityTypeCodes.Add(2027, "License".ToLower());
            EntityTypeCodes.Add(4300, "List".ToLower());
            EntityTypeCodes.Add(4301, "ListMember".ToLower());
            EntityTypeCodes.Add(2003, "MonthlyFiscalCalendar".ToLower());
            EntityTypeCodes.Add(3, "Opportunity".ToLower());
            EntityTypeCodes.Add(4208, "OpportunityClose".ToLower());
            EntityTypeCodes.Add(25, "OpportunityCompetitors".ToLower());
            EntityTypeCodes.Add(1083, "OpportunityProduct".ToLower());
            EntityTypeCodes.Add(4209, "OrderClose".ToLower());
            EntityTypeCodes.Add(1019, "Organization".ToLower());
            EntityTypeCodes.Add(7, "OrganizationMap".ToLower());
            EntityTypeCodes.Add(1021, "OrganizationUI".ToLower());
            EntityTypeCodes.Add(4210, "PhoneCall".ToLower());
            EntityTypeCodes.Add(4602, "PluginType".ToLower());
            EntityTypeCodes.Add(1022, "PriceLevel".ToLower());
            EntityTypeCodes.Add(11, "PrincipalObjectAccess".ToLower());
            EntityTypeCodes.Add(1023, "Privilege".ToLower());
            EntityTypeCodes.Add(31, "PrivilegeObjectTypeCodes".ToLower());
            EntityTypeCodes.Add(1024, "Product".ToLower());
            EntityTypeCodes.Add(1025, "ProductAssociation".ToLower());
            EntityTypeCodes.Add(1026, "ProductPriceLevel".ToLower());
            EntityTypeCodes.Add(21, "ProductSalesLiterature".ToLower());
            EntityTypeCodes.Add(1028, "ProductSubstitute".ToLower());
            EntityTypeCodes.Add(2002, "QuarterlyFiscalCalendar".ToLower());
            EntityTypeCodes.Add(2020, "Queue".ToLower());
            EntityTypeCodes.Add(2029, "QueueItem".ToLower());
            EntityTypeCodes.Add(1084, "Quote".ToLower());
            EntityTypeCodes.Add(4211, "QuoteClose".ToLower());
            EntityTypeCodes.Add(1085, "QuoteDetail".ToLower());
            EntityTypeCodes.Add(4500, "RelationshipRole".ToLower());
            EntityTypeCodes.Add(4501, "RelationshipRoleMap".ToLower());
            EntityTypeCodes.Add(4002, "Resource".ToLower());
            EntityTypeCodes.Add(4005, "ResourceGroup".ToLower());
            EntityTypeCodes.Add(4006, "ResourceSpec".ToLower());
            EntityTypeCodes.Add(1036, "Role".ToLower());
            EntityTypeCodes.Add(12, "RolePrivileges".ToLower());
            EntityTypeCodes.Add(1037, "RoleTemplate".ToLower());
            EntityTypeCodes.Add(28, "RoleTemplatePrivileges".ToLower());
            EntityTypeCodes.Add(1038, "SalesLiterature".ToLower());
            EntityTypeCodes.Add(1070, "SalesLiteratureItem".ToLower());
            EntityTypeCodes.Add(1088, "SalesOrder".ToLower());
            EntityTypeCodes.Add(1089, "SalesOrderDetail".ToLower());
            EntityTypeCodes.Add(1039, "SavedQuery".ToLower());
            EntityTypeCodes.Add(2001, "SemiAnnualFiscalCalendar".ToLower());
            EntityTypeCodes.Add(4001, "Service".ToLower());
            EntityTypeCodes.Add(4214, "ServiceAppointment".ToLower());
            EntityTypeCodes.Add(20, "ServiceContractContacts".ToLower());
            EntityTypeCodes.Add(4009, "Site".ToLower());
            EntityTypeCodes.Add(1075, "StatusMap".ToLower());
            EntityTypeCodes.Add(1043, "StringMap".ToLower());
            EntityTypeCodes.Add(129, "Subject".ToLower());
            EntityTypeCodes.Add(29, "Subscription".ToLower());
            EntityTypeCodes.Add(8, "SystemUser".ToLower());
            EntityTypeCodes.Add(13, "SystemUserLicenses".ToLower());
            EntityTypeCodes.Add(14, "SystemUserPrincipals".ToLower());
            EntityTypeCodes.Add(15, "SystemUserRoles".ToLower());
            EntityTypeCodes.Add(4212, "Task".ToLower());
            EntityTypeCodes.Add(9, "Team".ToLower());
            EntityTypeCodes.Add(23, "TeamMembership".ToLower());
            EntityTypeCodes.Add(2010, "Template".ToLower());
            EntityTypeCodes.Add(2013, "Territory".ToLower());
            EntityTypeCodes.Add(2012, "UnresolvedAddress".ToLower());
            EntityTypeCodes.Add(1055, "UoM".ToLower());
            EntityTypeCodes.Add(1056, "UoMSchedule".ToLower());
            EntityTypeCodes.Add(1086, "UserFiscalCalendar".ToLower());
            EntityTypeCodes.Add(4230, "UserQuery".ToLower());
            EntityTypeCodes.Add(150, "UserSettings".ToLower());
            EntityTypeCodes.Add(1061, "WFAction".ToLower());
            EntityTypeCodes.Add(1062, "WFActionLog".ToLower());
            EntityTypeCodes.Add(1064, "WFCondition".ToLower());
            EntityTypeCodes.Add(1065, "WFEventLog".ToLower());
            EntityTypeCodes.Add(1092, "WFEventQueue".ToLower());
            EntityTypeCodes.Add(1066, "WFParameter".ToLower());
            EntityTypeCodes.Add(121, "WFProcess".ToLower());
            EntityTypeCodes.Add(122, "WFProcessInstance".ToLower());
            EntityTypeCodes.Add(1067, "WFRule".ToLower());
            EntityTypeCodes.Add(1068, "WFRuleLog".ToLower());
            EntityTypeCodes.Add(1069, "WFStep".ToLower());
        }
    }
}
