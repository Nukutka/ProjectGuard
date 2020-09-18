using Abp.Dependency;
using MailKit.Net.Smtp;
using MimeKit;
using ProjectGuard.Ef.Entities;

namespace ProjectGuard.Services.Email
{
    public class EmailSender : ISingletonDependency
    {
        private readonly string[] _recipients;

        public EmailSender()
        {
            // TODO: appsettins
            _recipients = new string[] { "nikitka6751@gmail.com" };
        }

        public MimeMessage GetBadMailMessage(Verification verification, string recipient)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(MailboxAddress.Parse("snaffuxgreh@yandex.ru"));
            mailMessage.To.Add(MailboxAddress.Parse(recipient));
            mailMessage.Subject = "Project guard неудачная проверка";

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
            using var smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.yandex.ru", 465, true);
            smtpClient.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            smtpClient.Authenticate("snaffuxgreh@yandex.ru", "qwerty123456");

            foreach (var recipient in _recipients)
            {
                smtpClient.Send(GetBadMailMessage(verification, recipient));
            }

            smtpClient.Disconnect(true);
        }
    }
}
