using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemEditorAPI
{
    public static GameObject FindItem(BASICItem itemID)
    {
        return GameControllerScript.Instance.itemObjects[(int)itemID -1];
    }

    public static int GetItemID(BASICItem itemID)
    {
        return (int)itemID;
    }
}
