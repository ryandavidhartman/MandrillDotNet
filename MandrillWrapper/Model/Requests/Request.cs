﻿using System.Runtime.Serialization;

namespace MandrillWrapper.Model.Requests
{
   
    [DataContract(Name = "request")]
    public class Request : IRequest
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
    }
}
