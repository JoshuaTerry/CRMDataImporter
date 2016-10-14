using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
    public class NotesMapper : MapperBase<Annotation>
    {
        public NotesMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        {
            this.Query = @"select 
                                an.AnnotationId, 
                                su.DomainName as 'OwnerId',
                                an.ObjectId ,
                                an.ObjectTypeCode as 'ObjectIdMapperTypeCode',
                                an.[Subject],
	                            an.DocumentBody,
	                            --an.FileSize,
	                            an.FileName,
                                an.MimeType,
                                an.NoteText,
                                an.CreatedOn as 'OveriddenCreatedOn'
                                from Annotation an 
                                inner join SystemUser su 
                                on an.OwnerId = su.SystemUserId "; 
        }

        public override bool IsImportable(Annotation entity)
        { 
            return !DestinationKeyExists(entity.AnnotationId.Value,"Annotation") && DestinationKeyExists(entity.ObjectId.Id, "Account", "Contact", "Opportunity", "Incident", "Quote", "Task", "Appointment", "Letter", "Fax", "Competitor", "PhoneCall" ); 
        }
    }
}
