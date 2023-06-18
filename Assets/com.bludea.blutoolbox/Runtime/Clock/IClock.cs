namespace BluToolbox
{
    public interface IClock
    {
        float SecondsSinceStartup { get; }
        int FrameCount { get; }
    }
}