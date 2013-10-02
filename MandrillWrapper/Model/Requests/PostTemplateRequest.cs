using System.Runtime.Serialization;
using MandrillWrapper.Model.Data;
using ServiceStack.ServiceHost;

namespace MandrillWrapper.Model.Requests
{
    [DataContract(Name = "post_template_request")]
    public class PostTemplateRequest : IRequest, IReturn<Template>
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