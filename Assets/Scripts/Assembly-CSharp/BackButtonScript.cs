using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class BackButtonScript : MonoBehaviour
{
	
	private void Start()
	{
		this.button = base.GetComponent<Button>();
		this.button.onClick.AddListener(new UnityAction(this.CloseScreen));
	}

	
	private void CloseScreen()
	{
		this.screen.SetActive(false);
	}

	
	private Button button;

	
	public GameObject screen;
}
