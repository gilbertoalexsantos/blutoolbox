using BluEngine;
using UnityEngine.Scripting;
using Zenject;

[Preserve]
public class LoadingScreenInstaller : ClassInstaller
{
    public override void InstallBindings(DiContainer container)
    {
        container.BindFactory<
            LoadingScreenController, 
            LoadingScreenController.Factory
        >();
    }
}