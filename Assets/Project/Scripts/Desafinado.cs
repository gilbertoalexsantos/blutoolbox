using System.Threading;
using BluToolbox;
using UnityEngine;

public class Desafinado : MonoBehaviour
{
  public BluButtonBh btn;

  private readonly CancellationTokenSource source = new();

  public void Start()
  {
    CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(
      btn.CreateCancellationToken(),
      source.Token
    );

    btn.SetOnClick(async () =>
    {
      Debug.Log(Time.frameCount + " Start of click");
      await Awaitable.NextFrameAsync();
      Debug.Log(Time.frameCount + " End of click");
    }, tokenSource.Token);
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