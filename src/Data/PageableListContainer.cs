using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yokoins.Salesforce.MC
{
    public class PageableListContainer<TItem>
    {
        [JsonPropertyName("count")]
        public long Count { get; set; }
        [JsonPropertyName("page")]
        public long Page { get; set; }
        [JsonPropertyName("pageSize")]
        public long PageSize { get; set; }
        [JsonPropertyName("items")]
        public List<TItem> Items { get; set; } = new List<TItem>();
    }
}
