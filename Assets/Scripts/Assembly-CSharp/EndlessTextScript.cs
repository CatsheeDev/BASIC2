using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class EndlessTextScript : MonoBehaviour
{
	
	private void Start()
	{
		this.text.text = string.Concat(new object[]
		{
			this.text.text,
			"\nHigh Score: ",
			PlayerPrefs.GetInt("HighBooks"),
			" Notebooks"
		});
	}

	
	public TMP_Text text;
}
