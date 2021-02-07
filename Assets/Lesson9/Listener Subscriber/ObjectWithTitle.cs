using UnityEngine;


[RequireComponent(typeof(BoxCollider))] 
public class ObjectWithTitle : MonoBehaviour
{
    
    public string Title = "";
    public Texture2D Icon = null;

    private protected bool _mouseCursorOnAnObject = false;

    public void OnMouseEnter() 
    {
        EventBus.Bus.CallToChangeNameOfObject(this); 
        _mouseCursorOnAnObject = true;
    }

    public void OnMouseExit()
    {
        _mouseCursorOnAnObject = false;
    }
}
