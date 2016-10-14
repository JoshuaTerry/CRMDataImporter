using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Data;

namespace CRMDataImport.Mappers
{
    public class AgentAreaCodeMapper : MapperBase<allgnt_agentareacodemap>
	{
        public AgentAreaCodeMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
		{
            this.Query = @"          
	                        select  a.New_AgentAreaCodeMapId as 'allgnt_agentareacodemapId',
			                        a.New_AreaCode as 'allgnt_AreaCode', 
			                        sm.Value as 'allgnt_State',
		                         su.DomainName as 'allgnt_SystemUser'
                        from dbo.New_AgentAreaCodeMapExtensionBase a
                         inner join SystemUser su 
                         on su.SystemUserId = New_SystemUser
                        inner join StringMap sm
                        on a.New_State = sm.AttributeValue and sm.AttributeName = 'new_state'  "; 
		}

        protected override bool MapField(string name, IDataReader reader, NewEntityModel model)
        {
            if (name == "allgnt_State")
            {
                string fullname = reader.GetTypedValue<string>("allgnt_State"); 
                string abbrv = StaticDictionaries.StateFullNameToAbbreviation[fullname];

                osv_statecodes sc = (osv_statecodes)Enum.Parse(typeof(osv_statecodes), abbrv);

                model.Entity.allgnt_State = new OptionSetValue((int)sc);

                return true;
            }

            return base.MapField(name, reader, model);
        }

		public override bool IsImportable(allgnt_agentareacodemap entity)
		{ 
            return !DestinationKeyExists(entity.allgnt_agentareacodemapId.Value, "allgnt_agentareacodemap"); 
		} 
	}
}


