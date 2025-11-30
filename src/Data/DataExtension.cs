using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yokins.Salesforce.MCE
{
    public abstract class DataExtensionCommon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }

        [JsonPropertyName("isSendable")]
        public bool? IsSendable { get; set; }

        [JsonPropertyName("sendableCustomObjectField")]
        public string SendableCustomObjectField { get; set; }

        [JsonPropertyName("sendableSubscriberField")]
        public string SendableSubscriberField { get; set; }

        [JsonPropertyName("isTestable")]
        public bool? IsTestable { get; set; }

        [JsonPropertyName("categoryId")]
        public long? CategoryId { get; set; }

        [JsonPropertyName("ownerId")]
        public long? OwnerId { get; set; }

        [JsonPropertyName("isObjectDeletable")]
        public bool? IsObjectDeletable { get; set; }

        [JsonPropertyName("isFieldAdditionAllowed")]
        public bool? IsFieldAdditionAllowed { get; set; }

        [JsonPropertyName("isFieldModificationAllowed")]
        public bool? IsFieldModificationAllowed { get; set; }

        [JsonPropertyName("createdDate")]
        public DateTime? CreatedDate { get; set; }

        [JsonPropertyName("createdById")]
        public long? CreatedById { get; set; }

        [JsonPropertyName("createdByName")]
        public string CreatedByName { get; set; }

        [JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [JsonPropertyName("modifiedById")]
        public long? ModifiedById { get; set; }

        [JsonPropertyName("modifiedByName")]
        public string ModifiedByName { get; set; }

        [JsonPropertyName("ownerName")]
        public string OwnerName { get; set; }

        [JsonPropertyName("partnerApiObjectTypeId")]
        public int? PartnerApiObjectTypeId { get; set; }

        [JsonPropertyName("partnerApiObjectTypeName")]
        public string PartnerApiObjectTypeName { get; set; }

        [JsonPropertyName("rowCount")]
        public long? RowCount { get; set; }

        [JsonPropertyName("dataRetentionProperties")]
        public DataRetentionProperties DataRetentionProperties { get; set; }
    }
    public class DataExtensionToCreate : DataExtensionCommon
    {
        [JsonPropertyName("fields")]
        public List<DataExtensionFieldToCreate> Fields { get; set; } = new List<DataExtensionFieldToCreate>();
    }
    public class DataExtension : DataExtensionCommon
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("fieldCount")]
        public int? FieldCount { get; set; }

    }

    public class DataRetentionProperties
    {
        [JsonPropertyName("isDeleteAtEndOfRetentionPeriod")]
        public bool? IsDeleteAtEndOfRetentionPeriod { get; set; }

        [JsonPropertyName("isRowBasedRetention")]
        public bool? IsRowBasedRetention { get; set; }

        [JsonPropertyName("isResetRetentionPeriodOnImport")]
        public bool? IsResetRetentionPeriodOnImport { get; set; }
    }
    public class DataExtensionFieldToCreate
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        // Field "type" in JSON (e.g. "Number", "Text", "Date")
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("maskType")]
        public string MaskType { get; set; }

        [JsonPropertyName("storageType")]
        public string StorageType { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("ordinal")]
        public int? Ordinal { get; set; }

        [JsonPropertyName("isNullable")]
        public bool? IsNullable { get; set; }

        [JsonPropertyName("isPrimaryKey")]
        public bool? IsPrimaryKey { get; set; }

        [JsonPropertyName("isTemplateField")]
        public bool? IsTemplateField { get; set; }

        [JsonPropertyName("isInheritable")]
        public bool? IsInheritable { get; set; }

        [JsonPropertyName("isOverridable")]
        public bool? IsOverridable { get; set; }

        [JsonPropertyName("isHidden")]
        public bool? IsHidden { get; set; }

        [JsonPropertyName("isReadOnly")]
        public bool? IsReadOnly { get; set; }

        [JsonPropertyName("mustOverride")]
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
                };
                return typeof(string);
            }
        }
    }
    public class DataExtensionField : DataExtensionFieldToCreate
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
