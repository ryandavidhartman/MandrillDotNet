using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MandrillAPI.Model.Data
{

    [DataContract(Name = "rcpt_merge_var")]
    public class RecipientMergeVariables
    {
        [DataMember(Name = "Recipient")]
        public string Recipient { get; set; }

        [DataMember(Name = "vars")]
        public List<MergeVariable> MergeVariables;

        public RecipientMergeVariables()
        {
            MergeVariables = new List<MergeVariable>();
        }
    }
}
