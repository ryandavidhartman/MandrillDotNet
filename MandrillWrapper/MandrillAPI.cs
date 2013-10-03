using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using MandrillWrapper.Model.Data;
using MandrillWrapper.Model.Requests;
using MandrillWrapper.Model.Responses;
using MandrillWrapper.Utilities;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp.Serializers;

namespace MandrillWrapper
{
    public class MandrillAPI
    {
        private readonly string _key;
        private readonly string _url;
        public RestClient RestClient { get; set; }

        public MandrillAPI(string key, string url)
        {
            _key = key;
            _url = url;
            RestClient = new RestClient(_url);
        }

        public bool Ping(PingRequest request)
        {
            var response = Post<string>("/users/ping.json", request);
            return response != null && "PONG!".Equals(response);
        }

        public void PingAsync(PingRequest request, Action<string> pingHandler)
        {
            AddKeyToRequest(request);
            //RestClient.PostAsync("/users/ping.json", request, pingHandler, (r, ex) => { throw ex; });
        }

        public InfoResponse Info(GetInfoRequest request)
        {
           
            var response = Post<InfoResponse>("/users/info.json", request);
            return response;
        }

        public void InfoAsync(GetInfoRequest request, Action<InfoResponse> infoHandler)
        {
            AddKeyToRequest(request);
          //  RestClient.PostAsync("/users/info.json", request, infoHandler, (r, ex) => { throw ex; });
        }

        public List<SenderDataResponse> GetSenderData(SenderDataRequest request)
        {
            var senderData = Post<List<SenderDataResponse>>("/users/senders.json", request );
            return senderData;
        }

        public void GetSenderDataAsync(SenderDataRequest request, Action<List<SenderDataResponse>> senderDataHandler)
        {
            AddKeyToRequest(request);
          //  RestClient.PostAsync("/users/senders.json", request, senderDataHandler, (r, ex) => { throw ex; });
        }

        public Template AddTemplate(PostTemplateRequest request)
        {
            var response = Post<Template>("/templates/add.json", request);
            return response;

        }

        public void AddTemplateAsync(PostTemplateRequest request, Action<Template> addTemplateHandler)
        {
            AddKeyToRequest(request);
            //RestClient.PostAsync("/templates/add.json", request, addTemplateHandler, (r, ex) => { throw ex; });
        }

        public List<Template> GetTemplates(GetTemplatesRequest request)
        {
            var templates = Post<List<Template>>("/templates/list.json", request);
            return templates;
        }

        public List<SendEmailResponse> SendEmail(SendEmailRequest request)
        {
            var response = Post<List<SendEmailResponse>>("/messages/send.json", request);
            return response;
        }

        public List<SendEmailResponse> SendEmail(EmailMessage message, string templateName, List<TemplateContent> templateContent)
        {
            var request = new SendEmailWithTemplateRequest { Key = _key, Message = message, TemplateName  = templateName, TemplateContent = templateContent};
            var response = Post<List<SendEmailResponse>>("/messages/send-template.json", request);
            return response;
        }

        public void AddKeyToRequest(IRequest request)
        {
            if (string.IsNullOrEmpty(request.Key))
                request.Key = _key;
        }

        public T Post<T>(string path, IRequest data)
        {
           var request = new RestRequest(path, Method.POST) {RequestFormat = DataFormat.Json, JsonSerializer = new CustomJsonSerializer(data.GetType())};
         
            AddKeyToRequest(data);
            request.AddBody(data);
            
            var response = RestClient.Execute(request);

            //if internal server error, then mandrill should return a custom error.
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                    
                    
                    //JSON.Parse<ErrorResponse>(response.Content);
                var ex = new MandrillException(error, string.Format("Post failed {0}, Raw Results: {1}", path, response.Content));
                throw ex;
            }
            else if (response.StatusCode != HttpStatusCode.OK)
            {
                //used to throw errors not returned from the server, such as no response, etc.
                throw response.ErrorException;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }         
        }
    }
}
