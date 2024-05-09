using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData_", menuName = "BASIC/Item Editor/Item Profile 2.0 (the sequel)™️", order = 1)]
public class ItemProfile2 : ScriptableObject
{
    public ItemInfo[] items;
    public string profileName;
}