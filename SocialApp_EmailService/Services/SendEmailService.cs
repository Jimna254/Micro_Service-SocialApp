using MailKit.Net.Smtp;
using MimeKit;

using SocialApp_EmailService.Models;


namespace SocialApp_EmailService.Services
{
    public class SendEmailService
    {
        private readonly string email;
        private readonly string password;

        public SendEmailService(IConfiguration _configuration)
        {
            email = _configuration.GetSection("EmailService:Email").Get<string>();
            password = _configuration.GetSection("EmailService:Password").Get<string>();

        }

        public async Task SendEmail(UserMessage res, string message)
        {
            MimeMessage message1 = new MimeMessage();
            message1.From.Add(new MailboxAddress("My SocialApp ", email));

            // Set the recipient's email address
            message1.To.Add(new MailboxAddress(res.Name, res.Email));

            message1.Subject = "Welcome to my Social App";

            var body = new TextPart("html")
            {
                Text = message.ToString()
            };
            message1.Body = body;

            var client = new SmtpClient();

            client.Connect("smtp.gmail.com", 587, false);

            client.Authenticate(email, password);

            await client.SendAsync(message1);

            await client.DisconnectAsync(true);
        }

    }
}
