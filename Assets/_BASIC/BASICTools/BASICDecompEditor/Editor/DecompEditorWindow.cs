#if UNITY_EDITOR
using BASIC.UI.States;
using UnityEditor;
using UnityEngine;
using BASIC.UI;

public class DecompEditorWindow : BASICEditorSingleton<DecompEditorWindow>
{
    public BASIC_UIStates stateManager;
    public GameControllerScript gc; 

    [MenuItem("BASIC/Tools/Decomp Editor")]
    public static void ShowWindow(GameControllerScript gC)
    {
        BASIC_UIBASE.createWindow<DecompEditorWindow>("Decomp Editor");
        Instance.gc = gC;
    }

    private void OnEnable()
    {
        DecompEditorStates_Main newWindow = new();
        newWindow.currentGC = Instance.gc; 
        stateManager = new BASIC_UIStates(newWindow);
    }

    private void OnGUI()
    {
        stateManager.RenderCurrentState();
    }
}
#endif