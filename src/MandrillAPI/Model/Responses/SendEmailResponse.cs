using System.Runtime.Serialization;
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

    [DataContract(Name = "send_email_response")]
    public class SendEmailResponse
    {
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EmailResultStatus Status { get; set; }

        [DataMember(Name = "reject_reason")]
        public string RejectReason { get; set; }

        [DataMember(Name = "_id")]
        public string Id { get; set; }
    }
}
