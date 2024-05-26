using System;
using UnityEngine;


public class PlatformSpecificMenu : MonoBehaviour
{
	
	private void Start()
	{
		this.pC.SetActive(true);
	}

	
	public GameObject pC;

	
	public GameObject mobile;
}
