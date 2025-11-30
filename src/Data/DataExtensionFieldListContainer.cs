using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yokins.Salesforce.MCE
{
    public class DataExtensionFieldListContainer
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("fields")]
        public List<DataExtensionField> Fields { get; set; } = new List<DataExtensionField>();
        [JsonPropertyName("fieldCount")]
        public int? FieldCount { get; set; }
    }
}
