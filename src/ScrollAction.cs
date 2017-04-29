using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollAction : MonoBehaviour, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
{
	public Image scrollImage;

	public void OnBeginDrag(PointerEventData eventData)
	{
		this.scrollImage.set_enabled(true);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		this.scrollImage.set_enabled(false);
	}
}
