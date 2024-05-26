using System;
using UnityEngine;


public class BullyScript : BASICNPC
{
	
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>(); 
		this.waitTime = UnityEngine.Random.Range(60f, 120f); 
	}

	
	private void Update()
	{
		if (this.waitTime > 0f) 
		{
			this.waitTime -= Time.deltaTime;
		}
		else if (!this.active)
		{
			this.Activate(); 
		}
		if (this.active) 
		{
			this.activeTime += Time.deltaTime; 
			if (this.activeTime >= 180f & (base.transform.position - this.player.position).magnitude >= 120f) 
			{
				this.Reset(); 
			}
		}
		if (this.guilt > 0f)
		{
			this.guilt -= Time.deltaTime; 
		}
	}

	
	private void FixedUpdate()
	{
		Vector3 direction = this.player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + new Vector3(0f, 4f, 0f), direction, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & (base.transform.position - this.player.position).magnitude <= 30f & this.active)
		{
			if (!this.spoken) 
			{
				int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f)); 
				this.audioDevice.PlayOneShot(this.aud_Taunts[num]); 
				this.spoken = true; 
			}
			this.guilt = 10f; 
		}
	}

	
	private void Activate()
	{
		this.wanderer.GetNewTargetHallway(); 
		base.transform.position = this.wanderTarget.position + new Vector3(0f, 5f, 0f); 
		while ((base.transform.position - this.player.position).magnitude < 20f) 
		{
			this.wanderer.GetNewTargetHallway(); 
			base.transform.position = this.wanderTarget.position + new Vector3(0f, 5f, 0f);
        } 
		this.active = true; 
	}

	
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player") 
		{
			if (this.gc.item[0] == 0 & this.gc.item[1] == 0 & this.gc.item[2] == 0) 
			{
				this.audioDevice.PlayOneShot(this.aud_Denied); 
			}
			else
			{
				int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 2f)); 
				while (this.gc.item[num] == 0) 
				{
					num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 2f)); 
				}
				this.gc.LoseItem(num); 
				int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
				this.audioDevice.PlayOneShot(this.aud_Thanks[num2]);
				this.Reset();
			}
		}
	}

	
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "Principal of the Thing" & this.guilt > 0f) 
		{
			this.Reset(); 
		}
	}

	
	private void Reset()
	{
		base.transform.position = base.transform.position - new Vector3(0f, 20f, 0f); 
		this.waitTime = UnityEngine.Random.Range(60f, 120f); 
		this.active = false; 
		this.activeTime = 0f; 
		this.spoken = false; 
	}

	
	public Transform player;

	
	public GameControllerScript gc;

	
	public Renderer bullyRenderer;

	
	public Transform wanderTarget;

	
	public AILocationSelectorScript wanderer;

	
	public float waitTime;

	
	public float activeTime;

	
	public float guilt;

	
	public bool active;

	
	public bool spoken;

	
	private AudioSource audioDevice;

	
	public AudioClip[] aud_Taunts = new AudioClip[2];

	
	public AudioClip[] aud_Thanks = new AudioClip[2];

	
	public AudioClip aud_Denied;
}
