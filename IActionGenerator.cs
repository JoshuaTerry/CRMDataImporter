using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Collections.Generic;

namespace CRMDataImport
{
    public interface IActionGenerator
    {
        IEnumerable<Action<IOrganizationService, CrmContext>> GetActions();
        string Query { get; set; }
        string Notes { get; set; }
        bool AllowParallelism { get; }
    }
}
