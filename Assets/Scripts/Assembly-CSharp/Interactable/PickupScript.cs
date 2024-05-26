using System;
using UnityEngine;


public class PickupScript : MonoBehaviour
{
	[SerializeField] private int itemIndex;
	[SerializeField] private float pickupDistance = 15f; 
	private void Start()
	{
		
	}

	
	private void FixedUpdate()
	{
		if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				if (Vector3.Distance(GameControllerScript.Instance.playerTransform.position, transform.position) < pickupDistance)
				{
                    gameObject.SetActive(false);
                    GameControllerScript.Instance.CollectItem_BASIC(itemIndex); 
                }
			}
		}
	}
}
