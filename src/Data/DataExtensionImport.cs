using System.Collections.Generic;

namespace Yokinsoft.Salesforce.MCE
{
    public class OneTimeImport
    {
        public SourceInfo Source { get; set; }

        public TargetInfo Target { get; set; }

        public MappingInfo Mapping { get; set; }

        public TransportInfo Transport { get; set; }
        public class SourceInfo
        {
            public FileInfoSpec FileInfo { get; set; }
        }
        public class FileInfoSpec
        {
            public string Specifier { get; set; }
            /// <summary>
            /// [CSV,TAB,OTHER]
            /// </summary>

            public string ContentType { get; set; }

            public bool? StandardQuotedStrings { get; set; }
        }
        public class TargetInfo
        {
            /// <summary>
            /// DataExtension
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// DE_Customer_Key
            /// </summary>
            public string Key { get; set; }
            /// <summary>
            /// [Overwrite, AddAndUpdate, AddAndDoNotUpdate, UpdateButDoNotAdd, ColumnBased]
            /// </summary>

            public string UpdateType { get; set; }
        }
        public class MappingInfo
        {
            public bool? AllowErrors { get; set; }
            /// <summary>
            /// [InferFromColumnHeadings, MapByOrdinal, ManualMap]
            /// </summary>

            public string FieldMappingType { get; set; }
        }
        public class TransportInfo
        {
            /// <summary>
            /// [AWS_FTL , ExternalSignedURL (use this static key in case of import from a pre-signed URL source)]
            /// </summary>
            public string Key { get; set; }
        }
    }

    public class OneTimeImportResult : Result
    {
        public string Id { get; set; }
    }
    public class OneTimeImportStatusResult : Result
    {
        public OneTimeImportSummary Summary { get; set; }
    }
    public class OneTimeImportSummary
    {
        public int? DuplicateRows { get; set; }

        public int? RestrictedRows { get; set; }

        // Keeping as string because incoming format may be culture-specific (e.g. "10/16/2023 10:22:26 AM")
        public string EndDate { get; set; }

        public int? Errors { get; set; }

        public long? Id { get; set; }

        public string ImportStatus { get; set; }

        public string ImportId { get; set; }

        // Keeping as string because incoming format may be culture-specific
        public string StartDate { get; set; }

        public int? SuccessfulRows { get; set; }

        public string TargetId { get; set; }

        public string TargetKey { get; set; }

        public string TargetUpdateType { get; set; }

        public int? TotalRows { get; set; }
    }
    public class OneTimeImportValidationSummaryResult : Result
    {
        public string Id { get; set; }
        public List<OneTimeImportValidationSummary> ImportValidationSummary { get; set; } = new List<OneTimeImportValidationSummary>();
    }
    public class OneTimeImportValidationSummary
    {
        public string ValidationErrorType { get; set; }
        public int? ValidationErrorCodeId { get; set; }
        public int? ErrorsCount { get; set; }
    }
    public class OneTimeImportValidationDetailsResult : Result
    {
        public string Id { get; set; }
        public List<OneTimeImportValidationSummary> ImportValidationSummary { get; set; } = new List<OneTimeImportValidationSummary>();
    }
    public class OneTimeImportValidationDetails
    {
        public long RowId { get; set; }
        public string ValidationErrorType { get; set; }
        public int? ValidationErrorCodeId { get; set; }
        public int? ValidationErrorDetails { get; set; }
    }
}
