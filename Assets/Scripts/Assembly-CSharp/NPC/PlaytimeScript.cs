using System;
using UnityEngine;
using UnityEngine.AI;


public class PlaytimeScript : BASICNPC
{
	
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>(); 
		this.audioDevice = base.GetComponent<AudioSource>();
		this.Wander(); 
	}

	
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.playCool >= 0f)
		{
			this.playCool -= Time.deltaTime;
		}
		else if (this.animator.GetBool("disappointed"))
		{
			this.playCool = 0f;
			this.animator.SetBool("disappointed", false);
		}
	}

	
	private void FixedUpdate()
	{
		if (!this.ps.jumpRope)
		{
			Vector3 direction = this.player.position - base.transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position, direction, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & (base.transform.position - this.player.position).magnitude <= 80f & this.playCool <= 0f)
			{
				this.playerSeen = true; 
				this.TargetPlayer();
			}
			else if (this.playerSeen & this.coolDown <= 0f)
			{
				this.playerSeen = false; 
				this.Wander();
			}
			else if (this.agent.velocity.magnitude <= 1f & this.coolDown <= 0f)
			{
				this.Wander();
			}
			this.jumpRopeStarted = false;
		}
		else
		{
			if (!this.jumpRopeStarted)
			{
				this.agent.Warp(base.transform.position - base.transform.forward * 10f); 
			}
			this.jumpRopeStarted = true;
			this.agent.speed = 0f;
			this.playCool = 15f;
		}
	}

	
	private void Wander()
	{
		this.wanderer.GetNewTargetHallway();
		this.agent.SetDestination(this.wanderTarget.position);
		this.agent.speed = 15f;
		this.playerSpotted = false;
		this.audVal = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
		if (!this.audioDevice.isPlaying)
		{
			this.audioDevice.PlayOneShot(this.aud_Random[this.audVal]);
		}
		this.coolDown = 1f;
	}

	
	private void TargetPlayer()
	{
		this.animator.SetBool("disappointed", false); 
		this.agent.SetDestination(this.player.position); 
		this.agent.speed = 20f; 
		this.coolDown = 0.2f;
		if (!this.playerSpotted)
		{
			this.playerSpotted = true;
			this.audioDevice.PlayOneShot(this.aud_LetsPlay);
		}
	}

	
	public void Disappoint()
	{
		this.animator.SetBool("disappointed", true); 
		this.audioDevice.Stop();
		this.audioDevice.PlayOneShot(this.aud_Sad);
	}

	
	public bool db;

	
	public bool playerSeen;

	
	public bool disappointed;

	
	public int audVal;

	
	public Animator animator;

	
	public Transform player;

	
	public PlayerScript ps;

	
	public Transform wanderTarget;

	
	public AILocationSelectorScript wanderer;

	
	public float coolDown;

	
	public float playCool;

	
	public bool playerSpotted;

	
	public bool jumpRopeStarted;

	
	private NavMeshAgent agent;

	
	public AudioClip[] aud_Numbers = new AudioClip[10];

	
	public AudioClip[] aud_Random = new AudioClip[2];

	
	public AudioClip aud_Instrcutions;

	
	public AudioClip aud_Oops;

	
	public AudioClip aud_LetsPlay;

	
	public AudioClip aud_Congrats;

	
	public AudioClip aud_ReadyGo;

	
	public AudioClip aud_Sad;

	
	public AudioSource audioDevice;
}
