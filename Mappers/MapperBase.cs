using log4net;
using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CRMDataImport.Mappers
{
    public class MapperBase<T> : IActionGenerator where T : Entity, new()
    {
        protected class NewEntityModel
        {
            public T Entity = new T();
            public List<Action<CrmContext, IOrganizationService>> Subactions = new List<Action<CrmContext, IOrganizationService>>();
        }

        protected ILog Log = log4net.LogManager.GetLogger("MapperBase");

        public MapperBase(SourceDatabaseEnum source, bool update)
        { 
            SourceDatabase = source;
            IsUpdate = update;
            AllowParallelism = true;
        }

        public enum SourceDatabaseEnum { CRM3, ACT };
        public SourceDatabaseEnum SourceDatabase { get; set; } 

        //todo: possibly remove this property since the actions generated are exectuted in a project context by the consumer not this class
        public Project Project { get { return DataMigration.CurrentProject; } }
        public string Query { get; set; }
        public string Notes { get; set; }
        public bool IsUpdate { get; protected set; }
        /// <summary>
        /// Determines if the Actions can be threaded
        /// </summary>
        public bool AllowParallelism { get; protected set; }
         
        public bool DestinationKeyExists(Guid key, params string[] entities)
        {
            return Project.Dictionaries.HasKeyBeenImported(key, entities);
        }

        public virtual bool IsUpdateable(T entity) { return false; }
        public virtual bool IsImportable(T entity) { return false; }

        /// <summary>
        /// override to manually map by field name, return true to disable automatic field mapping
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool MapField(string name, IDataReader reader, NewEntityModel model) { return false; }

        /// <summary>
        /// override to manually map the entire row, return true to disable the field level mapping (including the MapField method)
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual bool MapRow(IDataReader reader, NewEntityModel model) { return false; }

        protected virtual Action<IOrganizationService, CrmContext> MakeLastAction() { return null; }

        protected virtual Action<IOrganizationService, CrmContext> MakeFirstAction() { return null; }

        /// <summary>
        /// Returns a list of Actions created by the mapper
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Action<IOrganizationService, CrmContext>> GetActions()
        {
            var actions = new List<Action<IOrganizationService, CrmContext>>();

            var firstAction = MakeFirstAction();
            if (firstAction != null)
            {
                actions.Add(new Action<IOrganizationService, CrmContext>((service, context) =>
                {
                    try
                    {
                        firstAction.Invoke(service, context);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Exception in MapperBase generated first action.", ex);
                        throw;
                    }
                }
                    ));
            }

            //what is our source?  we have to do this now, because we might have changed the
            //connection string with the ui since the mapper was newed
            string connString = SourceDatabase == SourceDatabaseEnum.CRM3 ? Project.CRM3ConnectionString : Project.ACTConnectionString;

            SqlCommand command = new SqlCommand(Query, new SqlConnection(connString));

            command.Connection.Open();
            command.CommandTimeout = 60 * 60; //60mins
            using (IDataReader reader = command.ExecuteReader())
            {
                foreach (NewEntityModel model in MapReaderToEntities(reader))
                {
                    if (IsUpdate ? IsUpdateable(model.Entity) : IsImportable(model.Entity))
                    {
                        // Convert the values from the row of the reader to a string to be included in case of Exception
                        string readervalues = Common.TryReaderRowToString(reader);
                        actions.Add(new Action<IOrganizationService, CrmContext>((service, context) =>
                        {
                            try
                            {
                                if (IsUpdate)
                                {
                                    if (!context.IsAttached(model.Entity))
                                        context.Attach(model.Entity);

                                    context.UpdateObject(model.Entity);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    service.Create(model.Entity);
                                }

                                //execute all the subactions for this entity
                                foreach (var x in model.Subactions)
                                {
                                    x.Invoke(context, service);
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error(string.Format("Exception in MapperBase generated action. Reader Values {0}", readervalues), ex);
                                //I'd like a way to attach the reader values to the thrown exception
                                throw;
                            }
                        }));
                    }
                }
            }

            var lastAction = MakeLastAction();
            if (lastAction != null)
            {
                actions.Add(new Action<IOrganizationService, CrmContext>((service, context) =>
                    {
                        try
                        {
                            lastAction.Invoke(service, context);
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Exception in MapperBase generated last action.", ex);
                            throw;
                        }
                    }
                    ));
            }

            return actions; 
        }

        /// <summary>
        /// Map fields in the reader to fields on the Entity 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private IEnumerable<NewEntityModel> MapReaderToEntities(IDataReader reader)
        {
            while (reader.Read())
            {
                NewEntityModel m = new NewEntityModel();

                //see if the override wants to map this entire row
                if (!MapRow(reader, m))
                {
                    for (int x = 0; x < reader.FieldCount; x++)
                    {
                        // Get the name of the field
                        string name = reader.GetName(x);
                       
                        // Check for an override for this field in the implementing mapper
                        if (!MapField(name, reader, m))
                        {
                            // Get the property on the entity with the name from the reader
                            var property = typeof(T).GetProperty(name);

                            if (property != null)
                            {
                                var type = property.PropertyType;
                                // GetTypedValue is an extension method in Common that maps value in the reader based on the type
                                // of the specified field on the entity
                                MethodInfo method = typeof(Common).GetMethod("GetTypedValue").MakeGenericMethod(type);
                                var value = method.Invoke(null, new object[] { reader, name });
                                                               
                                if (value != null)
                                    typeof(T).GetProperty(name).SetValue(m.Entity, value, null);                              
                            }
                        }
                       
                    }
                }
                yield return m;
            }
        }
    }
}
