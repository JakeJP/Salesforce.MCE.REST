using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;
using Yokoins.Salesforce.MC;

namespace Yokins.Salesforce.MCE
{
    public class DataExtensionItemListContainer : PageableListContainer<DataExtensionItem>
    {
        [JsonPropertyName("links")]
        public Links Links { get; set; }

        [JsonPropertyName("requestToken")]
        public string RequestToken { get; set; }

        [JsonPropertyName("tokenExpireDateUtc")]
        public DateTime? TokenExpireDateUtc { get; set; }

        [JsonPropertyName("customObjectId")]
        public string CustomObjectId { get; set; }

        [JsonPropertyName("customObjectKey")]
        public string CustomObjectKey { get; set; }

        [JsonPropertyName("top")]
        public int Top { get; set; }

        public IEnumerable<DataExtensionItem> AsEnumerable()
        {
            var currentContainer = this;
            while (currentContainer != null)
            {
                foreach (var item in currentContainer.Items)
                    yield return item;
                if (currentContainer.Links.Next != null)
                {
                    currentContainer = GetNext();
                }
                else
                {
                    currentContainer = null;
                }
            }
        }

        public IDataReader AsDataReader() => new DataExtensionDataReader(this);

        internal Func<DataExtensionItemListContainer> GetNext = null;
        internal Func<DataExtensionItemListContainer> GetPrev = null;
        internal Func<IList<DataExtensionField>> GetFields = null;


        /// <summary>
        /// IDataReader implementation that exposes Items as rows and columns are the union of Keys and Values names.
        /// Column order: first occurrence order across Items (keys first then values per item).
        /// Fields that appear in any item's Keys dictionary are marked as primary keys in the schema.
        /// </summary>
        private class DataExtensionDataReader : IDataReader
        {
            private List<DataExtensionItem> _items;
            private readonly List<string> _fieldNames;
            private readonly HashSet<string> _keyFieldSet;
            private readonly Dictionary<string, int> _nameToOrdinal;
            private IDictionary<string, DataExtensionField> _defields;
            private int _rowIndex = -1;
            private bool _closed;
            private Func<DataExtensionItemListContainer> _getNext;
            private Func<DataExtensionItemListContainer> _getPrev;
            public DataExtensionDataReader(DataExtensionItemListContainer container)
            {
                _items = container?.Items ?? new List<DataExtensionItem>();
                _fieldNames = new List<string>();
                _nameToOrdinal = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                _keyFieldSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                _defields = container?.GetFields?.Invoke().ToDictionary(m => m.Name, m => m, StringComparer.OrdinalIgnoreCase);

                // Build field list by first occurrence: keys then values per item.
                // Track names that come from Keys as primary key fields.
                foreach (var item in _items)
                {
                    if (item?.Keys != null)
                    {
                        foreach (var k in item.Keys.Keys)
                            AddFieldIfMissing(k, isKey: true);
                    }

                    if (item?.Values != null)
                    {
                        foreach (var v in item.Values.Keys)
                            AddFieldIfMissing(v, isKey: false);
                    }
                }
                _getNext = container?.GetNext;
                _getPrev = container?.GetPrev;
            }

            private void AddFieldIfMissing(string name, bool isKey)
            {
                if (string.IsNullOrEmpty(name))
                    return;
                if (!_nameToOrdinal.ContainsKey(name))
                {
                    _nameToOrdinal[name] = _fieldNames.Count;
                    _fieldNames.Add(name);
                }
                if (isKey)
                    _keyFieldSet.Add(name);
            }

            public int FieldCount => _fieldNames.Count;

            private object GetRawValue(int i)
            {
                if (_rowIndex < 0 || _rowIndex >= _items.Count)
                    throw new InvalidOperationException("No current row. Call Read() first.");

                if (i < 0 || i >= _fieldNames.Count)
                    throw new IndexOutOfRangeException(nameof(i));

                var name = _fieldNames[i];
                var row = _items[_rowIndex];

                // Prefer Keys (primary key) values if present, otherwise Values
                if (row.Keys != null && row.Keys.TryGetValue(name, out var v1))
                    return v1;
                if (row.Values != null && row.Values.TryGetValue(name, out var v2))
                    return v2;

                return null;
            }
            public object GetValue(int i)
            {
                var v = GetRawValue(i);

                if (v == null || (v is string s && string.IsNullOrEmpty(s)))
                    return DBNull.Value;
                return v;
            }

            public bool Read()
            {
                if (_closed) return false;
                if (_rowIndex + 1 >= _items.Count)
                {
                    if (_getNext != null)
                    {
                        // Attempt to get next page of data
                        var currentContainer = _getNext.Invoke();
                        if (currentContainer != null && currentContainer.Items != null && currentContainer.Items.Count > 0)
                        {
                            _items = currentContainer.Items;
                            _rowIndex = 0;
                            _getNext = currentContainer.GetNext;
                            _getPrev = currentContainer.GetPrev;
                            return true;
                        }
                    }
                    return false;
                }
                _rowIndex++;
                return true;
            }

            public int GetOrdinal(string name)
            {
                if (name == null) throw new ArgumentNullException(nameof(name));
                if (_nameToOrdinal.TryGetValue(name, out var ord)) return ord;
                throw new IndexOutOfRangeException($"Column '{name}' does not exist.");
            }

            public string GetName(int i)
            {
                if (i < 0 || i >= _fieldNames.Count) throw new IndexOutOfRangeException(nameof(i));
                return _defields?.ContainsKey(_fieldNames[i]) == true ? _defields[_fieldNames[i]].Name : _fieldNames[i];
            }

            public bool IsDBNull(int i)
            {
                var val = GetRawValue(i);
                return val == null || val == DBNull.Value || (val is string s && s.Length == 0);
            }

            public int GetValues(object[] values)
            {
                if (values == null) throw new ArgumentNullException(nameof(values));
                var toCopy = Math.Min(values.Length, FieldCount);
                for (int i = 0; i < toCopy; i++)
                    values[i] = GetValue(i);
                return toCopy;
            }

            // Basic typed getters attempt conversion from string where appropriate
            public string GetString(int i) => ConvertTo<string>(i, s => s);

            public int GetInt32(int i) => ConvertTo<int>(i, s => int.Parse(s, CultureInfo.InvariantCulture));

            public long GetInt64(int i) => ConvertTo<long>(i, s => long.Parse(s, CultureInfo.InvariantCulture));

            public short GetInt16(int i) => ConvertTo<short>(i, s => short.Parse(s, CultureInfo.InvariantCulture));

            public bool GetBoolean(int i) => ConvertTo<bool>(i, s => bool.Parse(s));

            public DateTime GetDateTime(int i) => ConvertTo<DateTime>(i, s => DateTime.Parse(s, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal));

            public decimal GetDecimal(int i) => ConvertTo<decimal>(i, s => decimal.Parse(s, CultureInfo.InvariantCulture));

            public double GetDouble(int i) => ConvertTo<double>(i, s => double.Parse(s, CultureInfo.InvariantCulture));

            public float GetFloat(int i) => ConvertTo<float>(i, s => float.Parse(s, CultureInfo.InvariantCulture));

            public Guid GetGuid(int i) => ConvertTo<Guid>(i, s => Guid.Parse(s));

            private T ConvertTo<T>(int i, Func<string, T> parser)
            {
                var val = GetValue(i);
                if (val == DBNull.Value) throw new InvalidCastException("Value is DBNull");
                if (val is T t) return t;
                if (val is string s)
                {
                    try { return parser(s); }
                    catch (Exception ex) { throw new InvalidCastException($"Failed to convert column '{GetName(i)}' value '{s}' to {typeof(T).Name}.", ex); }
                }
                try { return (T)Convert.ChangeType(val, typeof(T), CultureInfo.InvariantCulture); }
                catch (Exception ex) { throw new InvalidCastException($"Failed to convert column '{GetName(i)}' to {typeof(T).Name}.", ex); }
            }

            #region IDataRecord members with default/simple implementations

            public Type GetFieldType(int i)
            {
                if (_defields != null && _defields.TryGetValue(_fieldNames[i], out var defield))
                {
                    return defield.DataType;
                }
                return typeof(string);
            }

            public string GetDataTypeName(int i) => "string";

            public object this[int i] => GetValue(i);

            public object this[string name] => GetValue(GetOrdinal(name));

            public int GetOrdinalSafe(string name) => GetOrdinal(name);

            public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => throw new NotSupportedException();

            public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => throw new NotSupportedException();

            public IDataReader GetData(int i) => throw new NotSupportedException();

            public bool NextResult() => false;

            public int Depth => 0;

            public bool IsClosed => _closed;

            public int RecordsAffected => -1;

            public void Close() => _closed = true;

            public void Dispose()
            {
                Close();
            }

            public DataTable GetSchemaTable()
            {
                var table = new DataTable("SchemaTable");
                // Standard schema columns
                table.Columns.Add("ColumnName", typeof(string));
                table.Columns.Add("ColumnOrdinal", typeof(int));
                table.Columns.Add("ColumnSize", typeof(long));
                table.Columns.Add("DataType", typeof(Type));
                table.Columns.Add("AllowDBNull", typeof(bool));
                // Mark primary key columns
                table.Columns.Add("IsKey", typeof(bool));
                table.Columns.Add("IsUnique", typeof(bool));

                for (int i = 0; i < _fieldNames.Count; i++)
                {
                    var row = table.NewRow();
                    var name = _defields?.ContainsKey(_fieldNames[i]) == true ? _defields[_fieldNames[i]].Name : _fieldNames[i];
                    var isKey = _keyFieldSet.Contains(name);

                    row["ColumnName"] = name;
                    row["ColumnOrdinal"] = i;
                    row["ColumnSize"] = int.MaxValue;
                    row["DataType"] = _defields?.ContainsKey(_fieldNames[i]) == true ? _defields[_fieldNames[i]].DataType : typeof(string);
                    row["AllowDBNull"] = !isKey; // primary key fields marked non-nullable in schema
                    row["IsKey"] = isKey;
                    row["IsUnique"] = isKey; // keys are typically unique
                    table.Rows.Add(row);
                }

                return table;
            }

            public int GetSchemaOrdinal(string name) => GetOrdinal(name);

            // Unused typed getters (not required often) - route to generic conversions
            public byte GetByte(int i) => ConvertTo<byte>(i, s => byte.Parse(s, CultureInfo.InvariantCulture));
            public char GetChar(int i) => ConvertTo<char>(i, s => char.Parse(s));
            public bool GetBooleanSafe(int i) => GetBoolean(i);
            #endregion
        }
    }

    public class Links
    {
        [JsonPropertyName("previous")]
        public string Previous { get; set; }

        [JsonPropertyName("self")]
        public string Self { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }
    }


}
