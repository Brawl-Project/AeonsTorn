using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using System;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [InitializeOnLoad]
    public class Playmaker4STEEditorStartup
    {
        static Playmaker4STEEditorStartup()
        {
            /*
            Debug.Log("Running Playmaker4STEEditorStartup...");
            string[] aChangeLogs = AssetDatabase.FindAssets("changelog");
            for (int i = 0; i < aChangeLogs.Length; ++i)
            {
                string path = AssetDatabase.GUIDToAssetPath(aChangeLogs[i]);
                if(path.Contains("SuperTilemapEditor"))
                {
                    TextAsset txtAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                    string firstline = txtAsset.text.Substring(0, txtAsset.text.IndexOf(System.Environment.NewLine));
                    firstline = firstline.Replace("---", "");
                    Debug.Log(" Super Tilemap Editor " + firstline + " found!");
                    //return;
                }
            }
            */

            EditorApplication.update += DoWelcomeScreen;

        }

        private static void DoWelcomeScreen()
        {
            if (!Playmaker4STEWelcomeWindow.CheckPlaymakerInstalled() || !Playmaker4STEWelcomeWindow.CheckSuperTilemapEditorInstalled())
            {
                Playmaker4STEWelcomeWindow.Display();
            }
            EditorApplication.update -= DoWelcomeScreen;
        }
    }

    [InitializeOnLoad]
    public class Playmaker4STEWelcomeWindow : EditorWindow
    {
        public static void Display()
        {
            EditorWindow wnd = GetWindow<Playmaker4STEWelcomeWindow>(true, "Welcome to Playmaker For Super Tilemap Editor", true);
            wnd.maxSize = new Vector2(420, 100);//
            wnd.minSize = wnd.maxSize;
            wnd.position = new Rect(new Vector2(Screen.currentResolution.width / 2f, Screen.currentResolution.height / 2f) - wnd.maxSize / 2f, wnd.maxSize);
        }

        public static bool CheckPlaymakerInstalled()
        {
            var symbol = GetType("PlayMakerGlobals");
            return symbol != null;
        }

        public static bool CheckSuperTilemapEditorInstalled()
        {
            var symbol = GetType("STEditorStyles");
            return symbol != null;
        }

        void OnGUI()
        {
            if(CheckPlaymakerInstalled() && CheckSuperTilemapEditorInstalled())
            {
                GUILayout.Label("Everything is fine, you can now use Playmaker4STE. Enjoy!");
            }
            else
            {
                GUILayout.Label("In order to use this asset you need to install the following assets:");
            }
            EditorGUILayout.Space();
            if (!CheckSuperTilemapEditorInstalled() && GUILayout.Button("Super Tilemap Editor"))
            {
                AssetStore.Open("content/56339");
            }
            if (!CheckPlaymakerInstalled() && GUILayout.Button("Playmaker"))
            {
                AssetStore.Open("content/368");
            }
        }

        // NOTE: this code is from PlaymakerEditorStarup.cs
        // Normally we would use ReflectionUtils.GetGlobalType but this code needs to be standalone
        // Instead of copy/pasting ReflectionUtils, decided to try this code from UnityAnswers:
        // http://answers.unity3d.com/questions/206665/typegettypestring-does-not-work-in-unity.html
        public static Type GetType(string typeName)
        {
            // Try Type.GetType() first. This will work with types defined
            // by the Mono runtime, in the same assembly as the caller, etc.
            var type = Type.GetType(typeName);

            // If it worked, then we're done here
            if (type != null)
                return type;

            // otherwise look in loaded assemblies
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (type != null)
                {
                    break;
                }
            }

            return type;
        }
    }
}