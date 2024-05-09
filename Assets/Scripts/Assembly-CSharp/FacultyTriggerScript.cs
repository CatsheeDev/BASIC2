using System;
using UnityEngine;


public class FacultyTriggerScript : MonoBehaviour
{
	
	private void Start()
	{
		this.hitBox = base.GetComponent<BoxCollider>();
	}

	
	private void Update()
	{
	}

	
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			this.ps.ResetGuilt("faculty", 1f); 
		}
	}

	
	public PlayerScript ps;

	
	private BoxCollider hitBox;
}
