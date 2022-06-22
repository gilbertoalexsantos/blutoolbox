namespace Bludk
{
    public interface IClock
    {
        float SecondsSinceStartup { get; }
        int FrameCount { get; }
    }
}