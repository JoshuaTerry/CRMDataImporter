using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace CRMDataImport.Mappers
{
    public class AppointmentMapper : MapperBase<Appointment>
    {
        public AppointmentMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update)
        {

            if (update)
            {
                this.Query = @"
select
                                Appointment.ActivityId, 
                                case 
									when Appointment.New_TypeApp = 2 then 739280000
									when Appointment.New_TypeApp = 3 then 739280001
									when Appointment.New_TypeApp = 1 then 739280002
									when Appointment.New_TypeApp = 5 then 739280003
									else null
								end as 'allgnt_AppointmentType'
                                from Appointment
                                where Appointment.New_TypeApp is not null and Appointment.StateCode = 0
";
            }
            else
            {
                this.Query = @"

select
                                Appointment.ActivityId,
                                Appointment.[Subject], 
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
                                where Party.ActivityId = Appointment.ActivityId 
                                 FOR XML RAW('Party'), TYPE, Root('Parties')) as 'Parties', 
                                Appointment.Location,
                                Appointment.ActualStart,
                                Appointment.ScheduledStart, 
                                Appointment.RegardingObjectId, 
                                Appointment.RegardingObjectTypeCode as 'RegardingObjectIdMapperTypeCode',
                                Appointment.ScheduledEnd,
                               cast (Appointment.IsAllDayEvent as bit) as 'IsAllDayEvent',
                                case 
									when Appointment.New_TypeApp = 2 then 739280000
									when Appointment.New_TypeApp = 3 then 739280001
									when Appointment.New_TypeApp = 1 then 739280002
									when Appointment.New_TypeApp = 5 then 739280003
									else null
								end as 'allgnt_AppointmentType',
                                Appointment.CreatedOn as 'OverriddenCreatedOn',
                                Appointment.[Description], 
                                su.DomainName as 'OwnerId' 
                                from Appointment 
                                inner join SystemUser su
	                                on su.SystemUserId = Appointment.OwnerId  ";
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
                var typesWeCareAbout = new int[] { 5, 6, 7 };  
                var required = new List<ActivityParty>();  
                var optional = new List<ActivityParty>();  
                var organizers = new List<ActivityParty>();   

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
                            string partyIdName = element.Attribute("PartyIdName").Value;

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
                            ap.AddressUsed = Common.CleanEmailAddress( element.Attribute("AddressUsed").Value);
                        }

                        //make sure activity party is valid
                        if (string.IsNullOrEmpty(ap.AddressUsed) && ap.PartyId == null)
                        {
                            Log.Warn(string.Format("ActivityParty had neither valid address or linked object. Source ActivityPartyId:{0}", activityPartyId));
                            continue;
                        }

                        //what bucket do we need to store this ap in so we can add it to the entity once we collect them all
                        switch (typeMask)
                        {
                            case (5):
                                //this is a from
                                required.Add(ap);
                                break;
                            case (6):
                                //this is a to
                                optional.Add(ap);
                                break;
                            case (7):
                                //this is a cc
                                organizers.Add(ap);
                                break; 
                        }
                    }
                }

                //add the xml generated ap's to the right properties
                model.Entity.OptionalAttendees = optional;
                model.Entity.RequiredAttendees = required;
                model.Entity.Organizer = organizers; 

                return true;
            }

            return base.MapField(name, reader, model);
        }

        public override bool IsUpdateable(Appointment entity)
        {
            return DestinationKeyExists(entity.ActivityId.Value, "Appointment");
        }

        public override bool IsImportable(Appointment entity)
        {
            return !DestinationKeyExists(entity.ActivityId.Value, "Appointment")&& (entity.RegardingObjectId == null || 
                DestinationKeyExists(entity.RegardingObjectId.Id,"Account","Contact","Opportunity","Incident"));
        }
    }
}
 