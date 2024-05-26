using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BASICInteractable : MonoBehaviour
{
    [SerializeField] private float interactDistance;

    /// <returns>Returns true if player has clicked, timeScale is not 0, the raycast to object has hit and is within interactDistance</returns>
    protected bool Interacted()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
        {
            RaycastHit raycastHit;
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f)), out raycastHit))
                return false;

            if (Vector3.Distance(GameControllerScript.Instance.playerTransform.position, transform.position) < interactDistance)
                return true;
        }

        return false;
    }

}
