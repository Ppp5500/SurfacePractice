using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Material MT;
    private int i;

    public void Change()
    {
        i = Random.Range(0, 2);

        if (!TryGetComponent<Material>(out var MT))
        {
            Debug.Log("MT is NULL!");
        }
        else
        {
            switch (i)
            {
                case 0: MT.color = Color.red; break;
                case 1: MT.color = Color.green; break;
                case 2: MT.color = Color.blue; break;
            }
        }

    }
}
