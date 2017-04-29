using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using XNetwork;

public class PetManager : PetManagerBase
{
	public class MEventNames
	{
		public const string RefreshPets = "PetManager.RefreshPets";

		public const string ComposeRuneSucess = "PetManager.ComposeRuneSucess";

		public const string PetFormationHaveChange = "PetManager.PetFormationHaveChange";
	}

	private struct ObtainPet
	{
		public int petId;

		public int obtain_star;

		public int decompose_star;

		public bool exist;

		public bool replace;

		public string petName;
	}

	public const int MAX_FORMATION_PET = 3;

	private const int ID2SupportSkillCDMinus = 6;

	private const int ID2FuseTimePlus = 7;

	private const int ID2BeginActPoint = 8;

	public const int PET_INIT_LEVEL = 1;

	public const int PET_INIT_STAR = 1;

	public const int PET_MAX_STAR = 9;

	private Dictionary<long, PetInfo> _maplistPet = new Dictionary<long, PetInfo>();

	private List<PetFormation> formation;

	public PetInfo TempOldPetInfo;

	private PetExtraInfos m_ExtraInfos;

	public int CurrentFormationID = 1;

	public static List<long> PetActiveIds = new List<long>();

	private static PetManager instance;

	public bool IsLuckDrawing;

	private List<PetManager.ObtainPet> ObtainPetNtys = new List<PetManager.ObtainPet>();

	public Action mPetObtainUIFinishedCallback;

	private string _SkillPointInfo = string.Empty;

	private string format_skillPoint = string.Empty;

	private TimeCountDown m_timeCDOfSkillPoint;

	private int MaxSkillPoint;

	private List<UpgradeUnitData> m_UnitsOfUp = new List<UpgradeUnitData>();

	private List<UpgradeUnitData> m_UnitsOfNew = new List<UpgradeUnitData>();

	private List<int> modelIds = new List<int>();

	private static readonly bool IsUpgradeTipOn = true;

	private static readonly bool IsLevelUpTipOn = true;

	private static readonly bool IsSkillUpTipOn = false;

	private static readonly bool IsActivateTipOn = false;

	private static readonly bool IsFormationTipOn = true;

	private List<int> _ExpItemIds;

	private int fx_SuccessOfLevelUp;

	private List<PetInfo> resultOfPet = new List<PetInfo>();

	private List<PetInfo> m_experiencePets;

	private bool m_IsFormationFromInstance;

	private bool m_IsFromLink;

	private List<int> battleUIds = new List<int>();

	private string[] actors_layers = new string[]
	{
		"Default",
		"CameraRange",
		"LayerA",
		"LayerB",
		"LayerC",
		"LayerD",
		"LayerE",
		"LayerF",
		"LayerG",
		"LayerH",
		"LayerI"
	};

	public Dictionary<long, PetInfo> MaplistPet
	{
		get
		{
			return this._maplistPet;
		}
		set
		{
			this._maplistPet = value;
		}
	}

	public List<PetFormation> Formation
	{
		get
		{
			if (this.formation == null)
			{
				this.formation = new List<PetFormation>();
			}
			return this.formation;
		}
	}

	public static PetManager Instance
	{
		get
		{
			if (PetManager.instance == null)
			{
				PetManager.instance = new PetManager();
				PetManager.instance.InitSkillPointMax();
				TimerHeap.AddTimer(15000u, 10000, delegate
				{
					PetManager.instance.CheckBadge();
				});
			}
			return PetManager.instance;
		}
	}

	public string SkillPointInfo
	{
		get
		{
			return this._SkillPointInfo;
		}
		set
		{
			this._SkillPointInfo = value;
			if (PetBasicUIViewModel.Instance != null && PetBasicUIViewModel.Instance.get_gameObject().get_activeSelf())
			{
				PetBasicUIViewModel.Instance.SkillPointInfo = value;
			}
		}
	}

	public List<int> ExpItemIds
	{
		get
		{
			if (this._ExpItemIds == null)
			{
				this._ExpItemIds = new List<int>();
				string time = DataReader<CChongWuSheZhi>.Get("upgradeCost").time;
				string[] array = time.Split(new char[]
				{
					';'
				});
				for (int i = 0; i < array.Length; i++)
				{
					this._ExpItemIds.Add(int.Parse(array[i]));
				}
			}
			return this._ExpItemIds;
		}
	}

	public bool IsFormationFromInstance
	{
		get
		{
			return this.m_IsFormationFromInstance;
		}
		set
		{
			this.m_IsFormationFromInstance = value;
		}
	}

	public bool IsFromLink
	{
		get
		{
			return this.m_IsFromLink;
		}
		set
		{
			this.m_IsFromLink = value;
		}
	}

	private PetManager()
	{
	}

	private void Clone2Temp(PetInfo petInfo)
	{
		MemoryStream memoryStream = new MemoryStream();
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		binaryFormatter.Serialize(memoryStream, petInfo);
		memoryStream.set_Position(0L);
		this.TempOldPetInfo = (binaryFormatter.Deserialize(memoryStream) as PetInfo);
	}

	public int GetSkillPoint()
	{
		if (this.m_ExtraInfos != null)
		{
			return this.m_ExtraInfos.skillPoint;
		}
		return 0;
	}

	public static long GetLinkPet(long currentUId, bool next)
	{
		if (PetManager.PetActiveIds.get_Count() == 0)
		{
			return 0L;
		}
		int i = 0;
		while (i < PetManager.PetActiveIds.get_Count())
		{
			if (PetManager.PetActiveIds.get_Item(i) == currentUId)
			{
				if (next)
				{
					if (i + 1 < PetManager.PetActiveIds.get_Count())
					{
						return PetManager.PetActiveIds.get_Item(i + 1);
					}
					return PetManager.PetActiveIds.get_Item(0);
				}
				else
				{
					if (i - 1 >= 0)
					{
						return PetManager.PetActiveIds.get_Item(i - 1);
					}
					return PetManager.PetActiveIds.get_Item(PetManager.PetActiveIds.get_Count() - 1);
				}
			}
			else
			{
				i++;
			}
		}
		return 0L;
	}

	public static bool IsLinkPetMoreOne()
	{
		return PetManager.PetActiveIds.get_Count() > 1;
	}

	public PetInfo GetFollow()
	{
		return this.GetPetInfo(this.m_ExtraInfos.petUUId);
	}

	public bool IsFollow(long uuid)
	{
		return this.m_ExtraInfos.petUUId == uuid;
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.MaplistPet.Clear();
		this.Formation.Clear();
		this.m_ExtraInfos = null;
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<PetExtraInfos>(new NetCallBackMethod<PetExtraInfos>(this.OnPetExtraInfos));
		NetworkManager.AddListenEvent<SkillPointChangedNty>(new NetCallBackMethod<SkillPointChangedNty>(this.OnSkillPointChangedNty));
		NetworkManager.AddListenEvent<RoleAttrChangedNty>(new NetCallBackMethod<RoleAttrChangedNty>(this.UpdateAttr));
		NetworkManager.AddListenEvent<PetInfos>(new NetCallBackMethod<PetInfos>(this.OnGetPetDataRes));
		NetworkManager.AddListenEvent<PetFormationPush>(new NetCallBackMethod<PetFormationPush>(this.OnGetPetFormationDataRes));
		NetworkManager.AddListenEvent<SetFormationRes>(new NetCallBackMethod<SetFormationRes>(this.OnGetPetFormationResDataRes));
		NetworkManager.AddListenEvent<ComposePetRes>(new NetCallBackMethod<ComposePetRes>(this.OnComposePetRes));
		NetworkManager.AddListenEvent<ObtainPetNty>(new NetCallBackMethod<ObtainPetNty>(this.OnObtainPetNty));
		NetworkManager.AddListenEvent<PetUpStarRes>(new NetCallBackMethod<PetUpStarRes>(this.OnPetUpStarRes));
		NetworkManager.AddListenEvent<PetUpgradeRes>(new NetCallBackMethod<PetUpgradeRes>(this.OnPetUpgradeRes));
		NetworkManager.AddListenEvent<PetTalentTrainRes>(new NetCallBackMethod<PetTalentTrainRes>(this.OnRecvPetTalentTrainRes));
		NetworkManager.AddListenEvent<PurchaseSkillPointRes>(new NetCallBackMethod<PurchaseSkillPointRes>(this.OnPurchaseSkillPointRes));
		NetworkManager.AddListenEvent<PetLevelUpRes>(new NetCallBackMethod<PetLevelUpRes>(this.OnPetUpgradeLevelRes));
		NetworkManager.AddListenEvent<SetCurFormationIdRes>(new NetCallBackMethod<SetCurFormationIdRes>(this.OnGetSetCurFormationIdRes));
		NetworkManager.AddListenEvent<ChangeMainCityFollowPetRes>(new NetCallBackMethod<ChangeMainCityFollowPetRes>(this.OnChangeMainCityFollowPetRes));
		NetworkManager.AddListenEvent<PresResetPetLvRes>(new NetCallBackMethod<PresResetPetLvRes>(this.OnPresResetPetLvRes));
		NetworkManager.AddListenEvent<ResetPetLvRes>(new NetCallBackMethod<ResetPetLvRes>(this.OnResetPetLvRes));
		NetworkManager.AddListenEvent<PresPetUpStarRes>(new NetCallBackMethod<PresPetUpStarRes>(this.OnPresPetUpStarRes));
	}

	public void SendPetTalentTrainReq(long uuid, int _talentId)
	{
		NetworkManager.Send(new PetTalentTrainReq
		{
			petUUId = uuid,
			talentId = _talentId
		}, ServerType.Data);
	}

	public void SendMsgPetFormation(int formationId, List<long> listPets)
	{
		PetFormation petFormation = new PetFormation();
		Int64ArrayMsg int64ArrayMsg = new Int64ArrayMsg();
		petFormation.petFormationArr = int64ArrayMsg;
		petFormation.formationId = formationId;
		for (int i = 0; i < listPets.get_Count(); i++)
		{
			Int64IndexValue int64IndexValue = new Int64IndexValue();
			int64IndexValue.index = i;
			int64IndexValue.value = listPets.get_Item(i);
			int64ArrayMsg.Int64Array.Add(int64IndexValue);
		}
		NetworkManager.Send(new SetFormationReq
		{
			formation = petFormation
		}, ServerType.Data);
	}

	public void SendComposePet(int _petId)
	{
		NetworkManager.Send(new ComposePetReq
		{
			toPetId = _petId
		}, ServerType.Data);
	}

	public void SendPetUpStar(long petUid)
	{
		if (this.TempOldPetInfo != null)
		{
			UIManagerControl.Instance.ShowToastText("请求过于频繁,请稍等");
			return;
		}
		this.Clone2Temp(this.GetPetInfo(petUid));
		NetworkManager.Send(new PetUpStarReq
		{
			petUUId = petUid
		}, ServerType.Data);
	}

	public void SendRuneComposite(int _matId)
	{
		NetworkManager.Send(new RuneCompositeReq
		{
			toItemId = _matId
		}, ServerType.Data);
	}

	public void SendPurchaseSkillPoint()
	{
		NetworkManager.Send(new PurchaseSkillPointReq(), ServerType.Data);
	}

	public void SendUpgradeLevel(long petUid, int itemId, int level)
	{
		if (this.TempOldPetInfo != null)
		{
			return;
		}
		this.Clone2Temp(this.GetPetInfo(petUid));
		NetworkManager.Send(new PetLevelUpReq
		{
			petUUId = petUid,
			itemId = itemId,
			levelTimes = level
		}, ServerType.Data);
	}

	public void SendSetCurFormationIdReq(int formationId)
	{
		NetworkManager.Send(new SetCurFormationIdReq
		{
			formationId = formationId
		}, ServerType.Data);
	}

	public void SendChangeMainCityFollowPet(long id)
	{
		NetworkManager.Send(new ChangeMainCityFollowPetReq
		{
			petUUId = id
		}, ServerType.Data);
	}

	public void SendPresResetPetLv(long petUid)
	{
		NetworkManager.Send(new PresResetPetLvReq
		{
			petUUId = petUid
		}, ServerType.Data);
	}

	public void SendResetPetLv(long petUid)
	{
		NetworkManager.Send(new ResetPetLvReq
		{
			petUUId = petUid
		}, ServerType.Data);
	}

	public void SendPresPetUpStar(long petUid)
	{
		if (petUid <= 0L)
		{
			return;
		}
		NetworkManager.Send(new PresPetUpStarReq
		{
			petUUId = petUid
		}, ServerType.Data);
	}

	private void OnChangeMainCityFollowPetRes(short state, ChangeMainCityFollowPetRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.m_ExtraInfos.petUUId = down.petUUId;
		if (PetBasicUIViewModel.Instance != null && PetBasicUIViewModel.Instance.get_gameObject().get_activeSelf())
		{
			PetBasicUIViewModel.Instance.SetFollow(PetBasicUIViewModel.PetUID);
		}
	}

	private void OnGetSetCurFormationIdRes(short state, SetCurFormationIdRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.CurrentFormationID = down.formationId;
		EventDispatcher.Broadcast(EventNames.OnGetSetCurFormationIdRes);
	}

	private void OnPetExtraInfos(short state, PetExtraInfos down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.m_ExtraInfos = down;
		this.SetSkillPoint(this.m_ExtraInfos.skillPoint, this.m_ExtraInfos.residueRecoverTime);
	}

	private void OnSkillPointChangedNty(short state, SkillPointChangedNty down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (this.m_ExtraInfos == null)
		{
			Debug.LogError("m_ExtraInfos is null");
			return;
		}
		this.m_ExtraInfos.skillPoint = down.petSkillPoint;
		this.m_ExtraInfos.residueRecoverTime = down.residueRecoverTime;
		this.SetSkillPoint(this.m_ExtraInfos.skillPoint, this.m_ExtraInfos.residueRecoverTime);
	}

	private void OnGetPetDataRes(short state, PetInfos down = null)
	{
		if (state != 0 || down == null)
		{
			return;
		}
		for (int i = 0; i < down.info.get_Count(); i++)
		{
			this.MaplistPet.set_Item(down.info.get_Item(i).id, down.info.get_Item(i));
		}
		this.OnRefreshPets();
	}

	private void OnGetPetFormationDataRes(short state, PetFormationPush down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.CurrentFormationID = down.formationId;
		this.formation = down.formations;
	}

	private void OnGetPetFormationResDataRes(short state, SetFormationRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		int num = this.formation.FindIndex((PetFormation a) => a.formationId == down.formation.formationId);
		if (num == -1)
		{
			this.formation.Add(down.formation);
		}
		else
		{
			this.formation.set_Item(num, down.formation);
		}
		EventDispatcher.Broadcast("PetManager.PetFormationHaveChange");
		if (UIManagerControl.Instance.IsOpen("PetBasicUI"))
		{
			PetBasicUIViewModel.Instance.SubFormationBadge = PetManager.Instance.CheckAllCanFormation();
		}
	}

	private void OnComposePetRes(short state, ComposePetRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down.petInfo != null)
		{
			this.MaplistPet.set_Item(down.petInfo.id, down.petInfo);
			if (!PetManager.PetActiveIds.Contains(down.petInfo.id))
			{
				PetManager.PetActiveIds.Add(down.petInfo.id);
			}
			this.OnRefreshPets();
			if (PetBasicUIViewModel.Instance != null && PetBasicUIViewModel.Instance.get_gameObject().get_activeSelf())
			{
				PetBasicUIViewModel.Instance.ShowSelectedPetInfo(down.petInfo.id, down.petInfo.petId);
				PetBasicUIView.Instance.ShowPetBackground(true);
				PetBasicUIView.Instance.SetPositionMiddle();
				PetBasicUIView.Instance.SetRawImageModelLayer(false, false, true);
				PetBasicUIViewModel.Instance.SubPanelPetInfo = true;
				PetBasicUIViewModel.Instance.SubPanelPetInfoRoot = false;
				CurrenciesUIViewModel.Show(false);
				int templateId = 919;
				ModelDisplayManager.Instance.HideModel(true);
				PetBasicUIView.Instance.SetRawImageModelLayer(true, true, true);
				FXManager.Instance.Preload(templateId, delegate
				{
					XTaskManager.instance.StartTask(this.AnimSuccessOfCompose(templateId), null);
				});
			}
		}
	}

	private void OnObtainPetNty(short state, ObtainPetNty down = null)
	{
		if (state != 0 || down == null || down.petInfo == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (this.IsLuckDrawing)
		{
			this.MaplistPet.set_Item(down.petInfo.id, down.petInfo);
		}
		else
		{
			int obtain_star = (down.getStar <= 0) ? down.petInfo.star : down.getStar;
			int num = (down.getStar <= 0) ? down.petInfo.star : down.getStar;
			bool replace = false;
			string petName = string.Empty;
			PetInfo petInfoById = this.GetPetInfoById(down.petInfo.petId);
			bool exist;
			if (petInfoById == null)
			{
				exist = false;
			}
			else
			{
				exist = true;
				if (num > petInfoById.star)
				{
					replace = true;
					petName = PetManager.GetPetName(petInfoById.petId, num);
					num = petInfoById.star;
				}
				else
				{
					replace = false;
					petName = PetManager.GetPetName(petInfoById.petId, petInfoById.star);
				}
			}
			this.MaplistPet.set_Item(down.petInfo.id, down.petInfo);
			this.JustObtainPetNty(down.petInfo.petId, obtain_star, num, exist, replace, petName, null);
		}
	}

	public void JustObtainPetNty(int petId, int obtain_star, int decompose_star, bool exist, bool replace, string petName, Action finishedCallback = null)
	{
		PetManager.ObtainPet obtainPet = default(PetManager.ObtainPet);
		obtainPet.petId = petId;
		obtainPet.obtain_star = obtain_star;
		obtainPet.decompose_star = decompose_star;
		obtainPet.exist = exist;
		obtainPet.replace = replace;
		obtainPet.petName = petName;
		this.mPetObtainUIFinishedCallback = finishedCallback;
		this.ObtainPetNtys.Add(obtainPet);
		if (LevelUpUIView.Instance != null && LevelUpUIView.Instance.get_gameObject() != null && LevelUpUIView.Instance.get_gameObject().get_activeSelf())
		{
			UIQueueManager.Instance.Push(delegate
			{
				this.CheckObtainPetNty(true);
			}, PopPriority.JustPop, PopCondition.None);
		}
		else
		{
			this.CheckObtainPetNty(true);
		}
	}

	public bool CheckObtainPetNty(bool needuiclose)
	{
		if (needuiclose && PetObtainUIViewModel.Instance != null && PetObtainUIViewModel.Instance.get_gameObject() != null && PetObtainUIViewModel.Instance.get_gameObject().get_activeInHierarchy())
		{
			return false;
		}
		if (this.ObtainPetNtys.get_Count() == 0)
		{
			return false;
		}
		int petId = this.ObtainPetNtys.get_Item(0).petId;
		int obtain_star = this.ObtainPetNtys.get_Item(0).obtain_star;
		int decompose_star = this.ObtainPetNtys.get_Item(0).decompose_star;
		bool exist = this.ObtainPetNtys.get_Item(0).exist;
		bool replace = this.ObtainPetNtys.get_Item(0).replace;
		string petName = this.ObtainPetNtys.get_Item(0).petName;
		this.ObtainPetNtys.RemoveAt(0);
		if (petId > 0)
		{
			PetManagerBase.GetBackgroundById(petId);
			UIManagerControl.Instance.OpenUI("PetObtainUI", UINodesManager.TopUIRoot, false, UIType.NonPush);
			PetObtainUIViewModel.Instance.SetPetInfo(petId, obtain_star, decompose_star, exist, replace, petName);
			int templateId = 919;
			ModelDisplayManager.Instance.HideModel(true);
			if (PetBasicUIView.Instance != null)
			{
				PetBasicUIView.Instance.SetRawImageModelLayer(true, true, true);
			}
			FXManager.Instance.Preload(templateId, delegate
			{
				Pet dataPet = DataReader<Pet>.Get(petId);
				ModelDisplayManager.Instance.ShowModel(PetManagerBase.GetPlayerPetModel(dataPet, obtain_star), false, ModelDisplayManager.OFFSET_TO_PETUI, delegate(int uid)
				{
					XTaskManager.instance.StartTask(PetManager.Instance.AnimSuccessOfObtain(petId, uid, templateId), null);
				});
			});
			return true;
		}
		return false;
	}

	private void OnPetUpStarRes(short state, PetUpStarRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			this.TempOldPetInfo = null;
			return;
		}
		if (down.petInfo != null)
		{
			this.MaplistPet.set_Item(down.petInfo.id, down.petInfo);
			this.OnRefreshPets();
			if (this.MaplistPet.ContainsKey(down.petInfo.id))
			{
				PetInfo petInfo = down.petInfo;
				this.SuccessPanelOfPetStarUp(this.TempOldPetInfo, petInfo);
			}
			if (UIManagerControl.Instance.IsOpen("PetBasicUI"))
			{
				PetBasicUIViewModel.Instance.ShowSelectedPetInfo();
				PetBasicUIViewModel.Instance.RefreshUpgradeUI();
			}
		}
		this.TempOldPetInfo = null;
	}

	private void OnPetUpgradeRes(short state, PetUpgradeRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			this.TempOldPetInfo = null;
			return;
		}
		if (down.petInfo != null)
		{
			this.MaplistPet.set_Item(down.petInfo.id, down.petInfo);
			if (PetBasicUIViewModel.Instance != null && PetBasicUIViewModel.Instance.get_gameObject().get_activeSelf())
			{
				PetBasicUIViewModel.Instance.ShowSelectedPetInfo();
				this.OnRefreshPets();
			}
			if (this.MaplistPet.ContainsKey(down.petInfo.id))
			{
			}
		}
		this.TempOldPetInfo = null;
	}

	private void OnPetUpgradeLevelRes(short state, PetLevelUpRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			OOPetEXPUnit.IsCancelDown = true;
			this.TempOldPetInfo = null;
			return;
		}
		if (down.petInfo != null)
		{
			int uplevel = down.petInfo.lv - this.TempOldPetInfo.lv;
			this.MaplistPet.set_Item(down.petInfo.id, down.petInfo);
			if (UIManagerControl.Instance.IsOpen("PetBasicUI"))
			{
				PetBasicUIViewModel.Instance.lockRefreshEXPUI = true;
				PetBasicUIViewModel.Instance.ShowSelectedPetInfo();
				PetBasicUIViewModel.Instance.lockRefreshEXPUI = false;
				long num = down.petInfo.publicBaseInfo.simpleInfo.Fighting - this.TempOldPetInfo.publicBaseInfo.simpleInfo.Fighting;
				PetBasicUIViewModel.Instance.SetFighting(down.petInfo.publicBaseInfo.simpleInfo.Fighting, num > 0L, this.TempOldPetInfo.publicBaseInfo.simpleInfo.Fighting);
				PetBasicUIViewModel.Instance.RefreshEXPUI(true, uplevel);
				this.OnRefreshPets();
			}
		}
		this.TempOldPetInfo = null;
	}

	private void OnRecvPetTalentTrainRes(short state, PetTalentTrainRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.MaplistPet.set_Item(down.petInfo.id, down.petInfo);
		this.UpdatePetEvoUI();
		this.m_ExtraInfos.skillPoint = down.skillPoint;
		this.SetSkillPoint(this.m_ExtraInfos.skillPoint);
		PetBasicUIViewModel.Instance.CheckPetEvoBadge();
	}

	private void OnPurchaseSkillPointRes(short state, PurchaseSkillPointRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		this.m_ExtraInfos.skillPoint = down.skillPoint;
		this.SetSkillPoint(this.m_ExtraInfos.skillPoint);
	}

	private void OnPresResetPetLvRes(short state, PresResetPetLvRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		string itemString = PetManager.GetItemString(down.items);
		string content = string.Format("是否花费{0}钻石重置宠物等级?\n重置宠物返还{1}", down.needDiamond, itemString);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel("重置宠物等级", content, delegate
		{
		}, delegate
		{
			PetManager.Instance.SendResetPetLv(down.petUUId);
		}, GameDataUtils.GetChineseContent(500012, false), "确定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void OnResetPetLvRes(short state, ResetPetLvRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down.petInfo != null)
		{
			this.MaplistPet.set_Item(down.petInfo.id, down.petInfo);
			if (UIManagerControl.Instance.IsOpen("PetBasicUI"))
			{
				PetBasicUIViewModel.Instance.ShowSelectedPetInfo();
				PetBasicUIViewModel.Instance.SetFighting(down.petInfo.publicBaseInfo.simpleInfo.Fighting, false, 0L);
				PetBasicUIViewModel.Instance.RefreshEXPUI(false, 0);
				this.OnRefreshPets();
			}
			string itemString = PetManager.GetItemString(down.items);
			DialogBoxUIViewModel.Instance.ShowAsConfirm("重置成功", string.Format("获得{0}", itemString), delegate
			{
			}, "确定", "button_yellow_1", null);
		}
	}

	private void OnPresPetUpStarRes(short state, PresPetUpStarRes down = null)
	{
		if (state != 0 || down == null)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (PetBasicUIViewModel.Instance != null)
		{
			PetBasicUIViewModel.Instance.SetUpgradePetUpFighting(down.fighting, down.afterFighting);
		}
	}

	private void UpdateAttr(short state, RoleAttrChangedNty down = null)
	{
		if (state != 0 || down == null)
		{
			return;
		}
		if (down.type == GameObjectType.ENUM.Pet && this.MaplistPet.ContainsKey(down.id))
		{
			PetInfo petInfo = this.MaplistPet.get_Item(down.id);
			if (petInfo != null)
			{
				this.UpdatePetAttrs(petInfo, down.attrs);
				this.RefreshTalents();
			}
		}
	}

	private void UpdatePetAttrs(PetInfo petInfo, List<RoleAttrChangedNty.AttrPair> attrs)
	{
		for (int i = 0; i < attrs.get_Count(); i++)
		{
			this.UpdatePetAttr(petInfo, (GameData.AttrType)attrs.get_Item(i).attrType, attrs.get_Item(i).attrValue);
		}
	}

	private void UpdatePetAttr(PetInfo petInfo, GameData.AttrType attrType, long attrValue)
	{
		switch (attrType)
		{
		case GameData.AttrType.PveAtk:
			petInfo.publicBaseInfo.PveAtk = (int)attrValue;
			return;
		case GameData.AttrType.PvpAtk:
			petInfo.publicBaseInfo.PvpAtk = (int)attrValue;
			return;
		case GameData.AttrType.HitRatio:
			petInfo.publicBaseInfo.HitRatio = (int)attrValue;
			return;
		case GameData.AttrType.DodgeRatio:
			petInfo.publicBaseInfo.DodgeRatio = (int)attrValue;
			return;
		case GameData.AttrType.CritRatio:
			petInfo.publicBaseInfo.CritRatio = (int)attrValue;
			return;
		case GameData.AttrType.DecritRatio:
			petInfo.publicBaseInfo.DecritRatio = (int)attrValue;
			return;
		case GameData.AttrType.CritHurtAddRatio:
			petInfo.publicBaseInfo.CritHurtAddRatio = (int)attrValue;
			return;
		case GameData.AttrType.ParryRatio:
			petInfo.publicBaseInfo.ParryRatio = (int)attrValue;
			return;
		case GameData.AttrType.DeparryRatio:
			petInfo.publicBaseInfo.DeparryRatio = (int)attrValue;
			return;
		case GameData.AttrType.ParryHurtDeRatio:
			petInfo.publicBaseInfo.ParryHurtDeRatio = (int)attrValue;
			return;
		case (GameData.AttrType)1314:
		case (GameData.AttrType)1321:
		case (GameData.AttrType)1322:
		case (GameData.AttrType)1327:
		case (GameData.AttrType)1328:
		case GameData.AttrType.OnlineTime:
		case GameData.AttrType.Vp:
		case GameData.AttrType.ExpAddRate:
			IL_A2:
			switch (attrType)
			{
			case GameData.AttrType.Fighting:
				petInfo.publicBaseInfo.simpleInfo.Fighting = attrValue;
				return;
			case GameData.AttrType.Diamond:
			case GameData.AttrType.Gold:
				IL_BE:
				switch (attrType)
				{
				case GameData.AttrType.WaterBuffAddProbAddAmend:
					petInfo.publicBaseInfo.WaterBuffAddProbAddAmend = (int)attrValue;
					return;
				case (GameData.AttrType)1222:
				case (GameData.AttrType)1223:
					IL_DA:
					switch (attrType)
					{
					case GameData.AttrType.ThunderBuffAddProbAddAmend:
						petInfo.publicBaseInfo.ThunderBuffAddProbAddAmend = (int)attrValue;
						return;
					case (GameData.AttrType)1232:
					case (GameData.AttrType)1233:
						IL_F6:
						if (attrType == GameData.AttrType.MoveSpeed)
						{
							petInfo.publicBaseInfo.simpleInfo.MoveSpeed = (int)attrValue;
							return;
						}
						if (attrType == GameData.AttrType.ActSpeed)
						{
							petInfo.publicBaseInfo.simpleInfo.AtkSpeed = (int)attrValue;
							return;
						}
						if (attrType == GameData.AttrType.Atk)
						{
							petInfo.publicBaseInfo.Atk = (int)attrValue;
							return;
						}
						if (attrType == GameData.AttrType.AtkMulAmend)
						{
							petInfo.publicBaseInfo.AtkMulAmend = (int)attrValue;
							return;
						}
						if (attrType == GameData.AttrType.ActPointRecoverSpeedAmend)
						{
							petInfo.publicBaseInfo.ActPointRecoverSpeedAmend = (int)attrValue;
							return;
						}
						if (attrType == GameData.AttrType.HpLmt)
						{
							petInfo.publicBaseInfo.HpLmt = attrValue;
							return;
						}
						if (attrType == GameData.AttrType.SuckBloodScale)
						{
							petInfo.publicBaseInfo.SuckBloodScale = (int)attrValue;
							return;
						}
						if (attrType == GameData.AttrType.Defence)
						{
							petInfo.publicBaseInfo.Defence = (int)attrValue;
							return;
						}
						if (attrType != GameData.AttrType.Lv)
						{
							return;
						}
						petInfo.publicBaseInfo.simpleInfo.Lv = (int)attrValue;
						return;
					case GameData.AttrType.ThunderBuffDurTimeAddAmend:
						petInfo.publicBaseInfo.ThunderBuffDurTimeAddAmend = (int)attrValue;
						return;
					}
					goto IL_F6;
				case GameData.AttrType.WaterBuffDurTimeAddAmend:
					petInfo.publicBaseInfo.WaterBuffDurTimeAddAmend = (int)attrValue;
					return;
				}
				goto IL_DA;
			case GameData.AttrType.VipLv:
				petInfo.publicBaseInfo.simpleInfo.VipLv = (int)attrValue;
				return;
			}
			goto IL_BE;
		case GameData.AttrType.HurtAddRatio:
			petInfo.publicBaseInfo.HurtAddRatio = (int)attrValue;
			return;
		case GameData.AttrType.HurtDeRatio:
			petInfo.publicBaseInfo.HurtDeRatio = (int)attrValue;
			return;
		case GameData.AttrType.PveHurtAddRatio:
			petInfo.publicBaseInfo.PveHurtAddRatio = (int)attrValue;
			return;
		case GameData.AttrType.PveHurtDeRatio:
			petInfo.publicBaseInfo.PveHurtDeRatio = (int)attrValue;
			return;
		case GameData.AttrType.PvpHurtAddRatio:
			petInfo.publicBaseInfo.PvpHurtAddRatio = (int)attrValue;
			return;
		case GameData.AttrType.PvpHurtDeRatio:
			petInfo.publicBaseInfo.PvpHurtDeRatio = (int)attrValue;
			return;
		case GameData.AttrType.DefMulAmend:
			petInfo.publicBaseInfo.DefMulAmend = (int)attrValue;
			return;
		case GameData.AttrType.HpLmtMulAmend:
			petInfo.publicBaseInfo.HpLmtMulAmend = (int)attrValue;
			return;
		case GameData.AttrType.PveAtkMulAmend:
			petInfo.publicBaseInfo.PveAtkMulAmend = (int)attrValue;
			return;
		case GameData.AttrType.PvpAtkMulAmend:
			petInfo.publicBaseInfo.PvpAtkMulAmend = (int)attrValue;
			return;
		case GameData.AttrType.VpLmt:
			petInfo.publicBaseInfo.VpLmt = (int)attrValue;
			return;
		case GameData.AttrType.VpLmtMulAmend:
			petInfo.publicBaseInfo.VpLmtMulAmend = (int)attrValue;
			return;
		case GameData.AttrType.VpResume:
			petInfo.publicBaseInfo.VpResume = (int)attrValue;
			return;
		case GameData.AttrType.VpAtk:
			petInfo.publicBaseInfo.VpAtk = (int)attrValue;
			return;
		case GameData.AttrType.VpAtkMulAmend:
			petInfo.publicBaseInfo.VpAtkMulAmend = (int)attrValue;
			return;
		case GameData.AttrType.IdleVpResume:
			petInfo.publicBaseInfo.IdleVpResume = (int)attrValue;
			return;
		case GameData.AttrType.HealIncreasePercent:
			petInfo.publicBaseInfo.HealIncreasePercent = (int)attrValue;
			return;
		case GameData.AttrType.CritAddValue:
			petInfo.publicBaseInfo.CritAddValue = (int)attrValue;
			return;
		case GameData.AttrType.HpRestore:
			petInfo.publicBaseInfo.HpRestore = (int)attrValue;
			return;
		}
		goto IL_A2;
	}

	public PetInfo GetPetInfoById(int petId)
	{
		if (petId <= 0)
		{
			return null;
		}
		using (Dictionary<long, PetInfo>.ValueCollection.Enumerator enumerator = this.MaplistPet.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PetInfo current = enumerator.get_Current();
				if (current.petId == petId)
				{
					return current;
				}
			}
		}
		if (this.m_experiencePets != null)
		{
			for (int i = 0; i < this.m_experiencePets.get_Count(); i++)
			{
				if (this.m_experiencePets.get_Item(i).petId == petId)
				{
					return this.m_experiencePets.get_Item(i);
				}
			}
		}
		return null;
	}

	public PetInfo GetPetInfo(long uid)
	{
		if (uid <= 0L)
		{
			return null;
		}
		if (this.MaplistPet.ContainsKey(uid))
		{
			return this.MaplistPet.get_Item(uid);
		}
		if (this.m_experiencePets != null)
		{
			for (int i = 0; i < this.m_experiencePets.get_Count(); i++)
			{
				if (this.m_experiencePets.get_Item(i).id == uid)
				{
					return this.m_experiencePets.get_Item(i);
				}
			}
		}
		return null;
	}

	public int GetPetLevel(int petId)
	{
		PetInfo petInfoById = this.GetPetInfoById(petId);
		if (petInfoById != null)
		{
			return petInfoById.lv;
		}
		return 0;
	}

	public PetFormation GetFormationData(int formationID)
	{
		if (this.formation != null)
		{
			return this.formation.Find((PetFormation a) => a.formationId == formationID);
		}
		return null;
	}

	public bool CanComposeRune(int _runeId)
	{
		FuWen fuWen = DataReader<FuWen>.Get(_runeId);
		if (fuWen == null)
		{
			return false;
		}
		bool result = true;
		for (int i = 0; i < fuWen.syntheticMaterialID.get_Count(); i++)
		{
			long num = BackpackManager.Instance.OnGetGoodCount(fuWen.syntheticMaterialID.get_Item(i));
			if (num < (long)fuWen.syntheticMaterialValue.get_Item(i))
			{
				result = false;
				break;
			}
		}
		return result;
	}

	public List<long> GetCurrentPetFormation()
	{
		List<long> list = new List<long>(0);
		if (this.formation == null)
		{
			return list;
		}
		PetFormation petFormation = this.formation.Find((PetFormation a) => a.formationId == PetManager.Instance.CurrentFormationID);
		if (petFormation == null)
		{
			return list;
		}
		if (petFormation.petFormationArr == null)
		{
			return list;
		}
		for (int i = 0; i < petFormation.petFormationArr.Int64Array.get_Count(); i++)
		{
			if (petFormation.petFormationArr.Int64Array == null)
			{
				return list;
			}
			list.Add(petFormation.petFormationArr.Int64Array.get_Item(i).value);
		}
		return list;
	}

	public bool IsInFormation(long perUID)
	{
		if (this.formation == null)
		{
			return false;
		}
		for (int i = 0; i < this.formation.get_Count(); i++)
		{
			PetFormation petFormation = this.formation.get_Item(i);
			if (petFormation != null && petFormation.petFormationArr != null)
			{
				for (int j = 0; j < petFormation.petFormationArr.Int64Array.get_Count(); j++)
				{
					if (perUID == petFormation.petFormationArr.Int64Array.get_Item(j).value)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public bool InBattleContainsPet(int petId)
	{
		PetInfo petInfoById = this.GetPetInfoById(petId);
		return petInfoById != null && EntityWorld.Instance.EntSelf.OwnedIDs.Contains(petInfoById.id);
	}

	public void LevelUp(int itemId, int level)
	{
		if (BackpackManager.Instance.OnGetGoodCount(itemId) > 0L)
		{
			this.SendUpgradeLevel(PetBasicUIViewModel.PetUID, itemId, level);
		}
		else
		{
			LinkNavigationManager.ItemNotEnoughToLink(itemId, true, null, true);
		}
	}

	private void SetSkillPoint(int skillPoint)
	{
		if (this.m_timeCDOfSkillPoint != null)
		{
			this.SetSkillPoint(skillPoint, this.m_timeCDOfSkillPoint.GetSeconds());
		}
		else
		{
			this.SetSkillPoint(skillPoint, 0);
		}
		this.SetSkillPointBtnBuy();
	}

	private void SetSkillPoint(int skillPoint, int residueRecoverTime)
	{
		if (skillPoint < this.MaxSkillPoint)
		{
			if (residueRecoverTime > 0)
			{
				string color = TextColorMgr.GetColor("{0}", "FF6800", string.Empty);
				this.format_skillPoint = string.Concat(new object[]
				{
					"可用技能点数: ",
					skillPoint,
					" （",
					color,
					"恢复1点）"
				});
				this.ResetTimeCountDown(residueRecoverTime);
			}
			else
			{
				if (this.m_timeCDOfSkillPoint != null)
				{
					this.m_timeCDOfSkillPoint.StopTimer();
				}
				this.SkillPointInfo = "可用技能点数: " + skillPoint;
			}
		}
		else
		{
			if (this.m_timeCDOfSkillPoint != null)
			{
				this.m_timeCDOfSkillPoint.StopTimer();
			}
			this.SkillPointInfo = "可用技能点数: " + skillPoint + "（点数已满）";
		}
	}

	public void ResetTimeCountDown(int seconds)
	{
		if (this.m_timeCDOfSkillPoint == null)
		{
			if (seconds > 0)
			{
				this.m_timeCDOfSkillPoint = new TimeCountDown(seconds, TimeFormat.MMSS, delegate
				{
					if (!string.IsNullOrEmpty(this.format_skillPoint))
					{
						this.SkillPointInfo = string.Format(this.format_skillPoint, this.m_timeCDOfSkillPoint.GetTime());
					}
				}, delegate
				{
					if (!string.IsNullOrEmpty(this.format_skillPoint))
					{
						this.SkillPointInfo = string.Format(this.format_skillPoint, this.m_timeCDOfSkillPoint.GetTime());
					}
				}, true);
			}
		}
		else if (seconds > 0)
		{
			this.m_timeCDOfSkillPoint.ResetSeconds(seconds);
		}
		else
		{
			this.m_timeCDOfSkillPoint.Dispose();
			this.m_timeCDOfSkillPoint = null;
		}
	}

	public void InitSkillPointMax()
	{
		this.MaxSkillPoint = DataReader<CChongWuSheZhi>.Get("skillPoint").num;
	}

	public void SetSkillPointBtnBuy()
	{
		if (PetBasicUIViewModel.Instance != null && PetBasicUIViewModel.Instance.get_gameObject().get_activeSelf())
		{
			PetBasicUIViewModel.Instance.SPBtnBuyEnable = (this.m_ExtraInfos.skillPoint == 0);
		}
	}

	public int GetPetUpgradeLevel(int petId)
	{
		PetInfo petInfoById = this.GetPetInfoById(petId);
		if (petInfoById != null)
		{
			return petInfoById.star;
		}
		return 1;
	}

	public static string GetPetName(Pet dataPet, bool color)
	{
		string text = string.Empty;
		PetConversion petConversion = DataReader<PetConversion>.Get(PetManager.Instance.GetPetUpgradeLevel((int)dataPet.id));
		if (petConversion != null)
		{
			text = GameDataUtils.GetChineseContent(dataPet.name, false) + petConversion.nameSuffix;
			if (color)
			{
				return TextColorMgr.GetColorByQuality(text, petConversion.nameQY);
			}
		}
		return text;
	}

	public static string GetPetName(Pet dataPet, int upgrade)
	{
		string text = string.Empty;
		PetConversion petConversion = DataReader<PetConversion>.Get(upgrade);
		if (petConversion != null)
		{
			text = GameDataUtils.GetChineseContent(dataPet.name, false) + petConversion.nameSuffix;
			return TextColorMgr.GetColorByQuality(text, petConversion.nameQY);
		}
		return text;
	}

	public static string GetPetName(int petId, int upgrade)
	{
		Pet dataPet = DataReader<Pet>.Get(petId);
		return PetManager.GetPetName(dataPet, upgrade);
	}

	public static SpriteRenderer GetPetFrameCircle(int upgrade_level)
	{
		PetConversion petConversion = DataReader<PetConversion>.Get(upgrade_level);
		if (petConversion != null)
		{
			return ResourceManager.GetIconSprite("chongwuqufen_" + petConversion.frameQY);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static SpriteRenderer GetPetFrameSquare(int upgrade_level)
	{
		PetConversion petConversion = DataReader<PetConversion>.Get(upgrade_level);
		if (petConversion != null)
		{
			PinZhiYanSe pinZhiYanSe = DataReader<PinZhiYanSe>.Get(petConversion.frameQY);
			if (pinZhiYanSe != null)
			{
				return ResourceManager.GetIconSprite(pinZhiYanSe.name);
			}
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static SpriteRenderer GetPetFrame01(int upgrade_level)
	{
		PetConversion petConversion = DataReader<PetConversion>.Get(upgrade_level);
		if (petConversion != null)
		{
			return GameDataUtils.GetItemFrameByColor(petConversion.frameQY);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static SpriteRenderer GetPetFrame02(int upgrade_level)
	{
		PetConversion petConversion = DataReader<PetConversion>.Get(upgrade_level);
		if (petConversion != null)
		{
			return GameDataUtils.GetIcon(petConversion.frameID);
		}
		return ResourceManagerBase.GetNullSprite();
	}

	public static SpriteRenderer GetPetQualityIcon(int star)
	{
		star = Mathf.Max(1, star);
		return ResourceManager.GetIconSprite(PetManager.GetQualityIconName(star));
	}

	public static string GetQualityIconName(int star)
	{
		string result = string.Empty;
		if (star == 1)
		{
			result = "pet_icon_D";
		}
		else if (star == 2)
		{
			result = "pet_icon_C";
		}
		else if (star == 3)
		{
			result = "pet_icon_B";
		}
		else if (star == 4)
		{
			result = "pet_icon_A";
		}
		else if (star == 5)
		{
			result = "pet_icon_S";
		}
		return result;
	}

	private void SuccessPanelOfPetLevelUp(PetInfo petOld, PetInfo petNew)
	{
	}

	private void SuccessPanelOfPetStarUp(PetInfo petOld, PetInfo petNew)
	{
		if (petOld == null || petNew == null)
		{
			return;
		}
		Pet pet = DataReader<Pet>.Get(petNew.petId);
		if (pet == null)
		{
			return;
		}
		if (UIManagerControl.Instance.IsOpen("PetBasicUI"))
		{
			XTaskManager.instance.StartTask(this.AnimSuccessOfUpgrade(petNew), null);
		}
		else
		{
			ModelDisplayManager.Instance.ShowModel(PetManagerBase.GetPlayerPetModel(petNew.petId, petNew.star), false, ModelDisplayManager.OFFSET_TO_PETUI, delegate(int model_uid)
			{
				PetBasicUIViewModel.PetModelUID = model_uid;
				XTaskManager.instance.StartTask(this.AnimSuccessOfUpgrade(petNew), null);
			});
		}
		this.CreateChangeAttrsOfNew(petOld, petNew);
		UpgradeUIViewModel.Open();
		UpgradeUIView.Instance.SetBackground(pet.petType);
		UpgradeUIViewModel.Instance.SetUpgradeUnits(this.m_UnitsOfNew);
		UpgradeUIViewModel.Instance.StarIcon = PetManager.GetPetQualityIcon(petNew.star);
		UpgradeUIViewModel.Instance.PetName = PetManager.GetPetName(pet, true);
		UpgradeUIViewModel.Instance.FightingNum1 = petOld.publicBaseInfo.simpleInfo.Fighting.ToString();
		UpgradeUIViewModel.Instance.FightingNum2 = petNew.publicBaseInfo.simpleInfo.Fighting.ToString();
		ChongWuTianFu activeNatural = PetManager.GetActiveNatural(pet, petNew.star);
		if (activeNatural != null)
		{
			UpgradeUIViewModel.Instance.ShowNaturalRegion = true;
			UpgradeUIViewModel.Instance.NaturalIcon = ResourceManager.GetIconSprite(activeNatural.picture);
			UpgradeUIViewModel.Instance.NaturalName = GameDataUtils.GetChineseContent(activeNatural.name, false);
			UpgradeUIViewModel.Instance.NaturalDesc = GameDataUtils.GetChineseContent(activeNatural.describe, false);
		}
		else
		{
			UpgradeUIViewModel.Instance.ShowNaturalRegion = false;
		}
	}

	private void CreateChangeAttrsOfUp(PetInfo petOld, PetInfo petNew)
	{
		Pet dataPet = DataReader<Pet>.Get(petOld.petId);
		Pet dataPet2 = DataReader<Pet>.Get(petNew.petId);
		this.m_UnitsOfUp.Clear();
		if (this.GetATK(dataPet2, petNew, false, false) - this.GetATK(dataPet, petOld, false, false) > 0)
		{
			UpgradeUnitData upgradeUnitData = new UpgradeUnitData();
			string attrName = AttrUtility.GetAttrName(GameData.AttrType.Atk);
			upgradeUnitData.BeginStr = attrName;
			upgradeUnitData.BeginStr1 = this.GetATK(dataPet, petOld, false, false).ToString();
			upgradeUnitData.EndStr = "+" + (this.GetATK(dataPet2, petNew, false, false) - this.GetATK(dataPet, petOld, false, false));
			this.m_UnitsOfUp.Add(upgradeUnitData);
		}
		if (this.GetDefence(dataPet2, petNew, false, false) - this.GetDefence(dataPet, petOld, false, false) > 0)
		{
			UpgradeUnitData upgradeUnitData = new UpgradeUnitData();
			string attrName = AttrUtility.GetAttrName(GameData.AttrType.Defence);
			upgradeUnitData.BeginStr = attrName;
			upgradeUnitData.BeginStr1 = this.GetDefence(dataPet, petOld, false, false).ToString();
			upgradeUnitData.EndStr = "+" + (this.GetDefence(dataPet2, petNew, false, false) - this.GetDefence(dataPet, petOld, false, false));
			this.m_UnitsOfUp.Add(upgradeUnitData);
		}
		if (this.GetHP(dataPet2, petNew, false, false) - this.GetHP(dataPet, petOld, false, false) > 0)
		{
			UpgradeUnitData upgradeUnitData = new UpgradeUnitData();
			string attrName = AttrUtility.GetAttrName(GameData.AttrType.Hp);
			upgradeUnitData.BeginStr = attrName;
			upgradeUnitData.BeginStr1 = this.GetHP(dataPet, petOld, false, false).ToString();
			upgradeUnitData.EndStr = "+" + (this.GetHP(dataPet2, petNew, false, false) - this.GetHP(dataPet, petOld, false, false));
			this.m_UnitsOfUp.Add(upgradeUnitData);
		}
		if (this.GetATKMonsterAdd(dataPet2, petNew) - this.GetATKMonsterAdd(dataPet, petOld) > 0)
		{
			UpgradeUnitData upgradeUnitData = new UpgradeUnitData();
			string attrName = AttrUtility.GetAttrName(GameData.AttrType.PveAtk);
			upgradeUnitData.BeginStr = attrName;
			upgradeUnitData.BeginStr1 = this.GetATKMonsterAdd(dataPet, petOld).ToString();
			upgradeUnitData.EndStr = "+" + (this.GetATKMonsterAdd(dataPet2, petNew) - this.GetATKMonsterAdd(dataPet, petOld));
			this.m_UnitsOfUp.Add(upgradeUnitData);
		}
		if (this.GetATKRoleAdd(dataPet2, petNew) - this.GetATKRoleAdd(dataPet, petOld) > 0)
		{
			UpgradeUnitData upgradeUnitData = new UpgradeUnitData();
			string attrName = AttrUtility.GetAttrName(GameData.AttrType.PvpAtk);
			upgradeUnitData.BeginStr = attrName;
			upgradeUnitData.BeginStr1 = this.GetATKRoleAdd(dataPet, petOld).ToString();
			upgradeUnitData.EndStr = "+" + (this.GetATKRoleAdd(dataPet2, petNew) - this.GetATKRoleAdd(dataPet, petOld));
			this.m_UnitsOfUp.Add(upgradeUnitData);
		}
	}

	private void CreateChangeAttrsOfNew(PetInfo petOld, PetInfo petNew)
	{
		Pet dataPet = DataReader<Pet>.Get(petOld.petId);
		Pet dataPet2 = DataReader<Pet>.Get(petNew.petId);
		this.m_UnitsOfNew.Clear();
		UpgradeUnitData upgradeUnitData = new UpgradeUnitData();
		string attrName = AttrUtility.GetAttrName(GameData.AttrType.Atk);
		upgradeUnitData.BeginStr = attrName;
		upgradeUnitData.BeginStr1 = this.GetATK(dataPet, petOld, false, false).ToString();
		upgradeUnitData.EndStr = this.GetATK(dataPet2, petNew, false, false).ToString();
		this.m_UnitsOfNew.Add(upgradeUnitData);
		upgradeUnitData = new UpgradeUnitData();
		attrName = AttrUtility.GetAttrName(GameData.AttrType.Defence);
		upgradeUnitData.BeginStr = attrName;
		upgradeUnitData.BeginStr1 = this.GetDefence(dataPet, petOld, false, false).ToString();
		upgradeUnitData.EndStr = this.GetDefence(dataPet2, petNew, false, false).ToString();
		this.m_UnitsOfNew.Add(upgradeUnitData);
		upgradeUnitData = new UpgradeUnitData();
		attrName = AttrUtility.GetAttrName(GameData.AttrType.Hp);
		upgradeUnitData.BeginStr = attrName;
		upgradeUnitData.BeginStr1 = this.GetHP(dataPet, petOld, false, false).ToString();
		upgradeUnitData.EndStr = this.GetHP(dataPet2, petNew, false, false).ToString();
		this.m_UnitsOfNew.Add(upgradeUnitData);
	}

	private void RefreshTalents()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		using (Dictionary<long, PetInfo>.ValueCollection.Enumerator enumerator = this.MaplistPet.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PetInfo current = enumerator.get_Current();
				List<int> talentIds = current.talentIds;
				for (int i = 0; i < talentIds.get_Count(); i++)
				{
					ChongWuTianFu chongWuTianFu = DataReader<ChongWuTianFu>.Get(talentIds.get_Item(i));
					if (chongWuTianFu != null)
					{
						if (chongWuTianFu.effect == 6)
						{
							if (chongWuTianFu.parameter.get_Count() > 0)
							{
								num += chongWuTianFu.parameter.get_Item(0);
							}
						}
						else if (chongWuTianFu.effect == 7)
						{
							if (chongWuTianFu.parameter.get_Count() > 0)
							{
								num2 += chongWuTianFu.parameter.get_Item(0);
							}
						}
						else if (chongWuTianFu.effect == 8 && chongWuTianFu.parameter.get_Count() > 0)
						{
							num3 += chongWuTianFu.parameter.get_Item(0);
						}
					}
				}
			}
		}
		if (EntityWorld.Instance.EntSelf != null)
		{
			EntityWorld.Instance.EntSelf.TotalFuseTimePlus = (float)num2;
			EntityWorld.Instance.EntSelf.TotalBeginActPoint = num3;
		}
	}

	public static ChongWuTianFu GetActiveNatural(Pet dataPet, int star)
	{
		int num = -1;
		for (int i = 0; i < dataPet.talentStart.get_Count(); i++)
		{
			if (dataPet.talentStart.get_Item(i) == star)
			{
				num = i;
				break;
			}
		}
		if (num >= 0)
		{
			int key = dataPet.talent.get_Item(num);
			return DataReader<ChongWuTianFu>.Get(key);
		}
		return null;
	}

	private List<int> GetPetModelIds()
	{
		this.modelIds.Clear();
		using (Dictionary<long, PetInfo>.ValueCollection.Enumerator enumerator = PetManager.Instance.MaplistPet.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PetInfo current = enumerator.get_Current();
				this.modelIds.Add(current.modelId);
			}
		}
		List<Pet> levelPassPets = this.GetLevelPassPets();
		for (int i = 0; i < levelPassPets.get_Count(); i++)
		{
			int selfPetModel = base.GetSelfPetModel(levelPassPets.get_Item(i));
			if (!this.modelIds.Contains(selfPetModel))
			{
				this.modelIds.Add(selfPetModel);
			}
		}
		return this.modelIds;
	}

	public List<Pet> GetLevelPassPets()
	{
		List<Pet> list = new List<Pet>();
		if (EntityWorld.Instance.EntSelf == null)
		{
			return list;
		}
		List<Pet> dataList = DataReader<Pet>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (EntityWorld.Instance.EntSelf.Lv >= dataList.get_Item(i).readNeedRoleLv)
			{
				list.Add(dataList.get_Item(i));
			}
		}
		return list;
	}

	public int GetATK(Pet dataPet, PetInfo petInfo, bool nextlevel = false, bool nextstar = false)
	{
		return this.GetAttr(201, dataPet, petInfo, nextlevel, nextstar);
	}

	public int GetDefence(Pet dataPet, PetInfo petInfo, bool nextlevel = false, bool nextstar = false)
	{
		return this.GetAttr(601, dataPet, petInfo, nextlevel, nextstar);
	}

	public int GetHP(Pet dataPet, PetInfo petInfo, bool nextlevel = false, bool nextstar = false)
	{
		return this.GetAttr(301, dataPet, petInfo, nextlevel, nextstar);
	}

	public int GetATKMonsterAdd(Pet dataPet, PetInfo petInfo)
	{
		return this.GetAttr(1304, dataPet, petInfo, false, false);
	}

	public int GetATKRoleAdd(Pet dataPet, PetInfo petInfo)
	{
		return this.GetAttr(1305, dataPet, petInfo, false, false);
	}

	private int GetAttr(int attrId, Pet dataPet, PetInfo petInfo, bool nextlevel = false, bool nextstar = false)
	{
		int num = 1;
		int num2 = 1;
		if (petInfo != null)
		{
			num = petInfo.lv;
			num2 = petInfo.star;
		}
		if (nextlevel)
		{
			num++;
		}
		if (nextstar)
		{
			num2++;
		}
		int num3 = Mathf.Clamp(num2 - 1, 0, 8);
		int key = dataPet.attributeAddToPlayerID.get_Item(num3);
		int key2 = dataPet.attributeAddToPlayerGrowID.get_Item(num3);
		Attrs dataAttrs = DataReader<Attrs>.Get(key);
		Attrs dataAttrs2 = DataReader<Attrs>.Get(key2);
		return (int)(this.GetAttrValue(dataAttrs, attrId) + this.GetAttrValue(dataAttrs2, attrId) * (float)num);
	}

	private float GetAttrValue(Attrs dataAttrs, int attrId)
	{
		for (int i = 0; i < dataAttrs.attrs.get_Count(); i++)
		{
			if (dataAttrs.attrs.get_Item(i) == attrId)
			{
				return (float)dataAttrs.values.get_Item(i);
			}
		}
		return 0f;
	}

	public bool CheckBadge()
	{
		if (EntityWorld.Instance.EntSelf == null || EntityWorld.Instance.EntSelf.IsInBattle)
		{
			return false;
		}
		if (PetManager.IsUpgradeTipOn)
		{
			if (this.CheckAllCanUpgrade())
			{
				StrongerManager.Instance.UpdatePromoteWayDic(PromoteWayType.PetUpgrade, true);
				return true;
			}
			StrongerManager.Instance.UpdatePromoteWayDic(PromoteWayType.PetUpgrade, false);
		}
		if (PetManager.IsLevelUpTipOn)
		{
			StrongerManager.Instance.UpdatePromoteWayDic(PromoteWayType.PetLevelUp, this.CheckFormationCanLevelUp());
			if (this.CheckAllCanLevelUp())
			{
				return true;
			}
		}
		if (PetManager.IsSkillUpTipOn && this.CheckAllCanSkillUp())
		{
			return true;
		}
		if (PetManager.IsActivateTipOn && this.CheckAllCanActivate())
		{
			return true;
		}
		if (PetManager.IsFormationTipOn)
		{
			if (this.CheckAllCanFormation())
			{
				StrongerManager.Instance.UpdatePromoteWayDic(PromoteWayType.PetFormation, true);
				return true;
			}
			StrongerManager.Instance.UpdatePromoteWayDic(PromoteWayType.PetFormation, false);
		}
		return false;
	}

	public bool CheckPetBadge(PetInfo petInfo)
	{
		return petInfo != null && ((PetManager.IsUpgradeTipOn && this.CheckCanUpgrade(petInfo)) || (PetManager.IsLevelUpTipOn && this.CheckCanLevelUp(petInfo)) || (PetManager.IsSkillUpTipOn && this.CheckCanSkillUp(petInfo)));
	}

	private bool CheckAllCanUpgrade()
	{
		using (Dictionary<long, PetInfo>.ValueCollection.Enumerator enumerator = this.MaplistPet.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PetInfo current = enumerator.get_Current();
				if (this.CheckCanUpgrade(current))
				{
					EventDispatcher.Broadcast<int>("GuideManager.PetUpStarOn", current.petId);
					return true;
				}
			}
		}
		return false;
	}

	public bool CheckCanUpgrade(PetInfo petInfo)
	{
		if (!PetManager.IsUpgradeTipOn)
		{
			return false;
		}
		Pet pet = DataReader<Pet>.Get(petInfo.petId);
		if (pet != null && pet.needFragment.get_Count() > 0)
		{
			int num = petInfo.star + 1;
			if (num <= pet.needFragment.get_Count() && BackpackManager.Instance.OnGetGoodCount(pet.fragmentId) >= (long)pet.needFragment.get_Item(num - 1))
			{
				return true;
			}
		}
		return false;
	}

	private bool CheckFormationCanLevelUp()
	{
		List<PetFormation> list = PetManager.Instance.Formation;
		if (list == null || list.get_Count() <= 0)
		{
			return false;
		}
		int num = list.FindIndex((PetFormation a) => a.formationId == this.CurrentFormationID);
		if (num >= 0)
		{
			PetFormation petFormation = list.get_Item(num);
			if (petFormation.petFormationArr == null)
			{
				return false;
			}
			for (int i = 0; i < petFormation.petFormationArr.Int64Array.get_Count(); i++)
			{
				Int64IndexValue int64IndexValue = petFormation.petFormationArr.Int64Array.get_Item(i);
				PetInfo petInfo = PetManager.Instance.GetPetInfo(int64IndexValue.value);
				if (petInfo != null && this.CheckCanLevelUp(petInfo))
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool CheckAllCanLevelUp()
	{
		using (Dictionary<long, PetInfo>.ValueCollection.Enumerator enumerator = this.MaplistPet.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PetInfo current = enumerator.get_Current();
				if (this.CheckCanLevelUp(current))
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool CheckCanLevelUp(PetInfo petInfo)
	{
		return petInfo != null && PetManager.IsLevelUpTipOn && (!this.IsPetMaxLevel(petInfo.lv) && petInfo.lv < EntityWorld.Instance.EntSelf.Lv && this.IsExpItemReachTip());
	}

	public bool IsPetMaxLevel(int petLevel)
	{
		List<GenericAttribute> dataList = DataReader<GenericAttribute>.DataList;
		return dataList != null && (dataList.get_Count() <= 0 || petLevel >= dataList.get_Item(dataList.get_Count() - 1).lv);
	}

	public bool IsExpItemReachTip()
	{
		long num = 0L;
		for (int i = 0; i < this.ExpItemIds.get_Count(); i++)
		{
			num += BackpackManager.Instance.OnGetGoodCount(this.ExpItemIds.get_Item(i));
		}
		return num > 20L;
	}

	private bool CheckAllCanSkillUp()
	{
		return PetEvoGlobal.IsHavePetCanSkillLvUp();
	}

	public bool CheckCanSkillUp(int petId)
	{
		PetInfo petInfoById = this.GetPetInfoById(petId);
		return this.CheckCanSkillUp(petInfoById);
	}

	private bool CheckCanSkillUp(PetInfo petInfo)
	{
		return petInfo != null && PetManager.IsSkillUpTipOn && PetEvoGlobal.IsSkillsCanLvUp(petInfo.petId);
	}

	public bool CheckAllCanActivate()
	{
		List<Pet> levelPassPets = this.GetLevelPassPets();
		for (int i = 0; i < levelPassPets.get_Count(); i++)
		{
			Pet pet = levelPassPets.get_Item(i);
			if (this.GetPetInfoById((int)pet.id) == null && EntityWorld.Instance.EntSelf.Lv >= pet.readNeedRoleLv && BackpackManager.Instance.OnGetGoodCount(pet.fragmentId) >= (long)pet.needFragment.get_Item(0))
			{
				EventDispatcher.Broadcast<int>("GuideManager.PetActivateOn", (int)pet.id);
				return true;
			}
		}
		return false;
	}

	public bool CheckAllCanFormation()
	{
		PetFormation petFormation = PetManager.Instance.Formation.Find((PetFormation a) => a.formationId == PetManager.Instance.CurrentFormationID);
		if (petFormation == null || petFormation.petFormationArr == null || petFormation.petFormationArr.Int64Array == null)
		{
			if (PetManager.Instance.MaplistPet.get_Values().get_Count() > 0)
			{
				return true;
			}
		}
		else if (petFormation.petFormationArr.Int64Array.get_Count() < 3 && petFormation.petFormationArr.Int64Array.get_Count() < PetManager.Instance.MaplistPet.get_Values().get_Count())
		{
			return true;
		}
		return false;
	}

	[DebuggerHidden]
	private IEnumerator AnimSuccessOfCompose(int templateId)
	{
		PetManager.<AnimSuccessOfCompose>c__Iterator32 <AnimSuccessOfCompose>c__Iterator = new PetManager.<AnimSuccessOfCompose>c__Iterator32();
		<AnimSuccessOfCompose>c__Iterator.templateId = templateId;
		<AnimSuccessOfCompose>c__Iterator.<$>templateId = templateId;
		<AnimSuccessOfCompose>c__Iterator.<>f__this = this;
		return <AnimSuccessOfCompose>c__Iterator;
	}

	[DebuggerHidden]
	public IEnumerator AnimSuccessOfObtain(int petId, int uid, int templateId)
	{
		PetManager.<AnimSuccessOfObtain>c__Iterator33 <AnimSuccessOfObtain>c__Iterator = new PetManager.<AnimSuccessOfObtain>c__Iterator33();
		<AnimSuccessOfObtain>c__Iterator.uid = uid;
		<AnimSuccessOfObtain>c__Iterator.templateId = templateId;
		<AnimSuccessOfObtain>c__Iterator.petId = petId;
		<AnimSuccessOfObtain>c__Iterator.<$>uid = uid;
		<AnimSuccessOfObtain>c__Iterator.<$>templateId = templateId;
		<AnimSuccessOfObtain>c__Iterator.<$>petId = petId;
		<AnimSuccessOfObtain>c__Iterator.<>f__this = this;
		return <AnimSuccessOfObtain>c__Iterator;
	}

	public int GetPetSkill(int petId, int talentIndex)
	{
		Pet pet = DataReader<Pet>.Get(petId);
		if (pet != null && talentIndex < pet.talent.get_Count())
		{
			int key = pet.talent.get_Item(talentIndex);
			ChongWuTianFu chongWuTianFu = DataReader<ChongWuTianFu>.Get(key);
			if (chongWuTianFu != null && chongWuTianFu.parameter.get_Count() > 0)
			{
				return chongWuTianFu.parameter.get_Item(0);
			}
		}
		return 0;
	}

	[DebuggerHidden]
	private IEnumerator AnimSuccessOfUpgrade(PetInfo petNew)
	{
		PetManager.<AnimSuccessOfUpgrade>c__Iterator34 <AnimSuccessOfUpgrade>c__Iterator = new PetManager.<AnimSuccessOfUpgrade>c__Iterator34();
		<AnimSuccessOfUpgrade>c__Iterator.petNew = petNew;
		<AnimSuccessOfUpgrade>c__Iterator.<$>petNew = petNew;
		<AnimSuccessOfUpgrade>c__Iterator.<>f__this = this;
		return <AnimSuccessOfUpgrade>c__Iterator;
	}

	public void AnimSuccessOfLevelUp()
	{
		if (this.fx_SuccessOfLevelUp > 0)
		{
			return;
		}
		this.fx_SuccessOfLevelUp = FXManager.Instance.PlayFXOfDisplay(917, CamerasMgr.Camera2RTCommon.get_transform(), ModelDisplayManager.OFFSET_TO_PETUI_FX, Quaternion.get_identity(), 1f, 1f, 0, false, delegate
		{
			FXManager.Instance.DeleteFX(this.fx_SuccessOfLevelUp);
			this.fx_SuccessOfLevelUp = 0;
		}, null);
	}

	public List<PetInfo> GetPetBattleInfo(int mapType)
	{
		this.resultOfPet.Clear();
		if (mapType == 111)
		{
			return this.GetExperiencePets();
		}
		PetFormation formationData = this.GetFormationData(this.CurrentFormationID);
		if (formationData == null)
		{
			return this.resultOfPet;
		}
		if (formationData.petFormationArr == null)
		{
			return this.resultOfPet;
		}
		if (formationData.petFormationArr.Int64Array == null)
		{
			return this.resultOfPet;
		}
		using (List<Int64IndexValue>.Enumerator enumerator = formationData.petFormationArr.Int64Array.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Int64IndexValue current = enumerator.get_Current();
				if (this.MaplistPet.ContainsKey(current.value))
				{
					this.resultOfPet.Add(this.MaplistPet.get_Item(current.value));
				}
			}
		}
		return this.resultOfPet;
	}

	private List<PetInfo> GetExperiencePets()
	{
		if (this.m_experiencePets != null)
		{
			return this.m_experiencePets;
		}
		this.m_experiencePets = new List<PetInfo>();
		string value = DataReader<GlobalParams>.Get("beginBattlePet").value;
		string value2 = DataReader<GlobalParams>.Get("beginBattlePetAttr").value;
		int num = int.Parse(GameDataUtils.SplitString4Dot0(value));
		int num2 = int.Parse(GameDataUtils.SplitString4Dot0(value2));
		Pet pet = DataReader<Pet>.Get(num);
		if (pet == null)
		{
			Debug.LogError("没有找到宠物, id = " + num);
			return this.m_experiencePets;
		}
		Attrs attrs = DataReader<Attrs>.Get(num2);
		if (attrs == null)
		{
			Debug.LogError("没有找到属性, id = " + num2);
			return this.m_experiencePets;
		}
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(base.GetSelfPetModel(pet));
		if (avatarModel == null)
		{
			return this.m_experiencePets;
		}
		PetInfo petInfo = new PetInfo();
		petInfo.publicBaseInfo = new PublicBaseInfo();
		petInfo.publicBaseInfo.simpleInfo = new SimpleBaseInfo();
		petInfo.id = 9223372036854775807L;
		petInfo.petId = num;
		petInfo.modelId = base.GetSelfPetModel(pet);
		petInfo.name = GameDataUtils.GetChineseContent(pet.name, false);
		petInfo.lv = 1;
		petInfo.exp = 1L;
		petInfo.star = 1;
		petInfo.quality = 1;
		for (int i = 0; i < pet.skill.get_Count(); i++)
		{
			string value3 = pet.skill.get_Item(i).value;
			if (!string.IsNullOrEmpty(value3))
			{
			}
		}
		List<int> fuseSkill = this.GetFuseSkill(pet);
		for (int j = 0; j < fuseSkill.get_Count(); j++)
		{
			petInfo.fuseSkillIds.Add(fuseSkill.get_Item(j));
		}
		for (int k = 0; k < pet.talent.get_Count(); k++)
		{
			petInfo.talentIds.Add(pet.talent.get_Item(k));
			PetTalent petTalent = new PetTalent();
			petTalent.talentId = pet.talent.get_Item(k);
			petTalent.talentLv = 5;
			petInfo.petTalents.Add(petTalent);
		}
		petInfo.publicBaseInfo.simpleInfo.MoveSpeed = avatarModel.speed * 100;
		petInfo.publicBaseInfo.HpLmtMulAmend = 1;
		petInfo.publicBaseInfo.AtkMulAmend = 1;
		List<int> attrs2 = attrs.attrs;
		List<int> values = attrs.values;
		int num3 = 0;
		while (num3 < attrs2.get_Count() && num3 < values.get_Count())
		{
			this.UpdatePetAttr(petInfo, (GameData.AttrType)attrs2.get_Item(num3), (long)values.get_Item(num3));
			num3++;
		}
		this.m_experiencePets.Add(petInfo);
		return this.m_experiencePets;
	}

	public long GetAllHadPetFighting()
	{
		long num = 0L;
		using (Dictionary<long, PetInfo>.ValueCollection.Enumerator enumerator = this.MaplistPet.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PetInfo current = enumerator.get_Current();
				num += current.publicBaseInfo.simpleInfo.Fighting;
			}
		}
		return num;
	}

	public void Jump2Formation(bool fromInstance = true)
	{
		this.IsFormationFromInstance = fromInstance;
		this.IsFromLink = true;
		LinkNavigationManager.OpenPetUI(delegate
		{
			PetBasicUIViewModel.Instance.SubPanelFormation = true;
		});
	}

	public void Jump2PetLevelUI()
	{
		this.IsFromLink = true;
		LinkNavigationManager.OpenPetUI(delegate
		{
			if (PetChooseUIView.Instance != null)
			{
				PetChooseUIView.Instance.Show(false);
			}
			PetChooseUIViewModel.Instance.SetSelectedByID(this.GetDefaultPetId(), true);
			PetBasicUIViewModel.Instance.SubPanelPetInfo = true;
			PetBasicUIViewModel.Instance.FnBtnLevel = true;
		});
	}

	public void Jump2PetUpgradeUI()
	{
		this.IsFromLink = true;
		LinkNavigationManager.OpenPetUI(delegate
		{
			if (PetChooseUIView.Instance != null)
			{
				PetChooseUIView.Instance.Show(false);
			}
			PetChooseUIViewModel.Instance.SetSelectedByID(this.GetUpgradeDefaultPetID(), true);
			PetBasicUIViewModel.Instance.SubPanelPetInfo = true;
			PetBasicUIViewModel.Instance.FnBtnUpgrade = true;
		});
	}

	public void Jump2PetSkillUI()
	{
		this.IsFromLink = true;
		LinkNavigationManager.OpenPetUI(delegate
		{
			if (PetChooseUIView.Instance != null)
			{
				PetChooseUIView.Instance.Show(false);
			}
			PetChooseUIViewModel.Instance.SetSelectedByID(this.GetDefaultPetId(), true);
			PetBasicUIViewModel.Instance.SubPanelPetInfo = true;
			PetBasicUIViewModel.Instance.FnBtnSkillEvo = true;
		});
	}

	public int PlayScreenFXOfBattle(int template)
	{
		int hide_nodes_2D = 14;
		UIManagerControl.Instance.FakeHideAllUI(true, hide_nodes_2D);
		int num = FXManager.Instance.ScreenBattlePlay();
		this.battleUIds.Add(num);
		return num;
	}

	public void DeleteScreenFXOfBattles()
	{
		for (int i = 0; i < this.battleUIds.get_Count(); i++)
		{
			FXManager.Instance.ScreenBattleDelete();
		}
		this.DeleteScreenFXOfCommonSetting();
		this.battleUIds.Clear();
	}

	public void DeleteScreenFXOfBattle(int uid)
	{
		if (this.battleUIds.Contains(uid))
		{
			this.battleUIds.Remove(uid);
			this.DeleteScreenFXOfCommonSetting();
			FXManager.Instance.ScreenBattleDelete();
		}
	}

	private void DeleteScreenFXOfCommonSetting()
	{
		UIManagerControl.Instance.FakeHideAllUI(false, 7);
		Utils.SetCameraCullingMask(CamerasMgr.CameraMain, 1);
		CamerasMgr.CameraMain.set_clearFlags(1);
	}

	public void SetMainCameraCullingMaskWithActors()
	{
		int mask = LayerSystem.GetMask(this.actors_layers);
		CamerasMgr.CameraMain.set_cullingMask(mask);
	}

	public int GetExistTime(PetInfo petInfo)
	{
		if (petInfo == null)
		{
			return 0;
		}
		Pet pet = DataReader<Pet>.Get(petInfo.petId);
		if (pet == null)
		{
			return 0;
		}
		return (petInfo.star <= 0 || petInfo.star > pet.keepTime.get_Count()) ? 0 : (pet.keepTime.get_Item(petInfo.star - 1) + SkillDataManager.Instance.GetPetExistTimeChange(pet));
	}

	public BattleSkillInfo GetSummonSkillInfo(PetInfo petInfo, int index)
	{
		if (petInfo == null)
		{
			return null;
		}
		Pet pet = DataReader<Pet>.Get(petInfo.petId);
		if (pet == null)
		{
			return null;
		}
		return new BattleSkillInfo
		{
			skillId = pet.summonSkill,
			skillIdx = index + 21,
			skillLv = 1
		};
	}

	public bool GetSummonMonopolize(PetInfo petInfo)
	{
		if (petInfo == null)
		{
			return true;
		}
		Pet pet = DataReader<Pet>.Get(petInfo.petId);
		return pet == null || pet.only != 0;
	}

	public float GetSummonPoint(PetInfo petInfo)
	{
		if (petInfo == null)
		{
			return 0f;
		}
		Pet pet = DataReader<Pet>.Get(petInfo.petId);
		if (pet == null)
		{
			return 0f;
		}
		return (petInfo.star <= 0) ? 0f : pet.summonEnergy;
	}

	public float GetResummonPoint(PetInfo petInfo)
	{
		if (petInfo == null)
		{
			return 0f;
		}
		Pet pet = DataReader<Pet>.Get(petInfo.petId);
		if (pet == null)
		{
			return 0f;
		}
		if (petInfo.talentIds.Contains(80002))
		{
			return (petInfo.star <= 0) ? -1f : pet.summonEnergy;
		}
		return 0f;
	}

	public int GetSupportSkill(int petID)
	{
		Pet pet = DataReader<Pet>.Get(petID);
		if (pet == null)
		{
			return 0;
		}
		for (int i = 0; i < pet.supportSkill.get_Count(); i++)
		{
			if (pet.supportSkill.get_Item(i).key == EntityWorld.Instance.EntSelf.TypeID)
			{
				return pet.supportSkill.get_Item(i).value;
			}
		}
		return 0;
	}

	public int GetManualSkillBound(PetInfo petInfo)
	{
		if (petInfo == null)
		{
			return 2147483647;
		}
		Pet pet = DataReader<Pet>.Get(petInfo.petId);
		if (pet == null)
		{
			return 2147483647;
		}
		return pet.manualSkillAP;
	}

	public BattleSkillInfo GetManualSkillInfo(PetInfo petInfo, int index)
	{
		if (petInfo == null)
		{
			return null;
		}
		List<PetAttribute> list = PetEvoGlobal.ManualSkill(petInfo.petId);
		if (list.get_Count() > 0)
		{
			BattleSkillInfo battleSkillInfo = new BattleSkillInfo();
			battleSkillInfo.skillId = list.get_Item(0).roleSkillId;
			battleSkillInfo.skillIdx = index + 41;
			battleSkillInfo.skillLv = 1;
		}
		return null;
	}

	public int GetFuseRitualSkillBound(PetInfo petInfo)
	{
		if (petInfo == null)
		{
			return 2147483647;
		}
		Pet pet = DataReader<Pet>.Get(petInfo.petId);
		if (pet == null)
		{
			return 2147483647;
		}
		return pet.fuseAP;
	}

	public BattleSkillInfo GetFuseRitualSkillInfo(PetInfo petInfo, int index)
	{
		if (petInfo == null)
		{
			return null;
		}
		Pet pet = DataReader<Pet>.Get(petInfo.petId);
		if (pet == null)
		{
			return null;
		}
		return new BattleSkillInfo
		{
			skillId = pet.fuseNeedSkill,
			skillIdx = 101,
			skillLv = 1
		};
	}

	public List<int> GetTalentIDs(long uid)
	{
		List<int> list = new List<int>();
		PetInfo petInfo = this.GetPetInfo(uid);
		if (petInfo != null)
		{
			for (int i = 0; i < petInfo.petTalents.get_Count(); i++)
			{
				list.Add(petInfo.petTalents.get_Item(i).talentId);
			}
		}
		return list;
	}

	public List<int> GetSummonOwnerAttrChange(long uid)
	{
		List<int> list = new List<int>();
		PetInfo petInfo = this.GetPetInfo(uid);
		if (petInfo != null)
		{
			List<PetAttribute> skills = PetEvoGlobal.GetSkills12(petInfo.petId);
			for (int i = 0; i < skills.get_Count(); i++)
			{
				using (List<int>.Enumerator enumerator = skills.get_Item(i).templateIds.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int current = enumerator.get_Current();
						Debug.LogError(petInfo.petId + " Get12: " + current);
					}
				}
				list.AddRange(skills.get_Item(i).templateIds);
			}
		}
		return list;
	}

	public XDict<int, int> GetCheckOwnerAttrChange(long uid)
	{
		XDict<int, int> xDict = new XDict<int, int>();
		PetInfo petInfo = this.GetPetInfo(uid);
		if (petInfo != null)
		{
			List<PetAttribute> skills = PetEvoGlobal.GetSkills13(petInfo.petId);
			for (int i = 0; i < skills.get_Count(); i++)
			{
				for (int j = 0; j < skills.get_Item(i).templateIds.get_Count(); j++)
				{
					Debug.LogError(string.Concat(new object[]
					{
						petInfo.petId,
						" Get13 item.Key: ",
						skills.get_Item(i).templateIds.get_Item(j),
						" item.Value: ",
						skills.get_Item(i).templateIds2.get_Item(j)
					}));
					xDict.Add(skills.get_Item(i).templateIds.get_Item(j), skills.get_Item(i).templateIds2.get_Item(j));
				}
			}
		}
		return xDict;
	}

	public XDict<int, int> GetCheckPetSelfAttrChange(long uid)
	{
		XDict<int, int> xDict = new XDict<int, int>();
		PetInfo petInfo = this.GetPetInfo(uid);
		if (petInfo != null)
		{
			List<PetAttribute> skills = PetEvoGlobal.GetSkills14(petInfo.petId);
			for (int i = 0; i < skills.get_Count(); i++)
			{
				for (int j = 0; j < skills.get_Item(i).templateIds.get_Count(); j++)
				{
					Debug.LogError(string.Concat(new object[]
					{
						petInfo.petId,
						" Get14 item.Key: ",
						skills.get_Item(i).templateIds.get_Item(j),
						" item.Value: ",
						skills.get_Item(i).templateIds2.get_Item(j)
					}));
					xDict.Add(skills.get_Item(i).templateIds.get_Item(j), skills.get_Item(i).templateIds2.get_Item(j));
				}
			}
		}
		return xDict;
	}

	public List<BattleSkillInfo> GetSkillInfo(int petID)
	{
		List<BattleSkillInfo> list = new List<BattleSkillInfo>();
		List<PetAttribute> skills = PetEvoGlobal.GetSkills10(petID);
		List<PetAttribute> list2 = PetEvoGlobal.ManualSkill(petID);
		for (int i = 0; i < skills.get_Count(); i++)
		{
			list.Add(new BattleSkillInfo
			{
				skillIdx = skills.get_Item(i).petSkillSlot,
				skillId = skills.get_Item(i).petSkillId,
				skillLv = skills.get_Item(i).petSkillLv
			});
		}
		for (int j = 0; j < list2.get_Count(); j++)
		{
			list.Add(new BattleSkillInfo
			{
				skillIdx = list2.get_Item(j).petSkillSlot,
				skillId = list2.get_Item(j).petSkillId,
				skillLv = list2.get_Item(j).petSkillLv
			});
		}
		return list;
	}

	public int GetFormationPetLevel()
	{
		PetFormation petFormation = PetManager.Instance.Formation.Find((PetFormation a) => a.formationId == PetManager.Instance.CurrentFormationID);
		if (petFormation == null || petFormation.petFormationArr == null || petFormation.petFormationArr.Int64Array == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < petFormation.petFormationArr.Int64Array.get_Count(); i++)
		{
			Int64IndexValue int64IndexValue = petFormation.petFormationArr.Int64Array.get_Item(i);
			PetInfo petInfo = PetManager.Instance.GetPetInfo(int64IndexValue.value);
			if (petInfo != null)
			{
				num += petInfo.lv;
			}
		}
		return num;
	}

	public void GetFormationAddAttrValue(PetFormation formation, out int value1, out int value2, out int value3)
	{
		value1 = 0;
		value2 = 0;
		value3 = 0;
		for (int i = 0; i < formation.petFormationArr.Int64Array.get_Count(); i++)
		{
			Int64IndexValue int64IndexValue = formation.petFormationArr.Int64Array.get_Item(i);
			PetInfo petInfo = PetManager.Instance.GetPetInfo(int64IndexValue.value);
			Pet pet = DataReader<Pet>.Get(petInfo.petId);
			Attrs attrs = DataReader<Attrs>.Get(pet.attributeAddToPlayerID.get_Item(petInfo.star - 1));
			for (int j = 0; j < attrs.attrs.get_Count(); j++)
			{
				int num = attrs.attrs.get_Item(j);
				if (num != 201)
				{
					if (num != 301)
					{
						if (num == 601)
						{
							value2 += attrs.values.get_Item(j);
						}
					}
					else
					{
						value3 += attrs.values.get_Item(j);
					}
				}
				else
				{
					value1 += attrs.values.get_Item(j);
				}
			}
			attrs = DataReader<Attrs>.Get(pet.attributeAddToPlayerGrowID.get_Item(petInfo.star - 1));
			for (int k = 0; k < attrs.attrs.get_Count(); k++)
			{
				int num = attrs.attrs.get_Item(k);
				if (num != 201)
				{
					if (num != 301)
					{
						if (num == 601)
						{
							value2 += attrs.values.get_Item(k) * petInfo.lv;
						}
					}
					else
					{
						value3 += attrs.values.get_Item(k) * petInfo.lv;
					}
				}
				else
				{
					value1 += attrs.values.get_Item(k) * petInfo.lv;
				}
			}
		}
	}

	public long GetFormationAddFighting()
	{
		PetFormation petFormation = PetManager.Instance.Formation.Find((PetFormation a) => a.formationId == PetManager.Instance.CurrentFormationID);
		if (petFormation == null || petFormation.petFormationArr == null || petFormation.petFormationArr.Int64Array == null)
		{
			return 0L;
		}
		return this.GetFormationAddFighting(petFormation);
	}

	public long GetFormationAddFighting(PetFormation formation)
	{
		long num = 0L;
		for (int i = 0; i < formation.petFormationArr.Int64Array.get_Count(); i++)
		{
			Int64IndexValue int64IndexValue = formation.petFormationArr.Int64Array.get_Item(i);
			PetInfo petInfo = PetManager.Instance.GetPetInfo(int64IndexValue.value);
			Pet pet = DataReader<Pet>.Get(petInfo.petId);
			Attrs attrs = DataReader<Attrs>.Get(pet.attributeAddToPlayerID.get_Item(petInfo.star - 1));
			num += EquipGlobal.CalculateFightingByIDAndValue(attrs.attrs, attrs.values);
			attrs = DataReader<Attrs>.Get(pet.attributeAddToPlayerGrowID.get_Item(petInfo.star - 1));
			num += EquipGlobal.CalculateFightingByIDAndValue(attrs.attrs, attrs.values);
		}
		return num;
	}

	private void OnRefreshPets()
	{
		this.RefreshTalents();
		if (UIManagerControl.Instance.IsOpen("PetChooseUI"))
		{
			EventDispatcher.Broadcast("PetManager.RefreshPets");
		}
	}

	private string GetGrow(float grow)
	{
		return (grow * 100f).ToString("F2");
	}

	private List<int> GetFuseSkill(Pet dataPet)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < dataPet.fuseSkill.get_Count(); i++)
		{
			if (dataPet.fuseSkill.get_Item(i).key == EntityWorld.Instance.EntSelf.TypeID)
			{
				list.Add(dataPet.fuseSkill.get_Item(i).value);
			}
		}
		return list;
	}

	public void UpdatePetEvoUI()
	{
		PetEvoNaturalUI petEvoNaturalUI = UIManagerControl.Instance.GetUIIfExist("PetEvoNaturalUI") as PetEvoNaturalUI;
		if (petEvoNaturalUI != null)
		{
			petEvoNaturalUI.Initialise();
		}
		PetEvoSkillUI petEvoSkillUI = UIManagerControl.Instance.GetUIIfExist("PetEvoSkillUI") as PetEvoSkillUI;
		if (petEvoSkillUI != null)
		{
			petEvoSkillUI.Initialise();
			if (petEvoSkillUI.ClickedCellIndex != -1)
			{
				Transform child = petEvoSkillUI.get_transform().GetChild(petEvoSkillUI.ClickedCellIndex);
				Transform transform = child.FindChild("imgNatural");
				petEvoSkillUI.PlaySkillUpgrade(transform);
				petEvoSkillUI.ClickedCellIndex = -1;
			}
		}
	}

	private int GetDefaultPetId()
	{
		List<Pet> levelPassPets = PetManager.Instance.GetLevelPassPets();
		for (int i = 0; i < levelPassPets.get_Count(); i++)
		{
			PetInfo petInfoById = this.GetPetInfoById((int)levelPassPets.get_Item(i).id);
			if (petInfoById != null)
			{
				return petInfoById.petId;
			}
		}
		return (int)levelPassPets.get_Item(0).id;
	}

	private int GetUpgradeDefaultPetID()
	{
		List<Pet> list = new List<Pet>();
		List<Pet> dataList = DataReader<Pet>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			int num = dataList.get_Item(i).needFragment.get_Item(0);
			long num2 = BackpackManager.Instance.OnGetGoodCount(dataList.get_Item(i).fragmentId);
			if (EntityWorld.Instance.EntSelf.Lv >= dataList.get_Item(i).readNeedRoleLv && num2 >= (long)num)
			{
				list.Add(dataList.get_Item(i));
			}
		}
		if (list.get_Count() > 0)
		{
			return (int)list.get_Item(0).id;
		}
		return (int)dataList.get_Item(0).id;
	}

	private static string GetItemString(List<ItemBriefInfo> items)
	{
		string text = string.Empty;
		for (int i = 0; i < items.get_Count(); i++)
		{
			if (i != 0)
			{
				text += ", ";
			}
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				GameDataUtils.GetItemName(items.get_Item(i).cfgId, true, 0L),
				"x",
				items.get_Item(i).count
			});
		}
		return text;
	}

	public static int GetReturnFragment(Pet dataPet, int decompose_star)
	{
		if (decompose_star < 0)
		{
			return 0;
		}
		int num = 0;
		int num2 = 0;
		while (num2 < decompose_star && num2 < dataPet.returnFragment.get_Count())
		{
			num += dataPet.returnFragment.get_Item(num2);
			num2++;
		}
		return num;
	}
}
