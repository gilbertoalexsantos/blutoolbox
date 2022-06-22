using UnityEngine;
using Zenject;

namespace BluEngine
{
    public abstract class AppStartCustomSceneInstaller : MonoBehaviour
    {
        public abstract void InstallBindings(DiContainer container);
    }
}