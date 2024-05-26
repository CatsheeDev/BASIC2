using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class PauseMenuScript : MonoBehaviour
{
    
    private void Update()
    {
        if (this.usingJoystick & EventSystem.current.currentSelectedGameObject == null)
        {
            if (!this.gc.mouseLocked)
            {
                this.gc.LockMouse();
            }
        }
        else if (!this.usingJoystick && this.gc.mouseLocked)
        {
            this.gc.UnlockMouse();
        }
    }

    
    public GameControllerScript gc;

    private bool usingJoystick
    {
        get
        {
            return false;
        }
    }
}
