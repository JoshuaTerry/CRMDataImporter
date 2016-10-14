using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;

namespace CRMDataImport.Mappers
{
    public class CompetitorMapper : MapperBase<Competitor>
    {
        public CompetitorMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        {
            this.Query = @"select 
                                    c.CompetitorId,
                                    c.[Name],
                                    c.KeyProduct,
                                    c.WebSiteUrl,
                                    c.ReportedRevenue,
                                    c.Address1_Line1,
                                    c.Address1_Line2,
                                    c.Address1_Line3,
                                    c.Address1_City,
                                    c.Address1_StateOrProvince,
                                    c.Address1_PostalCode,
                                    c.Address1_Country,
                                    c.Overview,
                                    c.Strengths,
                                    c.Weaknesses,
                                    c.Opportunities,
                                    c.Threats  
                                    from Competitor c  ";
        }

        public override bool IsImportable(Competitor entity)
        {
            return !DestinationKeyExists(entity.CompetitorId.Value,"Competitor");
        }
         
        protected override Action<IOrganizationService, CrmContext> MakeLastAction()
        {
            // These are Competitors that only existed as list values in CRM 3.0
            return new Action<IOrganizationService, CrmContext>((service, context) =>
            { 
                service.Create(new Competitor { CompetitorId = Guid.Parse("d1d6d27f-f651-e311-ace4-00155d028117"), Name = "[Redacted]" });
                service.Create(new Competitor { CompetitorId = Guid.Parse("d2d6d27f-f651-e311-ace4-00155d028117"), Name = "[Redacted]" });
                service.Create(new Competitor { CompetitorId = Guid.Parse("fdfd8576-aefa-4907-a60c-723af61c5dbd"), Name = "[Redacted]" });
                service.Create(new Competitor { CompetitorId = Guid.Parse("db384190-fb12-4b2d-8fee-e4cbeb6f8fdb"), Name = "[Redacted]" });
                service.Create(new Competitor { CompetitorId = Guid.Parse("44d10f67-d63d-41b0-8a30-5e30c2aa095f"), Name = "[Redacted]" });
            });
        } 
    }
}

