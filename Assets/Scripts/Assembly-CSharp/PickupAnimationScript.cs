using System;
using UnityEngine;


public class PickupAnimationScript : MonoBehaviour
{
	
	private void Start()
	{
		this.itemPosition = base.GetComponent<Transform>();
	}

	
	private void Update()
	{
		this.itemPosition.localPosition = new Vector3(0f, Mathf.Sin((float)Time.frameCount * 0.017453292f) / 2f + 1f, 0f);
	}

	
	private Transform itemPosition;
}
