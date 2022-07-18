using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[DisallowMultipleComponent]
[RequireComponent(typeof(SphereCollider))]
public class DetectWithTag : MonoBehaviour
{
    private SphereCollider SC;

    [Tooltip("이 태그를 가진 오브젝트와 상호작용이 가능해집니다.")]
    [SerializeField, Tag] private string TagToDetect;

    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerStayEvent;
    public UnityEvent OnTriggerExitEvent;

    private void Awake()
    {
        SC = GetComponent<SphereCollider>();
        SC.isTrigger = true;
    }

    void OnTriggerEnter(Collider obj)
    {
        if(obj.CompareTag(TagToDetect))
            OnTriggerEnterEvent.Invoke();
    }

    void OnTriggerStay(Collider obj)
    {
        if (obj.CompareTag(TagToDetect))
            OnTriggerStayEvent.Invoke();
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.CompareTag(TagToDetect))
            OnTriggerExitEvent.Invoke();
    }
}
