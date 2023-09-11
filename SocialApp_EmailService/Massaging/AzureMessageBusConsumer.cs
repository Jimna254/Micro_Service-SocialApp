using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SocialApp_EmailService.Massaging;
using SocialApp_EmailService.Models;
using SocialApp_EmailService.Services;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp_EmailService.Messaging
{
    public class AzureMessageBusConsumer : IAzureMessageBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        private readonly string QueueName;
        private readonly ServiceBusProcessor _serviceProcessor;
        private readonly SendEmailService _sendMailService;

        public AzureMessageBusConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            //ConnectionString = "Endpoint=sb://socialapp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=pS/1OjG7QicFm6kXujGsvH3e+aF2kQAoU+ASbGq3gF4=";
            //QueueName = "registeruser";
            ConnectionString = _configuration.GetSection("ServiceBus:ConnectionString").Get<string>();
            QueueName = _configuration.GetSection("QueuesandTopic:RegisterUser").Get<string>();
            _serviceProcessor = new ServiceBusClient(ConnectionString).CreateProcessor(QueueName, new ServiceBusProcessorOptions());
            _sendMailService = new SendEmailService(_configuration);
        }

        public async Task Start()
        {
            _serviceProcessor.ProcessMessageAsync += OnRegistration;
            _serviceProcessor.ProcessErrorAsync += ErrorHandler;
            await _serviceProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await _serviceProcessor.StopProcessingAsync();
            await _serviceProcessor.DisposeAsync();
        }

        private async Task ErrorHandler(ProcessErrorEventArgs args)
        {
            // Handle errors gracefully, log them, and take appropriate actions.
            Console.WriteLine($"Error occurred: {args.Exception}");
        }

        private async Task OnRegistration(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            var userMessage = JsonConvert.DeserializeObject<UserMessage>(body);

            // Send email
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"<p>Hi {userMessage.Name},</p>");
                sb.Append($"<p>Thank you for registering with us.</p>");
                sb.AppendLine("<br/>");

                // new line
                sb.Append($"<p>Regards,</p>");
                sb.Append($"<p>My SocialApp init</p>");
                await _sendMailService.SendEmail(userMessage, sb.ToString());
                await args.CompleteMessageAsync(message);
            }
            catch (Exception ex)
            {
                // Handle email sending errors
                Console.WriteLine($"Error sending email: {ex}");
            }
        }

    }
}

