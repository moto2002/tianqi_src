using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetDragDropItem : MonoBehaviour, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
	public PetDragDropBar petDragBar;

	private Canvas canvas;

	public Camera cameraUI;

	public bool isOnDrag;

	private void Start()
	{
		this.canvas = UINodesManager.NormalUIRoot.GetComponent<Canvas>();
		this.cameraUI = this.canvas.get_worldCamera();
	}

	private void Update()
	{
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		Debuger.Info("OnBeginDrag  ", new object[0]);
		this.isOnDrag = true;
		this.petDragBar.OnDragBegin(base.get_transform());
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector3 position = this.cameraUI.ScreenToWorldPoint(eventData.get_position());
		position.z = 0f;
		base.get_transform().GetComponent<RectTransform>().set_position(position);
	}

	public void OnDrop(PointerEventData eventData)
	{
		Debuger.Info("OnDrop  ", new object[0]);
		this.isOnDrag = false;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debuger.Info("OnEndDrag  ", new object[0]);
		this.isOnDrag = false;
		this.petDragBar.OnEndDrag(base.get_transform());
	}
}
