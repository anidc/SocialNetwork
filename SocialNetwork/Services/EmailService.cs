using SocialNetwork.Dtos.Account;
using SocialNetwork.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace SocialNetwork.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("SocialNetwork", "anidcamdzic@gmail.com"));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = subject;
        message.Body = new TextPart("html")
        {
            Text = body
        };

        using var client = new SmtpClient();

        await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync("anidcamdzic@gmail.com", "zgpr pygo nasn dcow");
        await client.SendAsync(message);
        Console.WriteLine("Email sent successfully");
    }
}