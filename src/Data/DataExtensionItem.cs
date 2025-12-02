using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Yokinsoft.Salesforce.MCE
{
    public class DataExtensionItem
    {
        // Keys are typically simple string key/value pairs
        [JsonPropertyName("keys")]
        public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();

        // Values can contain strings or date strings; keep as string dictionary for simplicity
        [JsonPropertyName("values")]
        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();

        internal Dictionary<string,string> Flatten()
            => Keys.Concat(Values).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
