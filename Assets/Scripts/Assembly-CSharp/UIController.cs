using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    
    private void Start()
    {
        if (this.unlockOnStart & !this.joystickEnabled)
        {
            this.cc.UnlockCursor();
        }
    }

    
    private void OnEnable()
    {
        this.dummyButtonPC.Select();
        this.UpdateControllerType();
    }

    
    private void Update()
    {
        this.UpdateControllerType();
    }

    
    public void SwitchMenu()
    {
        this.SelectDummy();
        this.UpdateControllerType();
    }

    
    private void UpdateControllerType()
    {
        if (!this.joystickEnabled & usingJoystick)
        {
            this.joystickEnabled = true;
            if (this.controlMouse)
            {
                this.cc.LockCursor();
            }
        }
        else if (this.joystickEnabled & !usingJoystick)
        {
            this.joystickEnabled = false;
            if (this.controlMouse)
            {
                this.cc.UnlockCursor();
            }
        }
        this.UIUpdate();
    }

    
    private void UIUpdate()
    {
        if (this.uiControlEnabled)
        {
            if (this.joystickEnabled)
            {
                if (EventSystem.current.currentSelectedGameObject.tag != this.buttonTag & this.firstButton != null)
                {
                    this.firstButton.Select();
                    this.firstButton.OnSelect(null);
                }
            }
            else
            {
                this.SelectDummy();
            }
        }
    }

    
    public void EnableControl()
    {
        this.uiControlEnabled = true;
    }

    
    public void DisableControl()
    {
        this.uiControlEnabled = false;
    }

    
    private void SelectDummy()
    {
        this.dummyButtonPC.Select();
    }

    
    public CursorControllerScript cc;

    
    private bool joystickEnabled;

    
    public bool controlMouse;

    
    public bool unlockOnStart;

    
    public bool uiControlEnabled;

    
    public Selectable firstButton;

    
    public Selectable dummyButtonPC;

    
    public Selectable dummyButtonElse;

    
    public string buttonTag;

    public bool usingJoystick
    {
        get
        {
            return false;
        }
    }
}
