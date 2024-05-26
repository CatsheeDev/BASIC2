using System;
using UnityEngine;


public class AudioQueueScript : MonoBehaviour
{
	
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>();
	}

	
	private void Update()
	{
		if (!this.audioDevice.isPlaying && this.audioInQueue > 0)
		{
			this.PlayQueue();
		}
	}

	
	public void QueueAudio(AudioClip sound)
	{
		this.audioQueue[this.audioInQueue] = sound;
		this.audioInQueue++;
	}

	
	private void PlayQueue()
	{
		this.audioDevice.PlayOneShot(this.audioQueue[0]);
		this.UnqueueAudio();
	}

	
	private void UnqueueAudio()
	{
		for (int i = 1; i < this.audioInQueue; i++)
		{
			this.audioQueue[i - 1] = this.audioQueue[i];
		}
		this.audioInQueue--;
	}

	
	public void ClearAudioQueue()
	{
		this.audioInQueue = 0;
	}

	
	private AudioSource audioDevice;

	
	private int audioInQueue;

	
	private AudioClip[] audioQueue = new AudioClip[100];
}
