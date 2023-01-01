using System;
using System.Collections.Generic;
using Bludk;

namespace BluEngine
{
  public class LoadingStepsDatasource : IAsyncDatasource<IEnumerable<LoadingStep>>
  {
    private readonly AddressablesContentDatasource _contentManager;

    public LoadingStepsDatasource(AddressablesContentDatasource contentManager)
    {
      _contentManager = contentManager;
    }

    public IEnumerator<IEnumerable<LoadingStep>> LoadAsync()
    {
      return _contentManager.LoadAsset<LoadingStepsData>("LoadingStepsData")
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