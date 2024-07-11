using BASIC.UI.States;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

public class BASICAchievementsWindow_Main : IUIState
{
    private Dictionary<int, bool> itemFoldouts = new Dictionary<int, bool>();
    private Vector2 scrollPos;
    private Achievement[] itemsBackup;

    private AchievementProfile Profile;
    private GameControllerScript _controllerScript; 

    public void RenderUI()
    {

        #region CONTROLS

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("CONTROLS", EditorStyles.boldLabel);

        _controllerScript = (GameControllerScript)EditorGUILayout.ObjectField(_controllerScript, typeof(GameControllerScript), true);
        if (_controllerScript == null)
        {
            EditorGUILayout.LabelField("GameController not found", EditorStyles.boldLabel);
            EditorGUILayout.EndVertical();
            return;
        }

        Profile = (AchievementProfile)EditorGUILayout.ObjectField(Profile, typeof(AchievementProfile), false);
        if (Profile == null)
        {
            EditorGUILayout.LabelField("Achievement Profile not found", EditorStyles.boldLabel);
            EditorGUILayout.EndVertical();
            return;
        }

        if (itemsBackup == null)
        {
            itemsBackup = new Achievement[Profile.Achievements.Count];
            Array.Copy(Profile.Achievements.ToArray(), itemsBackup, Profile.Achievements.Count);
        }

        _controllerScript.achievementProfile = Profile; 

        if (GUILayout.Button(new GUIContent("Save", "Save your Achievements")))
        {
            ItemEditor_EnumBuilder.Instance.beginBuild(ItemEditorLogic.Instance._itemProfile);
        }

        if (GUILayout.Button(new GUIContent("Add Achievement", "Add a new item to the list")))
        {
            Achievement NewAchievement = new(); 
            Profile.Achievements.Add(NewAchievement); //not sure why c didnt use a list for item editor but fuck me ig
        }

        EditorGUILayout.EndVertical();
        #endregion CONTROLS

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("ACHIEVEMENTS", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        if (Profile.Achievements.Count == 0)
        {
            EditorGUILayout.EndScrollView();
            return;
        }

        for (int i = 0; i < Profile.Achievements.Count; i++)
        {
            if (!itemFoldouts.ContainsKey(i))
            {
                itemFoldouts[i] = false;
            }
            EditorGUILayout.BeginVertical("box");
            itemFoldouts[i] = EditorGUILayout.Foldout(itemFoldouts[i], $"{Profile.Achievements[i].Name} || {i}");
            if (itemFoldouts[i])
            {
                Achievement tempAchievement = new Achievement();
                EditorGUILayout.BeginHorizontal();
                tempAchievement.Name = EditorGUILayout.TextField("Name", Profile.Achievements[i].Name);

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                tempAchievement.Description = EditorGUILayout.TextField("Description", Profile.Achievements[i].Description);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                tempAchievement.Icon = (Sprite)EditorGUILayout.ObjectField("Icon", Profile.Achievements[i].Icon, typeof(Sprite), false);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Remove"))
                {
                    Profile.Achievements.RemoveAt(i--);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndScrollView();
                    break; 
                }
                Profile.Achievements[i] = tempAchievement; 
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();

        bool hasChanges = !itemsBackup.SequenceEqual(Profile.Achievements);
        if (hasChanges)
        {
            EditorUtility.SetDirty(Profile);
            AssetDatabase.SaveAssets();
        }

        itemsBackup = null;
    }
}