using System.Collections;
using System.Collections.Generic;

namespace Bludk
{
    public interface ICommandAsync
    {
        IEnumerator Execute();
    }

    public interface ICommandAsync<T>
    {
        IEnumerator<T> Execute();
    }
}