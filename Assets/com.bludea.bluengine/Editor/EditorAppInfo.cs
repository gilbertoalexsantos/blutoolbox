using UnityEditor;
using UnityEngine;

namespace BluEngine.Editor
{
    [CreateAssetMenu(menuName = "BluEngine/EditorAppInfo", fileName = "EditorAppInfo")]
    public class EditorAppInfo : ScriptableObject
    {
        public SceneAsset StartScene;
    }
}