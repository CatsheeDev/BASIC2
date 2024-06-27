#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BASIC.UI;
using BASIC.UI.States;

public class BASICAchievementsWindow : BASICEditorSingleton<BASICAchievementsWindow>
{
    public BASIC_UIStates stateManager; 

    [MenuItem("BASIC/Tools/Achievement Editor")]
    private static void CreateWindow()
    {
        BASIC_UIBASE.createWindow<BASICAchievementsWindow>("Achivement Editor"); 
    }

    private void OnEnable()
    {
        stateManager = new BASIC_UIStates(new ItemEditor_State_Main());
    }

    private void OnGUI()
    {
        stateManager.RenderCurrentState();
    }
}
#endif