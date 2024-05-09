using System;
using UnityEngine;


public class CraftersTriggerScript : MonoBehaviour
{
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.crafters.GiveLocation(this.goTarget.position, false);
		}
	}

	
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.crafters.GiveLocation(this.fleeTarget.position, true);
		}
	}

	
	public Transform goTarget;

	
	public Transform fleeTarget;

	
	public CraftersScript crafters;
}
