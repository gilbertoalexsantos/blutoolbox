using UnityEngine;
using Zenject;

namespace BluEngine
{
    public abstract class AppStartCustomScriptableInstaller : ScriptableObject
    {
        public abstract void InstallBindings(DiContainer container);
    }
}