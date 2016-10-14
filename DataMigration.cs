using CRMDataImport.ActionGenerators;
using CRMDataImport.Mappers;
using CRMDataImport.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CRMDataImport
{
    public partial class DataMigration : Form
    {
        private log4net.ILog Log = null;
        public static Project CurrentProject = new Project();
        List<RecentProject> recentprojects = new List<RecentProject>();
        Dictionary<string, IActionGenerator> mappers = new Dictionary<string, IActionGenerator>();
        private bool stop = false;

        /// <summary>
        /// Load Recent Projects & Setup Logging
        /// </summary>
        public DataMigration()
        {
            InitializeComponent();
            LoadRecentProjects();
            log4net.Config.XmlConfigurator.Configure();
            Log = log4net.LogManager.GetLogger(typeof(DataMigration));
        } 

        /// <summary>
        /// Load Recent Projects List
        /// </summary>
        private void LoadRecentProjects()
        {
            string recentProjectsPath = Application.StartupPath + "\\recent.xml";

            if (File.Exists(recentProjectsPath))
            {
                FileStream fs = new FileStream(recentProjectsPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using (XmlReader reader = new XmlTextReader(fs))
                {
                    recentprojects = (from r in XDocument.Load(reader).Descendants("RecentProject")
                                      select new RecentProject { FilePath = r.TryGetElementValue("FilePath") }).ToList();
                }

                foreach (var i in recentprojects)
                {
                    string[] path = i.FilePath.Split('\\');
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(path[path.Length - 1], null, delegate(object sender, EventArgs e) { OpenProject(i.FilePath); });
                    fileToolStripMenuItem.DropDownItems.Add(tsmi);
                }
            } 
        }

        /// <summary>
        /// Show Database Connection Info Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setDatabaseConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseConnectionInfo dbinfo = new DatabaseConnectionInfo();
            dbinfo.ShowDialog();
        }

        /// <summary>
        /// Open and Load a Project file from XML
        /// </summary>
        /// <param name="filepath"></param>
        private void OpenProject(string filepath)
        {
            using (FileStream stream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (XmlReader xmlRdr = new XmlTextReader(stream))
            {
                var element = (from proj in XDocument.Load(xmlRdr).Descendants("Project")
                               select proj).FirstOrDefault();

                if (element != null)
                {
                    CurrentProject = new Project { FilePath = OpenProjectFileDialog.FileName };
                    CurrentProject.UpdateCRMSettings(element.TryGetElementValue("DiscoveryAddress"),
                                                        element.TryGetElementValue("OrganizationName"),
                                                        element.TryGetElementValue("Username"),
                                                        element.TryGetElementValue("Password"),
                                                        element.TryGetElementValue("Domain"));

                    CurrentProject.UpdateConnectionStrings(element.TryGetElementValue("CRM3ConnectionString"),
                                                            element.TryGetElementValue("CRM2011ConnectionString"),
                                                            element.TryGetElementValue("ACTConnectionString"));
                }
            }

            if (CurrentProject.OrganizationName != null)
                OrgName.Text = CurrentProject.OrganizationName;

            Tabs.Enabled = IsProjectValid;  
             
            if (recentprojects.FirstOrDefault(p => p.FilePath == filepath) == null)
            {
                if (recentprojects.Count > 4)
                    recentprojects.RemoveAt(0);

                recentprojects.Add(new RecentProject { FilePath = filepath });
                SaveRecentProjects();
                LoadRecentProjects();                
            }

            LoadMappers();
        }
         
        /// <summary>
        /// Update list of recent projects
        /// </summary>
        private void SaveRecentProjects()
        {
            string recentProjectsPath = Application.StartupPath + "\\recent.xml";
            StreamWriter file = new StreamWriter(recentProjectsPath);
            try
            {
                XmlSerializer writer = new XmlSerializer(recentprojects.GetType());
                writer.Serialize(file, recentprojects);
            }
            catch (Exception ex) { string message = ex.Message; }
            finally
            {
                file.Close();
            }
        }

        /// <summary>
        /// Open file dialog for selecting new project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = OpenProjectFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                OpenProject(OpenProjectFileDialog.FileName);
            }
        }

        /// <summary>
        /// Save project settings to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = SaveProjectFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(SaveProjectFileDialog.FileName);
                try
                { 
                    DataContractSerializer ser = new DataContractSerializer(CurrentProject.GetType());
                    ser.WriteObject(file.BaseStream, CurrentProject);
                }
                catch { throw; }
                finally
                {
                    file.Close();
                }
            } 
        }

        /// <summary>
        /// Close program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Display CRM Connection Info Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setCRMConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CRMInstanceConnectionInfo crmconn = new CRMInstanceConnectionInfo();
            crmconn.ShowDialog();

            if (!string.IsNullOrEmpty(DataMigration.CurrentProject.OrganizationName))
                OrgName.Text = DataMigration.CurrentProject.OrganizationName;

            // Ensure an org is specified before continuing.
            Tabs.Enabled = IsProjectValid; 
        }

        /// <summary>
        /// Verify that prerequisite entities required for import exist and create them if not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerifyRequiredEntities_Click(object sender, EventArgs e)
        {
            AppendStatusLine("Verifying Required Entities...");

            try
            {
                AppendStatusLine("Verifying OS Products.");
                RequiredEntities.VerifyOSProducts();

                AppendStatusLine("Verifying System Settings.");
                RequiredEntities.VerifySystemSettings();

                AppendStatusLine("Verifying Name Titles.");
                RequiredEntities.VerifyNameTitles();
                 
                AppendStatusLine("Verifying OpportunityTypes.");
                RequiredEntities.VerifyOpportunityTypes();

                AppendStatusLine("Complete.");
            }
            catch (Exception ex)
            {
                Status.Text += "There was a problem verifying the entities.  Please consult the log" + Environment.NewLine;
                Log.Error(ex);
            } 
        }
         
        /// <summary>
        /// Each Mapper represents an Import Task.  Mappers can be Updates or Inserts.
        /// </summary>
        private void LoadMappers()
        {
            mappers.Clear();
            mappers.Add("Fix Entity Owner", new OwnerAssignmentGenerator());
            mappers.Add("Phase 1 - Pre Advantage Load", new DummyMapper());
            mappers.Add("User Import", new UserImportActionGenerator());
            mappers.Add("User Update Details", new UserUpdateActionGenerator());
            mappers.Add("UserRole Import", new UserRoleAddGenerator());
            mappers.Add("UoM Schedule", new UoMScheduleMapper(false));
            mappers.Add("UoM", new UoMMapper(false));
            mappers.Add("Subject", new SubjectMapper(false)); 
            mappers.Add("KbArticle Template", new KbArticleTemplateMapper(false));
            mappers.Add("KbArticle", new KBArticleMapper(false));
            mappers.Add("Competitors", new CompetitorMapper(false));
            mappers.Add("DiscountType", new DiscountTypeMapper(false));
            mappers.Add("Discount", new DiscountMapper(false));
            mappers.Add("Product", new ProductMapper(false));
            mappers.Add("PriceList", new PriceListMapper(false));
            mappers.Add("Price List Product", new PriceListProductMapper(false));
            mappers.Add("Agent AreaCode", new AgentAreaCodeMapper(false));
            mappers.Add("------------", new DummyMapper());
            mappers.Add("Contact", new ContactMapper(false)); 
            mappers.Add("Account", new AccountMapper(true));
            mappers.Add("Address", new AddressMapper(false)); 
            mappers.Add("Opportunity", new OpportunityMapper(false));
            mappers.Add("Opportunity Update", new OpportunityMapper(true));
            mappers.Add("Opportunity Product", new OpportunityProductMapper(false));
            mappers.Add("Opportunity Competitors", new OpportunityCompetitorMapper(false)); 
            mappers.Add("Opportunity Close", new OpportunityCloseMapper(false));
            mappers.Add("Quote", new QuoteMapper(false));
            mappers.Add("Quote Details", new QuoteDetailMapper(false));
            mappers.Add("Quote Close", new QuoteCloseMapper(false));
            mappers.Add("Incidents", new IncidentMapper(false));
            mappers.Add("Incidents Update", new IncidentMapper(true));
            mappers.Add("Incident Resolution", new IncidentResolutionMapper(false));
            mappers.Add("Email", new EmailMapper(false)); 
            mappers.Add("Phone Call", new PhoneCallMapper(false));
            mappers.Add("Fax", new FaxMapper(false));
            mappers.Add("Letter", new LetterMapper(false));
            mappers.Add("Appointment", new AppointmentMapper(false)); 
            mappers.Add("Task", new TaskMapper(false));
            mappers.Add("Notes", new NotesMapper(false));
            mappers.Add("Queues", new QueueMapper(false));
            mappers.Add("QueuesItems", new QueueItemMapper(false));
            mappers.Add("Contact Status", new ContactStatusChangeGenerator());
            mappers.Add("Account Status", new AccountStatusChangeGenerator());
            mappers.Add("Quote Status Update", new QuoteStatusChangeActionGenerator());
            mappers.Add("Opportunity Status", new OpportunityStatusChangeGenerator());
            mappers.Add("Activity Status Update", new ActivityStateChangeGenerator());
            mappers.Add("Incident Status", new IncidentStatusChangeGenerator());  
              
            EntitiesToImport.Items.Clear(); 

            foreach (var mapper in mappers)
            {
                EntitiesToImport.Items.Add(mapper.Key); 
            } 
        }
         
        /// <summary>
        /// Import Entities based on the selected Mapper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportEntities_Click(object sender, EventArgs e)
        {
            CurrentProject.ResetDictionaries();
            stop = false;
            ImportEntities.Enabled = false; 
            string value = Convert.ToString(EntitiesToImport.SelectedItem);
            string query = string.Empty;
            IEnumerable<Action<IOrganizationService, CrmContext>> actions = null;
            
            // Clear previous status and count
            Status.Text = string.Empty;
            lblRowCount.Text = "0";
            Status.Refresh();

            // Display Import Start Time
            AppendStatusLine(string.Format("Import started: {0}", DateTime.Now));
            BackgroundWorker thread = new BackgroundWorker();

            thread.DoWork += delegate(object caller, DoWorkEventArgs args)
            {
                try
                {
                    if (mappers.ContainsKey(value))
                    {
                        AppendStatusLine("Getting actions to perform and preparing data...");
                        var mapper = mappers[value];
                        
                        // Check to see if the default or overridden query should be used
                        if (OverrideQuery.Checked)
                            mapper.Query = Query.Text;

                        actions = mappers[value].GetActions();

                        if (actions.FirstOrDefault() != null)
                        {
                            AppendStatusLine(string.Format("Total Actions to perform: {0}", actions.Count()));
                            AppendStatusLine("Executing actions...");

                            // Get the number of threads to create
                            int threads = Convert.ToInt32(NumberOfThreads.Text);
                            // Execute the actions in a threaded context
                            ExecuteActionsParallelOnContextPerThread(actions, mapper.AllowParallelism ? threads : 1);
                        }
                        else
                        {
                            AppendStatusLine("No actions to perform");
                        }
                    }
                    else
                    {
                        AppendStatusLine("No mapper found.");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        ex = ex.InnerException;

                    Log.Error("Exception:"+ex.Message, ex);
                    AppendStatusLine(ex.Message);
                }
            };

            //if the thread runs to completion then show completed or aborted
            thread.RunWorkerCompleted += (t_sender, t_e) =>
            {
                if (stop)
                    AppendStatusLine("Aborted");
                else
                    AppendStatusLine("Completed");

                AppendStatusLine(string.Format("Process finished at: {0}", DateTime.Now));
                EnableImportButton();
            };

            thread.RunWorkerAsync(); 
        }

        /// <summary>
        /// Enable the Import Button
        /// </summary>
        private void EnableImportButton()
        {
            this.Invoke((MethodInvoker)delegate
            {
                ImportEntities.Enabled = true;
            });
        }

        #region Action Executers 
         
        /// <summary>
        /// Execute a collection of Actions in a threaded Context
        /// </summary>
        /// <param name="actions"></param>
        /// <param name="maxParallelism"></param>
        private void ExecuteActionsParallelOnContextPerThread(IEnumerable<Action<IOrganizationService, CrmContext>> actions, int maxParallelism)
        {
            // FirstActions are actions that need done before threading begins
            // MiddleAcctions are the threaded bulk of actions
            // Last Actions are actions that must be done after all threaded actions end

            var actionCount = actions.Count();
            if (actionCount > 0)
            {
                //we have at least 1 action here, so it is safe to pick out the first one
                var firstAction = actions.First();
                //the 'middle actions' are the 
                var middleActions = actionCount > 2 ? actions.Skip(1).Take(actionCount - 2) : null;
                var lastAction = actionCount > 1 ? actions.Last() : null;

                //todo:revisit the way the first and last action are run by themselves.  Is it more reasonable for the 'actions' themselves to be 
                //somehow noted as needing to be run sequentially?  This works ok, but progress is difficult to track when the first and last actions
                //are long running

                int i = 0;
                using (var tc = new ThreadActionContext(CurrentProject))
                {
                    //execute first action
                    if (!stop)
                    {
                        AppendStatusLine("Executing first action...");
                        try
                        {
                            firstAction.Invoke(tc.Current.Service, tc.Current.CrmContext);
                            UpdateRowCount(++i);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message, ex);
                            AppendStatusLine(string.Format("Exception on first action! ({0})", ex.Message));
                            return;
                        }
                    }

                    if (!stop && middleActions != null)
                    {
                        AppendStatusLine("Executing the bulk of the actions...");
                        //execute all the middle actions in parallel if allowed
                        middleActions.AsParallel().WithDegreeOfParallelism(maxParallelism).ForAll(a =>
                        {
                            if (!stop)
                            {
                                try
                                {
                                    a.Invoke(tc.Current.Service, tc.Current.CrmContext);
                                    UpdateRowCount(++i);
                                }
                                catch (Exception ex)
                                {
                                    Log.Error(ex.Message, ex);
                                    tc.InvalidateContext();
                                    stop = true;
                                    AppendStatusLine(string.Format("Exception! ({0})", ex.Message));
                                }
                            }
                        });
                    }

                    //execute last action
                    if (!stop && lastAction != null)
                    {
                        AppendStatusLine("Executing last action...");
                        try
                        {
                            lastAction.Invoke(tc.Current.Service, tc.Current.CrmContext);
                            UpdateRowCount(++i);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message, ex);
                            //AppendStatusLine(string.Format("Exception on last action! ({0})", ex.Message));
                            AppendNestedExceptionMessages(ex);
                            return;
                        }
                    }
                }
            };
        }

        #endregion
        
        /// <summary>
        /// Append Nested Exceptions as an InnerException
        /// </summary>
        /// <param name="ex"></param>
        private void AppendNestedExceptionMessages(Exception ex)
        {
            AppendStatusLine(ex.Message);

            if (ex.InnerException != null)
                AppendNestedExceptionMessages(ex.InnerException);
        }

        /// <summary>
        /// Update the status of the process on the UI
        /// </summary>
        /// <param name="message"></param>
        private void AppendStatusLine(string message)
        {
            //support use across many threads
            lock (this)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Status.AppendText(string.Format("{0}\n", message));
                    this.Status.Refresh();
                });
            }
        }

        /// <summary>
        /// Update the number of Actions Processed
        /// </summary>
        /// <param name="count"></param>
        private void UpdateRowCount(int count)
        {
            //support use across many threads
            lock (this)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.lblRowCount.Text = count.ToString();
                    this.lblRowCount.Refresh();
                });
            }
        }
         
        /// <summary>
        /// This is a method used for testing code or processes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {  
            BackgroundWorker thread = new BackgroundWorker();
             thread.DoWork += (caller, args) =>
            {

               CurrentProject.ExecuteInContext(delegate(CrmContext context, OrganizationServiceProxy proxy)
                            {
                                var closes = (from oc in context.IncidentResolutionSet
                                              where oc.Subject == "Migration Close"
                                                 select oc.ActivityId.Value).ToList();

                                int i = 0;
                                AppendStatusLine(closes.Count.ToString() + " records");
                                List<Action<IOrganizationService>> actions = new List<Action<IOrganizationService>>();
                                foreach (Guid id in closes)
                                {
                                    actions.Add(new Action<IOrganizationService>(service =>
                                    {
                                        i++;
                                        proxy.Delete(IncidentResolution.EntityLogicalName, id);
                                        UpdateRowCount(i);
                                    }));
                                }

                                actions.AsParallel().WithDegreeOfParallelism(10).ForAll(t =>
                                {
                                    try
                                    {
                                        t.Invoke(proxy);
                                    }
                                    catch (Exception ex)
                                    {
                                        AppendStatusLine(ex.Message);
                                    }
                                });
                            });  
            };
            thread.RunWorkerAsync();
        }
                
        /// <summary>
        /// Stop the Import
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop_Click(object sender, EventArgs e)
        {
            AppendStatusLine("Stopping...");
            stop = true;
        } 
         
        /// <summary>
        /// Ensure that project has the required information set
        /// </summary>
        private bool IsProjectValid
        {
            get
            {
                return (!string.IsNullOrEmpty(CurrentProject.OrganizationName) && !string.IsNullOrEmpty(CurrentProject.DiscoveryAddress) &&
                    !string.IsNullOrEmpty(CurrentProject.Domain) && !string.IsNullOrEmpty(CurrentProject.Username) && !string.IsNullOrEmpty(CurrentProject.Password));
            }
        }
         
        /// <summary>
        /// Import Email Attachments
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportEmailAttachments_Click(object sender, EventArgs e)
        {
            BackgroundWorker thread = new BackgroundWorker();

            btnImportEmailAttachments.Enabled = false;
            UpdateRowCount(0);
            AppendStatusLine("Beginning import of email attachments...");

            // Break the Email Attachment Import into Batches
            List<EmailAttachmentImportTask> taskGroups = new List<EmailAttachmentImportTask>();
            taskGroups.Add(new EmailAttachmentImportTask(1, 10000));
            taskGroups.Add(new EmailAttachmentImportTask(10001, 20000));
            taskGroups.Add(new EmailAttachmentImportTask(20001, 30000));
            taskGroups.Add(new EmailAttachmentImportTask(30001, 40000));
            taskGroups.Add(new EmailAttachmentImportTask(40001, 50000));
            taskGroups.Add(new EmailAttachmentImportTask(50001, 60000));
            taskGroups.Add(new EmailAttachmentImportTask(60001, 70000));
            taskGroups.Add(new EmailAttachmentImportTask(70001, 80000));

            thread.DoWork += (caller, args) =>
            {
                int emailAttachmentsImported = 0;

                try
                {
                    // 10 threads
                    taskGroups.AsParallel().WithDegreeOfParallelism(10).ForAll(t =>
                    {
                        t.OnItemCreated += (t_sender, t_e) =>
                        {
                            UpdateRowCount(++emailAttachmentsImported);
                            AppendStatusLine(string.Format("Imported '{0}' of length {1:0.0}KiB", t_e.FileName, t_e.FileSize / 1024.0));
                        };

                        try
                        {
                            t.PerformTask();
                        }
                        catch (Exception ex)
                        {
                            AppendStatusLine(string.Format("Exception in group '{0} to {1}'! {2}", t.StartRow, t.EndRow, ex.Message));
                        }
                    });
                }
                catch (Exception ex)
                {
                    AppendStatusLine(string.Format("Exception! {0}", ex.Message));
                }
            };

            thread.RunWorkerCompleted += (x, y) =>
            {
                btnImportEmailAttachments.Enabled = true;
                AppendStatusLine("Done");
            };

            thread.RunWorkerAsync();
        }

        /// <summary>
        /// Display Query and notes based on Mapper Selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntitiesToImport_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = Convert.ToString(EntitiesToImport.SelectedItem);
            var mapper = mappers[value];
            Query.Text = mapper.Query;
            MapperNotes.Clear();
            MapperNotes.Text = mapper.Notes;
            ChangeText(MapperNotes);
        }

        /// <summary>
        /// Enable the default query to be overridden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverrideQuery_CheckedChanged(object sender, EventArgs e)
        {
            Query.Enabled = OverrideQuery.Checked;
        }

        /// <summary>
        /// Formats text in the Notes Field
        /// </summary>
        /// <param name="richTextBox"></param>
        public static void ChangeText(RichTextBox richTextBox)
        {
            string[] titles = new string[] { "BEFORE IMPORT", "AFTER IMPORT" };
            int startIndex = 0;

            System.Drawing.Font newFont = new Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 178, false);

            foreach (string line in richTextBox.Lines)
            {
                foreach (string title in titles)
                {
                    if (line.Contains(title))
                    {
                        richTextBox.Select(startIndex, line.Length);
                        richTextBox.SelectionFont = newFont;
                    }
                }
                startIndex += line.Length + 1;
            }
        }

        /// <summary>
        /// Enables user to continue import by acknowledging that the required steps have been completed first
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Yes_Click(object sender, EventArgs e)
        {
            VerifyRequiredEntities.Enabled = true;
        }
    }
}
