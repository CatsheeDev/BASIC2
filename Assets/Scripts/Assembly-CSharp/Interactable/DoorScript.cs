using System;
using UnityEngine;


public class DoorScript : BASICInteractable
{
	
	private void Start()
	{
		this.myAudio = base.GetComponent<AudioSource>();

		//BASIC
		baldi = GameControllerScript.Instance.baldiScrpt;
		player = GameControllerScript.Instance.playerTransform; 
    }

	
	private void Update()
	{
		if (this.lockTime > 0f) 
		{
			this.lockTime -= 1f * Time.deltaTime;
		}
		else if (this.bDoorLocked) 
		{
			this.UnlockDoor();
		}
		if (this.openTime > 0f) 
		{
			this.openTime -= 1f * Time.deltaTime;
		}
		if (this.openTime <= 0f & this.bDoorOpen)
		{
			this.barrier.enabled = true; 
			this.invisibleBarrier.enabled = true; 
			this.bDoorOpen = false; 
			this.inside.material = this.closed; 
			this.outside.material = this.closed; 
            if (this.silentOpens <= 0) 
			{
				this.myAudio.PlayOneShot(this.doorClose, 1f); 
			}
		}

		if (base.Interacted() && !bDoorLocked) 
		{
			this.OpenDoor();
			if (this.silentOpens > 0) 
			{
				this.silentOpens--; 
			}
		}
	}

	
	public void OpenDoor()
	{
		if (this.silentOpens <= 0 && !this.bDoorOpen) 
		{
			this.myAudio.PlayOneShot(this.doorOpen, 1f);
			if (this.baldi.isActiveAndEnabled & this.silentOpens <= 0)
			{
				this.baldi.Hear(base.transform.position, 1f); 
			}
		}
		this.barrier.enabled = false; 
		this.invisibleBarrier.enabled = false;
		this.bDoorOpen = true; 
		this.inside.material = this.open; 
		this.outside.material = this.open; 
        this.openTime = 3f; 
	}

	
	private void OnTriggerStay(Collider other)
	{
		if (!this.bDoorLocked & other.CompareTag("NPC")) 
		{
			this.OpenDoor();
		}
	}

	
	public void LockDoor(float time) 
	{
		this.bDoorLocked = true;
		this.lockTime = time;
	}

	
	public void UnlockDoor() 
	{
		this.bDoorLocked = false;
	}

    
	public bool DoorLocked
	{
		get
		{
			return this.bDoorLocked;
		}
	}

	
	public void SilenceDoor() 
	{
		this.silentOpens = 4;
	}

	
	public float openingDistance;

	
	private Transform player;

	
	private BaldiScript baldi;

	
	public MeshCollider barrier;

	
	public MeshCollider trigger;

	
	public MeshCollider invisibleBarrier;

	
	public MeshRenderer inside;

	
	public MeshRenderer outside;

	
	public AudioClip doorOpen;

	
	public AudioClip doorClose;

	
	public Material closed;

	
	public Material open;

	
	private bool bDoorOpen;

	
	private bool bDoorLocked;

	
	public int silentOpens;

	
	private float openTime;

	
	public float lockTime;

	
	private AudioSource myAudio;

}
