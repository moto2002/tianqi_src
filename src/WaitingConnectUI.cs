using System;
using UnityEngine.UI;

public class WaitingConnectUI : UIBase
{
	public Text linName;

	protected override void OnEnable()
	{
		base.OnEnable();
		GuideUIView.IsDownOn = false;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		GuideUIView.IsDownOn = true;
	}

	public void SetLineName(int times)
	{
		if (times > 0)
		{
			this.linName.set_text(string.Format("尝试连接{0}...", times));
		}
		else
		{
			this.linName.set_text("尝试连接...");
		}
	}
}
