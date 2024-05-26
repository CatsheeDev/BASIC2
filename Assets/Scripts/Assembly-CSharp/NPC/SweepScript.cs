using System;
using UnityEngine;
using UnityEngine.AI;


public class SweepScript : BASICNPC
{
	
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.audioDevice = base.GetComponent<AudioSource>();
		this.origin = base.transform.position;
		this.waitTime = UnityEngine.Random.Range(120f, 180f);
	}

	
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.waitTime > 0f)
		{
			this.waitTime -= Time.deltaTime;
		}
		else if (!this.active)
		{
			this.active = true;
			this.wanders = 0;
			this.Wander(); 
			this.audioDevice.PlayOneShot(this.aud_Intro); 
		}
	}

	
	private void FixedUpdate()
	{
		if ((double)this.agent.velocity.magnitude <= 0.1 & this.coolDown <= 0f & this.wanders < 5 & this.active) 
		{
			this.Wander(); 
		}
		else if (this.wanders >= 5)
		{
			this.GoHome(); 
		}
	}

	
	private void Wander()
	{
		this.wanderer.GetNewTargetHallway();
		this.agent.SetDestination(this.wanderTarget.position);
		this.coolDown = 1f;
		this.wanders++;
	}

	
	private void GoHome()
	{
		this.agent.SetDestination(this.origin);
		this.waitTime = UnityEngine.Random.Range(120f, 180f);
		this.wanders = 0;
		this.active = false;
	}

	
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "NPC" || other.tag == "Player")
		{
			this.audioDevice.PlayOneShot(this.aud_Sweep);
		}
	}

	
	public Transform wanderTarget;

	
	public AILocationSelectorScript wanderer;

	
	public float coolDown;

	
	public float waitTime;

	
	public int wanders;

	
	public bool active;

	
	private Vector3 origin;

	
	public AudioClip aud_Sweep;

	
	public AudioClip aud_Intro;

	
	private NavMeshAgent agent;

	
	private AudioSource audioDevice;
}
