using System.Collections.Generic;
using UnityEngine;

namespace BluEngine
{
  [CreateAssetMenu(menuName = "BluEngine/Data/LoadingStepsData", fileName = "LoadingStepsData")]
  public class LoadingStepsData : ScriptableObject
  {
    [SerializeField]
    private List<LoadingStep> _loadingSteps;

    public List<LoadingStep> LoadingSteps => _loadingSteps;
  }
}