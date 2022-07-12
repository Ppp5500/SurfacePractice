using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Slime,
    Ent,
    Goblin
}

public class Enemy : MonoBehaviour
{
    public MonsterType monsterType;
    public int hp;
    public float damage;
    public string mytag;
    public bool canRun;
}
