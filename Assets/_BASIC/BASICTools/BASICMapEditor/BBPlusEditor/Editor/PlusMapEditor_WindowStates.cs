using BASIC.UI.States;
using UnityEditor;
using UnityEngine;

public class PlusMapEditor_RoomEditor : IUIState
{
    ObjectProfile2 profile = Resources.Load<ObjectProfile2>("ObjectData_Autoconnect");
    public void RenderUI()
    {
        MapEditorLogic.Instance.currentlySelectedTile = profile.profiledObjects[0].prefab;
        MapEditorLogic.Instance.newTile(profile);
        MapEditorLogic.Instance.autoBuild = true;
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Map", EditorStyles.boldLabel);

        if (GUILayout.Button("Create Room"))
        {
            PlusMapLogic.Instance.CreateMap(new Vector3(10, 1, 10));
        }

        if (GUILayout.Button("Export Room To Plus"))
        {
            MapToRoomAsset.Convert(GameObject.Find("RoomTest"), "MapName");
        }

        if (GUILayout.Button("Settings"))
        {
            MapEditorWindow.Instance.stateManager.SetState(new MapEditor_state_EditorSettings());
        }
        MapEditorLogic.Instance.currentTileProfileInIndex = 0;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear"))
        {
            MapEditorLogic.Instance.clearTile();
        }
        EditorGUILayout.EndHorizontal();
    }
}