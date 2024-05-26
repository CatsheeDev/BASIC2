using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitTriggerScript : MonoBehaviour
{
	
	private void OnTriggerEnter(Collider other)
	{
		if (this.gc.notebooks >= gc.settingsProfile.maxNotebooks & other.tag == "Player")
		{
			if (this.gc.failedNotebooks >= gc.settingsProfile.maxNotebooks) 
			{
				SceneManager.LoadScene("Secret"); 
			}
			else
			{
				SceneManager.LoadScene("Results"); 
			}
		}
	}

	
	public GameControllerScript gc;
}
