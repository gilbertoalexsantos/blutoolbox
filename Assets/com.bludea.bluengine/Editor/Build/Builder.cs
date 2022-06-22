using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bludk.Editor;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BluEngine.Editor
{
    public static class Builder
    {
        [MenuItem("Blu/Build/Android")]
        public static void BuildAndroidFromEditor()
        {
            List<string> args = new List<string>
            {
                "-bundle_version", "0.1",
                "-build_number", "0",
                "-output_path",
                Path.GetFullPath(Path.Combine(Application.dataPath, "..", "Builds", "editor_build_android.apk")),
                "-keystore_path",
                Path.GetFullPath(Path.Combine(Application.dataPath, "..", "tools", "debug.keystore")),
                "-keystore_pass", "nftown",
                "-keystore_alias_name", "nftown",
                "-keystore_alias_pass", "nftown",
            };
            BuildAndroidWithArgs(args);
        }

        [MenuItem("Blu/Build/Mac")]
        public static void BuildMacFromEditor()
        {
            List<string> args = new List<string>
            {
                "-bundle_version", "0.1",
                "-build_number", "0",
                "-output_path",
                Path.GetFullPath(Path.Combine(Application.dataPath, "..", "Builds", "editor_build_mac")),
            };
            BuildMacWithArgs(args);
        }

        public static void BuildAndroid()
        {
            BuildAndroidWithArgs(Environment.GetCommandLineArgs().ToList());
        }

        public static void BuildMac()
        {
            BuildMacWithArgs(Environment.GetCommandLineArgs().ToList());
        }

        public static void BuildAndroidWithArgs(List<string> args)
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            }

            string bundleVersion = GetArg("bundle_version", args);
            int buildNumber = Int32.Parse(GetArg("build_number", args)) + 1;
            CreateBuildInfoFile(bundleVersion, buildNumber);

            PlayerSettings.productName = "nftown";
            PlayerSettings.companyName = "nftown";
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.nftown.nftown.ci");
            PlayerSettings.bundleVersion = bundleVersion;
            PlayerSettings.Android.bundleVersionCode = buildNumber;

            PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
            PlayerSettings.runInBackground = true;

            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keystoreName = GetArg("keystore_path", args);
            PlayerSettings.Android.keystorePass = GetArg("keystore_pass", args);
            PlayerSettings.Android.keyaliasName = GetArg("keystore_alias_name", args);
            PlayerSettings.Android.keyaliasPass = GetArg("keystore_alias_pass", args);

            BuildOptions optionFlags =
                BuildOptions.Development |
                BuildOptions.AllowDebugging |
                BuildOptions.EnableDeepProfilingSupport |
                BuildOptions.CleanBuildCache;
            var buildPlayerOptions = new BuildPlayerOptions
            {
                target = BuildTarget.Android,
                targetGroup = BuildTargetGroup.Android,
                locationPathName = GetArg("output_path", args),
                options = optionFlags,
                scenes = GetBuildScenes(),
            };

            BuildReport buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);
            Debug.Log($"Result: {buildReport.summary.result}");
            if (buildReport.summary.result != BuildResult.Succeeded)
            {
                throw new Exception("Build failed");
            }
        }

        public static void BuildMacWithArgs(List<string> args)
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.StandaloneOSX)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
            }

            if (HasArg("fix_mono_path", args))
            {
                FixMonoPath();
            }

            string bundleVersion = GetArg("bundle_version", args);
            int buildNumber = Int32.Parse(GetArg("build_number", args)) + 1;
            CreateBuildInfoFile(bundleVersion, buildNumber);

            PlayerSettings.productName = "nftown";
            PlayerSettings.companyName = "nftown";
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, "com.nftown.nftown.ci");
            PlayerSettings.bundleVersion = bundleVersion;

            PlayerSettings.fullScreenMode = FullScreenMode.MaximizedWindow;
            PlayerSettings.resizableWindow = true;
            PlayerSettings.runInBackground = true;
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono2x);
            PlayerSettings.macOS.buildNumber = buildNumber.ToString();

            BuildOptions optionFlags =
                BuildOptions.Development |
                BuildOptions.AllowDebugging |
                BuildOptions.EnableDeepProfilingSupport |
                BuildOptions.CleanBuildCache;
            string outputPath = GetArg("output_path", args);
            var buildPlayerOptions = new BuildPlayerOptions
            {
                target = BuildTarget.StandaloneOSX,
                targetGroup = BuildTargetGroup.Standalone,
                locationPathName = outputPath,
                options = optionFlags,
                scenes = GetBuildScenes(),
            };

            BuildReport buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);
            Debug.Log($"Result: {buildReport.summary.result}");
            if (buildReport.summary.result != BuildResult.Succeeded)
            {
                throw new Exception("Build failed");
            }
        }

        private static void CreateBuildInfoFile(string buildVersion, int buildNumber)
        {
            string assetFullPath = Path.Combine(
                Application.dataPath,
                "BlueEngine",
                "Resources",
                "BluEngine",
                "buildInfo.json"
            );
            BuildInfoData infoData = new BuildInfoData(buildVersion, buildNumber);
            string systemInfoDataJson = JsonConvert.SerializeObject(infoData);
            File.WriteAllText(assetFullPath, systemInfoDataJson);
            AssetDatabase.Refresh();
        }

        private static void FixMonoPath()
        {
            string sourcePath =
                "/opt/unity/Editor/Data/PlaybackEngines/MacStandaloneSupport/Variations/macos_x64arm64_player_development_mono";
            string linkPath =
                "/opt/unity/Editor/Data/PlaybackEngines/MacStandaloneSupport/Variations/macos_x64ARM64_player_development_mono";
            string cmd = $"ln -s {sourcePath} {linkPath}";
            CommandLineUtils.RunBashCommand(cmd);
        }

        private static string[] GetBuildScenes()
        {
            return EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();
        }

        private static string GetArg(string name, List<string> args)
        {
            for (int i = 0; i < args.Count; i++)
            {
                if (args[i] == $"-{name}")
                {
                    return args[i + 1];
                }
            }

            throw new Exception($"Missing arg {name}");
        }

        private static bool HasArg(string name, List<string> args)
        {
            for (int i = 0; i < args.Count; i++)
            {
                if (args[i] == $"-{name}")
                {
                    return true;
                }
            }

            return false;
        }
    }
}