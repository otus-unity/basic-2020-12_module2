using System;


public interface IMessageEvent
{
    DateTime TimeRaised { get; }
    float DisplayTime { get; }
    MessagePriority Priority { get; }
    object Message { get; }
}
