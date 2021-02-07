using System.Collections.Generic;


public class EventQueueManager
{
    public delegate void EventDelegateX<T>(T myEvent) where T : GameEvent;
    private delegate void EventDelegate(GameEvent gameEvent);


    private Dictionary<System.Type, EventDelegate> DelegatesMap = new Dictionary<System.Type, EventDelegate>();
    private Dictionary<System.Delegate, EventDelegate> DelegateLookupMap = new Dictionary<System.Delegate, EventDelegate>();
    private static EventQueueManager _instance;
    public static EventQueueManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventQueueManager();
            }
            return _instance;
        }
    }

    public void AddListener<T>(EventDelegateX<T> delegateX) where T : GameEvent
    {

        EventDelegate internalDelegate = (gameEvent) => { delegateX((T)gameEvent); };
                
        if (DelegateLookupMap.ContainsKey(delegateX) && DelegateLookupMap[delegateX] == internalDelegate)
        {
            return;
        }

        DelegateLookupMap[delegateX] = internalDelegate;

        if (DelegatesMap.TryGetValue(typeof(T), out EventDelegate tempDel))
        {
            DelegatesMap[typeof(T)] = tempDel += internalDelegate;
        }
        else
        {
            DelegatesMap[typeof(T)] = internalDelegate;
        }
    }

    public void RemoveListener<T>(EventDelegateX<T> delegateX) where T : GameEvent
    {
        if (DelegateLookupMap.TryGetValue(delegateX, out EventDelegate internalDelegate))
        {
            if (DelegatesMap.TryGetValue(typeof(T), out EventDelegate tempDel))
            {
                tempDel -= internalDelegate;
                if (tempDel == null)
                {
                    DelegatesMap.Remove(typeof(T));
                }
                else
                {
                    DelegatesMap[typeof(T)] = tempDel;
                }
            }

            DelegateLookupMap.Remove(delegateX);
        }
    }

    public void AddEventToQueue(GameEvent gameEvent)
    {
        if (DelegatesMap.TryGetValue(gameEvent.GetType(), out EventDelegate del))
        {
            del.Invoke(gameEvent);
        }
    }

}
