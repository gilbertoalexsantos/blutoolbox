using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Bludk
{
  public class AddressablesContentDatasource : IContentDatasource
  {
    public IEnumerator<Either<T, Exception>> LoadAsset<T>(string path)
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

    public Task<Either<T, Exception>> Test<T>()
    {
      if (UnityEngine.Random.Range(0, 5f) <= 1f)
      {
        T t = default;
        var l = t.AsLeft<T, Exception>();
        return Task.FromResult(l);
      }
      else
      {
        Exception t = default;
        var r = t.AsRight<T, Exception>();
        return Task.FromResult(r);
      }
    }
  }
}