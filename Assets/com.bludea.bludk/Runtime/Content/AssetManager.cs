using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Bludk
{
    public class AssetManager
    {
        public IEnumerator<Either<T, Exception>> Load<T>(string path)
        {
            AsyncOperationHandle<T> ao = Addressables.LoadAssetAsync<T>(path);
            return ao.Then(() =>
            {
                if (ao.Status == AsyncOperationStatus.Succeeded)
                {
                    return ao.Result.AsLeft<T, Exception>();
                }
                else
                {
                    return ao.OperationException.AsRight<T, Exception>();
                }
            });
        }
    }
}