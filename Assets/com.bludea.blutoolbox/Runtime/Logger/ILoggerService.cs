namespace BluToolbox
{
  public interface ILoggerService : ILogger
  {
    void Init(LogType enabledLogs);
  }
}
