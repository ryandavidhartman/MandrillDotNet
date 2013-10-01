using System.Runtime.Serialization;

namespace MandrillWrapper.Model.Data
{
    [DataContract(Name = "merge_var")]
    public class MergeVariable
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }


        public MergeVariable()
        {

        }

        public MergeVariable(string name, string content)
        {
            Name = name;
            Content = content;
        }

    }
}
