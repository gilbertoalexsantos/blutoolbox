namespace BluToolbox.Injector
{
  public interface IInjectorBinder
  {
    IInjectorBinder Bind<T>();
    IInjectorBinder To<T>();

    void AsValue<T>(T value);
    void AsSingleton();
    void AsTransient();

    T Resolve<T>();
  }
}