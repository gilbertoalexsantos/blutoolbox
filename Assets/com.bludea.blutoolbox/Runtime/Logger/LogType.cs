using System;

namespace BluToolbox
{
  [Flags]
  public enum LogType
  {
    Info = 2 << 0,
    Warning = 2 << 1,
    Error = 2 << 2,
    All = Info | Warning | Error
  }
}