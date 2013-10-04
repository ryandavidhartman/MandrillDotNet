using System;
using System.Collections.Generic;
using System.Configuration;
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
            try
            {
               SynchronousAPICalls();
               AsynchronousAPICalls();
               Console.ReadLine();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.ReadLine();
            }

        }


        static void SynchronousAPICalls()
        {
            string key = ConfigurationManager.AppSettings["MandrillKey"];
            string url = ConfigurationManager.AppSettings["MandrillUrl"];
            string fromEmail = ConfigurationManager.AppSettings["MandrillFromEmail"];
            string fromDisplayName = ConfigurationManager.AppSettings["MandrillFromEmailDisplay"];
            string toEmail = ConfigurationManager.AppSettings["MandrillToEmail"];
            string toDisplayName = ConfigurationManager.AppSettings["MandrillToEmailDisplay"];
            
            var madrilTest = new Mandrill(key, url);

            // 1 Ping Mandrill
            var ping = madrilTest.Ping(new PingRequest());
            Console.WriteLine("Ping returns: " + ping);


            // 2 Get Info about the user associated with the current Mandril key
            var getInfo = madrilTest.GetInfo(new GetInfoRequest());
            Console.WriteLine(getInfo.Username);


            // 3 Get Some statistics about recently sent emails
            var senderDataResponses = madrilTest.GetSenderData(new GetSenderDataRequest());
            foreach (var sender in senderDataResponses)
            {
                Console.WriteLine("Sender:{0} Create Date:{1} Opens:{2}", sender.Address, sender.CreatedAt, sender.Opens);
            }


            // 4 Create a new template
            var newTemplate = new PostTemplateRequest
            {
                TemplateName = "Dummy Template",
                FromEmail = fromEmail,
                FromName = fromDisplayName,
                Subject = "My fancy template",
                Code = "<strong>Here is some html for the email body</strong>",
                Text = "Here is some plain text for the body",
                Publish = true
            };

            var response = madrilTest.PostTemplate(newTemplate);
            Console.WriteLine(response.Slug);

            // 5 get a list of all templates
            var templates = madrilTest.GetTemplates(new GetTemplatesRequest());
            foreach (var templateInfo in templates)
            {
                Console.WriteLine("Template Name: {0} Slug: {1}", templateInfo.TemplateName, templateInfo.Slug);
            }

            // 6 Update a template
            response = madrilTest.PutTemplate(new PutTemplateRequest { TemplateName = "Dummy Template", Code = "<strong>Updated!</strong>" });
            Console.WriteLine(response.Code);

            // 7 Delete a template
            var results = madrilTest.DeleteTemplate(new DeleteTemplateRequest { TemplateName = "Dummy Template" });
            Console.WriteLine(results.TemplateName + " was deleted");

            //8 Send a simple email message

            var message1 = new EmailMessage
            {
                To = new List<EmailAddress> { new EmailAddress { Email = toEmail, Name = toDisplayName } },
                FromEmail = fromEmail,
                Subject = "Mandril Test Email",
                Html = "<strong>Html email in the house!</strong>",
                Text = "plain text email on the job"
            };

            var sendResponses1 = madrilTest.SendEmail(new SendEmailRequest { Message = message1 });
            foreach (var sendEmailResponse in sendResponses1)
            {
                Console.WriteLine("Email send results: " + sendEmailResponse.Status);
            }


            //9 Send a templated email with merge variables and and mc:edit region for the footer

            var message2 = new EmailMessage
            {
                To =
                    new List<EmailAddress>
                                {
                                    new EmailAddress {Email = toEmail, Name = toDisplayName}
                                },
                FromEmail = fromEmail,
                FromName = fromDisplayName,
                Html = null,
                Text = null
            };


            //string to = "ryandavidhartman@gmail.com";
            message2.AddGlobalVariable("customername", toDisplayName);
            message2.AddGlobalVariable("orderdate", DateTime.Now.Date.ToShortDateString());
            message2.AddGlobalVariable("invoicedetails", "SMS Data fee $19.99");
            message2.Merge = true;


            var templateContents = new List<TemplateContent>
                    {
                        new TemplateContent {Name = "footer", Content = "Contact us at sales@pumpalarm.com"}
                      
                    };

            var request = new SendEmailWithTemplateRequest
            {
                Message = message2,
                TemplateContent = templateContents,
                TemplateName = "InvoiceTemplate"
            };

            var sendResponses = madrilTest.SendEmail(request);


            foreach (var sendEmailResponse in sendResponses)
            {
                Console.WriteLine("Templated email send results: " + sendEmailResponse.Status);
            }
            
        }

        static void AsynchronousAPICalls()
        {
            string key = ConfigurationManager.AppSettings["MandrillKey"];
            string url = ConfigurationManager.AppSettings["MandrillUrl"];
            string fromEmail = ConfigurationManager.AppSettings["MandrillFromEmail"];
            string fromDisplayName = ConfigurationManager.AppSettings["MandrillFromEmailDisplay"];
            string toEmail = ConfigurationManager.AppSettings["MandrillToEmail"];
            string toDisplayName = ConfigurationManager.AppSettings["MandrillToEmailDisplay"];

            var madrilTest = new Mandrill(key, url);

            Console.WriteLine("1 sending ping request");
            madrilTest.PingAsync(new PingRequest(), PingHandler);
            
            Console.WriteLine("2 sending get info request");
            madrilTest.GetInfoAsync(new GetInfoRequest(), InfoHandler);
            
            Console.WriteLine("3 sending get sender data request");
            madrilTest.GetSenderDataAsync(new GetSenderDataRequest(), SenderDataHandler);

            Console.WriteLine("4 sending get templates request");
            madrilTest.GetTemplatesAsync(new GetTemplatesRequest(), TemaplateListHandler);

            Console.WriteLine("5 sending a create email request");
            var message = new EmailMessage
            {
                To = new List<EmailAddress> { new EmailAddress { Email = toEmail, Name = toDisplayName } },
                FromEmail = fromEmail,
                FromName = fromDisplayName,
                Subject = "Mandril Test Email",
                Html = "<strong>Html email in the house!</strong>",
                Text = "plain text email on the job"
            };

            madrilTest.SendEmailAsync(new SendEmailRequest { Message = message }, SendEmailHandler);
        }
        
        // Sample callback for the async ping call
        public static void PingHandler(string response)
        {
            Console.WriteLine("Handling Ping response");
            if (response != null && "PONG!".Equals(response))
                Console.WriteLine("Ping Success");
            else
                Console.WriteLine("Ping Failure");
        }

        // Sample callback for the assync get info call
        public static void InfoHandler(GetInfoResponse getInfo)
        {
            Console.WriteLine("Handling GetInfoReponse response");
            Console.WriteLine("{0} has an hourly quota of {1} emails", getInfo.Username, getInfo.HourlyQuota);}
        
        // Sample callback for the get sender data call
        private static void SenderDataHandler(List<SenderDataResponse> senderDataResponses)
        {
            Console.WriteLine("Handling SenderDataResponse response");
            foreach (var sender in senderDataResponses)
            {
                Console.WriteLine("Sender:{0} Create Date:{1} Opens:{2}", sender.Address, sender.CreatedAt, sender.Opens);
            }
        }

        // Sample callback for the get template list request
        private static void TemaplateListHandler(List<Template> templates)
        {
            Console.WriteLine("Handling Get Templates response");
            foreach (var templateInfo in templates)
            {
                Console.WriteLine("Template Name: {0} Slug: {1}", templateInfo.TemplateName, templateInfo.Slug);
            }
        }

        // Sample callback for the send email call
        private static void SendEmailHandler(List<SendEmailResponse> sendResponses)
        {
            Console.WriteLine("Handling Send Email response");
           foreach (var sendEmailResponse in sendResponses)
            {
                Console.WriteLine("Email send results To:{0} Status:{1}", sendEmailResponse.Email, sendEmailResponse.Status);
            }
        }
    }
}
