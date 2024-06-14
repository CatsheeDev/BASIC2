using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;

public class MinimapParentController : MonoBehaviour
{
    private Transform playerCam;
    private PlayerScript player;

    private Transform lastCurrentTile;
    [SerializeField] private GameObject tileTemplate;
    [SerializeField] private float multi; 

    private List<Transform> indexedTiles = new(); 
    
    private void Start()
    {
        playerCam = GameControllerScript.Instance.cameraTransform;
        player = GameControllerScript.Instance.player;
        lastCurrentTile = MapMaidAPI.GetCurrentPlayersTile();
        CreateNextTile(lastCurrentTile); 
    }

    private void CreateNextTile(Transform tile)
    {
        if (tile == null || tileTemplate == null)
        {
            Debug.LogError("TILE, TEMPLATE OR CURRECT WAS NULL, COULD NOT CREATE NEXT MAP TILE");
            return; 
        }

        GameObject newTile = Instantiate(tileTemplate, tile, false);

        newTile.name = "Mapper";
        newTile.SetActive(true); 
    }

    private void LateUpdate()
    {
        Transform temptile = MapMaidAPI.GetCurrentPlayersTile();

        if (temptile == null) return;
        if (temptile != lastCurrentTile && !indexedTiles.Contains(temptile))
        {
            lastCurrentTile = temptile; 
            CreateNextTile(temptile);
        }
    }
}
