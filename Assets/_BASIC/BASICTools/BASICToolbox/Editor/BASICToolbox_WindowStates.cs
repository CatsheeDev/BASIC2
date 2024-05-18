#if UNITY_EDITOR
using BASIC.UI.States;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public enum toolboxTypeParams
{
    All,
    Scene, 
    Item
}

public class BASICTOOLBOX_WINDOWSTATES_MAIN : IUIState
{
    private dynamic[] jsones = new dynamic[0];

    private toolboxTypeParams currSearchParams;

    void IUIState.RenderUI()
    {
        if (GUILayout.Button("Refresh Toolbox"))
        {
            BASICToolbox_PackageDatabase.Instance.downloadAllPackages();
        }

        string[] searchTypeStrings = Enum.GetNames(typeof(toolboxTypeParams));
        int selectedIndex = (int)currSearchParams;

        currSearchParams = (toolboxTypeParams)EditorGUILayout.Popup("Search Type", selectedIndex, searchTypeStrings);

        foreach (BASICPackage package in BASICToolbox_PackageDatabase.Instance.packageInfos.Select(v => (BASICPackage)v))
        {
            if (package.packageType == currSearchParams.ToString() || currSearchParams == toolboxTypeParams.All)
            {
                EditorGUILayout.BeginVertical("box");
                GUILayout.Label(
                    $"Name: {package.packageName} \nBASIC Version: {package.verCreated} \nType: {package.packageType}"
                );
                if (GUILayout.Button("Import"))
                {
                    //AssetDatabase.ImportPackage(packagePath, true);
                }
                EditorGUILayout.EndVertical();
            }
            else
            {
                continue;
            }
        }
    }

}
#endif