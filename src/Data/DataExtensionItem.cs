using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yokins.Salesforce.MCE
{
    public class DataExtensionItem
    {
        // Keys are typically simple string key/value pairs
        [JsonPropertyName("keys")]
        public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();

        // Values can contain strings or date strings; keep as string dictionary for simplicity
        [JsonPropertyName("values")]
        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
    }
}
