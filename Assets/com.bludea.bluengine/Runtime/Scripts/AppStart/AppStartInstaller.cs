using System;
using System.Collections.Generic;
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
            Container.Bind<IDeviceIdProvider>().To<UnityDeviceIdProvider>().AsSingle();
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
            Container.Bind<AssetManager>().AsSingle();

            Container.Bind<IMonoCallbacks>().FromInstance(_monoCallbacks);
            Container.Bind<ScreenSceneRoot>().FromInstance(screenSceneRoot);

            BindDatasources();
            BindSceneAutoBinders();
        }

        private void BindDatasources()
        {
            Container.Bind<IAsyncDatasource<IEnumerable<LoadingStep>>>().To<LoadingStepsDatasource>().AsSingle();
            Container.Bind<ISyncDatasource<GameSettings>>().To<GameSettingsDatasource>().AsSingle();
        }

        private void BindSceneAutoBinders()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            GameObject[] roots = activeScene.GetRootGameObjects();
            foreach (GameObject root in roots)
            {
                foreach (var obj in root.GetComponentsInChildren<ISceneAutoBinder>(true))
                {
                    Type type = obj.GetType();
                    Container.Bind(type).FromInstance(obj);
                }
            }
        }
    }
}