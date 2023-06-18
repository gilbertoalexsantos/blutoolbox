using System;
using System.Collections;
using System.Collections.Generic;

namespace BluToolbox
{
    public static class TxongaExtensions
    {
        public static IEnumerator Then(this IEnumerator parent, Action next)
        {
            return new Txonga<object, object>(parent, _ => new ActionEnumerator(next));
        }

        public static IEnumerator Then<Tin>(this IEnumerator<Tin> parent, Action<Tin> next)
        {
            return new Txonga<Tin, object>(parent, result =>
            {
                var funcEnumerator = new ActionEnumerator<Tin>(result, next);
                return funcEnumerator;
            });
        }

        public static IEnumerator<Tout> Then<Tout>(this IEnumerator parent, Func<Tout> nextFn)
        {
            return new Txonga<object, Tout>(parent, _ => nextFn().ToEnumerator());
        }

        public static IEnumerator Then(this IEnumerator parent, Func<IEnumerator> nextFn)
        {
            return new Txonga<object, object>(parent, _ => nextFn());
        }

        public static IEnumerator<Tout> Then<Tout>(this IEnumerator parent, Func<IEnumerator<Tout>> nextFn)
        {
            return new Txonga<object, Tout>(parent, _ => nextFn());
        }

        public static IEnumerator Then<Tin>(this IEnumerator<Tin> parent, Func<Tin, IEnumerator> nextFn)
        {
            return new Txonga<Tin, object>(parent, nextFn);
        }

        public static IEnumerator<Tout> Then<Tin, Tout>(
            this IEnumerator<Tin> parent,
            Func<Tin, IEnumerator<Tout>> nextFn)
        {
            return new Txonga<Tin, Tout>(parent, nextFn);
        }

        public static IEnumerator ThenAll<Tin>(this IEnumerator<Tin> parent, Func<Tin, IEnumerable<IEnumerator>> enumeratorsFn)
        {
            return new Txonga<Tin, object>(parent, v => new MultipleEnumerator(enumeratorsFn(v)));
        }

        public static IEnumerator ThenAll(this IEnumerator parent, Func<IEnumerable<IEnumerator>> enumeratorsFn)
        {
            return new Txonga<object, object>(parent, _ => new MultipleEnumerator(enumeratorsFn()));
        }

        public static IEnumerator<T> Yield<T>(this T obj)
        {
            yield return obj;
        }

        public static Txonga Start(this IEnumerator enumerator, ICancelToken token = null)
        {
            var txonga = new Txonga<object, object>(enumerator);
            txonga.SetCancelToken(token);
            return txonga.Start(Runner.Instance);
        }

        public static Txonga<Tout> Start<Tout>(this IEnumerator<Tout> enumerator, ICancelToken token = null)
        {
            var txonga = new Txonga<Tout>(enumerator);
            txonga.SetCancelToken(token);
            txonga.Start(Runner.Instance);
            return txonga;
        }

        public static IEnumerator StartAndAwait(this IEnumerator enumerator, ICancelToken token = null)
        {
            var txonga = new Txonga<object, object>(enumerator);
            txonga.SetCancelToken(token);
            return txonga.Start(Runner.Instance).Await();
        }
    }
}