using System.Collections;
using Bludk;
using Zenject;

public class MainScreenController : ScreenController<MainScreenController, MainScreen>
{
    private CancelToken _token = new();

    public MainScreenController(ScreenManager screenManager) : base(screenManager)
    {
    }

    public IEnumerator CustomShow()
    {
        return Init()
            .Then(Setup)
            .Then(PlayShowAnimation)
            .Then(SetupAfterShow);
    }

    private void Setup()
    {
        UI.Btn1.SetOnClickRoutine(OnBtn1Click, _token);
        UI.Btn2.SetOnClickRoutine(OnBtn2Click, _token);
    }

    private IEnumerator OnBtn1Click()
    {
        UI.Btn1.SetInteractable(false);
        UnityEngine.Debug.LogError("OnBtn1Clicked!");
        return AwaitConstants.WaitTwoSeconds
            .Then(() =>
            {
            })
            .Then(() =>
            {
                UnityEngine.Debug.LogError("OnBtn1Clicked!");
                UI.Btn1.SetInteractable(true);
            });
    }

    private IEnumerator OnBtn2Click()
    {
        UnityEngine.Debug.LogError("OnBtn2Clicked!");
        _token.Cancel();
        yield return null;
    }

    public override void Dispose()
    {
        _token.Dispose();
    }

    public class Factory : PlaceholderFactory<MainScreenController>
    {
        
    }
}