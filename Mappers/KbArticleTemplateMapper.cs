using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
    public class KbArticleTemplateMapper : MapperBase<KbArticleTemplate>
    {
        public KbArticleTemplateMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        {
            this.Notes = @"BEFORE IMPORT  
Delete the existing KBArticleTemplates in your destination site. 

AFTER IMPORT  
Open each KBArticleTemplate in CRM 2011.  Edit a section adding a letter to the title and save.  
Edit the section again and remove the letter and save.  Do this for each template.";

            this.Query = @"select 
                                    k.KbArticleTemplateId,
                                    k.StructureXml,
                                    k.FormatXml,
                                    k.Title,
                                    k.[Description],
                                    k.IsActive,
                                    1033 as 'LanguageCode'
                                    from KbArticleTemplate k";
        }

        public override bool IsImportable(KbArticleTemplate entity)
        {
            return !Project.Dictionaries.KbArticleTemplates.ContainsKey(entity.KbArticleTemplateId.Value);
        } 
    }
}

