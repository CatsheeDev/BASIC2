using UnityEngine;

public static class MapMaidAPI
{
    private static readonly Tag newTileTag = Resources.Load<Tag>("MapTile"); 
    public static GameObject CheckForTileParent(GameObject obj)
    {

        if (obj != null)
        {

            Transform currentTransform = obj.transform;


            while (currentTransform != null)
            {
                Tags tags = currentTransform.GetComponent<Tags>();
                if (tags != null && tags.GetTags()[0] == newTileTag)
                {
                    return currentTransform.gameObject;
                }
                currentTransform = currentTransform.parent;
            }
        }

        return null;
    }

    public static Transform GetCurrentPlayersTile()
    {
        RaycastHit hit;
        Vector3 startPosition = GameControllerScript.Instance.playerTransform.position;
        Vector3 direction = -Vector3.up;
        if (Physics.Raycast(startPosition, direction, out hit, 5))
        {
            GameObject tileParent = CheckForTileParent(hit.collider.gameObject);
            if (tileParent != null)
            {
                return tileParent.transform;
            }
        }

        return null;
    }

    public static Transform FindTileAt(Transform map, int x, int y, int z)
    {
        try
        {
            return map.Find($"Tile_{x}_{y}_{z}");
        } catch
        {
            Debug.LogError("Invalid map paramater or invalid coordinates");
            return null;
        }
    }
}
