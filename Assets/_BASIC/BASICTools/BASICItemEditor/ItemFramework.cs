using UnityEngine;

[System.Serializable]
public struct ItemInfo
{
    public int Value;
    public string Name;
    public string InGameName;

    public UnityEngine.Object itemScript;

    public Texture2D Icon;
}


public class ItemBase : MonoBehaviour
{
    public virtual void Pickup() { }
    public virtual void Use()
    {
        GameControllerScript.Instance.ResetItem(); 
    }

    public int uses; 

    public int useAmount; 
}
