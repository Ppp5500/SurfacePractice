using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastManager : MonoBehaviour
{
    public Camera m_camera;
    
    void Update()
    {
        RaycastHit hit;
        Ray ray = m_camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            // Do something with the object that was hit by the raycast.
            //Debug.Log("hit!");
            //Debug.Log(objectHit);
        }
    }
}
