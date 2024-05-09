using System;
using UnityEngine;


public class Script : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	private void Update()
	{
		if (!this.audioDevice.isPlaying & this.played)
		{
			Application.Quit();
		}
	}

	
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player" & !this.played)
		{
			this.audioDevice.Play();
			this.played = true;
		}
	}

	
	public AudioSource audioDevice;

	
	private bool played;
}
