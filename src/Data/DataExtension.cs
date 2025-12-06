using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Yokinsoft.Salesforce.MCE
{
    /// <summary>
    /// Provides a base class for representing common properties of a data extension, including metadata, ownership, and
    /// configuration settings.
    /// </summary>
    /// <remarks>This abstract class is intended to be inherited by types that model data extensions with
    /// shared characteristics, such as name, key, sendability, retention policies, and audit information. It
    /// centralizes common fields to promote consistency and reuse across data extension implementations.</remarks>
    public abstract class DataExtensionCommon
    {
        public string Name { get; set; }

        public string Key { get; set; }

        public string Description { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsSendable { get; set; }

        public string SendableCustomObjectField { get; set; }

        public string SendableSubscriberField { get; set; }

        public bool? IsTestable { get; set; }

        public long? CategoryId { get; set; }

        public long? OwnerId { get; set; }

        public bool? IsObjectDeletable { get; set; }

        public bool? IsFieldAdditionAllowed { get; set; }

        public bool? IsFieldModificationAllowed { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedById { get; set; }

        public string CreatedByName { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? ModifiedById { get; set; }

        public string ModifiedByName { get; set; }

        public string OwnerName { get; set; }

        public int? PartnerApiObjectTypeId { get; set; }

        public string PartnerApiObjectTypeName { get; set; }

        public long? RowCount { get; set; }

        public DataRetentionProperties DataRetentionProperties { get; set; }
    }
    public class DataExtensionToCreate : DataExtensionCommon
    {
        public List<DataExtension.FieldToCreate> Fields { get; set; } = new List<DataExtension.FieldToCreate>();
    }
    public class DataExtension : DataExtensionCommon
    {
        public string Id { get; set; }

        public int? FieldCount { get; set; }
        /// <summary>
        /// Represents a data record of a DataExtension.
        /// This is a member of DataExtensionItemListContainer which is returned by
        /// GetData call.
        /// An item that contains case-insensitive collections of keys and values as string pairs.
        /// </summary>
        /// <remarks>The Keys and Values dictionaries use case-insensitive string comparison for their
        /// keys. Key names may be normalized to lowercase and may not preserve their original casing. This class is
        /// intended for scenarios where key/value pairs must be accessed without regard to case sensitivity.</remarks>
        public class Item
        {
            /// <summary>
            /// Keys are typically simple string key/value pairs.
            /// Key names are case insensitive and can be changed to lowercase and may differ from the original casing.
            /// </summary>
            [JsonConverter(typeof(CaseInsensitiveDictionaryConverter))]
            public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>( StringComparer.InvariantCultureIgnoreCase );
            [JsonConverter(typeof(CaseInsensitiveDictionaryConverter))]
            public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            internal Dictionary<string, string> Flatten()
                => Keys.Concat(Values).ToDictionary(kvp => kvp.Key, kvp => kvp.Value, StringComparer.InvariantCultureIgnoreCase);
        }

        public class FieldToCreate
        {
            public string Name { get; set; }
            // Field "type" in JSON (e.g. "Number", "Text", "Date")
            public string Type { get; set; }

            public string MaskType { get; set; }

            public string StorageType { get; set; }

            public string Description { get; set; }

            public int? Ordinal { get; set; }

            public bool? IsNullable { get; set; }

            public bool? IsPrimaryKey { get; set; }

            public bool? IsTemplateField { get; set; }

            public bool? IsInheritable { get; set; }

            public bool? IsOverridable { get; set; }

            public bool? IsHidden { get; set; }

            public bool? IsReadOnly { get; set; }

            public bool? MustOverride { get; set; }


            [JsonIgnore]
            public Type DataType
            {
                get
                {
                    switch ((Type ?? "").ToLower())
                    {
                        case "text": return typeof(string);
                        case "number": return typeof(int);
                        case "date": return typeof(DateTime);
                        case "boolean": return typeof(bool);
                        case "decimal": return typeof(decimal);
                        case "email": return typeof(string);
                        case "phone": return typeof(string);
                        case "locale": return typeof(string);
                        case "country": return typeof(string);
                    }
                    ;
                    return typeof(string);
                }
            }
        }
        public class Field : FieldToCreate
        {
            public string Id { get; set; }
        }
        internal class FieldListContainer
        {
            public string Id { get; set; }
            public List<Field> Fields { get; set; } = new List<Field>();
            public int? FieldCount { get; set; }
        }
    }

    public class DataRetentionProperties
    {
        public bool? IsDeleteAtEndOfRetentionPeriod { get; set; }

        public bool? IsRowBasedRetention { get; set; }

        public bool? IsResetRetentionPeriodOnImport { get; set; }
    }
    

}
