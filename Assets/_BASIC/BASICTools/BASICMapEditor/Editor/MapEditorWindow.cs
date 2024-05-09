#if UNITY_EDITOR
using BASIC.UI.States;
using UnityEditor;
using UnityEngine;
using BASIC.UI;

public class MapEditorWindow : BASICEditorSingleton<MapEditorWindow>
{
    public BASIC_UIStates stateManager;

    [MenuItem("BASIC/Map Editor")]
    public static void ShowWindow()
    {
        BASIC_UIBASE.createWindow<MapEditorWindow>("Map Editor");
    }

    private void OnEnable()
    {
        stateManager = new BASIC_UIStates(new MapEditor_State_Start());
    }

    private void OnGUI()
    {
        stateManager.RenderCurrentState();
        if (GUILayout.Button("Return to Menu"))
        {
            stateManager.SetState(new MapEditor_State_Start()); 
        }
    }
}
#endif