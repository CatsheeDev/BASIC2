using BASIC.UI.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BASICHome_ActuallyWindow : BASICEditorSingleton<BASICHome_ActuallyWindow>
{
    private BASIC_UIStates stateManager; 

    private void OnEnable()
    {
        stateManager = new BASIC_UIStates(new BASICHome_EditorWindow_Main());
    }

    private void OnGUI()
    {
        stateManager.RenderCurrentState();
    }
}
