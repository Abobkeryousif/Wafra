using Microsoft.Extensions.Options;
using MimeKit;
using Wafra.Application.Contracts.Services;
using Wafra.Core.Common;
using MailKit.Net.Smtp;


namespace Wafra.Infrastructure.Repository
{
    public class SendEmail : ISendEmail
    {
        private readonly MailSetting _setting;

        public SendEmail(IOptions<MailSetting> setting)
        {
            _setting = setting.Value;
        }

        void ISendEmail.SendEmail(string mailTo, string subject, string message)
        {
            using(var Client = new SmtpClient()) 
            {
                Client.Connect(_setting.Host, _setting.Port);
                Client.Authenticate(_setting.Email, _setting.Password);

                var bodybuilder = new BodyBuilder
                {
                    HtmlBody = message,
                     TextBody = "Hello"
                };

                var Message = new MimeMessage
                {
                    Body = bodybuilder.ToMessageBody()
                };

                Message.From.Add(new MailboxAddress("Wafra Team", _setting.Email));
                Message.To.Add(new MailboxAddress("Mr", mailTo));
                Message.Subject = subject;
                Client.Send(Message);
                Client.Disconnect(true);
            }
        }
    }
}

    