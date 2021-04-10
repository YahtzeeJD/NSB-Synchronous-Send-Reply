using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using System;

public class RequestDataMessageHandler : IHandleMessages<RequestDataMessage>
{
    static ILog log = LogManager.GetLogger<RequestDataMessageHandler>();

    public async Task Handle(RequestDataMessage message, IMessageHandlerContext context)
    {
        log.Info($"Received request {message.DataId} from {message.Sender}.");

        string[] teamMembers = { "John", "Rishabh", "Paul", "Neel", "Rupesh", "Stacy", "Helen", "Viswa", "Prem", "Venkat", "Durga" };
        Random rnd = new Random();
        var index = rnd.Next(11);

        var response = new DataResponseMessage
        {
            DataId = message.DataId,
            Receiver = $"Hello {message.Sender}. I am {teamMembers[index]}."
        };

        await context.Reply(response).ConfigureAwait(false);
    }

}