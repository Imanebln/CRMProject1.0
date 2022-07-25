using CRMServer.Models.Email;

namespace EmailService
{
    public interface IEmailSender
    {
        void SendEmail(Email email);
        Task SendEmailAsync(Email email);

    }
}
