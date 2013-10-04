using System.Runtime.Serialization;

namespace MandrillAPI.Model.Data
{
    [DataContract(Name = "attachment")]
    public class Attachment
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }
    }
}
