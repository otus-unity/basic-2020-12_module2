using System;


public class MessageEvent : GameEvent, IMessageEvent
{
    public DateTime TimeRaised { private set; get; }
    public MessagePriority Priority { private set; get; }
    public float DisplayTime { private set; get; }
    public object Message { private set; get; }

    public MessageEvent(object message, float displayTime, MessagePriority priority)
    {
        Message = message;
        DisplayTime = displayTime;
        Priority = priority;
        TimeRaised = DateTime.Now;
    }
}
