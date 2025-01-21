using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace PTTS.Infrastructure.Services;
public class EmailSender(IConfiguration configuration) : IEmailSender
{
    private readonly IConfiguration _configuration = configuration;

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using var smtp = new SmtpClient(_configuration["Email:Smtp:Host"])
        {
            Port = int.Parse(_configuration["Email:Smtp:Port"] ?? "587"),
            Credentials = new NetworkCredential(
                  _configuration["Email:Smtp:Username"],
                  _configuration["Email:Smtp:Password"]),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["Email:From"] ?? "example@gmail.com"),
            Subject = subject,
            Body = message,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        await smtp.SendMailAsync(mailMessage);
    }
}