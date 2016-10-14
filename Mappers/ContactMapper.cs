using Microsoft.Xrm.Sdk;
using Osv.Crm.Entities;
using System;
using System.Data;

namespace CRMDataImport.Mappers
{
    public class ContactMapper : MapperBase<Contact>
    {
        public ContactMapper(bool update)
            : base(SourceDatabaseEnum.CRM3, update) 
        {
            
                this.Query = @"select  
                                        c.ContactId,
                                        cast(
                                        case
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Archbishop' then '9BB150B3-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'ARCHDEACON' then 'A3FDE9BD-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'ARCHPRIEST' then '7A4859C7-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'assoc pastor' then 'CA6F8E3D-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Associat Pastor' then 'CA6F8E3D-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Associate Pastor' then 'CA6F8E3D-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'AUX BISHOP' then 'FC0862D0-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'AUXILIARY BISHOP' then 'FC0862D0-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'BISHOP' then 'FC0862D0-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'BR' then 'F51B5DDB-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Bro' then 'F51B5DDB-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Brother' then 'F51B5DDB-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Brotherev' then 'F51B5DDB-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Dc' then '6A7139E3-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Dcn' then '6A7139E3-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'DEACON' then '6A7139E3-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Deacon - Rev' then '9C489C66-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Deacon Rev' then '9C489C66-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Deacons' then '6A7139E3-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Deacpm' then '6A7139E3-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Decan' then '6A7139E3-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Decon' then '6A7139E3-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Dn' then '6A7139E3-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'DR' then 'AF3E4DED-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Dr.' then 'AF3E4DED-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'EV' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Fasther' then 'CFCDECF8-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'FATHER' then 'CFCDECF8-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'FATHER C.Ss.R.' then 'CFCDECF8-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'FDEACON' then '6A7139E3-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'FR' then 'CFCDECF8-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'FR- Pastoral Asst.' then 'CFCDECF8-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Fr.' then 'CFCDECF8-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'FTHER' then 'CFCDECF8-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Mgr' then '36F35014-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Minister' then '4A9CCE0A-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Miss' then 'A022F035-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Monsignor' then '36F35014-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Monsognor' then '36F35014-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'MOST REV' then '88F1D81C-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Most Rev.' then '88F1D81C-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Most Reverend' then '88F1D81C-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'MR' then 'C496CE25-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Mr.' then 'C496CE25-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'MRMS' then 'C496CE25-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'MRS' then 'B4686E2E-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Mrs.' then 'B4686E2E-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'MRSS' then 'B4686E2E-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'MS' then 'A022F035-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Ms.' then 'A022F035-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Msgr' then '36F35014-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Msgr Rev' then '17803183-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Msgr.' then '36F35014-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'MsMS' then 'A022F035-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'NRS' then 'B4686E2E-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Parochial Vicar' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Pastor' then 'CA6F8E3D-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Pastor- Emeritus' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Pr' then 'CA6F8E3D-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rav' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Reb' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rec' then '145A2548-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rector' then '145A2548-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Ref' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Refv' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'REN' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'RETIRED Rev' then 'CFCDECF8-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev -' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev - Associate Pastor' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'REV  MSGR' then '17803183-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev - Parochial Vicar' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev - Pastor Emeritus' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'REV -assistant Pastor' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev Assoc.' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev -assoc. pastor' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev- Associate Pastor' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'REV DEACON' then '9C489C66-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev Decon' then '9C489C66-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'REV FATHER' then '62EEDA70-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'REV FR' then '62EEDA70-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev Monsignor' then '17803183-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev Most Rev' then '88F1D81C-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'REV MR' then 'F2FE98A5-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev Msg' then '17803183-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'REV MSGR' then '17803183-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'REV MSGR.' then '17803183-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev- Parochial Vicar' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev -Parochial Vicars' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev- Pastor Emeritus' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev- resident' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev Rev' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev vicar' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev.' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev. Assoc. Pastor' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev. Canon' then '70DBD959-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev. Father' then '62EEDA70-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev. Mr.' then 'F2FE98A5-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev. Msgr.' then '17803183-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Administrator' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Asoc. Pastor' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Assoc' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Assoc,.Pastor' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Assoc.' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-assoc. Pastor' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Assoc.Pastor' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Associate Pastor' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'REV-associates' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Assoicate Pastor' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Asst' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Asst.' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Asst-Pastor' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Revc' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-EMERITUS' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Reverend' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Grajales' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-In residence' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Parochial Vicar' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Parochial Vicars' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Parochial VZicar' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Parocial Vicars' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-pastor Emeritus' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Pastoral Associate' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Rev-Priest Moderator' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'RT REV' then '7AECE88E-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'SIS' then '16FE2697-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'SISTER' then '16FE2697-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Sister-Pastoral Assoc.' then '16FE2697-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Sr' then '16FE2697-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Sr.' then '16FE2697-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Sr-Pastoral associate' then '16FE2697-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'THE MOST REV' then '88F1D81C-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'THE MOST REVEREND' then '88F1D81C-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'THE REV MR' then 'F2FE98A5-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'The Reverend, RC.M.' then 'ACA4914F-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'THE VENERABLE' then 'D14C70B0-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'The Very Rev' then 'D44CF8B8-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'ther' then 'CFCDECF8-9F36-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'VERY REV' then 'D44CF8B8-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Very Rev Msgr' then '0D730BC1-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'VERY REV.' then 'D44CF8B8-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'Very Reverend' then 'D44CF8B8-A036-E311-B1CF-00155D028117'
	                                        when LTRIM(RTRIM(c.Salutation)) = 'VREV FR' then '0C730BC1-A036-E311-B1CF-00155D028117'
	                                        else null
                                        end as uniqueidentifier) as 'allgnt_Prefix',
                                        'allgnt_customernametitle' as 'allgnt_PrefixLogicalName',
                                        c.FirstName,
                                        c.MiddleName,
                                        c.LastName,
                                        c.GenderCode, 
                                        c.ParentCustomerId,
                                        'account' as 'ParentCustomerIdLogicalName',                                        
                                        case
	                                        when AccountRoleCode = 8 then 739280001
	                                        when AccountRoleCode = 6 then null
	                                        when AccountRoleCode = 15 then 739280002
	                                        when AccountRoleCode = 9 then 739280003
	                                        when AccountRoleCode = 10 then null
	                                        when AccountRoleCode = 12 then 739280004
	                                        when AccountRoleCode = 3 then 739280009
	                                        when AccountRoleCode = 14 then 739280006
	                                        when AccountRoleCode = 17 then 739280004
	                                        when AccountRoleCode = 16 then 739280007
	                                        when AccountRoleCode = 7 then 739280005
	                                        when AccountRoleCode = 19 then 739280007
	                                        when AccountRoleCode = 4 then 739280006
	                                        when AccountRoleCode = 1 then 739280008
	                                        when AccountRoleCode = 11 then 739280009
	                                        when AccountRoleCode = 13 then 739280004
	                                        when AccountRoleCode = 2 then 739280004
	                                        when AccountRoleCode = 5 then 739280004
	                                        when AccountRoleCode = 18 then 739280007
	                                        else null
                                        end as 'allgnt_OSRole',  
                                        su.DomainName as 'OwnerId', 
                                        --c.Address1_AddressId, 
                                        c.Address1_AddressTypeCode,
                                        c.Address1_Line1,
                                        c.Address1_Line2,
                                        c.Address1_City,
                                        c.Address1_StateOrProvince,
                                        c.Address1_PostalCode,
										case 
											when LTRIM(RTRIM(c.Address1_Country)) = 'USA' then 'US'
											when LTRIM(RTRIM(c.Address1_Country)) = 'U.S.A.' then 'US'
											when LTRIM(RTRIM(c.Address1_Country)) = 'UNITED STATES' then 'US'
											when LTRIM(RTRIM(c.Address1_Country)) = '' then 'US'
											when c.Address1_Country is null then 'US'
											else c.Address1_Country
										end as 'Address1_Country', 
                                        c.Telephone1,
                                        c.Telephone2,
                                        c.MobilePhone, 
                                        c.Fax,
                                        c.EMailAddress1, 
                                        c.[Description],
                                        c.PreferredContactMethodCode, 
                                        c.DoNotPostalMail, 
                                        c.DoNotPhone,
                                        c.DoNotFax,
                                        c.DoNotBulkPostalMail,
                                        c.DoNotEMail,
                                        a.AccountNumber as 'allgnt_CustomerNumber'     
                                        from Contact c
                                        inner join SystemUser su 
	                                        on c.OwnerId = su.SystemUserId
	                                    inner join Account a 
											on a.AccountId = c.AccountId
                                        left join StringMap os_rol
	                                        on os_rol.AttributeValue = c.AccountRoleCode and os_rol.AttributeName = 'accountrolecode'  ";
           
        }

        protected override bool MapField(string name, IDataReader reader, NewEntityModel model)
        {
            
            if (name == "OwnerId")
            {
                string domainname = reader.GetTypedValue<string>("OwnerId").ToLower();
                string defaultCRM3owner = Common.MissingCRM3DefaultOwner;

                Guid ownerid;

                if (DataMigration.CurrentProject.Dictionaries.SystemUsers.ContainsKey(domainname))
                    ownerid = DataMigration.CurrentProject.Dictionaries.SystemUsers[domainname];
                else
                    ownerid = DataMigration.CurrentProject.Dictionaries.SystemUsers[defaultCRM3owner];

                model.Entity.OwnerId = new EntityReference(SystemUser.EntityLogicalName, ownerid);

                return true;
            }
            return base.MapField(name, reader, model);
        }

        public override bool IsImportable(Contact entity)
        {
            return !DestinationKeyExists(entity.Id,"Contact") && entity.ParentCustomerId != null && DestinationKeyExists(entity.ParentCustomerId.Id, "Account");
        } 
    }
}
