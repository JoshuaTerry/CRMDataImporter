
using Osv.Crm.Entities;
using System;

namespace CRMDataImport.Mappers
{
    public class TaskMapper : MapperBase<Task>
    {
        public TaskMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update)
        {
            this.Query = @" select  
                                Task.ActivityId as 'ActivityId', 
                                Task.[Subject] as 'Subject',
                                Task.[Description] as 'Description',
                                Task.RegardingObjectId as 'RegardingObjectId',
                                Task.RegardingObjectTypeCode as 'RegardingObjectIdMapperTypeCode',  
                                su.DomainName as 'OwnerId', 
                                Task.ActualDurationMinutes,
                                Task.ScheduledEnd as 'ScheduledEnd',  
                                Task.CreatedOn as 'OverriddenCreatedOn',
                                Task.PriorityCode,
                                Task.ActualStart,
                                Task.ScheduledStart
                                from Task Task 
                                inner join SystemUser su
	                                on su.SystemUserId = Task.OwnerId";
        }
          
        public override bool IsImportable(Task entity)
        {
            return !DestinationKeyExists(entity.ActivityId.Value, "Task")&& (entity.RegardingObjectId == null || 
                DestinationKeyExists(entity.RegardingObjectId.Id,"Account","Contact","Opportunity","Incident", "Quote"));
        }
    }
}
 