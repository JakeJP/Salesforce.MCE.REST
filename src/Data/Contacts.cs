using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yokinsoft.Salesforce.MCE
{
    public class SchemaItemListContainer : PageableListContainer<SchemaItem>
    {
        public SchemaLinks Links { get; set; }
        public string RequestServiceMessageID { get; set; }
        public List<ResultMessage> ResultMessages { get; set; } = new List<ResultMessage>();
        public string ServiceMessageID { get; set; }
    }

    public class SchemaItem
    {
        public string Id { get; set; }

        [JsonPropertyName("enterpriseID")]
        public int EnterpriseId { get; set; }

        public List<int> AvailableBusinessUnits { get; set; }

        public int Version { get; set; }

        public string SchemaType { get; set; }

        public SchemaLinks Links { get; set; }
    }

    public class SchemaLinks
    {
        public LinkHref Self { get; set; }

        public LinkHref AttributeGroups { get; set; }

        public LinkHref AttributeSetDefinitions { get; set; }
    }

    public class LinkHref
    {
        public string Href { get; set; }
    }
    public class ContactAttributeSet
    {
        public class NameValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }
        public class Item
        {
            public List<NameValue> Values { get; set; }

        }
        public string Name { get; set; }
        public List<Item> Items { get; set; }
    }
    public class ContactsOperationStatusResponse
    {
        public string OperationStatus { get; set; }

        public int RowsAffected { get; set; }

        public string ContactKey { get; set; }

        public int ContactId { get; set; }

        [JsonPropertyName("contactTypeID")]
        public int ContactTypeId { get; set; }

        public bool IsNewContactKey { get; set; }

        public string RequestServiceMessageID { get; set; }
        public DateTime? ResponseDateTime { get; set; }

        public bool HasErrors { get; set; }

        public List<ResultMessageWithFormat> ResultMessages { get; set; } = new List<ResultMessageWithFormat>();

        public string ServiceMessageID { get; set; }


    }

    public class ContactsOperationResponse
    {
        public bool OperationInitiated { get; set; }
        public long OperationID { get; set; }
        public string RequestServiceMessageID { get; set; }
        public List<ResultMessage> ResultMessages { get; set; }
        public string ServiceMessageID { get; set; }
    }
    public class ContactsOperationStatus
    {
        public ContactsOperation Operation { get; set; }
        public string RequestServiceMessageID { get; set; }
        public List<ResultMessage> ResultMessages { get; set; }
        public string ServiceMessageID { get; set; }
    }
    public class ContactsOperation
    {
        [JsonPropertyName("listTypeID")]
        public int ListTypeId { get; set; }

        public string ListIdentifier { get; set; }

        public string ListKey { get; set; }

        public int ExpectedListCount { get; set; }

        public string DeleteType { get; set; }

        public bool DeleteListOnCompleted { get; set; }

        public long OperationID { get; set; }

        public int EID { get; set; }

        public int MID { get; set; }

        public int EmployeeID { get; set; }

        public string OperationRequestID { get; set; }

        public string Status { get; set; }

        public DateTime? ScheduledTime { get; set; }

        public int RetryCount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }
    }
    public class ContactKeyFromEmailAddressResult
    {
        public class Entity
        {
            public List<ContactKeyDetail> ContactKeyDetails = new List<ContactKeyDetail>();
            public string ChannelAddress { get; set; }
        }
        public class ContactKeyDetail
        {
            public string ContactKey { get; set; }
            public DateTime? CreateDate { get; set; }
        }
        public List<Entity> ChannelAddressResponseEntities = new List<Entity>();
        public string RequestServiceMessageID { get; set; }
        public DateTime? ResponseDateTime { get; set; }
        public List<ResultMessage> ResultMessages { get; set; } = new List<ResultMessage>();
        public string ServiceMessageID { get; set; }
    }
}
