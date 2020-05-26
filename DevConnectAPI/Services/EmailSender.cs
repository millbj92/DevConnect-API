using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DevConnectAPI.Services
{
    public class SendGridOptions
    {
        /*public string Server { get; set; }
        public int Port { get; set; }
        public string FromAddress { get; set; }
        public string Password { get; set; }*/
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }

    public class EmailSender : IEmailSender
    {
        private readonly SendGridOptions Options;

        public EmailSender(IOptions<SendGridOptions> options)
        {
            Options = options.Value;

        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("admin@brandonmiller.io", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
