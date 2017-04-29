using System;

public class FinishTipsUI : UIBase
{
	private void Start()
	{
		FXSpineManager.Instance.ReplaySpine(0, 206, base.get_transform(), "FinishTipsUI", 3001, delegate
		{
			UIManagerControl.Instance.UnLoadUIPrefab("FinishTipsUI");
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}
}
