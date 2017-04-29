using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RedBagUI : UIBase
{
	public static RedBagUI Instance;

	private TimeCountDown m_timeCountDown;

	private Text m_textUpdateTime;

	private Transform FxPanel;

	private Transform FxPanel2;

	private Text m_textInfoStr;

	private int m_startFxId;

	private int m_playFxId;

	private int m_bgFxId;

	private int m_stayFxId;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
		base.hideMainCamera = false;
	}

	private void Awake()
	{
		RedBagUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.FindTransform("BtnEnsure").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickEnSure);
		this.m_textUpdateTime = base.FindTransform("BtnEnsure").FindChild("TextBtnEnsureName").GetComponent<Text>();
		this.FxPanel = base.FindTransform("Fx").get_transform();
		this.FxPanel2 = base.FindTransform("Fx2").get_transform();
		base.FindTransform("BackGround1").GetComponent<DepthOfUINoCollider>().SortingOrder = 2002;
		this.m_textInfoStr = base.FindTransform("TextInfoStr").GetComponent<Text>();
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.m_textUpdateTime.set_text(string.Empty);
		this.RfreshUI();
	}

	protected override void OnDisable()
	{
		this.ClearTimeCountDown();
	}

	protected override void ActionClose()
	{
		base.ActionClose();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			this.ClearFx();
			base.ReleaseSelf(true);
		}
	}

	private void OnClickEnSure(GameObject go)
	{
		this.OnClickClose(null);
	}

	private void OnClickClose(GameObject go)
	{
		this.ClearTimeCountDown();
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
		RedBagManager.Instance.ShowGift();
	}

	protected override void OnClickMaskAction()
	{
		this.OnClickClose(null);
	}

	public void RfreshUI()
	{
		this.m_startFxId = FXSpineManager.Instance.PlaySpine(4501, this.FxPanel, "RedBagUI", 2002, null, "UI", 0f, 0f, 2f, 2f, false, FXMaskLayer.MaskState.None);
		this.m_playFxId = FXSpineManager.Instance.PlaySpine(4503, this.FxPanel2, "RedBagUI", 2000, delegate
		{
			this.KeepPlayRedBagAction();
		}, "UI", 0.5f, -50f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void KeepPlayRedBagAction()
	{
		this.m_bgFxId = FXSpineManager.Instance.PlaySpine(4504, this.FxPanel, "RedBagUI", 2000, null, "UI", 0f, 0f, 2f, 2f, false, FXMaskLayer.MaskState.None);
		this.m_stayFxId = FXSpineManager.Instance.PlaySpine(4502, this.FxPanel2, "RedBagUI", 2002, null, "UI", 0f, -50f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.ShowInfo();
		int time = (int)float.Parse(DataReader<GlobalParams>.Get("hongbao_Time").value);
		this.LeftTimeCountDown(time);
	}

	private void ShowInfo()
	{
		GetRedPacketRes panelData = RedBagManager.Instance.GetPanelData();
		if (panelData != null)
		{
			GetRedPacketRes.RedPacketType type = panelData.Type;
			int taskId = panelData.taskId;
			HongBaoShiJie hongBaoShiJie = DataReader<HongBaoShiJie>.Get(taskId);
			if (hongBaoShiJie == null)
			{
				return;
			}
			string chineseContent = GameDataUtils.GetChineseContent(hongBaoShiJie.Chinese, false);
			string text = string.Empty;
			if (type == GetRedPacketRes.RedPacketType.KillBoss)
			{
				text = string.Format(chineseContent, panelData.parameter.get_Item(0), panelData.parameter.get_Item(1), panelData.parameter.get_Item(2));
			}
			else if (type == GetRedPacketRes.RedPacketType.Recharges)
			{
				text = string.Format(chineseContent, panelData.parameter.get_Item(0), panelData.parameter.get_Item(1));
			}
			else if (type == GetRedPacketRes.RedPacketType.VipLv)
			{
				text = string.Format(chineseContent, panelData.parameter.get_Item(0), panelData.parameter.get_Item(1));
			}
			else if (type == GetRedPacketRes.RedPacketType.TotalVipLv)
			{
				text = string.Format(chineseContent, panelData.parameter.get_Item(0), panelData.parameter.get_Item(1));
			}
			else if (type == GetRedPacketRes.RedPacketType.TotalRechargeTimes)
			{
				text = string.Format(chineseContent, panelData.parameter.get_Item(0));
			}
			else if (type == GetRedPacketRes.RedPacketType.TotalTwistedEggTimes)
			{
				text = string.Format(chineseContent, panelData.parameter.get_Item(0));
			}
			this.m_textInfoStr.set_text(text);
		}
	}

	private void ClearFx()
	{
		if (this.m_startFxId != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.m_startFxId, true);
		}
		if (this.m_playFxId != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.m_playFxId, true);
		}
		if (this.m_bgFxId != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.m_bgFxId, true);
		}
		if (this.m_stayFxId != 0)
		{
			FXSpineManager.Instance.DeleteSpine(this.m_stayFxId, true);
		}
	}

	private void LeftTimeCountDown(int time)
	{
		if (time < 0)
		{
			return;
		}
		string str = "我笑纳了({0})";
		this.m_timeCountDown = new TimeCountDown(time, TimeFormat.SECOND, delegate
		{
			if (this.m_textUpdateTime != null)
			{
				this.m_textUpdateTime.set_text(string.Format(str, this.m_timeCountDown.GetSeconds()));
			}
		}, delegate
		{
			this.OnClickClose(null);
		}, true);
	}

	private void ClearTimeCountDown()
	{
		if (this.m_timeCountDown != null)
		{
			this.m_timeCountDown.Dispose();
			this.m_timeCountDown = null;
		}
	}
}
