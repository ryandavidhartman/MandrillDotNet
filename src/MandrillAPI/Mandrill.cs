using System;
using System.Collections.Generic;
using System.Net;
using MandrillAPI.Model.Data;
using MandrillAPI.Model.Requests;
using MandrillAPI.Model.Responses;
using MandrillAPI.Utilities;
using RestSharp;
using Newtonsoft.Json;

namespace MandrillAPI
{
    public class Mandrill
    {
        private readonly string _key;
        private readonly string _url;
        public RestClient RestClient { get; set; }

        public Mandrill(string key, string url)
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

        public void PingAsync(PingRequest request, Action<string> callBack)
        {
            PostAsync("/users/ping.json", request, callBack);
        }

        public GetInfoResponse GetInfo(GetInfoRequest request)
        {
           
            var response = Post<GetInfoResponse>("/users/info.json", request);
            return response;
        }

        public void GetInfoAsync(GetInfoRequest request, Action<GetInfoResponse> callBack)
        {
            PostAsync("/users/info.json", request, callBack);
        }

        public List<SenderDataResponse> GetSenderData(GetSenderDataRequest request)
        {
            var senderData = Post<List<SenderDataResponse>>("/users/senders.json", request );
            return senderData;
        }

        public void GetSenderDataAsync(GetSenderDataRequest request, Action<List<SenderDataResponse>> callback)
        {
            PostAsync("/users/senders.json", request, callback);
        }

        public List<Template> GetTemplates(GetTemplatesRequest request)
        {
            var templates = Post<List<Template>>("/templates/list.json", request);
            return templates;
        }

        public void GetTemplatesAsync(GetTemplatesRequest request, Action<List<Template>> callback)
        {
            PostAsync("/templates/list.json", request, callback);
        }

        public Template PostTemplate(PostTemplateRequest request)
        {
            var response = Post<Template>("/templates/add.json", request);
            return response;

        }

        public void PostTemplateAsync(PostTemplateRequest request, Action<Template> callback)
        {
            PostAsync("/templates/add.json", request, callback);
        }

        public Template PutTemplate(PutTemplateRequest request)
        {
            var response = Post<Template>("/templates/update.json", request);
            return response;
        }

        public void PutTemplateAsync(PutTemplateRequest request, Action<Template> callback)
        {
            PostAsync("/templates/update.json", request, callback);
        }

        public Template DeleteTemplate(DeleteTemplateRequest request)
        {
            var response = Post<Template>("/templates/delete.json", request);
            return response;
        }

        public void DeleteTemplateAsync(PostTemplateRequest request, Action<Template> callback)
        {
            PostAsync("/templates/delete.json", request, callback);
        }

        public List<SendEmailResponse> SendEmail(SendEmailRequest request)
        {
            var response = Post<List<SendEmailResponse>>("/messages/send.json", request);
            return response;
        }

        public void SendEmailAsync(SendEmailRequest request, Action<List<SendEmailResponse>> callback)
        {
            PostAsync("/messages/send.json", request, callback);
        }

        public List<SendEmailResponse> SendEmail(SendEmailWithTemplateRequest request)
        {
            var response = Post<List<SendEmailResponse>>("/messages/send-template.json", request);
            return response;
        }

        public void SendEmail(SendEmailWithTemplateRequest request, Action<List<SendEmailResponse>> callback)
        {
            PostAsync("/messages/send-template.json", request, callback);
        }

        // Helpers

        public void AddKeyToRequest(IRequest request)
        {
            if (string.IsNullOrEmpty(request.Key))
                request.Key = _key;
        }

        public T Post<T>(string path, IRequest data)
        {
            var request = new RestRequest(path, Method.POST) { RequestFormat = DataFormat.Json, JsonSerializer = new CustomJsonSerializer(data.GetType()) };

            AddKeyToRequest(data);
            request.AddBody(data);

            var response = RestClient.Execute(request);

            //if internal server error, then mandrill should return a custom error.
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);


                //JSON.Parse<ErrorResponse>(response.Content);
                var ex = new MandrillException(error, string.Format("Post failed {0}, Raw Results: {1}", path, response.Content));
                throw ex;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                //used to throw errors not returned from the server, such as no response, etc.
                throw response.ErrorException;
            }

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public RestRequestAsyncHandle PostAsync<T>(string path, IRequest data, Action<T> callBack)
        {
            var request = new RestRequest(path, Method.POST) { RequestFormat = DataFormat.Json, JsonSerializer = new CustomJsonSerializer(data.GetType()) };

            AddKeyToRequest(data);
            request.AddBody(data);

            var handle =RestClient.ExecuteAsync(request, response =>
                {
                    //if internal server error, then mandrill should return a custom error.
                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        var error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                        var ex = new MandrillException(error, string.Format("Post failed {0}, Raw Results: {1}", path, response.Content));
                        throw ex;
                    }

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        //used to throw errors not returned from the server, such as no response, etc.
                        throw response.ErrorException;
                    }

                    callBack(JsonConvert.DeserializeObject<T>(response.Content));

                });

            return handle;
        }
        
    }
}
