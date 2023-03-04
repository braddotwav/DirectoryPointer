using UnityEngine;
using UnityEditor;
using System.Linq;

namespace NightshiftGames.DirectoryPointer
{
    internal class ContextMenu : EditorWindow
    {
        static ContextMenu window;
        static Settings settings;
        static Editor editor;
        Texture headerTexture;

        GUIStyle headerStyle = new();
        GUIStyle buttonStyle;

        [MenuItem("Window/DirectoryPointer/Settings")]
        static void PopWindow()
        {
            if (window != null) { return; }
            window = GetWindow<ContextMenu>(typeof(ContextMenu));
            window.titleContent = new("DirectoryPointer");
            window.minSize = new(250,450);
            window.maxSize = new(450,600);
            window.Show();
        }

        private void OnEnable()
        {
            settings = Resources.Load<Settings>("Nightshift Games/DirectoryPointer/Settings");
            headerTexture = Resources.Load<Texture>("Nightshift Games/DirectoryPointer/directorypointerheader");
            editor = Editor.CreateEditor(settings);
        }

        private void OnGUI()
        {
            headerStyle.margin = new(10, 10, 10, 10);
            buttonStyle = new("button")
            {
                alignment = TextAnchor.MiddleCenter,
                padding = new(2,2,8,8),
                fontSize = 14,
            };
            GUILayout.Box(headerTexture, headerStyle);
            editor.OnInspectorGUI();
            GUILayout.Space(10);
            if (GUILayout.Button("Generate C# Class", buttonStyle))
            {
                if (!settings.Directorys.Any()) { Debug.LogWarning("Can not generate empty file"); return; }
                ClassGenerator.CheckAndCreateClass(settings);
            }
            GUILayout.Space(10);
            if (GUILayout.Button("Clear List", buttonStyle))
            {
                if (!settings.Directorys.Any()) { return; }
                settings.Directorys.Clear();
            }
            GUILayout.Space(2);
            if (GUILayout.Button("Delete File", buttonStyle))
            {
                ClassGenerator.DeleteClassFile();
            }
        }

        private void OnDisable()
        {
            window = null;
        }
    }
}