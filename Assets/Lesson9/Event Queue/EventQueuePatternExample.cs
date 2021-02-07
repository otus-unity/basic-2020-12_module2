using UnityEngine;
using System.Collections.Generic;


public class EventQueuePatternExample : MonoBehaviour
{
    private Dictionary<MessagePriority, string> priorityLookup = new Dictionary<MessagePriority, string>();

    private void Start()
    {
        priorityLookup.Add(MessagePriority.Low, "Low");
        priorityLookup.Add(MessagePriority.Medium, "Medium");
        priorityLookup.Add(MessagePriority.High, "High");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MessagePriority priority = MessagePriority.High;
            float random = UnityEngine.Random.value;
            if (random < 0.33f)
            {
                priority = MessagePriority.Low;
            }
            else if (random < 0.7f)
            {
                priority = MessagePriority.Medium;
            }

            float showTime = UnityEngine.Random.Range(1f, 5f);

            //Add an event to the queue
            EventQueueManager.Instance.AddEventToQueue(new MessageEvent(priorityLookup[priority] + " priority message shown at "
                + System.DateTime.Now + " for " + showTime + " seconds", Time.time + showTime, priority));
        }
    }
}