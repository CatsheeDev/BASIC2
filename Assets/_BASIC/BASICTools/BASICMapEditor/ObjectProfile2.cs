using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData_", menuName = "BASIC/Map Editor/Object Profile 2.0 (the sequel)™️", order = 1)]
public class ObjectProfile2 : ScriptableObject
{
    public ObjectProfie2Object[] profiledObjects;
    public string profileName;
}

[System.Serializable]
public struct ObjectProfie2Object
{
    public string name;
    public Sprite icon; 

    [SerializeReference]
    public UnityEngine.Object prefab;
}