using System;
using UnityEngine;


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
