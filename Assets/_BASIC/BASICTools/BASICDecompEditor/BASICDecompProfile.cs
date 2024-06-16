using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "DecompData_", menuName = "BASIC/Decomp Editor/Decomp Profile", order = 1)]
public class BASICDecompProfile : ScriptableObject
{
    public bool DebugMode = false;

    [Header("General Options")]
    public bool YCTP = true;

    public bool baldicator;

    public bool swapItems;

    [Header("Notebooks")]
    public int maxNotebooks = 7;

    public int startingNotebooks;

    public int startingFailedNotebooks;

    [Header("Stamina")]
    public float maxStamina = 100;

    [Header("EXPERIMENTAL")]
    [InfoBox("ENABLE AT OWN RISK", EInfoBoxType.Warning)]
    public bool UseExperiments;

    [ShowIf("EnabledExperiments")]
    public bool UseBASICController;

    public bool EnabledExperiments() { return UseExperiments; }
}