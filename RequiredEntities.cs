using Microsoft.Xrm.Sdk.Client;
using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRMDataImport
{
    public class RequiredEntities
    { 
        /// <summary>
        /// Verify that the required OS Products exist and create if not
        /// </summary>
        public static void VerifyOSProducts()
        {
            var Log = log4net.LogManager.GetLogger(typeof(RequiredEntities));

            DataMigration.CurrentProject.ExecuteInContext(
                delegate(CrmContext context, OrganizationServiceProxy proxy)
                {
                    var osProducts = new string[] { "IOP", "OLG", "Websites" };
                    try
                    {
                        foreach (string p in osProducts)
                        {
                            var item = (from ent in context.allgnt_offertorysolutionsproductSet
                                        where ent.allgnt_name == p
                                        select ent).FirstOrDefault();

                            if (item == null)
                            {
                                item = new allgnt_offertorysolutionsproduct { allgnt_name = p };
                                Guid id = proxy.Create(item);
                                Log.Info(string.Format("OS Product: {0}, ID: {1} created.", p, id));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                        throw ex;
                    }
                });
        }

        /// <summary>
        /// Verify that Name Titles exist and create if not
        /// </summary>
        public static void VerifyNameTitles()
        {
            var Log = log4net.LogManager.GetLogger(typeof(RequiredEntities));

            DataMigration.CurrentProject.ExecuteInContext(
                delegate(CrmContext context, OrganizationServiceProxy proxy)
                { 
                    try
                    {
                        foreach (var t in DataMigration.CurrentProject.Dictionaries.NameTitles)
                        {
                            if (!DataMigration.CurrentProject.Dictionaries.NameTitleIds.ContainsKey(t.Item1))
                            {
                                Log.Info(string.Format("Name Title: {0}, ID: {1} did not exist.", t.Item2, t.Item1));                                 
                                Guid id = proxy.Create(new allgnt_customernametitle { allgnt_customernametitleId = t.Item1, allgnt_name = t.Item2, allgnt_Value = t.Item3 });
                                DataMigration.CurrentProject.Dictionaries.NameTitleIds.Add(id, string.Empty);
                                Log.Info(string.Format("Name Title: {0}, ID: {1} created.", t.Item2, t.Item1));
                            } 
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                        throw ex;
                    }
                });
        }
          
         

        public static void VerifyOpportunityTypes()
        {
            var Log = log4net.LogManager.GetLogger(typeof(RequiredEntities));

            //DataMigration.CurrentProject.ExecuteInContext(
            //    delegate(CrmContext context, OrganizationServiceProxy proxy)
            //    {
            //        try
            //        {
            //            foreach (var t in DataMigration.CurrentProject.Dictionaries.OpportunityTypes)
            //            {
            //                if (!DataMigration.CurrentProject.Dictionaries.OpportunityTypeIds.ContainsKey(t.Item1))
            //                {
            //                    Log.Info(string.Format("OpportunityType: {0}, ID: {1} did not exist.", t.Item2, t.Item1));
            //                    Guid id = proxy.Create(new allgnt_opportunity_type { allgnt_opportunity_typeId = t.Item1, allgnt_name = t.Item2 });
            //                    DataMigration.CurrentProject.Dictionaries.OpportunityTypeIds.Add(id, string.Empty);
            //                    Log.Info(string.Format("OpportunityType: {0}, ID: {1} created.", t.Item2, t.Item1));
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Log.Error(ex);
            //            throw ex;
            //        }
            //    });
        }

        /// <summary>
        /// Verify that Systems Settings exist and create if not
        /// </summary>
        public static void VerifySystemSettings()
        {
            var Log = log4net.LogManager.GetLogger(typeof(RequiredEntities));

            var settings = new List<Tuple<string, string>>();
            settings.Add(new Tuple<string, string>("AddressApprovedRoles", "System Administrator"));
            settings.Add(new Tuple<string, string>("validusnames", "US;U.S.;USA;U.S.A.;United States;United States of America"));

            DataMigration.CurrentProject.ExecuteInContext(
                delegate(CrmContext context, OrganizationServiceProxy proxy)
                {
                    try
                    {
                        foreach (var entity in settings)
                        {
                            var item = (from ent in context.allgnt_systemsettingSet
                                        where ent.allgnt_name == entity.Item1
                                        select ent).FirstOrDefault();

                            if (item == null)
                            {
                                Log.Info(string.Format("Setting: {0} did not exist.", entity.Item1));
                                Guid id = proxy.Create(new allgnt_systemsetting { allgnt_name = entity.Item1, allgnt_Value = entity.Item2 });
                                Log.Info(string.Format("Setting: {0} created.", entity.Item1));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                        throw ex;
                    }
                });
        }
    }
}
