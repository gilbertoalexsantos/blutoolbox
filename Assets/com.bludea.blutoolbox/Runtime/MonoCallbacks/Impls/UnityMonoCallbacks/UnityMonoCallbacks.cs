using System;
using UnityEngine;

namespace BluToolbox
{
    public class UnityMonoCallbacks : MonoBehaviour, IMonoCallbacks, IHardReload, IDisposable
    {
        private IHardReloadManager _hardReloadManager;

        private Action onUpdate;
        private Action onLateUpdate;
        private Action onFixedUpdate;

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

        private void FixedUpdate()
        {
            onFixedUpdate?.Invoke();
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

        public void AddOnFixedUpdate(Action action)
        {
            onFixedUpdate += action;
        }

        public void RemoveOnFixedUpdate(Action action)
        {
            onFixedUpdate -= action;
        }

        public void OnHardReload()
        {
            Dispose();
        }

        public void Dispose()
        {
            onUpdate = null;
            onLateUpdate = null;
            onFixedUpdate = null;
        }
    }
}