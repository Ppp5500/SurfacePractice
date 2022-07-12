using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    public Enemy selectedEnemy;

    // Editor에서 OnEnable은 실제 에디터에서 오브젝트를 눌렀을 때 활성화 됨
    private void OnEnable()
    {
        // target은 Editor에 있는 변수로 선택한 오브젝트를 받아옴.
        if (AssetDatabase.Contains(target))
        {
            selectedEnemy = null;
        }
        else
        {
            // target은 Object형이므로 Enemy 로 형변환
            selectedEnemy = target as Enemy;
        }
    }

     // 유니티가 인스펙터를 GUI로 그려주는 함수
    public override void OnInspectorGUI()
    {
        if (selectedEnemy == null)
            return;

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("****** 몬스터 정보 입력툴 ******");
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        Color tempColor = Color.white;
        switch (selectedEnemy.monsterType)
        {
            case MonsterType.Slime:
                tempColor = Color.yellow;
                break;
            case MonsterType.Ent:
                tempColor = Color.cyan;
                break;
            case MonsterType.Goblin:
                tempColor = Color.green;
                break;
            default:
                break;
        }

        GUI.color = tempColor;
        selectedEnemy.monsterType = (MonsterType)EditorGUILayout.EnumPopup("몬스터 종류", selectedEnemy.monsterType);

        GUI.color = Color.white;
        selectedEnemy.hp = EditorGUILayout.IntField("몬스터 체력", selectedEnemy.hp);
        if (selectedEnemy.hp < 0)
            selectedEnemy.hp = 0;

        selectedEnemy.damage = EditorGUILayout.FloatField("몬스터 공격력", selectedEnemy.damage);
        selectedEnemy.mytag = EditorGUILayout.TextField("설명", selectedEnemy.mytag);

        // Release 세팅하고 버튼누르면 모든변수가 다바뀌게. Test 세팅하면 그렇게 바뀌게 그런식으로 사용할 수 있음.
        if (GUILayout.Button("Resize"))
        {
            selectedEnemy.transform.localScale = Vector3.one * Random.Range(0.5f, 1f);
        }

        GUILayout.Button("DoNotthing");

        if(GUILayout.Button("Make Slime"))
        {
            selectedEnemy.monsterType = MonsterType.Slime;
        }
    }
}
