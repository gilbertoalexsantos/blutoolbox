using System;
using System.Collections;
using Bludk;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BluEngine
{
    public class BluButton : BluBehaviour
    {
        private Button Btn => GetCached<Button>();
        private TextMeshProUGUI Tmp => GetCached<TextMeshProUGUI>();

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

        public void SetText(string text)
        {
            Tmp.SetText(text);
        }

        public void SetInteractable(bool interactable)
        {
            Btn.interactable = interactable;
        }

        public void SetColor(Color color)
        {
            Btn.image.color = color;
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