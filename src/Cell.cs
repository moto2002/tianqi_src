using System;
using UnityEngine;

public class Cell
{
	public int row;

	public long cellID;

	public string identify;

	public ListView listView;

	private GameObject m_content;

	public GameObject content
	{
		get
		{
			return this.m_content;
		}
		set
		{
			this.m_content = value;
			if (this.m_content != null)
			{
				this.m_content.get_transform().SetParent(this.listView.m_contentView.get_transform());
				this.SetCellScrollStyle(this.listView.m_listViewScrollStyle);
			}
		}
	}

	public Cell(ListView listViewOwner)
	{
		long expr_0C = CellID.cellID;
		CellID.cellID = expr_0C + 1L;
		this.cellID = expr_0C;
		this.listView = listViewOwner;
	}

	public void SetCellScrollStyle(ListView.ListViewScrollStyle scrollStyle)
	{
		if (scrollStyle == ListView.ListViewScrollStyle.Up)
		{
			RectTransform component = this.m_content.GetComponent<RectTransform>();
			component.set_anchorMin(new Vector2(0.5f, 1f));
			component.set_anchorMax(new Vector2(0.5f, 1f));
			component.set_pivot(new Vector2(0.5f, 1f));
		}
		else if (scrollStyle == ListView.ListViewScrollStyle.Down)
		{
			RectTransform component2 = this.m_content.GetComponent<RectTransform>();
			component2.set_anchorMin(new Vector2(0.5f, 0f));
			component2.set_anchorMax(new Vector2(0.5f, 0f));
			component2.set_pivot(new Vector2(0.5f, 0f));
		}
		else if (scrollStyle == ListView.ListViewScrollStyle.Left)
		{
			RectTransform component3 = this.m_content.GetComponent<RectTransform>();
			component3.set_anchorMin(new Vector2(0f, 0.5f));
			component3.set_anchorMax(new Vector2(0f, 0.5f));
			component3.set_pivot(new Vector2(0f, 0.5f));
		}
		else if (scrollStyle == ListView.ListViewScrollStyle.Right)
		{
			RectTransform component4 = this.m_content.GetComponent<RectTransform>();
			component4.set_anchorMin(new Vector2(1f, 0.5f));
			component4.set_anchorMax(new Vector2(1f, 0.5f));
			component4.set_pivot(new Vector2(1f, 0.5f));
		}
	}

	public void Destory()
	{
		this.listView = null;
		if (this.content != null)
		{
			Object.Destroy(this.content);
			this.content = null;
		}
	}
}
