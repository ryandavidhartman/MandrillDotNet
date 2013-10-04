using System.Collections.Generic;
using System.Runtime.Serialization;
using MandrillAPI.Model.Data;

namespace MandrillAPI.Model.Requests
{
    [DataContract(Name = "send_email_with_template_request")]
    public class SendEmailWithTemplateRequest : IRequest
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "template_name")]
        public string TemplateName { get; set; }

        [DataMember(Name = "template_content")]
        public List<TemplateContent> TemplateContent { get; set; }

        [DataMember(Name = "message")]
        public EmailMessage Message { get; set; }
    }
}