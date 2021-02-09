using InsuranceClaims.Core.Common;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceClaims.Services.SendEmail
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _frontEndURL;
        public EmailService(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;

            // front url
            _frontEndURL = _configuration["ClientSubscriber:Url"];
        }

        public async Task Send(EmailMessage emailMessage)
        {
            try
            {
                var message = new MimeMessage();

                // Prepare the email object settings [to, cc, from]
                message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

                if (emailMessage.CcAddresses != null && emailMessage.CcAddresses.Count > 0)
                {
                    message.Cc.AddRange(emailMessage.CcAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                }

                var fromEmail = _configuration["EmailConfiguration:FromEmail"];
                message.From.Add(new MailboxAddress(fromEmail, fromEmail));

                // Prepare email subject
                message.Subject = emailMessage.Subject;

                // Prepare email content
                message.Body = emailMessage.Body;

                // Authenticate then send email
                using (var emailClient = new SmtpClient() { })
                {
                    emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;


                    // Connect to server with account credentials and ssl settings
                    emailClient.Connect(_configuration["EmailConfiguration:SmtpServer"], int.Parse(_configuration["EmailConfiguration:SmtpPort"]));
                    // emailClient();

                    //Remove any OAuth functionality as we won't be using it. 
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Authenticate the connection to server
                    emailClient.Authenticate(_configuration["EmailConfiguration:SmtpUsername"], _configuration["EmailConfiguration:SmtpPassword"]);

                    // Send email
                    await emailClient.SendAsync(message);

                    // Disconnect the object
                    emailClient.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /*
         * Helper Function
        */
        private string GetTemplatePath(string templateName)
        {
            var pathToFile = _hostingEnvironment.WebRootPath
                    + Path.DirectorySeparatorChar.ToString()
                    + "EmailTemplates"
                    + Path.DirectorySeparatorChar.ToString()
                    + templateName;
            return pathToFile;
        }


        /*
         * Templates
        */

        public async Task AfterRegistiration(string email, string vaildToken)
        {
            var redirectPage = $"{_frontEndURL}/auth/reset-password?email={email}&token={vaildToken}";

            // Get TemplateFile located at wwwroot/EmailTemplates/AfterRegistiration.html
            var pathToFile = GetTemplatePath("AfterRegistiration.html");

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = string.Format(SourceReader.ReadToEnd(), redirectPage);
            }

            var subject = $"Registration Success";
            var emailMessage = new EmailMessage()
            {
                Body = builder.ToMessageBody(),
                Subject = subject,
                ToAddresses = new List<EmailAddress>()
                {
                    new EmailAddress() { Address = email, Name = email }
                }
            };

            await Send(emailMessage);
        }
        public async Task RequestToResetPassword(string email, string validToken)
        {
            var redirectPage = $"{_frontEndURL}/auth/reset-password?email={email}&token={validToken}";

            // Get TemplateFile located at wwwroot/EmailTemplates/RequestToResetPassword.html
            var pathToFile = GetTemplatePath("RequestToResetPassword.html");

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = string.Format(SourceReader.ReadToEnd(), redirectPage);
            }

            var subject = $"Reset the Password";
            var emailMessage = new EmailMessage()
            {
                Body = builder.ToMessageBody(),
                Subject = subject,
                ToAddresses = new List<EmailAddress>()
                {
                    new EmailAddress() { Address = email, Name = email }
                }
            };

            await Send(emailMessage);
        }
    }
}
