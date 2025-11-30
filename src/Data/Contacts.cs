using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Yokoins.Salesforce.MC;

namespace Yokinsoft.Salesforce.MCE
{
    public class SchemaItemListContainer : PageableListContainer<SchemaItem>
    {
        [JsonPropertyName("links")]
        public SchemaLinks Links { get; set; }
        [JsonPropertyName("requestServiceMessageID")]
        public string RequestServiceMessageID { get; set; }
        [JsonPropertyName("resultMessages")]
        public List<ResultMessage> ResultMessages { get; set; } = new List<ResultMessage>();
        [JsonPropertyName("serviceMessageID")]
        public string ServiceMessageID { get; set; }
    }

    public class SchemaItem
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("enterpriseID")]
        public int EnterpriseId { get; set; }

        [JsonPropertyName("availableBusinessUnits")]
        public List<int> AvailableBusinessUnits { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("schemaType")]
        public string SchemaType { get; set; }

        [JsonPropertyName("links")]
        public SchemaLinks Links { get; set; }
    }

    public class SchemaLinks
    {
        [JsonPropertyName("self")]
        public LinkHref Self { get; set; }

        [JsonPropertyName("attributeGroups")]
        public LinkHref AttributeGroups { get; set; }

        [JsonPropertyName("attributeSetDefinitions")]
        public LinkHref AttributeSetDefinitions { get; set; }
    }

    public class LinkHref
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }
    public class ContactAttributeSet
    {
        public class ValueItem
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }
        public class Item
        {
            [JsonPropertyName("values")]
            public List<ValueItem> Values { get; set; }
            
        }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }
    public class ContactUpdateResult
    {
        [JsonPropertyName("operationStatus")]
        public string OperationStatus { get; set; }

        [JsonPropertyName("rowsAffected")]
        public int RowsAffected { get; set; }

        [JsonPropertyName("contactKey")]
        public string ContactKey { get; set; }

        [JsonPropertyName("contactId")]
        public int ContactId { get; set; }

        [JsonPropertyName("contactTypeID")]
        public int ContactTypeId { get; set; }

        [JsonPropertyName("isNewContactKey")]
        public bool IsNewContactKey { get; set; }

        [JsonPropertyName("requestServiceMessageID")]
        public string RequestServiceMessageID { get; set; }
        [JsonPropertyName("responseDateTime")]
        public DateTime? ResponseDateTime { get; set; }

        [JsonPropertyName("hasErrors")]
        public bool HasErrors { get; set; }

        [JsonPropertyName("resultMessages")]
        public List<ResultMessageWithFormat> ResultMessages { get; set; } = new List<ResultMessageWithFormat>();

        [JsonPropertyName("serviceMessageID")]
        public string ServiceMessageID { get; set; }


    }
}
