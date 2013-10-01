using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MandrillWrapper.Model.Data
{
    [DataContract(Name = "email_address")]
    public class EmailAddress
    {
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }


        public EmailAddress()
        {
        }


        public EmailAddress(string email)
        {
            Email = email;
            Name = "";
        }


        public EmailAddress(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}
