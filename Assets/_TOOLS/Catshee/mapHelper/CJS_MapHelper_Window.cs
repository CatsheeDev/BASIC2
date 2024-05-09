using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CJS.UI.States;
using CJS.UI;

public class CJS_MapHelper_Window : CJSEditorSingleton<CJS_MapHelper_Window>
{
    private CJSFramework_UIStates stateManager; 

    [MenuItem("CatsheeJS/Map Editor")]
    public static void ShowWindow()
    {
        CJSFramework_UIBASE.createWindow<CJS_MapHelper_Window>("Hall Creator");
    }

    private void OnEnable()
    {
        stateManager = new CJSFramework_UIStates(new CJS_MapHelper_HallBuilder());
    }

    private void OnGUI()
    {
        stateManager.RenderCurrentState();
    }
}
