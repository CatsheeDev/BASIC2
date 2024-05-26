using System;

using UnityEngine;


public class InputTypeManager : MonoBehaviour
{
	
	private void Awake()
	{
		Input.simulateMouseWithTouches = false;
		if (InputTypeManager.itm == null)
		{
			InputTypeManager.itm = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else if (InputTypeManager.itm != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	
	private void Update()
	{
		if (Input.touchCount > 0 && !InputTypeManager.usingTouch)
		{
			InputTypeManager.usingTouch = true;
		}
		else if (Input.anyKeyDown)
		{
			InputTypeManager.usingTouch = false;
		}
	}

	
	private static InputTypeManager itm;

	
	public static bool usingTouch;
}
