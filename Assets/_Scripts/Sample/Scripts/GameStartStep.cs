using System.Collections;
using Bludk;
using BluEngine;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Game/GameStartStep", fileName = "GameStartStep")]
public class GameStartStep : LoadingStep
{
  private ScreenManager _screenManager;
  private BuildInfoManager _buildInfoManager;

  [Inject]
  public void Construct(
    ScreenManager screenManager,
    BuildInfoManager buildInfoManager)
  {
    _screenManager = screenManager;
    _buildInfoManager = buildInfoManager;
  }

  public override IEnumerator Execute()
  {
    BuildInfoData buildInfoData = _buildInfoManager.BuildInfoData;
    Debug.Log(buildInfoData);

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