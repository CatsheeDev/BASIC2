using System;
using UnityEngine;


public class DebugScript : MonoBehaviour
{
	
	private void Start()
	{
		if (this.limitFramerate)
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = this.framerate;
		}
	}

	
	private void Update()
	{
	}

	
	public bool limitFramerate;

	
	public int framerate;
}
