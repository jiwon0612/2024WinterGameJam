
using System;
using UnityEditor;
using UnityEngine;

public class HideIfEnumAttribute : PropertyAttribute
{
    public string EnumFieldName;   // Enum 필드 이름
    public int[] EnumValues;       // 숨길 조건에 해당하는 Enum 값들

    public HideIfEnumAttribute(string enumFieldName, params int[] enumValues)
    {
        EnumFieldName = enumFieldName;
        EnumValues = enumValues;
    }
}

[CustomPropertyDrawer(typeof(HideIfEnumAttribute))]
public class HideIfEnumDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        HideIfEnumAttribute hideIfEnum = (HideIfEnumAttribute)attribute;
        SerializedProperty enumProperty = property.serializedObject.FindProperty(hideIfEnum.EnumFieldName);

        if (enumProperty != null)
        {
            // Enum 값이 숨김 조건 중 하나에 해당하는지 확인
            bool shouldHide = false;
            foreach (int value in hideIfEnum.EnumValues)
            {
                if (enumProperty.enumValueIndex == value)
                {
                    shouldHide = true;
                    break;
                }
            }

            // 숨김 조건에 해당하지 않을 때만 필드 표시
            if (!shouldHide)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        HideIfEnumAttribute hideIfEnum = (HideIfEnumAttribute)attribute;
        SerializedProperty enumProperty = property.serializedObject.FindProperty(hideIfEnum.EnumFieldName);

        if (enumProperty != null)
        {
            foreach (int value in hideIfEnum.EnumValues)
            {
                if (enumProperty.enumValueIndex == value)
                {
                    return 0; // 숨길 때 높이를 0으로 설정
                }
            }
        }

        return EditorGUI.GetPropertyHeight(property, label); // 기본 높이 반환
    }
}
