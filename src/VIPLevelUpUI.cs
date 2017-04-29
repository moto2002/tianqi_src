using Foundation.Core.Databinding;
using System;
using UnityEngine.UI;

public class VIPLevelUpUI : UIBase
{
	public static VIPLevelUpUI Instance;

	private Image vip1L;

	private Image vip10L;

	private Image vip1R;

	private Image vip10R;

	private int fxid;

	private uint m_timerID;

	private bool canStart1;

	private bool canStart2;

	private void Awake()
	{
		VIPLevelUpUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.InitializeTrans();
		base.SetMask(0.75f, true, false);
	}

	private void InitializeTrans()
	{
		this.vip1L = base.FindTransform("vip1L").GetComponent<Image>();
		this.vip10L = base.FindTransform("vip10L").GetComponent<Image>();
		this.vip1R = base.FindTransform("vip1R").GetComponent<Image>();
		this.vip10R = base.FindTransform("vip10R").GetComponent<Image>();
	}

	public void SetVIPlv(int vipLv)
	{
		int vipLv2 = EntityWorld.Instance.EntSelf.VipLv;
		if (vipLv != vipLv2 && vipLv < vipLv2)
		{
			ResourceManager.SetSprite(this.vip1L, GameDataUtils.GetNumIcon10(vipLv, NumType.Yellow_big));
			this.vip1L.SetNativeSize();
			ResourceManager.SetSprite(this.vip10L, GameDataUtils.GetNumIcon1(vipLv, NumType.Yellow_big));
			this.vip10L.SetNativeSize();
			ResourceManager.SetSprite(this.vip1R, GameDataUtils.GetNumIcon10(vipLv2, NumType.Yellow_big));
			this.vip1R.SetNativeSize();
			ResourceManager.SetSprite(this.vip10R, GameDataUtils.GetNumIcon1(vipLv2, NumType.Yellow_big));
			this.vip10R.SetNativeSize();
		}
		this.AddFX();
	}

	private void Close()
	{
		TimerHeap.DelTimer(this.m_timerID);
		this.m_timerID = 0u;
		this.DeleteFX();
		VIPLevelUpUI.Instance = null;
		this.ReleaseSelf(true);
		FirstPayManager.Instance.CheckFirstPayToVIPUp();
	}

	public void AddFX()
	{
		this.DeleteFX();
		base.FindTransform("VIP").get_gameObject().SetActive(false);
		FXSpineManager.Instance.PlaySpine(706, base.FindTransform("Name"), "VIPLevelUPUI", 15002, delegate
		{
			base.FindTransform("VIP").get_gameObject().SetActive(true);
			this.canStart2 = true;
			this.PlayFX();
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		FXSpineManager.Instance.PlaySpine(707, base.FindTransform("Name"), "VIPLevelUPUI", 15002, delegate
		{
			this.canStart1 = true;
			this.PlayFX();
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void PlayFX()
	{
		if (this.canStart1 && this.canStart2)
		{
			this.fxid = FXSpineManager.Instance.ReplaySpine(this.fxid, 705, base.FindTransform("Name"), "VIPLevelUPUI", 15002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			TimerHeap.DelTimer(this.m_timerID);
			TimerHeap.AddTimer(1000u, 0, new Action(this.Close));
		}
	}

	public void DeleteFX()
	{
		FXSpineManager.Instance.DeleteSpine(this.fxid, true);
	}
}
