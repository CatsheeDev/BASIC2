
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDNS : ItemBase
{
    public override void Pickup()
    {
        base.Pickup();
    }

    public override void Use()
    {
        Ray ray5 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
        RaycastHit raycastHit5;
        if (Physics.Raycast(ray5, out raycastHit5) && (raycastHit5.collider.tag == "Door" & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, raycastHit5.transform.position) <= 10f))
        {
            raycastHit5.collider.gameObject.GetComponent<DoorScript>().SilenceDoor();
            GameControllerScript.Instance.audioDevice.PlayOneShot(GameControllerScript.Instance.aud_Spray);
            base.Use();
        }
    }
}