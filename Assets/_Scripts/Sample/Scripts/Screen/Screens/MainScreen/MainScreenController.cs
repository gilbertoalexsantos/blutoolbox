using System.Collections;
using Bludk;

public class MainScreenController : ScreenController<MainScreenController, MainScreen>
{
    private readonly CancelToken _token = new();

    private readonly DebugManager _debugManager;

    public MainScreenController(ScreenManager screenManager, DebugManager debugManager) : base(screenManager)
    {
        _debugManager = debugManager;
    }

    public IEnumerator CustomShow()
    {
        return SetupOnBeforeShow()
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
        _debugManager.Bark();
        _token.Cancel();
        yield return null;
    }

    public override void Dispose()
    {
        _token.Dispose();
    }
}