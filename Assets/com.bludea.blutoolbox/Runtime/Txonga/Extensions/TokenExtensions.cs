using UnityEngine;

namespace BluToolbox
{
    public static class TokenExtensions
    {
        public static ICancelToken CreateCancelToken(this GameObject go)
        {
            return new GameObjectCancelToken(go);
        }
    }
}