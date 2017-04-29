using Foundation.Core;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GuildStoveUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string GuildStoveSubs = "GuildStoveSubs";

		public const string SmeltEquips = "SmeltEquips";

		public const string EqiupParts = "EqiupParts";
	}

	private const int INDEX_TO_EQUIP_SMELT_UI = 0;

	private const int INDEX_TO_EQUIP_BUILD_UI = 1;

	public static GuildStoveUIViewModel Instance;

	public ObservableCollection<OOButtonToggle2SubUI> GuildStoveSubs = new ObservableCollection<OOButtonToggle2SubUI>();

	public ObservableCollection<BackpackObservableItem> SmeltEquips = new ObservableCollection<BackpackObservableItem>();

	public ObservableCollection<OOStoveBuildEquipPartCheck> EqiupParts = new ObservableCollection<OOStoveBuildEquipPartCheck>();

	private int CurrentSubIndex;

	private List<Goods> temp_smelt_equips = new List<Goods>();

	private int CurrentEqupPartID = 1;

	private Dictionary<int, long> return_list = new Dictionary<int, long>();

	protected override void Awake()
	{
		GuildStoveUIViewModel.Instance = this;
		base.Awake();
		this.SetGuildStoveSubs();
	}

	private void OnEnable()
	{
		this.CurrentSubIndex = 0;
		this.CurrentEqupPartID = 1;
		this.RefreshUI();
	}

	private void OnDisable()
	{
		GuildManager.Instance.IsBuildEquipWaitting = false;
	}

	public void RefreshUI()
	{
		GuildStoveUI.Instance.RefreshGuildContribution();
		GuildStoveUI.Instance.RefreshEqiupEssence();
		GuildStoveUI.Instance.ShowSubUI(this.CurrentSubIndex);
		if (this.CurrentSubIndex == 0)
		{
			this.RefreshEquipSmeltUI();
		}
		else if (this.CurrentSubIndex == 1)
		{
			this.RefreshEquipBuildUI();
		}
	}

	private void RefreshEquipSmeltUI()
	{
		this.SetSmeltEqiups();
		GuildStoveUI.Instance.SetEquipSmeltUITips01(GuildManager.Instance.GetEquipSmeltDayFund(), this.GetMaxFund());
	}

	private void RefreshEquipBuildUI()
	{
		this.SetEqiupParts();
		this.SetBuildLimit(GuildManager.Instance.GetGuildLevel(), EntityWorld.Instance.EntSelf.Lv, GuildManager.Instance.GetEquipBuildDayTimes());
	}

	private void SetGuildStoveSubs()
	{
		this.GuildStoveSubs.Clear();
		this.GuildStoveSubs.Add(this.GetGuildStoveSubs(0, "装备熔炼", true));
		this.GuildStoveSubs.Add(this.GetGuildStoveSubs(1, "装备打造", false));
	}

	private OOButtonToggle2SubUI GetGuildStoveSubs(int index, string name, bool toggleOn)
	{
		OOButtonToggle2SubUI oOButtonToggle2SubUI = new OOButtonToggle2SubUI();
		oOButtonToggle2SubUI.ToggleIndex = index;
		oOButtonToggle2SubUI.Action2CallBack = new Action<int>(this.OnStoveSubClick);
		oOButtonToggle2SubUI.Name = name;
		oOButtonToggle2SubUI.IsTip = false;
		oOButtonToggle2SubUI.SetIsToggleOn(toggleOn);
		return oOButtonToggle2SubUI;
	}

	private void OnStoveSubClick(int index)
	{
		for (int i = 0; i < this.GuildStoveSubs.Count; i++)
		{
			this.GuildStoveSubs[i].SetIsToggleOn(i == index);
		}
		if (this.CurrentSubIndex != index)
		{
			this.CurrentSubIndex = index;
			this.RefreshUI();
		}
	}

	public void SetSmeltEqiups()
	{
		this.SmeltEquips.Clear();
		BackpackManager.Instance.GetEquimentGoods(ref this.temp_smelt_equips, 4, true);
		if (this.temp_smelt_equips != null && this.temp_smelt_equips.get_Count() > 0)
		{
			GuildStoveUI.Instance.SetIsEquipSmeltsEmpty(false);
			for (int i = 0; i < this.temp_smelt_equips.get_Count(); i++)
			{
				this.SmeltEquips.Add(BackpackTools.GetBackpackObservableItem(this.temp_smelt_equips.get_Item(i), new Action<BackpackObservableItem>(this.BackpackObservableItemClick), 2));
			}
		}
		else
		{
			GuildStoveUI.Instance.SetIsEquipSmeltsEmpty(true);
		}
	}

	private void BackpackObservableItemClick(BackpackObservableItem item)
	{
		if (item.ItemRootNullOn)
		{
			return;
		}
		for (int i = 0; i < this.SmeltEquips.Count; i++)
		{
			BackpackObservableItem item2 = this.SmeltEquips.GetItem(i);
			if (item == item2)
			{
				item2.SetIsSelected(!item.GetIsSelected());
			}
		}
	}

	public void SetEqiupParts()
	{
		if (this.EqiupParts.Count != 0)
		{
			this.SetSelectedEquipParts();
			return;
		}
		this.EqiupParts.Clear();
		for (int i = 1; i <= 10; i++)
		{
			EquipPartPeiZhi equipPartPeiZhi = DataReader<EquipPartPeiZhi>.Get(i);
			if (equipPartPeiZhi != null)
			{
				OOStoveBuildEquipPartCheck oOStoveBuildEquipPartCheck = new OOStoveBuildEquipPartCheck();
				oOStoveBuildEquipPartCheck.id = i;
				oOStoveBuildEquipPartCheck.Name = ResourceManager.GetIconSprite(equipPartPeiZhi.nameImage);
				oOStoveBuildEquipPartCheck.ItemIcon = ResourceManager.GetIconSprite(equipPartPeiZhi.partIcon1);
				oOStoveBuildEquipPartCheck.ItemIconCm = ResourceManager.GetIconSprite(equipPartPeiZhi.partIcon2);
				this.EqiupParts.Add(oOStoveBuildEquipPartCheck);
			}
		}
		this.SetSelectedEquipParts();
	}

	private void SetSelectedEquipParts()
	{
		for (int i = 0; i < this.EqiupParts.Count; i++)
		{
			OOStoveBuildEquipPartCheck oOStoveBuildEquipPartCheck = this.EqiupParts[i];
			oOStoveBuildEquipPartCheck.IsOn = (oOStoveBuildEquipPartCheck.id == this.CurrentEqupPartID);
		}
	}

	public void DoSmeltOneKey()
	{
		if (this.SmeltEquips.Count == 0)
		{
			return;
		}
		List<MultiCheckUIViewModel.ItemData> list = new List<MultiCheckUIViewModel.ItemData>();
		list.Add(new MultiCheckUIViewModel.ItemData
		{
			id = 4,
			name = TextColorMgr.GetColorByQuality("紫色装备", 4),
			isOn = false
		});
		list.Add(new MultiCheckUIViewModel.ItemData
		{
			id = 5,
			name = TextColorMgr.GetColorByQuality("橙色装备", 5),
			isOn = false
		});
		list.Add(new MultiCheckUIViewModel.ItemData
		{
			id = 6,
			name = TextColorMgr.GetColorByQuality("金色装备", 6),
			isOn = false
		});
		MultiCheckUIViewModel.Instance.ShowAsConfirm("批量熔炼", list, delegate
		{
			List<long> list2 = new List<long>();
			for (int i = 4; i <= 6; i++)
			{
				if (MultiCheckUIViewModel.Instance.IsOn(i))
				{
					BackpackManager.Instance.GetEquimentGoods(ref this.temp_smelt_equips, i, false);
					if (this.temp_smelt_equips != null && this.temp_smelt_equips.get_Count() > 0)
					{
						for (int j = 0; j < this.temp_smelt_equips.get_Count(); j++)
						{
							list2.Add(this.temp_smelt_equips.get_Item(j).GetLongId());
						}
					}
				}
			}
			for (int k = 0; k < this.SmeltEquips.Count; k++)
			{
				BackpackObservableItem backpackObservableItem = this.SmeltEquips[k];
				backpackObservableItem.SetIsSelected(list2.Contains(backpackObservableItem.id));
			}
		}, "确定", "button_orange_1", UINodesManager.MiddleUIRoot, string.Empty);
	}

	public void DoSmelt()
	{
		if (this.SmeltEquips.Count == 0)
		{
			return;
		}
		List<long> list = new List<long>();
		for (int i = 0; i < this.SmeltEquips.Count; i++)
		{
			BackpackObservableItem backpackObservableItem = this.SmeltEquips[i];
			if (backpackObservableItem.GetIsSelected())
			{
				list.Add(backpackObservableItem.id);
			}
		}
		if (list.get_Count() > 0)
		{
			this.ShowSmeltWillReturn(list);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText("请选择要熔炼的装备");
		}
	}

	public void ShowSmeltWillReturn(List<long> list_equip_uuid)
	{
		bool flag = EquipGlobal.IsContainHighLevel(list_equip_uuid);
		this.return_list.Clear();
		for (int i = 0; i < list_equip_uuid.get_Count(); i++)
		{
			int key = BackpackManager.Instance.OnGetGoodItemId(list_equip_uuid.get_Item(i));
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(key);
			for (int j = 0; j < zZhuangBeiPeiZhiBiao.smeltDrop.get_Count(); j++)
			{
				zZhuangBeiPeiZhiBiao.SmeltdropPair smeltdropPair = zZhuangBeiPeiZhiBiao.smeltDrop.get_Item(j);
				if (this.return_list.ContainsKey(smeltdropPair.key))
				{
					this.return_list.set_Item(smeltdropPair.key, this.return_list.get_Item(smeltdropPair.key) + (long)smeltdropPair.value);
				}
				else
				{
					this.return_list.set_Item(smeltdropPair.key, (long)smeltdropPair.value);
				}
			}
		}
		string text = string.Empty;
		string text2 = string.Empty;
		using (Dictionary<int, long>.Enumerator enumerator = this.return_list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, long> current = enumerator.get_Current();
				int num = this.GetMaxFund() - GuildManager.Instance.GetEquipSmeltDayFund();
				string text3;
				if (current.get_Key() == MoneyType.GetItemId(6))
				{
					if (GuildManager.Instance.GetEquipSmeltDayFund() >= this.GetMaxFund())
					{
						text2 = TextColorMgr.GetColorByID("本日军团资金贡献已达最大值\n", 1000007);
						continue;
					}
					if ((long)num < current.get_Value())
					{
						string itemName = GameDataUtils.GetItemName(current.get_Key(), true, 0L);
						text3 = text;
						text = string.Concat(new object[]
						{
							text3,
							itemName,
							"x",
							num,
							"\n"
						});
						continue;
					}
				}
				string itemName2 = GameDataUtils.GetItemName(current.get_Key(), true, 0L);
				text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					itemName2,
					"x",
					current.get_Value(),
					"\n"
				});
			}
		}
		text += text2;
		string text4 = string.Empty;
		if (flag)
		{
			text4 = "熔炼后将会获得{0}检测到您选中了高级装备\n是否进行熔炼？";
		}
		else
		{
			text4 = "熔炼后将会获得{0}是否进行熔炼？";
		}
		text4 = string.Format(text4, text);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel("熔炼预览", text4, null, delegate
		{
			Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
			for (int k = 0; k < list_equip_uuid.get_Count(); k++)
			{
				int key2 = BackpackManager.Instance.OnGetGoodItemId(list_equip_uuid.get_Item(k));
				zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao2 = DataReader<zZhuangBeiPeiZhiBiao>.Get(key2);
				if (dictionary.ContainsKey(zZhuangBeiPeiZhiBiao2.position))
				{
					List<long> list = dictionary.get_Item(zZhuangBeiPeiZhiBiao2.position);
					list.Add(list_equip_uuid.get_Item(k));
				}
				else
				{
					List<long> list2 = new List<long>();
					list2.Add(list_equip_uuid.get_Item(k));
					dictionary.Add(zZhuangBeiPeiZhiBiao2.position, list2);
				}
			}
			List<DecomposeEquipInfo> list3 = new List<DecomposeEquipInfo>();
			using (Dictionary<int, List<long>>.Enumerator enumerator2 = dictionary.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<int, List<long>> current2 = enumerator2.get_Current();
					DecomposeEquipInfo decomposeEquipInfo = new DecomposeEquipInfo();
					decomposeEquipInfo.position = current2.get_Key();
					decomposeEquipInfo.equipIds.AddRange(current2.get_Value());
					list3.Add(decomposeEquipInfo);
				}
			}
			GuildManager.Instance.SendSmeltEquip(list3);
		}, "取消", "确定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	public void DoBuild()
	{
		for (int i = 0; i < this.EqiupParts.Count; i++)
		{
			OOStoveBuildEquipPartCheck oOStoveBuildEquipPartCheck = this.EqiupParts[i];
			if (oOStoveBuildEquipPartCheck.IsOn)
			{
				this.CurrentEqupPartID = oOStoveBuildEquipPartCheck.id;
				GuildManager.Instance.SendBuildEquip(this.CurrentEqupPartID);
				return;
			}
		}
		UIManagerControl.Instance.ShowToastText("请选择部位");
	}

	private int GetMaxFund()
	{
		string value = DataReader<GongHuiXinXi>.Get("MaxFund").value;
		return int.Parse(GameDataUtils.SplitString4Dot0(value));
	}

	public void SetBuildLimit(int guild_lv, int level, int build_times)
	{
		int num = 1;
		string value = DataReader<GongHuiXinXi>.Get("EquipStep").value;
		string[] array = value.Split(new char[]
		{
			';'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				':'
			});
			if (array2.Length >= 2 && int.Parse(array2[0]) == guild_lv)
			{
				num = int.Parse(array2[1]);
				break;
			}
		}
		int num2 = 1;
		List<equipSmith> dataList = DataReader<equipSmith>.DataList;
		for (int j = 0; j < dataList.get_Count(); j++)
		{
			equipSmith equipSmith = dataList.get_Item(j);
			if (equipSmith.minLevel <= level && equipSmith.maxLevel >= level)
			{
				num2 = equipSmith.maxEquipStep;
				break;
			}
		}
		int num3 = Mathf.Min(num, num2);
		string value2 = DataReader<GongHuiXinXi>.Get("ContributeCost").value;
		string[] array3 = value2.Split(new char[]
		{
			';'
		});
		int num4 = 0;
		if (array3.Length >= 3)
		{
			int num5 = int.Parse(array3[2]);
			int num6 = int.Parse(array3[1]) * build_times;
			num4 = int.Parse(array3[0]) + num6;
			num4 = Mathf.Min(num4, num5);
		}
		string value3 = DataReader<GongHuiXinXi>.Get("EssenceCost").value;
		string[] array4 = value3.Split(new char[]
		{
			';'
		});
		int const_essence = 0;
		for (int k = 0; k < array4.Length; k++)
		{
			string[] array5 = array4[k].Split(new char[]
			{
				':'
			});
			if (array5.Length >= 2 && int.Parse(array5[0]) == num3)
			{
				const_essence = int.Parse(array5[1]);
				break;
			}
		}
		GuildStoveUI.Instance.SetEquipBuildUITips01(num3);
		GuildStoveUI.Instance.SetEquipBuildUITips02(num);
		GuildStoveUI.Instance.SetEquipBuildUITips03(num2);
		GuildStoveUI.Instance.SetEquipBuildCost(num4, const_essence);
	}
}
