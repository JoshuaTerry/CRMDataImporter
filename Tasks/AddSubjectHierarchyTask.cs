using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRMDataImport.Tasks
{ 
    public class AddSubjectHierarchyTask
    { 
        public AddSubjectHierarchyTask(IOrganizationService proxy, CrmContext context)
        {
            AddSubjects();
            _proxy = proxy;
            _context = context;
        }

        protected class SubjectModel
        {
            public string Division { get; set; }
            public string Category { get;set;}
            public string Title { get; set; }
        }

        protected List<SubjectModel> _subjects = new List<SubjectModel>();
        protected Dictionary<string, string> _catAbbreviations = new Dictionary<string, string>();
        protected Dictionary<string, Guid> _keyDictionary = new Dictionary<string, Guid>();
        protected CrmContext _context;
        protected IOrganizationService _proxy;
         
        /// <summary>
        /// These are Additional Subjects that were supplied via a Spreadsheet.  They are set up in a heirarchy
        /// </summary>
        private void AddSubjects()
        {
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Order Detail", Title = "Order entry error" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Order Detail", Title = "Sent to wrong address" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Order Detail", Title = "Wrong discount" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Order Detail", Title = "Used wrong shipping method" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Order Detail", Title = "Missing Product(s)" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Invoicing/Credit", Title = "Invoiced for incorrect amount" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Invoicing/Credit", Title = "Return never received at OSV" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Invoicing/Credit", Title = "Return never processed in Advantage " });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Invoicing/Credit", Title = "Improper credit used " });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Invoicing/Credit", Title = "Credit not issued due to damaged product received" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Invoicing/Credit", Title = "Credit not issued because product was not purchased from OSV" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Invoicing/Credit", Title = "Website/Credit card issue" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Fulfillment/Shipping", Title = "Picked wrong item(s)" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Fulfillment/Shipping", Title = "Shipped on wrong date" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Fulfillment/Shipping", Title = "Used wrong shipping method" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Fulfillment/Shipping", Title = "Picked damaged product" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Fulfillment/Shipping", Title = "Short-shipped" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Fulfillment/Shipping", Title = "Order not received " });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Fulfillment/Shipping", Title = "No tracking information" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Fulfillment/Shipping", Title = "Over-pick" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Transportation Issues", Title = "Delivered to wrong address" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Transportation Issues", Title = "Damaged in transit" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Transportation Issues", Title = "Delivered late" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Transportation Issues", Title = "Order lost " });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Transportation Issues", Title = "Customer unable to locate delivery" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Technical Support", Title = "Accessibility Issues" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Technical Support", Title = "Administrative Issues" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Technical Support", Title = "Content Issues" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Technical Support", Title = "CSR-ISR Referral" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Technical Support", Title = "Documentation Issue" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Technical Support", Title = "E-mail Issues" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Technical Support", Title = "Installation" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Technical Support", Title = "Other" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Technical Support", Title = "Sales Referral " });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Technical Support", Title = "Training" });
            _subjects.Add(new SubjectModel { Division = "Cases (Pub)", Category = "Technical Support", Title = "Website Issues" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Delivery (postal)", Title = "Non-delivery" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Delivery (postal)", Title = "Late delivery" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Delivery (postal)", Title = "Damaged Mail Piece" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Images", Title = "Wrong image/text" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Images", Title = "Missing image/text" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Images", Title = "Poor Image/Text Quality" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Invoice", Title = "Invoiced for incorrect amount" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Invoice", Title = "Not Invoiced" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Missing item from order " });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Received item that should have been removed" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Received wrong item" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Incorrect date" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Envelopes in the wrong order (sequence)" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Order not received " });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Poor quality" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Off Collation" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Printed Wrong Side" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Does not match (personalized) " });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Received item - did not order" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Customer submitted wrong data" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Order Detail", Title = "Other" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Parishioner List", Title = "Manual changes incorrect" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Parishioner List", Title = "Customer sent wrong file" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Parishioner List", Title = "Received envelopes with incorrect name/address or env# " });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Parishioner List", Title = "Received envelopes from the wrong parish " });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Parishioner List", Title = "Wrong file used for mailing" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Product/Services", Title = "Product Design " });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Product/Services", Title = "Products Offered " });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Product/Services", Title = "Services Offered" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Shipping", Title = "Order went to wrong address" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Shipping", Title = "Order not received " });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Shipping", Title = "Late shipment" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Shipping", Title = "Damaged order" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Shipping", Title = "Wrong shipping method selected" });
            _subjects.Add(new SubjectModel { Division = "Cases (OS)", Category = "Systems", Title = "Process failed" });

            _catAbbreviations.Add("Order Detail", "ORD");
            _catAbbreviations.Add("Invoicing/Credit", "INV");
            _catAbbreviations.Add("Fulfillment/Shipping", "SHP");
            _catAbbreviations.Add("Technical Support", "TEC");
            _catAbbreviations.Add("Transportation Issues", "TRN");
            _catAbbreviations.Add("Delivery (postal)", "DEL");
            _catAbbreviations.Add("Images", "IMG");
            _catAbbreviations.Add("Invoice", "INV");
            _catAbbreviations.Add("Parishioner List", "LST");
            _catAbbreviations.Add("Product/Services", "PRO");
            _catAbbreviations.Add("Shipping", "SHP");
            _catAbbreviations.Add("Systems", "SYS");

        }

        public void PerformTask()
        {
            foreach (var subject in _subjects)
            {
                //find or make the division (root level)
                var divId = FindOrMakeSubject(subject.Division, null);
                
                //get the category abbreviation
                var catAbbreviation = _catAbbreviations[subject.Category];
                if (catAbbreviation == null)
                    throw new InvalidOperationException(string.Format("Category '{0}' doesn't have a defined abbreviation.", subject.Category));
                
                //find or make the category incl the abbreviation
                var catId = FindOrMakeSubject(subject.Category+string.Format(" ({0})",catAbbreviation), divId);
                
                //find or make the subject incl the abbreviation
                FindOrMakeSubject(string.Format("{0} - ", catAbbreviation)+subject.Title, catId); 
            }
        }
         
        /// <summary>
        /// Find or create the subject and return the ID
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private Guid FindOrMakeSubject(string name, Guid? parent)
        { 
            //see if we already know about this one, return it now
            string key = GetKeyFromSubjectRequest(name, parent);
            if(_keyDictionary.ContainsKey(key))
                return _keyDictionary[key];

            //a reference to the parent (or null)
            EntityReference parentReference = parent.HasValue?new EntityReference(Subject.EntityLogicalName,parent.Value):null;

            //use linq to search the database for a subject with with the specified parent (or null for root level) and name
            var subject = (from s in _context.SubjectSet where s.ParentSubject == parentReference && s.Title==name select s).FirstOrDefault();

            //if we found one, return it
            if (subject != null)
            {
                //remember it and return it
                _keyDictionary[key] = subject.SubjectId.Value;
                return subject.SubjectId.Value;
            }
            else
            {

                Subject s = new Subject();
                s.Title = name;
                s.ParentSubject = parentReference;
                s.FeatureMask = 1;
                var newId = _proxy.Create(s);
                
                //remember it and return it
                _keyDictionary[key] = newId;
                return newId;
            } 
        }

        private string GetKeyFromSubjectRequest(string name, Guid? parent)
        {
            return string.Concat(parent.HasValue?parent.Value.ToString():String.Empty,":",name);
        } 
    }
}
