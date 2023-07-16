using System.Threading;
using BluToolbox;
using UnityEngine;

public class Desafinado : MonoBehaviour
{ 
  public BluButton btn;

  private readonly CancellationTokenSource source = new();

  public void Start()
  {
    CancellationToken token = btn.CreateCancellationToken();
    btn.SetOnClickRoutine(async () =>
    {
      Debug.Log(Time.frameCount);
      while (true)
      {
        Debug.Log(Time.frameCount);
        await Awaitable.NextFrameAsync(token);
      }
    }, token.Some());
  }

  public void Update()
  {
    if (Input.GetKeyDown(KeyCode.A))
    {
      source.Cancel();
      source.Dispose();
    }
  }
}
