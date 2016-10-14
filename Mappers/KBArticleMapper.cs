using Osv.Crm.Entities;

namespace CRMDataImport.Mappers
{
    public class KBArticleMapper : MapperBase<KbArticle>
    {
        public KBArticleMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        {
            this.Query = @"select 
                            k.KbArticleId,
                            k.KbArticleTemplateId,
                            'kbarticletemplate' as 'KbArticleTemplateIdLogicalName',
                            k.ArticleXml,
                            k.Title,
                            k.SubjectId,
                            'subject' as 'SubjectIdLogicalName',
                            k.KeyWords,
                            k.Number as 'allgnt_OldArticleNumber',
		                    k.CreatedOn  as 'OverriddenCreatedOn',
                            1033 as 'LanguageCode' 
                            from KbArticle k ";
        }

        public override bool IsImportable(KbArticle entity)
        { 
            return !Project.Dictionaries.KbArticles.ContainsKey(entity.KbArticleId.Value);
        } 
    }
}

