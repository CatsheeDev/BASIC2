#if UNITY_EDITOR
using BASIC.UI;
using BASIC.UI.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusMapEditor_Window : BASICEditorSingleton<PlusMapEditor_Window>
{
    public BASIC_UIStates stateManager;

    public static void ShowWindow()
    {
        BASIC_UIBASE.createWindow<PlusMapEditor_Window>("Plus Map Editor (supa secret owo)");
    }

    private void OnEnable()
    {
        stateManager = new BASIC_UIStates(new PlusMapEditor_RoomEditor());
    }

    private void OnDestroy()
    {
        PlusMapLogic.Instance.KillLogic(); 
    }

    private void OnGUI()
    {
        stateManager.RenderCurrentState();
        if (GUILayout.Button("Return to Menu"))
        {
            MapEditorLogic.Instance.autoBuild = false;
            stateManager.SetState(new PlusMapEditor_RoomEditor());
        }
    }
}
#endif