using System.Runtime.Serialization;

namespace MandrillAPI.Model.Requests
{
    [DataContract(Name = "templates_request")]
    public class GetTemplatesRequest : IRequest
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
    }
}