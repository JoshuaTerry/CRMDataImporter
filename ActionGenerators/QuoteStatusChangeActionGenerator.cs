using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Linq;

namespace CRMDataImport.ActionGenerators
{
    public class QuoteStatusChangeActionGenerator : ImportQueryActionGenerator
    {
        public QuoteStatusChangeActionGenerator()
            : base(SourceDatabaseEnum.CRM3)
        {
            Notes = @"AFTER IMPORT
Some Opportunities will have multiple Quotes in a Draft status which is not valid.  Find and delete these Quotes.";

            this.Query = @"select 
                            QuoteId,
                            StateCode,
                            StatusCode
                            from Quote  
                         where StateCode <> 0 
";
            AllowParallelism = true;
        }
                 
        protected override Action<IOrganizationService, Osv.Crm.Entities.CrmContext> MakeRowAction(System.Data.IDataReader reader)
        {
            Guid id = reader.GetTypedValue<Guid>("QuoteId");
            int statecode = reader.GetTypedValue<int>("StateCode");
            int statuscode = reader.GetTypedValue<int>("StatusCode"); 

            if (!DataMigration.CurrentProject.Dictionaries.HasKeyBeenImported(id,"Quote"))
                return null;
             
            return new Action<IOrganizationService, CrmContext>((service, context) =>
            {
                var quote = (from q in context.QuoteSet
                             where q.QuoteId == id
                             select q).FirstOrDefault();
                
                if ((int)quote.StateCode != statecode)
                {
                    if (statecode == 1) // Change Status from Draft to Active
                    {
                        ActivateQuote(service, id);
                    }
                    else if (statecode == 2)
                    {
                        if (quote.StateCode != QuoteState.Active)
                            ActivateQuote(service, id);

                        WinQuote(service, id, statuscode);
                    }
                    else if (statecode == 3)
                    {
                        if (quote.StateCode != QuoteState.Active)
                            ActivateQuote(service, id);

                        CloseQuote(service, id, statuscode);
                    }
                }
            });
        }
    
        /// <summary>
        /// Activate a Quote
        /// </summary>
        /// <param name="service"></param>
        /// <param name="id"></param>
        private void ActivateQuote(IOrganizationService service, Guid id)
        {
            SetStateRequest request = new SetStateRequest();
            request.EntityMoniker = new EntityReference(Quote.EntityLogicalName, id);
            request.State = new OptionSetValue(1);
            request.Status = new OptionSetValue(2);
            service.Execute(request);  
        }

        /// <summary>
        /// Win the Quote
        /// </summary>
        /// <param name="service"></param>
        /// <param name="id"></param>
        /// <param name="statuscode"></param>
        private void WinQuote(IOrganizationService service, Guid id, int statuscode)
        {
            WinQuoteRequest r = new WinQuoteRequest();
            // Due to the order they were entered and the number assigned by CRM 2011, adding 100,000,000 and then subtracting 9
            // should arrive the the equivalent value.  This did not apply to statuscode 4 because it is a system status and does not get
            // assigned a number prefix
            r.Status = new OptionSetValue(statuscode == 4 ? 4 : statuscode + 100000000 - 9);
            QuoteClose qc = new QuoteClose();
            // Added manual set of Activity Id to aid in deleting the close
            qc.ActivityId = Guid.NewGuid();
            qc.QuoteId = new EntityReference(Quote.EntityLogicalName, id);
            qc.Subject = "Migration Win";
            r.QuoteClose = qc;
            service.Execute(r);

            // Delete the Close Activity that was just created
            service.Delete(QuoteClose.EntityLogicalName, qc.ActivityId.Value);
        }

        /// <summary>
        /// Close the Quote
        /// </summary>
        /// <param name="service"></param>
        /// <param name="id"></param>
        /// <param name="statuscode"></param>
        private void CloseQuote(IOrganizationService service, Guid id, int statuscode)
        {
            CloseQuoteRequest r = new CloseQuoteRequest();
            r.Status = new OptionSetValue(statuscode);
            QuoteClose qc = new QuoteClose();
            // Added manual set of Activity Id to aid in deleting the close
            qc.ActivityId = Guid.NewGuid();
            qc.QuoteId = new EntityReference(Quote.EntityLogicalName, id);
            qc.Subject = "Migration Close";
            r.QuoteClose = qc;
            service.Execute(r);

            // Delete the Close Activity that was just created
            service.Delete(QuoteClose.EntityLogicalName, qc.ActivityId.Value);
        }
    }
}
