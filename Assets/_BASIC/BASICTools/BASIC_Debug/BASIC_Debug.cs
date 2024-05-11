using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BASIC.Debugging
{
    public class BASIC_Debug : MonoBehaviour
    {
        private GameControllerScript currentGameController;
        private BASIC_DebugBase[] objects;

        private float lastTimescale;

        private void Start()
        {
            currentGameController = GameControllerScript.Instance;

            if (currentGameController == null)
            {
                Debug.LogError("BASIC // DEBUG // GAME CONTROLLER NOT FOUND");
                Destroy(gameObject);
                return;
            }

            gameObject.GetComponent<Canvas>().enabled = false;
            objects = GetComponentsInChildren<BASIC_DebugBase>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && currentGameController.debugMode)
            {
                gameObject.GetComponent<Canvas>().enabled = !gameObject.GetComponent<Canvas>().enabled;
                if (currentGameController.cursorController.isLocked)
                {
                    currentGameController.cursorController.UnlockCursor();
                }
                else
                {
                    currentGameController.cursorController.LockCursor();
                }

                foreach (var obj in objects)
                {
                    obj.RefreshList();
                }
            }

            if (currentGameController.cursorController.isLocked && gameObject.GetComponent<Canvas>().enabled)
            {
                currentGameController.cursorController.UnlockCursor();
            }
        }
    }

}