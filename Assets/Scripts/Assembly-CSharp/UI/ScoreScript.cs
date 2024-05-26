using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class ScoreScript : MonoBehaviour
{
	
	private void Start()
	{
		if (PlayerPrefs.GetString("CurrentMode") == "endless")
		{
			this.scoreText.SetActive(true);
			this.text.text = "Score:\n" + PlayerPrefs.GetInt("CurrentBooks") + " Notebooks";
		}
	}

	
	private void Update()
	{
	}

	
	public GameObject scoreText;

	
	public TMP_Text text;
}
