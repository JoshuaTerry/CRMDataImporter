﻿using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace CRMDataImport.Mappers
{
    public class EmailMapper : MapperBase<Email>
	{
        public EmailMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
		{
            if (update)
            {
                this.Query = @" select top 100
                                Email.ActivityId as 'ActivityId', 
                                Email.[Description] as 'Description'
                                  from Email Email
                                
                                ";
            }
            else
            {
                this.Query = @" select
                                Email.ActivityId as 'ActivityId', 
                                (select  
	                                PartyId,
                                    PartyIdName,
	                                PartyObjectTypeCode,
	                                ParticipationTypeMask,
	                                AddressUsed,
	                                ActivityPartyId,
									su.DomainName  
                                from ActivityParty Party 
								left join SystemUser su
									on Party.PartyId = su.SystemUserId and 
									Party.PartyObjectTypeCode = 8 
                                where Party.ActivityId = Email.ActivityId 
                                FOR XML Raw('Party'), TYPE, Root('Parties')) as 'Parties' ,	  
                                Email.[Subject] as 'Subject',
                                Email.[Description] as 'Description',
                                --'(not loaded)' as 'Description',
                                Email.RegardingObjectId as 'RegardingObjectId', 
                                Email.RegardingObjectTypeCode as 'RegardingObjectIdMapperTypeCode', 
                                su.DomainName as 'OwnerId',
                                Email.ActualDurationMinutes,
                                Email.ScheduledEnd as 'ScheduledEnd',  
                                Email.PriorityCode,
                                Email.CreatedOn as 'OverriddenCreatedOn' 
                                from Email Email  
                                inner join SystemUser su
	                                on su.SystemUserId = Email.OwnerId 
                                ";
            }
		}

        protected override bool MapField(string name, IDataReader reader, NewEntityModel model)
        {
            if (name == "Parties")
            {
                string partiesText = reader.GetTypedValue<string>("Parties");
                Guid activityId = reader.GetTypedValue<Guid>("ActivityId");
                XDocument partiesXdoc = XDocument.Parse(partiesText);

                ///define the types we care about, and buckets to store
                ///the generated activity parties
                var typesWeCareAbout = new int[] { 1,2,3,4 }; //1-sender, 
                var froms = new List<ActivityParty>(); //1
                var tos = new List<ActivityParty>(); //2
                var ccs = new List<ActivityParty>(); //3
                var bccs = new List<ActivityParty>(); //4
                
                //get all the parties from the parsed xml doc
                var partiesElements = (from e in partiesXdoc.Elements("Parties").Descendants() 
                           select e);

                //walk through all the found party elements and figure out what we can do with them
                foreach (var element in partiesElements)
                {
                    //get all the attributes for this element and also what the mask (type)
                    //of the party we are looking at is.
                    var attributes = element.Attributes();
                    var typeMask = int.Parse(element.Attribute("ParticipationTypeMask").Value);
                    int typeCode = int.Parse(element.Attribute("PartyObjectTypeCode").Value);
                    Guid activityPartyId = Guid.Parse(element.Attribute("ActivityPartyId").Value);

                    //is this element a representation of a party type we even care to import?
                    if (typesWeCareAbout.Contains(typeMask))
                    {
                        ActivityParty ap = new ActivityParty();

                        //is this activity party related to another object?
                        if (attributes.Any(x => x.Name == "PartyId"))
                        {  
                            Guid partyid = Guid.Parse(element.Attribute("PartyId").Value);


                            // If the typeCode indicates PartyId is a SystemUser
                            // The Id must be set to the mapped SystemUser Id based on DomainName 
                            if (typeCode == 8)
                            {
                                string domainname = element.Attribute("DomainName").Value.ToLower();

                                if (Project.Dictionaries.SystemUsers.ContainsKey(domainname))
                                    partyid = Project.Dictionaries.SystemUsers[domainname]; 
                            }

                            if (DestinationKeyExists(partyid, "SystemUser", "Contact", "Account"))
                            {
                                ap.PartyId = new EntityReference(StaticDictionaries.EntityTypeCodes[typeCode], partyid);
                            }
                            else
                            {
                                Log.Warn(string.Format("Unable to find ActivityParty's related object in the destination system. Source ActivityPartyId:{0}", activityPartyId));
                            }
                            
                        }

                        if (attributes.Any(x => x.Name == "AddressUsed"))
                        {
                            ap.AddressUsed = Common.CleanEmailAddress(element.Attribute("AddressUsed").Value);
                        }

                        //make sure activity party is valid
                        if (string.IsNullOrEmpty(ap.AddressUsed) && ap.PartyId == null)
                        {
                            Log.Warn(string.Format("ActivityParty had neither valid address or linked object. Source ActivityPartyId:{0}", activityPartyId));
                            continue;
                        }

                        //what bucket do we need to store this ap in so we can add it to the entity once we collect them all
                        switch(typeMask)
                        {
                            case(1):
                                //this is a from
                                froms.Add(ap);
                                break;
                            case (2):
                                //this is a to
                                tos.Add(ap);
                                break;
                            case(3):
                                //this is a cc
                                ccs.Add(ap);
                                break;
                            case(4):
                                //this is a bcc
                                bccs.Add(ap);
                                break;
                        }
                    }
                }

                //add the xml generated ap's to the right properties
                model.Entity.To = tos;
                model.Entity.From = froms;
                model.Entity.Cc = ccs;
                model.Entity.Bcc = bccs;
                 
                return true;
            }

            return base.MapField(name, reader, model);
        }
         
        public override bool IsImportable(Email entity)
		{
            return !DestinationKeyExists(entity.ActivityId.Value,"Email") 
                && (entity.RegardingObjectId == null ||
                DestinationKeyExists(entity.RegardingObjectId.Id, "Account", "Contact", "Opportunity", "Incident", "Quote"));
		}

        public override bool IsUpdateable(Email entity)
        { 
            return DestinationKeyExists(entity.ActivityId.Value, "Email");
        }        
	}
}