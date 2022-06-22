using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bludk.Editor;
using UnityEditor;
using UnityEngine;

public static class Builder
{
    [MenuItem("Blu/ExportPackage")]
    public static void ExportPackageMenuItem()
    {
        ExportPackageWithArgs(new()
        {
            "-output_path",
            Path.GetFullPath(Path.Combine(Application.dataPath, "..", "Builds", "bluengine.unitypackage"))
        });
    }

    public static void ExportPackage()
    {
        ExportPackageWithArgs(Environment.GetCommandLineArgs().ToList());
    }

    private static void ExportPackageWithArgs(List<string> args)
    {
        Log("BluEngine Builder: Starting build...");

        string outputPath = CommandLineUtils.GetArg("output_path", args);
        List<string> packagePaths = new()
        {
            "Assets/com.bludea.bludk",
            "Assets/com.bludea.bluengine",
            "Assets/Plugins"
        };

        Log("BluEngine Builder: Exporting package...");
        string fileName = "build.unitypackage";
        AssetDatabase.ExportPackage(
            packagePaths.ToArray(), 
            fileName,
            ExportPackageOptions.Recurse
        );
        if (!File.Exists(fileName))
        {
            Log("BluEngine Builder: Could not export package");
        }

        string directoryPath = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
        {
            Log($"Directory {directoryPath} does not exists. Creating it...");
            Directory.CreateDirectory(directoryPath);
            Log($"Directory {directoryPath} created with success!");
        }

        Log($"BluEngine Builder: Moving package to {outputPath}");
        File.Move(fileName, outputPath);

        if (File.Exists(outputPath))
        {
            Log("BluEngine Builder: Build finished with success!");
        }
        else
        {
            throw new Exception("BluEngine Builder: Build failed!");
        }
    }

    private static void Log(string msg)
    {
        Debug.Log($"BluEngine Builder: {msg}");
    }
}
