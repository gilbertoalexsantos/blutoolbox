using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BluToolbox.Editor;
using UnityEditor;
using UnityEngine;

public static class Builder
{
  [MenuItem("Blu/ExportPackage")]
  public static void ExportPackageMenuItem()
  {
    ExportPackageWithArgs(new List<string>
    {
      "-output_path",
      Path.GetFullPath(Path.Combine(Application.dataPath, "..", "Builds", "blutoolbox.unitypackage"))
    });
  }

  public static void ExportPackage()
  {
    ExportPackageWithArgs(Environment.GetCommandLineArgs().ToList());
  }

  private static void ExportPackageWithArgs(List<string> args)
  {
    Log("BluToolbox Builder: Starting build...");

    string outputPath = CommandLineUtils.GetArg("output_path", args);
    List<string> packagePaths = new()
    {
      "Assets/com.bludea.BluToolbox",
    };

    Log("BluToolbox Builder: Exporting package...");
    string fileName = "build.unitypackage";
    AssetDatabase.ExportPackage(
      packagePaths.ToArray(), 
      fileName,
      ExportPackageOptions.Recurse
    );
    if (!File.Exists(fileName))
    {
      Log("BluToolbox Builder: Could not export package");
    }

    string directoryPath = Path.GetDirectoryName(outputPath);
    if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
    {
      Log($"Directory {directoryPath} does not exists. Creating it...");
      Directory.CreateDirectory(directoryPath);
      Log($"Directory {directoryPath} created with success!");
    }

    Log($"BluToolbox Builder: Moving package to {outputPath}");
    File.Move(fileName, outputPath);

    if (File.Exists(outputPath))
    {
      Log("BluToolbox Builder: Build finished with success!");
    }
    else
    {
      throw new Exception("BluToolbox Builder: Build failed!");
    }
  }

  private static void Log(string msg)
  {
    Debug.Log($"BluToolbox Builder: {msg}");
  }
}
