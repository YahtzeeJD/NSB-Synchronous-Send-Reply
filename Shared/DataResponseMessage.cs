using NServiceBus;
using System;

public class DataResponseMessage : IMessage
{
    public Guid DataId { get; set; }
    public string Receiver { get; set; }
}