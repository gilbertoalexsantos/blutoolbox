using UnityEngine;

namespace BluToolbox
{
  public class DontDestroyOnLoad : MonoBehaviour
  {
    private void Awake()
    {
      DontDestroyOnLoad(gameObject);
    }
  }
}