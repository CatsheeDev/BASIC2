using System;
using UnityEngine;


public class DebugFirstPrizeScript : MonoBehaviour
{
	
	private void Start()
	{
	}

	
	private void Update()
	{
		base.transform.position = this.first.position + new Vector3((float)Mathf.RoundToInt(this.first.forward.x), 0f, (float)Mathf.RoundToInt(this.first.forward.z)) * 3f;
	}

	
	public Transform player;

	
	public Transform first;
}
