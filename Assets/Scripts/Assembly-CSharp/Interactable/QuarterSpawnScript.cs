using System;
using UnityEngine;


public class QuarterSpawnScript : MonoBehaviour
{
	
	private void Start()
	{
		this.wanderer.QuarterExclusive();
		base.transform.position = this.location.position + Vector3.up * 4f;
	}

	
	private void Update()
	{
	}

	
	public AILocationSelectorScript wanderer;

	
	public Transform location;
}
