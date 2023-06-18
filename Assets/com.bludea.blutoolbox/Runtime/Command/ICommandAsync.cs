using System.Collections;
using System.Collections.Generic;

namespace BluToolbox
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