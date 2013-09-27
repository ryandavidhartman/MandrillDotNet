using System.Collections.Generic;
using MandrillWrapper.Model.Data;
using MandrillWrapper.Model.Operations;
using MandrillWrapper.Model.Requests;
using MandrillWrapper.Model.Responses;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;

namespace MandrillWrapper
{
    public class MandrillAPI
    {
        public string Key { get; set; }
        public string Url { get; set; }
        public IRestClient RestClient { get; set; }

        public MandrillAPI(string key, string url)
        {
            Key = key;
            Url = url;
            RestClient = new JsonServiceClient(Url);
        }

        public bool Ping()
        {
            var response = RestClient.Post<string>("/users/ping.json", new Request { key = Key });
            return response != null && "\"PONG!\"".Equals(response);
        }

        public UserInformation Info()
        {
            var info = RestClient.Post<UserInformation>("/users/info.json", new Request {key = Key});
            return info;
        }

        public List<SenderData> GetSenderData()
        {
            var senderData = RestClient.Post<List<SenderData>>("/users/senders.json", new Request { key = Key });
            return senderData;
        }

        public List<TemplateInfo> GetTemplates()
        {
            var templates = RestClient.Post<List<TemplateInfo>>("/templates/list.json", new Request { key = Key });
            return templates;
        }

        public List<SendEmailResponse> SendEmail(EmailMessage message)
        {
            var request = new SendEmailRequest {key = Key, message = message};
            var response = RestClient.Post<List<SendEmailResponse>>("/messages/send.json", request);
            return response;
        }

        public List<SendEmailResponse> SendEmail(EmailMessage message, string templateName, List<TemplateContent> templateContent)
        {
            var request = new SendEmailWithTemplateRequest { key = Key, message = message, template_name  = templateName, template_content = templateContent};
            var response = RestClient.Post<List<SendEmailResponse>>("/messages/send-template.json", request);
            return response;
        }
    }
}
