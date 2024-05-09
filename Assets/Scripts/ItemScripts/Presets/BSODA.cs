
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSODA : ItemBase
{
    public override void Pickup()
    {
        base.Pickup();
    }

    public override void Use()
    {
        UnityEngine.Object.Instantiate<GameObject>(GameControllerScript.Instance.bsodaSpray, GameControllerScript.Instance.playerTransform.position, GameControllerScript.Instance.cameraTransform.rotation);
        GameControllerScript.Instance.player.ResetGuilt("drink", 1f);
        GameControllerScript.Instance.audioDevice.PlayOneShot(GameControllerScript.Instance.aud_Soda);
        base.Use();
    }
}