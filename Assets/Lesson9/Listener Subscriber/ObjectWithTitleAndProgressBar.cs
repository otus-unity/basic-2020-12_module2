using UnityEngine;


public class ObjectWithTitleAndProgressBar : ObjectWithTitle
{
    public float Progress = 0f;
    private bool _fillingRate = true;

    public void Update()
    {
        if (!_fillingRate) return; 

        Progress += (0.3f * Time.deltaTime); 
        Progress %= 1;
        
        if (_mouseCursorOnAnObject)
        {
            EventBus.Bus.CallToChangeNameOfObject(this);
        }
    }

    public void ChangeFillRate()
    {
        _fillingRate = !_fillingRate;

    }
}
