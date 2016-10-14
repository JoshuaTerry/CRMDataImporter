using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Linq;

namespace CRMDataImport.ActionGenerators
{
    public class ActivityStateChangeGenerator : ImportQueryActionGenerator
    {
        public ActivityStateChangeGenerator()
            : base(SourceDatabaseEnum.CRM3)
        {
            
            AllowParallelism = true;
            Query = @"select
                        ap.ActivityId,
                        ap.StateCode,
                        ap.StatusCode,
                        ap.RegardingObjectId,
                        ap.RegardingObjectTypeCode,
                        ap.ActivityTypeCode,
                        case
			                when ActivityTypeCode = 4208 then 'opportunityclose'
			                when ActivityTypeCode = 4206 then 'incidentresolution'
                            when ActivityTypeCode = 4211 then 'quoteclose'
                            when ActivityTypeCode = 4202 then 'email'
                            when ActivityTypeCode = 4210 then 'phonecall'
                            when ActivityTypeCode = 4204 then 'fax'
                            when ActivityTypeCode = 4207 then 'letter'
                            when ActivityTypeCode = 4201 then 'appointment'
                            when ActivityTypeCode = 4212 then 'task'

		                end as 'EntityLogicalName'
                    from ActivityPointer ap
                    where   (ActivityTypeCode = 4208 and ap.StateCode <> 1) or 
                            (ActivityTypeCode = 4206 and ap.StateCode <> 1) or
                            (ActivityTypeCode = 4211 and ap.StateCode <> 1) or
                            (ActivityTypeCode = 4202 and ap.StateCode <> 0) or
                            (ActivityTypeCode = 4210 and ap.StateCode <> 0) or 
                            (ActivityTypeCode = 4204 and ap.StateCode <> 0) or 
                            (ActivityTypeCode = 4207 and ap.StateCode <> 0) or 
                            (ActivityTypeCode = 4201 and ap.StateCode <> 0) or 
                            (ActivityTypeCode = 4212 and ap.StateCode <> 0) 
";  
        }

        protected override Action<IOrganizationService, CrmContext> MakeRowAction(System.Data.IDataReader reader)
        {
            Guid activityId = reader.GetTypedValue<Guid>("ActivityId");
            int act = reader.GetTypedValue<int>("ActivityTypeCode");
            int statuscode = reader.GetTypedValue<int>("StatusCode");
            int statecode = reader.GetTypedValue<int>("StateCode");
            string logicalname = reader.GetTypedValue<string>("EntityLogicalName");

            if (logicalname != string.Empty)
            {
                return new Action<IOrganizationService, CrmContext>((service, context) =>
                 { 
                     var currentState = (from ap in context.ActivityPointerSet
                              where ap.ActivityId == activityId
                              select ap.StateCode).FirstOrDefault(); 

                     if (currentState!=null && currentState.HasValue && (int)currentState.Value != statecode)
                     {
                         SetStateRequest request = new SetStateRequest();
                         request.EntityMoniker = new EntityReference(logicalname, activityId);
                         request.State = new OptionSetValue(statecode);
                         request.Status = new OptionSetValue(statuscode);

                         service.Execute(request);
                     }
                 });
            } 
            return null;
        }  
    }
}
