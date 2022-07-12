using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(SphereCollider))]
public class ButtonMain : MonoBehaviour
{
    private SphereCollider SC;

    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerStayEvent;

    private void Awake()
    {
        SC = GetComponent<SphereCollider>();
        SC.isTrigger = true;
    }

    void OnTriggerEnter(Collider obj)
    {
        OnTriggerEnterEvent.Invoke();
    } 

    void OnTriggerStay(Collider obj)
    {
        OnTriggerStayEvent.Invoke();
    }
}
