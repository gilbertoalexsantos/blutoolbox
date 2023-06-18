using BluToolbox;
using UnityEngine;

public class GameObjectCancelToken : ICancelToken
{
  private readonly GameObject _go;

  public bool IsCancelled => _go == null;

  public GameObjectCancelToken(GameObject go)
  {
    _go = go;
  }
}