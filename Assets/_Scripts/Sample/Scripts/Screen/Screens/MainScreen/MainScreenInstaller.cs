using BluEngine;
using UnityEngine.Scripting;
using Zenject;

[Preserve]
public class MainScreenInstaller : ClassInstaller
{
    public override void InstallBindings(DiContainer container)
    {
        container.BindFactory<
            MainScreenController, 
            MainScreenController.Factory
        >();
    }
}