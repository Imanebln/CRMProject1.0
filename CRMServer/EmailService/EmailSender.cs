using CRMServer.Models.Email;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        protected readonly EmailConfiguration _emailConfiguration;

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        public void SendEmail(Email email)
        {
            var emailMessage = CreateEmailMessage(email);

            Send(emailMessage);
        }

        public async Task SendEmailAsync(Email email)
        {
            var emailMessage = CreateEmailMessage(email);

            await SendAsync(emailMessage);

        }

        protected virtual MimeMessage CreateEmailMessage(Email email)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfiguration.From));
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.DisplayName, _emailConfiguration.From));
            emailMessage.To.Add(MailboxAddress.Parse(email.To));
            emailMessage.Subject = email.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = email.Content +"\n"+ email.Link};
            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                   await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                   client.AuthenticationMechanisms.Remove("XOAUTH2");
                   await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);
                   await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }

            }
        }
        private  void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                     client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                     client.AuthenticationMechanisms.Remove("XOAUTH2");
                     client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
                     client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }

            }
        }
    }
}
