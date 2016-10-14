using Microsoft.Xrm.Sdk;
using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CRMDataImport
{
    public static partial class Common
    {
        //Item 189 & 71 - The following notes the users that will assume ownership of certain entities when the referenced 
        // owner Id is not found in the 2011 system.
        // The above items are no longer valid.  It was decided that the following new Accounts should be used for importing entities
        public const string MissingCRM3DefaultOwner = @"[Redacted]\osintegration";
        public const string MissingACTDefaultOwner = @"[Redacted]\pubintegration";
        
        /// <summary>
        /// Method extension for IDataReader to automatically return a value in the desired type
        /// </summary>
        /// <typeparam name="T">In addition to all value types T can be of Type Money, OptionSetValue, & EntityReference</typeparam>
        /// <param name="reader"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static T GetTypedValue<T>(this IDataReader reader, string field)
        {
            T t = default(T);
            int ordinal = reader.GetOrdinal(field);

            if (!reader.IsDBNull(ordinal))
            {
                object value = reader.GetValue(ordinal);

                if (value != null && typeof(T) == typeof(Money))
                {
                    t = Activator.CreateInstance<T>();
                    (t as Money).Value = (decimal)value;
                }
                else if (value != null && typeof(T) == typeof(OptionSetValue))
                {
                    t = Activator.CreateInstance<T>();
                    (t as OptionSetValue).Value = (int)value;
                }
                else if (value != null && typeof(T) == typeof(EntityReference))
                {
                    // Id's are preserved where possible when moving data between systems, however user guids for system users are different
                    // For fields referencing users we have to use the domain/username to lookup the id in the new system
                    string logicalname = string.Empty;
                    if (field == "OwnerId" || field == "PrimaryUserId" || field == "CreatedBy" || field == "allgnt_SystemUser")
                    {
                        string domainname = reader.GetString(reader.GetOrdinal(field)).ToLower();
                        Guid? ownerid = null;

                        if (DataMigration.CurrentProject.Dictionaries.SystemUsers.ContainsKey(domainname))
                            ownerid = DataMigration.CurrentProject.Dictionaries.SystemUsers[domainname];
                        else //Added 3/18 All CRM3 entities without resolvable owners will now belong to a single user
                            ownerid = DataMigration.CurrentProject.Dictionaries.SystemUsers[MissingCRM3DefaultOwner];

                        logicalname = "systemuser";
                        value = ownerid;
                    }
                    // Checks to see if the MapperTypeCode has been specified for the EntityReference and then
                        //uses the dictionary to convert that into the appropriate logical name
                    else if (reader.HasColumn(field+"MapperTypeCode"))
                    {
                        int typecode = reader.GetInt32(reader.GetOrdinal(field + "MapperTypeCode"));
                        logicalname = StaticDictionaries.EntityTypeCodes[typecode];
                    }
                    else
                    { 
                        // Entity References require a Logical Name to be specified.  Any field that is of Type Entity reference will have 
                        // an additional field in the reader that is the field name + "LogicalName"
                        if (!reader.HasColumn(field + "LogicalName"))
                            throw new Exception("EntityReference requires a LogicalName in addition to the Id field.  Field: " + field);
                        
                        logicalname = reader.GetString(reader.GetOrdinal(field + "LogicalName")); 
                    } 

                    if (value != null)
                    {
                        t = Activator.CreateInstance<T>();
                        (t as EntityReference).LogicalName = logicalname;
                        (t as EntityReference).Id = Guid.Parse(Convert.ToString(value));
                    } 
                }
                else
                {
                    t = (T)value;
                }
            }
            return t;
        }
         
        /// <summary>
        /// Extension to see if a datareader has a column
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
         
        private static Regex emailRegex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                        + "@"
                        + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");

        public static string CleanEmailAddress(string address)
        {

            //some addresses are blank or the string 'none' in the source system
            //crm hates this
            address = address.ToLower().Trim();
            if (address == string.Empty || address == "none")
                return "[Redacted]";

            //there are some quotes in email addresses
            address = address.Replace("\"", "");

            //remove single quotes
            address = address.Replace("'", "");

            //verify that it is correct, because CRM will blow up if wrong
            if (!emailRegex.IsMatch(address))
            {
                return "[Redacted]";
            }
             
            return address;
        }
         
        /// <summary>
        /// Extension method to try to return a value or the default value for that type in an XElement
        /// </summary>
        /// <param name="parentEl"></param>
        /// <param name="elementName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string TryGetElementValue(this XElement parentEl, string elementName, string defaultValue = null)
        {
            var foundEl = parentEl.Element(elementName);
            if (foundEl != null)
            {
                return foundEl.Value;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Convert a row of a datareader to a string
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static string TryReaderRowToString(IDataReader reader)
        {
            StringBuilder b = new StringBuilder();
            try
            {
                object[] valueArray = new object[reader.FieldCount];
                reader.GetValues(valueArray);
                foreach(object val in valueArray)
                {
                    if(val!=null)
                        b.AppendFormat("{0},",val.ToString());
                    else
                        b.Append("<null>,");
                }
                return b.ToString();
            }
            catch { return String.Empty; }
        }
    } 
}
