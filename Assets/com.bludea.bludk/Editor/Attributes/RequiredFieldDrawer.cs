using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bludk.Editor
{
  [CustomPropertyDrawer(typeof(RequiredFieldAttribute))]
  public class RequiredFieldDrawer : PropertyDrawer
  {
    private float LabelHeight => 25f;

    private bool HasSerializedValue(SerializedProperty property)
    {
      if (property.propertyType == SerializedPropertyType.ObjectReference)
      {
        return property.objectReferenceInstanceIDValue != 0;
      }

      return true;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      if (Event.current.type == EventType.Layout)
      {
        return;
      }

      if (HasSerializedValue(property))
      {
        EditorGUI.PropertyField(position, property, label);
        return;
      }

      Rect labelRect = position.Shrink(bottom: - (position.height - LabelHeight));
      Rect propertyRect = position.Shrink(top: LabelHeight);

      GUI.Box(position.Shrink(left: -2f, right: +2f), "");
      EditorGUI.HelpBox(labelRect, "You need to assign this element", MessageType.Error);
      EditorGUI.PropertyField(propertyRect, property, label);
    }

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
      return new Label("123");
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      float baseHeight = base.GetPropertyHeight(property, label);
      return baseHeight + (HasSerializedValue(property) ? 0f : LabelHeight);
    }
  }
}