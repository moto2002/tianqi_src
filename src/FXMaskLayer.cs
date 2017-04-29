using System;
using UnityEngine;

public class FXMaskLayer : MonoBehaviour
{
	public enum MaskState
	{
		None,
		SizeXZ,
		SizeXY,
		ScaleXY
	}

	public FXMaskLayer.MaskState state = FXMaskLayer.MaskState.SizeXZ;

	private void Start()
	{
		switch (this.state)
		{
		case FXMaskLayer.MaskState.SizeXZ:
			this.SetSize();
			break;
		case FXMaskLayer.MaskState.SizeXY:
			this.SetSize();
			break;
		case FXMaskLayer.MaskState.ScaleXY:
			this.SetScale();
			break;
		}
	}

	private void SetSize()
	{
		int num;
		int num2;
		UIConst.GetRealScreenSize(out num, out num2);
		num += 20;
		num2 += 20;
		if (this.state == FXMaskLayer.MaskState.SizeXZ)
		{
			base.get_transform().set_localScale(new Vector3((float)num, 1f, (float)num2));
		}
		else if (this.state == FXMaskLayer.MaskState.SizeXY)
		{
			base.get_transform().set_localScale(new Vector3((float)num, (float)num2, 1f));
		}
	}

	private void SetScale()
	{
		float num;
		float num2;
		UIConst.GetRealScreenScale(out num, out num2);
		if (this.state == FXMaskLayer.MaskState.ScaleXY)
		{
			base.get_transform().set_localScale(new Vector3(num, num2, 1f));
		}
	}
}
