namespace BluToolbox
{
  public interface ILogger
  {
    void Init(LogType enabledLogs);
    void Info(string msg);
    void Warning(string msg);
    void Error(string msg);
  }
}