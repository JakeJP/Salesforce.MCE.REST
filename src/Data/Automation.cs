using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Yokins.Salesforce.MCE
{
    public class Automation
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("typeId")]
        public int? TypeId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("statusId")]
        public int? StatusId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("categoryId")]
        public long? CategoryId { get; set; }

        [JsonPropertyName("lastRunTime")]
        public DateTime? LastRunTime { get; set; }

        [JsonPropertyName("lastRunInstanceId")]
        public string LastRunInstanceId { get; set; }

        [JsonPropertyName("schedule")]
        public AutomationSchedule Schedule { get; set; }

        [JsonPropertyName("fileTrigger")]
        public AutomationFileTrigger FileTrigger { get; set; }

        [JsonPropertyName("automationTrigger")]
        public AutomationTrigger AutomationTrigger { get; set; }
    }

    public class AutomationSchedule
    {
        [JsonPropertyName("scheduleStatus")]
        public string ScheduleStatus { get; set; }
    }

    public class AutomationFileTrigger
    {
        [JsonPropertyName("queueFiles")]
        public bool? QueueFiles { get; set; }

        [JsonPropertyName("isPublished")]
        public bool? IsPublished { get; set; }

        [JsonPropertyName("fileNamePatternTypeId")]
        public int? FileNamePatternTypeId { get; set; }

        [JsonPropertyName("fileNamingPattern")]
        public string FileNamingPattern { get; set; }

        [JsonPropertyName("folderLocationText")]
        public string FolderLocationText { get; set; }

        [JsonPropertyName("triggerActive")]
        public bool? TriggerActive { get; set; }
    }

    public class AutomationTrigger
    {
        [JsonPropertyName("queueFiles")]
        public bool? QueueFiles { get; set; }

        [JsonPropertyName("filenamePattern")]
        public string FilenamePattern { get; set; }

        [JsonPropertyName("filenamePatternTypeId")]
        public int? FilenamePatternTypeId { get; set; }

        [JsonPropertyName("fileTransferLocationId")]
        public string FileTransferLocationId { get; set; }

        [JsonPropertyName("sampleFileName")]
        public string SampleFileName { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }
    }

    public class AutomationStep
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("step")]
        public int? Step { get; set; }

        [JsonPropertyName("activities")]
        public List<AutomationActivity> Activities { get; set; } = new List<AutomationActivity>();

        public static AutomationStep FromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("json must not be empty", nameof(json));

            var opts = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true
            };

            return JsonSerializer.Deserialize<AutomationStep>(json, opts);
        }

        public string ToJson()
        {
            var opts = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Serialize(this, opts);
        }
    }

    public class AutomationActivity
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("activityObjectId")]
        public string ActivityObjectId { get; set; }

        [JsonPropertyName("objectTypeId")]
        public int? ObjectTypeId { get; set; }

        [JsonPropertyName("displayOrder")]
        public int? DisplayOrder { get; set; }
    }

    public class AutomationTriggerStatus
    {
        [JsonPropertyName("triggerdetails")]
        public AutomationTriggerDetails TriggerDetails { get; set; }

        [JsonPropertyName("triggerRequestID")]
        public string TriggerRequestID { get; set; }

        [JsonPropertyName("resultMessages")]
        public List<string> ResultMessages { get; set; } = new List<string>();
    }

    public class AutomationTriggerDetails
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("automationDetails")]
        public AutomationDetails AutomationDetails { get; set; }
    }

    public class AutomationDetails
    {
        [JsonPropertyName("automationName")]
        public string AutomationName { get; set; }

        [JsonPropertyName("automationExternalKey")]
        public string AutomationExternalKey { get; set; }
    }
}
