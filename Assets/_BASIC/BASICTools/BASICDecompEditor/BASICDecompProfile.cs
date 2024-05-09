using UnityEngine;

[CreateAssetMenu(fileName = "DecompData_", menuName = "BASIC/Decomp Editor/Decomp Profile", order = 1)]
public class BASICDecompProfile : ScriptableObject
{
    public bool DebugMode;

    [Header("General Options")]
    public bool YCTP;

    [Header("Notebooks")]
    public int maxNotebooks;

    public int startingNotebooks;

    public int startingFailedNotebooks;

    [Header("Stamina")]
    public float maxStamina;
}