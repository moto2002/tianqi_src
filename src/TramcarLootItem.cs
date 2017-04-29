using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TramcarLootItem : MonoBehaviour
{
	public Action<TramcarLootItem> EventHandler;

	private int mMinLevel;

	private int mMaxLevel;

	private int mTick;

	private Text mTxName;

	private Text mTxFlag;

	private Text mTxCar;

	private Text mTxTime;

	private Text mTxButton;

	private ButtonCustom mBtnLoot;

	private Image mImgHead;

	public bool CanLoot
	{
		get;
		private set;
	}

	public TramcarRoomInfo Info
	{
		get;
		private set;
	}

	public int Now
	{
		get;
		private set;
	}

	private void Awake()
	{
		this.mTxName = UIHelper.GetText(base.get_transform(), "background/txName");
		this.mTxFlag = UIHelper.GetText(base.get_transform(), "background/txFlag");
		this.mTxCar = UIHelper.GetText(base.get_transform(), "background/txCar");
		this.mTxTime = UIHelper.GetText(base.get_transform(), "background/txTime");
		this.mImgHead = UIHelper.GetImage(base.get_transform(), "background/Friend/FriendIcon");
		this.mTxButton = UIHelper.GetText(base.get_transform(), "background/btnLoot/btnLootText");
		this.mBtnLoot = UIHelper.GetCustomButton(base.get_transform(), "background/btnLoot");
		this.mBtnLoot.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickLoot);
	}

	private void OnClickLoot(GameObject go)
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Invoke(this);
		}
	}

	private void RefreshRoom()
	{
		if (this.Info != null)
		{
			this.CanLoot = (EntityWorld.Instance.EntSelf.Lv >= this.mMinLevel && EntityWorld.Instance.EntSelf.Lv <= this.mMaxLevel && !this.Info.isGrab);
			ResourceManager.SetSprite(this.mImgHead, UIUtils.GetRoleSmallIcon(this.Info.career));
			this.mTxName.set_text(this.Info.name);
			this.mTxCar.set_text(TramcarManager.Instance.TRAMCAR_NAME[this.Info.quality]);
			this.mTxFlag.set_text((!this.Info.isEnemy) ? ((!this.Info.isFriend) ? string.Empty : "<color=#159534>好友</color>") : "<color=#cf4e3a>仇人</color>");
			this.Now = this.mTick - this.Info.createTime;
			if (this.Now > 300)
			{
				this.Now = 300;
			}
			this.mTxTime.set_text("已护送时间：" + ((this.Now <= 0) ? "00:00:00" : TimeConverter.GetTime(this.Now, TimeFormat.HHMMSS)));
			this.mTxButton.set_text((!this.CanLoot) ? "不可抢夺" : "抢 夺");
			this.mBtnLoot.set_interactable(this.CanLoot);
		}
	}

	public void SetData(TramcarRoomInfo info, int minLv, int maxLv, int tick)
	{
		this.Info = info;
		this.mMinLevel = minLv;
		this.mMaxLevel = maxLv;
		this.mTick = tick;
		this.RefreshRoom();
	}

	public void SetUnused()
	{
		base.get_gameObject().set_name("Unused");
		base.get_gameObject().SetActive(false);
	}
}
