using UnityEngine;

namespace BluToolbox
{
  public class SetActiveOnAwakeBh : MonoBehaviour
  {
    [SerializeField]
    private bool _setActive;

    private void Awake()
    {
      gameObject.SetActive(_setActive);
    }
  }
}