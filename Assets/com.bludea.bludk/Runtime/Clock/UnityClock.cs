using UnityEngine;

namespace Bludk
{
    public class UnityClock : IClock
    {
        public float SecondsSinceStartup => Time.realtimeSinceStartup;
        public int FrameCount => Time.frameCount;
    }
}