using GameData;
using Package;
using System;
using System.Collections.Generic;
using XNetwork;

public class EquipmentManager : BaseSubSystemManager
{
	public const int EquipAllPosNum = 10;

	private EquipLib weaponLib = new EquipLib();

	private EquipLib clothesLib = new EquipLib();

	private EquipLib waistLib = new EquipLib();

	private EquipLib necklaceLib = new EquipLib();

	private EquipLib pantsLib = new EquipLib();

	private EquipLib shoesLib = new EquipLib();

	public EquipLoginPush equipmentData;

	public Dictionary<long, EquipSimpleInfo> dicEquips = new Dictionary<long, EquipSimpleInfo>();

	public static List<int> listWearingID = new List<int>();

	public Dictionary<EquipLibType.ELT, long> RecommendDic;

	public List<int> EquipSuitActiveIndexIds;

	public EquipLibType.ELT LastSelectUIPos;

	public EquipDetailedUIState LastSelectUIDetailState;

	private static EquipmentManager instance;

	public int LastLv;

	private int currentWashPos = -1;

	private int CurrentStarToMaterial;

	private int enchantPartPos;

	private int refineEquipPart;

	private long refineEquipID;

	public bool ShowRecommendTip;

	public Dictionary<EquipLibType.ELT, bool> CanShowEnchantmentTipPosDic;

	public Dictionary<EquipLibType.ELT, List<int>> CanShowEnchantmentTipSlotsDic;

	public Dictionary<EquipLibType.ELT, bool> CanShowStarUpTipPosDic;

	private long LastRefineEquipID;

	private int LastRefineEquipPart;

	public EquipLib WeaponLib
	{
		get
		{
			return this.weaponLib;
		}
	}

	public EquipLib ClothesLib
	{
		get
		{
			return this.clothesLib;
		}
	}

	public EquipLib WaistLib
	{
		get
		{
			return this.waistLib;
		}
	}

	public EquipLib NecklaceLib
	{
		get
		{
			return this.necklaceLib;
		}
	}

	public EquipLib PantsLib
	{
		get
		{
			return this.pantsLib;
		}
	}

	public EquipLib ShoesLib
	{
		get
		{
			return this.shoesLib;
		}
	}

	public static EquipmentManager Instance
	{
		get
		{
			if (EquipmentManager.instance == null)
			{
				EquipmentManager.instance = new EquipmentManager();
			}
			return EquipmentManager.instance;
		}
	}

	private EquipmentManager()
	{
	}

	public override void Init()
	{
		this.CanShowEnchantmentTipPosDic = new Dictionary<EquipLibType.ELT, bool>();
		this.CanShowStarUpTipPosDic = new Dictionary<EquipLibType.ELT, bool>();
		this.CanShowEnchantmentTipSlotsDic = new Dictionary<EquipLibType.ELT, List<int>>();
		for (int i = 1; i <= 10; i++)
		{
			this.CanShowStarUpTipPosDic.Add((EquipLibType.ELT)i, true);
			this.CanShowEnchantmentTipPosDic.Add((EquipLibType.ELT)i, true);
			this.CanShowEnchantmentTipSlotsDic.Add((EquipLibType.ELT)i, new List<int>());
		}
		this.LastSelectUIDetailState = EquipDetailedUIState.EquipStrengthen;
		this.LastSelectUIPos = EquipLibType.ELT.Weapon;
		base.Init();
	}

	public override void Release()
	{
		this.equipmentData = null;
		if (this.dicEquips != null)
		{
			this.dicEquips.Clear();
		}
		if (EquipmentManager.listWearingID != null)
		{
			EquipmentManager.listWearingID.Clear();
		}
		this.RecommendDic = null;
		this.CanShowEnchantmentTipPosDic = null;
		this.CanShowStarUpTipPosDic = null;
		this.CanShowEnchantmentTipSlotsDic = null;
		this.LastSelectUIDetailState = EquipDetailedUIState.EquipStrengthen;
		this.LastSelectUIPos = EquipLibType.ELT.Weapon;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<EquipLibs>(new NetCallBackMethod<EquipLibs>(this.OnGetEquipmentLib));
		NetworkManager.AddListenEvent<EquipCompositeRes>(new NetCallBackMethod<EquipCompositeRes>(this.OnCombieEquipmentRes));
		NetworkManager.AddListenEvent<LightUpEquipmentRes>(new NetCallBackMethod<LightUpEquipmentRes>(this.OnLightEquipmentRes));
		NetworkManager.AddListenEvent<PutOnEquipmentRes>(new NetCallBackMethod<PutOnEquipmentRes>(this.OnPutEquipmentRes));
		NetworkManager.AddListenEvent<EquipLoginPush>(new NetCallBackMethod<EquipLoginPush>(this.OnGetEquipLoginPush));
		NetworkManager.AddListenEvent<AcquireNewEquipNty>(new NetCallBackMethod<AcquireNewEquipNty>(this.OnGetAcquireNewEquipNty));
		NetworkManager.AddListenEvent<IntensifyPositionRes>(new NetCallBackMethod<IntensifyPositionRes>(this.OnGetIntensifyPositionRes));
		NetworkManager.AddListenEvent<EquipAdvancedRes>(new NetCallBackMethod<EquipAdvancedRes>(this.OnGetEquipAdvancedRes));
		NetworkManager.AddListenEvent<AutoIntensifyPositionRes>(new NetCallBackMethod<AutoIntensifyPositionRes>(this.OnAutoIntensifyPositionRes));
		NetworkManager.AddListenEvent<UpdateEquipInfoChangeNty>(new NetCallBackMethod<UpdateEquipInfoChangeNty>(this.OnUpdateEquipInfoChangeNty));
		NetworkManager.AddListenEvent<DeleteEquipInfoNty>(new NetCallBackMethod<DeleteEquipInfoNty>(this.OnDeleteEquipInfoNty));
		NetworkManager.AddListenEvent<RefineEquipRes>(new NetCallBackMethod<RefineEquipRes>(this.OnRefineEquipRes));
		NetworkManager.AddListenEvent<RefineEquipResultAckRes>(new NetCallBackMethod<RefineEquipResultAckRes>(this.OnRefineEquipResultAckRes));
		NetworkManager.AddListenEvent<EnhanceEquipStarRes>(new NetCallBackMethod<EnhanceEquipStarRes>(this.OnEnhanceEquipStarRes));
		NetworkManager.AddListenEvent<ResetEquipStarRes>(new NetCallBackMethod<ResetEquipStarRes>(this.OnResetEquipStarRes));
		NetworkManager.AddListenEvent<EnchantEquipRes>(new NetCallBackMethod<EnchantEquipRes>(this.OnEnchantEquipRes));
		NetworkManager.AddListenEvent<EnchantEquipResultAckRes>(new NetCallBackMethod<EnchantEquipResultAckRes>(this.OnEnchantEquipResultAckRes));
		NetworkManager.AddListenEvent<DecomposeEquipRes>(new NetCallBackMethod<DecomposeEquipRes>(this.OnDecomposeEquipRes));
		NetworkManager.AddListenEvent<DataCompositeRes>(new NetCallBackMethod<DataCompositeRes>(this.OnDataCompositeRes));
		NetworkManager.AddListenEvent<CancelRefineDataRes>(new NetCallBackMethod<CancelRefineDataRes>(this.OnCancelRefineDataRes));
		NetworkManager.AddListenEvent<ForgingSuitRes>(new NetCallBackMethod<ForgingSuitRes>(this.OnForgingSuitRes));
		NetworkManager.AddListenEvent<EquipSuitChangeNty>(new NetCallBackMethod<EquipSuitChangeNty>(this.OnEquipSuitChangeNty));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.LvChanged, new Callback(this.OnRoleLevelUp));
		EventDispatcher.AddListener("ChangeCareerManager.RoleSelfProfessionChange", new Callback(this.OnRoleChangeCarrer));
		EventDispatcher.AddListener(ParticularCityAttrChangedEvent.GoldChanged, new Callback(this.OnRoleGoldChanged));
		EventDispatcher.AddListener(EventNames.OnUpdateGoods, new Callback(this.OnUpdateGoods));
	}

	private void OnGetEquipLoginPush(short state, EquipLoginPush down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.equipmentData = down;
			for (int i = 0; i < this.equipmentData.equipLibs.get_Count(); i++)
			{
				EquipLib equipLib = this.equipmentData.equipLibs.get_Item(i);
				for (int j = 0; j < equipLib.equips.get_Count(); j++)
				{
					EquipSimpleInfo equipSimpleInfo = equipLib.equips.get_Item(j);
					this.AddToDicEquips(equipSimpleInfo.equipId, equipSimpleInfo);
					if (equipSimpleInfo.refineData != null && equipSimpleInfo.refineData.attrId > 0)
					{
						this.LastRefineEquipID = equipSimpleInfo.equipId;
						this.LastRefineEquipPart = (int)equipLib.type;
					}
				}
			}
			if (down.ids != null)
			{
				this.EquipSuitActiveIndexIds = down.ids.indexId;
			}
			this.CheckRecommend();
			this.ShowRecommendTip = true;
			this.ResetWearingIDs();
			this.CheckCanShowEquipStrengthenPromoteWay();
			this.CheckCanShowPromoteWay();
		}
	}

	private void OnGetEquipAdvancedRes(short state, EquipAdvancedRes down = null)
	{
		if (state == 0)
		{
			if (down != null)
			{
				EquipLib equipLib = this.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)down.position);
				EquipLib equipLib2 = this.equipmentData.equipLibs.Find((EquipLib a) => a.type == EquipLibType.ELT.Experience);
				int num = equipLib.equips.FindIndex((EquipSimpleInfo a) => a.equipId == down.equip.equipId);
				bool arg = false;
				if (equipLib.equips.get_Item(num).cfgId != down.equip.cfgId)
				{
					arg = true;
				}
				equipLib.equips.set_Item(num, down.equip);
				this.dicEquips.set_Item(down.equip.equipId, down.equip);
				for (int i = 0; i < down.sourceEquipIds.get_Count(); i++)
				{
					if (equipLib2.equips.Contains(this.dicEquips.get_Item(down.sourceEquipIds.get_Item(i))))
					{
						equipLib2.equips.Remove(this.dicEquips.get_Item(down.sourceEquipIds.get_Item(i)));
					}
					else
					{
						equipLib.equips.Remove(this.dicEquips.get_Item(down.sourceEquipIds.get_Item(i)));
					}
					this.dicEquips.Remove(down.sourceEquipIds.get_Item(i));
				}
				this.ResetWearingIDs();
				EventDispatcher.Broadcast<bool>(EventNames.OnGetEquipAdvancedRes, arg);
			}
			else
			{
				Debuger.Info("down = null  ", new object[0]);
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetIntensifyPositionRes(short state, IntensifyPositionRes down = null)
	{
		if ((int)state == Status.ITEM_NOT_ENOUGH_COUNT)
		{
			LinkNavigationManager.ItemNotEnoughToLink(down.itemId, true, null, true);
			return;
		}
		if (state == 0)
		{
			if (down != null)
			{
				EquipLib equipLib = this.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)down.intensifyInfo.position);
				this.LastLv = equipLib.lv;
				if (down.isSuccess && equipLib.lv < down.intensifyInfo.lv)
				{
					equipLib.lv = down.intensifyInfo.lv;
				}
				equipLib.blessRatio = down.intensifyInfo.blessRatio;
				equipLib.blessValue = down.intensifyInfo.blessValue;
				EventDispatcher.Broadcast<bool>(EventNames.OnIntensifyPosSuccessOrFailed, down.isSuccess);
				this.CheckCanShowEquipStrengthenPromoteWay();
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, down.itemId);
		}
	}

	private void OnAutoIntensifyPositionRes(short state, AutoIntensifyPositionRes down = null)
	{
		if ((int)state == Status.ITEM_NOT_ENOUGH_COUNT)
		{
			LinkNavigationManager.ItemNotEnoughToLink(down.itemId, true, null, true);
			return;
		}
		if (state == 0)
		{
			if (down != null)
			{
				EquipLib equipLib = this.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)down.intensifyInfo.position);
				this.LastLv = equipLib.lv;
				if (equipLib.lv < down.intensifyInfo.lv)
				{
					equipLib.lv = down.intensifyInfo.lv;
				}
				equipLib.blessRatio = down.intensifyInfo.blessRatio;
				equipLib.blessValue = down.intensifyInfo.blessValue;
				EventDispatcher.Broadcast<bool>(EventNames.OnIntensifyPosSuccessOrFailed, true);
				this.CheckCanShowEquipStrengthenPromoteWay();
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, down.itemId);
		}
	}

	private void OnGetAcquireNewEquipNty(short state, AcquireNewEquipNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.equips.get_Count(); i++)
			{
				int pos = DataReader<zZhuangBeiPeiZhiBiao>.Get(down.equips.get_Item(i).cfgId).position;
				List<EquipSimpleInfo> equips = this.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)pos).equips;
				if (equips.Contains(down.equips.get_Item(i)))
				{
					equips.set_Item(equips.IndexOf(down.equips.get_Item(i)), down.equips.get_Item(i));
				}
				else
				{
					equips.Add(down.equips.get_Item(i));
				}
				this.AddToDicEquips(down.equips.get_Item(i).equipId, down.equips.get_Item(i));
				this.CheckCanRecommendByPos((EquipLibType.ELT)pos);
			}
			this.CheckCanShowPromoteWay();
			this.ResetWearingIDs();
			EventDispatcher.Broadcast(EventNames.OnGetAcquireNewEquipNty);
		}
	}

	private void OnUpdateEquipInfoChangeNty(short state, UpdateEquipInfoChangeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.equips.get_Count(); i++)
			{
				EquipSimpleInfo equipInfo = down.equips.get_Item(i);
				this.UpdateDicEquips(equipInfo.equipId, equipInfo);
				for (int j = 0; j < this.equipmentData.equipLibs.get_Count(); j++)
				{
					int num = this.equipmentData.equipLibs.get_Item(j).equips.FindIndex((EquipSimpleInfo e) => e.equipId == equipInfo.equipId);
					if (num >= 0)
					{
						this.equipmentData.equipLibs.get_Item(j).equips.set_Item(num, equipInfo);
					}
				}
			}
		}
	}

	private void OnDeleteEquipInfoNty(short state, DeleteEquipInfoNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < down.equips.get_Count(); i++)
			{
				EquipSimpleInfo equipInfo = down.equips.get_Item(i);
				this.RemoveFromDicEquips(equipInfo.equipId);
				for (int j = 0; j < this.equipmentData.equipLibs.get_Count(); j++)
				{
					int num = this.equipmentData.equipLibs.get_Item(j).equips.FindIndex((EquipSimpleInfo e) => e.equipId == equipInfo.equipId);
					if (num >= 0)
					{
						this.equipmentData.equipLibs.get_Item(j).equips.RemoveAt(num);
					}
				}
				int position = DataReader<zZhuangBeiPeiZhiBiao>.Get(down.equips.get_Item(i).cfgId).position;
				this.CheckCanRecommendByPos((EquipLibType.ELT)position);
			}
		}
	}

	private void OnPutEquipmentRes(short state, PutOnEquipmentRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			SoundManager.PlayUI(10009, false);
			EquipLib equipLib = this.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)down.position);
			equipLib.wearingId = down.equipId;
			equipLib.blessValue = down.blessValue;
			int num = equipLib.equips.FindIndex((EquipSimpleInfo a) => a.equipId == down.equipId);
			if (num >= 0)
			{
				equipLib.equips.get_Item(num).binding = true;
				if (this.dicEquips.ContainsKey(down.equipId))
				{
					this.dicEquips.set_Item(down.equipId, equipLib.equips.get_Item(num));
				}
			}
			this.CheckCanRecommendByPos((EquipLibType.ELT)down.position);
			EventDispatcher.Broadcast(EventNames.EquipEquipmentSucess);
			this.ResetWearingIDs();
			this.CheckCanShowPromoteWay();
		}
	}

	private void OnRefineEquipRes(short state, RefineEquipRes down = null)
	{
		if ((int)state == Status.LESS_REFINE_MATERIAL || (int)state == Status.ITEM_NOT_ENOUGH_COUNT)
		{
			EquipDetailedUI equipdetailUI = UIManagerControl.Instance.GetUIIfExist("EquipDetailedUI") as EquipDetailedUI;
			bool rclose = true;
			if (equipdetailUI != null)
			{
				equipdetailUI.SetBtnWashState(false);
				if (UIManagerControl.Instance.IsOpen("EquipDetailedUI") && !equipdetailUI.IsFirstWash)
				{
					rclose = false;
				}
			}
			LinkNavigationManager.ItemNotEnoughToLink(7, true, delegate
			{
				if (equipdetailUI != null && UIManagerControl.Instance.IsOpen("EquipDetailedUI") && !equipdetailUI.IsFirstWash)
				{
					DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(651056, false), null, delegate
					{
						DialogBoxUIViewModel.Instance.BtnRclose = true;
						equipdetailUI.IsFirstWash = true;
						EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == equipdetailUI.currentSelectPos);
						if (equipLib != null)
						{
							EquipmentManager.Instance.SendCancelRefineDataReq((int)equipdetailUI.currentSelectPos, equipLib.wearingId);
						}
						LinkNavigationManager.OpenXMarketUI2();
						UIManagerControl.Instance.HideUI("DialogBoxUI");
					}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
				}
				else
				{
					LinkNavigationManager.OpenXMarketUI2();
					UIManagerControl.Instance.HideUI("DialogBoxUI");
				}
			}, rclose);
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.ClearEquipNoteData();
			EventDispatcher.Broadcast<ExcellentAttr>(EventNames.OnRefineEquipRes, down.excellentAttrs);
		}
	}

	private void OnRefineEquipResultAckRes(short state, RefineEquipResultAckRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < this.equipmentData.equipLibs.get_Count(); i++)
			{
				EquipLib equipLib = this.equipmentData.equipLibs.get_Item(i);
				for (int j = 0; j < equipLib.equips.get_Count(); j++)
				{
					if (down.equipId == equipLib.equips.get_Item(j).equipId)
					{
						equipLib.equips.get_Item(j).position = this.currentWashPos;
						if (equipLib.equips.get_Item(j).refineData != null)
						{
							equipLib.equips.get_Item(j).refineData = null;
						}
						if (equipLib.equips.get_Item(j).excellentAttrs.get_Item(this.currentWashPos) != null)
						{
							equipLib.equips.get_Item(j).excellentAttrs.set_Item(this.currentWashPos, down.excellentAttrs);
						}
						this.UpdateDicEquips(down.equipId, equipLib.equips.get_Item(j));
					}
				}
			}
			EventDispatcher.Broadcast<int>(EventNames.OnRefineEquipResultAckRes, this.refineEquipPart);
		}
	}

	private void OnEnhanceEquipStarRes(short state, EnhanceEquipStarRes down)
	{
		if ((int)state == Status.ITEM_NOT_ENOUGH_COUNT || (int)state == Status.LESS_ENCHANCE_STAR_MATERIAL)
		{
			LinkNavigationManager.ItemNotEnoughToLink(8, false, null, true);
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			bool arg = false;
			for (int i = 0; i < this.equipmentData.equipLibs.get_Count(); i++)
			{
				EquipLib equipLib = this.equipmentData.equipLibs.get_Item(i);
				for (int j = 0; j < equipLib.equips.get_Count(); j++)
				{
					if (down.equipId == equipLib.equips.get_Item(j).equipId && equipLib.equips.get_Item(j).star != down.currentStar)
					{
						arg = true;
						equipLib.equips.get_Item(j).star = down.currentStar;
						equipLib.equips.get_Item(j).starAttrs.Clear();
						for (int k = 0; k < down.excellentAttrs.get_Count(); k++)
						{
							equipLib.equips.get_Item(j).starAttrs.Add(down.excellentAttrs.get_Item(k));
						}
						if (down.currentStar > 0)
						{
							equipLib.equips.get_Item(j).starToMaterial.Add(this.CurrentStarToMaterial);
						}
						this.UpdateDicEquips(down.equipId, equipLib.equips.get_Item(j));
					}
				}
			}
			EventDispatcher.Broadcast<bool, int>(EventNames.OnStarUpRes, arg, down.currentStar);
		}
	}

	private void OnResetEquipStarRes(short state, ResetEquipStarRes down)
	{
		if (state != 0)
		{
			if (down == null)
			{
				StateManager.Instance.StateShow(state, 0);
			}
			else
			{
				StateManager.Instance.StateShow(state, (int)down.equipId);
			}
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < this.equipmentData.equipLibs.get_Count(); i++)
			{
				EquipLib equipLib = this.equipmentData.equipLibs.get_Item(i);
				for (int j = 0; j < equipLib.equips.get_Count(); j++)
				{
					if (down.equipId == equipLib.equips.get_Item(j).equipId)
					{
						equipLib.equips.get_Item(j).star = down.currentStar;
						equipLib.equips.get_Item(j).starAttrs.Clear();
						equipLib.equips.get_Item(j).starToMaterial.Clear();
						this.UpdateDicEquips(down.equipId, equipLib.equips.get_Item(j));
					}
				}
			}
			EventDispatcher.Broadcast(EventNames.OnResetEquipStar);
		}
	}

	private void OnEnchantEquipRes(short state, EnchantEquipRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			EquipEnchantmentUI equipEnchantmentUI = UIManagerControl.Instance.GetUIIfExist("EquipEnchantMentUI") as EquipEnchantmentUI;
			if (equipEnchantmentUI != null)
			{
				int partPos = EquipEnchantmentUI.Instance.CurrentPos;
				if (down.oldEnchantAttr == null || down.oldEnchantAttr.attrId <= 0)
				{
					EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)partPos);
					if (equipLib != null)
					{
						this.SendEnchantEquipResultAckReq(partPos, equipLib.wearingId, down.newEnchantAttr);
						return;
					}
				}
				EquipEnchantmentUI.Instance.OnEnchantmentEquipRes(down.oldEnchantAttr, down.newEnchantAttr);
			}
		}
	}

	private void OnEnchantEquipResultAckRes(short state, EnchantEquipResultAckRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			for (int i = 0; i < this.equipmentData.equipLibs.get_Count(); i++)
			{
				EquipLib equipLib = this.equipmentData.equipLibs.get_Item(i);
				for (int j = 0; j < equipLib.equips.get_Count(); j++)
				{
					if (down.equipId == equipLib.equips.get_Item(j).equipId)
					{
						if (equipLib.equips.get_Item(j).enchantAttrs.get_Count() > down.position)
						{
							equipLib.equips.get_Item(j).enchantAttrs.set_Item(down.position, down.excellentAttr);
						}
						else
						{
							equipLib.equips.get_Item(j).enchantAttrs.Add(down.excellentAttr);
						}
						this.UpdateDicEquips(down.equipId, equipLib.equips.get_Item(j));
					}
				}
			}
			EventDispatcher.Broadcast<int>(EventNames.OnEnchantEquipResultAckRes, down.position);
		}
	}

	private void OnDecomposeEquipRes(short state, DecomposeEquipRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.RemoveEquipIds(down.decomposeEquipIds);
			this.CheckCanShowPromoteWay();
			EventDispatcher.Broadcast<List<DecomposeItem>>(EventNames.OnDecomposeEquipRes, down.decomposeItems);
		}
	}

	private void OnDataCompositeRes(short state, DataCompositeRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			string text = "获得{0}个{1}";
			string itemName = GameDataUtils.GetItemName(down.cfgId, true, 0L);
			string text2 = string.Format(text, down.count, itemName);
			UIManagerControl.Instance.ShowToastText(text2);
		}
	}

	private void OnCancelRefineDataRes(short state, CancelRefineDataRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.ClearEquipNoteData();
		EventDispatcher.Broadcast(EventNames.OnCancelRefineDataRes);
	}

	private void OnForgingSuitRes(short state, ForgingSuitRes down = null)
	{
		if ((int)state == Status.NOT_ENOUGH_DATA)
		{
			LinkNavigationManager.ItemNotEnoughToLink(33005, false, null, true);
			return;
		}
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null && this.equipmentData != null && this.equipmentData.equipLibs != null)
		{
			EquipLib equipLib = this.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)down.libPosition);
			if (equipLib != null)
			{
				for (int i = 0; i < equipLib.equips.get_Count(); i++)
				{
					if (down.equipId == equipLib.equips.get_Item(i).equipId)
					{
						equipLib.equips.get_Item(i).suitId = down.suitId;
						this.UpdateDicEquips(down.equipId, equipLib.equips.get_Item(i));
					}
				}
			}
			EventDispatcher.Broadcast<int>(EventNames.OnForgingSuitRes, down.libPosition);
		}
	}

	private void OnEquipSuitChangeNty(short state, EquipSuitChangeNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
		}
		if (down != null && down.ids != null)
		{
			this.EquipSuitActiveIndexIds = down.ids.indexId;
		}
		else
		{
			this.EquipSuitActiveIndexIds = null;
		}
	}

	private void OnLightEquipmentRes(short state, LightUpEquipmentRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		EventDispatcher.Broadcast(EventNames.LightEquipmentSucess);
	}

	private void OnCombieEquipmentRes(short state, EquipCompositeRes down = null)
	{
		if (state == 0)
		{
			EventDispatcher.Broadcast<bool>(EventNames.ComposeEquipmentSucess, true);
		}
		else
		{
			EventDispatcher.Broadcast<bool>(EventNames.ComposeEquipmentSucess, false);
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetEquipmentLib(short state, EquipLibs down = null)
	{
		if (state == 0)
		{
			if (down != null)
			{
				for (int i = 0; i < down.libs.get_Count(); i++)
				{
					switch (down.libs.get_Item(i).type)
					{
					case EquipLibType.ELT.Weapon:
						this.weaponLib = down.libs.get_Item(i);
						break;
					case EquipLibType.ELT.Shirt:
						this.clothesLib = down.libs.get_Item(i);
						break;
					case EquipLibType.ELT.Pant:
						this.pantsLib = down.libs.get_Item(i);
						break;
					case EquipLibType.ELT.Shoe:
						this.shoesLib = down.libs.get_Item(i);
						break;
					case EquipLibType.ELT.Waist:
						this.waistLib = down.libs.get_Item(i);
						break;
					case EquipLibType.ELT.Necklace:
						this.necklaceLib = down.libs.get_Item(i);
						break;
					}
				}
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	public void SendEquipAdvancedReq(int pos, long equipID, List<long> eatEquipIDs)
	{
		EquipAdvancedReq equipAdvancedReq = new EquipAdvancedReq();
		equipAdvancedReq.position = pos;
		equipAdvancedReq.targetEquipId = equipID;
		equipAdvancedReq.sourceEquipIds.AddRange(eatEquipIDs);
		NetworkManager.Send(equipAdvancedReq, ServerType.Data);
	}

	public void SendAutoIntensifyPositionReq(int pos)
	{
		NetworkManager.Send(new AutoIntensifyPositionReq
		{
			position = pos
		}, ServerType.Data);
	}

	public void SendIntensifyPositionReq(int pos, int stoneID = 0)
	{
		NetworkManager.Send(new IntensifyPositionReq
		{
			position = pos,
			intensifyStoneId = stoneID
		}, ServerType.Data);
	}

	public void SendPutOnEquipmentReq(int pos, long equipmentId, int itemID)
	{
		NetworkManager.Send(new PutOnEquipmentReq
		{
			position = pos,
			equipId = equipmentId
		}, ServerType.Data);
	}

	public void SendRefineEquipReq(int partPos, int washPos, long equipmentId)
	{
		this.refineEquipPart = partPos;
		this.refineEquipID = equipmentId;
		this.currentWashPos = washPos;
		NetworkManager.Send(new RefineEquipReq
		{
			libPosition = partPos,
			position = washPos,
			equipId = equipmentId
		}, ServerType.Data);
	}

	public void SendRefineEquipResultAckReq(int partPos, int excellentID, long equipmentId, int washPos)
	{
		this.refineEquipPart = partPos;
		this.refineEquipID = equipmentId;
		this.currentWashPos = washPos;
		NetworkManager.Send(new RefineEquipResultAckReq
		{
			libPosition = partPos,
			attrId = excellentID,
			equipId = equipmentId,
			position = washPos
		}, ServerType.Data);
	}

	public void SendEnhanceEquipStarReq(int partPos, int materialID, long equipmentID)
	{
		this.CurrentStarToMaterial = materialID;
		NetworkManager.Send(new EnhanceEquipStarReq
		{
			libPosition = partPos,
			starMaterialId = materialID,
			equipId = equipmentID
		}, ServerType.Data);
	}

	public void SendResetEquipStarReq(int pos, long equipID)
	{
		NetworkManager.Send(new ResetEquipStarReq
		{
			libPosition = pos,
			equipId = equipID
		}, ServerType.Data);
	}

	public void SendEnchantEquipReq(int partPos, int enchantPos, int materialID, long equipID)
	{
		NetworkManager.Send(new EnchantEquipReq
		{
			libPosition = partPos,
			position = enchantPos,
			enchantId = materialID,
			equipId = equipID
		}, ServerType.Data);
	}

	public void SendEnchantEquipResultAckReq(int partPos, long equipID, ExcellentAttr excellentAtt)
	{
		NetworkManager.Send(new EnchantEquipResultAckReq
		{
			libPosition = partPos,
			equipId = equipID,
			excellentAttr = excellentAtt
		}, ServerType.Data);
	}

	public void SendDecomposeEquipReq(List<DecomposeEquipInfo> decomposeInfoList)
	{
		DecomposeEquipReq decomposeEquipReq = new DecomposeEquipReq();
		if (decomposeInfoList != null && decomposeInfoList.get_Count() > 0)
		{
			decomposeEquipReq.decomposeEquipInfos.AddRange(decomposeInfoList);
			NetworkManager.Send(decomposeEquipReq, ServerType.Data);
		}
	}

	public void SendDataCompositeReq(int _itemID, int _method = 1, DataCompositeReq.OpType _type = DataCompositeReq.OpType.Enchanting)
	{
		NetworkManager.Send(new DataCompositeReq
		{
			cfgId = _itemID,
			tType = _type,
			method = _method
		}, ServerType.Data);
	}

	public void SendCancelRefineDataReq(int equipPart, long equipID)
	{
		this.refineEquipPart = equipPart;
		this.refineEquipID = equipID;
		NetworkManager.Send(new CancelRefineDataReq
		{
			libPosition = equipPart,
			equipId = equipID
		}, ServerType.Data);
	}

	public void SendForgingSuitReq(long equipID, EquipLibType.ELT pos, int suitID)
	{
		NetworkManager.Send(new ForgingSuitReq
		{
			equipId = equipID,
			libPosition = (int)pos,
			suitId = suitID
		}, ServerType.Data);
	}

	private List<int> ResetWearingIDs()
	{
		EquipmentManager.listWearingID.Clear();
		for (int i = 0; i < this.equipmentData.equipLibs.get_Count(); i++)
		{
			if (this.equipmentData.equipLibs.get_Item(i).type != EquipLibType.ELT.Experience)
			{
				if (this.equipmentData.equipLibs.get_Item(i).wearingId != -1L)
				{
					EquipmentManager.listWearingID.Add(this.dicEquips.get_Item(this.equipmentData.equipLibs.get_Item(i).wearingId).cfgId);
				}
			}
		}
		return EquipmentManager.listWearingID;
	}

	private void UpdateDicEquips(long equipID, EquipSimpleInfo simpleInfo)
	{
		if (this.dicEquips.ContainsKey(equipID))
		{
			this.dicEquips.set_Item(equipID, simpleInfo);
		}
	}

	private void RemoveFromDicEquips(long equipID)
	{
		if (this.dicEquips.ContainsKey(equipID))
		{
			this.dicEquips.Remove(equipID);
		}
	}

	private void AddToDicEquips(long equipID, EquipSimpleInfo simpleInfo)
	{
		if (this.dicEquips.ContainsKey(equipID))
		{
			this.dicEquips.set_Item(equipID, simpleInfo);
		}
		else
		{
			this.dicEquips.Add(equipID, simpleInfo);
		}
	}

	private void OnRoleLevelUp()
	{
		if (!SystemOpenManager.IsSystemOn(47) && this.RecommendDic != null)
		{
			this.RecommendDic = null;
			this.ShowRecommendTip = false;
			return;
		}
		this.CheckRecommend();
	}

	private void OnRoleChangeCarrer()
	{
		if (!SystemOpenManager.IsSystemOn(47) && this.RecommendDic != null)
		{
			this.RecommendDic = null;
			this.ShowRecommendTip = false;
			return;
		}
		if (this.RecommendDic != null)
		{
			List<EquipLibType.ELT> list = new List<EquipLibType.ELT>();
			using (Dictionary<EquipLibType.ELT, long>.Enumerator enumerator = this.RecommendDic.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<EquipLibType.ELT, long> current = enumerator.get_Current();
					long value = current.get_Value();
					if (this.dicEquips.ContainsKey(value))
					{
						EquipSimpleInfo equipSimpleInfo = this.dicEquips.get_Item(value);
						zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipSimpleInfo.cfgId);
						if (zZhuangBeiPeiZhiBiao.occupation != EntityWorld.Instance.EntSelf.TypeID)
						{
							list.Add(current.get_Key());
						}
					}
				}
			}
			for (int i = 0; i < list.get_Count(); i++)
			{
				if (this.RecommendDic.ContainsKey(list.get_Item(i)))
				{
					this.RecommendDic.Remove(list.get_Item(i));
				}
			}
			EventDispatcher.Broadcast(EventNames.UpdateEquipRecommend);
		}
		this.CheckRecommend();
	}

	private void OnUpdateGoods()
	{
	}

	private void OnRoleGoldChanged()
	{
		this.CheckCanShowEquipStrengthenPromoteWay();
	}

	private void CheckRecommend()
	{
		if (SystemOpenManager.IsSystemOn(47))
		{
			this.CheckCanRecommendByPos(EquipLibType.ELT.Weapon);
			this.CheckCanRecommendByPos(EquipLibType.ELT.Shirt);
			this.CheckCanRecommendByPos(EquipLibType.ELT.Pant);
			this.CheckCanRecommendByPos(EquipLibType.ELT.Shoe);
			this.CheckCanRecommendByPos(EquipLibType.ELT.Waist);
			this.CheckCanRecommendByPos(EquipLibType.ELT.Necklace);
			this.CheckCanRecommendByPos(EquipLibType.ELT.Part7);
			this.CheckCanRecommendByPos(EquipLibType.ELT.Part8);
			this.CheckCanRecommendByPos(EquipLibType.ELT.Part9);
			this.CheckCanRecommendByPos(EquipLibType.ELT.Part10);
		}
	}

	private void CheckCanRecommendByPos(EquipLibType.ELT pos)
	{
		if (!SystemOpenManager.IsSystemOn(47))
		{
			return;
		}
		if (this.RecommendDic == null)
		{
			this.RecommendDic = new Dictionary<EquipLibType.ELT, long>();
		}
		if (this.RecommendDic.ContainsKey(pos) && !EquipmentManager.Instance.dicEquips.ContainsKey(this.RecommendDic.get_Item(pos)))
		{
			this.RecommendDic.Remove(pos);
		}
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		if (equipLib == null)
		{
			return;
		}
		if (!EquipmentManager.Instance.dicEquips.ContainsKey(equipLib.wearingId))
		{
			return;
		}
		float num = (float)this.GetEquipFightingByEquipID(equipLib.wearingId);
		for (int i = 0; i < equipLib.equips.get_Count(); i++)
		{
			EquipSimpleInfo equipSimpleInfo = equipLib.equips.get_Item(i);
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipSimpleInfo.cfgId);
			if (zZhuangBeiPeiZhiBiao != null)
			{
				float num2 = (float)this.GetEquipFightingByEquipID(equipSimpleInfo.equipId);
				if (EntityWorld.Instance.EntSelf == null)
				{
					break;
				}
				if (RankUpManager.Instance.Rank >= zZhuangBeiPeiZhiBiao.advancedStep && EntityWorld.Instance.EntSelf.Lv >= zZhuangBeiPeiZhiBiao.level && zZhuangBeiPeiZhiBiao.occupation == EntityWorld.Instance.EntSelf.TypeID && num < num2)
				{
					if (this.RecommendDic.ContainsKey(pos) && this.RecommendDic.get_Item(pos) != equipSimpleInfo.equipId)
					{
						float num3 = (float)this.GetEquipFightingByEquipID(this.RecommendDic.get_Item(pos));
						if (num3 < num2)
						{
							this.RecommendDic.set_Item(pos, equipSimpleInfo.equipId);
							this.ShowRecommendTip = true;
						}
					}
					else if (!this.RecommendDic.ContainsKey(pos))
					{
						this.RecommendDic.Add(pos, equipSimpleInfo.equipId);
						this.ShowRecommendTip = true;
					}
				}
				else if (num >= num2 && this.RecommendDic.ContainsKey(pos) && this.RecommendDic.get_Item(pos) == equipLib.wearingId)
				{
					this.RecommendDic.Remove(pos);
				}
			}
		}
		if (this.ShowRecommendTip)
		{
			EventDispatcher.Broadcast(EventNames.UpdateEquipRecommend);
		}
	}

	public bool CheckBadageByPosAndEquipDetailedUIState(EquipLibType.ELT pos, EquipDetailedUIState state)
	{
		if (state == EquipDetailedUIState.EquipGem)
		{
			return GemManager.Instance.IsCanWearGem(pos);
		}
		if (state == EquipDetailedUIState.EquipEnchantment)
		{
			return EquipmentManager.Instance.CheckCanShowEnchantmentTipByPos(pos);
		}
		if (state == EquipDetailedUIState.EquipStarUp)
		{
			return EquipmentManager.Instance.CheckCanShowStarUpTipByPos(pos);
		}
		return state == EquipDetailedUIState.EquipStrengthen && EquipmentManager.Instance.CheckCanStrengthen(pos);
	}

	private void CheckCanShowPromoteWay()
	{
		bool isShow = this.CheckCanChangeEquipAllPos();
		StrongerManager.Instance.UpdatePromoteWayDic(PromoteWayType.EquipCanChange, isShow);
	}

	public bool CheckCanChangeEquipAllPos()
	{
		return this.CheckCanChangeEquip(EquipLibType.ELT.Necklace) || this.CheckCanChangeEquip(EquipLibType.ELT.Pant) || this.CheckCanChangeEquip(EquipLibType.ELT.Shirt) || this.CheckCanChangeEquip(EquipLibType.ELT.Shoe) || this.CheckCanChangeEquip(EquipLibType.ELT.Waist) || this.CheckCanChangeEquip(EquipLibType.ELT.Weapon) || this.CheckCanChangeEquip(EquipLibType.ELT.Part7) || this.CheckCanChangeEquip(EquipLibType.ELT.Part8) || this.CheckCanChangeEquip(EquipLibType.ELT.Part9) || this.CheckCanChangeEquip(EquipLibType.ELT.Part10);
	}

	public bool CheckCanChangeEquip(EquipLibType.ELT pos)
	{
		if (EquipmentManager.Instance.equipmentData == null || EquipmentManager.Instance.equipmentData.equipLibs == null)
		{
			return false;
		}
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		if (equipLib == null)
		{
			return false;
		}
		if (EquipmentManager.Instance.dicEquips == null || !EquipmentManager.Instance.dicEquips.ContainsKey(equipLib.wearingId))
		{
			return false;
		}
		int equipFightingByEquipID = EquipmentManager.Instance.GetEquipFightingByEquipID(equipLib.wearingId);
		for (int i = 0; i < equipLib.equips.get_Count(); i++)
		{
			EquipSimpleInfo equipSimpleInfo = equipLib.equips.get_Item(i);
			if (equipSimpleInfo == null)
			{
				return false;
			}
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipSimpleInfo.cfgId);
			if (zZhuangBeiPeiZhiBiao == null)
			{
				return false;
			}
			int equipFightingByEquipID2 = EquipmentManager.Instance.GetEquipFightingByEquipID(equipSimpleInfo.equipId);
			if (equipFightingByEquipID2 > equipFightingByEquipID && EntityWorld.Instance.EntSelf.Lv >= zZhuangBeiPeiZhiBiao.level && zZhuangBeiPeiZhiBiao.occupation == EntityWorld.Instance.EntSelf.TypeID)
			{
				return true;
			}
		}
		return false;
	}

	private void CheckCanShowEquipStrengthenPromoteWay()
	{
		bool isShow = this.CheckCanShowStrengthenTipAllPos();
		StrongerManager.Instance.UpdatePromoteWayDic(PromoteWayType.EquipCanStrength, isShow);
	}

	public bool CheckCanShowStrengthenTipAllPos()
	{
		return this.CheckCanStrengthen(EquipLibType.ELT.Necklace) || this.CheckCanStrengthen(EquipLibType.ELT.Pant) || this.CheckCanStrengthen(EquipLibType.ELT.Shirt) || this.CheckCanStrengthen(EquipLibType.ELT.Shoe) || this.CheckCanStrengthen(EquipLibType.ELT.Waist) || this.CheckCanStrengthen(EquipLibType.ELT.Weapon) || this.CheckCanStrengthen(EquipLibType.ELT.Part7) || this.CheckCanStrengthen(EquipLibType.ELT.Part8) || this.CheckCanStrengthen(EquipLibType.ELT.Part9) || this.CheckCanStrengthen(EquipLibType.ELT.Part10);
	}

	public bool CheckCanStrengthen(EquipLibType.ELT pos)
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return false;
		}
		if (!SystemOpenManager.IsSystemOn(40) || !SystemOpenManager.IsSystemOn(21))
		{
			return false;
		}
		if (EntityWorld.Instance.EntSelf.Gold < 1000000L)
		{
			return false;
		}
		int equipCfgIDByPos = EquipGlobal.GetEquipCfgIDByPos(pos);
		if (equipCfgIDByPos <= 0)
		{
			return false;
		}
		if (EquipmentManager.Instance.equipmentData == null || EquipmentManager.Instance.equipmentData.equipLibs == null)
		{
			return false;
		}
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		if (equipLib == null)
		{
			return false;
		}
		bool result = true;
		zBuWeiQiangHua zBuWeiQiangHua = DataReader<zBuWeiQiangHua>.DataList.Find((zBuWeiQiangHua a) => a.partLv == equipLib.lv + 1);
		if (zBuWeiQiangHua == null)
		{
			return false;
		}
		if (DataReader<zZhuangBeiPeiZhiBiao>.Contains(equipCfgIDByPos))
		{
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgIDByPos);
			if (zZhuangBeiPeiZhiBiao.step < zBuWeiQiangHua.openStep)
			{
				result = false;
			}
		}
		return result;
	}

	public bool CheckCanShowEnchantmentTipAllPos()
	{
		return this.CheckCanShowEnchantmentTipByPos(EquipLibType.ELT.Waist) || this.CheckCanShowEnchantmentTipByPos(EquipLibType.ELT.Weapon) || this.CheckCanShowEnchantmentTipByPos(EquipLibType.ELT.Shoe) || this.CheckCanShowEnchantmentTipByPos(EquipLibType.ELT.Shirt) || this.CheckCanShowEnchantmentTipByPos(EquipLibType.ELT.Pant) || this.CheckCanShowEnchantmentTipByPos(EquipLibType.ELT.Necklace) || this.CheckCanShowEnchantmentTipByPos(EquipLibType.ELT.Part7) || this.CheckCanShowEnchantmentTipByPos(EquipLibType.ELT.Part8) || this.CheckCanShowEnchantmentTipByPos(EquipLibType.ELT.Part9) || this.CheckCanShowEnchantmentTipByPos(EquipLibType.ELT.Part10);
	}

	public bool CheckCanShowEnchantmentTipByPos(EquipLibType.ELT pos)
	{
		if (this.CanShowEnchantmentTipPosDic != null && this.CanShowEnchantmentTipPosDic.ContainsKey(pos) && this.CanShowEnchantmentTipPosDic.get_Item(pos))
		{
			if (!SystemOpenManager.IsSystemOn(40) || !SystemOpenManager.IsSystemOn(39))
			{
				return false;
			}
			int equipCfgIDByPos = EquipGlobal.GetEquipCfgIDByPos(pos);
			if (!DataReader<zZhuangBeiPeiZhiBiao>.Contains(equipCfgIDByPos))
			{
				return false;
			}
			zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipCfgIDByPos);
			if (zZhuangBeiPeiZhiBiao.enchantNum <= 0 || zZhuangBeiPeiZhiBiao.level < EquipGlobal.GetCanEnchantmentMinLv())
			{
				return false;
			}
			for (int i = 0; i < zZhuangBeiPeiZhiBiao.enchantNum; i++)
			{
				if (this.CheckCanShowEnchantmentTipBySlotIndex(i, pos))
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool CheckCanShowEnchantmentTipBySlotIndex(int index, EquipLibType.ELT pos)
	{
		if (!SystemOpenManager.IsSystemOn(40) || !SystemOpenManager.IsSystemOn(39))
		{
			return false;
		}
		if (this.CanShowEnchantmentTipSlotsDic == null || !this.CanShowEnchantmentTipSlotsDic.ContainsKey(pos))
		{
			return false;
		}
		int num = this.CanShowEnchantmentTipSlotsDic.get_Item(pos).FindIndex((int a) => a == index);
		if (num >= 0)
		{
			return false;
		}
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == pos);
		if (equipLib == null)
		{
			return false;
		}
		EquipSimpleInfo equipSimpleInfo = equipLib.equips.Find((EquipSimpleInfo a) => a.equipId == equipLib.wearingId);
		if (equipSimpleInfo == null)
		{
			return false;
		}
		int cfgId = equipSimpleInfo.cfgId;
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(cfgId);
		if (zZhuangBeiPeiZhiBiao == null)
		{
			return false;
		}
		if (zZhuangBeiPeiZhiBiao.enchantNum <= 0 || zZhuangBeiPeiZhiBiao.level < EquipGlobal.GetCanEnchantmentMinLv())
		{
			return false;
		}
		for (int i = 0; i < zZhuangBeiPeiZhiBiao.enchantNum; i++)
		{
			List<Goods> canEnchantmentGoods = EquipGlobal.GetCanEnchantmentGoods((int)pos, i);
			if (canEnchantmentGoods != null && canEnchantmentGoods.get_Count() > 0)
			{
				if (index == i)
				{
					if (equipSimpleInfo.enchantAttrs.get_Count() <= i || equipSimpleInfo.enchantAttrs.get_Item(i).attrId <= 0)
					{
						return true;
					}
					int attrId = equipSimpleInfo.enchantAttrs.get_Item(i).attrId;
					int num2 = attrId / 100;
					for (int j = 0; j < canEnchantmentGoods.get_Count(); j++)
					{
						int itemId = canEnchantmentGoods.get_Item(j).GetItemId();
						int num3 = itemId / 100;
						if (num3 > num2)
						{
							return true;
						}
						if (num3 == num2 && DataReader<FuMoDaoJuShuXing>.Contains(attrId))
						{
							FuMoDaoJuShuXing fuMoDaoJuShuXing = DataReader<FuMoDaoJuShuXing>.Get(attrId);
							if (fuMoDaoJuShuXing.runeValue.get_Count() >= 2)
							{
								int num4 = fuMoDaoJuShuXing.runeValue.get_Item(1);
								if ((long)num4 > equipSimpleInfo.enchantAttrs.get_Item(i).value)
								{
									return true;
								}
							}
						}
					}
				}
			}
		}
		return false;
	}

	public bool CheckCanShowStarUpTipAllPos()
	{
		return this.CheckCanShowStarUpTipByPos(EquipLibType.ELT.Waist) || this.CheckCanShowStarUpTipByPos(EquipLibType.ELT.Weapon) || this.CheckCanShowStarUpTipByPos(EquipLibType.ELT.Shoe) || this.CheckCanShowStarUpTipByPos(EquipLibType.ELT.Shirt) || this.CheckCanShowStarUpTipByPos(EquipLibType.ELT.Pant) || this.CheckCanShowStarUpTipByPos(EquipLibType.ELT.Necklace) || this.CheckCanShowStarUpTipByPos(EquipLibType.ELT.Part7) || this.CheckCanShowStarUpTipByPos(EquipLibType.ELT.Part8) || this.CheckCanShowStarUpTipByPos(EquipLibType.ELT.Part9) || this.CheckCanShowStarUpTipByPos(EquipLibType.ELT.Part10);
	}

	public bool CheckCanShowStarUpTipByPos(EquipLibType.ELT pos)
	{
		EquipmentManager.<CheckCanShowStarUpTipByPos>c__AnonStorey108 <CheckCanShowStarUpTipByPos>c__AnonStorey = new EquipmentManager.<CheckCanShowStarUpTipByPos>c__AnonStorey108();
		<CheckCanShowStarUpTipByPos>c__AnonStorey.pos = pos;
		if (this.CanShowStarUpTipPosDic == null || !this.CanShowStarUpTipPosDic.ContainsKey(<CheckCanShowStarUpTipByPos>c__AnonStorey.pos) || !this.CanShowStarUpTipPosDic.get_Item(<CheckCanShowStarUpTipByPos>c__AnonStorey.pos))
		{
			return false;
		}
		if (!SystemOpenManager.IsSystemOn(40) || !SystemOpenManager.IsSystemOn(37))
		{
			return false;
		}
		if (BackpackManager.Instance.OnGetGoodCount(8) <= 0L && BackpackManager.Instance.OnGetGoodCount(9) <= 0L && BackpackManager.Instance.OnGetGoodCount(10) <= 0L)
		{
			return false;
		}
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == <CheckCanShowStarUpTipByPos>c__AnonStorey.pos);
		if (equipLib == null)
		{
			return false;
		}
		EquipSimpleInfo equipSimpleInfo = equipLib.equips.Find((EquipSimpleInfo a) => a.equipId == equipLib.wearingId);
		if (equipSimpleInfo == null)
		{
			return false;
		}
		List<int> value = DataReader<ShengXingJiChuPeiZhi>.Get("boostStarLevel").value;
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipSimpleInfo.cfgId);
		return zZhuangBeiPeiZhiBiao != null && zZhuangBeiPeiZhiBiao.level >= EquipGlobal.GetCanStarUpMinLv() && zZhuangBeiPeiZhiBiao.starNum > 0 && equipSimpleInfo.star < zZhuangBeiPeiZhiBiao.starNum && equipLib.lv >= value.get_Item(equipSimpleInfo.star);
	}

	public List<KeyValuePair<int, int>> GetEquipSuitBattleSkillExtendIDs()
	{
		List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
		List<int> equipSuitCfgSkillTemplateIDList = this.GetEquipSuitCfgSkillTemplateIDList();
		if (equipSuitCfgSkillTemplateIDList != null && equipSuitCfgSkillTemplateIDList.get_Count() > 0)
		{
			for (int i = 0; i < equipSuitCfgSkillTemplateIDList.get_Count(); i++)
			{
				if (equipSuitCfgSkillTemplateIDList.get_Item(i) > 0)
				{
					list.Add(new KeyValuePair<int, int>(equipSuitCfgSkillTemplateIDList.get_Item(i), 1));
				}
			}
		}
		return list;
	}

	public List<int> GetEquipSuitCfgSkillTemplateIDList()
	{
		List<int> result = new List<int>();
		if (this.EquipSuitActiveIndexIds != null && this.EquipSuitActiveIndexIds.get_Count() > 0)
		{
			for (int i = 0; i < this.EquipSuitActiveIndexIds.get_Count(); i++)
			{
				int key = this.EquipSuitActiveIndexIds.get_Item(i);
				if (DataReader<TaoZhuangDuanZhu>.Contains(key))
				{
					TaoZhuangDuanZhu taoZhuangDuanZhu = DataReader<TaoZhuangDuanZhu>.Get(key);
					if (taoZhuangDuanZhu.effectType == 2)
					{
						return taoZhuangDuanZhu.skillId;
					}
				}
			}
		}
		return result;
	}

	public int GetEquipFightingByExcellentAttrs(int itemID, List<ExcellentAttr> excellentAttrs)
	{
		float num = (float)this.GetEquipFightingByItemID(itemID);
		if (excellentAttrs == null)
		{
			return (int)num;
		}
		for (int i = 0; i < excellentAttrs.get_Count(); i++)
		{
			int attrId = excellentAttrs.get_Item(i).attrId;
			float num2 = (float)excellentAttrs.get_Item(i).value;
			ZhanDouLiBiaoZhun zhanDouLiBiaoZhun = DataReader<ZhanDouLiBiaoZhun>.Get(attrId);
			if (zhanDouLiBiaoZhun != null)
			{
				num += num2 * zhanDouLiBiaoZhun.unit * zhanDouLiBiaoZhun.unitFightPower * (float)zhanDouLiBiaoZhun.equitFightPower;
			}
		}
		return (int)num;
	}

	public int GetEquipFightingByEquipID(long equipID)
	{
		EquipSimpleInfo equipSimpleInfo = null;
		if (this.dicEquips.ContainsKey(equipID))
		{
			equipSimpleInfo = this.dicEquips.get_Item(equipID);
		}
		if (equipSimpleInfo != null)
		{
			float num = (float)this.GetEquipFightingByItemID(equipSimpleInfo.cfgId);
			for (int i = 0; i < equipSimpleInfo.excellentAttrs.get_Count(); i++)
			{
				int attrId = equipSimpleInfo.excellentAttrs.get_Item(i).attrId;
				float num2 = (float)equipSimpleInfo.excellentAttrs.get_Item(i).value;
				ZhanDouLiBiaoZhun zhanDouLiBiaoZhun = DataReader<ZhanDouLiBiaoZhun>.Get(attrId);
				if (zhanDouLiBiaoZhun != null)
				{
					num += num2 * zhanDouLiBiaoZhun.unit * zhanDouLiBiaoZhun.unitFightPower * (float)zhanDouLiBiaoZhun.equitFightPower;
				}
			}
			return (int)num;
		}
		return 0;
	}

	public int GetEquipFightingByItemID(int itemID)
	{
		zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(itemID);
		if (zZhuangBeiPeiZhiBiao == null)
		{
			return 0;
		}
		Attrs attrs = DataReader<Attrs>.Get(zZhuangBeiPeiZhiBiao.attrBaseValue);
		if (attrs == null)
		{
			return 0;
		}
		float num = 0f;
		for (int i = 0; i < attrs.attrs.get_Count(); i++)
		{
			int key = attrs.attrs.get_Item(i);
			if (i >= attrs.values.get_Count())
			{
				break;
			}
			float num2 = (float)attrs.values.get_Item(i);
			ZhanDouLiBiaoZhun zhanDouLiBiaoZhun = DataReader<ZhanDouLiBiaoZhun>.Get(key);
			if (zhanDouLiBiaoZhun != null)
			{
				num += num2 * zhanDouLiBiaoZhun.unit * zhanDouLiBiaoZhun.unitFightPower * (float)zhanDouLiBiaoZhun.equitFightPower;
			}
		}
		return (int)num;
	}

	public void ShowIfGoOnWashOperate()
	{
		if (this.dicEquips.ContainsKey(this.LastRefineEquipID) && this.dicEquips.get_Item(this.LastRefineEquipID).refineData != null && this.dicEquips.get_Item(this.LastRefineEquipID).refineData.attrId > 0)
		{
			string chineseContent = GameDataUtils.GetChineseContent(621264, false);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(chineseContent, "是否继续执行之前中断的洗炼操作", delegate
			{
				EquipmentManager.Instance.SendCancelRefineDataReq(this.LastRefineEquipPart, this.LastRefineEquipID);
			}, delegate
			{
				LinkNavigationManager.OpenEquipWashUI((EquipLibType.ELT)this.LastRefineEquipPart, null);
			}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
		}
	}

	private void ClearEquipNoteData()
	{
		EquipLib equipLib = EquipmentManager.Instance.equipmentData.equipLibs.Find((EquipLib a) => a.type == (EquipLibType.ELT)this.refineEquipPart);
		int num = equipLib.equips.FindIndex((EquipSimpleInfo a) => a.equipId == this.refineEquipID);
		if (num >= 0 && equipLib.equips.get_Item(num).refineData != null)
		{
			equipLib.equips.get_Item(num).refineData = null;
			this.UpdateDicEquips(this.refineEquipID, equipLib.equips.get_Item(num));
		}
	}

	public void RemoveGuildStorageDonateEquips(List<long> equipIds)
	{
		this.RemoveEquipIds(equipIds);
		this.CheckCanShowPromoteWay();
	}

	public void RemoveEquipIds(List<long> equipIds)
	{
		if (equipIds == null || equipIds.get_Count() <= 0)
		{
			return;
		}
		for (int i = 0; i < equipIds.get_Count(); i++)
		{
			long equipID = equipIds.get_Item(i);
			for (int j = 0; j < this.equipmentData.equipLibs.get_Count(); j++)
			{
				int num = this.equipmentData.equipLibs.get_Item(j).equips.FindIndex((EquipSimpleInfo e) => e.equipId == equipID);
				if (num >= 0)
				{
					this.equipmentData.equipLibs.get_Item(j).equips.RemoveAt(num);
				}
			}
			int position = DataReader<zZhuangBeiPeiZhiBiao>.Get(EquipGlobal.GetEquipCfgIDByEquipID(equipID)).position;
			this.RemoveFromDicEquips(equipID);
			this.CheckCanRecommendByPos((EquipLibType.ELT)position);
		}
	}
}
