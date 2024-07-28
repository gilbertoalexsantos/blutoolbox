using UnityEngine;

namespace BluToolbox
{
  public class DontDestroyOnLoadBh : MonoBehaviour
  {
    private void Awake()
    {
      DontDestroyOnLoad(gameObject);
    }
  }
}