using UnityEditor;
using UnityEngine;

namespace BluEngine.Editor
{
    [CreateAssetMenu(menuName = "BluEngine/EditorSettings", fileName = "EditorSettings")]
    public class EditorSettings : ScriptableObject
    {
        public static readonly string ResourcesPath = "BluEngine/EditorSettings";

        public SceneAsset StartScene;
    }
}