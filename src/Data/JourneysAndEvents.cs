using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Yokinsoft.Salesforce.MCE
{
    public class DiscoveryResult
    {
        public Dictionary<string, DiscoveredMethod> Methods { get; set; }
    }
    public class DiscoveredMethod
    {
        public class Parameter { }
        public string Path { get; set; }
        public string HttpMethod { get; set; }
        public string Description { get; set; }
        public List<object> Parameters = new List<object>();
    }
    public class FireEventResult
    {
        public string EventInstanceId { get; set; }
        public string Message { get; set; }
        public long? ErrorCode { get; set; }
        public string Documentation { get; set; }
    }
}
