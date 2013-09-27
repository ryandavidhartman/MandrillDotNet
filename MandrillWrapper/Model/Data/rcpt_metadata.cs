using System.Collections.Generic;

namespace MandrillWrapper.Model.Data
{
    public struct rcpt_metadata
    {
        public string rcpt;
        public IEnumerable<string> values;
    }
}
