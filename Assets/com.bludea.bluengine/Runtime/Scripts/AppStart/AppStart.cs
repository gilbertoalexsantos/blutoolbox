using System.Collections;
using UnityEngine;
using Zenject;

namespace BluEngine
{
    public class AppStart : MonoBehaviour
    {
        [Inject]
        private LoadingStepsManager _loadingStepsManager;

        private IEnumerator Start()
        {
            return _loadingStepsManager.Execute();
        }
    }
}