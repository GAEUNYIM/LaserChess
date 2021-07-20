using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputReciever : MonoBehaviour
{
    protected UIInputHandler[] inputHandlers;
    public abstract void OnInputReceived(int text);

    private void Awake()
    {
        inputHandlers = GetComponents<UIInputHandler>();
    }
}
