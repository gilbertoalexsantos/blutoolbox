using UnityEngine;

namespace BluToolbox
{
  public class UnityLogger : ILogger
  {
    private LogType _enabledLogs;

    public void Init(LogType enabledLogs)
    {
      _enabledLogs = enabledLogs;
    }

    public void Info(string msg)
    {
      if (!IsEnabled(LogType.Info))
      {
        return;
      }

      Debug.Log(msg);
    }

    public void Warning(string msg)
    {
      if (!IsEnabled(LogType.Warning))
      {
        return;
      }

      Debug.LogWarning(msg);
    }

    public void Error(string msg)
    {
      if (!IsEnabled(LogType.Error))
      {
        return;
      }

      Debug.LogError(msg);
    }

    private bool IsEnabled(LogType log)
    {
      return (_enabledLogs & log) == log;
    }
  }
}