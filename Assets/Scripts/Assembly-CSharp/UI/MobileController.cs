using System;
using UnityEngine;


public class MobileController : MonoBehaviour
{
	
	private void Start()
	{
		base.gameObject.SetActive(false);
	}

	
	private void Update()
	{
		if (InputTypeManager.usingTouch)
		{
			if (!this.active)
			{
				this.ActivateMobileControls();
			}
		}
		else if (this.active)
		{
			this.DeactivateMobileControls();
		}
	}

	
	private void ActivateMobileControls()
	{
		this.simpleControls.SetActive(true);
		this.active = true;
	}

	
	private void DeactivateMobileControls()
	{
		this.proControls.SetActive(false);
		this.simpleControls.SetActive(false);
		this.active = false;
	}

	
	public GameObject simpleControls;

	
	public GameObject proControls;

	
	private bool active;
}
