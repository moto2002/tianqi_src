using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ListView : ScrollRect
{
	public enum ListViewScrollStyle
	{
		Up,
		Down,
		Left,
		Right
	}

	public ListView.ListViewScrollStyle m_listViewScrollStyle;

	public GameObject m_contentView;

	private List<Cell> m_listCellOfUnuse = new List<Cell>();

	private List<Cell> m_listCellOfAll = new List<Cell>();

	private List<int> m_rowOfHaveShowed = new List<int>();

	private Dictionary<float, int> m_dicCheckOffsets = new Dictionary<float, int>();

	private Dictionary<int, Hashtable> m_dicRowInfo = new Dictionary<int, Hashtable>();

	private uint m_countOfRows;

	private float m_scrollSizeTotal;

	private float m_lastOffsetReal;

	private float m_offsetReal;

	private float m_checkOffset;

	private float m_maxOffset;

	private float m_scrollSize;

	private float m_timeCheck;

	public ListViewInterface manager;

	private bool m_isInited;

	private List<float> m_listOffsets = new List<float>();

	private List<int> m_listRowShouldShow = new List<int>();

	public override void OnBeginDrag(PointerEventData eventData)
	{
		if (GuideManager.Instance.uidrag_lock)
		{
			return;
		}
		base.OnBeginDrag(eventData);
	}

	public override void OnEndDrag(PointerEventData eventData)
	{
		if (GuideManager.Instance.uidrag_lock)
		{
			return;
		}
		base.OnEndDrag(eventData);
	}

	public override void OnDrag(PointerEventData eventData)
	{
		if (GuideManager.Instance.uidrag_lock)
		{
			return;
		}
		base.OnDrag(eventData);
	}

	public void Init(ListView.ListViewScrollStyle scrollStyle)
	{
		if (this.m_isInited)
		{
			return;
		}
		this.m_isInited = true;
		if (this.manager == null)
		{
			Debuger.Error("Init()  err : manager == null  && return", new object[0]);
			return;
		}
		this.m_contentView = new GameObject();
		this.m_contentView.set_name("ContentView");
		this.m_contentView.AddComponent<RectTransform>();
		base.set_content(this.m_contentView.GetComponent<RectTransform>());
		this.m_contentView.get_transform().SetParent(base.get_transform());
		this.m_contentView.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
		this.SetScrollStyle(scrollStyle);
		Vector3 zero = Vector3.get_zero();
		if (scrollStyle == ListView.ListViewScrollStyle.Up)
		{
			zero.y = base.GetComponent<RectTransform>().get_sizeDelta().y / 2f;
		}
		else if (scrollStyle == ListView.ListViewScrollStyle.Down)
		{
			zero.y = 0f - base.GetComponent<RectTransform>().get_sizeDelta().y / 2f;
		}
		else if (scrollStyle == ListView.ListViewScrollStyle.Left)
		{
			zero.x = 0f - base.GetComponent<RectTransform>().get_sizeDelta().x / 2f;
		}
		else if (scrollStyle == ListView.ListViewScrollStyle.Right)
		{
			zero.x = base.GetComponent<RectTransform>().get_sizeDelta().x / 2f;
		}
		this.m_contentView.GetComponent<RectTransform>().set_localPosition(zero);
		Image image = this.m_contentView.AddComponent<Image>();
		image.set_enabled(false);
		base.get_gameObject().AddMissingComponent<Image>();
		Mask mask = base.get_gameObject().AddMissingComponent<Mask>();
		mask.set_showMaskGraphic(false);
	}

	public void Refresh()
	{
		if (this.manager == null)
		{
			Debuger.Error("ReloadData()  err : manager == null  && return", new object[0]);
			return;
		}
		this.SetScrollSize();
		this.ClearData();
		this.m_countOfRows = this.manager.CountOfRows(this);
		if (this.m_countOfRows == 0u)
		{
			return;
		}
		this.m_checkOffset = this.manager.SpacingForRow(this, 0);
		this.m_maxOffset = this.m_checkOffset;
		this.CalRowInfos();
		this.AddInDics();
		Vector2 contentViewSize = this.GetContentViewSize();
		this.m_contentView.GetComponent<RectTransform>().set_sizeDelta(contentViewSize);
		this.ReCalOffsetReal();
		this.LoadCurrentShow();
	}

	private void SetScrollSize()
	{
		if (this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Up || this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Down)
		{
			this.m_scrollSize = base.GetComponent<RectTransform>().get_sizeDelta().y;
		}
		else
		{
			this.m_scrollSize = base.GetComponent<RectTransform>().get_sizeDelta().x;
		}
	}

	private void CalRowInfos()
	{
		this.m_listOffsets.Clear();
		this.m_scrollSizeTotal = 0f;
		int num = 0;
		while ((long)num < (long)((ulong)this.m_countOfRows))
		{
			this.m_listOffsets.Add(this.m_scrollSizeTotal);
			float num2 = this.manager.SpacingForRow(this, num);
			if (this.m_checkOffset > num2)
			{
				this.m_checkOffset = num2;
			}
			if (this.m_maxOffset < num2)
			{
				this.m_maxOffset = num2;
			}
			Hashtable hashtable = new Hashtable();
			hashtable.Add("offset", this.m_scrollSizeTotal);
			hashtable.Add("height", num2);
			this.m_dicRowInfo.Add(num, hashtable);
			this.m_scrollSizeTotal += num2;
			num++;
		}
	}

	private Vector2 GetContentViewSize()
	{
		Vector2 sizeDelta = this.m_contentView.GetComponent<RectTransform>().get_sizeDelta();
		if (this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Up || this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Down)
		{
			sizeDelta.x = base.GetComponent<RectTransform>().get_sizeDelta().x;
			sizeDelta.y = this.m_scrollSizeTotal;
		}
		else
		{
			sizeDelta.x = this.m_scrollSizeTotal;
			sizeDelta.y = base.GetComponent<RectTransform>().get_sizeDelta().y;
		}
		return sizeDelta;
	}

	private void AddInDics()
	{
		int num = 0;
		float num2 = this.m_listOffsets.get_Item(num);
		float num3;
		if (num + 1 < this.m_listOffsets.get_Count())
		{
			num3 = this.m_listOffsets.get_Item(num + 1);
		}
		else
		{
			num3 = this.m_listOffsets.get_Item(num);
		}
		for (float num4 = 0f; num4 <= this.m_scrollSizeTotal + this.m_checkOffset; num4 += this.m_checkOffset)
		{
			if (num4 == num2)
			{
				this.AddInDic(num4, num);
			}
			else if (num4 > num2 && num4 < num3)
			{
				this.AddInDic(num4, num);
			}
			else if (num4 == num3)
			{
				num++;
				this.AddInDic(num4, num);
				num2 = this.m_listOffsets.get_Item(num);
				if (num + 1 < this.m_listOffsets.get_Count())
				{
					num3 = this.m_listOffsets.get_Item(num + 1);
				}
				else
				{
					num3 = this.m_listOffsets.get_Item(num);
				}
			}
			else
			{
				num++;
				if (num < this.m_listOffsets.get_Count())
				{
					num2 = this.m_listOffsets.get_Item(num);
					if (num + 1 < this.m_listOffsets.get_Count())
					{
						num3 = this.m_listOffsets.get_Item(num + 1);
					}
					else
					{
						num3 = this.m_listOffsets.get_Item(num);
					}
					this.AddInDic(num4, num);
				}
				else
				{
					this.AddInDic(num4, this.m_listOffsets.get_Count() - 1);
				}
			}
		}
	}

	public void Release()
	{
		for (int i = 0; i < this.m_listCellOfUnuse.get_Count(); i++)
		{
			this.m_listCellOfUnuse.get_Item(i).Destory();
		}
		this.m_listCellOfUnuse.Clear();
		for (int j = 0; j < this.m_listCellOfAll.get_Count(); j++)
		{
			this.m_listCellOfAll.get_Item(j).Destory();
		}
		this.m_listCellOfAll.Clear();
	}

	public void ShowContentBackgroud(bool show)
	{
		if (show)
		{
			this.m_contentView.GetComponent<Image>().set_enabled(true);
		}
		else
		{
			this.m_contentView.GetComponent<Image>().set_enabled(false);
		}
	}

	private void SetScrollStyle(ListView.ListViewScrollStyle scrollStyle)
	{
		this.m_listViewScrollStyle = scrollStyle;
		if (this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Up || this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Down)
		{
			base.set_horizontal(false);
			base.set_vertical(true);
		}
		else
		{
			base.set_horizontal(true);
			base.set_vertical(false);
		}
		switch (this.m_listViewScrollStyle)
		{
		case ListView.ListViewScrollStyle.Up:
			this.m_contentView.GetComponent<RectTransform>().set_pivot(new Vector2(0.5f, 1f));
			break;
		case ListView.ListViewScrollStyle.Down:
			this.m_contentView.GetComponent<RectTransform>().set_pivot(new Vector2(0.5f, 0f));
			break;
		case ListView.ListViewScrollStyle.Left:
			this.m_contentView.GetComponent<RectTransform>().set_pivot(new Vector2(0f, 0.5f));
			break;
		case ListView.ListViewScrollStyle.Right:
			this.m_contentView.GetComponent<RectTransform>().set_pivot(new Vector2(1f, 0.5f));
			break;
		}
	}

	private void ClearData()
	{
		this.m_dicRowInfo.Clear();
		this.m_listCellOfUnuse.Clear();
		for (int i = 0; i < this.m_listCellOfAll.get_Count(); i++)
		{
			this.ShowCell(this.m_listCellOfAll.get_Item(i), false);
		}
		this.m_listCellOfUnuse.AddRange(this.m_listCellOfAll);
		this.m_dicCheckOffsets.Clear();
		this.m_dicRowInfo.Clear();
		this.m_rowOfHaveShowed.Clear();
	}

	private void AddInDic(float key, int row)
	{
		if ((long)row >= (long)((ulong)this.m_countOfRows))
		{
			return;
		}
		if (!this.m_dicCheckOffsets.ContainsKey(key))
		{
			this.m_dicCheckOffsets.Add(key, row);
		}
	}

	public Cell CellForReuseIndentify(string indentify)
	{
		Cell cell = this.m_listCellOfUnuse.Find((Cell go) => go.identify.Equals(indentify));
		if (cell != null)
		{
			this.m_listCellOfUnuse.Remove(cell);
			this.ShowCell(cell, true);
			return cell;
		}
		return null;
	}

	private void LoadCurrentShow()
	{
		float num = this.m_offsetReal + this.m_maxOffset + this.m_scrollSize;
		this.m_listRowShouldShow.Clear();
		for (float num2 = (float)Mathf.FloorToInt(this.m_offsetReal); num2 <= num + this.m_checkOffset; num2 += this.m_checkOffset)
		{
			int num3 = (int)(num2 / this.m_checkOffset);
			float num4 = (float)num3 * this.m_checkOffset;
			if (this.m_dicCheckOffsets.ContainsKey(num4))
			{
				int num5 = this.m_dicCheckOffsets.get_Item(num4);
				if (!this.m_listRowShouldShow.Contains(num5) && (long)num5 < (long)((ulong)this.m_countOfRows))
				{
					this.m_listRowShouldShow.Add(num5);
				}
			}
		}
		for (int i = 0; i < this.m_rowOfHaveShowed.get_Count(); i++)
		{
			int rowShow = this.m_rowOfHaveShowed.get_Item(i);
			if (!this.m_listRowShouldShow.Contains(rowShow))
			{
				this.m_rowOfHaveShowed.Remove(rowShow);
				i--;
				Cell cell2 = this.m_listCellOfAll.Find((Cell e) => e.row == rowShow);
				if (cell2 != null)
				{
					this.m_listCellOfUnuse.Add(cell2);
				}
			}
		}
		for (int j = 0; j < this.m_listRowShouldShow.get_Count(); j++)
		{
			int num6 = this.m_listRowShouldShow.get_Item(j);
			if (!this.m_rowOfHaveShowed.Contains(num6))
			{
				Cell cell = this.manager.CellForRow(this, this.m_listRowShouldShow.get_Item(j));
				cell.row = num6;
				if (this.m_listCellOfAll.Find((Cell e) => e.cellID == cell.cellID) == null)
				{
					this.m_listCellOfAll.Add(cell);
				}
				RectTransform component = cell.content.GetComponent<RectTransform>();
				if (this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Up)
				{
					component.set_localPosition(new Vector3(0f, 0f - (float)this.m_dicRowInfo.get_Item(num6).get_Item("offset"), 0f));
				}
				else if (this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Down)
				{
					component.set_localPosition(new Vector3(0f, (float)this.m_dicRowInfo.get_Item(num6).get_Item("offset"), 0f));
				}
				else if (this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Left)
				{
					component.set_localPosition(new Vector3((float)this.m_dicRowInfo.get_Item(num6).get_Item("offset"), 0f, 0f));
				}
				else if (this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Right)
				{
					component.set_localPosition(new Vector3(0f - (float)this.m_dicRowInfo.get_Item(num6).get_Item("offset"), 0f, 0f));
				}
				component.set_localScale(Vector3.get_one());
				this.m_rowOfHaveShowed.Add(num6);
			}
		}
	}

	private void ReCalOffsetReal()
	{
		if (this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Up)
		{
			float y = base.get_normalizedPosition().y;
			this.m_offsetReal = (1f - y) * (this.m_scrollSizeTotal - this.m_scrollSize);
		}
		else if (this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Down)
		{
			float y2 = base.get_normalizedPosition().y;
			this.m_offsetReal = y2 * (this.m_scrollSizeTotal - this.m_scrollSize);
		}
		else if (this.m_listViewScrollStyle == ListView.ListViewScrollStyle.Left)
		{
			float x = base.get_normalizedPosition().x;
			this.m_offsetReal = x * (this.m_scrollSizeTotal - this.m_scrollSize);
		}
		else
		{
			float x2 = base.get_normalizedPosition().x;
			this.m_offsetReal = (1f - x2) * (this.m_scrollSizeTotal - this.m_scrollSize);
		}
	}

	private void Update()
	{
		this.ReCalOffsetReal();
		if (Mathf.Abs(this.m_lastOffsetReal - this.m_offsetReal) > 1f && this.m_dicCheckOffsets.get_Count() != 0 && Time.get_realtimeSinceStartup() - this.m_timeCheck > 0.001f)
		{
			this.LoadCurrentShow();
		}
		this.m_timeCheck = Time.get_realtimeSinceStartup();
		this.m_lastOffsetReal = this.m_offsetReal;
	}

	private void ShowCell(Cell cell, bool isShow)
	{
		if (cell != null && cell.content != null)
		{
			cell.content.SetActive(isShow);
		}
	}
}
