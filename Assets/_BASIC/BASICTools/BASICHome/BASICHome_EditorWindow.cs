#if UNITY_EDITOR
using BASIC.UI.States;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BASICHome_EditorWindow_Main : IUIState
{
    void IUIState.RenderUI()
    {
        EditorGUILayout.HelpBox("hi", MessageType.None); 
    }
}


public class BASICHome_EditorWindow_Update : IUIState
{
    void IUIState.RenderUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 14;
        buttonStyle.fixedHeight = 50;

        GUILayout.Label("BASIC just got an update!", buttonStyle);

        EditorGUILayout.HelpBox("Upgrade BASIC to the latest version for the latest features, patches and improvements! \n \n \n or dont...", MessageType.Info); 
        EditorPrefs.SetBool("BASIC_showUpdate", EditorGUILayout.Toggle("Notify About Update?", EditorPrefs.GetBool("BASIC_showUpdate")));
    }
}

#endif