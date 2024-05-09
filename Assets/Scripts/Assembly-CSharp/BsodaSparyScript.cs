using System;
using UnityEngine;


public class BsodaSparyScript : MonoBehaviour
{
	
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>(); 
		this.rb.velocity = base.transform.forward * this.speed; 
		this.lifeSpan = 30f; 
	}

	
	private void Update()
	{
		this.rb.velocity = base.transform.forward * this.speed; 
		this.lifeSpan -= Time.deltaTime; 
		if (this.lifeSpan < 0f) 
		{
			UnityEngine.Object.Destroy(base.gameObject, 0f);
		}
	}

	
	public float speed;

	
	private float lifeSpan;

	
	private Rigidbody rb;
}
