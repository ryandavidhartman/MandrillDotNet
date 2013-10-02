using System.Collections.Generic;
using System.Runtime.Serialization;
using MandrillWrapper.Model.Responses;
using ServiceStack.ServiceHost;

namespace MandrillWrapper.Model.Requests
{
    [DataContract(Name = "sender_data_request")]
    public class SenderDataRequest : IRequest, IReturn<List<SenderDataResponse>>
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
    }
}