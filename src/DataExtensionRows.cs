using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Yokoins.Salesforce.MC;

namespace Yokinsoft.Salesforce.MCE
{
    public class DataExtensionRows : APIClientBase
    {
        public DataExtensionRows(AccessToken accessToken) : base(accessToken)
        {
        }
        #region Asynchronous

        public class RequestStatus
        {
            [JsonPropertyName("status")]
            public StatusDetail Status { get; set; }

            [JsonPropertyName("requestId")]
            public string RequestId { get; set; }

            [JsonPropertyName("resultMessages")]
            public List<string> ResultMessages { get; set; } = new List<string>();
        }

        public class StatusDetail
        {
            [JsonPropertyName("callDateTime")]
            public DateTime? CallDateTime { get; set; }

            [JsonPropertyName("completionDateTime")]
            public DateTime? CompletionDateTime { get; set; }

            [JsonPropertyName("hasErrors")]
            public bool? HasErrors { get; set; }

            [JsonPropertyName("pickupDateTime")]
            public DateTime? PickupDateTime { get; set; }

            [JsonPropertyName("requestStatus")]
            public string RequestStatusText { get; set; }

            [JsonPropertyName("resultStatus")]
            public string ResultStatus { get; set; }

            [JsonPropertyName("requestId")]
            public string RequestId { get; set; }
        }

        public IList<ResultMessage> GetRequestResults(string requestId)
        {
            // /data/v1/async/{requestId}/results
            return Get<PageableListContainer<ResultMessage>>($"/data/v1/async/{requestId}/results")
                .Items;
        }
        public RequestStatus GetRequestStatus(string requestId)
        {
            // /data/v1/async/{requestId}/status
            return Get<RequestStatus>($"/data/v1/async/{requestId}/status");
        }
        public Result InsertRows<TItem>(string externalKey, IEnumerable<TItem> items)
        {
            return Post<Result>($"/data/v1/async/dataextensions/key:{externalKey}/rows", new { items });
        }
        public Result UpsertRows<TItem>(string externalKey, IEnumerable<TItem> items)
        {
            return Put<Result>($"/data/v1/async/dataextensions/key:{externalKey}/rows", new { items });
            ;
        }
        #endregion
        public DataExtensionItem UpsertRow(string dataExtensionId, DataExtensionItem item)
        {
            return Put<DataExtensionItem>($"/hub/v1/dataevents/{Uri.EscapeDataString(dataExtensionId)}/rows/{string.Join(",", item.Keys.Select(kv => Uri.EscapeDataString(kv.Key) + ":" + Uri.EscapeDataString(kv.Value)))}", new { values = item.Values });
        }
        public DataExtensionItem UpsertRowByKey(string dataExtensionKey, DataExtensionItem item)
        {
            return Put<DataExtensionItem>($"/hub/v1/dataevents/key:{Uri.EscapeDataString(dataExtensionKey)}/rows/{string.Join(",", item.Keys.Select(kv => Uri.EscapeDataString(kv.Key) + ":" + Uri.EscapeDataString(kv.Value)))}", new { values = item.Values });
        }
        public void IncrementColumnValue(string dataExtensionId, string primaryKey, string primaryKeyValue, string column, int? step = null)
        {
            Put<object>($"/hub/v1/dataevents/{Uri.EscapeDataString(dataExtensionId)}/rows/{Uri.EscapeDataString(primaryKey)}:{Uri.EscapeDataString(primaryKeyValue)}/column/{Uri.EscapeDataString(column)}/increment"
                + (step != null ? "?step=" + step.ToString() : ""));
        }
        public void IncrementColumnValueByKey(string externalKey, string primaryKey, string primaryKeyValue, string column, int? step = null)
        {
            Put<object>($"/hub/v1/dataevents/key:{Uri.EscapeDataString(externalKey)}/rows/{Uri.EscapeDataString(primaryKey)}:{Uri.EscapeDataString(primaryKeyValue)}/column/{Uri.EscapeDataString(column)}/increment"
                + (step != null ? "?step=" + step.ToString() : ""));
        }
        const int maximumRowsSynchronous = 50;
        const int maximumRowsAsynchronous = 5000;
        public List<DataExtensionItem> UpsertRowSet(string dataExtensionId, IEnumerable<DataExtensionItem> items)
        {
            return Post<List<DataExtensionItem>>($"/hub/v1/dataevents/{Uri.EscapeDataString(dataExtensionId)}/rowset", items);
        }
        public List<DataExtensionItem> UpsertRowSetByKey(string dataExtensionKey, IEnumerable<DataExtensionItem> items)
        {
            return Post<List<DataExtensionItem>>($"/hub/v1/dataevents/key:{Uri.EscapeDataString(dataExtensionKey)}/rowset", items);
        }
    }
}
