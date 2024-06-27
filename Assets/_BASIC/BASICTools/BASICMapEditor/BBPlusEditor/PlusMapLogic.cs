#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PlusMapLogic : BASICSingleton<PlusMapLogic>
{
    private static Tag PlusMapTag; 
    private static Tag PlusTileTag;

    private static UnityEngine.Object EmptyTile;
    private static UnityEngine.Object FullTile;

    private bool Loaded;
    public bool DoSelection;

    private int NorthValue = 1;
    private int EastValue = 2;
    private int SouthValue = 4; 
    private int WestValue = 8;

    //not static to init the singleton
    public void CreateMap(Vector3 MapSize)
    {
        InitResources(); 
        GameObject mapParent = new("RoomTest");
        mapParent.transform.position = Vector3.zero;

        Tags mapTileTag = mapParent.AddComponent<Tags>();
        mapTileTag.SetTags(PlusMapTag);

        for (int x = 0; x < MapSize.x; x++) { for (int y = 0; y < MapSize.y; y++) { for (int z = 0; z < MapSize.z; z++) {
            GameObject NextTile = Instantiate(EmptyTile) as GameObject;
            NextTile.transform.position = new Vector3(x * 10, y * 10, z * 10);
            
            NextTile.transform.parent = mapParent.transform;
            NextTile.name = $"Tile_{x}_{y}_{z}";
            NextTile.AddComponent<Tags>().SetTags(PlusTileTag);
            NextTile.AddComponent<PlusTile>().pos = new IntVector2(x, z); 
        } } }
    }

    private bool CheckForPlusMap(GameObject CurrentlySelected, out GameObject Returner)
    {
        Returner = CurrentlySelected; 
        Transform ObjectTransform = CurrentlySelected.transform;

        while (ObjectTransform != null)
        {
            Tags tags = ObjectTransform.GetComponent<Tags>();
            if (tags != null && tags.GetTags()[0] == PlusMapTag)
            {
                return true;
            }
            ObjectTransform = ObjectTransform.parent;
            Returner = ObjectTransform.gameObject;
        }

        return false; 
    }

    private bool CheckForPlusTile(GameObject CurrentlySelected, out GameObject Returner)
    {
        Returner = CurrentlySelected;
        Transform ObjectTransform = CurrentlySelected.transform;

        while (ObjectTransform != null)
        {
            Tags tags = ObjectTransform.GetComponent<Tags>();
            if (tags != null && tags.GetTags()[0] == PlusTileTag)
            {
                return true;
            }
            Returner = ObjectTransform.gameObject;
            ObjectTransform = ObjectTransform.parent;
        }

        return false;
    }

    private void InitResources()
    {
        if (Loaded == true) return; 

        Debug.Log("init selectino");
        PlusMapTag = Resources.Load<Tag>("PlusResources_MapTag");
        PlusTileTag = Resources.Load<Tag>("PlusResources_TileTag");
        EmptyTile = Resources.Load<UnityEngine.Object>("PlusResources_EmptyTile");
        FullTile = Resources.Load<UnityEngine.Object>("PlusResources_FullTile");
        Loaded = true; 
    }
    private void Start()
    {
        Selection.selectionChanged += OnSelection;
        InitResources(); 
    }

    public void KillLogic()
    {
        Selection.selectionChanged -= OnSelection;
        DestroyImmediate(this.gameObject); 
    }

    public void OnSelection()
    {
        if (Instance == null || !DoSelection) return;
        GameObject CurrentlySelected = Selection.activeGameObject;
        if (CheckForPlusTile(CurrentlySelected, out CurrentlySelected))
        {
            PlaceTile(CurrentlySelected);
        }
    }

    [RuntimeInitializeOnLoadMethod]
    private static void Refresh()
    {
        Instance.Loaded = false; 
    }

    public void PlaceTile(GameObject Tile)
    {
        if (Tile.transform.childCount > 0 && Tile.transform.GetChild(0).name == "PlusResources_FullTile")
        {
            DisconnectWalls(Tile.transform.GetChild(0));
            DestroyImmediate(Tile.transform.GetChild(0).gameObject);
            return; 
        }

        GameObject NewTile = PrefabUtility.InstantiatePrefab(FullTile) as GameObject;
        NewTile.transform.parent = Tile.transform;
        NewTile.transform.localPosition = Vector3.zero;
        NewTile.transform.localEulerAngles = Vector3.zero;

        ConnectWalls(NewTile.transform); 
    }

    public void CalculateValue(GameObject Tile, bool isAddingWalls)
    {
        int ParentValue = 0;
        foreach (Transform child in Tile.GetComponentsInChildren<Transform>(false))
        {
            if (!child.name.Contains("Wall")) continue;

            if (child.gameObject.activeSelf) // Only consider active walls
            {
                if (child.name.Contains("North"))
                {
                    ParentValue |= NorthValue;
                }
                if (child.name.Contains("East"))
                {
                    ParentValue |= EastValue;
                }
                if (child.name.Contains("South"))
                {
                    ParentValue |= SouthValue;
                }
                if (child.name.Contains("West"))
                {
                    ParentValue |= WestValue;
                }
            }
        }

        Debug.Log($"Original Value: {Tile.transform.parent.GetComponentInParent<PlusTile>().type}");
        Debug.Log($"Final ParentValue: {ParentValue}");
        Tile.transform.parent.GetComponentInParent<PlusTile>().type = ParentValue;
    }



    public void ConnectWalls(Transform Tile)
    {
        List<Transform> Walls = new();

        foreach (Transform child in Tile.GetComponentsInChildren<Transform>())
        {
            if (child.name.Contains("Wall"))
            {
                Walls.Add(child);
            }
        }

        GameObject otherTile = null; 
        foreach (Transform wall in Walls)
        {
            Collider[] hitColliders = Physics.OverlapSphere(wall.position, .1f);

            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.name.Contains("Wall") && collider.transform != wall)
                {
                    otherTile = collider.transform.parent.gameObject; 
                    wall.gameObject.GetComponent<WallLink>().Link = collider.transform;
                    collider.gameObject.GetComponent<WallLink>().Link = wall.transform;

                    // Deactivate linked walls
                    collider.gameObject.SetActive(false);
                    wall.gameObject.SetActive(false);
                    Debug.Log($"Linked {wall.name} with {collider.name}");
                }
            }
        }

        if (otherTile != null)
        {
            CalculateValue(otherTile.gameObject, false);
        }
        CalculateValue(Tile.gameObject, false);
    }

    public void DisconnectWalls(Transform tile)
    {
        List<Transform> Walls = new();

        foreach (Transform child in tile.GetComponentsInChildren<Transform>(true))
        {
            if (child.name.Contains("Wall"))
            {
                Walls.Add(child);
            }
        }

        foreach (Transform wall in Walls)
        {
            if (wall.TryGetComponent(out WallLink linker))
            {
                if (linker.Link != null)
                {
                    linker.Link.gameObject.SetActive(true);
                    CalculateValue(linker.Link.parent.gameObject, true);

                    Debug.Log($"Unlinked {wall.name} from {linker.Link.name}");
                }
            }
        }

        CalculateValue(tile.gameObject, true);
    }
}
#endif