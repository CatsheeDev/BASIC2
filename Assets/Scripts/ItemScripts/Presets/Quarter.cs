
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarter : ItemBase
{
    public override void Pickup()
    {
        base.Pickup();
    }

    public override void Use()
    {
        Ray ray3 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
        RaycastHit raycastHit3;
        if (Physics.Raycast(ray3, out raycastHit3))
        {
            if (raycastHit3.collider.name == "BSODAMachine" & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, raycastHit3.transform.position) <= 10f)
            {
                base.Use();
                GameControllerScript.Instance.CollectItem(4);
            }
            else if (raycastHit3.collider.name == "ZestyMachine" & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, raycastHit3.transform.position) <= 10f)
            {
                base.Use();
                GameControllerScript.Instance.CollectItem(1);
            }
            else if (raycastHit3.collider.name == "PayPhone" & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, raycastHit3.transform.position) <= 10f)
            {
                raycastHit3.collider.gameObject.GetComponent<TapePlayerScript>().Play();
                base.Use();
            }
        }
    }
}