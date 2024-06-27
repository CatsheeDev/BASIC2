using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapToRoomAsset //woah a static class in basic? unheard of 
{
    public static string Convert(GameObject MapParent, string AssetName) 
    {
        string Converted = string.Empty;
        char insertedChar = '{';
        char insertedBackChar = '}';
        foreach (Transform t in MapParent.transform)
        {
            if (t.GetChild(0).childCount == 0) continue; 
            PlusTile currTile = null; 
            if (t.TryGetComponent<PlusTile>(out currTile))
            {
                Converted += $"{AssetName}.cells.Add(new CellData() {insertedChar} pos= new IntVector2({currTile.pos.x}, {currTile.pos.z}), type = {currTile.type} {insertedBackChar});";
            }
        }

        Debug.Log(Converted);
        return Converted;
    }
}
