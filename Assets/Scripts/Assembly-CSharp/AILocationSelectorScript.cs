using System;
using UnityEngine;


public class AILocationSelectorScript : MonoBehaviour
{
	
	public void GetNewTarget()
	{
		this.id = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 28f)); 
		base.transform.position = this.newLocation[this.id].position; 
		this.ambience.PlayAudio(); 
	}

	
	public void GetNewTargetHallway()
	{
		this.id = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 15f)); 
		base.transform.position = this.newLocation[this.id].position; 
		this.ambience.PlayAudio(); 
	}

	
	public void QuarterExclusive()
	{
		this.id = Mathf.RoundToInt(UnityEngine.Random.Range(1f, 15f)); 
		base.transform.position = this.newLocation[this.id].position; 
	}

	
	public Transform[] newLocation = new Transform[29];

	
	public AmbienceScript ambience;

	
	private int id;
}
