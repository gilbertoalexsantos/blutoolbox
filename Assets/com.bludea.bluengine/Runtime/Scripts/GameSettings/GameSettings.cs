using UnityEngine;

namespace BluEngine
{
    [CreateAssetMenu(menuName = "BluEngine/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public static readonly string ResourcesPath = "BluEngine/GameSettings";

        [Header("Where your buildInfo is stored in Resources")]
        public string BuildInfoResourcesPath = "BluEngine/buildInfo";
    }
}