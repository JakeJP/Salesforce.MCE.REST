using System;
using System.Collections.Generic;

namespace Yokinsoft.Salesforce.MCE
{
    public class DataExtensionData : APIClientBase
    {
        public DataExtensionData(AccessToken accessToken) : base(accessToken)
        {
        }

        public DataExtensionItemListContainer GetData(string dataExtensionId, string filter = null, IEnumerable<string> fields = null, string orderBy = null, long top = -1, long offset = 0, long pagesize = 0)
        {
            var qs = new Dictionary<string, string>
            {
                {"$filter", filter },
                {"$fields", fields != null ? string.Join(",", fields) : null },
                {"$orderby", orderBy  },
                {"$top", top > 0 ? top.ToString() : null },
                {"$offset", offset > 0 ? offset.ToString() : null },
                {"$pagesize", pagesize > 0 ? pagesize.ToString() : null }
            };

            return GetDataByUrl($"/data/v1/customobjectdata/{Uri.EscapeDataString(dataExtensionId)}/rowset", qs);
        }
        public DataExtensionItemListContainer GetDataByKey(string key, string filter = null, IEnumerable<string> fields = null, string orderBy = null, long top = -1, long offset = 0, long pagesize = 0)
        {
            var qs = new Dictionary<string, string>
            {
                {"$filter", filter },
                {"$fields", fields != null ? string.Join(",", fields) : null },
                {"$orderby", orderBy  },
                {"$top", top > 0 ? top.ToString() : null },
                {"$offset", offset > 0 ? offset.ToString() : null },
                {"$pagesize", pagesize > 0 ? pagesize.ToString() : null }
            };

            return GetDataByUrl($"/data/v1/customobjectdata/key/{Uri.EscapeDataString(key)}/rowset", qs);
        }

        public DataExtensionItemListContainer GetDataByRequestToken(string requestToken)
        {
            return GetDataByUrl($"/data/v1/customobjectdata/token/{Uri.EscapeDataString(requestToken)}/rowset");
        }

        internal DataExtensionItemListContainer GetDataByUrl(string url, Dictionary<string, string> qs = null)
        {
            var dataContainer = Get<DataExtensionItemListContainer>(url, qs);
            if (dataContainer.Links != null)
            {
                if (dataContainer.Links.Next != null)
                {
                    dataContainer.GetNext = () => GetDataByUrl(new Uri(new Uri(AccessToken.RestInstanceUrl), "/data" + dataContainer.Links.Next).ToString());
                }
                if (dataContainer.Links.Previous != null)
                {
                    dataContainer.GetPrev = () => GetDataByUrl(new Uri(new Uri(AccessToken.RestInstanceUrl), "/data" + dataContainer.Links.Previous).ToString());
                }
            }
            dataContainer.GetFields = () =>
            {
                var de = new DataExtensions(AccessToken);
                return de.GetDataExtensionFields(dataContainer.CustomObjectId);
            };
            return dataContainer;
        }
    }
}
