using SocialNetwork.Dtos.Account;

namespace SocialNetwork.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}