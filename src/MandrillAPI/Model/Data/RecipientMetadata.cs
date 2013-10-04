using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MandrillAPI.Model.Data
{
    [DataContract(Name = "rcpt_metadata")]
    public class RecipientMetadata
    {
        [DataMember(Name = "rcpt")]
        public string Recipient { get; set; }

        [DataMember(Name = "values")]
        public IEnumerable<string> Values { get; set; }
    }
}
