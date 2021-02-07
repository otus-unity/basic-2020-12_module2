using UnityEngine;
using System.Collections.Generic;


public class EventQueue : MonoBehaviour
{
    private static readonly GUIStyle LOW_PRIORTY = new GUIStyle();
    private static readonly GUIStyle NORMAL_PRIORITY = new GUIStyle();
    private static readonly GUIStyle HIGH_PRIORITY = new GUIStyle();

    [Header("Setting options")]
    public bool logToConsole = true;
    public bool prependDateTime = false;

    [Header("Message Colours")]
    public Color highPriorityColour = Color.red;
    public Color normalPriorityColour = Color.black;
    public Color lowPriorityColour = Color.white;

    [Header("Message Font Style")]
    public FontStyle highPriorityStyle = FontStyle.Bold;
    public FontStyle normalPriorityStyle = FontStyle.Normal;
    public FontStyle lowPriorityStyle = FontStyle.Normal;

    [Header("Message Location")]
    public Vector2 queueLocation = new Vector2(25, 25);
    public Vector2 messageSize = new Vector2(200, 15);


    #region event queue

    private List<IMessageEvent> pendingEventQueueList = new List<IMessageEvent>();
       
    private void OnAddEventToQueue(IMessageEvent e)
    {
        pendingEventQueueList.Add(e);

        if (logToConsole)
        {
            Debug.Log("Message Recieved [" + System.DateTime.Now + "]: " + e.Message.ToString());
        }
    }

    private void Update()
    {
        for (int i = pendingEventQueueList.Count - 1; i >= 0; i--)
        {
            if (Time.time > pendingEventQueueList[i].DisplayTime)
                pendingEventQueueList.RemoveAt(i);
        }
    }

    private void Start()
    {
        if (pendingEventQueueList.Count > 0)
        {
            pendingEventQueueList.Clear();
        }

        EventQueueManager.Instance.AddListener<MessageEvent>(OnAddEventToQueue);
    }

    private void OnDisable()
    {
        EventQueueManager.Instance.RemoveListener<MessageEvent>(OnAddEventToQueue);
    }

    #endregion


    private void OnEnable()
    {
        SetUIStyle();
    }


    private void SetUIStyle()
    {
        LOW_PRIORTY.normal.textColor = lowPriorityColour;
        LOW_PRIORTY.fontStyle = lowPriorityStyle;

        NORMAL_PRIORITY.normal.textColor = normalPriorityColour;
        NORMAL_PRIORITY.fontStyle = normalPriorityStyle;

        HIGH_PRIORITY.normal.textColor = highPriorityColour;
        HIGH_PRIORITY.fontStyle = highPriorityStyle;
    }
    
    private void OnGUI()
    {
        float yPosition = queueLocation.y;

        foreach (var m in pendingEventQueueList)
        {

            GUIStyle style = GetMessageStyle(m);

            string message = (prependDateTime) ? "[" + m.TimeRaised + "]: " + m.Message.ToString() : m.Message.ToString();

            GUI.Label(new Rect(queueLocation.x, yPosition, messageSize.x, messageSize.y), message, style);

            yPosition += messageSize.y;
        }
    }

    private GUIStyle GetMessageStyle(IMessageEvent e)
    {
        switch (e.Priority)
        {
            case MessagePriority.Low:
                return LOW_PRIORTY;
            case MessagePriority.Medium:
                return NORMAL_PRIORITY;
            default:
                return HIGH_PRIORITY;
        }

    }
       
}
