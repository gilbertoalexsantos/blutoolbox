using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BluToolbox
{
  public class BluButton : BluBehaviour
  {
    [SerializeField]
    private Button _btn;

    private Either<Func<Task>, Action> _cb;
    private Maybe<CancellationToken> _maybeToken;

    public void SetOnClick(Action cb)
    {
      _cb = cb.AsRight<Func<Task>, Action>();

      _btn.onClick.RemoveAllListeners();
      _btn.onClick.AddListener(OnBtnClicked);
    }

    public void SetOnClick(Func<Task> task, Maybe<CancellationToken> token)
    {
      _cb = task.AsLeft<Func<Task>, Action>();
      _maybeToken = token;

      _btn.onClick.RemoveAllListeners();
      _btn.onClick.AddListener(OnBtnClicked);
    }

    public void SetInteractable(bool interactable)
    {
      _btn.interactable = interactable;
    }

    private void OnBtnClicked()
    {
      CancellationTokenSource source = CreateCancelTokenSource();
      if (source.Token.IsCancellationRequested)
      {
        source.Dispose();
        return;
      }

      if (_cb.TryGetLeft(out Func<Task> task))
      {
        TaskHelper.FireAndForget(async () =>
        {
          await task();
          source.Dispose();
        }, source.Token);
      }
      else if (_cb.TryGetRight(out Action action))
      {
        action();
        source.Dispose();
      }
    }

    private CancellationTokenSource CreateCancelTokenSource()
    {
      CancellationToken token1 = gameObject.GetOrAddComponent<OnDestroyBehaviour>().Token;
      CancellationToken token2 = _maybeToken.TryGetValue(out CancellationToken v) ? v : CancellationToken.None;
      return CancellationTokenSource.CreateLinkedTokenSource(token1, token2);
    }
  }
}