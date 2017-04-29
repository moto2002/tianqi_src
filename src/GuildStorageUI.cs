using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildStorageUI : UIBase
{
	private GameObject noGuildStorageTipObj;

	private ScrollRect storageLogListSR;

	private ButtonCustom btnDecompose;

	private ButtonCustom btnDonate;

	private ButtonCustom btnOk;

	private ButtonCustom btnCancle;

	private ListPool storageBagListPool;

	private ListPool storageLogListPool;

	private Text storageNumText;

	private Text changeNumText;

	private Image coinImg;

	private Text coinNumText;

	private bool isDecomposeNow;

	private List<EquipSimpleInfo> selectDecomposeEquips;

	private List<Goods> temp_smelt_equips = new List<Goods>();

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.SetMask(0.7f, true, false);
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.storageLogListSR = base.FindTransform("StorageLogListSR").GetComponent<ScrollRect>();
		this.noGuildStorageTipObj = base.FindTransform("NoGuildStorageLog").get_gameObject();
		this.btnDecompose = base.FindTransform("BtnDecompose").GetComponent<ButtonCustom>();
		this.btnDecompose.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickDecompose);
		this.btnDonate = base.FindTransform("BtnDonate").GetComponent<ButtonCustom>();
		this.btnDonate.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickDonate);
		this.btnOk = base.FindTransform("BtnOK").GetComponent<ButtonCustom>();
		this.btnOk.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnOK);
		this.btnCancle = base.FindTransform("BtnCancel").GetComponent<ButtonCustom>();
		this.btnCancle.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnCancle);
		this.coinImg = base.FindTransform("Coin01Icon").GetComponent<Image>();
		this.coinNumText = base.FindTransform("Coin01Num").GetComponent<Text>();
		this.storageNumText = base.FindTransform("StorageNum").GetComponent<Text>();
		this.changeNumText = base.FindTransform("HadChangedNum").GetComponent<Text>();
		this.storageBagListPool = base.FindTransform("BagListPool").GetComponent<ListPool>();
		this.storageBagListPool.Clear();
		this.storageLogListPool = base.FindTransform("LogListPool").GetComponent<ListPool>();
		this.storageLogListPool.Clear();
		ResourceManager.SetSprite(this.coinImg, GameDataUtils.GetItemIcon(19));
		this.selectDecomposeEquips = new List<EquipSimpleInfo>();
		this.selectDecomposeEquips.Clear();
		if (this.noGuildStorageTipObj != null && this.noGuildStorageTipObj.get_activeSelf())
		{
			this.noGuildStorageTipObj.SetActive(false);
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.SetBtnsVisible(false);
		this.storageLogListSR.set_verticalNormalizedPosition(1f);
		GuildStorageManager.Instance.SendQueryGuildStorageInfoReq();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnQueryGuildStorageInfoRes, new Callback(this.OnQueryGuildStorageInfoRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnQueryGuildStorageInfoRes, new Callback(this.OnQueryGuildStorageInfoRes));
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void RefreshUI()
	{
		this.UpdateStorageBagList();
		this.UpdateStorageLogList();
		this.UpdateStorageNum();
		this.UpdatePersonalInfo();
	}

	private void UpdatePersonalInfo()
	{
		if (GuildStorageManager.Instance.GuildStorageInfo != null && GuildStorageManager.Instance.GuildStoragePersonalInfo != null)
		{
			this.coinNumText.set_text(GuildStorageManager.Instance.GuildStoragePersonalInfo.points + string.Empty);
		}
	}

	private void UpdateStorageNum()
	{
		if (GuildStorageManager.Instance.GuildStorageInfo != null && GuildStorageManager.Instance.GuildStoragePersonalInfo != null)
		{
			this.storageNumText.set_text(GuildStorageManager.Instance.GuildStorageInfo.baseInfo.size + "/" + GuildStorageManager.Instance.GuildStorageInfo.baseInfo.capacity);
			this.changeNumText.set_text(GuildStorageManager.Instance.GuildStoragePersonalInfo.exchanges + "/" + GuildStorageManager.Instance.GuildStorageInfo.baseInfo.exchanges);
		}
	}

	private void UpdateStorageLogList()
	{
		this.storageLogListPool.Clear();
		if (GuildStorageManager.Instance.GuildStorageInfo != null && GuildStorageManager.Instance.GuildStorageInfo.logTraces != null && GuildStorageManager.Instance.GuildStorageInfo.logTraces.get_Count() > 0)
		{
			if (this.noGuildStorageTipObj != null && this.noGuildStorageTipObj.get_activeSelf())
			{
				this.noGuildStorageTipObj.SetActive(false);
			}
			List<GuildLogTrace> guildStorageList = GuildStorageManager.Instance.GetGuildStorageLogList(GuildStorageManager.Instance.GuildStorageInfo.logTraces);
			this.storageLogListPool.Create(guildStorageList.get_Count(), delegate(int index)
			{
				if (index < guildStorageList.get_Count() && index < this.storageLogListPool.Items.get_Count())
				{
					GuildLogItem component = this.storageLogListPool.Items.get_Item(index).GetComponent<GuildLogItem>();
					if (component != null)
					{
						component.RefreshUI(guildStorageList.get_Item(index));
					}
				}
			});
		}
		else if (this.noGuildStorageTipObj != null && !this.noGuildStorageTipObj.get_activeSelf())
		{
			this.noGuildStorageTipObj.SetActive(true);
		}
	}

	private void UpdateStorageBagList()
	{
		this.storageBagListPool.Clear();
		if (GuildStorageManager.Instance.GuildStorageInfo != null)
		{
			int storageSize = GuildStorageManager.Instance.GuildStorageInfo.baseInfo.size;
			int storageTotalCount = GuildStorageManager.Instance.GuildStorageInfo.baseInfo.capacity;
			int specialItemCount = (GuildStorageManager.Instance.GuildStorageInfo.baseInfo.items == null) ? 0 : GuildStorageManager.Instance.GuildStorageInfo.baseInfo.items.get_Count();
			this.storageBagListPool.Create(storageTotalCount, delegate(int index)
			{
				if (index < storageTotalCount && index < this.storageBagListPool.Items.get_Count())
				{
					GuildStorageBagItem component = this.storageBagListPool.Items.get_Item(index).GetComponent<GuildStorageBagItem>();
					this.storageBagListPool.Items.get_Item(index).GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectBagItem);
					if (component != null)
					{
						if (index < storageSize)
						{
							if (GuildStorageManager.Instance.GuildStorageInfo.baseInfo.items != null && index < GuildStorageManager.Instance.GuildStorageInfo.baseInfo.items.get_Count())
							{
								component.UpdateItemData(GuildStorageManager.Instance.GuildStorageInfo.baseInfo.items.get_Item(index));
							}
							else
							{
								int num = index - specialItemCount;
								if (num >= 0 && num < GuildStorageManager.Instance.GuildStorageInfo.equipsInfo.get_Count())
								{
									component.UpdateEquipItemData(GuildStorageManager.Instance.GuildStorageInfo.equipsInfo.get_Item(num));
								}
							}
						}
						else
						{
							component.UpdateItemNull();
						}
					}
				}
			});
		}
	}

	private void SetBtnsVisible(bool isSelectNow)
	{
		this.isDecomposeNow = isSelectNow;
		this.btnCancle.get_gameObject().SetActive(this.isDecomposeNow);
		this.btnOk.get_gameObject().SetActive(this.isDecomposeNow);
		this.btnDecompose.get_gameObject().SetActive(!this.isDecomposeNow);
		this.btnDonate.get_gameObject().SetActive(!this.isDecomposeNow);
	}

	private void SetAllBagUnCheck()
	{
		if (this.selectDecomposeEquips != null)
		{
			this.selectDecomposeEquips.Clear();
		}
		for (int i = 0; i < this.storageBagListPool.Items.get_Count(); i++)
		{
			GuildStorageBagItem component = this.storageBagListPool.Items.get_Item(i).GetComponent<GuildStorageBagItem>();
			if (component != null)
			{
				component.Selected = false;
			}
		}
	}

	private void SetSelectBagCheck()
	{
		if (this.selectDecomposeEquips != null)
		{
			for (int i = 0; i < this.selectDecomposeEquips.get_Count(); i++)
			{
				EquipSimpleInfo equipSimpleInfo = this.selectDecomposeEquips.get_Item(i);
				for (int j = 0; j < this.storageBagListPool.Items.get_Count(); j++)
				{
					GuildStorageBagItem component = this.storageBagListPool.Items.get_Item(j).GetComponent<GuildStorageBagItem>();
					if (component != null && component.m_EquipSimpleInfo != null && !component.isSpecialItem && equipSimpleInfo.equipId == component.m_EquipSimpleInfo.equipId && equipSimpleInfo.cfgId == component.m_EquipSimpleInfo.cfgId)
					{
						component.Selected = true;
					}
				}
			}
		}
	}

	private void OnClickSelectBagItem(GameObject go)
	{
		if (!this.isDecomposeNow)
		{
			if (GuildStorageManager.Instance.GuildStorageInfo != null && GuildStorageManager.Instance.GuildStorageInfo.baseInfo != null && GuildStorageManager.Instance.GuildStorageInfo.baseInfo.exchanges <= GuildStorageManager.Instance.GuildStoragePersonalInfo.exchanges)
			{
				DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(621601, false), null, GameDataUtils.GetChineseContent(505114, false), "button_orange_1", null);
				return;
			}
			GuildStorageBagItem bagItem = go.GetComponent<GuildStorageBagItem>();
			if (bagItem != null && bagItem.m_SpecialItemInfo != null && bagItem.isSpecialItem)
			{
				int specialItemChangePoint = GuildStorageManager.Instance.GetSpecialItemChangePoint();
				UIManagerControl.Instance.OpenUI("BuyUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
				BuyUIViewModel.Instance.BuyNumberAdjustOn = true;
				BuyUIViewModel.Instance.BuyCallback = delegate(int count)
				{
					ItemBriefInfo itemBriefInfo = new ItemBriefInfo();
					itemBriefInfo.cfgId = bagItem.m_SpecialItemInfo.cfgId;
					itemBriefInfo.uId = bagItem.m_SpecialItemInfo.uId;
					itemBriefInfo.count = (long)count;
					GuildStorageManager.Instance.SendGuildStorageExchangeReq(null, itemBriefInfo);
				};
				BuyUIViewModel.Instance.RefreshInfo(bagItem.m_SpecialItemInfo.cfgId, (int)bagItem.m_SpecialItemInfo.count, specialItemChangePoint, 19, "兑换", "兑换物品");
			}
			else if (bagItem != null && bagItem.m_EquipSimpleInfo != null)
			{
				EquipItemChangeUI equipItemChangeUI = UIManagerControl.Instance.OpenUI("EquipItemChangeUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as EquipItemChangeUI;
				int points = GuildStorageManager.Instance.GuildStoragePersonalInfo.points;
				equipItemChangeUI.SetEquipItemData(bagItem.m_EquipSimpleInfo, points);
				equipItemChangeUI.ActionClickBtnCB = delegate
				{
					EquipBriefInfo equipBriefInfo = new EquipBriefInfo();
					equipBriefInfo.equipId = bagItem.m_EquipSimpleInfo.equipId;
					if (DataReader<zZhuangBeiPeiZhiBiao>.Contains(bagItem.m_EquipSimpleInfo.cfgId))
					{
						equipBriefInfo.position = DataReader<zZhuangBeiPeiZhiBiao>.Get(bagItem.m_EquipSimpleInfo.cfgId).position;
					}
					GuildStorageManager.Instance.SendGuildStorageExchangeReq(equipBriefInfo, null);
				};
			}
		}
		else
		{
			GuildStorageBagItem bagItem = go.GetComponent<GuildStorageBagItem>();
			if (bagItem != null && !bagItem.isSpecialItem && bagItem.m_EquipSimpleInfo != null)
			{
				bagItem.Selected = !bagItem.Selected;
				int num = this.selectDecomposeEquips.FindIndex((EquipSimpleInfo a) => a.equipId == bagItem.m_EquipSimpleInfo.equipId && a.cfgId == bagItem.m_EquipSimpleInfo.cfgId);
				if (num >= 0 && !bagItem.Selected)
				{
					this.selectDecomposeEquips.RemoveAt(num);
				}
				else if (num < 0 && bagItem.Selected)
				{
					this.selectDecomposeEquips.Add(bagItem.m_EquipSimpleInfo);
				}
			}
		}
	}

	private void OnClickDecompose(GameObject go)
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		if (!GuildManager.Instance.IsGuildCaptain(EntityWorld.Instance.EntSelf.ID))
		{
			UIManagerControl.Instance.ShowToastText("只有团长才能操作");
			return;
		}
		List<MultiCheckUIViewModel.ItemData> list = new List<MultiCheckUIViewModel.ItemData>();
		list.Add(new MultiCheckUIViewModel.ItemData
		{
			id = 1,
			name = TextColorMgr.GetColorByQuality("橙色两勾玉", 5),
			isOn = false
		});
		list.Add(new MultiCheckUIViewModel.ItemData
		{
			id = 2,
			name = TextColorMgr.GetColorByQuality("橙色三勾玉", 5),
			isOn = false
		});
		list.Add(new MultiCheckUIViewModel.ItemData
		{
			id = 3,
			name = TextColorMgr.GetColorByQuality("金色两勾玉", 6),
			isOn = false
		});
		list.Add(new MultiCheckUIViewModel.ItemData
		{
			id = 4,
			name = TextColorMgr.GetColorByQuality("金色三勾玉", 6),
			isOn = false
		});
		MultiCheckUIViewModel.Instance.ShowAsConfirm("批量选择", list, delegate
		{
			List<int> list2 = new List<int>();
			for (int i = 1; i <= 3; i++)
			{
				if (MultiCheckUIViewModel.Instance.IsOn(i))
				{
					list2.Add(i);
				}
			}
			this.selectDecomposeEquips = GuildStorageManager.Instance.GetSelectDecomposeEquips(list2);
			this.SetSelectBagCheck();
			this.SetBtnsVisible(true);
		}, GameDataUtils.GetChineseContent(505114, false), "button_orange_1", UINodesManager.MiddleUIRoot, GameDataUtils.GetChineseContent(621607, false));
	}

	private void OnClickDonate(GameObject go)
	{
		if (GuildStorageManager.Instance.GuildStorageInfo != null && GuildStorageManager.Instance.GuildStorageInfo.baseInfo != null && GuildStorageManager.Instance.GuildStorageInfo.baseInfo.size >= GuildStorageManager.Instance.GuildStorageInfo.baseInfo.capacity)
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(621603, false), null, GameDataUtils.GetChineseContent(505114, false), "button_orange_1", null);
			return;
		}
		GuildDonateUI guildDonateUI = UIManagerControl.Instance.OpenUI("GuildDonateUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as GuildDonateUI;
		guildDonateUI.get_transform().SetAsLastSibling();
	}

	private void OnClickBtnOK(GameObject go)
	{
		if (this.selectDecomposeEquips != null && this.selectDecomposeEquips.get_Count() > 0)
		{
			List<int> list = new List<int>();
			List<EquipBriefInfo> equipItemList = new List<EquipBriefInfo>();
			for (int i = 0; i < this.selectDecomposeEquips.get_Count(); i++)
			{
				EquipBriefInfo equipBriefInfo = new EquipBriefInfo();
				equipBriefInfo.equipId = this.selectDecomposeEquips.get_Item(i).equipId;
				if (DataReader<zZhuangBeiPeiZhiBiao>.Contains(this.selectDecomposeEquips.get_Item(i).cfgId))
				{
					equipBriefInfo.position = DataReader<zZhuangBeiPeiZhiBiao>.Get(this.selectDecomposeEquips.get_Item(i).cfgId).position;
				}
				list.Add(this.selectDecomposeEquips.get_Item(i).cfgId);
				equipItemList.Add(equipBriefInfo);
			}
			int decomposeEquipTotalGuildFund = GuildStorageManager.Instance.GetDecomposeEquipTotalGuildFund(list);
			string content = string.Format(GameDataUtils.GetChineseContent(621604, false), decomposeEquipTotalGuildFund);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), content, delegate
			{
				this.SetAllBagUnCheck();
				this.SetBtnsVisible(false);
			}, delegate
			{
				GuildStorageManager.Instance.SendGuildStorageDecomposeEquipReq(equipItemList);
				this.SetAllBagUnCheck();
				this.SetBtnsVisible(false);
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(621606, false));
		}
	}

	private void OnClickBtnCancle(GameObject go)
	{
		this.SetBtnsVisible(false);
		this.SetAllBagUnCheck();
	}

	private void OnQueryGuildStorageInfoRes()
	{
		this.RefreshUI();
	}
}
