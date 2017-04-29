using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class DungeonCountDownUI : UIBase
{
	public static DungeonCountDownUI Instance;

	private Text HintText;

	private string content;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		DungeonCountDownUI.Instance = this;
	}

	protected override void InitUI()
	{
		this.HintText = base.FindTransform("HintText").GetComponent<Text>();
		this.content = GameDataUtils.GetChineseContent(511998, false);
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			DungeonCountDownUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	public void UpdateNum(int second)
	{
		this.HintText.set_text(string.Format(this.content, second));
	}
}
