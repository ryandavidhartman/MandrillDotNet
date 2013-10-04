using System.Runtime.Serialization;

namespace MandrillAPI.Model.Data
{
    [DataContract(Name = "template_content")]
    public class TemplateContent
    {
        [DataMember(Name = "name", Order = 1)]
        public string Name { get; set; }
        
        [DataMember(Name = "content", Order = 2)]
        public string Content { get; set; }
    }
}
