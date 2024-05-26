using System;
using UnityEngine;


public class EndlessNotebookScript : BASICInteractable
{
	
	private void Start()
	{
		this.gc = GameControllerScript.Instance;
		this.player = gc.playerTransform;
	}

	
	private void Update()
	{
        if (base.Interacted())
        {
            base.gameObject.SetActive(false);
            this.gc.CollectNotebook();
            this.learningGame.SetActive(true);
        }
	}

	
	public float openingDistance;

	
	public GameControllerScript gc;

	
	public Transform player;

	
	public GameObject learningGame;

}
