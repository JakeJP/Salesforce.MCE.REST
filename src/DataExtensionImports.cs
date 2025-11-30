using System;

namespace Yokins.Salesforce.MCE
{
    public class DataExtensionImports : APIClientBase
    {
        public DataExtensionImports(AccessToken accessToken) : base(accessToken)
        {
        }
        public OneTimeImportResult QueueAndStart(OneTimeImport request)
        {
            // /data/v1/async/import
            return Post<OneTimeImportResult>($"/data/v1/async/import", request);
        }
        public OneTimeImportStatusResult GetStatus(string id)
        {
            return Get<OneTimeImportStatusResult>($"/data/v1/async/import/{Uri.EscapeDataString(id)}/summary");
        }
        public OneTimeImportValidationSummaryResult GetValidationSummary(string id)
        {
            return Get<OneTimeImportValidationSummaryResult>($"/data/v1/async/import/{Uri.EscapeDataString(id)}/validationsummary");
        }
        public OneTimeImportValidationDetailsResult GetValidationDetails(string id)
        {
            return Get<OneTimeImportValidationDetailsResult>($"/data/v1/async/import/{Uri.EscapeDataString(id)}/validationresult");
        }
    }
}
