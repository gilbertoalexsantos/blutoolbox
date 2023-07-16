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

    public void SetOnClickRoutine(Func<Task> task, Maybe<CancellationToken> token)
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

      if (_cb.IsLeft)
      {
        TaskExtensions.RunOnMainThread(_cb.Left, source.Token);
      }
      else
      {
        _cb.Right();
        source.Dispose();
      }
    }

    private CancellationTokenSource CreateCancelTokenSource()
    {
      CancellationToken token1 = gameObject.GetOrAddComponent<OnDestroyBehaviour>().Token;
      CancellationToken token2 = _maybeToken.HasValue ? _maybeToken.Value : CancellationToken.None;
      return CancellationTokenSource.CreateLinkedTokenSource(token1, token2);
    }
  }
}