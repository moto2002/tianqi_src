using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TramcarLootUI : UIBase
{
	private GameObject mLootPanel;

	private Text mTxMapName;

	private Text mTxLootTimes;

	private Text mTxCountDown;

	private TimeCountDown mLootTimeCountDown;

	private List<TramcarLootItem> mLootList;

	private KuangCheDiTuPeiZhi mData;

	private string mStrCD;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.alpha = 0.7f;
		this.isMask = true;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mLootList = new List<TramcarLootItem>();
		this.mLootPanel = base.FindTransform("Grid").get_gameObject();
		this.mTxMapName = base.FindTransform("txMapName").GetComponent<Text>();
		this.mTxLootTimes = base.FindTransform("txLootTimes").GetComponent<Text>();
		this.mTxCountDown = base.FindTransform("txCountDown").GetComponent<Text>();
		base.FindTransform("BtnRefresh").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRefresh);
		this.mStrCD = GameDataUtils.GetChineseContent(513678, false);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		this.mData = DataReader<KuangCheDiTuPeiZhi>.Get(TramcarUI.LastSelectMapId);
		if (this.mData != null)
		{
			this.mTxMapName.set_text(GameDataUtils.GetChineseContent(this.mData.name, false));
		}
		TramcarManager.Instance.SendOpenGrabPanelReq(TramcarUI.LastSelectMapId);
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(EventNames.OpenTramcarLootList, new Callback(this.RefreshLootList));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.OpenTramcarLootList, new Callback(this.RefreshLootList));
	}

	private void OnClickLoot(TramcarLootItem item)
	{
		if (item != null && item.Info != null)
		{
			if (item.CanLoot)
			{
				if (item.Now > 240)
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513675, false));
				}
				else
				{
					TramcarManager.Instance.SendEnterGrabReq(TramcarUI.LastSelectMapId, item.Info.roleId, item.Info.quality);
				}
			}
			else
			{
				UIManagerControl.Instance.ShowToastText("对方被抢夺中，稍后重试！");
			}
		}
	}

	private void OnClickRefresh(GameObject go)
	{
		TramcarManager.Instance.SendOpenGrabPanelReq(TramcarUI.LastSelectMapId);
	}

	private void RefreshLootList()
	{
		this.ClearLootList();
		int preciseServerSecond = TimeManager.Instance.PreciseServerSecond;
		List<TramcarRoomInfo> tramcarLootInfos = TramcarManager.Instance.TramcarLootInfos;
		if (tramcarLootInfos != null)
		{
			tramcarLootInfos.Sort(new Comparison<TramcarRoomInfo>(this.SortLootList));
			for (int i = 0; i < tramcarLootInfos.get_Count(); i++)
			{
				this.CreateLoot(tramcarLootInfos.get_Item(i), preciseServerSecond);
			}
		}
		this.mTxLootTimes.set_text("当日剩余抢夺次数：" + TramcarManager.Instance.FightInfo.todayGrabTimes);
		int num = TramcarManager.Instance.LootCountDown + TramcarManager.Instance.LootTimeLimit - preciseServerSecond;
		if (num > 0)
		{
			this.AddLootCountDown(num);
		}
		else
		{
			this.mTxCountDown.set_text(this.mStrCD + "00:00");
		}
	}

	private int SortLootList(TramcarRoomInfo a, TramcarRoomInfo b)
	{
		if (a.quality > b.quality)
		{
			return 1;
		}
		if (a.quality < b.quality)
		{
			return -1;
		}
		return 0;
	}

	private void ClearLootList()
	{
		for (int i = 0; i < this.mLootList.get_Count(); i++)
		{
			this.mLootList.get_Item(i).SetUnused();
		}
	}

	private void CreateLoot(TramcarRoomInfo info, int tick)
	{
		if (info != null)
		{
			TramcarLootItem tramcarLootItem = this.mLootList.Find((TramcarLootItem e) => e.get_gameObject().get_name() == "Unused");
			if (tramcarLootItem == null)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("TramcarLootItem");
				UGUITools.SetParent(this.mLootPanel, instantiate2Prefab, false);
				tramcarLootItem = instantiate2Prefab.GetComponent<TramcarLootItem>();
				tramcarLootItem.EventHandler = new Action<TramcarLootItem>(this.OnClickLoot);
				this.mLootList.Add(tramcarLootItem);
			}
			tramcarLootItem.SetData(info, this.mData.minLv, this.mData.maxLv, tick);
			tramcarLootItem.get_gameObject().set_name("Tramcar" + info.quality);
			tramcarLootItem.get_gameObject().SetActive(true);
		}
	}

	private void AddLootCountDown(int remianTime)
	{
		this.RemoveLootCountDown();
		this.mLootTimeCountDown = new TimeCountDown(remianTime, TimeFormat.SECOND, delegate
		{
			this.mTxCountDown.set_text(this.mStrCD + TimeConverter.GetTime(this.mLootTimeCountDown.GetSeconds(), TimeFormat.MMSS));
		}, delegate
		{
			this.mTxCountDown.set_text(this.mStrCD + "00:00");
		}, true);
	}

	public void RemoveLootCountDown()
	{
		if (this.mLootTimeCountDown != null)
		{
			this.mLootTimeCountDown.Dispose();
			this.mLootTimeCountDown = null;
		}
	}
}
