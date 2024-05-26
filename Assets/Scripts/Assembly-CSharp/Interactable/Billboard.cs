using System;
using UnityEngine;

//OPTIMIZED BILLBOARD PACKAGE IS AVAILABLE, PLEASE USE THAT INSTEAD OF DIRECTLY EDITING THIS
public class Billboard : MonoBehaviour
{
	
	private void Start()
	{
		this.m_Camera = Camera.main;
	}

	
	private void LateUpdate()
	{
		base.transform.LookAt(base.transform.position + this.m_Camera.transform.rotation * Vector3.forward); 
	}

	
	private Camera m_Camera;
}
