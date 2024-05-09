using System;
using UnityEngine;


public class EntranceScript : MonoBehaviour
{
	
	public void Lower()
	{
		base.transform.position = base.transform.position - new Vector3(0f, 10f, 0f);
		if (this.gc.finaleMode)
		{
			this.wall.material = this.map;
		}
	}

	
	public void Raise()
	{
		base.transform.position = base.transform.position + new Vector3(0f, 10f, 0f);
	}

	
	public GameControllerScript gc;

	
	public Material map;

	
	public MeshRenderer wall;
}
