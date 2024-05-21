#if UNITY_EDITOR
using BASIC.UI;
using BASIC.UI.States;
using System.Collections.Generic;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace BASIC.Toolbox.UI
{
    public class BASICToolbox_Window : BASICEditorSingleton<BASICToolbox_Window>
    {
        public BASIC_UIStates stateManager;

        [MenuItem("BASIC/Tools/Toolbox")] 
        private static void showWindow()
        {
            BASIC_UIBASE.createWindow<BASICToolbox_Window>("Toolbox");
        }

        private void OnEnable()
        {
            BASICTOOLBOX_WINDOWSTATES_MAIN newWindow = new();

            stateManager = new BASIC_UIStates(newWindow);
            if (EditorPrefs.GetString("BASIC_Packages") != string.Empty)
            {
                fillCache(EditorPrefs.GetString("BASIC_Packages"), newWindow);
            }
        }

        void fillCache(string newJson, BASICTOOLBOX_WINDOWSTATES_MAIN window)
        {
            window.cachePackageInfos.Clear();
            List<BASICPackage> dPkg = JsonConvert.DeserializeObject<List<BASICPackage>>(newJson);
            foreach (var packageInfo in dPkg)
            {
                window.cachePackageInfos.Add(packageInfo);
            }

            string packagesPath = Path.Combine(Application.dataPath, "packages.json");
            window.packageDatabase = JsonConvert.DeserializeObject<BASICToolbox_PackageDatabase.RootObject>(File.ReadAllText(packagesPath));
        }
        private void OnGUI()
        {
            stateManager.RenderCurrentState();
        }
    }
}
#endif