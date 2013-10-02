using System;
using System.Collections.Generic;
using MandrillWrapper.Model.Data;
using MandrillWrapper.Model.Requests;
using MandrillWrapper.Model.Responses;
using ServiceStack.ServiceClient.Web;

namespace MandrillWrapper
{
    public class MandrillAPI
    {
        private readonly string _key;
        private readonly string _url;
        public JsonServiceClient RestClient { get; set; }

        public MandrillAPI(string key, string url)
        {
            _key = key;
            _url = url;
            RestClient = new JsonServiceClient(_url);
        }

        public bool Ping()
        {
            var pingRequest = new PingRequest {Key = _key};
            var response = RestClient.Post<string>("/users/ping.json", pingRequest);
            return response != null && "\"PONG!\"".Equals(response);
        }

        public void PingAsync(Action<string> pingHandler)
        {
            var pingRequest = new PingRequest { Key = _key };
            RestClient.PostAsync("/users/ping.json", pingRequest, pingHandler, (r, ex) => { throw ex; });
        }

        public InfoResponse Info()
        {
            var infoRequest = new InfoRequest { Key = _key };
            var response = RestClient.Post<InfoResponse>("/users/info.json", infoRequest);
            return response;
        }

        public void InfoAsync(Action<InfoResponse> infoHandler)
        {
            var infoRequest = new InfoRequest { Key = _key };
            RestClient.PostAsync("/users/info.json", infoRequest, infoHandler, (r, ex) => { throw ex; });
        }

        public List<SenderDataResponse> GetSenderData()
        {
            var senderDataRequest = new SenderDataRequest {Key = _key};
            var senderData = RestClient.Post<List<SenderDataResponse>>("/users/senders.json", senderDataRequest );
            return senderData;
        }

        public void GetSenderDataAsync(Action<List<SenderDataResponse>> senderDataHandler)
        {
            var senderDataRequest = new SenderDataRequest { Key = _key };
            RestClient.PostAsync<List<SenderDataResponse>>("/users/senders.json", senderDataRequest, senderDataHandler, (r, ex) => { throw ex; });
        }

        public List<TemplateInfo> GetTemplates()
        {
            var templates = RestClient.Post<List<TemplateInfo>>("/templates/list.json", new Request { Key = _key });
            return templates;
        }

        public List<SendEmailResponse> SendEmail(EmailMessage message)
        {
            var request = new SendEmailRequest {key = _key, message = message};
            var response = RestClient.Post<List<SendEmailResponse>>("/messages/send.json", request);
            return response;
        }

        public List<SendEmailResponse> SendEmail(EmailMessage message, string templateName, List<TemplateContent> templateContent)
        {
            var request = new SendEmailWithTemplateRequest { key = _key, message = message, template_name  = templateName, template_content = templateContent};
            var response = RestClient.Post<List<SendEmailResponse>>("/messages/send-template.json", request);
            return response;
        }
    }
}
