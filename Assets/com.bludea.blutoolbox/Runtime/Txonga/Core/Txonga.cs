using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BluToolbox
{
    public abstract class Txonga : IEnumerator
    {
        public static int GlobalId;
        public abstract bool MoveNext();
        public abstract void Reset();
        public abstract object Current { get; }
        public abstract IEnumerator Await();
        public abstract bool IsCompleted { get; }
        public abstract bool IsCancelled { get; }
        public abstract void Cancel();
        public abstract void SetCancelToken(ICancelToken token);
    }

    public class Txonga<Tout> : Txonga<object, Tout>
    {
        public Txonga(IEnumerator head) : base(head)
        {
        }

        public Txonga(IEnumerator<Tout> head) : base(head)
        {
        }

        public Txonga(IEnumerator parent, Func<object, IEnumerator> headFn) : base(parent, headFn)
        {
        }
    }

    public class Txonga<Tin, Tout> : Txonga, IEnumerator<Tout>
    {
        private MonoBehaviour _runner;
        private IEnumerator _parent;
        private IEnumerator _head;
        private Func<Tin, IEnumerator> _headFn;
        private bool _finishedParent;
        private bool _finishedHead;
        private bool _isCancelled;
        private object _current;
        private ICancelToken _token;
        private Coroutine _coroutine;
        private int _id = Txonga.GlobalId++;

        public override object Current => _current;
        Tout IEnumerator<Tout>.Current => (Tout) Current;
        public override bool IsCompleted => _finishedParent && _finishedHead;
        public override bool IsCancelled
        {
            get
            {
                if (_token != null && _token.IsCancelled)
                {
                    return true;
                }

                return _isCancelled;
            }   
        }
        public Tout Result => (Tout) Current;

        public Txonga(IEnumerator head)
        {
            _head = head;
            _finishedParent = true;
        }

        public Txonga(IEnumerator<Tout> head)
        {
            _head = head;
            _finishedParent = true;
        }

        public Txonga(IEnumerator parent, Func<Tin, IEnumerator> headFn)
        {
            _parent = parent;
            _headFn = headFn;
        }

        public Txonga<Tin, Tout> Start(MonoBehaviour runner)
        {
            _runner = runner;
            _coroutine = _runner.StartCoroutine(this);
            return this;
        }

        public override IEnumerator Await()
        {
            while (!IsCompleted && !IsCancelled)
            {
                yield return null;
            }

            if (IsCancelled)
            {
                yield return null;
            }
            else
            {
                yield return Current is YieldInstruction ? null : Current;   
            }
        }

        public override void Cancel()
        {
            _isCancelled = true;
            MakeSureRunnerIsStopped();
        }

        public override void SetCancelToken(ICancelToken token)
        {
            _token = token;
        }

        public override bool MoveNext()
        {
            if (IsCompleted || IsCancelled)
            {
                MakeSureRunnerIsStopped();
                return false;
            }

            if (!_finishedParent)
            {
                bool movedParent = MoveIEnumerator(_parent);
                _current = _parent.Current;
                if (movedParent)
                {
                    return true;
                }
                else
                {
                    _finishedParent = true;
                }
            }

            // Amarrada
            if (IsCancelled)
            {
                MakeSureRunnerIsStopped();
                return false;
            }

            if (_head == null)
            {
                _head = _current == null ? _headFn(default) : _headFn((Tin) _current);
                _headFn = null;
            }

            bool movedHead = MoveIEnumerator(_head);
            _current = _head.Current;
            if (movedHead)
            {
                return true;
            }
            else
            {
                _finishedHead = true;
                return false;
            }
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        private bool MoveIEnumerator(IEnumerator enumerator)
        {
            if (IsCancelled)
            {
                return false;
            }

            return enumerator.MoveNext();
        }

        private void MakeSureRunnerIsStopped()
        {
            if (_coroutine != null && _runner != null)
            {
                _runner.StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        public void Dispose()
        {
            Cancel();
        }
    }
}