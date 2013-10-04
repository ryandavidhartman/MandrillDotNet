using System.Runtime.Serialization;

namespace MandrillWrapper.Model.Requests
{
    public interface IRequest
    {
        [DataMember(Name = "key")]
        string Key { get; set; }
    }
}