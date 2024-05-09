using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class BasicButtonScript : MonoBehaviour
{
	
	private void Start()
	{
		this.button = base.GetComponent<Button>();
		this.button.onClick.AddListener(new UnityAction(this.OpenScreen));
	}

	
	private void OpenScreen()
	{
		this.screen.SetActive(true);
	}

	
	private Button button;

	
	public GameObject screen;
}
