using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartButton : MonoBehaviour
{
	
	public void StartGame()
	{
		if (this.currentMode == StartButton.Mode.Story)
		{
			PlayerPrefs.SetString("CurrentMode", "story");
		}
		else
		{
			PlayerPrefs.SetString("CurrentMode", "endless");
		}
		SceneManager.LoadSceneAsync("School");
	}

	
	public StartButton.Mode currentMode;

	
	public enum Mode
	{
		
		Story,
		
		Endless
	}
}
