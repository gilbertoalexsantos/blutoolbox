using System;
using System.Collections.Generic;

namespace Bludk
{
  public interface IContentDatasource
  {
    IEnumerator<Either<T, Exception>> LoadAsset<T>(string path);
  }
}