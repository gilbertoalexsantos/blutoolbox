namespace Bludk
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ICommand<T>
    {
        T Execute();
    }
}