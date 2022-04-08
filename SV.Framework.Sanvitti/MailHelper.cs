using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace SV.Framework.Sanvitti
{
    public class MailHelper
    {
        private IConfiguration configuration;

        public MailHelper(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public bool SendEmail(string from, string subject, string content)
        {
            //, List<string> attachments
            try 
            {
                //po

                var host = configuration["EmailSettings:Host"];
                var port = int.Parse(configuration["EmailSettings:Port"]);
                var email = configuration["EmailSettings:Email"];
                var password = configuration["EmailSettings:Password"];
               // var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);

                var smtpClient = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    //EnableSsl = enable,
                    Credentials = new NetworkCredential(email, password)
                };
                var mailMessage = new MailMessage(from, email);
                mailMessage.Subject = subject;
                mailMessage.Body = content;
                mailMessage.IsBodyHtml = true;
                //if (attachments != null)
                //{
                //    foreach (var attachment in attachments)
                //    {
                //        mailMessage.Attachments.Add(new Attachment(attachment));
                //    }
                //}

                smtpClient.Send(mailMessage);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}