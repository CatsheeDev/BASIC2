using Pixelplacement;
using System;
using UnityEngine;


public class CursorControllerScript : Singleton<CursorControllerScript> 
{
	public bool isLocked = true;
	public bool basic_deb_ovv; 

	public void LockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked; 
		Cursor.visible = false; 
		isLocked = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isLocked = false;
    }

    public void toggleMouse()
    {
        if (isLocked)
		{
			UnlockCursor();
		} else
		{
			LockCursor();
		}
    }
}
