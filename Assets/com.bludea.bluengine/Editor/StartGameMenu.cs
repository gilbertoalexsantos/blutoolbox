using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace BluEngine.Editor
{
    public static class StartGameMenu
    {
        static StartGameMenu()
        {
            RegisterForPlayModeStateCallback();
        }

        [MenuItem("Blu/Start %g")]
        public static void StartGame()
        {
            SceneAsset sceneAsset = Resources.Load<EditorSettings>(EditorSettings.ResourcesPath).StartScene;
            EditorSceneManager.playModeStartScene = sceneAsset;
            EditorApplication.isPlaying = true;
        }

        private static void OnPlayModeState(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                EditorSceneManager.playModeStartScene = null;
            }
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            RegisterForPlayModeStateCallback();
        }

        private static void RegisterForPlayModeStateCallback()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeState;
            EditorApplication.playModeStateChanged += OnPlayModeState;
        }
    }
}