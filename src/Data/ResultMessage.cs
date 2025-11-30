using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yokins.Salesforce.MCE
{
    public class Result
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }
        [JsonPropertyName("resultMessages")]
        public List<ResultMessage> ResultMessages { get; set; }
    }

    public class ResultMessage
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("resultType")]
        public string ResultType { get; set; }
        [JsonPropertyName("resultClass")]
        public string ResultClass { get; set; }
        [JsonPropertyName("resultCode")]
        public string ResultCode { get; set; }
    }
    public class ResultMessageWithFormat : ResultMessage
    {
        [JsonPropertyName("formatStringParams")]
        public string[] FormatStringParams { get; set; }
        [JsonPropertyName("messageFormatString")]
        public string MessageFormatString { get; set; }
    }
}
