using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EliteNewOpenCopyTipUI : UIBase
{
	private Text textContent;

	private Transform rankInfoIconExplodeFxSlot;

	private int rankFxID;

	private uint timerID;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.SetMask(0.7f, true, true);
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.textContent = base.FindTransform("TextContent").GetComponent<Text>();
		this.rankInfoIconExplodeFxSlot = base.FindTransform("RankInfoIconExplodeFxSlot");
		this.textContent.set_text(string.Empty);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.OpenRankFXAnim();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		FXSpineManager.Instance.DeleteSpine(this.rankFxID, true);
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void OpenRankFXAnim()
	{
		FXSpineManager.Instance.ReplaySpine(this.rankFxID, 4404, this.rankInfoIconExplodeFxSlot, string.Empty, 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	public void RefreshUI(string content)
	{
		this.textContent.set_text(content);
	}
}
