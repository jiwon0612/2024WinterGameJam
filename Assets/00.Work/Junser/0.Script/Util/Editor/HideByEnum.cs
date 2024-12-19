using UnityEditor;
using UnityEngine;

public class HideIfEnumAttribute : PropertyAttribute
{
    public string EnumFieldName;   // Enum �ʵ� �̸�
    public int[] EnumValues;       // ���� ���ǿ� �ش��ϴ� Enum ����

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
            // Enum ���� ���� ���� �� �ϳ��� �ش��ϴ��� Ȯ��
            bool shouldHide = false;
            foreach (int value in hideIfEnum.EnumValues)
            {
                if (enumProperty.enumValueIndex == value)
                {
                    shouldHide = true;
                    break;
                }
            }

            // ���� ���ǿ� �ش����� ���� ���� �ʵ� ǥ��
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
                    return 0; // ���� �� ���̸� 0���� ����
                }
            }
        }

        return EditorGUI.GetPropertyHeight(property, label); // �⺻ ���� ��ȯ
    }
}
