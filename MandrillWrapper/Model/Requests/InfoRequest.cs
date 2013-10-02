using System.Runtime.Serialization;
using MandrillWrapper.Model.Responses;
using ServiceStack.ServiceHost;

namespace MandrillWrapper.Model.Requests
{
    [DataContract(Name = "info_request")]
    public class InfoRequest : IRequest, IReturn<InfoResponse>
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
    }
}