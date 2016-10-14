using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Data;


namespace CRMDataImport.Mappers
{
    public class QuoteCloseMapper : MapperBase<QuoteClose>
	{
        public QuoteCloseMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
		{ 
            if (update)
            {
                this.Query = @"select
                                qc.ActivityId,
                                qc.StateCode,
                                qc.StatusCode
                               from QuoteClose qc
                                where qc.StateCode <> 1";
            }
            else
            {
                this.Query = @"select 
                            qc.QuoteId,
                            'quote' as 'QuoteIdLogicalName',
                            qc.Subject,
                            qc.ActivityId,
                            qc.Revision,
                            qc.StateCode,
                            qc.StatusCode,
                            qc.ActualEnd,
                            qc.[Description]
                        from QuoteClose qc ";
            }
		}

        protected override bool MapField(string name, IDataReader reader, NewEntityModel model)
        {
            // Statecode is needed for the subaction but it cannot be mapped because it is readonly
            if (name == "StateCode")
                return true;

            if (name == "StatusCode")
            {
                Guid activityId = reader.GetTypedValue<Guid>("ActivityId");
                int statuscode = reader.GetTypedValue<int>("StatusCode");
                int statecode = reader.GetTypedValue<int>("StateCode");

                model.Subactions.Add(new Action<CrmContext, IOrganizationService>((context, service) =>
                {
                    try
                    {
                        SetStateRequest request = new SetStateRequest();
                        request.EntityMoniker = new EntityReference(QuoteClose.EntityLogicalName, activityId);
                        request.State = new OptionSetValue(statecode);
                        request.Status = new OptionSetValue(statuscode);
                        service.Execute(request);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Cannot update Closed or Cancelled Activity"))
                        {
                            Log.Warn(string.Format("Unable to set status of quote close for ActivityId: {0} Message: {1}", activityId, ex.Message));
                        }
                        else
                            throw;

                    }
                }));

                return true;
            }

            return base.MapField(name, reader, model);
        }

        public override bool IsUpdateable(QuoteClose entity)
        {
            return DestinationKeyExists(entity.ActivityId.Value,"QuoteClose");
        }

        public override bool IsImportable(QuoteClose entity)
		{
            return (DestinationKeyExists(entity.QuoteId.Id, "Quote") && !DestinationKeyExists(entity.ActivityId.Value,"QuoteClose")); 
		} 
	}
}

