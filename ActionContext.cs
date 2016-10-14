using Microsoft.Xrm.Sdk.Client;
using Osv.Crm.Entities;
using System;

namespace CRMDataImport
{ 
    public class ActionContext : IDisposable
    {
        public OrganizationServiceProxy Service { get; set; }
        public CrmContext CrmContext { get; set; }
        public int UseCount { get; set; }
        
        public ActionContext(OrganizationServiceProxy service, CrmContext context)
        {
            Service = service;
            CrmContext = context;
        }

        public void Dispose()
        {
            CrmContext.Dispose();
            Service.Dispose();
        }
    }
}
