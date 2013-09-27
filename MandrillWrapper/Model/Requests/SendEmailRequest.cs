using MandrillWrapper.Model.Data;

namespace MandrillWrapper.Model.Requests
{
    public class SendEmailRequest
    {
        public string key { get; set; }
        public EmailMessage message { get; set; }
    }
}
