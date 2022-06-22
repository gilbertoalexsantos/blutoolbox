using Zenject;

namespace BluEngine
{
    public abstract class ClassInstaller
    {
        public abstract void InstallBindings(DiContainer container);
    }
}