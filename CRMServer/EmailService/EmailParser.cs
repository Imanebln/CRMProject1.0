using MimeKit;
using System.Text;
using CRMServer.Models.Email;

namespace EmailService {
	public class EmailParser {
		public static readonly string DefaultEmailTemplate = File.ReadAllText(@"resources\email.html", Encoding.UTF8);
		public static readonly string DefaultButtonTemplate = File.ReadAllText(@"resources\button.html", Encoding.UTF8);
		
		private static readonly Dictionary<EmailType, string[]> IconResolver = new(){
			{ EmailType.success, new string[] { "https://i.ibb.co/1GW7c4V/checked.png", "#32ba7c" } },
			{ EmailType.info, new string[] { "https://i.ibb.co/8PfpSrv/info.png", "#08a8ee" } },
			{ EmailType.warning, new string[] { "https://i.ibb.co/5RYdnQr/warning.png", "#ffc146" } },
			{ EmailType.danger, new string[] { "https://i.ibb.co/wJ0P5qG/danger.png", "#ff6174" } }
		};

		public static string GetStyledBody(EmailBody emailBody){
			string EmailTemplate = DefaultEmailTemplate;
			EmailTemplate = EmailTemplate.Replace("{{EMAIL_ICON}}", IconResolver[emailBody.Type][0]);
			EmailTemplate = EmailTemplate.Replace("{{EMAIL_TITLE}}", emailBody.Title);
			EmailTemplate = EmailTemplate.Replace("{{EMAIL_MESSAGE}}", emailBody.Message);
			EmailTemplate = EmailTemplate.Replace("{{EMAIL_BUTTONS}}", ButtonToHtml(emailBody.Buttons, emailBody.Type));

			return EmailTemplate;
		}

		private static string ButtonToHtml(IList<Button> buttons, EmailType type){
			StringBuilder ButtonHtml = new();
			foreach(Button button in buttons){
				string btnTemplate = DefaultButtonTemplate;
				btnTemplate = btnTemplate.Replace("{{BUTTON_TEXT}}", button.Text);
				btnTemplate = btnTemplate.Replace("{{BUTTON_LINK}}", button.Link);
				btnTemplate = btnTemplate.Replace("{{BUTTON_COLOR}}", IconResolver[type][1]);
				ButtonHtml.Append(btnTemplate);
			}
			return ButtonHtml.ToString();
		}
	}
}
