using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Range2Attribute))]

public class Range2Drawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Range2Attribute range2 = (Range2Attribute)attribute;

        // 속성 값이 인트형이면 range2의 최소/최대값을 적용
        if(property.propertyType == SerializedPropertyType.Integer)
            EditorGUI.IntSlider(position, property, range2.min, range2.max, label);
        else
            EditorGUI.PropertyField(position, property, label);
    }
}
