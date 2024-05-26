using System;
using UnityEngine;


public class NeedMoreScript : MonoBehaviour
{
	
	private void OnTriggerEnter(Collider other)
	{
		if (this.gc.notebooks < 2 & other.tag == "Player")
		{
			this.audioDevice.PlayOneShot(this.baldiDoor, 1f);
		}
	}

	
	public GameControllerScript gc;

	
	public AudioSource audioDevice;

	
	public AudioClip baldiDoor;
}
