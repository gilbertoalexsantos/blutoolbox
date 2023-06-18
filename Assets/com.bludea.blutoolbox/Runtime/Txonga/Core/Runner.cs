using UnityEngine;

namespace BluToolbox
{
  public class Runner : MonoBehaviour
  {
    private static Runner _instance;

    public static Runner Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new GameObject("Runner").AddComponent<Runner>();
        }

        return _instance;
      }
    }

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }
  }
}