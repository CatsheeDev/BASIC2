using System;

using UnityEngine;
using UnityEngine.SceneManagement;


public class WarningScreenScript : MonoBehaviour
{
	
	private void Start()
	{
		
	}

	
	private void Update()
	{
		if (Input.anyKeyDown)
		{
			SceneManager.LoadScene("MainMenu");
		}
	}

	
	
}
