using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;

namespace CRMDataImport.Mappers
{
    public class SubjectMapper: MapperBase<Subject>
    {
        public SubjectMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        {
            this.AllowParallelism = false;
            Notes = @"BEFORE IMPORT
Ensure that we have the most updated spreadsheet of Subject additions.";

            this.Query = @"
                                    WITH subjectcte AS
                                    (
                                          SELECT      1 AS Level,
                                                      s1.SubjectId,
                                                      s1.ParentSubject,
                                                      'subject' as 'ParentSubjectLogicalName',
                                                      s1.Title,
                                                      s1.Description,
                                                      1 as FeatureMask
                                          FROM  [Subject] s1
                                          WHERE s1.ParentSubject IS NULL
                                            and SubjectId <> '6AAED185-8567-4E3D-BF45-00A559280332' 

                                          UNION ALL
      
                                          SELECT      scte.Level + 1 AS Level,
                                                      s2.SubjectId,
                                                      s2.ParentSubject,
                                                      'subject' as 'ParentSubjectLogicalName',
                                                      s2.Title,
                                                      s2.Description,
                                                      1 as FeatureMask
                                          FROM  Subject s2
                                          JOIN  subjectcte scte
                                                ON    s2.ParentSubject = scte.subjectid
                                    )

                                    SELECT      SubjectId,
                                                ParentSubject,
                                                'subject' as 'ParentSubjectLogicalName',
                                                Title,
                                                Description,
                                                1 as FeatureMask
                                    FROM  subjectcte";
        }

        public override bool IsImportable(Subject entity)
        {
            return !DestinationKeyExists(entity.SubjectId.Value,"Subject");
        }

        protected override Action<IOrganizationService, CrmContext> MakeLastAction()
        { 
            return new Action<IOrganizationService, CrmContext>((service, context) =>
            {
                var task = new Tasks.AddSubjectHierarchyTask(service, context);
                task.PerformTask();
            });
        }
    }
}


   