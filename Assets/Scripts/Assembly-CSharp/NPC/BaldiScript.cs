using System;
using UnityEngine;
using UnityEngine.AI;


public class BaldiScript : BASICNPC
{
	[SerializeField] private Animator baldicator; 


    private void Start()
	{
		this.baldiAudio = base.GetComponent<AudioSource>(); 
		this.agent = base.GetComponent<NavMeshAgent>(); 
		this.timeToMove = this.baseTime; 
		this.Wander(); 
		if (PlayerPrefs.GetInt("Rumble") == 1)
		{
			this.rumble = true;
		}
	}

	
	private void Update()
	{
		if (this.timeToMove > 0f) 
		{
			this.timeToMove -= 1f * Time.deltaTime;
		}
		else
		{
			this.Move(); 
		}
		if (this.coolDown > 0f) 
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.baldiTempAnger > 0f) 
		{
			this.baldiTempAnger -= 0.02f * Time.deltaTime;
		}
		else
		{
			this.baldiTempAnger = 0f; 
		}
		if (this.antiHearingTime > 0f) 
		{
			this.antiHearingTime -= Time.deltaTime;
		}
		else
		{
			this.antiHearing = false;
		}
		if (this.endless) 
		{
			if (this.timeToAnger > 0f) 
			{
				this.timeToAnger -= 1f * Time.deltaTime;
			}
			else
			{
				this.timeToAnger = this.angerFrequency; 
				this.GetAngry(this.angerRate); 
				this.angerRate += this.angerRateRate; 
			}
		}
	}

	
	private void FixedUpdate()
	{
		if (this.moveFrames > 0f) 
		{
			this.moveFrames -= 1f;
			this.agent.speed = this.speed;
		}
		else
		{
			this.agent.speed = 0f;
		}
		Vector3 direction = this.player.position - base.transform.position; 
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player") 
		{
			this.db = true;
			this.TargetPlayer(); 
		}
		else
		{
			this.db = false;
		}
	}

	
	private void Wander()
	{
		this.wanderer.GetNewTarget(); 
		this.agent.SetDestination(this.wanderTarget.position); 
		this.coolDown = 1f; 
		this.currentPriority = 0f;
	}

	
	public void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position); 
		this.coolDown = 1f;
		this.currentPriority = 0f;
	}

	
	private void Move()
	{
		if (base.transform.position == this.previous & this.coolDown < 0f) 
		{
			this.Wander();
		}
		this.moveFrames = 10f;
		this.timeToMove = this.baldiWait - this.baldiTempAnger;
		this.previous = base.transform.position; 
		this.baldiAudio.PlayOneShot(this.slap); 
		this.baldiAnimator.SetTrigger("slap"); 
		if (this.rumble)
		{
			float num = Vector3.Distance(base.transform.position, this.player.position);
			if (num < this.vibrationDistance)
			{
				float motorLevel = 1f - num / this.vibrationDistance;
			}
		}
	}

	
	public void GetAngry(float value)
	{
		this.baldiAnger += value; 
		if (this.baldiAnger < 0.5f) 
		{
			this.baldiAnger = 0.5f;
		}
		this.baldiWait = -3f * this.baldiAnger / (this.baldiAnger + 2f / this.baldiSpeedScale) + 3f; 
	}

	
	public void GetTempAngry(float value)
	{
		this.baldiTempAnger += value; 
	}

	
	public void Hear(Vector3 soundLocation, float priority)
	{
		if (!this.antiHearing && priority >= this.currentPriority) 
		{
			this.agent.SetDestination(soundLocation); 
			this.currentPriority = priority;

            playBaldicator("Baldicator_Look");
        } else
		{
            playBaldicator("Baldicator_Think");
        }
	}

	public void playBaldicator(string name)
	{
		if (GameControllerScript.Instance.settingsProfile.baldicator)
		{
            baldicator.Play(name, -1, 0f);
        }
    }
	public void ActivateAntiHearing(float t)
	{
		this.Wander(); 
		this.antiHearing = true; 
		this.antiHearingTime = t; 
	}

	
	public bool db;

	
	public float baseTime;

	
	public float speed;

	
	public float timeToMove;

	
	public float baldiAnger;

	
	public float baldiTempAnger;

	
	public float baldiWait;

	
	public float baldiSpeedScale;

	
	private float moveFrames;

	
	private float currentPriority;

	
	public bool antiHearing;

	
	public float antiHearingTime;

	
	public float vibrationDistance;

	
	public float angerRate;

	
	public float angerRateRate;

	
	public float angerFrequency;

	
	public float timeToAnger;

	
	public bool endless;

	
	public Transform player;

	
	public Transform wanderTarget;

	
	public AILocationSelectorScript wanderer;

	
	private AudioSource baldiAudio;

	
	public AudioClip slap;

	
	public AudioClip[] speech = new AudioClip[3];

	
	public Animator baldiAnimator;

	
	public float coolDown;

	
	private Vector3 previous;

	
	private bool rumble;

	
	private NavMeshAgent agent;

}
