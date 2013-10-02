using System.Collections.Generic;
using System.Runtime.Serialization;
using MandrillWrapper.Model.Data;
using ServiceStack.ServiceHost;

namespace MandrillWrapper.Model.Requests
{
    [DataContract(Name = "templates_request")]
    public class GetTemplatesRequest : IRequest, IReturn<List<Template>>
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
    }
}