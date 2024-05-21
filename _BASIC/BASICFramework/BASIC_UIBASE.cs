#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BASIC.UI
{
    public class BASIC_UIBASE
    {
        
        public static EditorWindow createWindow<T>(string windowName) where T : EditorWindow
        {
            EditorWindow window = EditorWindow.GetWindow(typeof(T), false, windowName);
            window.minSize = new Vector2(300, 400);

            T typedWindow = window as T;

            return typedWindow;
        }
        
        public static EditorWindow createWindowWithSpecialNeeds<T>(string windowName) where T : EditorWindow
        {
            EditorWindow window = EditorWindow.GetWindow(typeof(T), false, windowName);
            window.minSize = new Vector2(500, 500);

            T typedWindow = window as T;

            return typedWindow;
        }
        
        public static void VerticalBox(params System.Action[] actions)
        {
            EditorGUILayout.BeginVertical();

            foreach (var action in actions)
            {
                action?.Invoke();
            }

            EditorGUILayout.EndVertical();
        }

        public static void beginVertical(bool box)
        {
            EditorGUILayout.Space();
            if (box)
            {
                EditorGUILayout.BeginVertical("box");
                return; 
            }

            EditorGUILayout.BeginVertical(); 
        }

        public static void endVertical()
        {
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        
        public static void header(string text, int spacing)
        {
            GUIStyle headerStyle = new GUIStyle(GUI.skin.label);
            headerStyle.fontSize = 20;
            headerStyle.fontStyle = FontStyle.Bold; 
            headerStyle.normal.textColor = Color.white; 
            headerStyle.alignment = TextAnchor.MiddleCenter; 

            EditorGUILayout.LabelField(text, headerStyle);

            EditorGUILayout.Space(spacing); 
        }
    }

}
#endif