using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class ImageNums : BaseUIBehaviour
{
	private GridLayoutGroup grid;

	private string m_imageNumName;

	private List<GameObject> numsObjs = new List<GameObject>();

	private Vector2 sizeImage;

	public int numShow;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.grid = base.get_gameObject().GetComponent<GridLayoutGroup>();
		if (this.grid == null)
		{
			this.grid = base.get_gameObject().AddComponent<GridLayoutGroup>();
		}
		this.grid.set_startAxis(0);
		this.grid.set_startCorner(0);
		this.grid.set_childAlignment(4);
		this.grid.set_constraint(2);
		this.grid.set_constraintCount(1);
	}

	public void Init(Vector2 cellSize, string imageNumName)
	{
		this.sizeImage = cellSize;
		this.grid.set_cellSize(cellSize);
		this.m_imageNumName = imageNumName;
	}

	public void SetImageNum(int num)
	{
		this.numShow = num;
		for (int i = 0; i < this.numsObjs.get_Count(); i++)
		{
			GameObject gameObject = this.numsObjs.get_Item(i);
			Object.Destroy(gameObject);
		}
		this.numsObjs.Clear();
		List<int> list = new List<int>();
		int num2 = 0;
		while (true)
		{
			list.Add(num % 10);
			if (num < 10)
			{
				break;
			}
			num /= 10;
			num2++;
		}
		for (int j = list.get_Count() - 1; j >= 0; j--)
		{
			GameObject gameObject2 = new GameObject();
			gameObject2.AddComponent<RectTransform>();
			gameObject2.GetComponent<RectTransform>().set_sizeDelta(this.sizeImage);
			ResourceManager.SetSprite(gameObject2.AddComponent<Image>(), ResourceManager.GetIconSprite(this.m_imageNumName + list.get_Item(j)));
			gameObject2.get_transform().SetParent(this.grid.get_transform());
			gameObject2.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
			Vector3 localPosition = gameObject2.GetComponent<RectTransform>().get_localPosition();
			localPosition.z = 0f;
			gameObject2.GetComponent<RectTransform>().set_localPosition(localPosition);
			this.numsObjs.Add(gameObject2);
		}
	}
}
