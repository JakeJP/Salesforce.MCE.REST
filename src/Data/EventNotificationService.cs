using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Yokins.Salesforce.MCE
{
    #region Subscription
    public class EventNotificationSubscriptionToCreate
    {
        [JsonPropertyName("callbackId")]
        public string CallbackId { get; set; }
        [JsonPropertyName("subscriptionName")]
        public string SubscriptionName { get; set; }
        [JsonPropertyName("eventCategoryTypes")]
        public List<string> EventCategoryTypes { get; set; } = new List<string>();
        [JsonPropertyName("filters")]
        public List<string> Filters { get; set; } = new List<string>();
    }
    public class EventNotificationSubscriptionCreated : EventNotificationSubscriptionToCreate
    {
        [JsonPropertyName("callbackName")]
        public string CallbackName { get; set; }
        [JsonPropertyName("subscriptionId")]
        public string SubscriptionId { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
    public class EventNotificationSubscription : EventNotificationSubscriptionCreated
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("maxBatchSize")]
        public int? MaxBatchSize { get; set; }
        [JsonPropertyName("statusReason")]
        public string StatusReason { get; set; }
    }
    #endregion
    public class EventNotificationCreateCallbackResult
    {
        [JsonPropertyName("callbackName")]
        public string CallbackName { get; set; }
        [JsonPropertyName("callbackId")]
        public string CallbackId { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("signatureKey")]
        public string SignatureKey { get; set; }
    }
    public abstract class EventNotificationCallbackBase
    {
        [JsonPropertyName("callbackId")]
        public string CallbackId { get; set; }

        [JsonPropertyName("callbackName")]
        public string CallbackName { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
    public class EventNotificationCallbackSignature : EventNotificationCallbackBase
    {
        [JsonPropertyName("signatureKey")]
        public string SignatureKey { get; set; }
    }
    public class EventNotificationCallback : EventNotificationCallbackBase
    {
        [JsonPropertyName("maxBatchSize")]
        public int? MaxBatchSize { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("statusReason")]
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
        [JsonPropertyName("eventCategoryType")]
        public string EventCategoryType { get; set; }

        // milliseconds since epoch
        [JsonPropertyName("timestampUTC")]
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

        [JsonPropertyName("compositeId")]
        public string CompositeId { get; set; }

        [JsonPropertyName("definitionKey")]
        public string DefinitionKey { get; set; }

        [JsonPropertyName("definitionId")]
        public string DefinitionId { get; set; }

        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        [JsonPropertyName("mid")]
        public long? Mid { get; set; }

        [JsonPropertyName("eid")]
        public long? Eid { get; set; }

        [JsonPropertyName("composite")]
        public CompositeInfo Composite { get; set; }

        [JsonPropertyName("info")]
        public TInfo Info { get; set; }

        public class CompositeInfo
        {
            [JsonPropertyName("jobId")]
            public string JobId { get; set; }

            [JsonPropertyName("batchId")]
            public string BatchId { get; set; }

            [JsonPropertyName("listId")]
            public string ListId { get; set; }

            [JsonPropertyName("subscriberId")]
            public string SubscriberId { get; set; }
        }
    }
    public class EmailOpenInfo
    {
        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; }

        [JsonPropertyName("userAgent")]
        public string UserAgent { get; set; }

        [JsonPropertyName("location")]
        public object Location { get; set; }
    }
    public class EmailClickInfo : EmailOpenInfo
    {
        [JsonPropertyName("jobUrlId")]
        public string JobUrlId { get; set; }
        [JsonPropertyName("contentLink")]
        public string ContentLink { get; set; }
        [JsonPropertyName("impressionRegion")]
        public string ImpressionRegion { get; set; }

    }
    public class EmailUnsubscribeInfo
    {
        [JsonPropertyName("to")]
        public string To { get; set; }
        [JsonPropertyName("domain")]
        public string Domain { get; set; }
        [JsonPropertyName("unsubscribeDate")]
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

        [JsonPropertyName("unsubscribeMethod")]
        public string UnsubscribeMethod { get; set; }
    }
    public class Location
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("region")]
        public string Region { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> AdditionalData { get; set; }
    }
}
