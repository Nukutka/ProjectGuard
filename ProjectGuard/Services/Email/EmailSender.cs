using Abp.Dependency;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using ProjectGuard.Ef.Entities;
using System;
using System.Linq;

namespace ProjectGuard.Services.Email
{
    public class EmailSender : ISingletonDependency, IDisposable
    {
        private readonly string[] _recipients;
        private readonly string _email;
        private readonly IConfiguration _configuration;

        private SmtpClient _smtpClient;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _email = _configuration.GetSection("Email:Email").Value;
            var password = _configuration.GetSection("Email:Password").Value;
            var host = _configuration.GetSection("Email:Host").Value;
            var port = int.Parse(_configuration.GetSection("Email:Port").Value);
            var recipientsStr = _configuration.GetSection("Email:Recipients").Value;

            _smtpClient = new SmtpClient();
            _smtpClient.Connect(host, port, true);
            _smtpClient.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            _smtpClient.Authenticate(_email, password);

            _recipients = recipientsStr
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(r => r.Trim())
                .ToArray();
        }

        public MimeMessage GetBadMailMessage(Verification verification)
        {
            var mailMessage = new MimeMessage();

            mailMessage.Subject = "Project guard неудачная проверка";
            mailMessage.From.Add(MailboxAddress.Parse(_email));

            foreach (var recipient in _recipients)
            {
                mailMessage.To.Add(MailboxAddress.Parse(recipient));
            }

            var files = "";
            foreach (var res in verification.FileCheckResults)
            {
                if (!res.Result)
                {
                    files += $"{res.HashValue.FileName} - {res.Message}<br />";
                }
            }

            mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"{verification.CreationTime} неудачная проверка проекта {verification.Project.Name}<br />{files}"
            };

            return mailMessage;
        }

        public void SendBadVerification(Verification verification)
        {
            _smtpClient.Send(GetBadMailMessage(verification));
        }


        public void Dispose()
        {
            _smtpClient.Dispose();
        }
    }
}
