using System;
using System.Collections.Generic;
using Bludk;

namespace BluEngine
{
  public class LoadingStepsDatasource : IAsyncDatasource<IEnumerable<LoadingStep>>
  {
    private readonly AssetManager _assetManager;

    public LoadingStepsDatasource(AssetManager assetManager)
    {
      _assetManager = assetManager;
    }

    public IEnumerator<IEnumerable<LoadingStep>> LoadAsync()
    {
      return _assetManager.Load<LoadingStepsData>("LoadingStepsData")
        .Then((Either<LoadingStepsData, Exception> either) =>
        {
          if (either.IsRight)
          {
            throw either.Right;
          }

          List<LoadingStep> loadingSteps = either.Left.LoadingSteps;
          return ((IEnumerable<LoadingStep>)loadingSteps).ToEnumerator();
        });
    }
  }
}