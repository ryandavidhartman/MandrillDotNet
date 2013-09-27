using System.Collections.Generic;

namespace MandrillWrapper.Model.Data
{
    public class rcpt_merge_var
    {
        public string rcpt;
        public List<merge_var> vars;

        public rcpt_merge_var()
        {
            vars = new List<merge_var>();
        }
    }
}
