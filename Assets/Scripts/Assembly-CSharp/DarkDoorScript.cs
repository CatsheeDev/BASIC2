using System;
using UnityEngine;


public class DarkDoorScript : MonoBehaviour
{
	
	private void Update()
	{
		if (this.door.bDoorOpen)
		{
			this.mesh.material = this.lightDoo60;
		}
		else if (this.door.bDoorLocked)
		{
			this.mesh.material = this.lightDooLock;
		}
		else
		{
			this.mesh.material = this.lightDoo0;
		}
	}

	
	public SwingingDoorScript door;

	
	public Material lightDoo0;

	
	public Material lightDoo60;

	
	public Material lightDooLock;

	
	public MeshRenderer mesh;
}
