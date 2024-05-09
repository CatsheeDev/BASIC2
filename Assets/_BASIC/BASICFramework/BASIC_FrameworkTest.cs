#if UNITY_EDITOR
using BASIC.UI.States;
using UnityEditor;
using UnityEngine;
using BASIC.UI;

public class BASIC_FrameworkTest : BASICEditorSingleton<BASIC_FrameworkTest>
{
    public BASIC_UIStates stateManager;

    [MenuItem("BASIC/FrameworkTest")]
    public static void ShowWindow()
    {
        BASIC_UIBASE.createWindow<BASIC_FrameworkTest>("framework test hi");
    }

    private void OnEnable()
    {
        stateManager = new BASIC_UIStates(new TestState2());
    }

    private void OnGUI()
    {
        stateManager.RenderCurrentState();
    }


    public class TestState : IUIState
    {
        public void RenderUI()
        {
            EditorGUILayout.LabelField("hi person in my phone!!!1", EditorStyles.boldLabel);
            BASIC_UIBASE.beginVertical(true); 
            if (GUILayout.Button("go to test 2"))
            {
                Instance.stateManager.SetState(new TestState2());
            }
            BASIC_UIBASE.endVertical(); 
        }
    }

    public class TestState2 : IUIState
    {
        public void RenderUI()
        {
            EditorGUILayout.LabelField("johnny this is an intervention, youve got to stop twsting peoples heads! johnny!");
            if (GUILayout.Button("go to test 1"))
            {
                Instance.stateManager.SetState(new TestState());
            }
        }
    }
}
#endif 