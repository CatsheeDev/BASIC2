using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievements_", menuName = "BASIC/Achievement Editor/Achievement Profile", order = 1)]
public class AchievementProfile : ScriptableObject
{
    public Achievement[] Achievements; 
}

[System.Serializable]
public struct Achievement
{
    public string Name; 
    public string Description; 
    public bool Unlocked;
}