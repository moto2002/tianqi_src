using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MopUpDialogUI : UIBase
{
	private Text TextLast;

	private ButtonCustom BtnMopOneTime;

	private ButtonCustom BtnMopTenTime;

	private ButtonCustom BtnVip;

	private int canMopupTime;

	public int CurrentInstanceID;

	private float yNoVip = 43f;

	private float yVip = 17f;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.isClick = true;
		this.alpha = 0.75f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TextLast = base.FindTransform("TextLast").GetComponent<Text>();
		this.BtnMopOneTime = base.FindTransform("BtnMopOneTime").GetComponent<ButtonCustom>();
		this.BtnMopTenTime = base.FindTransform("BtnMopTenTime").GetComponent<ButtonCustom>();
		this.BtnVip = base.FindTransform("BtnVip").GetComponent<ButtonCustom>();
		this.BtnMopOneTime.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnMopOneTime);
		this.BtnMopTenTime.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnBtnMopTenTime);
		this.BtnVip.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnVip);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.get_transform().SetAsLastSibling();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		EventDispatcher.Broadcast(EventNames.InstanceDetailShouldRefresh);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.CloseMopupBtn, new Callback(this.CloseMopupBtn));
		EventDispatcher.AddListener(EventNames.OpenMopupBtn, new Callback(this.OpenMopupBtn));
		EventDispatcher.AddListener(EventNames.UsedFreeMopUpTimesChange, new Callback(this.UsedFreeMopUpTimesChange));
		EventDispatcher.AddListener(DungeonManagerEvent.InstanceDataHaveChange, new Callback(this.InstanceDataHaveChange));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.CloseMopupBtn, new Callback(this.CloseMopupBtn));
		EventDispatcher.RemoveListener(EventNames.OpenMopupBtn, new Callback(this.OpenMopupBtn));
		EventDispatcher.RemoveListener(EventNames.UsedFreeMopUpTimesChange, new Callback(this.UsedFreeMopUpTimesChange));
		EventDispatcher.RemoveListener(DungeonManagerEvent.InstanceDataHaveChange, new Callback(this.InstanceDataHaveChange));
	}

	private void CloseMopupBtn()
	{
		this.BtnMopOneTime.set_enabled(false);
		this.BtnMopTenTime.set_enabled(false);
		ImageColorMgr.SetImageColor(this.BtnMopOneTime.get_transform().FindChild("ImageBG").GetComponent<Image>(), true);
		ImageColorMgr.SetImageColor(this.BtnMopTenTime.get_transform().FindChild("ImageBG").GetComponent<Image>(), true);
	}

	private void OpenMopupBtn()
	{
		this.BtnMopOneTime.set_enabled(true);
		this.BtnMopTenTime.set_enabled(true);
		ImageColorMgr.SetImageColor(this.BtnMopOneTime.get_transform().FindChild("ImageBG").GetComponent<Image>(), false);
		ImageColorMgr.SetImageColor(this.BtnMopTenTime.get_transform().FindChild("ImageBG").GetComponent<Image>(), false);
	}

	private void UsedFreeMopUpTimesChange()
	{
		this.RefreshUI(this.CurrentInstanceID);
	}

	private void InstanceDataHaveChange()
	{
		this.RefreshUI(this.CurrentInstanceID);
	}

	private void OnClickBtnMopOneTime(GameObject sender)
	{
		MopupManager.Instance.StartMopup(this.CurrentInstanceID, 1);
	}

	private void OnClickBtnBtnMopTenTime(GameObject sender)
	{
		if (VIPPrivilegeManager.Instance.IsOpenSweepSeries())
		{
			MopupManager.Instance.StartMopup(this.CurrentInstanceID, this.canMopupTime);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(510090, false));
		}
	}

	private void OnClickBtnVip(GameObject sender)
	{
		LinkNavigationManager.OpenVIPUI2Privilege();
	}

	public void RefreshUI(int instanceID)
	{
		this.CurrentInstanceID = instanceID;
		this.canMopupTime = DungeonManager.Instance.GetDungeonInfo(this.CurrentInstanceID).remainingChallengeTimes;
		if (this.canMopupTime > 10)
		{
			this.canMopupTime = 10;
		}
		if (this.canMopupTime == 0)
		{
			this.canMopupTime = 3;
		}
		string text = GameDataUtils.GetChineseContent(505134, false);
		text = text.Replace("xx", this.canMopupTime.ToString());
		this.BtnMopTenTime.get_transform().FindChild("Text").GetComponent<Text>().set_text(text);
		int vipLv = EntityWorld.Instance.EntSelf.VipLv;
		if (vipLv >= 5)
		{
			ImageColorMgr.SetImageColor(this.BtnMopTenTime.get_transform().FindChild("ImageBG").GetComponent<Image>(), false);
			this.BtnMopTenTime.GetComponent<ButtonCustom>().set_enabled(true);
			Vector3 vector = this.TextLast.GetComponent<RectTransform>().get_anchoredPosition();
			vector.y = this.yVip;
			this.TextLast.GetComponent<RectTransform>().set_anchoredPosition(vector);
			this.BtnVip.get_gameObject().SetActive(false);
		}
		else
		{
			ImageColorMgr.SetImageColor(this.BtnMopTenTime.get_transform().FindChild("ImageBG").GetComponent<Image>(), true);
			this.BtnMopTenTime.GetComponent<ButtonCustom>().set_enabled(false);
			Vector3 vector2 = this.TextLast.GetComponent<RectTransform>().get_anchoredPosition();
			vector2.y = this.yNoVip;
			this.TextLast.GetComponent<RectTransform>().set_anchoredPosition(vector2);
			this.BtnVip.get_gameObject().SetActive(true);
		}
		int vipTimesByType = VIPPrivilegeManager.Instance.GetVipTimesByType(3);
		int usedFreeMopUpTimes = DungeonManager.Instance.usedFreeMopUpTimes;
		if (vipTimesByType == -1)
		{
			this.TextLast.set_text(GameDataUtils.GetChineseContent(505135, false));
		}
		else
		{
			string text2 = GameDataUtils.GetChineseContent(505133, false);
			text2 = text2.Replace("xx", "<color=white>" + (vipTimesByType - usedFreeMopUpTimes).ToString() + "</color>");
			this.TextLast.set_text(text2);
		}
	}
}
