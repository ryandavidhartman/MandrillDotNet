using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
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
            var response = AsyncWrapper<PingRequest, string>(request, PingAsync);
            return response != null && "PONG!".Equals(response);
        }

        public void PingAsync(PingRequest request, Action<string> callBack)
        {
            PostAsync("/users/ping.json", request, callBack);
        }

        public GetInfoResponse GetInfo(GetInfoRequest request)
        {
            return AsyncWrapper<GetInfoRequest, GetInfoResponse>(request, GetInfoAsync);
        }

        public void GetInfoAsync(GetInfoRequest request, Action<GetInfoResponse> callBack)
        {
            PostAsync("/users/info.json", request, callBack);
        }

        public List<SenderDataResponse> GetSenderData(GetSenderDataRequest request)
        {
            return AsyncWrapper<GetSenderDataRequest, List<SenderDataResponse>>(request, GetSenderDataAsync);
        }

        public void GetSenderDataAsync(GetSenderDataRequest request, Action<List<SenderDataResponse>> callback)
        {
            PostAsync("/users/senders.json", request, callback);
        }

        public List<Template> GetTemplates(GetTemplatesRequest request)
        {
            return AsyncWrapper<GetTemplatesRequest, List<Template>>(request, GetTemplatesAsync);
        }

        public void GetTemplatesAsync(GetTemplatesRequest request, Action<List<Template>> callback)
        {
            PostAsync("/templates/list.json", request, callback);
        }

        public Template PostTemplate(PostTemplateRequest request)
        {
            return AsyncWrapper<PostTemplateRequest, Template>(request, PostTemplateAsync);
        }

        public void PostTemplateAsync(PostTemplateRequest request, Action<Template> callback)
        {
            PostAsync("/templates/add.json", request, callback);
        }

        public Template PutTemplate(PutTemplateRequest request)
        {
            return AsyncWrapper<PutTemplateRequest, Template>(request, PutTemplateAsync);
        }

        public void PutTemplateAsync(PutTemplateRequest request, Action<Template> callback)
        {
            PostAsync("/templates/update.json", request, callback);
        }

        public Template DeleteTemplate(DeleteTemplateRequest request)
        {
            return AsyncWrapper<DeleteTemplateRequest, Template>(request, DeleteTemplateAsync);
        }

        public void DeleteTemplateAsync(DeleteTemplateRequest request, Action<Template> callback)
        {
            PostAsync("/templates/delete.json", request, callback);
        }

        public List<SendEmailResponse> SendEmail(SendEmailRequest request)
        {
            return AsyncWrapper<SendEmailRequest, List<SendEmailResponse>>(request, SendEmailAsync);
        }

        public void SendEmailAsync(SendEmailRequest request, Action<List<SendEmailResponse>> callback)
        {
            PostAsync("/messages/send.json", request, callback);
        }

        public List<SendEmailResponse> SendEmailWithTemplate(SendEmailWithTemplateRequest request)
        {
            return AsyncWrapper<SendEmailWithTemplateRequest, List<SendEmailResponse>>(request, SendEmailWithTemplateAsync);
        }

        public void SendEmailWithTemplateAsync(SendEmailWithTemplateRequest request, Action<List<SendEmailResponse>> callback)
        {
            PostAsync("/messages/send-template.json", request, callback);
        }

        // Helpers

        public void AddKeyToRequest(IRequest request)
        {
            if (string.IsNullOrEmpty(request.Key))
                request.Key = _key;
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

        public TV AsyncWrapper<T, TV>(T request, Action<T, Action<TV>> asynPoster) where TV : class
        {
            TV response = null;
            var manualResetEvent = new ManualResetEvent(false);

            asynPoster(request, r => { response = r; manualResetEvent.Set(); });

            manualResetEvent.WaitOne();
            return response;
        }
    }
}
