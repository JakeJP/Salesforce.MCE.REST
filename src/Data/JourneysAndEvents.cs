using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Yokinsosft.Salesforce.MCE.Data
{
    public class DiscoveryResult
    {
        [JsonPropertyName("methods")]
        public Dictionary<string, DiscoveredMethod> Methods { get; set; }
    }
    public class DiscoveredMethod
    {
        public class Parameter { }
        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("httpMethod")]
        public string HttpMethod { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("parameters")]
        public List<object> Parameters = new List<object>();
    }
    public class FireEventResult
    {
        [JsonPropertyName("eventInstanceId")]
        public string EventInstanceId { get; set; }
        //
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("errorCode")]
        public long? ErrorCode { get; set; }
        [JsonPropertyName("documentation")]
        public string Documentation { get; set; }
    }
}
