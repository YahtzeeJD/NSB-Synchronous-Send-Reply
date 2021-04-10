using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

class Program
{
    static async Task Main()
    {
        Console.Title = "Samples.FullDuplex.Client";
        LogManager.Use<DefaultFactory>()
            .Level(LogLevel.Info);

        var endpointConfiguration = new EndpointConfiguration("Samples.FullDuplex.Client");
        endpointConfiguration.UsePersistence<LearningPersistence>();

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        transport.ConnectionString("Endpoint=sb://authoring-local-servicebus-john.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=g1QjzVL+aYL7QuDNNDsF/eZIXmdAfnWYRIhyXM2vsF0=");

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);

        Console.WriteLine("Press enter to send a message");
        Console.WriteLine("Press any other key to exit");

        #region ClientLoop

        while (true)
        {
            var key = Console.ReadKey();
            Console.WriteLine();

            if (key.Key != ConsoleKey.Enter)
                break;

            var guid = Guid.NewGuid();
            Console.WriteLine($"Requesting to get data by id: {guid:N}");

            string[] teamMembers = { "John", "Rishabh", "Paul", "Neel", "Rupesh", "Stacy", "Helen", "Viswa", "Prem", "Venkat", "Durga" };
            Random rnd = new Random();
            var index = rnd.Next(11);

            var message = new RequestDataMessage
            {
                DataId = guid,
                Sender = teamMembers[index]
            };
            await endpointInstance.Send("Samples.FullDuplex.Server", message).ConfigureAwait(false);
        }

        #endregion

        await endpointInstance.Stop().ConfigureAwait(false);
    }
}