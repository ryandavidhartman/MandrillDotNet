using System.Runtime.Serialization;
using MandrillWrapper.Model.Responses;

namespace MandrillWrapper.Model.Requests
{
    [DataContract(Name = "info_request")]
    public class GetInfoRequest : IRequest
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
    }
}