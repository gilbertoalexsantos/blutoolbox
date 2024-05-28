using System;
using System.IO;
using UnityEngine;

namespace BluToolbox.Editor
{
  public static class PathUtils
  {
    public static string FromAssetPathToAbsolutePath(string assetPath)
    {
      string assetPrefix = $"Assets{Path.DirectorySeparatorChar}";
      if (!assetPath.StartsWith(assetPrefix))
      {
        throw new Exception($"AssetPath '{assetPath}' doesn't starts with prefix {assetPrefix}");
      }

      DirectoryInfo dataPathDirectory = new(Application.dataPath);
      if (dataPathDirectory.Parent == null)
      {
        throw new Exception($"Could not find parent directory for {dataPathDirectory}");
      }

      string dataPath = dataPathDirectory.Parent.FullName;
      return Path.Combine(dataPath, assetPath);
    }
  }
}