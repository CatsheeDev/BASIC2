#if UNITY_EDITOR
using BASIC.UI.States;
using UnityEditor;

public class BASICTOOLBOX_WINDOWSTATES_MAIN : IUIState
{
    void IUIState.RenderUI()
    {
        EditorGUILayout.HelpBox("hey", MessageType.None); 
    }
}
#endif