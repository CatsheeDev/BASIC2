using System;
using UnityEngine;
using UnityEngine.UI;


public class UiSettings : MonoBehaviour
{
	
	public void UpdateState()
	{
		if (this.sAuto.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 0);
		}
		else if (this.sXLarge.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 1);
		}
		else if (this.sLarge.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 2);
		}
		else if (this.sMed.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 3);
		}
		else if (this.sSmall.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 4);
		}
		if (this.hLow.isOn)
		{
			PlayerPrefs.SetInt("UiHeight", 0);
		}
		else if (this.hMed.isOn)
		{
			PlayerPrefs.SetInt("UiHeight", 1);
		}
		else if (this.hHigh.isOn)
		{
			PlayerPrefs.SetInt("UiHeight", 2);
		}
	}

	
	public void RestoreState()
	{
		this.size = PlayerPrefs.GetInt("UiSize");
		this.height = PlayerPrefs.GetInt("UiHeight");
		if (this.size == 0)
		{
			this.sAuto.isOn = true;
		}
		else if (this.size == 1)
		{
			this.sXLarge.isOn = true;
		}
		else if (this.size == 2)
		{
			this.sLarge.isOn = true;
		}
		else if (this.size == 3)
		{
			this.sMed.isOn = true;
		}
		else if (this.size == 4)
		{
			this.sSmall.isOn = true;
		}
		if (this.height == 0)
		{
			this.hLow.isOn = true;
		}
		else if (this.height == 1)
		{
			this.hMed.isOn = true;
		}
		else if (this.height == 2)
		{
			this.hHigh.isOn = true;
		}
	}

	
	public Toggle sAuto;

	
	public Toggle sXLarge;

	
	public Toggle sLarge;

	
	public Toggle sMed;

	
	public Toggle sSmall;

	
	public Toggle hLow;

	
	public Toggle hMed;

	
	public Toggle hHigh;

	
	private int size;

	
	private int height;
}
