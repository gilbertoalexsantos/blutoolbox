using Bludk;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace BluEngine
{
    public class AppStartInstaller : MonoInstaller
    {
        [SerializeField]
        private MonoCallbacks _monoCallbacks;

        [SerializeField]
        private ScreenSceneRoot screenSceneRoot;

        public override void InstallBindings()
        {
            Container.Bind<IClock>().To<UnityClock>().AsSingle();
            Container.Bind<IHardReloadManager>().To<HardReloadManager>().AsSingle();
            Container.Bind<IScheduler>().To<Scheduler>().AsSingle();
            Container.Bind<IPrefs>().To<Prefs>().AsSingle();
            Container.Bind<ISerializer>().To<JsonNetSerializer>().AsSingle();
            Container.Bind<BuildInfoManager>().AsSingle();
            Container.Bind<LoadingStepsManager>().AsSingle();
            Container.Bind<ScreenManager>().AsSingle();
            Container.Bind<IScreenManagerInfo>().To<ScreenManagerInfo>().AsSingle();
            Container.Bind<IScreenResolver>().To<SceneScreenResolver>().AsSingle();

            Container.Bind<IMonoCallbacks>().FromInstance(_monoCallbacks);
            Container.Bind<ScreenSceneRoot>().FromInstance(screenSceneRoot);

            BindCustomInstallers();
        }

        private void BindCustomInstallers()
        {
            var scriptableInstallers = Resources.LoadAll<AppStartCustomScriptableInstaller>("Game/CustomInstallers");
            foreach (AppStartCustomScriptableInstaller installer in scriptableInstallers)
            {
                installer.InstallBindings(Container);
            }

            Scene activeScene = SceneManager.GetActiveScene();
            GameObject[] roots = activeScene.GetRootGameObjects();
            foreach (GameObject root in roots)
            {
                foreach (AppStartCustomSceneInstaller installer in root.GetComponentsInChildren<AppStartCustomSceneInstaller>(true))
                {
                    installer.InstallBindings(Container);
                }
            }
        }
    }
}