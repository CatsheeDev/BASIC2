using System;
using UnityEngine;
using UnityEngine.AI;


public class HuggingScript : MonoBehaviour
{
	
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
	}

	
	private void Update()
	{
		if (this.failSave > 0f)
		{
			this.failSave -= Time.deltaTime;
		}
		else
		{
			this.inBsoda = false;
		}
	}

	
	private void FixedUpdate()
	{
		if (this.inBsoda)
		{
			this.rb.velocity = this.otherVelocity;
		}
	}

	
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "1st Prize")
		{
			this.inBsoda = true;
			this.otherVelocity = this.rb.velocity * 0.1f + other.GetComponent<NavMeshAgent>().velocity;
			this.failSave = 1f;
		}
	}

	
	private void OnTriggerExit()
	{
		this.inBsoda = false;
	}

	
	private Rigidbody rb;

	
	private Vector3 otherVelocity;

	
	public bool inBsoda;

	
	private float failSave;
}
