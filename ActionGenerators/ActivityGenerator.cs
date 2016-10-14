using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace CRMDataImport.ActionGenerators
{
    public class ActivityGenerator : ImportQueryActionGenerator
    {
        public ActivityGenerator(SourceDatabaseEnum source) : base(source)
        {
            this.Query = @"";
        }

        protected override Action<IOrganizationService, CrmContext> MakeRowAction(IDataReader reader)
        {  
            throw new NotImplementedException();
        }

        /// <summary>
        /// Activities have fields that are collections.  These collections are returned as XML with record
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private IEnumerable<XElement> GetElements(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Activity")
                {
                    XElement element = XElement.ReadFrom(reader) as XElement;
                    if (element != null)
                        yield return element;
                }
            }
        }
    }
}
