using System.Collections.Generic;
using Bludk;
using UnityEngine;

namespace BluEngine
{
    public class LoadingStepsDatasource : IAsyncDatasource<IEnumerable<LoadingStep>>
    {
        public IEnumerator<IEnumerable<LoadingStep>> LoadAsync()
        {
            GameSettings gameSettings = Resources.Load<GameSettings>(GameSettings.ResourcesPath);

            List<LoadingStep> loadingSteps = new List<LoadingStep>();
            foreach (string resourcesFolder in gameSettings.LoadingStepsResourcesFolders)
            {
                loadingSteps.AddRange(Resources.LoadAll<LoadingStep>(resourcesFolder));
            }

            return ((IEnumerable<LoadingStep>)loadingSteps).ToEnumerator();
        }
    }
}