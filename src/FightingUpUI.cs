using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightingUpUI : UIBase
{
	protected const int BGOpen = 1502;

	protected const int BGIdle = 1505;

	protected const int BGClose = 1504;

	protected const int NumOpen = 1501;

	protected const int NumClose = 1503;

	protected const string prefix = "";

	protected Transform FightingUpUISlot;

	protected GameObject FightingUpUINumGO;

	protected List<Image> FightingUpUINumList = new List<Image>();

	protected int bgFxUID;

	protected int numFxUID;

	protected uint numShowTimer;

	protected uint numStayTimer;

	protected uint numHideTimer;

	protected bool isPlayingBGFxClose;

	protected bool isPlayingNumFxClose;

	public bool IsAnimationEnd;

	protected Action endCallBackAction;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		this.isInterruptStick = false;
		this.isEndNav = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.FightingUpUISlot = base.FindTransform("FightingUpUISlot");
		this.FightingUpUINumGO = base.FindTransform("FightingUpUINumGO").get_gameObject();
		for (int i = 0; i < 10; i++)
		{
			this.FightingUpUINumList.Add(base.FindTransform("FightingUpUINum" + i).GetComponent<Image>());
		}
		for (int j = 0; j < this.FightingUpUINumList.get_Count(); j++)
		{
			this.FightingUpUINumList.get_Item(j).get_gameObject().SetActive(false);
		}
		this.endCallBackAction = null;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.IsAnimationEnd = false;
	}

	public void SetPowerUp(long oldValue, long newValue, Action endCallBack = null)
	{
		if (oldValue >= newValue)
		{
			return;
		}
		this.SetPowerUp(newValue - oldValue, endCallBack);
	}

	public void SetPowerUp(long changeValue, Action endCallBack = null)
	{
		if (changeValue <= 0L)
		{
			return;
		}
		this.IsAnimationEnd = true;
		this.endCallBackAction = endCallBack;
		this.ResetAllState();
		this.SetPowerUpFxAndAnima(changeValue);
	}

	protected void ResetAllState()
	{
		FXSpineManager.Instance.DeleteSpine(this.bgFxUID, false);
		FXSpineManager.Instance.DeleteSpine(this.numFxUID, false);
		TimerHeap.DelTimer(this.numShowTimer);
		TimerHeap.DelTimer(this.numStayTimer);
		TimerHeap.DelTimer(this.numHideTimer);
		this.isPlayingBGFxClose = false;
		this.isPlayingNumFxClose = false;
		this.FightingUpUINumGO.SetActive(false);
		UIUtils.animReset(this.FightingUpUINumGO, "UI2VerticalOpen");
		UIUtils.animReset(this.FightingUpUINumGO, "UI2VerticalClose");
	}

	protected void SetPowerUpFxAndAnima(long newValue)
	{
		this.bgFxUID = FXSpineManager.Instance.PlaySpine(1502, this.FightingUpUISlot, "PowerUpUI", 14999, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.numShowTimer = TimerHeap.AddTimer(100u, 0, delegate
		{
			this.SetNum(newValue, string.Empty);
			this.DoVerticalOpen();
			this.numFxUID = FXSpineManager.Instance.PlaySpine(1501, this.FightingUpUISlot, "PowerUpUI", 15001, null, "UI", 0f, 0f, 0.5f, 1f, false, FXMaskLayer.MaskState.None);
			this.numStayTimer = TimerHeap.AddTimer(1200u, 0, delegate
			{
				this.isPlayingNumFxClose = true;
				FXSpineManager.Instance.DeleteSpine(this.numFxUID, false);
				this.numFxUID = FXSpineManager.Instance.PlaySpine(1503, this.FightingUpUISlot, "PowerUpUI", 15001, delegate
				{
					this.isPlayingNumFxClose = false;
					this.CheckAutoClose();
				}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				this.numHideTimer = TimerHeap.AddTimer(100u, 0, delegate
				{
					this.DoVerticalClose();
					this.isPlayingBGFxClose = true;
					FXSpineManager.Instance.DeleteSpine(this.bgFxUID, false);
					this.bgFxUID = FXSpineManager.Instance.PlaySpine(1504, this.FightingUpUISlot, "PowerUpUI", 14999, delegate
					{
						this.isPlayingBGFxClose = false;
						this.CheckAutoClose();
					}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
				});
			});
		});
	}

	protected void SetNum(long thisValue, string prefix)
	{
		this.FightingUpUINumGO.SetActive(true);
		for (int i = 0; i < this.FightingUpUINumList.get_Count(); i++)
		{
			this.FightingUpUINumList.get_Item(i).get_gameObject().SetActive(false);
		}
		int num = 0;
		string text = '+' + thisValue;
		string text2 = text;
		for (int j = 0; j < text2.get_Length(); j++)
		{
			char c = text2.get_Chars(j);
			this.FightingUpUINumList.get_Item(num).get_gameObject().SetActive(true);
			this.FightingUpUINumList.get_Item(num).get_transform().set_localPosition(new Vector3((float)(-15 * (text.get_Length() - 1) + 30 * num), 0f, 0f));
			if (c == '+')
			{
				ResourceManager.SetSprite(this.FightingUpUINumList.get_Item(num), ResourceManager.GetIconSprite("plus"));
			}
			else
			{
				ResourceManager.SetSprite(this.FightingUpUINumList.get_Item(num), ResourceManager.GetIconSprite(prefix + c));
			}
			this.FightingUpUINumList.get_Item(num).SetNativeSize();
			num++;
		}
	}

	protected void CheckAutoClose()
	{
		if (this.isPlayingBGFxClose || this.isPlayingNumFxClose)
		{
			return;
		}
		if (this.endCallBackAction != null)
		{
			this.endCallBackAction.Invoke();
			return;
		}
		this.IsAnimationEnd = false;
		this.Show(false);
	}

	private void DoVerticalOpen()
	{
		UIUtils.animPlay(this.FightingUpUINumGO, "UI2VerticalOpen");
	}

	private void DoVerticalClose()
	{
		UIUtils.animPlay(this.FightingUpUINumGO, "UI2VerticalClose");
	}
}
