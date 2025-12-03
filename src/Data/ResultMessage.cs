using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yokinsoft.Salesforce.MCE
{
    public class Result
    {
        public string RequestId { get; set; }
        public List<ResultMessage> ResultMessages { get; set; }
    }

    public class ResultMessage
    {
        public string Message { get; set; }
        public string ResultType { get; set; }
        public string ResultClass { get; set; }
        public string ResultCode { get; set; }
    }
    public class ResultMessageWithFormat : ResultMessage
    {
        public string[] FormatStringParams { get; set; }
        public string MessageFormatString { get; set; }
    }
}
