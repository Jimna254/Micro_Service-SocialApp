using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp_MessageBus
{
    public class MessageBus : IMessageBus
    {
        // Connection string to the service bus in Azure
        public string connection_string = "Endpoint=sb://socialapp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=pS/1OjG7QicFm6kXujGsvH3e+aF2kQAoU+ASbGq3gF4=";
        
        public async Task PublishMessage(object message, string queue_topicname)
        {
            var serviceBus = new ServiceBusClient(this.connection_string);

            var sender = serviceBus.CreateSender(queue_topicname);

            var jsonMessage = JsonConvert.SerializeObject(message);
            var theMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {

                //Give a unique iDentifier
                CorrelationId = Guid.NewGuid().ToString(),
            };

            // send the Message

            await sender.SendMessageAsync(theMessage);
            //clean up Resource

            await serviceBus.DisposeAsync();


        }
    }
}
