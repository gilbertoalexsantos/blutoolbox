using System.Collections.Generic;
using System.Reflection;
using BluEngine;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Installers/GameStart", fileName = "GameStartInstaller")]
public class GameStartCustomScriptableInstaller : AppStartCustomScriptableInstaller
{
    protected override IEnumerable<Assembly> CustomInstallerAssemblies => new List<Assembly>
    {
        Assembly.GetAssembly(typeof(GameStartCustomScriptableInstaller))
    };
}