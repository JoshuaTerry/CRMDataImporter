using log4net;
using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRMDataImport.ActionGenerators
{ 
    public abstract class ImportQueryActionGenerator : IActionGenerator
    { 
        protected ILog Log = log4net.LogManager.GetLogger(typeof(ImportQueryActionGenerator));

        public ImportQueryActionGenerator(SourceDatabaseEnum source)
        {
            SourceDatabase = source;
        }

        public enum SourceDatabaseEnum { CRM3, CRM2011, ACT };
        public SourceDatabaseEnum SourceDatabase { get; set; }

        public Project Project { get { return DataMigration.CurrentProject; } }
        public string Query { get; set; }
        public string Notes { get; set; }
        public bool AllowParallelism { get; protected set; }

        protected virtual Action<IOrganizationService, CrmContext> MakeFirstAction() {return null;}
        protected virtual Action<IOrganizationService, CrmContext> MakeLastAction() { return null; }

        protected abstract Action<IOrganizationService, CrmContext> MakeRowAction(IDataReader reader);

        public virtual IEnumerable<Action<IOrganizationService, CrmContext>> GetActions()
        {
            var actions = new List<Action<IOrganizationService, CrmContext>>();

            var firstAction = MakeFirstAction();
            if (firstAction != null)
                actions.Add(firstAction);

            //what is our source?  we have to do this now, because we might have changed the
            //connection string with the ui since the mapper was newed
            string connString = string.Empty;
            switch (SourceDatabase)
            {
                case SourceDatabaseEnum.CRM3:
                    connString = Project.CRM3ConnectionString;
                    break;
                case SourceDatabaseEnum.CRM2011:
                    connString = Project.CRM2011ConnectionString;
                    break;
                case SourceDatabaseEnum.ACT:
                    connString = Project.ACTConnectionString;
                    break;
            }
           
            SqlCommand command = new SqlCommand(Query, new SqlConnection(connString));

            command.Connection.Open();
            using (IDataReader reader = command.ExecuteReader())
            { 
                while (reader.Read())
                {
                    var action = MakeRowAction(reader);
                    if (action != null)
                    {
                        string rowData =  Common.TryReaderRowToString(reader);
                        actions.Add(new Action<IOrganizationService, CrmContext>((service, context) =>
                        {
                            try
                            {
                                action.Invoke(service, context);
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message != "The specified Active Directory user already exists as a CRM user.")
                                {
                                    Log.Error(string.Format("Error executing row action. Row data: {0}", rowData), ex);
                                    throw ex;
                                }
                            }
                        }));
                    } 
                }
            }

            var lastAction = MakeLastAction();
            if (lastAction != null)
                actions.Add(lastAction);

            return actions;
        } 
    }
}
