using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

namespace LasagnaSound
{
    class ClipBundleGenerator : EditorWindow
    {
        string nameRegex;
        string bundleName;
        private const string fileFormats = "(.(ogg)|(wav)|(mp3)|(aif))$";
           //TODO folders
        string clipDir;
        string targetDir;

        [MenuItem("Tools/LasagnaSound/Generate ClipBundle")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ClipBundleGenerator));
        }

        private ClipBundle GenerateBundle()
        {
            ClipBundle bundle = ScriptableObject.CreateInstance<ClipBundle>();
            Regex rx = new Regex("("+nameRegex+")"+fileFormats);

            string lookPath = Path.Combine(Application.dataPath, clipDir);
            List<AudioClip> matches = new List<AudioClip>();
            foreach (string file in Directory.GetFiles(lookPath))
            {
                if (rx.IsMatch(file))
                {
                    string[] splitted = file.Split('/');
                    string load = "Assets/" + clipDir + splitted[splitted.Length-1];
                    AudioClip a = AssetDatabase.LoadAssetAtPath<AudioClip>(load);
                    matches.Add(a);
                }
            }
            bundle.clips = new AudioClip[matches.Count];
            matches.CopyTo(bundle.clips);
            return bundle;
        }

        private void SaveBundle(ClipBundle bundle)
        {
            string targetFile = Path.Combine("Assets/"+targetDir, bundleName);
            targetFile = targetFile.Replace('\\', '/');
            AssetDatabase.CreateAsset(bundle,(targetFile)+".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        private void OnGUI()
        {
            GUILayout.Label("Generate new Clip Bundle");
            nameRegex = EditorGUILayout.TextField("Regex for clip name", nameRegex);
            bundleName = EditorGUILayout.TextField("Bundle Name", bundleName);
            clipDir = EditorGUILayout.TextField("Clip Directory", clipDir);
            targetDir = EditorGUILayout.TextField("ClipBundle Directory", targetDir);

            if(GUILayout.Button("Generate Bundle"))
            {
                
                SaveBundle(GenerateBundle());
            }

        }
    }
}
