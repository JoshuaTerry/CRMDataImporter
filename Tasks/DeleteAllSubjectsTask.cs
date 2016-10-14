using Microsoft.Xrm.Sdk.Client;
using Osv.Crm.Entities;
using System.Linq;

namespace CRMDataImport.Tasks
{
    public class DeleteAllSubjectsTask
    {
        public DeleteAllSubjectsTask(OrganizationServiceProxy proxy, CrmContext context)
        {           
            _proxy = proxy;
            _context = context;
        }
        
        private OrganizationServiceProxy _proxy;
        private CrmContext _context;

        public class DeleteAllSubjectsTaskResults
        {
            public int SubjectsDeleted;
            public int SubjectsNotDeleted;
        }

        public DeleteAllSubjectsTaskResults PerformTask()
        {
            int deleted = 0;
            int failed = 0;
            int deletedThisLoop = 0;
            do
            {
                deletedThisLoop = 0;
                var list = (from s in _context.SubjectSet 
                            select s);

                foreach (Subject s in list)
                {
                    try
                    {
                        _proxy.Delete(Subject.EntityLogicalName, s.SubjectId.Value);
                        deletedThisLoop++;
                    }
                    catch
                    {
                        failed++;
                    }

                }

                deleted += deletedThisLoop; 
            } while (deletedThisLoop > 0);

            return new DeleteAllSubjectsTaskResults { SubjectsDeleted = deleted, SubjectsNotDeleted = failed };
        }
    }
}
