using System.Runtime.Serialization;

namespace MandrillWrapper.Model.Requests
{
    [DataContract(Name = "delete_template_request")]
    public class DeleteTemplateRequest : IRequest
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "name")]
        public string TemplateName { get; set; }
    }
}