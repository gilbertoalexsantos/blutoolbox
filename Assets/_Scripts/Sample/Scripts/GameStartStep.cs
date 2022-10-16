using System.Collections;
using Bludk;
using BluEngine;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Game/GameStartStep", fileName = "GameStartStep")]
public class GameStartStep : LoadingStep
{
    private ScreenManager _screenManager;

    [Inject]
    public void Construct(ScreenManager screenManager)
    {
        _screenManager = screenManager;
    }

    public override IEnumerator Execute()
    {
        return _screenManager.Load<LoadingScreen, LoadingScreenController>()
            .Then(controller =>
            {
                return controller.Show()
                    .Then(() => AwaitConstants.WaitTwoSeconds)
                    .Then(() =>
                    {
                        return _screenManager.Load<MainScreen, MainScreenController>()
                            .Then(mainScreenController => mainScreenController.CustomShow());
                    });
            });
    }
}