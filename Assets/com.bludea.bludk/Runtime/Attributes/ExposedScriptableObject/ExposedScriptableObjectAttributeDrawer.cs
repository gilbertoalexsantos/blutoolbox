#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Bludk
{
    [CustomPropertyDrawer(typeof(ExposedScriptableObjectAttribute), true)]
    public class ExposedScriptableObjectAttributeDrawer : PropertyDrawer
    {
        private Editor _editor;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);

            if (property.objectReferenceValue != null)
            {
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
            }

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            {
                if (_editor == null)
                {
                    Editor.CreateCachedEditor(property.objectReferenceValue, null, ref _editor);
                }

                EditorGUI.BeginChangeCheck();
                {
                    if (_editor != null)
                    {
                        _editor.OnInspectorGUI();
                    }
                }
                if (EditorGUI.EndChangeCheck())
                {
                    _editor.serializedObject.ApplyModifiedProperties();
                }
            }
            EditorGUI.indentLevel--;
        }
    }

    // Required for the fetching of a default editor on MonoBehaviour objects.
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class MonoBehaviourEditor : Editor { }

    // Required for the fetching of a default editor on ScriptableObject objects.
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableObject), true)]
    public class ScriptableObjectEditor : Editor { }
}
#endif