using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yokins.Salesforce.MCE
{
    public class OneTimeImport
    {
        [JsonPropertyName("source")]
        public SourceInfo Source { get; set; }

        [JsonPropertyName("target")]
        public TargetInfo Target { get; set; }

        [JsonPropertyName("mapping")]
        public MappingInfo Mapping { get; set; }

        [JsonPropertyName("transport")]
        public TransportInfo Transport { get; set; }
    }

    public class SourceInfo
    {
        [JsonPropertyName("fileInfo")]
        public FileInfoSpec FileInfo { get; set; }
    }

    public class FileInfoSpec
    {
        [JsonPropertyName("specifier")]
        public string Specifier { get; set; }
        /// <summary>
        /// [CSV,TAB,OTHER]
        /// </summary>

        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }

        [JsonPropertyName("standardQuotedStrings")]
        public bool? StandardQuotedStrings { get; set; }
    }

    public class TargetInfo
    {
        /// <summary>
        /// DataExtension
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// DE_Customer_Key
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
        /// <summary>
        /// [Overwrite, AddAndUpdate, AddAndDoNotUpdate, UpdateButDoNotAdd, ColumnBased]
        /// </summary>

        [JsonPropertyName("updateType")]
        public string UpdateType { get; set; }
    }

    public class MappingInfo
    {
        [JsonPropertyName("allowErrors")]
        public bool? AllowErrors { get; set; }
        /// <summary>
        /// [InferFromColumnHeadings, MapByOrdinal, ManualMap]
        /// </summary>

        [JsonPropertyName("fieldMappingType")]
        public string FieldMappingType { get; set; }
    }

    public class TransportInfo
    {
        /// <summary>
        /// [AWS_FTL , ExternalSignedURL (use this static key in case of import from a pre-signed URL source)]
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }

    public class OneTimeImportResult : Result
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
    public class OneTimeImportStatusResult : Result
    {
        [JsonPropertyName("summary")]
        public OneTimeImportSummary Summary { get; set; }
    }
    public class OneTimeImportSummary
    {
        [JsonPropertyName("duplicateRows")]
        public int? DuplicateRows { get; set; }

        [JsonPropertyName("restrictedRows")]
        public int? RestrictedRows { get; set; }

        // Keeping as string because incoming format may be culture-specific (e.g. "10/16/2023 10:22:26 AM")
        [JsonPropertyName("endDate")]
        public string EndDate { get; set; }

        [JsonPropertyName("errors")]
        public int? Errors { get; set; }

        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("importStatus")]
        public string ImportStatus { get; set; }

        [JsonPropertyName("importId")]
        public string ImportId { get; set; }

        // Keeping as string because incoming format may be culture-specific
        [JsonPropertyName("startDate")]
        public string StartDate { get; set; }

        [JsonPropertyName("successfulRows")]
        public int? SuccessfulRows { get; set; }

        [JsonPropertyName("targetId")]
        public string TargetId { get; set; }

        [JsonPropertyName("targetKey")]
        public string TargetKey { get; set; }

        [JsonPropertyName("targetUpdateType")]
        public string TargetUpdateType { get; set; }

        [JsonPropertyName("totalRows")]
        public int? TotalRows { get; set; }
    }
    public class OneTimeImportValidationSummaryResult : Result
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("importValidationSummary")]
        public List<OneTimeImportValidationSummary> ImportValidationSummary { get; set; } = new List<OneTimeImportValidationSummary>();
    }
    public class OneTimeImportValidationSummary
    {
        [JsonPropertyName("validationErrorType")]
        public string ValidationErrorType { get; set; }
        [JsonPropertyName("validationErrorCodeId")]
        public int? ValidationErrorCodeId { get; set; }
        [JsonPropertyName("errorsCount")]
        public int? ErrorsCount { get; set; }
    }
    public class OneTimeImportValidationDetailsResult : Result
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("importValidationSummary")]
        public List<OneTimeImportValidationSummary> ImportValidationSummary { get; set; } = new List<OneTimeImportValidationSummary>();
    }
    public class OneTimeImportValidationDetails
    {
        [JsonPropertyName("rowId")]
        public long RowId { get; set; }
        [JsonPropertyName("validationErrorType")]
        public string ValidationErrorType { get; set; }
        [JsonPropertyName("validationErrorCodeId")]
        public int? ValidationErrorCodeId { get; set; }
        [JsonPropertyName("validationErrorDetails")]
        public int? ValidationErrorDetails { get; set; }
    }
}
