using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BluToolbox
{
    public class BluButton : BluBehaviour
    {
        [RequiredField, SerializeField]
        private Button _btn;

        private Func<IEnumerator> _routineCb;
        private Action _simpleCb;
        private ICancelToken _token;

        public void SetOnClick(Action cb)
        {
            _simpleCb = cb;
            _routineCb = null;

            _btn.onClick.RemoveAllListeners();
            _btn.onClick.AddListener(OnBtnClicked);
        }

        public void SetOnClickRoutine(Func<IEnumerator> cb, ICancelToken token = null)
        {
            _simpleCb = null;
            _routineCb = cb;
            _token = token;

            _btn.onClick.RemoveAllListeners();
            _btn.onClick.AddListener(OnBtnClicked);
        }

        public void SetInteractable(bool interactable)
        {
            _btn.interactable = interactable;
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