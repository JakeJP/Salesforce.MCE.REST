using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yokinsoft.Salesforce.MCE
{
    public class Automation
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Key { get; set; }

        public int? TypeId { get; set; }

        public string Type { get; set; }

        public int? StatusId { get; set; }

        public string Status { get; set; }

        public long? CategoryId { get; set; }

        public DateTime? LastRunTime { get; set; }

        public string LastRunInstanceId { get; set; }

        public AutomationSchedule Schedule { get; set; }

        public AutomationFileTrigger FileTrigger { get; set; }

        public AutomationTrigger AutomationTrigger { get; set; }
    }

    public class AutomationSchedule
    {
        public string ScheduleStatus { get; set; }
    }

    public class AutomationFileTrigger
    {
        public bool? QueueFiles { get; set; }

        public bool? IsPublished { get; set; }

        public int? FileNamePatternTypeId { get; set; }

        public string FileNamingPattern { get; set; }

        public string FolderLocationText { get; set; }

        public bool? TriggerActive { get; set; }
    }

    public class AutomationTrigger
    {
        public bool? QueueFiles { get; set; }

        public string FilenamePattern { get; set; }

        public int? FilenamePatternTypeId { get; set; }

        public string FileTransferLocationId { get; set; }

        public string SampleFileName { get; set; }

        public int? Status { get; set; }
    }

    public class AutomationStep
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int? Step { get; set; }

        public List<AutomationActivity> Activities { get; set; } = new List<AutomationActivity>();
    }

    public class AutomationActivity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ActivityObjectId { get; set; }

        public int? ObjectTypeId { get; set; }

        public int? DisplayOrder { get; set; }
    }

    public class AutomationTriggerStatus
    {
        [JsonPropertyName("triggerdetails")]
        public AutomationTriggerDetails TriggerDetails { get; set; }

        public string TriggerRequestID { get; set; }

        public List<string> ResultMessages { get; set; } = new List<string>();
    }

    public class AutomationTriggerDetails
    {
        public string Status { get; set; }

        public AutomationDetails AutomationDetails { get; set; }
    }

    public class AutomationDetails
    {
        public string AutomationName { get; set; }

        public string AutomationExternalKey { get; set; }
    }

    public class TriggeredAutomationResult
    {
        public string FileTransferLocationKey { get; set; }
        public string Filename { get; set; }
        public string RelativePath { get; set; }
        public string DeduplicationKey { get; set; }
    }
    public abstract class FileTransferActivityBase
    {
        public string Name { get; set; }
        public string CustomerKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class FileTransferActivitySimplified : FileTransferActivityBase
    {
        public string Id { get; set; }
    }
    public class FileTransferActivityToCreate : FileTransferActivityBase
    {

        public string Description { get; set; }
        public string FileSpec { get; set; }

        public bool IsEncrypted { get; set; }

        public bool IsCompressed { get; set; }

        public int MaxFileAge { get; set; }

        public int MaxFileAgeScheduleOffset { get; set; }

        public int MaxImportFrequency { get; set; }

        public string FileTransferLocationId { get; set; }

        public bool IsUpload { get; set; }

        public bool IsPgp { get; set; }

        public bool IsFileSpecLocalized { get; set; }

        public int? PublicKeyManagementId { get; set; }
    }
    public class FileTransferActivity : FileTransferActivityToCreate
    {
        public string Id { get; set; }
    }
    public class AutomationFolder
    {
        public long CategoryId { get; set; }
        public string CategoryType { get; set; }
        public bool HasChildren { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
    }
    public class AutomationFtpLocation
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int LocationTypeId { get; set; }
        public string LocationUrl { get; set; }
    }
    public class AutomationSsjsActivity
    {
        public string SsjsActivityId { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
