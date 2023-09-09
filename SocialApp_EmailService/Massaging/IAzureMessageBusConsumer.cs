namespace SocialApp_EmailService.Massaging
{
    public interface IAzureMessageBusConsumer
    {
       public Task Start();


       public Task Stop();
    }
}
