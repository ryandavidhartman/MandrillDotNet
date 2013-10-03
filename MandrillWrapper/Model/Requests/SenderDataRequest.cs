using System.Runtime.Serialization;

namespace MandrillWrapper.Model.Requests
{
    [DataContract(Name = "sender_data_request")]
    public class SenderDataRequest : IRequest
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
    }
}