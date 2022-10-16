using System;
using System.Collections;

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
            return SetupBeforeShow()
                .Then(PlayShowAnimation)
                .Then(SetupAfterShow);
        }

        public IEnumerator Hide()
        {
            return SetupBeforeHide()
                .Then(PlayHideAnimation)
                .Then(SetupAfterHide);
        }

        protected IEnumerator SetupBeforeShow()
        {
            UI.Canvas.overrideSorting = true;
            UI.Canvas.sortingOrder = _screenManager.IncSortingOrder();
            return TxongaHelper.Empty;
        }

        protected IEnumerator SetupAfterShow()
        {
            _screenManager.SetupAfterShow(this);
            return TxongaHelper.Empty;
        }

        protected IEnumerator SetupBeforeHide()
        {
            return TxongaHelper.Empty;
        }

        protected IEnumerator SetupAfterHide()
        {
            _screenManager.SetupAfterHide(this);
            return TxongaHelper.Empty;
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