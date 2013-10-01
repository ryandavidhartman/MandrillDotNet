using System;
using System.Collections.Generic;
using MandrillWrapper;
using MandrillWrapper.Model.Data;

namespace MandrilDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var madrilTest = new MandrillAPI("uKVPH3OmNPw4h-iw0PSuHA", "http://mandrillapp.com/api/1.0");
                var ping = madrilTest.Ping();
                Console.WriteLine("Ping returns: " + ping);
                var info = madrilTest.Info();
                Console.WriteLine(info.username);

                var sendData = madrilTest.GetSenderData();
                foreach (var sender in sendData)
                {
                    Console.WriteLine(string.Format("Sender:{0} Create Date:{1} Opens:{2}", sender.Address, sender.CreatedAt, sender.Opens));
                }

                var templates = madrilTest.GetTemplates();
                foreach (var templateInfo in templates)
                {
                    Console.WriteLine(templateInfo.name);
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
                    Console.WriteLine("Templated email send results: " + sendEmailResponse.status);
                }


                Console.ReadLine();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.ReadLine();
            }

        }
    }
}
