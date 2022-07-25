using CRMServer.Models.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using MimeKit;

namespace EmailService {
	public class PrettyEmail : EmailSender, IPrettyEmail {
        private readonly IConfiguration StdEmails;
		public PrettyEmail(EmailConfiguration configuration) : base(configuration) {
            StdEmails = new ConfigurationBuilder().AddJsonFile(@"resources\DefaultEmail.json").Build();
        }

		protected override MimeMessage CreateEmailMessage(Email email) {
			var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.DisplayName, _emailConfiguration.From));
            emailMessage.To.Add(MailboxAddress.Parse(email.To));
            emailMessage.Subject = email.Subject;
            BodyBuilder builder = new();
            builder.HtmlBody = EmailParser.GetStyledBody(email.EmailBody);
            emailMessage.Body = builder.ToMessageBody();
            return emailMessage;
		}

        public void SendRegister(string to, string link){
            Email email = GetEmail("VALIDATION", link);
            email.To = to;
            SendEmail(email);
		}

        public void SendPasswordReset(string to, string link) {
            Email email = GetEmail("PASSWORD_RESET", link);
            email.To = to;
            SendEmail(email);
        }

        private Email GetEmail(string EmailName, string? link){
            Email email = new() {
                Subject = getString(EmailName, "Subject"),
            };
            email.EmailBody.Title = getString(EmailName, "Title");
            email.EmailBody.Message = getString(EmailName, "Message");
            email.EmailBody.Type = (EmailType)Enum.Parse(typeof(EmailType), getString(EmailName, "Type"), true);
 
            if (link != null){
                email.EmailBody.Buttons.Add(new Button() {
                    Link = link,
                    Text = getString(EmailName, "ButtonText")
                });
            }

            return email;

            string getString(string name,string local){
                return StdEmails[$"{name}:{local}"];
			}
        }
	}
}
