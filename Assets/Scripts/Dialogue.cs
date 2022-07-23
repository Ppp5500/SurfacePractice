using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 대화 정보를 저장하는 스크립트
[System.Serializable]
public class Dialogue
{
    [Tooltip("말하는 사람")]
    public string name;

    [Tooltip("대사 내용")]
    public string[] contexts;
}

[System.Serializable]
public class DialogueEvent
{
    // 어디에서 쓰이는 대화인가
    public string name;

    public Vector2 line;
    public Dialogue[] dialogues;
}
