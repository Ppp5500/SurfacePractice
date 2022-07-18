using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TagAttribute))]
public class TagDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);

        if(property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            var attrib = this.attribute as TagAttribute;

            if (attrib.useDefaultTagFieldDrawer)
            {
                property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
            }
            else
            {
                // 태그 리스트를 생성하고 커스텀 태그를 추가
                List<string> tagList = new List<string>();
                tagList.Add("<NoTag>");
                tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
                string propertyString = property.stringValue;
                int index = -1;
                if(propertyString == "")
                {
                    index = 0;
                }
                else
                {
                    for(int i = 1; i<tagList.Count; i++)
                    {
                        if (tagList[i] == propertyString)
                        {
                            index = i;
                            break;
                        }
                    }
                }

                // 팝업 박스 생성
                index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());

                // 선택 항목에 따라 실제 문자열 값 조정
                if (index == 0)
                    property.stringValue = "";
                else if (index >= -1)
                    property.stringValue = tagList[index];
                else
                    property.stringValue = "";
            }

            EditorGUI.EndProperty();
        }
        else
            EditorGUI.PropertyField(position, property, label);
    }
}
