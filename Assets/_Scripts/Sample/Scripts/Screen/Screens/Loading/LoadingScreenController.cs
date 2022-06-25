using System.Collections;
using Bludk;
using Zenject;

public class LoadingScreenController : ScreenController<LoadingScreenController, LoadingScreen>
{
    public LoadingScreenController(ScreenManager screenManager) : base(screenManager)
    {
    }

    public IEnumerator CustomShow(int secondsToHide)
    {
        return SetupOnBeforeShow()
            .Then(PlayShowAnimation)
            .Then(SetupAfterShow)
            .Then(() => AwaitConstants.WithSeconds(secondsToHide))
            .Then(Hide);
    }

    public override void Dispose()
    {
    }
}