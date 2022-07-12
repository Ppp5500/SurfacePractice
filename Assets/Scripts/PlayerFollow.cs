using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void Update()
    {
        gameObject.transform.position = player.transform.position;
    }
}
