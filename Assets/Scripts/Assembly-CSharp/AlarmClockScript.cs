using System;
using UnityEngine;


public class AlarmClockScript : MonoBehaviour
{
	
	private void Start()
	{
		this.timeLeft = 30f;
		this.lifeSpan = 35f;
	}

	
	private void Update()
	{
		if (this.timeLeft >= 0f) 
		{
			this.timeLeft -= Time.deltaTime; 
		}
		else if (!this.rang) 
		{
			this.Alarm(); 
		}
		if (this.lifeSpan >= 0f) 
		{
			this.lifeSpan -= Time.deltaTime; 
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject, 0f); 
		}
	}

	
	private void Alarm()
	{
		this.rang = true;
		if (this.baldi.isActiveAndEnabled) this.baldi.Hear(base.transform.position, 8f); 
		this.audioDevice.clip = this.ring;
		this.audioDevice.loop = false; 
		this.audioDevice.Play(); 
	}

	
	public float timeLeft;

	
	private float lifeSpan;

	
	private bool rang;

	
	public BaldiScript baldi;

	
	public AudioClip ring;

	
	public AudioSource audioDevice;
}
