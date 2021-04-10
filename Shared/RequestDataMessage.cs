using System;
using NServiceBus;

public class RequestDataMessage : IMessage
{
    public Guid DataId { get; set; }
    public string Sender { get; set; }
}
