using System;

namespace BluToolbox
{
  public interface IMetronis
  {
    IDisposable StartTrack(string key);
  }
}
