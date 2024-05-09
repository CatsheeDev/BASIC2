#if UNITY_EDITOR
using BASIC.UI.States;
using UnityEditor;
using UnityEngine;
using BASIC.UI;

public class ItemEditorWindow : BASICEditorSingleton<ItemEditorWindow>
{
    public BASIC_UIStates stateManager;

    [MenuItem("BASIC/Item Editor")]
    public static void ShowWindow()
    {
        BASIC_UIBASE.createWindow<ItemEditorWindow>("Item Editor");
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