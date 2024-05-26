using System;
using UnityEngine;


public class NotebookScript : MonoBehaviour
{
	
	private void Start()
	{
		
		this.up = true;
	}

	
	private void Update()
	{
		if (this.gc.mode == "endless")
		{
			if (this.respawnTime > 0f)
			{
				if ((base.transform.position - this.player.position).magnitude > 60f)
				{
					this.respawnTime -= Time.deltaTime;
				}
			}
			else if (!this.up)
			{
				base.transform.position = new Vector3(base.transform.position.x, 4f, base.transform.position.z);
				this.up = true;
				this.audioDevice.Play();
			}
		}
		if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && (raycastHit.transform.tag == "Notebook" & Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance))
			{
				base.transform.position = new Vector3(base.transform.position.x, -20f, base.transform.position.z);
				this.up = false;
				this.respawnTime = 120f;
				this.gc.CollectNotebook();
				BasicAPI.Events.OnNotebookCollect.Invoke(); 

                if (!GameControllerScript.Instance.settingsProfile.YCTP)
                {
                    if (!this.gc.spoopMode && gc.notebooks == 2)
                        this.gc.ActivateSpoopMode();

                    if (this.gc.mode == "story" && gc.notebooks != 1)
                        gc.baldiScrpt.GetAngry(1f);

                    return;
                }

                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.learningGame);
                gameObject.GetComponent<MathGameScript>().gc = this.gc;
                gameObject.GetComponent<MathGameScript>().baldiScript = this.bsc;
                gameObject.GetComponent<MathGameScript>().playerPosition = this.player.position;
			}
		}
	}

	
	public float openingDistance;

	
	public GameControllerScript gc;

	
	public BaldiScript bsc;

	
	public float respawnTime;

	
	public bool up;

	
	public Transform player;

	
	public GameObject learningGame;

	
	public AudioSource audioDevice;

	
	
}
