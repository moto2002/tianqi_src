using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TramcarFriendItem : MonoBehaviour
{
	public Action<TramcarFriendItem> EventHandler;

	public Action<bool, TramcarFriendItem> InviteEventHandler;

	private int mMinLevel;

	private int mMaxLevel;

	private Text mTxName;

	private Text mTxLevel;

	private Text mTxPower;

	private Text mTxTips;

	private Text mTxCar;

	private Image mImgIcon;

	private Image mImgTips;

	private Image mImgVipLv1;

	private Image mImgVipLv2;

	private ButtonCustom mBtnInvite;

	private ButtonCustom mBtnAgree;

	private ButtonCustom mBtnReject;

	public BuddyInfo Info
	{
		get;
		private set;
	}

	public int ProtectTimes
	{
		get;
		private set;
	}

	private void Awake()
	{
		this.mTxName = UIHelper.GetText(base.get_transform(), "background/txName");
		this.mTxLevel = UIHelper.GetText(base.get_transform(), "background/txLevel");
		this.mTxPower = UIHelper.GetText(base.get_transform(), "background/BattlePower/BattlePowerValue");
		this.mTxTips = UIHelper.GetText(base.get_transform(), "background/txTips");
		this.mTxCar = UIHelper.GetText(base.get_transform(), "background/txCar");
		this.mImgIcon = UIHelper.GetImage(base.get_transform(), "background/Friend/FriendIcon");
		this.mImgTips = UIHelper.GetImage(base.get_transform(), "background/ImgTips");
		this.mImgVipLv1 = UIHelper.GetImage(base.get_transform(), "background/VIP/VIPLevel1");
		this.mImgVipLv2 = UIHelper.GetImage(base.get_transform(), "background/VIP/VIPLevel2");
		this.mBtnInvite = UIHelper.GetCustomButton(base.get_transform(), "background/btnInvite");
		this.mBtnAgree = UIHelper.GetCustomButton(base.get_transform(), "background/btnAgree");
		this.mBtnReject = UIHelper.GetCustomButton(base.get_transform(), "background/btnReject");
		this.mBtnInvite.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickInvite);
		this.mBtnAgree.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAgree);
		this.mBtnReject.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickReject);
	}

	private void OnClickInvite(GameObject go)
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Invoke(this);
		}
	}

	private void OnClickReject(GameObject go)
	{
		if (this.InviteEventHandler != null)
		{
			this.InviteEventHandler.Invoke(false, this);
		}
	}

	private void OnClickAgree(GameObject go)
	{
		if (this.InviteEventHandler != null)
		{
			this.InviteEventHandler.Invoke(true, this);
		}
	}

	private void RefreshRoom()
	{
		if (this.Info != null)
		{
			ResourceManager.SetSprite(this.mImgIcon, UIUtils.GetRoleSmallIcon(this.Info.career));
			this.mTxName.set_text(this.Info.name);
			this.mTxLevel.set_text("Lv" + this.Info.lv);
			this.mTxPower.set_text(this.Info.fighting.ToString());
			ResourceManager.SetSprite(this.mImgVipLv1, GameDataUtils.GetNumIcon10(this.Info.vipLv, NumType.Yellow_light));
			ResourceManager.SetSprite(this.mImgVipLv2, GameDataUtils.GetNumIcon1(this.Info.vipLv, NumType.Yellow_light));
			if (this.Info.lv > this.mMaxLevel)
			{
				this.mBtnInvite.get_gameObject().SetActive(false);
				this.mImgTips.get_gameObject().SetActive(true);
				this.mTxTips.get_gameObject().SetActive(false);
				ResourceManager.SetSprite(this.mImgTips, ResourceManager.GetIconSprite("hs_djgg"));
			}
			else if (this.Info.lv < this.mMinLevel)
			{
				this.mBtnInvite.get_gameObject().SetActive(false);
				this.mImgTips.get_gameObject().SetActive(true);
				this.mTxTips.get_gameObject().SetActive(false);
				ResourceManager.SetSprite(this.mImgTips, ResourceManager.GetIconSprite("hs_djgd"));
			}
			else if (this.ProtectTimes == 0)
			{
				this.mBtnInvite.get_gameObject().SetActive(false);
				this.mImgTips.get_gameObject().SetActive(true);
				this.mTxTips.get_gameObject().SetActive(false);
				ResourceManager.SetSprite(this.mImgTips, ResourceManager.GetIconSprite("hs_csbz"));
			}
			else if (this.ProtectTimes == -1)
			{
				this.mBtnInvite.get_gameObject().SetActive(false);
				this.mImgTips.get_gameObject().SetActive(false);
				this.mTxTips.get_gameObject().SetActive(true);
			}
			else
			{
				this.mBtnInvite.get_gameObject().SetActive(true);
				this.mImgTips.get_gameObject().SetActive(false);
				this.mTxTips.get_gameObject().SetActive(false);
			}
		}
	}

	public void SetData(BuddyInfo info, int times, int minLv, int maxLv)
	{
		this.Info = info;
		this.ProtectTimes = times;
		this.mMinLevel = minLv;
		this.mMaxLevel = maxLv;
		this.RefreshRoom();
	}

	public void SetData(int quality, BuddyInfo info)
	{
		this.SetData(info, -1, 0, 0);
		this.mTxCar.set_text(TramcarManager.Instance.TRAMCAR_NAME[quality]);
		this.mTxCar.get_gameObject().SetActive(true);
		this.mBtnAgree.get_gameObject().SetActive(true);
		this.mBtnReject.get_gameObject().SetActive(true);
		this.mBtnInvite.get_gameObject().SetActive(false);
		this.mImgTips.get_gameObject().SetActive(false);
		this.mTxTips.get_gameObject().SetActive(false);
	}

	public void SetUnused()
	{
		base.get_gameObject().set_name("Unused");
		base.get_gameObject().SetActive(false);
	}
}
