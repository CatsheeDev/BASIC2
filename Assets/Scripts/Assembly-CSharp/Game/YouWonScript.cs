using System;
using UnityEngine;


public class YouWonScript : MonoBehaviour
{
	
	private void Start()
	{
		this.delay = 10f;
	}

	
	private void Update()
	{
		this.delay -= Time.deltaTime;
		if (this.delay <= 0f)
		{
			Application.Quit();
		}
	}

	
	private float delay;
}
