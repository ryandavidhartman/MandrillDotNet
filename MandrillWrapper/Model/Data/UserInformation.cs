using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandrillWrapper.Model.Data
{
    public class UserInformation
    {
       
        public string username { get; set; }
        public string created_at { get; set; }
        public string public_id { get; set; }
        public int reputation { get; set; }
        public int hourly_quota { get; set; }
        public int backlog { get; set; }
        public Days stats { get; set; }
    }
}
