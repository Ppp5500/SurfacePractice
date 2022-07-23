using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CamTarget이 플레이어의 Rotation에 독립적으로
// 위치만 같게 하기위한 스크립트
public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void Update()
    {
        gameObject.transform.position = player.transform.position;
    }
}
