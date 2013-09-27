using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MandrillWrapper.Model.Responses
{
    public enum EmailResultStatus
    {
        Sent,
        Queued,
        Rejected,
        Invalid,
        Scheduled
    }

    public class SendEmailResponse
    {
        public string email { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EmailResultStatus status { get; set; }

        public string reject_reason { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }
    }
}
