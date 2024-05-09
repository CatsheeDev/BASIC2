
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoots : ItemBase
{
    public override void Pickup()
    {
        base.Pickup();
    }

    public override void Use()
    {
        GameControllerScript.Instance.player.ActivateBoots();
        base.StartCoroutine(GameControllerScript.Instance.BootAnimation());
        base.Use();
    }
}