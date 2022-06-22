using System;
using UnityEngine;
using Zenject;

namespace Bludk
{
    public class MonoCallbacks : MonoBehaviour, IMonoCallbacks, IHardReload, IDisposable
    {
        private IHardReloadManager _hardReloadManager;

        private Action onUpdate;
        private Action onLateUpdate;

        [Inject]
        public void Constructor(IHardReloadManager hardReloadManager)
        {
            _hardReloadManager = hardReloadManager;
        }

        private void Start()
        {
            _hardReloadManager.AddOnHardReload(this);
        }

        private void Update()
        {
            onUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            onLateUpdate?.Invoke();
        }

        public void AddOnUpdate(Action action)
        {
            onUpdate += action;
        }

        public void RemoveOnUpdate(Action action)
        {
            onUpdate -= action;
        }

        public void AddOnLateUpdate(Action action)
        {
            onLateUpdate += action;
        }

        public void RemoveOnLateUpdate(Action action)
        {
            onLateUpdate -= action;
        }

        public void OnHardReload()
        {
            Dispose();
        }

        public void Dispose()
        {
            onUpdate = null;
            onLateUpdate = null;
        }
    }
}