using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class WindowExample : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    bool myBool2 = false;

    // 상단 메뉴바에 여는 항목 추가
    [MenuItem("Window/WindowExample")]

    public static void Showwindow()
    {
        GetWindow<WindowExample>();
    }

    void OnGUI()
    {
        // 윈도우 창에서 보일 코드
        GUILayout.Label("Test String", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
            myBool = EditorGUILayout.Toggle("Toggle", myBool);
            myFloat = EditorGUILayout.Slider("mySlider", myFloat, 0, 10);
            

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        myBool2 = EditorGUILayout.Toggle("Second Toggle", myBool2);


        EditorGUILayout.EndToggleGroup();
    }
}
