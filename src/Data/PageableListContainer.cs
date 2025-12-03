using System.Collections.Generic;

namespace Yokoins.Salesforce.MC
{
    public class PageableListContainer<TItem>
    {
        public long Count { get; set; }
        public long Page { get; set; }
        public long PageSize { get; set; }
        public List<TItem> Items { get; set; } = new List<TItem>();
    }
}
