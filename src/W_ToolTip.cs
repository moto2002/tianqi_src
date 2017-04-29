using System;
using UnityEngine;

public class W_ToolTip : PropertyAttribute
{
	public readonly string tooltip;

	public W_ToolTip(string tooltip)
	{
		this.tooltip = tooltip;
	}
}
