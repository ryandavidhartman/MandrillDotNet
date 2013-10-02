using System.Runtime.Serialization;
using MandrillWrapper.Model.Responses;
using ServiceStack.ServiceHost;

namespace MandrillWrapper.Model.Requests
{
    [DataContract(Name = "info_request")]
    public class GetInfoRequest : IRequest, IReturn<InfoResponse>
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
    }
}