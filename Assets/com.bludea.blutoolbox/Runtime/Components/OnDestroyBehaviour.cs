using System.Threading;
using UnityEngine;

namespace BluToolbox
{
  public class OnDestroyBehaviour : MonoBehaviour
  {
    private readonly CancellationTokenSource _source = new();

    public CancellationToken Token => _source.Token;

    public void OnDestroy()
    {
      _source.Cancel();
      _source.Dispose();
    }
  }
}