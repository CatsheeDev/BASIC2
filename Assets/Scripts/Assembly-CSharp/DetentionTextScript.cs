using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class DetentionTextScript : MonoBehaviour
{
	
	private void Start()
	{
		this.text = base.GetComponent<TMP_Text>();
	}

	
	private void Update()
	{
		if (this.door.lockTime > 0f)
		{
			this.text.text = "You have detention! \n" + Mathf.CeilToInt(this.door.lockTime) + " seconds remain!";
		}
		else
		{
			this.text.text = string.Empty;
		}
	}

	
	public DoorScript door;

	
	private TMP_Text text;
}
