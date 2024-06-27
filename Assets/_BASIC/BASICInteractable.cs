using UnityEngine;

public class BASICInteractable : MonoBehaviour
{
    [SerializeField] private float interactDistance = 10;

    /// <returns>Returns true if player has clicked, timeScale is not 0, the raycast to object has hit and is within interactDistance</returns>
    protected bool Interacted()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, base.transform.position) < interactDistance)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.gameObject == gameObject))
            {
                return true;
            }
        }

        return false;
    }
}
