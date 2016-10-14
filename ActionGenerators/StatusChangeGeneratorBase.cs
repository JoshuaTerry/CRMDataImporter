using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;

namespace CRMDataImport.ActionGenerators
{
    public abstract class StatusChangeGeneratorBase : ImportQueryActionGenerator
    { 
        public StatusChangeGeneratorBase(SourceDatabaseEnum source) : base(source) { }
         
        protected void SetEntityState(IOrganizationService service, Guid id, string logicalname, int state, int status)
        {
            SetStateRequest request = new SetStateRequest();

            request.EntityMoniker = new EntityReference(logicalname, id);
            request.State = new OptionSetValue(state);
            request.Status = new OptionSetValue(status);

            try
            {
                SetStateResponse response = service.Execute(request) as SetStateResponse;
                //Log.Info(string.Format("Entity {0} Id: {1} was updated successfully", entity.LogicalName, entity.Id));
            }
            catch (Exception e)
            {
                Log.Error(string.Format("Status change for entity {0} Id: {1} to StatusCode {2} StateCode {3} failed.", logicalname, id, status, state), e);
            }
        }
    }
}
