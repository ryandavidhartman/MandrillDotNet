using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandrillWrapper.Model.Data
{
    public class EmailAddress
    {
        public string email { get; set; }

        public string name { get; set; }

        public EmailAddress()
        {
        }

        public EmailAddress(string email)
        {
            this.email = email;
            this.name = "";
        }

        public EmailAddress(string email, string name)
        {
            this.email = email;
            this.name = name;
        }
    }
}
