
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowDoorLock : ItemBase
{
    public override void Pickup()
    {
        base.Pickup();
    }

    public override void Use()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "SwingingDoor" & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, raycastHit.transform.position) <= 10f))
        {
            raycastHit.collider.gameObject.GetComponent<SwingingDoorScript>().LockDoor(15f);
            base.Use();
        }
    }
}