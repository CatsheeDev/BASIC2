using BASIC.UI.States;
using BASIC.UI;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MapEditor_State_Start : IUIState
{
    public void RenderUI()
    {
        BASIC_UIBASE.beginVertical(true);
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 14;
        buttonStyle.fixedHeight = 50;

        if (GUILayout.Button("Create Map", buttonStyle))
        {
            BASIC_UIBASE.endVertical();
            MapEditorWindow.Instance.stateManager.SetState(new MapEditor_State_CreateMap());
            return; 
        }

        if (GUILayout.Button("Edit Map", buttonStyle))
        {
            MapEditorWindow.Instance.stateManager.SetState(new MapEditor_State_EditMap());
        }

        EditorGUILayout.Space(30);
/*        if (GUILayout.Button("Manage Profiles", buttonStyle))
        {
        }
        if (GUILayout.Button("Manage Layers", buttonStyle))
        {
        }*/
        BASIC_UIBASE.endVertical();
    }
}

public class MapEditor_State_CreateMap : IUIState
{
    public void RenderUI()
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Map Name", EditorStyles.boldLabel);
        MapEditorLogic.Instance.mapName = EditorGUILayout.TextField("Name", MapEditorLogic.Instance.mapName);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Map Size", EditorStyles.boldLabel);
        MapEditorLogic.Instance.mapSize = EditorGUILayout.Vector3Field("Size", MapEditorLogic.Instance.mapSize);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            bool success = MapEditorLogic.Instance.createMap();

            if (!success)
            {
                Debug.LogError("FUCK! There was an error when creating the map!");
                return; 
            }

            MapEditorWindow.Instance.stateManager.SetState(new MapEditor_State_EditMap());
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
    }
}

public class MapEditor_state_EditorSettings : IUIState
{
    public void RenderUI()
    {
        EditorGUILayout.LabelField("Map Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Optimise Map", EditorStyles.boldLabel);
        if (GUILayout.Button("Optimise Map (IRREVERSIBLE)"))
        {
            MapEditorLogic.Instance.optimiseMap();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Generate NavMesh", EditorStyles.boldLabel);
        if (GUILayout.Button("Generate NavMesh"))
        {
            UnityEditor.AI.NavMeshBuilder.ClearAllNavMeshes();
            UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
            AssetDatabase.SaveAssets();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Bake Occlusion Culling", EditorStyles.boldLabel);
        if (GUILayout.Button("Bake Occlusion"))
        {
            StaticOcclusionCulling.Clear();
            StaticOcclusionCulling.Compute();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Development Items", EditorStyles.boldLabel);
        if (GUILayout.Button("Toggle AIPath"))
        {
            Tags[] foundAIPaths = GameObject.FindObjectsOfType<Tags>(true);

            foreach (Tags aiath in foundAIPaths)
            {
                if (aiath.HasTag(MapEditorLogic.Instance.aiTag))
                {
                    aiath.gameObject.SetActive(!aiath.gameObject.activeSelf);
                }
            }
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("EXPERIMENTAL OPTIONS", EditorStyles.boldLabel);

        MapEditorLogic.Instance.autoBuild = EditorGUILayout.ToggleLeft("Auto Build", MapEditorLogic.Instance.autoBuild); 
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
    }
}

public class MapEditor_State_EditMap : IUIState
{
    public void RenderUI()
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Object Profile", EditorStyles.boldLabel);
        ObjectProfile2 newProfile = (ObjectProfile2)EditorGUILayout.ObjectField(MapEditorLogic.Instance.currentObjectProfile, typeof(ObjectProfile2), false);
        
        if (newProfile == null)
        {
            EditorGUILayout.EndVertical();
            return; 
        }

        if (newProfile.name == "SPECIALPROFILES_AiPath")
        {
            MapEditorLogic.Instance.aiProfile = true;
            EditorGUILayout.HelpBox("This is a special Object Profile for creating AI paths, layers are seperate.", MessageType.Info);
        } else
        {
            MapEditorLogic.Instance.aiProfile = false;
        }

        EditorGUILayout.EndVertical();

        MapEditorLogic.Instance.newTile(newProfile); 

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Map", EditorStyles.boldLabel);

        MapEditorLogic.Instance.getSelectedTiles(); 

        if (GUILayout.Button("Settings"))
        {
            MapEditorWindow.Instance.stateManager.SetState(new MapEditor_state_EditorSettings()); 
        }

        MapEditorLogic.Instance.currentTileProfileInIndex = EditorGUILayout.Popup("Select Tile", MapEditorLogic.Instance.currentTileProfileInIndex, MapEditorLogic.Instance.currentObjectProfile.profiledObjects.Select(o => o.name).ToArray());
        if (MapEditorLogic.Instance.currentTileProfileInIndex >= 0)
        {
            MapEditorLogic.Instance.currentlySelectedTile = MapEditorLogic.Instance.currentObjectProfile.profiledObjects[MapEditorLogic.Instance.currentTileProfileInIndex].prefab as GameObject;
        }

        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Layer", EditorStyles.boldLabel);
            MapEditorLogic.Instance.currentLayer = EditorGUILayout.IntField("Current Layer", MapEditorLogic.Instance.currentLayer);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

        if (MapEditorLogic.Instance.selectedTiles.Length == 0)
        {
            EditorGUILayout.LabelField("Select one or more tiles to start editing");
            return;
        }

        if (MapEditorLogic.Instance.currentObjectProfile == null)
        {
            GUILayout.Label("No profile selected!");
            return;
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Set Tile"))
        {
            MapEditorLogic.Instance.placeTile(); 
        }

        if (GUILayout.Button("Clear"))
        {
            MapEditorLogic.Instance.clearTile();
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Rotation", EditorStyles.boldLabel);

        MapEditorLogic.Instance.exactRotation = EditorGUILayout.Vector3Field("Exact Rotation", MapEditorLogic.Instance.exactRotation);

        if (GUILayout.Button("Rotate"))
        {
            MapEditorLogic.Instance.rotateTile(MapEditorLogic.Instance.exactRotation, MapEditorLogic.Instance.addExactRotation);
        }
        MapEditorLogic.Instance.addExactRotation = EditorGUILayout.ToggleLeft("Add?", MapEditorLogic.Instance.addExactRotation);
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Rotate +90"))
        {
            MapEditorLogic.Instance.rotateTile(new Vector3(0, 90, 0), true);
        }

        if (GUILayout.Button("Rotate -90"))
        {
            MapEditorLogic.Instance.rotateTile(new Vector3(0, -90, 0), true); 
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }
}