
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MandrillWrapper.Model.Data
{
    [DataContract(Name = "days")]
    public class Days
    {
        [DataMember(Name = "today")]
        public SendingStatistics Today { get; set; }

        [DataMember(Name = "last_7_days")]
        public SendingStatistics Last7Days { get; set; }

        [DataMember(Name = "last_30_days")]
        public SendingStatistics Last30Days { get; set; }

        [DataMember(Name = "last_60_days")]
        public SendingStatistics Last60Days { get; set; }

        [DataMember(Name = "last_90_days")]
        public SendingStatistics Last90Days { get; set; }

        [DataMember(Name = "all_time")]
        public SendingStatistics AllTime { get; set; }

    }
}
