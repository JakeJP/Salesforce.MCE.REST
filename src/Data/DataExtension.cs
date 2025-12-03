using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Yokinsoft.Salesforce.MCE
{
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
        public class Item
        {
            // Keys are typically simple string key/value pairs
            public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();

            // Values can contain strings or date strings; keep as string dictionary for simplicity
            public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();

            internal Dictionary<string, string> Flatten()
                => Keys.Concat(Values).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
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
                        case "number": return typeof(decimal);
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
