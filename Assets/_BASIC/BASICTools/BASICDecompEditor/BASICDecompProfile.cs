using UnityEngine;

[CreateAssetMenu(fileName = "DecompData_", menuName = "BASIC/Decomp Editor/Decomp Profile", order = 1)]
public class BASICDecompProfile : ScriptableObject
{
    public bool DebugMode = false;

    [Header("General Options")]
    public bool YCTP = true;

    [Header("Notebooks")]
    public int maxNotebooks = 7;

    public int startingNotebooks;

    public int startingFailedNotebooks;

    [Header("Stamina")]
    public float maxStamina = 100;
}