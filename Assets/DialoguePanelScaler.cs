using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePanelScaler : MonoBehaviour
{
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = GetComponentInParent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.sizeDelta = new Vector2(0, parentRectTransform.sizeDelta.y/3);
    }
}
