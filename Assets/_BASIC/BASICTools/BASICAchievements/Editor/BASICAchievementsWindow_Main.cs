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

    public void RenderUI()
    {

        #region CONTROLS

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("CONTROLS", EditorStyles.boldLabel);

        if (ItemEditorLogic.Instance._controllerScript == null)
        {
            EditorGUILayout.LabelField("GameController not found", EditorStyles.boldLabel);
            EditorGUILayout.EndVertical();
            return;
        }

        if (ItemEditorLogic.Instance._itemProfile == null)
        {
            EditorGUILayout.LabelField("Item Profile not found", EditorStyles.boldLabel);
            EditorGUILayout.EndVertical();
            return;
        }

        if (itemsBackup == null)
        {
            itemsBackup = new Achievement[Profile.Achievements.Length];
            Array.Copy(Profile.Achievements, itemsBackup, ItemEditorLogic.Instance._itemProfile.items.Length);
        }

        ItemEditorLogic.Instance._controllerScript.itemProfile = ItemEditorLogic.Instance._itemProfile;

        if (GUILayout.Button(new GUIContent("Save", "Save your Items")))
        {
            ItemEditor_EnumBuilder.Instance.beginBuild(ItemEditorLogic.Instance._itemProfile);
        }

        if (GUILayout.Button(new GUIContent("Add Item", "Add a new item to the list")))
        {
            Array.Resize(ref ItemEditorLogic.Instance._itemProfile.items, ItemEditorLogic.Instance._itemProfile.items.Length + 1);
            ItemEditorLogic.Instance._itemProfile.items[^1] = new ItemInfo();
            ItemEditorLogic.Instance._itemProfile.items[^1].Value = ItemEditorLogic.Instance._itemProfile.items.Length - 1;
        }

        EditorGUILayout.EndVertical();
        #endregion CONTROLS

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("ITEMS", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        if (ItemEditorLogic.Instance._itemProfile.items.Length == 0)
        {
            EditorGUILayout.EndScrollView();
            return;
        }

        for (int i = 0; i < ItemEditorLogic.Instance._itemProfile.items.Length; i++)
        {
            if (!itemFoldouts.ContainsKey(i))
            {
                itemFoldouts[i] = false;
            }
            EditorGUILayout.BeginVertical("box");
            itemFoldouts[i] = EditorGUILayout.Foldout(itemFoldouts[i], $"{ItemEditorLogic.Instance._itemProfile.items[i].Name} || {ItemEditorLogic.Instance._itemProfile.items[i].Value}");
            if (itemFoldouts[i])
            {

                if (i == 0)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.HelpBox("WARNING!! THIS IS THE EMPTY SLOT. ONLY EDIT IF YOU KNOW WHAT YOU'RE DOING", MessageType.Warning);
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.BeginHorizontal();
                ItemEditorLogic.Instance._itemProfile.items[i].Name = EditorGUILayout.TextField("Name", ItemEditorLogic.Instance._itemProfile.items[i].Name);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                ItemEditorLogic.Instance._itemProfile.items[i].InGameName = EditorGUILayout.TextField("Game Name", ItemEditorLogic.Instance._itemProfile.items[i].InGameName);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                ItemEditorLogic.Instance._itemProfile.items[i].Icon = (Texture2D)EditorGUILayout.ObjectField("Icon", ItemEditorLogic.Instance._itemProfile.items[i].Icon, typeof(Texture2D), false);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                if (i != 0 && GUILayout.Button("Remove"))
                {
                    var list = ItemEditorLogic.Instance._itemProfile.items.ToList();
                    list.RemoveAt(i);
                    ItemEditorLogic.Instance._itemProfile.items = list.ToArray();
                }

                if (i != 0 && GUILayout.Button("Create Script"))
                {
                    ItemEditorLogic.Instance.CreateScript(ItemEditorLogic.Instance._itemProfile.items[i].Name, null);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();

        bool hasChanges = !itemsBackup.SequenceEqual(Profile.Achievements);
        if (hasChanges)
        {
            EditorUtility.SetDirty(ItemEditorLogic.Instance._itemProfile);
            AssetDatabase.SaveAssets();
        }

        itemsBackup = null;
    }
}