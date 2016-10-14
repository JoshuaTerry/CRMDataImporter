using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CRMDataImport.Tasks
{
    public class EmailAttachmentImportTask
    {
        public EmailAttachmentImportTask(int startRow, int endRow)
        {
            StartRow = startRow;
            EndRow = endRow;
            
            Query = @";WITH PageNumbers AS(
                            SELECT [AttachmentNumber]
                                              ,[ActivityMimeAttachmentId]
                                              ,[ActivityId]
                                              ,[Body]
                                              ,[Subject]
                                              ,[FileSize]
                                              ,[MimeType]
                                              ,[FileName],
                                    ROW_NUMBER() OVER(ORDER BY ActivityId) ROWNUMBER
                            FROM    ActivityMimeAttachment
                        )
                        SELECT  *
                        FROM    PageNumbers
                        WHERE   ROWNUMBER  >= @start and ROWNUMBER <= @end ";


        }

        private string Query { get; set; }

        public int StartRow { get; protected set; }
        public int EndRow { get; protected set; }

        public class OnItemCreatedEventArgs : EventArgs
        {
            public int TotalImported { get; set; }
            public int FileSize { get; set; }
            public string FileName { get; set; }
            public OnItemCreatedEventArgs(int count, int fileSize, string fileName)
            {
                TotalImported = count;
                FileSize = fileSize;
                FileName = fileName;
            }
        }

        public event EventHandler<OnItemCreatedEventArgs> OnItemCreated;
        
        private Project Project { get { return DataMigration.CurrentProject; } }

        public void PerformTask()
        {
            Project.ExecuteInContext((context, service) =>
            {
                int i = 0;

                using (SqlConnection sourceConnection = new SqlConnection(Project.CRM3ConnectionString))
                {
                     SqlCommand command = new SqlCommand(Query, sourceConnection);

                    command.Connection.Open();
                    command.CommandTimeout = 0;
                    command.Parameters.AddWithValue("@start", StartRow);
                    command.Parameters.AddWithValue("@end", EndRow);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid activityId = reader.GetTypedValue<Guid>("ActivityId");
                            Guid activityMimeAttachmentId = reader.GetTypedValue<Guid>("ActivityMimeAttachmentId");

                            //see if we already imported the activity that this attachment is related to
                            //AND we haven't already imported it
                            if (Project.Dictionaries.HasKeyBeenImported(activityId, "Email") &&
                                !Project.Dictionaries.HasKeyBeenImported(activityMimeAttachmentId,"ActivityMimeAttachment"))
                            {
                                ActivityMimeAttachment a = new ActivityMimeAttachment();

                                a.AttachmentNumber = reader.GetTypedValue<int>("AttachmentNumber");
                                a.ActivityMimeAttachmentId = activityMimeAttachmentId;
                                a.ActivityId = new EntityReference(Email.EntityLogicalName, activityId);
                                a.Body = reader.GetTypedValue<string>("Body");
                                a.Subject = reader.GetTypedValue<string>("Subject");
                                a.MimeType = reader.GetTypedValue<string>("MimeType");
                                a.FileName = reader.GetTypedValue<string>("FileName");
                                
                                service.Create(a);

                                OnItemCreated(this, new OnItemCreatedEventArgs(i++,a.Body.Length,a.FileName));

                            }
                        }
                    }
                }
            });
        }
    }
}
