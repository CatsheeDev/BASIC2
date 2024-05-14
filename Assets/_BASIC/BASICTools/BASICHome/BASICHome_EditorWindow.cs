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
