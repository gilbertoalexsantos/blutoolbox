using System;
using System.Collections;
using UnityEngine.UI;

namespace Bludk
{
    public class BluButton : BluBehaviour
    {
        private Button Btn => GetCached<Button>();

        private Func<IEnumerator> _routineCb;
        private Action _simpleCb;
        private ICancelToken _token;

        public void SetOnClick(Action cb)
        {
            _simpleCb = cb;
            _routineCb = null;

            Btn.onClick.RemoveAllListeners();
            Btn.onClick.AddListener(OnBtnClicked);
        }

        public void SetOnClickRoutine(Func<IEnumerator> cb, ICancelToken token = null)
        {
            _simpleCb = null;
            _routineCb = cb;
            _token = token;

            Btn.onClick.RemoveAllListeners();
            Btn.onClick.AddListener(OnBtnClicked);
        }

        public void SetInteractable(bool interactable)
        {
            Btn.interactable = interactable;
        }

        private void OnBtnClicked()
        {
            ICancelToken token = CreateCancelToken();
            if (token.IsCancelled)
            {
                return;
            }

            if (_simpleCb == null)
            {
                _routineCb().Start(token);   
            }
            else
            {
                _simpleCb();
            }
        }

        private ICancelToken CreateCancelToken()
        {
            ICancelToken btnToken = gameObject.CreateCancelToken();
            return _token == null ? btnToken : new CompositeCancelToken(btnToken, _token);
        }
    }
}