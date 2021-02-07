using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject] IData data;

    public void Controll ()
    {
        data.GetData(data);
    }
}
