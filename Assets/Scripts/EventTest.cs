using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour
{
    public UnityEvent myEvent;

    void Start()
    {
        EmitEvent();
    }

    private void EmitEvent()
    {
        myEvent.Invoke();
    }
}
