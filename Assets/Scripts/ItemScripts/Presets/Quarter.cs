
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
            if (raycastHit3.collider.GetComponent<VendingMachine>() & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, raycastHit3.transform.position) <= 10f)
            {
                base.Use();
                foreach (int itemID in raycastHit3.collider.GetComponent<VendingMachine>().itemsCanVend)
                {
                    if (itemID == -1)
                    {
                        raycastHit3.collider.gameObject.GetComponent<TapePlayerScript>().Play();
                        base.Use();
                        continue; 
                    }

                    GameControllerScript.Instance.CollectItem(itemID);
                }
            }
        }
    }
}