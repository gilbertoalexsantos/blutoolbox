using System.Threading.Tasks;

namespace BluToolbox
{
  public interface ICommandAsync
  {
    Task Execute();
  }

  public interface ICommandAsync<T>
  {
    Task<T> Execute();
  }
}