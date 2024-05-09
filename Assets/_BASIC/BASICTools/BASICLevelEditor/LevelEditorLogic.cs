using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static LevelEditor_Setings;

public class LevelEditorLogic : Singleton<LevelEditorLogic>
{
    [SerializeField] private Tag newTileTag;
    [HideInInspector][SerializeField] private GameObject currentTile;
    [SerializeField] private Material emptyTileText;
    [SerializeField] private LevelEditor_Setings 設定;
    [SerializeField] private GameObject テンプレート;

    [SerializeField] private Texture2D add;
    [SerializeField] private Texture2D remove;

    private GameObject DEBUG_CURRENTTILE;

    private int layer;


    private enum editMode
    {
        add, 
        remove
    }

    private editMode currentMode; 

    private void Awake()
    {
        foreach (objectFolder objec in 設定.folders)
        {
            Transform newThing = Instantiate(テンプレート).transform;
            newThing.SetParent(テンプレート.transform.parent, false);
            newThing.gameObject.SetActive(true);
            newThing.GetComponentInChildren<Button>().onClick.AddListener(() => UpdateFolderState(objec, newThing));
            newThing.Find("IMG").GetComponent<Image>().sprite = objec.preview;
        }
    }

    public void UpdateFolderState(objectFolder currObjs, Transform itchy)
    {
        currObjs.openFolder = !currObjs.openFolder;
        UpdateFolderUI(currObjs, itchy);
    }

    private void UpdateFolderUI(objectFolder currObjs, Transform itchy)
    {
        if (currObjs.openFolder)
        {
            ShowTilesets(currObjs, itchy);
        }
        else
        {
            HideTilesets(itchy);
        }
    }

    private void ShowTilesets(objectFolder currObjs, Transform itchy)
    {
        int index = 0;
        ClearTilePlaceholders(itchy);

        foreach (ObjectProfie2Object objm in currObjs.objects.profiledObjects)
        {
            index += 200;
            GameObject buttno = InstantiateTileButton(itchy, objm.icon, objm, index);
        }
    }

    private void HideTilesets(Transform itchy)
    {
        ClearTilePlaceholders(itchy);
    }

    private void ClearTilePlaceholders(Transform itchy)
    {
        foreach (Transform go in itchy.GetComponentsInChildren<Transform>())
        {
            if (go.gameObject.name == "TilePlaceholder(Clone)")
            {
                Destroy(go.gameObject);
            }
        }
    }

    private GameObject InstantiateTileButton(Transform parent, Sprite icon, ObjectProfie2Object prefab, int index)
    {
        GameObject buttno = Instantiate(parent.Find("TilePlaceholder").gameObject);
        buttno.transform.SetParent(parent, false);
        buttno.SetActive(true);
        buttno.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => setNewDreamTile((GameObject)prefab.prefab));
        buttno.transform.Find("IMG").GetComponent<Image>().sprite = icon;
        buttno.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = prefab.name;

        RectTransform localWorld = buttno.GetComponent<RectTransform>();
        localWorld.anchoredPosition = Vector2.zero;
        Tween.AnchoredPosition(localWorld, new Vector3(0, 0, 0), new Vector3(index, 0, 0), 0.25f, 0);

        return buttno;
    }

    private bool setNewDreamTile(GameObject newTile)
    {
        try
        {
            DEBUG_CURRENTTILE = newTile;
            Debug.Log("hey"); 
            return true; 
        } catch
        {
            return false;
        }
    }

    //EDITOR STUFF
    public void changeMode(bool enabled)
    {
        if (enabled)
        {
            currentMode = editMode.add;
            return; 
        }

        currentMode = editMode.remove;
    }
    //LOGIC
    public GameObject checkIfTile(GameObject obj)
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

    public bool isEmptyTile(GameObject obj)
    {
        if (obj.GetComponentInChildren<MeshRenderer>().gameObject.activeSelf)
        {
            return false; 
        }
        return true;
    }

    private void placeTile(GameObject tile)
    {
        if (DEBUG_CURRENTTILE == null)
            return;

        Transform layerObject = tile.transform.Find("Layer_" + layer);

        if (layerObject == null)
        {
            layerObject = new GameObject($"Layer_{layer}").transform;
            layerObject.parent = tile.transform;


            layerObject.localPosition = Vector3.zero;
            layerObject.localRotation = Quaternion.identity;
        }

        if (layerObject.childCount > 0)
        {
            if (layerObject.transform.childCount > 0)
            {
                DestroyImmediate(layerObject.transform.GetChild(0).gameObject);
            }
        }

        if (currentMode == editMode.remove)
            return; 

        foreach (MeshRenderer renderer in tile.GetComponentsInChildren<MeshRenderer>())
        {
            if (renderer != null && renderer.sharedMaterial == emptyTileText)
            {
                renderer.enabled = false;
                break;
            }
        }

        GameObject newTileContents = (GameObject)Instantiate(DEBUG_CURRENTTILE); 
        newTileContents.transform.parent = layerObject.transform;
        newTileContents.transform.localPosition = Vector3.zero;
        newTileContents.transform.localEulerAngles = Vector3.zero;
    }

    public void registerSelectedTile(GameObject tile)
    {
        currentTile = tile;
        if (!isEmptyTile(currentTile))
        {
            placeTile(currentTile);
            Debug.Log("hi"); 
        }
    }
}
