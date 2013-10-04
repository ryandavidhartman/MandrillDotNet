using System.Collections.Generic;
using System.Runtime.Serialization;
using MandrillWrapper.Model.Data;

namespace MandrillWrapper.Model.Responses
{
    [DataContract(Name="sender_data")]
    public class SenderDataResponse
    {
        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "send")]
        public string Sent { get; set; }

        [DataMember(Name = "hard_bounces")]
        public int HardBounces { get; set; }

        [DataMember(Name = "soft_bounces")]
        public int SoftBounces { get; set; }

        [DataMember(Name = "rejects")]
        public int Rejects { get; set; }

        [DataMember(Name = "complaints")]
        public int Complaints { get; set; }

        [DataMember(Name = "unsubs")]
        public int Unsubscriptions { get; set; }

        [DataMember(Name = "opens")]
        public int Opens { get; set; }

        [DataMember(Name = "clicks")]
        public int Clicks { get; set; }

        [DataMember(Name = "unique_opens")]
        public int UniqueOpens { get; set; }

        [DataMember(Name = "unique_clicks")]
        public int UniqueClicks { get; set; }

    }
}