using System.Runtime.Serialization;

namespace MandrillWrapper.Model.Responses
{
    [DataContract(Name = "error_response")]
    public class ErrorResponse
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "name")]
        public string ErrorName { get; set; }

        [DataMember(Name = "member")]
        public string Message { get; set; }
    }
}
