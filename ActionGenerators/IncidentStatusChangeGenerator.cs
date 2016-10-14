using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Linq;

namespace CRMDataImport.ActionGenerators
{
    public class IncidentStatusChangeGenerator : StatusChangeGeneratorBase
    {
        public IncidentStatusChangeGenerator()
            : base(SourceDatabaseEnum.CRM3)
        { 
            //Incidents may need to wait until activities are done
            Query = @"select 
                            i.IncidentId,
                            i.StateCode,
                            i.StatusCode
                            from Incident i
                            where i.StateCode <>0 ";
            AllowParallelism = true;
        }
         
        protected override Action<IOrganizationService, CrmContext> MakeRowAction(System.Data.IDataReader reader)
        {
            //we must read these outside of the function delegate because we aren't sure what state the reader will
            //be in when the action below is executed
            Guid id = reader.GetGuid(reader.GetOrdinal("IncidentId"));
            int statecode = reader.GetTypedValue<int>("StateCode");
            int statuscode = reader.GetTypedValue<int>("StatusCode");

            return new Action<IOrganizationService, CrmContext>((service, context) =>
            { 
                var currentState = (from ap in context.IncidentSet
                                    where ap.IncidentId == id
                                    select ap.StateCode).FirstOrDefault();

                if (currentState!=null && currentState.HasValue && (int)currentState.Value != statecode) 
                 {
                    //we need to resolve this case
                     if (statecode == 1)
                     {
                         CloseIncidentRequest request = new CloseIncidentRequest();
                         request.Status = new OptionSetValue(statuscode);

                         IncidentResolution close = new IncidentResolution();
                         // Added manual set of Activity Id to aid in deleting the close
                         close.ActivityId = Guid.NewGuid();
                         close.IncidentId = new EntityReference(Incident.EntityLogicalName, id);
                         close.Subject = "Migration Close";
                         request.IncidentResolution = close;
                         service.Execute(request);

                         // Delete the Close Activity that was just created
                         service.Delete(OpportunityClose.EntityLogicalName, close.ActivityId.Value);
                     }
                     //we need to cancel this request
                     else if (statecode == 2)
                     {
                         SetStateRequest request = new SetStateRequest();
                         request.EntityMoniker = new EntityReference(Incident.EntityLogicalName, id);
                         request.State = new OptionSetValue(2);  //cancelled state
                         request.Status = new OptionSetValue(6); //cancelled status

                         service.Execute(request);
                     }
                 }
            }); 
        }
    }
}
