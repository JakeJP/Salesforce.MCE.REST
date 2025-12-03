using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Yokinsoft.Salesforce.MCE
{
    #region Subscription
    public class EventNotificationSubscriptionToCreate
    {
        public string CallbackId { get; set; }
        public string SubscriptionName { get; set; }
        public List<string> EventCategoryTypes { get; set; } = new List<string>();
        public List<string> Filters { get; set; } = new List<string>();
    }
    public class EventNotificationSubscriptionCreated : EventNotificationSubscriptionToCreate
    {
        public string CallbackName { get; set; }
        public string SubscriptionId { get; set; }
        public string Status { get; set; }
    }
    public class EventNotificationSubscription : EventNotificationSubscriptionCreated
    {
        public string Url { get; set; }

        public int? MaxBatchSize { get; set; }
        public string StatusReason { get; set; }
    }
    #endregion
    public class EventNotificationCreateCallbackResult
    {
        public string CallbackName { get; set; }
        public string CallbackId { get; set; }
        public string Url { get; set; }
        public string SignatureKey { get; set; }
    }
    public abstract class EventNotificationCallbackBase
    {
        public string CallbackId { get; set; }

        public string CallbackName { get; set; }

        public string Url { get; set; }
    }
    public class EventNotificationCallbackSignature : EventNotificationCallbackBase
    {
        public string SignatureKey { get; set; }
    }
    public class EventNotificationCallback : EventNotificationCallbackBase
    {
        public int? MaxBatchSize { get; set; }

        public string Status { get; set; }

        public string StatusReason { get; set; }
    }
    public class EngagementEventsEmailOpen : EngagementEventsCommon<EmailOpenInfo>
    {
    }
    public class EngagementEventsEmailClick : EngagementEventsCommon<EmailClickInfo>
    {
    }
    public class  EngagementEventsEmailUnsubscribe : EngagementEventsCommon<EmailUnsubscribeInfo>
    {
    }
    public abstract class EngagementEventsCommon<TInfo> where TInfo : class
    {
        public string EventCategoryType { get; set; }

        // milliseconds since epoch
        public long TimestampUTC { get; set; }

        [JsonIgnore]
        public DateTime Timestamp
        {
            get
            {
                try
                {
                    return DateTimeOffset.FromUnixTimeMilliseconds(TimestampUTC).UtcDateTime;
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string CompositeId { get; set; }

        public string DefinitionKey { get; set; }

        public string DefinitionId { get; set; }

        public string Channel { get; set; }

        public long? Mid { get; set; }

        public long? Eid { get; set; }

        public CompositeInfo Composite { get; set; }

        public TInfo Info { get; set; }

        public class CompositeInfo
        {
            public string JobId { get; set; }

            public string BatchId { get; set; }

            public string ListId { get; set; }

            public string SubscriberId { get; set; }
        }
    }
    public class EmailOpenInfo
    {
        public string IpAddress { get; set; }

        public string UserAgent { get; set; }

        public object Location { get; set; }
    }
    public class EmailClickInfo : EmailOpenInfo
    {
        public string JobUrlId { get; set; }
        public string ContentLink { get; set; }
        public string ImpressionRegion { get; set; }

    }
    public class EmailUnsubscribeInfo
    {
        public string To { get; set; }
        public string Domain { get; set; }
        public long UnsubscribeDate { get; set; }

        [JsonIgnore]
        public DateTime Timestamp
        {
            get
            {
                try
                {
                    return DateTimeOffset.FromUnixTimeMilliseconds(UnsubscribeDate).UtcDateTime;
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string UnsubscribeMethod { get; set; }
    }
    public class Location
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }

        public string PostalCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> AdditionalData { get; set; }
    }
}
