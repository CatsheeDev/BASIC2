using System;
using System.Collections;
using UnityEngine;


public class DefaultSettingsScript : MonoBehaviour
{
	
	private void Start()
	{
		if (!PlayerPrefs.HasKey("OptionsSet"))
		{
			this.options.SetActive(true);
			base.StartCoroutine(this.CloseOptions());
			this.canvas.enabled = false;
		}
	}

	
	public IEnumerator CloseOptions()
	{
		yield return new WaitForEndOfFrame();
		this.canvas.enabled = true;
		this.options.SetActive(false);
		yield break;
	}

	
	public GameObject options;

	
	public Canvas canvas;
}
