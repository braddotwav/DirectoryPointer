using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NightshiftGames.DirectoryPointer
{
    internal static class ClassGenerator
    {
        private static readonly string FileName = "GeneratedDirectoryClass.cs";
        private static readonly string FilePath = $"Assets/Thirdparty/Nightshift Games/DirectoryPointer/Resources/Nightshift Games/Generated/{FileName}";

        public static void CheckAndCreateClass(Settings data)
        {
            #region Template
            var template = File.ReadAllLines($"{Application.dataPath}/Thirdparty/Nightshift Games/DirectoryPointer/Resources/Nightshift Games/DirectoryPointer/template.temp").ToList();
            if (template == null)
            {
                Debug.LogError("Could not find template file. Please re-import the DirectoryPoint package to reinstate the nessasery files.");
                return;
            }
            #endregion

            List<string> array = new();
            for (int i = 0; i < data.Directorys.Count; i++)
            {
                array.Add(CreateAttributeAndMethod(data.Directorys[i].Name, data.Directorys[i].Path));
            }
            
            template.InsertRange(8, array);

            File.WriteAllLines(FilePath, template);

            AssetDatabase.Refresh();
        }

        public static void DeleteClassFile()
        {
            if (!File.Exists(FilePath)) { Debug.LogWarning("Could not find a generated class file to delete"); return; }
            AssetDatabase.DeleteAsset($"Assets/Thirdparty/Nightshift Games/DirectoryPointer/Resources/Nightshift Games/Generated/{FileName}");
            AssetDatabase.Refresh();
        }

        private static string CreateAttributeAndMethod(string name, string path)
        {
            var attribute = $"\t[MenuItem(\"Directorys/{name}\")]";
            var method = $"\tprivate static void Open{name.Replace(" ", string.Empty)}() => System.Diagnostics.Process.Start(\"explorer.exe\", @\"{path}\");";
            return $"{attribute}\r\n{method}";
        }
    }
}