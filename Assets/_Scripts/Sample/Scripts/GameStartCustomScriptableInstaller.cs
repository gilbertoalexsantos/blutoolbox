using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BluEngine;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Game/Installers/GameStart", fileName = "GameStartInstaller")]
public class GameStartCustomScriptableInstaller : AppStartCustomScriptableInstaller
{
    public override void InstallBindings(DiContainer container)
    {
        BindClassInstallers(container);
    }

    private void BindClassInstallers(DiContainer container)
    {
        Type classInstallerType = typeof(ClassInstaller);
        IEnumerable<Type> customClassTypes = Assembly.GetAssembly(typeof(GameStartCustomScriptableInstaller)).GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(classInstallerType));
        foreach (Type type in customClassTypes)
        {
            ClassInstaller installer = (ClassInstaller) Activator.CreateInstance(type);
            installer.InstallBindings(container);
        }
    }
}