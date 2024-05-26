using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class MouseoverScript : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
{
	
	public void OnSelect(BaseEventData eventData)
	{
		this.mouseOver.Invoke();
	}

	
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.mouseOver.Invoke();
	}

	
	public void OnDeselect(BaseEventData eventData)
	{
		this.mouseLeave.Invoke();
	}

	
	public void OnPointerExit(PointerEventData eventData)
	{
		this.mouseLeave.Invoke();
	}

	
	public UnityEvent mouseOver;

	
	public UnityEvent mouseLeave;
}
