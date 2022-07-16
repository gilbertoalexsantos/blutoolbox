using Bludk;
using BluEngine;
using UnityEngine;
using Zenject;

public class DebugManager : MonoBehaviour, ISceneAutoBinder
{
    private ScreenManager _screenManager;

    [Inject]
    public void Construct(ScreenManager screenManager)
    {
        _screenManager = screenManager;
    }

    public void Bark()
    {
        Debug.Log("Woof! Woof!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(_screenManager.GetType());
        }
    }
}