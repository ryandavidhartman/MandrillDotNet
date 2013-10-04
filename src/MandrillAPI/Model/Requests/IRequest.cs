using System.Runtime.Serialization;

namespace MandrillAPI.Model.Requests
{
    public interface IRequest
    {
        [DataMember(Name = "key")]
        string Key { get; set; }
    }
}