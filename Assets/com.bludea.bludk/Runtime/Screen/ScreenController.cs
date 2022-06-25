using System;
using System.Collections;
using Zenject;

namespace Bludk
{
    public abstract class ScreenController : IDisposable
    {
        public ScreenUI BaseUI { get; private set; }

        public void SetUI(ScreenUI ui)
        {
            BaseUI = ui;
        }

        public abstract void Dispose();
    }

    public abstract class ScreenController<TCONTROLLER, TUI> : ScreenController
        where TCONTROLLER : ScreenController<TCONTROLLER, TUI>
        where TUI : ScreenUI<TUI, TCONTROLLER>
    {
        protected readonly ScreenManager _screenManager;

        public TUI UI => (TUI) BaseUI;

        public ScreenController(ScreenManager screenManager)
        {
            _screenManager = screenManager;
        }

        public IEnumerator Show()
        {
            return SetupOnBeforeShow()
                .Then(PlayShowAnimation)
                .Then(SetupAfterShow);
        }

        public IEnumerator Hide()
        {
            return PlayHideAnimation()
                .Then(SetupAfterHide);
        }

        public IEnumerator SetupOnBeforeShow()
        {
            UI.Canvas.overrideSorting = true;
            UI.Canvas.sortingOrder = _screenManager.IncSortingOrder();
            yield return null;
        }

        protected void SetupAfterShow()
        {
            _screenManager.SetupAfterShow(this);
        }

        protected void SetupAfterHide()
        {
            _screenManager.SetupAfterHide(this);
        }

        protected IEnumerator PlayShowAnimation()
        {
            return UI.Animation.ShowAnimation(this);
        }

        protected IEnumerator PlayHideAnimation()
        {
            return UI.Animation.HideAnimation(this);
        }
    }
}