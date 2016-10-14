using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
    public class QueueMapper : MapperBase<Queue>
	{
        public QueueMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
		{
            //Change for issue 72 to limit the Queues imported to those noted in the Mappings spreadsheet
            Notes = @"Queue Import is now limited to the queues specified in the Item 72.";
            this.Query = @"select
                                        q.QueueId,
                                        q.Name,
                                        q.EMailAddress,
                                        --q.PrimaryUserId,
                                        su.DomainName as 'PrimaryUserId',
                                        q.[Description]
                                    from [Queue] q		
                                    inner join SystemUser su
	                                    on q.PrimaryUserId = su.SystemUserId
										where QueueId in (
										'FB49E26F-1E5C-E311-BAD2-000BCDCD8B77',
                                        '09008D5D-1E5C-E311-BAD2-000BCDCD8B77',
                                        '62474F81-0D72-E211-AC2F-000BCDCD8B77',
                                        '1DB0B28E-0D72-E211-AC2F-000BCDCD8B77',
                                        '41338697-0D72-E211-AC2F-000BCDCD8B77',
                                        'CE0D53A2-0D72-E211-AC2F-000BCDCD8B77',
                                        '70CE40AB-0D72-E211-AC2F-000BCDCD8B77',
                                        '94BF67B4-0D72-E211-AC2F-000BCDCD8B77',
                                        '1831298C-1E5C-E311-BAD2-000BCDCD8B77',
                                        '6918B0F5-EEF5-DC11-ADF6-000BCDCD88A8',
                                        '29C49302-EFF5-DC11-ADF6-000BCDCD88A8',
                                        '41D2790F-EFF5-DC11-ADF6-000BCDCD88A8',
                                        '29CA031B-EFF5-DC11-ADF6-000BCDCD88A8',
                                        '21B34A29-EFF5-DC11-ADF6-000BCDCD88A8',
                                        'A0CEB7FB-B44C-DB11-8388-000BCDCD88A8',
                                        '4B2A2809-B54C-DB11-8388-000BCDCD88A8',
                                        '3A62209F-B44C-DB11-8388-000BCDCD88A8',
                                        '07C4A4A8-5FA4-433E-B702-B77A2FCF0555',
                                        'D105FFD6-B44C-DB11-8388-000BCDCD88A8',
                                        '885121BC-6F9C-DE11-84F3-000BCDCD8B77',
                                        '89686EF9-106E-E111-919D-000BCDCD8B77',
                                        'B273C0D8-CF81-DE11-9860-000BCDCD8B77',
                                        '1D6B9CC6-55B1-4B6D-AE52-C88E8FAB212A',
                                        '258DC68B-9C4C-DB11-8388-000BCDCD88A8',
                                        'C4F0C89B-9C4C-DB11-8388-000BCDCD88A8',
                                        'BB5F3EC6-9C4C-DB11-8388-000BCDCD88A8',
                                        'BF33FFE1-9C4C-DB11-8388-000BCDCD88A8',
                                        '6D4A07A4-5BDC-DF11-B149-000BCDCD8B77',
                                        '4F72B74C-856C-4271-813A-F64B399EB5D7')";
		}

        public override bool IsImportable(Queue entity)
		{
            return !Project.Dictionaries.Queues.ContainsKey(entity.QueueId.Value);
		} 
	}
}




