using Microsoft.Xrm.Sdk.Client;
using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CRMDataImport
{
    /// <summary>
    /// Common dictionaries contains keyed lists of entities that are created as needed.  This eliminates the execution time required when making the request
    /// to CRM for each Action significantly increasing performance.
    /// </summary>
    public class CommonDictionaries
    {
        public CommonDictionaries(Project prj)
        {
            _project = prj;
        }

        private Project _project;
        
        private  List<Tuple<Guid, string, string>> nametitles = null;
        private  object nametitlesSync = new object();
        /// <summary>
        /// Name Titles are now manually created from the specified list
        /// </summary>
        public  List<Tuple<Guid, string, string>> NameTitles
        {
            get
            {
                if (nametitles == null)
                {
                    lock (nametitlesSync)
                    {
                        if (nametitles == null)
                        {
                            nametitles = new List<Tuple<Guid, string, string>>();
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("D14C70B0-A036-E311-B1CF-00155D028117"), "Venerable", "100000000"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("D44CF8B8-A036-E311-B1CF-00155D028117"), "Very Reverend", "100000001"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("0C730BC1-A036-E311-B1CF-00155D028117"), "Very Reverend Father", "100000002"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("0D730BC1-A036-E311-B1CF-00155D028117"), "Very Reverend Monsignor", "100000003"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("9BB150B3-9F36-E311-B1CF-00155D028117"), "Archbishop", "100000010"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("A3FDE9BD-9F36-E311-B1CF-00155D028117"), "Archdeacon", "100000011"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("7A4859C7-9F36-E311-B1CF-00155D028117"), "Archpriest", "100000012"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("FC0862D0-9F36-E311-B1CF-00155D028117"), "Bishop", "100000015"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("F51B5DDB-9F36-E311-B1CF-00155D028117"), "Brother", "100000018"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("6A7139E3-9F36-E311-B1CF-00155D028117"), "Deacon", "100000054"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("AF3E4DED-9F36-E311-B1CF-00155D028117"), "Dr", "100000055"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("CFCDECF8-9F36-E311-B1CF-00155D028117"), "Father", "100000065"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("4B55BC01-A036-E311-B1CF-00155D028117"), "Friar", "100000066"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("4A9CCE0A-A036-E311-B1CF-00155D028117"), "Minister", "100000121"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("36F35014-A036-E311-B1CF-00155D028117"), "Monsignor", "100000122"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("88F1D81C-A036-E311-B1CF-00155D028117"), "Most Reverend", "100000123"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("C496CE25-A036-E311-B1CF-00155D028117"), "Mr", "100000125"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("B4686E2E-A036-E311-B1CF-00155D028117"), "Mrs", "100000132"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("A022F035-A036-E311-B1CF-00155D028117"), "Ms", "100000137"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("CA6F8E3D-A036-E311-B1CF-00155D028117"), "Pastor", "100000144"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("145A2548-A036-E311-B1CF-00155D028117"), "Rector", "100000153"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("ACA4914F-A036-E311-B1CF-00155D028117"), "Rev", "100000154"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("70DBD959-A036-E311-B1CF-00155D028117"), "Reverend Canon", "100000155"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("9C489C66-A036-E311-B1CF-00155D028117"), "Reverend Deacon", "100000156"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("62EEDA70-A036-E311-B1CF-00155D028117"), "Reverend Father", "100000157"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("17803183-A036-E311-B1CF-00155D028117"), "Reverend Monsignor", "100000158"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("7AECE88E-A036-E311-B1CF-00155D028117"), "Right Reverend", "100000159"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("16FE2697-A036-E311-B1CF-00155D028117"), "Sister", "100000165"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("F2FE98A5-A036-E311-B1CF-00155D028117"), "Reverend Mr", "100000166"));
                            nametitles.Add(new Tuple<Guid, string, string>(Guid.Parse("7CDE5A7A-A036-E311-B1CF-00155D028117"), "Reverend Friar", "739280000"));
                        }
                    }
                }
                return nametitles;
            }
        }
        
        private Dictionary<string, Guid> systemusers = null;
        private object systemusersSync = new object();
        /// <summary>
        /// Pulls the SystemUsers from the 2011 CRM instance
        /// </summary>
        public Dictionary<string, Guid> SystemUsers
        {
            get
            {
                if (systemusers == null)
                {
                    lock (systemusersSync)
                    {
                        if (systemusers == null)
                        {
                            _project.ExecuteInContext(delegate(CrmContext context, OrganizationServiceProxy proxy)
                            {
                                systemusers = (from u in context.SystemUserSet
                                               select new { DomainName = string.IsNullOrEmpty(u.DomainName) ? u.LastName.ToLower() : u.DomainName.ToLower(), SystemUserId = u.SystemUserId }).ToDictionary(t => t.DomainName.ToLower(), t => t.SystemUserId.Value);
                            });
                        }
                    }
                }
                return systemusers;
            }
        }
          
        private  object kbarticleSync = new object();
        private  Dictionary<Guid, string> kbarticles = null;
        public  Dictionary<Guid, string> KbArticles
        {
            get
            {
                if (kbarticles == null)
                {
                    lock (kbarticleSync)
                    {
                        _project.ExecuteInContext(delegate(CrmContext context, OrganizationServiceProxy proxy)
                        {
                            kbarticles = (from c in context.KbArticleSet
                                          select new { c.Id, string.Empty }).ToDictionary(t => t.Id, t => string.Empty);
                        });
                    }
                }
                return kbarticles;
            }
        }

        private object kbarticletemplateSync = new object();
        private Dictionary<Guid, string> kbarticletemplates = null;
        public Dictionary<Guid, string> KbArticleTemplates
        {
            get
            {
                if (kbarticletemplates == null)
                {
                    lock (kbarticletemplateSync)
                    {
                        _project.ExecuteInContext(delegate(CrmContext context, OrganizationServiceProxy proxy)
                        {
                            kbarticletemplates = (from c in context.KbArticleTemplateSet
                                          select new { c.Id, string.Empty }).ToDictionary(t => t.Id, t => string.Empty);
                        });
                    }
                }
                return kbarticletemplates;
            }
        }

        private  object promotioncodeSync = new object();
        private  Dictionary<string, int> promotioncodes = null;
        public  Dictionary<string, int> PromotionCodes
        {
            get
            {
                if (promotioncodes == null)
                {
                    lock (promotioncodeSync)
                    {
                        promotioncodes = new Dictionary<string, int>();
                        LoadDictionary<string, int>(promotioncodes, @"select AttributeValue as 'Id', Value as 'Label'
                                                        from StringMap 
                                                        where AttributeName = 'allgnt_PromotionCode'", "Label", "Id", _project.CRM2011ConnectionString);
                    }
                }
                return promotioncodes;
            }
        }

        private object contactaddress1Sync = new object();
        private Dictionary<Guid, string> contactaddress1Ids = null;
        public Dictionary<Guid, string> ContactAddress1Ids
        {
            get
            {
                if (contactaddress1Ids == null)
                {
                    lock (contactaddress1Sync)
                    {
                        contactaddress1Ids = new Dictionary<Guid, string>();
                        LoadDictionary(contactaddress1Ids, @"select 
	                                        ca.CustomerAddressId
                                        from CustomerAddress ca
                                        inner join Contact c
	                                        on c.Address1_AddressId = ca.CustomerAddressId", "CustomerAddressId", _project.CRM3ConnectionString);
                    }
                }
                return contactaddress1Ids;
            }
        }
         
        private  Dictionary<Guid, string> nametitleids = null;
        private  object nametitleidsSync = new object();
        public  Dictionary<Guid, string> NameTitleIds
        {
            get
            {
                if (nametitleids == null)
                {
                    lock (nametitlesSync)
                    {
                        if (nametitleids == null)
                        {
                            nametitleids = new Dictionary<Guid, string>();
                            LoadDictionary(nametitleids, "select allgnt_customernametitleId from allgnt_customernametitle", "allgnt_customernametitleId");
                        }
                    }
                }
                return nametitleids;
            }
        }

        private Dictionary<string, Guid> representativeids = null;
        private  object representativeidsSync = new object();
        public Dictionary<string, Guid> RepresentativeIds
        {
            get
            {
                if (representativeids == null)
                {
                    lock (representativeidsSync)
                    {
                        if (representativeids == null)
                        {
                            _project.ExecuteInContext(delegate(CrmContext context, OrganizationServiceProxy proxy)
                            {
                                representativeids = (from r in context.allgnt_representativeSet
                                                     where r.allgnt_Value != null 
                                                     select new { r.allgnt_Value, r.Id }).ToDictionary(t => t.allgnt_Value, t => t.Id);
                            });
                        }
                    }
                }
                return representativeids;
            }
        }
         
        private  Dictionary<Guid, string> addressids = null;
        private  object addressidsSync = new object();
        public  Dictionary<Guid, string> ImportedAddressIds
        {
            get
            {
                if (addressids == null)
                {
                    lock (addressidsSync)
                    {
                        if (addressids == null)
                        {
                            addressids = new Dictionary<Guid, string>();
                            LoadDictionary(addressids, "select CustomerAddressId from CustomerAddress where ObjectTypeCode = 2", "CustomerAddressId");
                        }
                    }
                }

                return addressids;
            }
        }

        private Dictionary<Guid, string> queues = null;
        private object queueSync = new object();
        public Dictionary<Guid, string> Queues
        {
            get
            {
                if (queues == null)
                {
                    lock (queueSync)
                    {
                        if (queues == null)
                        {
                            queues = new Dictionary<Guid, string>();
                            LoadDictionary(queues, "select QueueId from Queue", "QueueId");
                        }
                    }
                }

                return queues;
            }
        }



        private Dictionary<Guid, string> queueitems = null;
        private object queueitemSync = new object();
        public Dictionary<Guid, string> QueueItems
        {
            get
            {
                if (queueitems == null)
                {
                    lock (queueitemSync)
                    {
                        if (queueitems == null)
                        {
                            queueitems = new Dictionary<Guid, string>();
                            LoadDictionary(queueitems, "select QueueItemId from QueueItem", "QueueItemId");
                        }
                    }
                }

                return queueitems;
            }
        }

        private  object osproductsSync = new object();
        private  Dictionary<string, Guid> osproducts = null;
        public  Dictionary<string, Guid> OSProducts
        {
            get
            {
                if (osproducts == null)
                {
                    lock (osproductsSync)
                    {
                        _project.ExecuteInContext(delegate(CrmContext context, OrganizationServiceProxy proxy)
                        {
                            osproducts = (from p in context.allgnt_offertorysolutionsproductSet
                                          select new { p.allgnt_name, p.Id }).ToDictionary(t => t.allgnt_name, t => t.Id);
                        });
                    }
                }
                return osproducts;
            }
        }

        private Dictionary<string, Guid> buitems = null;
        private object buitemSync = new object();
        public Dictionary<string, Guid> BusinessUnits
        {
            get
            {
                if (buitems == null)
                {
                    lock (buitemSync)
                    {
                        if (buitems == null)
                        {
                            buitems = new Dictionary<string, Guid>();
                            LoadDictionary(buitems, "select u.Name, u.BusinessUnitId from  BusinessUnit u","Name","BusinessUnitId",_project.CRM2011ConnectionString);
                        }
                    }
                }

                return buitems;
            }
        }
         
        private object idkeyDictionarySync = new object();
        private Dictionary<Guid, string> idKeys = new Dictionary<Guid, string>();
        private Dictionary<string, string> loadedEntities = new Dictionary<string, string>();
        /// <summary>
        /// Checks to see if the key has previously been imported.
        /// This can replace many of the previous dictionaries once they are dereferenced.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool HasKeyBeenImported(Guid id, params string[] entities)
        {
            //todo ideas: change the value type to int for idKeys and use the
            //static mapping between entity name and id to verify that these things are valid instead of strings

            lock (idkeyDictionarySync)
            {
                //do we have every single one of the requested entities loaded?
                if (entities.All(e => loadedEntities.ContainsKey(e)))
                {
                    if (idKeys.ContainsKey(id))
                    {
                        if (!entities.Contains(idKeys[id]))
                            throw new InvalidOperationException("The matched id key was not of the type(s) you asked for.");
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    foreach (string entity in entities)
                    {
                        if (!loadedEntities.ContainsKey(entity))
                        {
                            string idColumn = string.Concat(entity, "Id");
                            string fromTable = entity;
                            switch (entity)
                            {
                                //special id field overrides
                                case ("Email"):
                                case ("PhoneCall"):
                                case ("Task"):
                                case ("Letter"):
                                case ("Fax"):
                                case ("BulkOperation"):
                                case ("OpportunityClose"):
                                case ("QuoteClose"):
                                case ("Appointment"): 
                                case ("IncidentResolution"):
                                    idColumn = "ActivityId";
                                    break;
                                case ("Activity"):
                                    idColumn = "ActivityId";
                                    fromTable = "ActivityPointer";
                                    break;
                            }
                            LoadDictionary(idKeys, string.Format("select {0} as Id, '{2}' as EntityName from {1}", idColumn, fromTable, entity), "Id", "EntityName", _project.CRM2011ConnectionString);
                            loadedEntities.Add(entity, string.Empty);
                        }
                    }

                    if (idKeys.ContainsKey(id))
                    {
                        if (!entities.Contains(idKeys[id]))
                            throw new InvalidOperationException("The matched id key was not of the type(s) you asked for.");
                        return true;
                    }
                    else
                        return false;
                }
            }
        }
          
        /// <summary>
        /// Loads a dictionary list from the query specified
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="query"></param>
        /// <param name="idfield"></param>
        private  void LoadDictionary(Dictionary<Guid, string> ht, string query, string idfield, string connectionString)
        {
            SqlCommand command = new SqlCommand(query, new SqlConnection(connectionString));
            command.Connection.Open();

            using (IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                do
                {
                    while (reader.Read())
                        ht.Add(reader.GetTypedValue<Guid>(idfield), string.Empty);
                } while (reader.NextResult());
            }
        }

        /// <summary>
        /// Loads a dictionary with a specific type of key and value
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <param name="ht"></param>
        /// <param name="query"></param>
        /// <param name="idfield"></param>
        /// <param name="valuefield"></param>
        /// <param name="connectionString"></param>
        private  void LoadDictionary<X, Y>(Dictionary<X, Y> ht, string query, string idfield, string valuefield, string connectionString)
        {
            SqlCommand command = new SqlCommand(query, new SqlConnection(connectionString));
            command.Connection.Open();

            using (IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                    ht.Add(reader.GetTypedValue<X>(idfield), reader.GetTypedValue<Y>(valuefield));
            }
        }

        private  void LoadDictionary(Dictionary<Guid, string> ht, string query, string idfield)
        {
            LoadDictionary(ht, query, idfield, _project.CRM2011ConnectionString);
        }
    }
}
