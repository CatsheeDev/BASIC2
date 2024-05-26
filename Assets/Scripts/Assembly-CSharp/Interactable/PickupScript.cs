using System;
using UnityEngine;


public class PickupScript : BASICInteractable
{
	[SerializeField] private int itemIndex;

	private void FixedUpdate()
	{
		if (base.Interacted())
		{
			gameObject.SetActive(false);
			GameControllerScript.Instance.CollectItem_BASIC(itemIndex);
		}
	}
}
