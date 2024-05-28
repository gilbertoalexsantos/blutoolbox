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
    btn.SetOnClick(async () =>
    {
      Debug.Log(Time.frameCount + " Start of click");
      await Awaitable.NextFrameAsync();
      Debug.Log(Time.frameCount + " End of click");
    }, token);
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