using BASIC.UI;
using BASIC.UI.States;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BASICHome_ActuallyWindow : BASICEditorSingleton<BASICHome_ActuallyWindow>
{
    public BASIC_UIStates stateManager;
     
    //[MenuItem("BASIC/Home")]
    private static void OpenWindow()
    {
        BASIC_UIBASE.createWindow<BASICHome_ActuallyWindow>("BASIC Home"); 
    }

    private void OnEnable()
    {
        stateManager = new BASIC_UIStates(new BASICHome_EditorWindow_Main());
    }

    private void OnGUI()
    {
        stateManager.RenderCurrentState();
    }
}