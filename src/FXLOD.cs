using System;
using UnityEngine;

public class FXLOD : MonoBehaviour
{
	public enum FXLODLevel
	{
		Middle = 250,
		High = 300
	}

	public FXLOD.FXLODLevel MinLOD = FXLOD.FXLODLevel.Middle;

	public void Init(bool isLowLod)
	{
		if (this.IsShield(isLowLod))
		{
			base.get_transform().get_gameObject().SetActive(false);
			int childCount = base.get_transform().get_childCount();
			for (int i = 0; i < childCount; i++)
			{
				Transform child = base.get_transform().GetChild(i);
				if (child != null)
				{
					child.get_gameObject().SetActive(false);
				}
			}
		}
	}

	public bool IsShield(bool isLowLod)
	{
		if (isLowLod)
		{
			return this.MinLOD > (FXLOD.FXLODLevel)200;
		}
		return this.MinLOD > (FXLOD.FXLODLevel)GameLevelManager.GameLevelVariable.LODLEVEL;
	}
}
