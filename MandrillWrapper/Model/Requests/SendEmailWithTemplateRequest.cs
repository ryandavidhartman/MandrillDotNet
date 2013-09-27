using System.Collections.Generic;
using MandrillWrapper.Model.Data;

namespace MandrillWrapper.Model.Requests
{
    public class SendEmailWithTemplateRequest
    {
        public string key { get; set; }
        public string template_name { get; set; }
        public List<TemplateContent> template_content { get; set; }
        public EmailMessage message { get; set; }
    }
}