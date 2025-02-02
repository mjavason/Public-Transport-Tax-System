using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace PTTS.Infrastructure.Services
{
	public class EmailSender : IEmailSender
	{

		private readonly IConfiguration _configuration;
		public EmailSender(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendEmailAsync(string email, string subject, string message)
		{
			var smtpSettings = _configuration.GetSection("Email:Smtp");

			var mimeMessage = new MimeMessage();
			mimeMessage.From.Add(new MailboxAddress("PTTS System", _configuration["Email:From"] ?? "donotreply@example.com"));
			mimeMessage.To.Add(new MailboxAddress(email, email));
			mimeMessage.Subject = subject;
			mimeMessage.Body = new TextPart("html") { Text = message };

			// Create a new SMTP client using MailKit
			using (var client = new SmtpClient())
			{
				// Connect to the SMTP server using the settings
				await client.ConnectAsync(smtpSettings["Host"], int.Parse(smtpSettings["Port"] ?? "587"), true);

				// Authenticate with the SMTP server using the credentials
				await client.AuthenticateAsync(smtpSettings["Username"], smtpSettings["Password"]);

				// Send the email message
				await client.SendAsync(mimeMessage);

				// Disconnect from the SMTP server
				await client.DisconnectAsync(true);
			}
		}
	}
}
