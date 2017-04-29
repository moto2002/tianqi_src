using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Gradient")]
public class Gradient : BaseMeshEffect
{
	[SerializeField]
	public Color32 topColor = Color.get_white();

	[SerializeField]
	public Color32 bottomColor = Color.get_black();

	private List<UIVertex> listUIVertex = new List<UIVertex>();

	public override void ModifyMesh(VertexHelper vh)
	{
		if (!this.IsActive())
		{
			return;
		}
		this.listUIVertex.Clear();
		vh.GetUIVertexStream(this.listUIVertex);
		if (this.listUIVertex.get_Count() <= 0)
		{
			return;
		}
		float num = this.listUIVertex.get_Item(0).position.y;
		float num2 = this.listUIVertex.get_Item(0).position.y;
		for (int i = 0; i < this.listUIVertex.get_Count(); i++)
		{
			float y = this.listUIVertex.get_Item(i).position.y;
			if (y > num2)
			{
				num2 = y;
			}
			else if (y < num)
			{
				num = y;
			}
		}
		float num3 = num2 - num;
		for (int j = 0; j < this.listUIVertex.get_Count(); j++)
		{
			UIVertex uIVertex = this.listUIVertex.get_Item(j);
			float y2 = uIVertex.position.y;
			float num4 = (y2 - num) / num3;
			float num5 = 1f - num4;
			byte b = (byte)(num4 * (float)this.topColor.r + num5 * (float)this.bottomColor.r);
			byte b2 = (byte)(num4 * (float)this.topColor.g + num5 * (float)this.bottomColor.g);
			byte b3 = (byte)(num4 * (float)this.topColor.b + num5 * (float)this.bottomColor.b);
			byte b4 = (byte)(num4 * (float)this.topColor.a + num5 * (float)this.bottomColor.a);
			uIVertex.color = new Color32(b, b2, b3, b4);
			this.listUIVertex.set_Item(j, uIVertex);
		}
		vh.Clear();
		vh.AddUIVertexTriangleStream(this.listUIVertex);
	}
}
