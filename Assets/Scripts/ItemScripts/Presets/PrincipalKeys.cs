
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalKeys : ItemBase
{
    public override void Pickup()
    {
        base.Pickup();
    }

    public override void Use()
    {
        Ray ray2 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
        RaycastHit raycastHit2;
        if (Physics.Raycast(ray2, out raycastHit2) && (raycastHit2.collider.tag == "Door" & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, raycastHit2.transform.position) <= 10f))
        {
            DoorScript component = raycastHit2.collider.gameObject.GetComponent<DoorScript>();
            if (component.DoorLocked)
            {
                component.UnlockDoor();
                component.OpenDoor();
                base.Use();
            }
        }
    }
}