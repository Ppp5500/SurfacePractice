using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range2Test : MonoBehaviour
{
    [SerializeField, Range2(0, 10)] int hp;

    [SerializeField, Range2(0, 10)] string str;
}
