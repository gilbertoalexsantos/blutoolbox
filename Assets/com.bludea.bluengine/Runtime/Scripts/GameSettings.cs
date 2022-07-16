using System.Collections.Generic;
using UnityEngine;

namespace BluEngine
{
    [CreateAssetMenu(menuName = "BluEngine/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public static readonly string ResourcesPath = "BluEngine/GameSettings";

        public string BuildInfoResourcesPath = "BluEngine/buildInfo";
        public List<string> CustomInstallersResourcesFolders;
        public List<string> LoadingStepsResourcesFolders;
    }
}