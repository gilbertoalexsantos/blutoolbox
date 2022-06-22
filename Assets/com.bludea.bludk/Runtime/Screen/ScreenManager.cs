using System;
using System.Collections;
using System.Collections.Generic;

namespace Bludk
{
    public class ScreenManager : IHardReload
    {
        private readonly IScreenManagerInfo _info;
        private readonly IScreenResolver _screenPrefabResolver;

        private Dictionary<Type, ScreenController> _controllers = new();
        private Dictionary<Type, ScreenUI> _uis = new();
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
            IHardReloadManager hardReloadManager
        )
        {
            _info = info;
            _screenPrefabResolver = screenPrefabResolver;
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

        public TCONTROLLER GetLoadedController<TUI, TCONTROLLER>()
            where TUI : ScreenUI
            where TCONTROLLER : ScreenController
        {
            if (!IsShown<TUI, TCONTROLLER>())
            {
                throw new Exception($"UI {typeof(TUI)} is not shown");
            }

            return (TCONTROLLER) _controllers[typeof(TCONTROLLER)];
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

        public void SetupAfterShow<TCONTROLLER>(TCONTROLLER controller) where TCONTROLLER : ScreenController
        {
            _controllers[typeof(TCONTROLLER)] = controller;
        }

        public void SetupAfterHide<TCONTROLLER>(TCONTROLLER controller) where TCONTROLLER : ScreenController
        {
            _controllers.Remove(typeof(TCONTROLLER));
        }

        public bool IsShown<TUI, TCONTROLLER>()
            where TUI : ScreenUI
            where TCONTROLLER : ScreenController
        {
            return _uis.ContainsKey(typeof(TUI)) && _controllers.ContainsKey(typeof(TCONTROLLER));
        }
    }
}