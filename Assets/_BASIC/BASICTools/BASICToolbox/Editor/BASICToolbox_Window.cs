#if UNITY_EDITOR
using BASIC.UI;
using BASIC.UI.States;
using UnityEditor; 

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
            stateManager = new BASIC_UIStates(new BASICTOOLBOX_WINDOWSTATES_MAIN());
        }

        private void OnGUI()
        {
            stateManager.RenderCurrentState();
        }
    }
}
#endif