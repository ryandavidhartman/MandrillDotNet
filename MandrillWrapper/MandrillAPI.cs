using System;
using System.Collections.Generic;
using System.Net;
using MandrillWrapper.Model.Data;
using MandrillWrapper.Model.Requests;
using MandrillWrapper.Model.Responses;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;

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

        public bool Ping(PingRequest request)
        {
            AddKeyToRequest(request);
            
            var response = RestClient.Post<string>("/users/ping.json", request);
            return response != null && "\"PONG!\"".Equals(response);
        }

        public void PingAsync(PingRequest request, Action<string> pingHandler)
        {
            AddKeyToRequest(request);
            RestClient.PostAsync("/users/ping.json", request, pingHandler, (r, ex) => { throw ex; });
        }

        public InfoResponse Info(GetInfoRequest request)
        {
            AddKeyToRequest(request);
            var response = RestClient.Post<InfoResponse>("/users/info.json", request);
            return response;
        }

        public void InfoAsync(GetInfoRequest request, Action<InfoResponse> infoHandler)
        {
            AddKeyToRequest(request);
            RestClient.PostAsync("/users/info.json", request, infoHandler, (r, ex) => { throw ex; });
        }

        public List<SenderDataResponse> GetSenderData(SenderDataRequest request)
        {
            AddKeyToRequest(request);
            var senderData = RestClient.Post<List<SenderDataResponse>>("/users/senders.json", request );
            return senderData;
        }

        public void GetSenderDataAsync(SenderDataRequest request, Action<List<SenderDataResponse>> senderDataHandler)
        {
            AddKeyToRequest(request);
            RestClient.PostAsync("/users/senders.json", request, senderDataHandler, (r, ex) => { throw ex; });
        }

        public Template AddTemplate(PostTemplateRequest request)
        {
            AddKeyToRequest(request);
            var template = RestClient.Post<Template>("/templates/add.json", request);
            return template;
            
        }

        public void AddTemplateAsync(PostTemplateRequest request, Action<Template> addTemplateHandler)
        {
            AddKeyToRequest(request);
            RestClient.PostAsync("/templates/add.json", request, addTemplateHandler, (r, ex) => { throw ex; });
        }

        public List<Template> GetTemplates(GetTemplatesRequest request)
        {
            AddKeyToRequest(request);
            var templates = RestClient.Post<List<Template>>("/templates/list.json", request);
            return templates;
        }

        public List<SendEmailResponse> SendEmail(SendEmailRequest request)
        {
            AddKeyToRequest(request);
            var response = RestClient.Post<List<SendEmailResponse>>("/messages/send.json", request);
            return response;
        }

        public List<SendEmailResponse> SendEmail(EmailMessage message, string templateName, List<TemplateContent> templateContent)
        {
            var request = new SendEmailWithTemplateRequest { key = _key, message = message, template_name  = templateName, template_content = templateContent};
            var response = RestClient.Post<List<SendEmailResponse>>("/messages/send-template.json", request);
            return response;
        }

        public void AddKeyToRequest(IRequest request)
        {
            if (string.IsNullOrEmpty(request.Key))
                request.Key = _key;
        }
    }
}
