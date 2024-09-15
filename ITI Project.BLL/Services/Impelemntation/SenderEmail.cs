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

        //public async Task SendEmailAsync1(string email, string subject, string htmlMessage)
        //{
        //    var emailSettings = _configuration.GetSection("EmailSettings");
        //    var smtpClient = new SmtpClient(emailSettings["MailServer"])
        //    {
        //        Port = int.Parse(emailSettings["MailPort"]),
        //        Credentials = new NetworkCredential(emailSettings["Username"], emailSettings["Password"]),
        //        EnableSsl = true,
        //    };

        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress(emailSettings["SenderEmail"], emailSettings["SenderName"]),
        //        Subject = subject,
        //        Body = htmlMessage,
        //        IsBodyHtml = true,
        //    };

        //    mailMessage.To.Add(email);
        //    try
        //    {
        //        smtpClient.Send(mailMessage);
        //        Console.WriteLine("Email sent successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error sending email: " + ex.Message);
        //    }
        //    await smtpClient.SendMailAsync(mailMessage);
        //}

        //public async Task SendEmailAsync2(string email, string subject, string htmlMessage)
        //{
        //    var emailSettings = _configuration.GetSection("EmailSettings");
        //    using (var smtpClient = new SmtpClient(emailSettings["MailServer"], int.Parse(emailSettings["MailPort"])))
        //    {
        //        smtpClient.UseDefaultCredentials = false;
        //        smtpClient.Credentials = new NetworkCredential(emailSettings["Username"], emailSettings["Password"]);
        //        smtpClient.EnableSsl = false; // Set to false if your SMTP doesn't require SSL

        //        var mailMessage = new MailMessage
        //        {
        //            From = new MailAddress(emailSettings["SenderEmail"], emailSettings["SenderName"]),
        //            Subject = subject,
        //            Body = htmlMessage,
        //            IsBodyHtml = true,
        //        };

        //        mailMessage.To.Add(email);

        //        try
        //        {
        //            await smtpClient.SendMailAsync(mailMessage);
        //            Console.WriteLine("Email sent successfully!");
        //        }
        //        catch (SmtpException ex)
        //        {
        //            Console.WriteLine($"SMTP Error: {ex.StatusCode} - {ex.Message}");
        //            // Additional logging or rethrow exception as needed
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("General Error sending email: " + ex.Message);
        //            // Additional logging or rethrow exception as needed
        //        }
        //    }
        //}

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
