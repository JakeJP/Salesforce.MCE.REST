using System;
using System.Collections.Generic;
using Yokoins.Salesforce.MC;
using static Yokinsoft.Salesforce.MCE.DataExtension;

namespace Yokinsoft.Salesforce.MCE
{
    public class DataExtensions : APIClientBase
    {
        public DataExtensions(AccessToken accessToken) : base(accessToken)
        {
        }

        public PageableListContainer<DataExtension> GetDataExtensions(string search)
        {
            var url = "/data/v1/customobjects";
            return Get<PageableListContainer<DataExtension>>(url, new Dictionary<string, string> { { "$search", search } });
        }
        public DataExtension CreateDataExtension(DataExtensionToCreate dataExtension)
        {
            // POST https://{subdomain}.rest.marketingcloudapis.com/data/v1/customobjects
            var url = "/data/v1/customobjects";
            return Post<DataExtension>(url, dataExtension);
        }
        public void DeleteDataExtension(string id)
        {
            // DELETE https://{subdomain}.rest.marketingcloudapis.com/data/v1/customobjects/{id}
            Delete<object>("/data/v1/customobjects/" + Uri.EscapeDataString(id));
        }
        public DataExtension GetDataExtension(string id)
        {
            return Get<DataExtension>("/data/v1/customobjects/" + Uri.EscapeDataString(id));
        }
        public void UpdateDataExtension(string id, DataExtensionToCreate dataExtension)
        {
            //PATCH https://{subdomain}.rest.marketingcloudapis.com/data/v1/customobjects/{id}
            Patch<object>("/data/v1/customobjects/" + Uri.EscapeDataString(id), dataExtension);
        }
        public void ClearDataExtensionData(string id)
        {
            // POST https://{subdomain}.rest.marketingcloudapis.com/data/v1/customobjects/{id}/cleardata
            Post<object>($"/data/v1/customobjects/{Uri.EscapeDataString(id)}/cleardata", null);
        }
        public IList<DataExtension.Field> GetDataExtensionFields(string id)
        {
            var res = Get<FieldListContainer>($"/data/v1/customobjects/{Uri.EscapeDataString(id)}/fields");
            return res.Fields;
        }
        public IList<DataExtension.Field> GetDataExtensionFieldsCategoryId(string id)
        {
            // GET https://{subdomain}.rest.marketingcloudapis.com/data/v1/customobjects/category/{categoryId}
            var res = Get<FieldListContainer>($"/data/v1/customobjects/category/{Uri.EscapeDataString(id)}");
            return res.Fields;
        }
        public void UpdateDataExtensionField(string id, IEnumerable<DataExtension.Field> fields)
        {
            // PATCH https://{subdomain}.rest.marketingcloudapis.com/data/v1/customobjectsasync/{id}/fields
            Patch<object>($"/data/v1/customobjectsasync/{Uri.EscapeDataString(id)}/fields", new { fields });
        }
        public void CreateDataExtensionField(string id, IEnumerable<DataExtension.FieldToCreate> fields)
        {
            // POST        https://{subdomain}.rest.marketingcloudapis.com/data/v1/customobjectsasync/{id}/fields
            Post<object>($"/data/v1/customobjectsasync/{Uri.EscapeDataString(id)}/fields", new { fields });
        }
    }
}
