using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ITI_Project.BLL.Services.Impelemntation
{
    public class EmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
            emailMessage.To.Add(MailboxAddress.Parse(email));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    // If you need to accept all SSL certificates (for testing), uncomment the line below
                    // smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    var secureSocketOptions = SecureSocketOptions.StartTlsWhenAvailable;

                    // Adjust the secure socket options based on your SMTP server requirements
                    smtpClient.Connect(emailSettings["MailServer"], int.Parse(emailSettings["MailPort"]), secureSocketOptions);

                    // Remove the OAuth authentication if not needed
                    smtpClient.Authenticate(emailSettings["Username"], emailSettings["Password"]);

                    await smtpClient.SendAsync(emailMessage);
                    Console.WriteLine("Email sent successfully!");
                }
                catch (SmtpCommandException ex)
                {
                    Console.WriteLine($"SMTP Command Error: {ex.StatusCode} - {ex.Message}");
                }
                catch (SmtpProtocolException ex)
                {
                    Console.WriteLine($"SMTP Protocol Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General Error sending email: " + ex.Message);
                }
                finally
                {
                    await smtpClient.DisconnectAsync(true);
                }
            }
        }
    }
}
