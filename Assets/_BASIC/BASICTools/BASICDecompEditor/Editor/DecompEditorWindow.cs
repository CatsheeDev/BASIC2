#if UNITY_EDITOR
using BASIC.UI.States;
using UnityEditor;
using UnityEngine;
using BASIC.UI;

public class DecompEditorWindow : BASICEditorSingleton<DecompEditorWindow>
{
    public BASIC_UIStates stateManager;

    [MenuItem("BASIC/Tools/Decomp Editor")]
    public static void ShowWindow()
    {
        BASIC_UIBASE.createWindow<DecompEditorWindow>("Decomp Editor");
    }

    private void OnEnable()
    {
        stateManager = new BASIC_UIStates(new DecompEditorStates_Main());
    }

    private void OnGUI()
    {
        stateManager.RenderCurrentState();
    }
}
#endif