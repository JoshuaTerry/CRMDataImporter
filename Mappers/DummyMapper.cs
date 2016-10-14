using System;
using System.Collections.Generic;

namespace CRMDataImport.Mappers
{
    public class DummyMapper : IActionGenerator
    { 
        public IEnumerable<Action<Microsoft.Xrm.Sdk.IOrganizationService, Osv.Crm.Entities.CrmContext>> GetActions()
        {
            return new List<Action<Microsoft.Xrm.Sdk.IOrganizationService, Osv.Crm.Entities.CrmContext>>();
        }

        public bool AllowParallelism
        {
            get { return false; }
        }

        public string Query { get; set; }

        public string Notes { get; set; }
    }
}
