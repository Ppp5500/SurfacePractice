using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectOnEnable : MonoBehaviour
{
	public EventSystem eventSystem;


	private void OnEnable()
    {
		eventSystem.SetSelectedGameObject(this.gameObject);
    }
}
