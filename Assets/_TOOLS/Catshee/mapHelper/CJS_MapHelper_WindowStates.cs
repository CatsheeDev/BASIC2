#if UNITY_EDITOR
using CJS.UI.States;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CJS_MapHelper_HallBuilder : CJS.UI.States.UIState
{
    private Material roofMaterial;
    private Material wallMaterial;
    private Material floorMaterial;

    private Vector3 size;
    private Vector3 offset;

    private Transform hallParent; 

    private bool isDetailsOpen;

    private bool builDRoof;
    private bool builDWalls;
    private bool builDFloor; 

    void UIState.RenderNewUI()
    {
        EditorGUILayout.BeginVertical("box");
        
        isDetailsOpen = EditorGUILayout.Foldout(isDetailsOpen, "Hall Details");
        if (isDetailsOpen)
        {
            EditorGUILayout.LabelField("Hall Materials", EditorStyles.boldLabel);
            roofMaterial = (Material)EditorGUILayout.ObjectField("Roof Material", roofMaterial, typeof(Material), true);
            wallMaterial = (Material)EditorGUILayout.ObjectField("Wall Material", wallMaterial, typeof(Material), true);
            floorMaterial = (Material)EditorGUILayout.ObjectField("Floor Material", floorMaterial, typeof(Material), true);

            EditorGUILayout.Space(); 
            
            EditorGUILayout.LabelField("Hall Options", EditorStyles.boldLabel);
            hallParent = (Transform)EditorGUILayout.ObjectField("Hall Parent", hallParent, typeof(Transform), true);

            EditorGUILayout.Space();


            builDRoof = EditorGUILayout.Toggle("Build Roof?", builDRoof);
            builDWalls = EditorGUILayout.Toggle("Build Wall?", builDWalls);
            builDFloor = EditorGUILayout.Toggle("Build Floor?", builDFloor);
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.LabelField("Hall Info", EditorStyles.boldLabel);
        size = EditorGUILayout.Vector3Field("Hallway Size", size);
        offset = EditorGUILayout.Vector3Field("Hallway Position (Local)", offset);
        EditorGUILayout.EndVertical();
    }
}
#endif