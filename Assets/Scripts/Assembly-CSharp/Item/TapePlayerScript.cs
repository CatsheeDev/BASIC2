using System;
using UnityEngine;


public class TapePlayerScript : MonoBehaviour
{
	
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>();
	}

	
	private void Update()
	{
		if (this.audioDevice.isPlaying & Time.timeScale == 0f)
		{
			this.audioDevice.Pause();
		}
		else if (Time.timeScale > 0f & this.baldi.antiHearingTime > 0f)
		{
			this.audioDevice.UnPause();
		}
	}

	
	public void Play()
	{
		this.sprite.sprite = this.closedSprite;
		this.audioDevice.Play();
		if (this.baldi.isActiveAndEnabled) this.baldi.ActivateAntiHearing(30f);
	}

	
	public Sprite closedSprite;

	
	public SpriteRenderer sprite;

	
	public BaldiScript baldi;

	
	private AudioSource audioDevice;
}
