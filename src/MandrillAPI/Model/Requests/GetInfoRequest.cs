using System.Runtime.Serialization;
using MandrillAPI.Model.Responses;

namespace MandrillAPI.Model.Requests
{
    [DataContract(Name = "info_request")]
    public class GetInfoRequest : IRequest
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
    }
}