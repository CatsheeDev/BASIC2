using System;
using UnityEngine;
using UnityEngine.Events;


public class OnAwakeTrigger : MonoBehaviour
{
	
	private void OnEnable()
	{
		this.OnEnableEvent.Invoke();
	}

	
	public UnityEvent OnEnableEvent;
}
