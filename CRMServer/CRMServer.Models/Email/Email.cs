namespace CRMServer.Models.Email
{
    public class Email
    {
        //public List<MailboxAddress> To { get; set; }
        public string To { get; set; } = string.Empty;
        public string From{ get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;

        public EmailBody EmailBody { get; set; } = new EmailBody();
    }
}
