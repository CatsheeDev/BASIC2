#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameControllerScript))]
public class GameControllerScriptEditor : Editor
{
    public static GameControllerScript currGC; 

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open Decompile Editor"))
        {
            currGC = (GameControllerScript)target;

            DecompEditorWindow.ShowWindow((GameControllerScript)target);
        }
        DrawDefaultInspector();
    }
}
#endif