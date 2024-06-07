using System;
using UnityEngine;


public class PickupScript : BASICInteractable
{
	public BASICItem itemIndex;

	private void FixedUpdate()
	{
		if (base.Interacted())
		{
			Debug.Log("htitttttt"); 
			gameObject.SetActive(false);
			GameControllerScript.Instance.CollectItem_BASIC((int)itemIndex, this);
		}
	}
}
