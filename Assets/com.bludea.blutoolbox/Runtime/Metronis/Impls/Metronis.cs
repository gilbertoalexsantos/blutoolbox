using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BluToolbox
{
  public class Metronis : IMetronis
  {
    private readonly Dictionary<string, Stopwatch> _trackers = new();
    private readonly ILogger _logger;

    public Metronis(ILogger logger)
    {
      _logger = logger;
    }

    public IDisposable StartTrack(string key)
    {
      Stopwatch stopwatch = new();
      _trackers[key] = stopwatch;
      stopwatch.Start();
      return new EmptyDisposableHolder(() => StopTrack(key));
    }

    private void StopTrack(string key)
    {
      if (_trackers.TryGetValue(key, out Stopwatch stopwatch))
      {
        stopwatch.Stop();
        long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        _logger.Info($"[{DateTime.UtcNow:O}] Key: {key}, Time: {elapsedMilliseconds} ms");
        _trackers.Remove(key);
      }
      else
      {
        _logger.Info($"[{DateTime.UtcNow:O}] Key: {key} not found or already stopped.");
      }
    }
  }
}
