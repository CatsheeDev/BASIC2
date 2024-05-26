using System;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
	
	private void Start()
	{
		int @int = PlayerPrefs.GetInt("UiSize");
		int int2 = PlayerPrefs.GetInt("UiHeight");
		if (@int == 1)
		{
			this.normScaler.referenceResolution = new Vector2(640f, 480f);
		}
		else if (@int == 2)
		{
			this.normScaler.referenceResolution = new Vector2(800f, 600f);
		}
		else if (@int == 3)
		{
			this.normScaler.referenceResolution = new Vector2(900f, 720f);
		}
		else if (@int == 4)
		{
			this.normScaler.referenceResolution = new Vector2(1024f, 720f);
		}
		if (int2 == 1)
		{
			foreach (RectTransform rectTransform in this.transforms)
			{
				rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + (float)(Screen.height / 8), rectTransform.position.z);
			}
		}
		else if (int2 == 2)
		{
			foreach (RectTransform rectTransform2 in this.transforms)
			{
				rectTransform2.position = new Vector3(rectTransform2.position.x, rectTransform2.position.y + (float)(Screen.height / 4), rectTransform2.position.z);
			}
		}
	}

	
	public CanvasScaler normScaler;
	
	public RectTransform[] transforms;
}
