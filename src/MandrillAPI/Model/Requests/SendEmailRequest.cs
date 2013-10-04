using System.Runtime.Serialization;
using MandrillAPI.Model.Data;

namespace MandrillAPI.Model.Requests
{
    
    [DataContract(Name = "send_email_request")]
    public class SendEmailRequest : IRequest
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "message")]
        public EmailMessage Message { get; set; }
    }
}
