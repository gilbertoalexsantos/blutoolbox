using System.Collections;
using Bludk;
using BluEngine;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Game/GameStartStep", fileName = "GameStartStep")]
public class GameStartStep : LoadingStep
{
    private ScreenManager _screenManager;
    private LoadingScreenController.Factory _loadingScreenFactory;
    private MainScreenController.Factory _mainScreenControllerFactory;

    [Inject]
    public void Construct(
        LoadingScreenController.Factory loadingScreenFactory, 
        MainScreenController.Factory mainScreenControllerFactory
    )
    {
        _loadingScreenFactory = loadingScreenFactory;
        _mainScreenControllerFactory = mainScreenControllerFactory;
    }

    public override IEnumerator Execute()
    {
        var loadingScreenController = _loadingScreenFactory.Create();
        return loadingScreenController.Show()
            .Then(() => AwaitConstants.WaitTwoSeconds)
            .Then(() =>
            {
                var mainScreenController = _mainScreenControllerFactory.Create();
                return mainScreenController.CustomShow();
            });
    }
}