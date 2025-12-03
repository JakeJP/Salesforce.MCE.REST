using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;

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
        public class RequestResult : PageableListContainer<RequestResult.Item>
        {
            public class Item
            {
                [JsonPropertyName("message")]
                public string Message { get; set; }
                [JsonPropertyName("status")]
                public string Status { get; set; }
                [JsonPropertyName("errorCode")]
                public long ErrorCoee   { get; set; }
            }
            [JsonPropertyName("requestId")]
            public string RequestId { get; set; }
            [JsonPropertyName("resultMessages")]
            public List<ResultMessage> ResultMessages { get; set; } = new List<ResultMessage>();
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
        /// <summary>
        /// Number of items should not exceed 5000.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="externalKey"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public Result InsertRows<TItem>(string externalKey, IEnumerable<TItem> items)
        {
            return Post<Result>($"/data/v1/async/dataextensions/key:{externalKey}/rows", new { items });
        }
        /// <summary>
        /// Number of items should not exceed 5000.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="externalKey"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public Result UpsertRows<TItem>(string externalKey, IEnumerable<TItem> items)
        {
            return Put<Result>($"/data/v1/async/dataextensions/key:{externalKey}/rows", new { items });
        }
        public IList<Result> InsertRows( string externalKey, IDataReader dataReader )
        {
            var items = DataReaderToDataExetnsionItems(dataReader).Select( m => m.Flatten()).ToList();
            var total = items.Count;
            List<Result> results = new List<Result>();
            for (var i = 0; i < total; i += maximumRowsAsynchronous)
            {
                var batch = items.Skip(i).Take(Math.Min(maximumRowsAsynchronous, total - i)).ToList();
                var res = InsertRows(externalKey, batch);
                results.Add(res);
            }
            return results;
        }
        public IList<Result> UpsertRows(string externalKey, IDataReader dataReader)
        {
            var items = DataReaderToDataExetnsionItems(dataReader).Select(m => m.Flatten()).ToList();
            var total = items.Count;
            List<Result> results = new List<Result>();
            for (var i = 0; i < total; i += maximumRowsAsynchronous)
            {
                var batch = items.Skip(i).Take(Math.Min(maximumRowsAsynchronous, total - i)).ToList();
                var res = UpsertRows(externalKey, batch);
                results.Add(res);
            }
            return results;
        }
        #endregion
        #region Synchronous
        public DataExtension.Item UpsertRow(string dataExtensionId, DataExtension.Item item)
        {
            return Put<DataExtension.Item>($"/hub/v1/dataevents/{Uri.EscapeDataString(dataExtensionId)}/rows/{string.Join(",", item.Keys.Select(kv => Uri.EscapeDataString(kv.Key) + ":" + Uri.EscapeDataString(kv.Value)))}", new { values = item.Values });
        }
        public DataExtension.Item UpsertRowByKey(string dataExtensionKey, DataExtension.Item item)
        {
            return Put<DataExtension.Item>($"/hub/v1/dataevents/key:{Uri.EscapeDataString(dataExtensionKey)}/rows/{string.Join(",", item.Keys.Select(kv => Uri.EscapeDataString(kv.Key) + ":" + Uri.EscapeDataString(kv.Value)))}", new { values = item.Values });
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
        /// <summary>
        /// Synchronous version of UpsertRowSet that takes dataExtensionId as key. Number of items should not exceed 50.
        /// </summary>
        /// <param name="dataExtensionId"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public List<DataExtension.Item> UpsertRowSet(string dataExtensionId, IEnumerable<DataExtension.Item> items)
        {
            return Post<List<DataExtension.Item>>($"/hub/v1/dataevents/{Uri.EscapeDataString(dataExtensionId)}/rowset", items);
        }
        /// <summary>
        /// Synchronous version of UpsertRowSet that takes dataExtensionKey as key. Number of items should not exceed 50.
        /// </summary>
        /// <param name="dataExtensionKey"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public List<DataExtension.Item> UpsertRowSetByKey(string dataExtensionKey, IEnumerable<DataExtension.Item> items)
        {
            return Post<List<DataExtension.Item>>($"/hub/v1/dataevents/key:{Uri.EscapeDataString(dataExtensionKey)}/rowset", items);
        }
        #endregion

        #region LINQ, IDataReader helpers
        // .NET Data extensions
        /// <summary>
        /// Synchronous version of UpsertRowSet that takes an IDataReader as input. Number of records in dataReader can exceed 50;
        /// the method will batch them as needed.
        /// </summary>
        /// <param name="dataExtensionId"></param>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public List<DataExtension.Item> UpsertRowSet(string dataExtensionId, IDataReader dataReader )
        {
            var url = $"/hub/v1/dataevents/{Uri.EscapeDataString(dataExtensionId)}/rowset";
            var items = DataReaderToDataExetnsionItems(dataReader);
            var total = items.Count;
            List<DataExtension.Item> result = null;
            for( var i = 0; i< total; i += maximumRowsSynchronous)
            {
                var batch = items.Skip(i).Take(Math.Min(maximumRowsSynchronous, total - i)).ToList();
                var res = Post<List<DataExtension.Item>>(url, batch);
                if( result == null )
                    result = res;
                else
                    result.AddRange(res);
            }
            return result;
        }
        /// <summary>
        /// Synchronous version of UpsertRowSet that takes an IDataReader as input. Number of records in dataReader can exceed 50;
        /// the method will batch them as needed.
        /// </summary>
        /// <param name="dataExtensionKey"></param>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public List<DataExtension.Item> UpsertRowSetByKey(string dataExtensionKey, IDataReader dataReader)
        {
            var url = $"/hub/v1/dataevents/key:{Uri.EscapeDataString(dataExtensionKey)}/rowset";
            var items = DataReaderToDataExetnsionItems(dataReader);
            var total = items.Count;
            List<DataExtension.Item> result = null;
            for (var i = 0; i < total; i += maximumRowsSynchronous)
            {
                var batch = items.Skip(i).Take(Math.Min(maximumRowsSynchronous, total - i)).ToList();
                var res = Post<List<DataExtension.Item>>(url, batch);
                if (result == null)
                    result = res;
                else
                    result.AddRange(res);
            }
            return result;
        }

        List<DataExtension.Item> DataReaderToDataExetnsionItems( IDataReader dataReader )
        {
            if (dataReader == null) throw new ArgumentNullException(nameof(dataReader));

            var result = new List<DataExtension.Item>();

            // collect column names
            int fieldCount = dataReader.FieldCount;
            var columnNames = new string[fieldCount];
            for (int i = 0; i < fieldCount; i++)
                columnNames[i] = dataReader.GetName(i);

            // determine primary key columns from schema table when available
            var keyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                var schema = dataReader.GetSchemaTable();
                if (schema != null)
                {
                    foreach (DataRow row in schema.Rows)
                    {
                        if (schema.Columns.Contains("ColumnName") && schema.Columns.Contains("IsKey"))
                        {
                            var colName = row["ColumnName"] as string;
                            if (colName != null && row["IsKey"] is bool isKey && isKey)
                                keyColumns.Add(colName);
                        }
                    }
                }
            }
            catch
            {
                // ignore schema retrieval issues, fall back to heuristics below
            }

            // fallback heuristic if no key columns found: look for common key names
            if (keyColumns.Count == 0)
            {
                throw new InvalidOperationException("Unable to determine primary key columns from the provided DataReader. Please ensure that the DataReader's schema includes key column information.");
            }

            // Read rows
            while (dataReader.Read())
            {
                var item = new DataExtension.Item();
                item.Keys = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                item.Values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                for (int i = 0; i < fieldCount; i++)
                {
                    var colName = columnNames[i];
                    object raw;
                    try
                    {
                        raw = dataReader.IsDBNull(i) ? null : dataReader.GetValue(i);
                    }
                    catch
                    {
                        // If GetValue fails, treat as null
                        raw = null;
                    }

                    string strVal = raw == null ? null : (raw is string s ? s : Convert.ToString(raw, CultureInfo.InvariantCulture));

                    if (keyColumns.Contains(colName))
                    {
                        if (strVal != null)
                            item.Keys[colName] = strVal;
                    }
                    else
                    {
                        if (strVal != null)
                            item.Values[colName] = strVal;
                    }
                }

                result.Add(item);
            }

            return result;
        }
        #endregion
    }
}
