using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PVPUpUIView : UIBase
{
	public static PVPUpUIView Instance;

	private Transform SpineRoot;

	private Transform FXSpine2Root;

	private int fx_1;

	private int fx_2;

	private int fx_loop_1;

	private int fx_loop_2;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.92f;
		this.isClick = true;
	}

	private void Awake()
	{
		PVPUpUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.SpineRoot = base.FindTransform("SpineRoot");
		this.FXSpine2Root = base.FindTransform("FXSpine2Root");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		UIQueueManager.Instance.Islocked = false;
		FXSpineManager.Instance.DeleteSpine(this.fx_1, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_2, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_loop_1, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_loop_2, true);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			PVPUpUIView.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void RefreshUI()
	{
		ResourceManager.SetSprite(base.FindTransform("IntegralIcon").GetComponent<Image>(), ResourceManager.GetIconSprite(PVPManager.Instance.GetIntegralByScore(PVPManager.Instance.PVPData.score, true)));
		base.FindTransform("Desc").GetComponent<Text>().set_text(string.Format(GameDataUtils.GetChineseContent(503010, false), PVPManager.Instance.GetIntegralLevelName(PVPManager.Instance.PVPData.score)));
	}

	public void FXSpine1()
	{
		this.fx_1 = FXSpineManager.Instance.PlaySpine(3103, this.SpineRoot, "PVPUpUI", 16005, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	public void FXSpine2()
	{
		this.fx_2 = FXSpineManager.Instance.PlaySpine(3104, this.FXSpine2Root, "PVPUpUI", 16005, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.fx_loop_1 = FXSpineManager.Instance.ReplaySpine(this.fx_loop_1, 3101, this.FXSpine2Root, "PVPUpUI", 15999, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.fx_loop_2 = FXSpineManager.Instance.ReplaySpine(this.fx_loop_2, 3102, this.FXSpine2Root, "PVPUpUI", 16001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}
}
