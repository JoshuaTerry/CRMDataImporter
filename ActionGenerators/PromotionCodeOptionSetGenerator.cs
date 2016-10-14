using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Osv.Crm.Entities;
using System;

namespace CRMDataImport.ActionGenerators
{
    public class PromotionCodeOptionSetGenerator : ImportQueryActionGenerator
    {
        public PromotionCodeOptionSetGenerator()
            : base(SourceDatabaseEnum.CRM3)
        {
            this.Query = @"select Value,
							AttributeValue,
							DisplayOrder
					from StringMap 
					where AttributeName = 'CFPpromotioncode'
					order by DisplayOrder";

            this.OSName = "allgnt_promotioncode";
            this.AllowParallelism = false;
        }

        protected string OSName { get; set; }

        protected override Action<IOrganizationService, CrmContext> MakeFirstAction()
        {
            //we have to create the option set
            return new Action<IOrganizationService, CrmContext>((service, context) =>
            {
                var test = service.Execute(new CreateOptionSetRequest
                {
                    OptionSet = new OptionSetMetadata
                    {
                        Name = this.OSName,
                        DisplayName = new Label("Promotion Code", 1033),
                        Description= new Label("Promotion Code",1033),
                        IsGlobal = true,
                        OptionSetType = OptionSetType.Picklist
                    }
                }); 
            });
        }

        protected override Action<Microsoft.Xrm.Sdk.IOrganizationService, Osv.Crm.Entities.CrmContext> MakeRowAction(System.Data.IDataReader reader)
        {

            string name = reader.GetTypedValue<string>("Value");
            int code = reader.GetTypedValue<int>("AttributeValue");
            int order = reader.GetTypedValue<int>("DisplayOrder");

            return new Action<IOrganizationService, CrmContext>((service, context) =>
            { 
                var response = service.Execute(new InsertOptionValueRequest
                {
                    OptionSetName = OSName,
                    Label = new Label(name, 1033)
                }); 
            });
        }

        protected override Action<IOrganizationService, CrmContext> MakeLastAction()
        {
            return new Action<IOrganizationService, CrmContext>((service, context) =>
            {
                PublishXmlRequest publishrequest = new PublishXmlRequest { ParameterXml = String.Format("<importexportxml><optionsets><optionset>{0}</optionset></optionsets></importexportxml>", OSName) };
                service.Execute(publishrequest);
            });
        }
    }
}
