
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmClock : ItemBase
{
    public override void Pickup()
    {
        base.Pickup();
    }

    public override void Use()
    {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameControllerScript.Instance.alarmClock, GameControllerScript.Instance.playerTransform.position, GameControllerScript.Instance.cameraTransform.rotation);
        gameObject.GetComponent<AlarmClockScript>().baldi = GameControllerScript.Instance.baldiScrpt;
        base.Use();
    }
}