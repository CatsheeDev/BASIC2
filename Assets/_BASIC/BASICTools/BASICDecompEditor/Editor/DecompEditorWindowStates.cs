using BASIC.UI.States;
using UnityEditor;
using UnityEngine; 

public class DecompEditorStates_Main : IUIState
{
    private BASICDecompProfile currentSettingsData;
    private Vector2 scrollPos;
    public GameControllerScript currentGC; 

    public void RenderUI()
    {
        currentGC = GameControllerScriptEditor.currGC; 

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Decomp Profile", EditorStyles.boldLabel);
        currentSettingsData = (BASICDecompProfile)EditorGUILayout.ObjectField(currentSettingsData, typeof(BASICDecompProfile), false);

        if (currentSettingsData == null)
        {
            EditorGUILayout.EndVertical();
            return; 
        }

        currentGC.settingsProfile = currentSettingsData;

        EditorGUILayout.EndVertical();
        SerializedObject serializedSettingsData = new SerializedObject(currentSettingsData);
        serializedSettingsData.Update();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        EditorGUILayout.BeginVertical("box");
        SerializedProperty property = serializedSettingsData.GetIterator();
        bool enterChildren = true;
        if (property.NextVisible(enterChildren))
        {
            enterChildren = false;
            while (property.NextVisible(enterChildren))
            {
                enterChildren = false;
                EditorGUILayout.PropertyField(property, true);
            }
        }
        serializedSettingsData.ApplyModifiedProperties();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }
}