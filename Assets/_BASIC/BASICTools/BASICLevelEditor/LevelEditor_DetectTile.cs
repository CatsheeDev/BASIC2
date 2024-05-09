using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class LevelEditor_DetectTile : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask ignoreUILayer; // Assign this in the Inspector

    private GameObject hoveredObject = null;

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        if (EventSystem.current.IsPointerOverGameObject())
                return; 

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hoveredObject != hit.collider.gameObject && LevelEditorLogic.Instance.checkIfTile(hit.collider.gameObject) != null)
            {
                if (Input.GetMouseButton(0))
                {
                    LevelEditorLogic.Instance.registerSelectedTile(LevelEditorLogic.Instance.checkIfTile(hit.collider.gameObject));
                }
            }
        }
        else
        {
            hoveredObject = null;
        }
    }
}
