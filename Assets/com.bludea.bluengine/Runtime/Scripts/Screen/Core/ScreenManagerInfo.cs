using Bludk;
using UnityEngine;

namespace BluEngine
{
    public class ScreenManagerInfo : IScreenManagerInfo
    {
        private readonly ScreenSceneRoot _screenSceneRoot;

        public Canvas RootCanvas => _screenSceneRoot.GetComponent<Canvas>();
        public int StartSortingOrder => 42;

        public ScreenManagerInfo(ScreenSceneRoot screenSceneRoot)
        {
            _screenSceneRoot = screenSceneRoot;
        }
    }
}