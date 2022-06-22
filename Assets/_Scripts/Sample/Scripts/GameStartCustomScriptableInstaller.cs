using System;
using BluEngine;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Installers/GameStart", fileName = "GameStartInstaller")]
public class GameStartCustomScriptableInstaller : AppStartCustomScriptableInstaller
{
    public override Type AnyTypeFromGameAssembly => typeof(GameStartCustomScriptableInstaller);
}