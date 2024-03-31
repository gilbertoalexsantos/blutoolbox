using System;
using UnityEditor;
using UnityEngine;

namespace BluToolbox.Editor
{
  public static class AssetUtils
  {
    public static void EditAssets (Action action)
    {
      AssetDatabase.StartAssetEditing ();
      try
      {
        action (); 
      }
      catch (Exception e)
      {
        Debug.LogError ($"Exception while executing {nameof(EditAssets)}. {e}");
      }
      finally
      {
        AssetDatabase.StopAssetEditing ();
      }
    }
  }
}