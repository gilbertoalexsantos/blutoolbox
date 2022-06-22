using UnityEngine;

namespace Bludk
{
    public interface IScreenManagerInfo
    {
        Canvas RootCanvas { get; }
        int StartSortingOrder { get; } 
    }
}