using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace Bludk
{
    public class ScreenManager : IHardReload
    {
        private readonly IScreenManagerInfo _info;
        private readonly IScreenResolver _screenPrefabResolver;
        private readonly DiContainer _container;

        private readonly Dictionary<Type, ScreenController> _controllers = new();
        private readonly Dictionary<Type, ScreenUI> _uis = new();
        private readonly HashSet<Type> _shownControllers = new();
        private int _lastSortingOrder = -1;

        public int IncSortingOrder()
        {
            return LastSortingOrder++;
        }

        private int LastSortingOrder
        {
            get
            {
                if (_lastSortingOrder == -1)
                {
                    _lastSortingOrder = _info.StartSortingOrder;
                }

                return _lastSortingOrder;
            }
            set => _lastSortingOrder = value;
        }

        public ScreenManager(
            IScreenManagerInfo info, 
            IScreenResolver screenPrefabResolver,
            DiContainer container,
            IHardReloadManager hardReloadManager
        )
        {
            _info = info;
            _screenPrefabResolver = screenPrefabResolver;
            _container = container;
            hardReloadManager.AddOnHardReload(this);
        }

        public void OnHardReload()
        {
            foreach (ScreenController controller in _controllers.Values)
            {
                controller.Dispose();
            }
            _controllers.Clear();
        }

        public IEnumerator CacheUI<TUI>() where TUI : ScreenUI
        {
            Type key = typeof(TUI);
            if (_uis.ContainsKey(key))
            {
                return TxongaHelper.Empty();
            }

            return _screenPrefabResolver.Load<TUI>()
                .Then(ui =>
                {
                    _uis[key] = ui;
                    ui.transform.SetParent(_info.RootCanvas.transform);
                });
        }

        public IEnumerator<TUI> LoadUI<TUI>() where TUI : ScreenUI
        {
            Type key = typeof(TUI);
            if (_uis.ContainsKey(key))
            {
                return ((TUI) _uis[key]).Yield();
            }

            return CacheUI<TUI>()
                .Then(LoadUI<TUI>);
        }

        public IEnumerator<TCONTROLLER> LoadController<TUI, TCONTROLLER>() 
            where TUI : ScreenUI
            where TCONTROLLER : ScreenController
        {
            Type controllerType = typeof(TCONTROLLER);
            if (_controllers.ContainsKey(controllerType))
            {
                return ((TCONTROLLER) _controllers[controllerType]).Yield();
            }

            TCONTROLLER controller = _container.Instantiate<TCONTROLLER>();
            _controllers[controllerType] = controller;
            return LoadUI<TUI>()
                .Then(ui =>
                {
                    controller.SetUI(ui);
                    return controller.Yield(); 
                });
        }

        public IEnumerator UnloadController<TUI, TCONTROLLER>()
            where TUI : ScreenUI
            where TCONTROLLER : ScreenController
        {
            Type uiType = typeof(TUI);
            Type controllerType = typeof(TCONTROLLER);
            if (!_uis.ContainsKey(uiType) || !_controllers.ContainsKey(controllerType))
            {
                return TxongaHelper.Empty();
            }

            TUI ui = (TUI)_uis[uiType];
            TCONTROLLER controller = (TCONTROLLER)_controllers[controllerType];
            controller.Dispose();

            return _screenPrefabResolver.Unload(ui)
                .Then(() =>
                {
                    _uis.Remove(uiType);
                    _controllers.Remove(controllerType);
                    _shownControllers.Remove(controllerType);
                });
        }

        public void SetupAfterShow<TCONTROLLER>(TCONTROLLER controller) where TCONTROLLER : ScreenController
        {
            _shownControllers.Add(typeof(TCONTROLLER));
        }

        public void SetupAfterHide<TCONTROLLER>(TCONTROLLER controller) where TCONTROLLER : ScreenController
        {
            _shownControllers.Remove(typeof(TCONTROLLER));
        }

        public bool IsShown<TUI, TCONTROLLER>()
            where TUI : ScreenUI
            where TCONTROLLER : ScreenController
        {
            return _shownControllers.Contains(typeof(TCONTROLLER));
        }
    }
}