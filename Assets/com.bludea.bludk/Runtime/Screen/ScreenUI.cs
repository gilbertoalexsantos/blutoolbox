using UnityEngine;

namespace Bludk
{
    [RequireComponent(
        typeof(CanvasGroup),
        typeof(Canvas)
    )]
    public abstract class ScreenUI : MonoBehaviour
    {
        [SerializeField]
        private ScreenUIAnimation _screenUIAnimation;

        public ScreenUIAnimation Animation => _screenUIAnimation;
        public Canvas Canvas => GetComponent<Canvas>();
    }

    public abstract class ScreenUI<TUI, TCONTROLLER> : ScreenUI
        where TUI : ScreenUI<TUI, TCONTROLLER>
        where TCONTROLLER : ScreenController<TCONTROLLER, TUI>
    {
    }
}