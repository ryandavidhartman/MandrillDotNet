using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MandrillAPI.Model.Requests
{
    [DataContract(Name = "ping_request")]
    public class PingRequest : IRequest
    {
        [DataMember(Name = "key")]
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
    }
}