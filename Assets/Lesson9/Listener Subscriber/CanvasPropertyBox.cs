using UnityEngine;
using UnityEngine.UI;


public class CanvasPropertyBox : MonoBehaviour
{
    
    public Text TextName;
    public RawImage Icon;
    public RawImage ProgressBar;
    public RawImage RawImageIndicator;
    public Button Button;

    private protected ObjectWithTitle _objectWithTitle = null;

    public void Awake()
    {
        DrawTextOnScreen(null);
        EventBus.Bus.ForObjectWithNameChange += DrawTextOnScreen;
        
    }

    public void DrawTextOnScreen(ObjectWithTitle objectWithTitle)
    {
        _objectWithTitle = objectWithTitle;
        if (objectWithTitle == null) 
        {
            TextName.text = "";
            Icon.enabled = false;
            RawImageIndicator.gameObject.SetActive(false);
            Button.gameObject.SetActive(false);
        }
        else 
        {
            TextName.text = objectWithTitle.Title;
            Icon.texture = objectWithTitle.Icon;
            Icon.enabled = (objectWithTitle.Icon != null);

            if (objectWithTitle.GetType() == typeof(ObjectWithTitleAndProgressBar))
            {
                Vector3 progressRectTransform = new Vector3(1.0f, 1.0f, 1.0f);
                progressRectTransform.x = ((ObjectWithTitleAndProgressBar)objectWithTitle).Progress;
                ProgressBar.GetComponent<RectTransform>().localScale = progressRectTransform;
                RawImageIndicator.gameObject.SetActive(true);
                Button.gameObject.SetActive(true);
            }
            else 
            {
                RawImageIndicator.gameObject.SetActive(false);
                Button.gameObject.SetActive(false);
            }
        }
    }

    public void OnEnable()
    {
        Button.onClick.AddListener(ActionButtonPressed);
    }
        

    protected void OnDestroy() 
    {
        EventBus.Bus.ForObjectWithNameChange -= DrawTextOnScreen;
        Button.onClick.RemoveListener(ActionButtonPressed);
    }

    protected void OnApplicationQuit() 
    {
        EventBus.Bus.ForObjectWithNameChange -= DrawTextOnScreen;
        Button.onClick.RemoveListener(ActionButtonPressed);
    }
    

    public void ActionButtonPressed()
    {
        Debug.Log("Button pressed");

        if ( _objectWithTitle!=null && _objectWithTitle.GetType() == typeof(ObjectWithTitleAndProgressBar))
        {
            ((ObjectWithTitleAndProgressBar)_objectWithTitle).ChangeFillRate();
        }
    }

}
