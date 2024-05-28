#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


/*TO-DO
    hall linking - aka suicide
 
 */

[ExecuteAlways]
public class MapEditorLogic : BASICSingleton<MapEditorLogic>
{
    [SerializeField] private UnityEngine.Object emptyTilePrefab;
    [SerializeField] private Material emptyTileText;
    [SerializeField] private Material aiTile;
    [SerializeField] private Tag newTileTag;
    public Tag aiTag; 

    public Vector3 mapSize = new Vector3(50f, 1f, 50f);
    public string mapName = "Map Name";

    public int currentLayer = 1;
    public int currentTileProfileInIndex; 

    public ObjectProfile2 currentObjectProfile; 
    public UnityEngine.Object currentlySelectedTile;

    public GameObject[] selectedTiles;
    public Vector3 lastRotation;
    public Vector3 exactRotation;

    public bool addExactRotation;
    public bool autoBuild;

    public static Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    public bool createMap()
    {
        try
        {
            GameObject mapParent = new GameObject(mapName);
            mapParent.transform.localPosition = new Vector3(0, 0, 0);
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    for (int z = 0; z < mapSize.z; z++)
                    {
                        GameObject tile = (GameObject)Instantiate(emptyTilePrefab);
                        tile.transform.position = new Vector3(x * 10, y * 10, z * 10);
                        tile.transform.parent = mapParent.transform;
                        tile.name = $"Tile_{x}_{y}_{z}";
                        Tags mapTileTag = tile.AddComponent<Tags>();
                        mapTileTag.SetTags(newTileTag); 
                    }
                }
            }

            return true; 
        }
        catch
        {
            return false;
        }
    }

    
    #region MAPEDITOR
    GameObject checkIfTile(GameObject obj)
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

    private bool subbed;
    public bool aiProfile;

    private void Update()
    {
       if (autoBuild && !subbed)
       {
         Selection.selectionChanged += selectonChanged;
         subbed = true;
       } else if (!autoBuild && subbed)
       {
            Selection.selectionChanged -= selectonChanged;
            subbed = false;
       }
    }

    void selectonChanged()
    {
        if (autoBuild)
        {
            List<GameObject> selectedTilesList = new List<GameObject>();
            foreach (var go in Selection.gameObjects)
            {
                Tags tags = go.GetComponent<Tags>();
                if (tags != null && tags.GetTags()[0] == newTileTag)
                {
                    selectedTilesList.Add(go);
                }
                else
                {
                    GameObject parentTile = checkIfTile(go);
                    if (parentTile != null)
                    {
                        selectedTilesList.Add(parentTile);
                    }
                }
            }

            GameObject[] tiles = selectedTiles = selectedTilesList.ToArray();

            foreach (GameObject tile in tiles)
            {
                placeTileAt(tile);
            }
        }
    }

    public void getSelectedTiles()
    {
        List<GameObject> selectedTilesList = new List<GameObject>();
        foreach (var go in Selection.gameObjects)
        {
            Tags tags = go.GetComponent<Tags>();
            if (tags != null && tags.GetTags()[0] == newTileTag)
            {
                selectedTilesList.Add(go);
            }
            else
            {
                GameObject parentTile = checkIfTile(go);
                if (parentTile != null)
                {
                    selectedTilesList.Add(parentTile);
                }
            }
        }

        selectedTiles = selectedTilesList.ToArray();
    }
    public void newTile(ObjectProfile2 newProfile)
    {
        if (newProfile != Instance.currentObjectProfile || (newProfile != null && newProfile.profiledObjects.Length < currentTileProfileInIndex))
        {
            currentTileProfileInIndex = 0;
            currentlySelectedTile = null;
        }

        currentObjectProfile = newProfile;
    }

    public void placeTileAt(GameObject tileSpot)
    {
        if (tileSpot != null)
        {
            Transform layerObject = null;
            if (aiProfile)
            {
                layerObject = tileSpot.transform.Find("AIPath_" + currentLayer);
            }
            else
            {
                layerObject = tileSpot.transform.Find("Layer_" + currentLayer);
            }

            if (layerObject == null)
                {
                if (aiProfile)
                {
                    layerObject = new GameObject("AIPath_" + currentLayer).transform;
                }
                else
                {
                    layerObject = new GameObject("Layer_" + currentLayer).transform;
                }
                layerObject.parent = tileSpot.transform;
                    
                    
                    layerObject.localPosition = Vector3.zero;
                if (aiProfile)
                {
                    layerObject.localEulerAngles = new Vector3(90, 0, 0);
                }
                    layerObject.localRotation = Quaternion.identity;
                }

                foreach (MeshRenderer renderer in tileSpot.GetComponentsInChildren<MeshRenderer>())
                {
                    if (renderer != null && renderer.sharedMaterial == emptyTileText)
                    {
                        renderer.enabled = false;
                        break;
                    }
                }

                if (layerObject.childCount > 0)
                {
                    Transform child = layerObject.GetChild(0);

                    if (layerObject.transform.childCount > 0)
                    {
                        DestroyImmediate(layerObject.transform.GetChild(0).gameObject);
                    }
                }

                GameObject newTileContents = (GameObject)PrefabUtility.InstantiatePrefab(currentlySelectedTile);
                newTileContents.transform.parent = layerObject.transform;
                newTileContents.transform.localPosition = Vector3.zero;
                if (!aiProfile)
                {
                    newTileContents.transform.localEulerAngles = lastRotation;
                }
                else
                {
                    Tags newTagObject = newTileContents.AddComponent<Tags>();
                    newTagObject.SetTags(aiTag); 
                }

                connectNeighbouringTiles(tileSpot.transform);

                Selection.objects = selectedTiles.Cast<UnityEngine.Object>().ToArray();
                EditorGUIUtility.PingObject(newTileContents);
        }
    }

    //i am a disappointment
    private void connectNeighbouringTiles(Transform tilesParent)
    {
        //REMEMBER ARRAYS START AT 0!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        string[] centrePos = tilesParent.name.Split('_');

        string[] leftPos = tilesParent.name.Split('_');
        leftPos[3] = (int.Parse(centrePos[3]) -1).ToString();

        string[] rightPos = tilesParent.name.Split('_');
        rightPos[3] = (int.Parse(centrePos[2]) +1).ToString();

        string[] upPos = tilesParent.name.Split('_');
        upPos[1] = (int.Parse(centrePos[1]) +1).ToString();

        string[] downPos = tilesParent.name.Split('_');
        downPos[1] = (int.Parse(centrePos[1]) -1).ToString();

        GameObject[] neighbouringTiles = new GameObject[4];
        neighbouringTiles[0] = tilesParent.parent.Find($"Tile_{leftPos[1]}_{leftPos[2]}_{leftPos[3]}").gameObject;
        neighbouringTiles[1] = tilesParent.parent.Find($"Tile_{rightPos[1]}_{rightPos[2]}_{rightPos[3]}").gameObject;
        neighbouringTiles[2] = tilesParent.parent.Find($"Tile_{upPos[1]}_{upPos[2]}_{upPos[3]}").gameObject;
        neighbouringTiles[3] = tilesParent.parent.Find($"Tile_{downPos[1]}_{downPos[2]}_{downPos[3]}").gameObject;

        foreach (GameObject fnf in neighbouringTiles)
        {
            Debug.Log($"the tile: {fnf.name} should be a neighbour, and isEmpty is: {checkIfAllLayersAreEmpty_IfYoureReadingThisAndYouEditMapMaidYouBetterCreateAPROnGithub(fnf)}");
        }
    }

    GameObject findAllWalls(GameObject obj)
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


    public void placeTile()
    {
        if (currentlySelectedTile != null)
        {
            foreach (var tile in selectedTiles)
            {
                Transform layerObject = null;
                if (aiProfile)
                {
                    layerObject = tile.transform.Find("AIPath_" + currentLayer);
                } else
                {
                    layerObject = tile.transform.Find("Layer_" + currentLayer);
                }

                if (layerObject == null)
                {
                    if (aiProfile)
                    {
                        layerObject = new GameObject("AIPath_" + currentLayer).transform;
                    }
                    else
                    {
                        layerObject = new GameObject("Layer_" + currentLayer).transform;
                    }

                    layerObject.parent = tile.transform;
                    

                    layerObject.localPosition = Vector3.zero;
                }

                foreach (MeshRenderer renderer in tile.GetComponentsInChildren<MeshRenderer>())
                {
                    if (renderer != null && renderer.sharedMaterial == emptyTileText)
                    {
                        renderer.enabled = false;
                        break; 
                    }
                }

                if (layerObject.childCount > 0)
                {
                    if (layerObject.transform.childCount > 0)
                    {
                        DestroyImmediate(layerObject.transform.GetChild(0).gameObject);
                    }
                }

                GameObject newTileContents = (GameObject)PrefabUtility.InstantiatePrefab(currentlySelectedTile);
                newTileContents.transform.parent = layerObject.transform;
                newTileContents.transform.localPosition = Vector3.zero;
                if (!aiProfile)
                {
                    newTileContents.transform.localEulerAngles = lastRotation;
                } else
                {
                    Tags newTagObject = newTileContents.AddComponent<Tags>();
                    newTagObject.SetTags(aiTag);
                }
                Selection.objects = selectedTiles.Cast<UnityEngine.Object>().ToArray();
            }
        }
    }

    public void clearTile()
    {
        if (currentlySelectedTile != null)
        {
            foreach (var tile in selectedTiles)
            {
                Transform tileParent = checkIfTile(tile).transform;

                Transform layerObject = tile.transform.Find("Layer_" + currentLayer);
                if (aiProfile)
                {
                    layerObject = tile.transform.Find("AIPath_" + currentLayer);
                }
                if (layerObject != null && layerObject.childCount > 0)
                {
                    DestroyImmediate(layerObject.GetChild(0).gameObject);
                }

                if (checkIfAllLayersAreEmpty_IfYoureReadingThisAndYouEditMapMaidYouBetterCreateAPROnGithub(tileParent.gameObject))
                {
                    foreach (MeshRenderer renderer in tile.GetComponentsInChildren<MeshRenderer>())
                    {
                        if (renderer != null && renderer.sharedMaterial == emptyTileText)
                        {
                            renderer.enabled = true;
                            break;
                        }

                    }
                }

                Selection.objects = selectedTiles.Cast<UnityEngine.Object>().ToArray();
                EditorGUIUtility.PingObject(tile);
            }
        }
    }

    private bool checkIfAllLayersAreEmpty_IfYoureReadingThisAndYouEditMapMaidYouBetterCreateAPROnGithub(GameObject parentTile)
    {
        List<GameObject> layerObjects = new List<GameObject>();

        foreach (Transform go in parentTile.GetComponentsInChildren<Transform>())
        {
            if (go.gameObject != null && go.name.Contains("Layer"))
            {
                layerObjects.Add(go.gameObject);
            }
        }

        foreach (GameObject go in layerObjects)
        {
            if (go.transform.childCount > 0)
            {
                return false; 
            }
        }

        return true;
    }

    public void rotateTile(Vector3 rotation, bool add)
    {
        foreach (var tile in selectedTiles)
        {
            Transform layerObject = tile.transform.Find("Layer_" + currentLayer);
            if (aiProfile)
            {
                layerObject = tile.transform.Find("AIPath_" + currentLayer);
            }
            if (layerObject != null && layerObject.childCount > 0)
            {
                Vector3 currentRotation = layerObject.GetChild(0).transform.eulerAngles; 
                if (add)
                {
                    currentRotation += rotation;
                } else
                {
                    currentRotation = rotation;
                }
                layerObject.GetChild(0).transform.eulerAngles = currentRotation;

                if (!aiProfile)
                {
                    lastRotation = currentRotation;
                }
            }
        }
    }

    #endregion

    public void optimiseMap()
    {
        List<GameObject> tiles = new List<GameObject>();

        foreach (Tags tag in FindObjectsOfType<Tags>())
        {
            if (tag != null && tag.HasTag(newTileTag))
            {
                tiles.Add(tag.gameObject);          
            }
        }

        GameObject[] tilesArray = tiles.ToArray();
        foreach (GameObject tile in tilesArray)
        {
            bool shouldDestroyTile = true;

            void CheckLayer(Transform layerObject)
            {
                MeshRenderer meshRenderer = layerObject.GetComponentInChildren<MeshRenderer>();
                if (meshRenderer == null || meshRenderer.sharedMaterial.mainTexture != emptyTileText.mainTexture)
                {
                    shouldDestroyTile = false;
                    return;
                }

                for (int i = 0; i < layerObject.childCount; i++)
                {
                    Transform childLayer = layerObject.GetChild(i);
                    if (childLayer.name.StartsWith("Layer_"))
                    {
                        CheckLayer(childLayer);
                        if (!shouldDestroyTile) return;
                    }
                }
            }

            for (int i = 0; i < tile.transform.childCount; i++)
            {
                Transform layerObject = tile.transform.GetChild(i);
                CheckLayer(layerObject);
                if (!shouldDestroyTile) break;
            }

            if (shouldDestroyTile)
            {
                DestroyImmediate(tile);
            }
        }
    }
}
#endif