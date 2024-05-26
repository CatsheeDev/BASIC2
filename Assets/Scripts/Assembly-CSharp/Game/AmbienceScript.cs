using System;
using UnityEngine;


public class AmbienceScript : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	public void PlayAudio()
	{
		int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 49f)); 
		if (!this.audioDevice.isPlaying & num == 0) 
		{
			base.transform.position = this.aiLocation.position; 
			int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, (float)(this.sounds.Length - 1))); 
			this.audioDevice.PlayOneShot(this.sounds[num2]); 
		}
	}

	
	public Transform aiLocation;

	
	public AudioClip[] sounds;

	
	public AudioSource audioDevice;
}
