using System;
using System.Collections.Generic;
using UnityEngine;

public class PetDragDropBar : MonoBehaviour
{
	public List<Transform> listDragDropItems = new List<Transform>();

	private List<float> listPosX = new List<float>();

	private float posY = -5f;

	private float startS = -164f;

	private float spaceX = 110f;

	private void Awake()
	{
		for (int i = 0; i < 4; i++)
		{
			this.listPosX.Add(this.startS + (float)i * this.spaceX);
		}
	}

	public void ResetDragBar(List<Transform> listDragDropItem)
	{
		for (int i = 0; i < this.listDragDropItems.get_Count(); i++)
		{
			Transform transform = this.listDragDropItems.get_Item(i);
			Object.Destroy(transform.get_gameObject());
		}
		this.listDragDropItems.Clear();
		if (listDragDropItem != null)
		{
			for (int j = 0; j < listDragDropItem.get_Count(); j++)
			{
				this.AddPetItem(listDragDropItem.get_Item(j));
			}
		}
	}

	private void AddPetItem(Transform dragDropItem)
	{
		if (this.listDragDropItems.get_Count() >= 4)
		{
			return;
		}
		this.listDragDropItems.Add(dragDropItem);
		dragDropItem.GetComponent<PetDragDropItem>().petDragBar = this;
		dragDropItem.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
		dragDropItem.GetComponent<RectTransform>().set_localPosition(Vector3.get_one());
	}

	public void OnDragBegin(Transform tr)
	{
		this.SetCorrectPlacesOnDrag();
	}

	public void OnEndDrag(Transform tr)
	{
		for (int i = 0; i < this.listDragDropItems.get_Count(); i++)
		{
			for (int j = i + 1; j < this.listDragDropItems.get_Count(); j++)
			{
				Transform transform = this.listDragDropItems.get_Item(i);
				Transform transform2 = this.listDragDropItems.get_Item(j);
				if (transform.get_localPosition().x > transform2.get_localPosition().x)
				{
					XUtility.ListExchange<Transform>(this.listDragDropItems, i, j);
				}
			}
		}
		this.SetCorrectPlacesEndDrag();
	}

	private void SetCorrectPlacesOnDrag()
	{
		int num = 0;
		for (int i = 0; i < this.listDragDropItems.get_Count(); i++)
		{
			Transform transform = this.listDragDropItems.get_Item(i);
			PetDragDropItem component = transform.GetComponent<PetDragDropItem>();
			if (component.isOnDrag)
			{
				num = i;
				break;
			}
		}
		for (int j = 0; j < this.listDragDropItems.get_Count(); j++)
		{
			Transform transform2 = this.listDragDropItems.get_Item(j);
			PetDragDropItem component2 = transform2.GetComponent<PetDragDropItem>();
			if (!component2.isOnDrag)
			{
				if (j > num)
				{
					Vector3 localPosition = transform2.GetComponent<RectTransform>().get_localPosition();
					localPosition.x = this.listPosX.get_Item(j - 1);
					localPosition.y = this.posY;
					transform2.GetComponent<BaseTweenPostion>().MoveTo(localPosition, 0.2f);
				}
			}
		}
	}

	private void SetCorrectPlacesEndDrag()
	{
		float num = 0.2f;
		for (int i = 0; i < this.listDragDropItems.get_Count(); i++)
		{
			Transform transform = this.listDragDropItems.get_Item(i);
			PetDragDropItem component = transform.GetComponent<PetDragDropItem>();
			if (!component.isOnDrag)
			{
				Vector3 localPosition = transform.GetComponent<RectTransform>().get_localPosition();
				localPosition.x = this.listPosX.get_Item(i);
				localPosition.y = this.posY;
				transform.GetComponent<BaseTweenPostion>().MoveTo(localPosition, num);
			}
		}
		TimerHeap.AddTimer((uint)(num * 1000f), 0, delegate
		{
			EventDispatcher.Broadcast(DungeonManagerEvent.PetDragDropItemOnDrag);
		});
	}
}
