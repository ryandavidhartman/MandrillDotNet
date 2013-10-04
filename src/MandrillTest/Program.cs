using System;
using System.Collections.Generic;
using MandrillWrapper;
using MandrillWrapper.Model.Data;
using MandrillWrapper.Model.Requests;
using MandrillWrapper.Model.Responses;

namespace MandrilDotNet
{
    class Program
    {
        static void Main()
        {
            Action<string> asyncPingHandler = PingHandler;
            Action<GetInfoResponse> asyncInfoHandler = InfoHandler;
            Action<List<SenderDataResponse>> asyncSenderDataHandler = SenderDataHandler;
            Action<List<SendEmailResponse>> asnySendEmailHandler = SendEmailHandler;


            try
            {
                var madrilTest = new MandrillAPI("uKVPH3OmNPw4h-iw0PSuHA", "http://mandrillapp.com/api/1.0");

                /*
                var ping = madrilTest.Ping(new PingRequest());
                Console.WriteLine("Ping returns: " + ping);

                var getInfo = madrilTest.GetInfo(new GetInfoRequest());
                Console.WriteLine(getInfo.Username);

                
                var senderDataResponses = madrilTest.GetSenderData(new GetSenderDataRequest());
                foreach (var sender in senderDataResponses)
                {
                    Console.WriteLine(string.Format("Sender:{0} Create Date:{1} Opens:{2}", sender.Address, sender.CreatedAt, sender.Opens));
                }

                var newTemplate = new PostTemplateRequest
                    {
                        TemplateName = "Dummy Template",
                        FromEmail = "rhartman@omnisite.com",
                        FromName = "Ryan Hartman",
                        Subject = "My fancy template",
                        Code = "<strong>Here is some html for the email body</strong>",
                        Text = "Here is some plain text for the body",
                        Publish = true
                    };


                var response = madrilTest.PostTemplate(newTemplate);
                Console.WriteLine(response.Slug);


                response = madrilTest.PutTemplate(new PutTemplateRequest { TemplateName = "Dummy Template", Code = "<strong>Updated!</strong>" });
                Console.WriteLine(response.Code);
                Console.ReadLine();

                var results = madrilTest.DeleteTemplate(new DeleteTemplateRequest {TemplateName = "Dummy Template"});
               
                var templates = madrilTest.GetTemplates(new GetTemplatesRequest());
                foreach (var templateInfo in templates)
                {
                    Console.WriteLine("Template Name: {0} Slug: {1}", templateInfo.TemplateName, templateInfo.Slug);
                }

                /*
                var message = new EmailMessage
                 {
                     to = new List<EmailAddress> { new EmailAddress { email = "ryandavidhartman@gmail.com", name = "Ryan Hartman" } },
                     from_email = "rhartman@omnisite.com",
                     subject = "Mandril Test Email",
                     html = "<strong>Html email in the house!</strong>",
                     text = "plain text email on the job"
                 };

                var sendResponses = madrilTest.SendEmail(message);
                foreach (var sendEmailResponse in sendResponses)
                {
                    Console.WriteLine("No template email send results: " + sendEmailResponse.status);
                }*/
                /*
                 
                var message = new EmailMessage
                    {
                        To =
                            new List<EmailAddress>
                                {
                                    new EmailAddress {Email = "ryandavidhartman@gmail.com", Name = "Ryan"}
                                },
                        FromEmail = "rhartman@omnisite.com",
                        FromName =  "Ryan Hartman",
                        Html = null,
                        Text = null
                    };


                //string to = "ryandavidhartman@gmail.com";
                message.AddGlobalVariable("customername", "Bob Smith");
                message.AddGlobalVariable("orderdate", DateTime.Now.Date.ToShortDateString());
                message.AddGlobalVariable("invoicedetails", "SMS Data fee $19.99");
                message.Merge = true;

                
                var tc = new List<TemplateContent>
                    {
                        new TemplateContent {Name = "footer", Content = "Contact us at sales@pumpalarm.com"}
                      
                    };

                var sendResponses = madrilTest.SendEmail(message, "invoicetemplate", tc);

                
                foreach (var sendEmailResponse in sendResponses)
                {
                    Console.WriteLine("Templated email send results: " + sendEmailResponse.Status);
                }
                */


                // async
                
                Console.WriteLine("1");
                madrilTest.PingAsync(new PingRequest(), asyncPingHandler);
                Console.WriteLine("2");
                madrilTest.GetInfoAsync(new GetInfoRequest(), asyncInfoHandler);
                Console.WriteLine("3");
                madrilTest.GetSenderDataAsync(new GetSenderDataRequest(), asyncSenderDataHandler);
                Console.WriteLine("4");

                var message = new EmailMessage
                {
                    To = new List<EmailAddress> { new EmailAddress { Email = "ryandavidhartman@gmail.com", Name = "Ryan Hartman" } },
                    FromEmail = "rhartman@omnisite.com",
                    Subject = "Mandril Test Email",
                    Html = "<strong>Html email in the house!</strong>",
                    Text = "plain text email on the job"
                };

                madrilTest.SendEmailAsync(new SendEmailRequest { Message = message }, SendEmailHandler);
                Console.WriteLine("5");
               
                Console.ReadLine();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.ReadLine();
            }

        }

        public static void PingHandler(string response)
        {
            if (response != null && "PONG!".Equals(response))
                Console.WriteLine("Ping Success");
            else
                Console.WriteLine("Ping Failure");
        }

        public static void InfoHandler(GetInfoResponse getInfo)
        {
            Console.WriteLine("{0} has an hourly quota of {1} emails", getInfo.Username, getInfo.HourlyQuota);
        }

        private static void SenderDataHandler(List<SenderDataResponse> senderDataResponses)
        {
            foreach (var sender in senderDataResponses)
            {
                Console.WriteLine("Sender:{0} Create Date:{1} Opens:{2}", sender.Address, sender.CreatedAt, sender.Opens);
            }
        }

        private static void SendEmailHandler(List<SendEmailResponse> sendResponses)
        {
           foreach (var sendEmailResponse in sendResponses)
            {
                Console.WriteLine("Email send results To:{0} Status:{1}", sendEmailResponse.Email, sendEmailResponse.Status);
            }
        }
    }
}
