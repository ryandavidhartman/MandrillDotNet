using System.Runtime.Serialization;

namespace MandrillWrapper.Model.Data
{
    [DataContract(Name = "image")]
    public class Image
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }
    }
}
