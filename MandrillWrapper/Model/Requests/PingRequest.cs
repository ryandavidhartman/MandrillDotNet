using System.Runtime.Serialization;
using ServiceStack.ServiceHost;

namespace MandrillWrapper.Model.Requests
{
    [DataContract(Name = "ping_request")]
    public class PingRequest : IRequest, IReturn<string>
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
    }
}