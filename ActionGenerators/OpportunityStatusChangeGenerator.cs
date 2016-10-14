using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Linq;

namespace CRMDataImport.ActionGenerators
{
    public class OpportunityStatusChangeGenerator : ImportQueryActionGenerator
    {
        public OpportunityStatusChangeGenerator()
            : base(SourceDatabaseEnum.CRM3)
        {
             
            this.Query = @"
                        select 
                            OpportunityId,
                            StateCode,
                            case	
								-- Lost Status Codes
								when StatusCode = 4 then 739280002
								when StatusCode = 5 then 739280004
								when StatusCode = 6 then 739280001
								when StatusCode = 13 then 739280001
								when StatusCode = 14 then 739280003
								when StatusCode = 15 then 739280001
								when StatusCode = 16 then 739280004
								when StatusCode = 18 then 739280004
								when StatusCode = 19 then 739280000 
								-- Won Status Codes
								when StatusCode = 17 then 3
								when StatusCode = 7 then 739280008
								when StatusCode = 8 then 739280009
								when StatusCode = 9 then 739280010
								when StatusCode = 10 then 739280011
								when StatusCode = 11 then 739280012
								when StatusCode = 12 then 739280013 
								else StatusCode
							end as 'StatusCode'
                            from Opportunity  
                         where StateCode <> 0
";

            AllowParallelism = true;
        }
         
        protected override Action<IOrganizationService, Osv.Crm.Entities.CrmContext> MakeRowAction(System.Data.IDataReader reader)
        {
            Guid id = reader.GetTypedValue<Guid>("OpportunityId");
            int statecode = reader.GetTypedValue<int>("StateCode");
            int statuscode = reader.GetTypedValue<int>("StatusCode");
            string rowData =  Common.TryReaderRowToString(reader);

            if (!DataMigration.CurrentProject.Dictionaries.HasKeyBeenImported(id, "Opportunity"))
                return null;
             
            return new Action<IOrganizationService, CrmContext>((service, context) =>
            {
                var entity = (from e in context.OpportunitySet
                             where e.OpportunityId == id
                             select e).FirstOrDefault();

                if ((int)entity.StateCode != statecode)
                {
                    try
                    {
                        if (statecode == 1)
                        {
                            WinOpportunity(service, id, statuscode);
                        }
                        else if (statecode == 2)
                        {
                            LoseOpportunity(service, id, statuscode);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!ex.Message.Contains("The opportunity can not be closed"))
                            throw;

                        Log.Warn(string.Format("Opportunity could not be closed.  RowData: {0}", rowData), ex);
                    }
                }
            });
        }
         
        /// <summary>
        /// Win the Opportunity
        /// </summary>
        /// <param name="service"></param>
        /// <param name="id"></param>
        /// <param name="statuscode"></param>
        private void WinOpportunity(IOrganizationService service, Guid id, int statuscode)
        {
            WinOpportunityRequest request = new WinOpportunityRequest();
            request.Status = new OptionSetValue(statuscode);
            OpportunityClose close = new OpportunityClose();
            // Added manual set of Activity Id to aid in deleting the close
            close.ActivityId = Guid.NewGuid();
            close.OpportunityId = new EntityReference(Opportunity.EntityLogicalName, id);
            close.Subject = "Migration Win";
            request.OpportunityClose = close;
            service.Execute(request);

            // Delete the Close Activity that was just created
            service.Delete(OpportunityClose.EntityLogicalName, close.ActivityId.Value);            
        }

        /// <summary>
        /// Lose the Opportunity
        /// </summary>
        /// <param name="service"></param>
        /// <param name="id"></param>
        /// <param name="statuscode"></param>
        private void LoseOpportunity(IOrganizationService service, Guid id, int statuscode)
        { 
            LoseOpportunityRequest request = new LoseOpportunityRequest();
            request.Status = new OptionSetValue(statuscode);
            OpportunityClose close = new OpportunityClose();
            // Added manual set of Activity Id to aid in deleting the close
            close.ActivityId = Guid.NewGuid();
            close.OpportunityId = new EntityReference(Opportunity.EntityLogicalName, id);
            close.Subject = "Migration Lost";
            request.OpportunityClose = close;
            service.Execute(request);

            // Delete the Close Activity that was just created
            service.Delete(OpportunityClose.EntityLogicalName, close.ActivityId.Value);
        }
    }
}
