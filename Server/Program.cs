using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

class Program
{
    static async Task Main()
    {
        Console.Title = "Samples.FullDuplex.Server";
        LogManager.Use<DefaultFactory>()
            .Level(LogLevel.Info);

        var endpointConfiguration = new EndpointConfiguration("Samples.FullDuplex.Server");
        endpointConfiguration.UsePersistence<LearningPersistence>();

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        transport.ConnectionString("Endpoint=sb://authoring-local-servicebus-john.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=g1QjzVL+aYL7QuDNNDsF/eZIXmdAfnWYRIhyXM2vsF0=");

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}