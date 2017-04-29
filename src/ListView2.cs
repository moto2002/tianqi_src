using System;
using System.Collections.Generic;
using UnityEngine;

public class ListView2 : MonoBehaviour
{
	public GameObject RowFrefab;

	public string RowFrefabName;

	public int height = -125;

	public int offset;

	public uint DurationDel = 750u;

	public uint DurationUp = 450u;

	public uint DurationUpStart = 350u;

	public int upInitX;

	public int upInitY = -670;

	private int movingCount;

	private bool isAddAtMoving;

	[HideInInspector]
	public List<GameObject> Items = new List<GameObject>();

	private void Start()
	{
		if (this.RowFrefab != null)
		{
			this.RowFrefab.SetActive(false);
		}
	}

	public void RemoveRow(int index)
	{
		if (index >= 0)
		{
			GameObject go = this.Items.get_Item(index);
			this.Items.RemoveAt(index);
			RectTransform component = go.GetComponent<RectTransform>();
			base.StartCoroutine(component.MoveToAnchoredPosition(new Vector3(component.get_anchoredPosition().x - 1500f, component.get_anchoredPosition().y), this.DurationDel / 1000f, EaseType.Linear, delegate
			{
				Object.Destroy(go);
				this.moveToPos();
			}));
		}
	}

	private void moveToPos()
	{
		for (int i = 0; i < this.Items.get_Count(); i++)
		{
			RectTransform component = this.Items.get_Item(i).GetComponent<RectTransform>();
			this.movingCount++;
			base.StartCoroutine(component.MoveToAnchoredPosition(new Vector3(0f, (float)(i * this.height + this.offset)), this.DurationUp / 1000f, EaseType.Linear, delegate
			{
				this.movingCount--;
				if (this.isAddAtMoving)
				{
					this.isAddAtMoving = false;
					this.moveToPos();
				}
			}));
		}
	}

	public void DoAnimation()
	{
		for (int i = 0; i < this.Items.get_Count(); i++)
		{
			RectTransform component = this.Items.get_Item(i).GetComponent<RectTransform>();
			component.set_anchoredPosition(new Vector2((float)this.upInitX, (float)this.upInitY));
			base.StartCoroutine(component.MoveToAnchoredPosition(new Vector3(0f, (float)(i * this.height + this.offset)), (float)((ulong)this.DurationUpStart + (ulong)((long)(100 * i))) / 1000f, EaseType.CubeOut, null));
		}
	}

	public void CreateRow(int num, int grid = 0)
	{
		base.GetComponent<RectTransform>().set_sizeDelta(new Vector2(base.GetComponent<RectTransform>().get_sizeDelta().x, (float)(-(float)(num * this.height + 2 * this.offset))));
		if (this.Items.get_Count() > num)
		{
			int i;
			for (i = 0; i < num; i++)
			{
				this.Items.get_Item(i).SetActive(true);
			}
			while (i < this.Items.get_Count())
			{
				this.Items.get_Item(i).SetActive(false);
				i++;
			}
		}
		else if (this.Items.get_Count() < num)
		{
			int j;
			for (j = 0; j < this.Items.get_Count(); j++)
			{
				if (!this.Items.get_Item(j).get_activeSelf())
				{
					this.Items.get_Item(j).SetActive(true);
				}
			}
			if (this.movingCount != 0)
			{
				this.isAddAtMoving = true;
			}
			while (j < num)
			{
				GameObject gameObject;
				if (string.IsNullOrEmpty(this.RowFrefabName))
				{
					gameObject = UGUITools.AddChild(base.get_gameObject(), this.RowFrefab, false);
				}
				else
				{
					gameObject = ResourceManager.GetInstantiate2Prefab(this.RowFrefabName);
					UGUITools.SetParent(base.get_gameObject(), gameObject, false);
				}
				if (gameObject != null)
				{
					if (string.IsNullOrEmpty(this.RowFrefabName))
					{
						gameObject.set_name(this.RowFrefab.get_name() + j);
					}
					else
					{
						gameObject.set_name(this.RowFrefabName + j);
					}
					Transform transform = gameObject.get_transform();
					if (j == 0)
					{
						transform.set_localPosition(new Vector3(0f, (float)(j * this.height + this.offset)));
					}
					else
					{
						transform.set_localPosition(new Vector3(0f, this.Items.get_Item(j - 1).get_transform().get_localPosition().y + (float)this.height));
					}
					transform.set_localRotation(Quaternion.get_identity());
					transform.set_localScale(Vector3.get_one());
					gameObject.SetActive(true);
					this.Items.Add(gameObject);
				}
				j++;
			}
		}
		else
		{
			for (int k = 0; k < num; k++)
			{
				if (!this.Items.get_Item(k).get_activeSelf())
				{
					this.Items.get_Item(k).SetActive(true);
				}
			}
		}
		for (int l = 0; l < num; l++)
		{
			this.Items.get_Item(l).set_name(l + string.Empty);
		}
	}
}
