using System.Collections;
using System.Collections.Generic;
using Bludk;
using UnityEngine;
using Zenject;

namespace BluEngine
{
    public class AppStart : MonoBehaviour
    {
        [Inject]
        private LoadingStepsManager _loadingStepsManager;

        [Inject]
        private IAsyncDatasource<IEnumerable<LoadingStep>> _loadingStepsDatasource;

        private IEnumerator Start()
        {
            return _loadingStepsDatasource.LoadAsync()
                .Then((IEnumerable<LoadingStep> steps) =>
                {
                    _loadingStepsManager.Init(steps);
                    return _loadingStepsManager.Execute();
                });
        }
    }
}