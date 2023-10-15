using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;
using Ecommerce.Application.Infrastructure;

namespace Ecommerce.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  bool SendEmail()
        {

            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailSender:EmailFrom").Value));
            email.To.Add(MailboxAddress.Parse(_configuration.GetSection("EmailSender:EmailTo").Value));
            email.Subject = _configuration.GetSection("EmailSender:Subject").Value;
            var message = _configuration.GetSection("EmailSender:Message").Value;
            email.Body = new TextPart(TextFormat.Html) { Text = message?.Trim() };

            bool isSend = false;
            // send email
            using (var smtp = new SmtpClient())
            {
                try
                {
                    isSend = Send(email, smtp) != null || Send(email, smtp) == string.Empty? 
                        true: false;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    smtp.Disconnect(true);
                    smtp.Dispose();
                }
                return isSend;
            }

        }


        private  string Send(MimeMessage email, SmtpClient smtp)
        {
            var SmtpHost = _configuration.GetSection("EmailSender:SmtpHost").Value;
            var SmtpPort = int.Parse(_configuration.GetSection("EmailSender:SmtpPort").Value);
            var SmtpUser = _configuration.GetSection("EmailSender:SmtpUser").Value;
            var SmtpPass = _configuration.GetSection("EmailSender:SmtpPass").Value;
            smtp.Connect(SmtpHost, SmtpPort, SecureSocketOptions.StartTls);
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            smtp.Authenticate(SmtpUser, SmtpPass);
            return smtp.Send(email);
        }
    }
}
