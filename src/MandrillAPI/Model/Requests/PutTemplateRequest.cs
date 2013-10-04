using System.Runtime.Serialization;

namespace MandrillAPI.Model.Requests
{
    [DataContract(Name = "put_template_request")]
    public class PutTemplateRequest : IRequest
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "name")]
        public string TemplateName { get; set; }

        [DataMember(Name = "from_email")]
        public string FromEmail { get; set; }

        [DataMember(Name = "from_name")]
        public string FromName { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "publish")]
        public bool Publish { get; set; }
    }
}