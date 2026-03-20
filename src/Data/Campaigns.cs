using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Yokinsoft.Salesforce.MCE
{
    public class CampaignToCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CampaignCode { get; set; }
        public string Color { get; set; }
        public bool Favorite { get; set; }
    }
    public class Campaign
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Id { get; set; }
    }
    public class CampaignAsset
    {
        public string Id { get; set; }
        public long CampaignId { get; set; }
        /// <summary>
        /// Enum values:
        ///     AUTOMATION_DEFINITION
        ///     CALENDAR_EVENT
        ///     CMS_ASSET
        ///     EMAIL
        ///     GROUP
        ///     LIST
        ///     PUSH_MESSAGE
        ///     SENDABLE_CUSTOM_OBJECT
        ///     SMS_MESSAGE
        ///     TRIGGERED
        /// </summary>
        public string Type { get; set; }
        public long ItemID { get; set; }
        public string ObjectID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
