using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInputHandler : MonoBehaviour, UIInputHandler
{
    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action callback)
    {
        callback?.Invoke();
    }

    public void ProcessInput(int tag)
    {
        Debug.Log("inttag");
    }
}
