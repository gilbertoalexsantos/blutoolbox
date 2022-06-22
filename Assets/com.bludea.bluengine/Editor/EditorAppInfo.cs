using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "BluEngine/EditorAppInfo", fileName = "EditorAppInfo")]
public class EditorAppInfo : ScriptableObject
{
    public SceneAsset StartScene;
}