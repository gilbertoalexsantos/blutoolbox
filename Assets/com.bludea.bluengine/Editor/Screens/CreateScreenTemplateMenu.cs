using System.IO;
using Bludk.Editor;
using UnityEditor;
using UnityEngine;

namespace BluEngine.Editor
{
    public static class CreateScreenTemplateMenu
    {
        [MenuItem("Assets/Create/BluEngine/Screen/CreateScreenTemplate")]
        public static void CreateScreenTemplate()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);

            float width = 300;
            float height = 100;
            CreateScreenTemplateWindow window = ScriptableObject.CreateInstance<CreateScreenTemplateWindow>();
            window.SetPath(path);
            float x = EditorGUIUtility.GetMainWindowPosition().center.x - width;
            float y = EditorGUIUtility.GetMainWindowPosition().center.y - height;
            window.position = new Rect(x, y, width, height);
            window.minSize = new Vector2(width, height);
            window.maxSize = new Vector2(width, height);
            window.ShowPopup();
        }
    }

    public class CreateScreenTemplateWindow : EditorWindow
    {
        private string _path;
        private string _screenName;

        public void SetPath(string path)
        {
            _path = path;
        }

        private void OnGUI()
        {
            var style = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter};
            EditorGUILayout.LabelField("ScreenName", style, GUILayout.ExpandWidth(true));   

            GUILayout.Space(10);

            _screenName = EditorGUILayout.TextField(_screenName);

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Close"))
                {
                    Close();
                }

                if (GUILayout.Button("Create"))
                {
                    string validateScreenName = ValidateScreenName();
                    if (!string.IsNullOrEmpty(validateScreenName))
                    {
                        Debug.LogError(validateScreenName);
                    }
                    else
                    {
                        CreateScreenTemplate();
                        Close();
                    }
                }
            }
            GUILayout.EndHorizontal();
        }

        private string ValidateScreenName()
        {
            if (string.IsNullOrEmpty(_screenName))
            {
                return "Screen name can't be empty";
            }

            if (_screenName.Contains(" "))
            {
                return "Screen name can't contains whitespace characters";
            }

            return "";
        }

        private string GetTemplatesAbsolutePath()
        {
            return Path.Combine(Application.dataPath, "com.bludea.bluengine", "Editor", "Screens");
        }

        private void CreateScreenTemplate()
        {
            string currentAbsolutePath = PathUtils.FromAssetPathToAbsolutePath(_path);
            string screenDirectoryAbsolutePath = Path.Combine(currentAbsolutePath, _screenName);
            if (Directory.Exists(screenDirectoryAbsolutePath))
            {
                Debug.LogError($"Directory {screenDirectoryAbsolutePath} already exists");
                return;
            }

            Directory.CreateDirectory(screenDirectoryAbsolutePath);

            string screenName = $"{_screenName}Screen";
            string screenControllerName = $"{screenName}Controller";

            string screenAbsolutePath = Path.Combine(screenDirectoryAbsolutePath, $"{screenName}.cs");
            string screenControllerAbsolutePath = Path.Combine(screenDirectoryAbsolutePath, $"{screenControllerName}.cs");

            File.WriteAllText(screenAbsolutePath, GetScreenFileText(screenName, screenControllerName));
            File.WriteAllText(screenControllerAbsolutePath, GetControllerFileText(screenName, screenControllerName));

            AssetDatabase.Refresh();
        }

        private string GetScreenFileText(string screenName, string screenControllerName)
        {
            string screenAbsolutePath = Path.Combine(GetTemplatesAbsolutePath(), "ScreenTemplate.txt");
            return File.ReadAllText(screenAbsolutePath)
                .Replace("{screenName}", screenName)
                .Replace("{screenControllerName}", screenControllerName);
        }

        private string GetControllerFileText(string screenName, string screenControllerName)
        {
            string controllerAbsolutePath = Path.Combine(GetTemplatesAbsolutePath(), "ScreenControllerTemplate.txt");
            return File.ReadAllText(controllerAbsolutePath)
                .Replace("{screenName}", screenName)
                .Replace("{screenControllerName}", screenControllerName);
        }
    }
}