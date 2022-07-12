using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SphereCollider))]
public class DoorMain : MonoBehaviour
{
    [SerializeField, Scene] private string newScene; 

    private void OnAwake()
    {
        TryGetComponent<SphereCollider>(out SphereCollider sphereCollider);
        sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
            SceneManager.LoadSceneAsync(newScene);
    }
}
