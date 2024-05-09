using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZestyBar : ItemBase
{
    public override void Pickup()
    {
        base.Pickup();
    }

    public override void Use()
    {
        GameControllerScript.Instance.player.stamina = GameControllerScript.Instance.player.maxStamina * 2f;
        base.Use();
    }
}
