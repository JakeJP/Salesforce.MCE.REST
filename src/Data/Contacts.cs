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
        public class NameValue
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("value")]
            public object Value { get; set; }
        }
        public class Item
        {
            [JsonPropertyName("values")]
            public List<NameValue> Values { get; set; }

        }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }
    public class ContactsOperationStatusResponse
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
   
    public class ContactsOperationResponse
    {
        [JsonPropertyName("operationInitiated")]
        public bool OperationInitiated { get; set; }
        [JsonPropertyName("operationID")]
        public long OperationID { get; set; }
        [JsonPropertyName("requestServiceMessageID")]
        public string RequestServiceMessageID { get; set; }
        [JsonPropertyName("resultMessages")]
        public List<ResultMessage> ResultMessages { get; set; }
        [JsonPropertyName("serviceMessageID")]
        public string ServiceMessageID { get; set; }
    }
    public class ContactsOperationStatus
    {
        [JsonPropertyName("operation")]
        public ContactsOperation Operation { get; set; }
        [JsonPropertyName("requestServiceMessageID")]
        public string RequestServiceMessageID { get; set; }
        [JsonPropertyName("resultMessages")]
        public List<ResultMessage> ResultMessages { get; set; }
        [JsonPropertyName("serviceMessageID")]
        public string ServiceMessageID { get; set; }
    }
    public class ContactsOperation
    {
        [JsonPropertyName("listTypeID")]
        public int ListTypeId { get; set; }

        [JsonPropertyName("listIdentifier")]
        public string ListIdentifier { get; set; }

        [JsonPropertyName("listKey")]
        public string ListKey { get; set; }

        [JsonPropertyName("expectedListCount")]
        public int ExpectedListCount { get; set; }

        [JsonPropertyName("deleteType")]
        public string DeleteType { get; set; }

        [JsonPropertyName("deleteListOnCompleted")]
        public bool DeleteListOnCompleted { get; set; }

        [JsonPropertyName("operationID")]
        public long OperationID { get; set; }

        [JsonPropertyName("eID")]
        public int EID { get; set; }

        [JsonPropertyName("mID")]
        public int MID { get; set; }

        [JsonPropertyName("employeeID")]
        public int EmployeeID { get; set; }

        [JsonPropertyName("operationRequestID")]
        public string OperationRequestID { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("scheduledTime")]
        public DateTime? ScheduledTime { get; set; }

        [JsonPropertyName("retryCount")]
        public int RetryCount { get; set; }

        [JsonPropertyName("createdDate")]
        public DateTime? CreatedDate { get; set; }

        [JsonPropertyName("createdBy")]
        public int CreatedBy { get; set; }

        [JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [JsonPropertyName("modifiedBy")]
        public int ModifiedBy { get; set; }
    }
    public class ContactKeyFromEmailAddressResult
    {
        public class Entity
        {
            [JsonPropertyName("contactKeyDetails")]
            public List<ContactKeyDetail> ContactKeyDetails = new List<ContactKeyDetail>();
            [JsonPropertyName("channelAddress")]
            public string ChannelAddress { get; set; }
        }
        public class ContactKeyDetail
        {
            [JsonPropertyName("contactKey")]
            public string ContactKey { get; set; }
            [JsonPropertyName("createDate")]    
            public DateTime? CreateDate { get; set; }
        }
        [JsonPropertyName("channelAddressResponseEntities")]
        public List<Entity> ChannelAddressResponseEntities = new List<Entity>();
        [JsonPropertyName("requestServiceMessageID")]
        public string RequestServiceMessageID { get; set; }
        [JsonPropertyName("responseDateTime")]
        public DateTime? ResponseDateTime { get; set; }
        [JsonPropertyName("resultMessages")]
        public List<ResultMessage> ResultMessages { get; set; } = new List<ResultMessage>();
        [JsonPropertyName("serviceMessageID")]
        public string ServiceMessageID { get; set; }
    }
}
