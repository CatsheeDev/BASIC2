using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameControllerScript : Singleton<GameControllerScript>
{
	private GameObject itemHolder;

	[HideInInspector] public List<GameObject> itemObjects = new(); 

	private void Start()
	{
		this.cullingMask = this.camera.cullingMask; 
		this.audioDevice = base.GetComponent<AudioSource>(); 
		this.mode = PlayerPrefs.GetString("CurrentMode"); 
		if (this.mode == "endless") 
		{
			this.baldiScrpt.endless = true; 
		}
		this.schoolMusic.Play(); 
		this.LockMouse(); 
		this.itemSelected = 0;
        this.itemSelect.anchoredPosition = this.itemSlot[itemSelected].GetComponent<RectTransform>().anchoredPosition;
        this.gameOverDelay = 0.5f;

		if (settingsProfile == null)
		{
			settingsProfile = ScriptableObject.CreateInstance<BASICDecompProfile>();

            Debug.LogError("BAISIC // GAMECONTROLLER // NO SETTINGS PROFILE");
        }

		if (itemProfile == null)
		{
			itemProfile = ScriptableObject.CreateInstance<ItemProfile2>();
			Array.Resize(ref itemProfile.items, 1);
			itemProfile.items[0].Value = 0;
			itemProfile.items[0].Name = "Nothing";

			Debug.LogError("BAISIC // GAMECONTROLLER // NO ITEM PROFILE");
		}
		this.debugMode = settingsProfile.DebugMode;
		this.notebooks = settingsProfile.startingNotebooks;
		this.failedNotebooks = settingsProfile.startingFailedNotebooks;

		UpdateNotebookCount(); 
        SetupItems(); 
    }

	void SetupItems()
	{
		itemHolder = new GameObject("Items"); 
		itemHolder.transform.parent = transform;

        ItemInfo[] itemData = itemProfile.items;

        foreach (ItemInfo item in itemData)
        {
            string scriptName = item.Name;
            string fullScriptName = $"{scriptName}";

            Type scriptType = Type.GetType(fullScriptName);
            if (scriptType != null)
            {
                if (!this.gameObject.GetComponent(scriptType))
                {
					GameObject hold = new GameObject(scriptName);
					hold.transform.parent = itemHolder.transform;
                    hold.AddComponent(scriptType);
					itemObjects.Add(hold); 
                }
            }
            else
            {
                Debug.LogError($"Script {scriptName} not found.");
            }
        }
    }

	private void Update()
	{
		if (!this.learningActive)
		{
			if (Input.GetButtonDown("Pause"))
			{
				if (!this.gamePaused)
				{
					this.PauseGame();
				}
				else
				{
					this.UnpauseGame();
				}
			}
			if (Input.GetKeyDown(KeyCode.Y) & this.gamePaused)
			{
				this.ExitGame();
			}
			else if (Input.GetKeyDown(KeyCode.N) & this.gamePaused)
			{
				this.UnpauseGame();
			}
			if (!this.gamePaused & Time.timeScale != 1f)
			{
				Time.timeScale = 1f;
			}
			if (Input.GetMouseButtonDown(1) && Time.timeScale != 0f)
			{
				this.UseItem();
			}
			if ((Input.GetAxis("Mouse ScrollWheel") > 0f && Time.timeScale != 0f))
			{
				this.ModifyItemSelection(-1);
			}
			else if ((Input.GetAxis("Mouse ScrollWheel") < 0f && Time.timeScale != 0f))
			{
				this.ModifyItemSelection(1);
			}
			if (Time.timeScale != 0f)
			{
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					this.itemSelected = 0;
					this.UpdateItemSelection();
				}
				else if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					this.itemSelected = 1;
					this.UpdateItemSelection();
				}
				else if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					this.itemSelected = 2;
					this.UpdateItemSelection();
				}
			}
		}
		else
		{
			if (Time.timeScale != 0f)
			{
				Time.timeScale = 0f;
			}
		}
		if (this.player.stamina < 0f & !this.warning.activeSelf)
		{
			this.warning.SetActive(true); 
		}
		else if (this.player.stamina > 0f & this.warning.activeSelf)
		{
			this.warning.SetActive(false); 
		}
		if (this.player.gameOver)
		{
			if (this.mode == "endless" && this.notebooks > PlayerPrefs.GetInt("HighBooks") && !this.highScoreText.activeSelf)
			{
				this.highScoreText.SetActive(true);
			}
			Time.timeScale = 0f;
			this.gameOverDelay -= Time.unscaledDeltaTime * 0.5f;
			this.camera.farClipPlane = this.gameOverDelay * 400f; 
			this.audioDevice.PlayOneShot(this.aud_buzz);
			if (PlayerPrefs.GetInt("Rumble") == 1)
			{

			}
			if (this.gameOverDelay <= 0f)
			{
				if (this.mode == "endless")
				{
					if (this.notebooks > PlayerPrefs.GetInt("HighBooks"))
					{
						PlayerPrefs.SetInt("HighBooks", this.notebooks);
					}
					PlayerPrefs.SetInt("CurrentBooks", this.notebooks);
				}
				Time.timeScale = 1f;
				SceneManager.LoadScene("GameOver");
			}
		}
		if (this.finaleMode && !this.audioDevice.isPlaying && this.exitsReached == 3)
		{
			this.audioDevice.clip = this.aud_MachineLoop;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
	}

	
	private void UpdateNotebookCount()
	{
		if (this.mode == "story")
		{
			this.notebookCount.text = this.notebooks.ToString() + $"/{settingsProfile.maxNotebooks} Notebooks";
		}
		else
		{
			this.notebookCount.text = this.notebooks.ToString() + " Notebooks";
		}
		if (this.notebooks == settingsProfile.maxNotebooks & this.mode == "story")
		{
			this.ActivateFinaleMode();
		}
	}

	
	public void CollectNotebook()
	{
		this.notebooks++;
		this.UpdateNotebookCount();
	}

    public void CollectNotebook(bool ye)
    {
        this.UpdateNotebookCount();
    }


    public void LockMouse()
	{
		if (!this.learningActive)
		{
			this.cursorController.LockCursor(); 
			this.mouseLocked = true;
			this.reticle.SetActive(true);
		}
	}

	
	public void UnlockMouse()
	{
		this.cursorController.UnlockCursor(); 
		this.mouseLocked = false;
		this.reticle.SetActive(false);
	}

	
	public void PauseGame()
	{
		if (!this.learningActive)
		{
			{
				this.UnlockMouse();
			}
			Time.timeScale = 0f;
			this.gamePaused = true;
			this.pauseMenu.SetActive(true);
		}
	}

	
	public void ExitGame()
	{
		SceneManager.LoadScene("MainMenu");
	}

	
	public void UnpauseGame()
	{
		Time.timeScale = 1f;
		this.gamePaused = false;
		this.pauseMenu.SetActive(false);
		this.LockMouse();
	}

	
	public void ActivateSpoopMode()
	{
		this.spoopMode = true; 
		foreach (EntranceScript entr in entrances) 
		{ 
			entr.Lower(); 
		}
		
		foreach (BASICNPC npc in FindObjectsOfType<BASICNPC>(true))
		{
			npc.gameObject.SetActive(npc.shouldSpoop); 
		}

		if (settingsProfile.YCTP)
		{
			this.audioDevice.PlayOneShot(this.aud_Hang);
		}
		else if (!settingsProfile.YCTP && this.notebooks == settingsProfile.maxNotebooks & this.mode == "story")
        {
            this.audioDevice.PlayOneShot(this.aud_AllNotebooks, 0.8f);
        }

		this.learnMusic.Stop(); 
		this.schoolMusic.Stop();
	}

	
	public void ActivateFinaleMode()
	{
		this.finaleMode = true;
        foreach (EntranceScript entr in entrances) { entr.Raise(); }
    }

	
	public void GetAngry(float value) 
	{
		if (!this.spoopMode)
		{
			this.ActivateSpoopMode();
		}
		this.baldiScrpt.GetAngry(value);
	}

	
	public void ActivateLearningGame()
	{
		
		this.learningActive = true;
		this.UnlockMouse(); 
		this.tutorBaldi.Stop(); 
		if (!this.spoopMode) 
		{
			this.schoolMusic.Stop(); 
			this.learnMusic.Play();
		}
	}

	
	public void DeactivateLearningGame(GameObject subject)
	{
		this.camera.cullingMask = this.cullingMask; 
		this.learningActive = false;
		UnityEngine.Object.Destroy(subject);
		this.LockMouse(); 
		if (this.player.stamina < 100f) 
		{
			this.player.stamina = 100f;
		}
		if (!this.spoopMode) 
		{
			this.schoolMusic.Play();
			this.learnMusic.Stop();
		}
		if (this.notebooks == 1 & !this.spoopMode) 
		{
			this.quarter.SetActive(true);
			this.tutorBaldi.PlayOneShot(this.aud_Prize);
		}
		else if (this.notebooks == settingsProfile.maxNotebooks & this.mode == "story") 
		{
			this.audioDevice.PlayOneShot(this.aud_AllNotebooks, 0.8f);
		}
	}


    /*	private void IncreaseItemSelection()
        {
            this.itemSelected++;
            if (this.itemSelected > 2)
            {
                this.itemSelected = 0;
            }
            this.itemSelect.anchoredPosition = this.itemSlot[itemSelected].GetComponent<RectTransform>().anchoredPosition; 
            //this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f); 
            this.UpdateItemName();
        }


        private void DecreaseItemSelection()
        {
            this.itemSelected--;
            if (this.itemSelected < 0)
            {
                this.itemSelected = 2;
            }
            this.itemSelect.anchoredPosition = this.itemSlot[itemSelected].GetComponent<RectTransform>().anchoredPosition;
            //this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f); 
            this.UpdateItemName();
        }*/


    //BASIC IEM SECTION
    public void CollectItem_BASIC(int itemId, PickupScript itemPickup)
	{
        int emptySlotIndex = -1;
        for (int i = 0; i < this.item.Length; i++)
        {
            if (this.item[i] == 0)
            {
                emptySlotIndex = i;
                break;
            }
        }
        if (emptySlotIndex == -1 && settingsProfile.swapItems)
		{
			itemPickup.gameObject.SetActive(true); 
			itemPickup.itemIndex = (BASICItem)item[itemSelected];

			Texture2D newTex = itemProfile.items[item[itemSelected]].Icon; 
			itemPickup.GetComponentInChildren<SpriteRenderer>().sprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
        }

        int slotIndex = emptySlotIndex != -1 ? emptySlotIndex : this.itemSelected;

        this.item[slotIndex] = itemId;
        this.itemSlot[slotIndex].texture = itemProfile.items[itemId].Icon;

		InteractItem(itemProfile.items[itemId], false);
        this.UpdateItemName();
    }

    private void InteractItem(ItemInfo itemInfo, bool use)
    {
        string scriptName = itemInfo.Name;
		Component scriptComponent = ItemEditorAPI.FindItem((BASICItem)itemInfo.Value).GetComponent(scriptName); 

        if (scriptComponent != null && scriptComponent is ItemBase)
        {
			if (use)
			{
                MethodInfo useMethod = typeof(ItemBase).GetMethod("Use");

                if (useMethod != null)
                {
                    useMethod.Invoke(scriptComponent, null);
                }
                else
                {
                    Debug.LogError("Use method not found on IItem interface.");
                }
            } else
			{
                MethodInfo useMethod = typeof(ItemBase).GetMethod("Pickup");
                if (useMethod != null)
                {
                    useMethod.Invoke(scriptComponent, null);
                }
                else
                {
                    Debug.LogError("Pickup method not found on IItem interface.");
                }
            }
        }
        else
        {
            Debug.LogError("Script not found on GameObject or does not implement IItem: " + scriptName);
        }
    }


    private void ModifyItemSelection(int count)
    {
        this.itemSelected += count;
        if (this.itemSelected > 2)
        {
            this.itemSelected = 0;
        } else if (itemSelected < 0)
		{
			this.itemSelected = 2; 
		}
        this.itemSelect.anchoredPosition = this.itemSlot[itemSelected].GetComponent<RectTransform>().anchoredPosition;
        //this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f); 
        this.UpdateItemName();
    }


    private void UpdateItemSelection()
	{
		this.itemSelect.anchoredPosition = this.itemSlot[itemSelected].GetComponent<RectTransform>().anchoredPosition;
        this.UpdateItemName();
	}

	
	public void CollectItem(int item_ID)
	{
		if (this.item[0] == 0)
		{
			this.item[0] = item_ID; 
			this.itemSlot[0].texture = this.itemTextures[item_ID]; 
		}
		else if (this.item[1] == 0)
		{
			this.item[1] = item_ID; 
            this.itemSlot[1].texture = this.itemTextures[item_ID]; 
        }
		else if (this.item[2] == 0)
		{
			this.item[2] = item_ID; 
            this.itemSlot[2].texture = this.itemTextures[item_ID]; 
        }
		else 
		{
			this.item[this.itemSelected] = item_ID;
			this.itemSlot[this.itemSelected].texture = this.itemTextures[item_ID];
		}
		this.UpdateItemName();
	}

	
	private void UseItem()
	{
		if (this.item[this.itemSelected] != 0)
		{
			InteractItem(itemProfile.items[item[this.itemSelected]], true);
        }
	}

	
	public IEnumerator BootAnimation()
	{
		float time = 15f;
		float height = 375f;
		Vector3 position = default(Vector3);
		this.boots.gameObject.SetActive(true);
		while (height > -375f)
		{
			height -= 375f * Time.deltaTime;
			time -= Time.deltaTime;
			position = this.boots.localPosition;
			position.y = height;
			this.boots.localPosition = position;
			yield return null;
		}
		position = this.boots.localPosition;
		position.y = -375f;
		this.boots.localPosition = position;
		this.boots.gameObject.SetActive(false);
		while (time > 0f)
		{
			time -= Time.deltaTime;
			yield return null;
		}
		this.boots.gameObject.SetActive(true);
		while (height < 375f)
		{
			height += 375f * Time.deltaTime;
			position = this.boots.localPosition;
			position.y = height;
			this.boots.localPosition = position;
			yield return null;
		}
		position = this.boots.localPosition;
		position.y = 375f;
		this.boots.localPosition = position;
		this.boots.gameObject.SetActive(false);
		yield break;
	}

	
	public void ResetItem()
	{
		this.item[this.itemSelected] = 0;
		this.itemSlot[this.itemSelected].texture = itemProfile.items[0].Icon;
		this.UpdateItemName();
	}

	
	public void LoseItem(int id)
	{
		this.item[id] = 0;
		this.itemSlot[id].texture =itemProfile.items[0].Icon;
		this.UpdateItemName();
	}

	
	private void UpdateItemName()
	{
		this.itemText.text = itemProfile.items[this.item[this.itemSelected]].InGameName;

    }

	
	public void ExitReached()
	{
		this.exitsReached++;
		if (this.exitsReached == 1)
		{
			RenderSettings.ambientLight = Color.red; 
			
			this.audioDevice.PlayOneShot(this.aud_Switch, 0.8f);
			this.audioDevice.clip = this.aud_MachineQuiet;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		if (this.exitsReached == 2) 
		{
			this.audioDevice.volume = 0.8f;
			this.audioDevice.clip = this.aud_MachineStart;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		if (this.exitsReached == 3) 
		{
			this.audioDevice.clip = this.aud_MachineRev;
			this.audioDevice.loop = false;
			this.audioDevice.Play();
		}
	}

	
	public void DespawnCrafters()
	{
		this.crafters.SetActive(false); 
	}

	
	public void Fliparoo()
	{
		this.player.height = 6f;
		this.player.fliparoo = 180f;
		this.player.flipaturn = -1f;
		Camera.main.GetComponent<CameraScript>().offset = new Vector3(0f, -1f, 0f);
	}

	[Header("BASIC Essentials")]
	public ItemProfile2 itemProfile;

	public BASICDecompProfile settingsProfile;


	[Header("Not BASIC")]
	public CursorControllerScript cursorController;

	
	public PlayerScript player;

	
	public Transform playerTransform;

	
	public Transform cameraTransform;

	
	public new Camera camera;

	
	private int cullingMask;

	
	[SerializeField] private EntranceScript[] entrances;
	
	public GameObject baldiTutor;

	
	public GameObject baldi;

	
	public BaldiScript baldiScrpt;

	
	public AudioClip aud_Prize;

	
	public AudioClip aud_PrizeMobile;

	
	public AudioClip aud_AllNotebooks;

	
	public GameObject principal;

	
	public GameObject crafters;

	
	public GameObject playtime;

	
	public PlaytimeScript playtimeScript;

	
	public GameObject gottaSweep;

	
	public GameObject bully;

	
	public GameObject firstPrize;

	
	public GameObject TestEnemy;

	
	public FirstPrizeScript firstPrizeScript;

	
	public GameObject quarter;

	
	public AudioSource tutorBaldi;

	
	public RectTransform boots;

	
	public string mode;

	
	public int notebooks;

	
	public GameObject[] notebookPickups;

	
	public int failedNotebooks;

	
	public bool spoopMode;

	
	public bool finaleMode;

	
	public bool debugMode;

	
	public bool mouseLocked;

	
	public int exitsReached;

	
	public int itemSelected;

	
	public int[] item = new int[3];

	
	public RawImage[] itemSlot = new RawImage[3];
	
	public TMP_Text itemText;

	
	public UnityEngine.Object[] items = new UnityEngine.Object[10];

	
	public Texture[] itemTextures = new Texture[10];

	
	public GameObject bsodaSpray;

	
	public GameObject alarmClock;

	
	public TMP_Text notebookCount;

	
	public GameObject pauseMenu;

	
	public GameObject highScoreText;

	
	public GameObject warning;

	
	public GameObject reticle;

	
	public RectTransform itemSelect;

	
	private int[] itemSelectOffset;

	
	private bool gamePaused;

	
	private bool learningActive;

	
	private float gameOverDelay;

	
	public AudioSource audioDevice;

	
	public AudioClip aud_Soda;

	
	public AudioClip aud_Spray;

	
	public AudioClip aud_buzz;

	
	public AudioClip aud_Hang;

	
	public AudioClip aud_MachineQuiet;

	
	public AudioClip aud_MachineStart;

	
	public AudioClip aud_MachineRev;

	
	public AudioClip aud_MachineLoop;

	
	public AudioClip aud_Switch;

	
	public AudioSource schoolMusic;

	
	public AudioSource learnMusic;

	
	
}
