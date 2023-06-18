using System;
using System.Collections;

namespace BluToolbox
{
    internal class ActionEnumerator : IEnumerator
    {
        private Action _action;
        private bool _moved;

        public object Current => null;

        public ActionEnumerator(Action action)
        {
            _action = action;
        }

        public bool MoveNext()
        {
            if (_moved)
            {
                return false;
            }
            else
            {
                _action();
                _moved = true;
                return true;
            }
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    internal class ActionEnumerator<Tin> : IEnumerator
    {
        private Action<Tin> _action;
        private bool _moved;
        private Tin _result;

        public object Current => null;

        public ActionEnumerator(Tin result, Action<Tin> action)
        {
            _result = result;
            _action = action;
        }

        public bool MoveNext()
        {
            if (_moved)
            {
                return false;
            }
            else
            {
                _action(_result);
                _moved = true;
                return false;
            }
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}