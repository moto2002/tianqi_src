using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceDetailUI : UIBase
{
	private Text TextInstanceTitle;

	private Image ImageStar1_1;

	private Image ImageStar1_2;

	private Image ImageStar2_1;

	private Image ImageStar2_2;

	private Image ImageStar3_1;

	private Image ImageStar3_2;

	private Transform Drop;

	private Transform Condition1;

	private Transform Condition2;

	private Transform Condition3;

	private Text TextPower;

	private Text TextEnergy;

	private ButtonCustom BtnEnter;

	private ButtonCustom BtnClean;

	private Text TextChallengeTime;

	private Text TextTimeLast;

	private RawImage ImageMap;

	private GameObject m_goPetLimit;

	private Text m_lblPetLimitText;

	private Image m_spPetLimitIcon;

	private int currentInstanceID;

	private List<GameObject> listDropItems = new List<GameObject>();

	private int CfgID;

	private int currentInstanceType;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.75f;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.TextInstanceTitle = base.FindTransform("TextInstanceTitle").GetComponent<Text>();
		this.ImageStar1_1 = base.FindTransform("ImageStar1_1").GetComponent<Image>();
		this.ImageStar1_2 = base.FindTransform("ImageStar1_2").GetComponent<Image>();
		this.ImageStar2_1 = base.FindTransform("ImageStar2_1").GetComponent<Image>();
		this.ImageStar2_2 = base.FindTransform("ImageStar2_2").GetComponent<Image>();
		this.ImageStar3_1 = base.FindTransform("ImageStar3_1").GetComponent<Image>();
		this.ImageStar3_2 = base.FindTransform("ImageStar3_2").GetComponent<Image>();
		this.Drop = base.FindTransform("Drop");
		this.Condition1 = base.FindTransform("Condition1");
		this.Condition2 = base.FindTransform("Condition2");
		this.Condition3 = base.FindTransform("Condition3");
		this.TextPower = base.FindTransform("TextPower").GetComponent<Text>();
		this.TextEnergy = base.FindTransform("TextEnergy").GetComponent<Text>();
		this.BtnEnter = base.FindTransform("BtnEnter").GetComponent<ButtonCustom>();
		this.BtnClean = base.FindTransform("BtnClean").GetComponent<ButtonCustom>();
		this.TextChallengeTime = base.FindTransform("TextChallengeTime").GetComponent<Text>();
		this.TextTimeLast = base.FindTransform("TextTimeLast").GetComponent<Text>();
		this.ImageMap = base.FindTransform("ImageMap").GetComponent<RawImage>();
		this.m_goPetLimit = base.FindTransform("PetLimit").get_gameObject();
		this.m_lblPetLimitText = base.FindTransform("PetLimitText").GetComponent<Text>();
		this.m_spPetLimitIcon = base.FindTransform("PetLimitIcon").GetComponent<Image>();
		this.BtnEnter.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEnter);
		this.BtnClean.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnClean);
	}

	protected override void OnEnable()
	{
		if (this.currentInstanceID != 0)
		{
		}
	}

	protected override void OnDisable()
	{
		UIManagerControl.Instance.HideUI("ChangePetChooseUI");
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.InstanceDetailShouldRefresh, new Callback(this.InstanceDetailShouldRefresh));
		EventDispatcher.AddListener(DungeonManagerEvent.InstanceDataHaveChange, new Callback(this.InstanceDataHaveChange));
		EventDispatcher.AddListener(EventNames.OnGetAllDungeonChallengeTimesResetNty, new Callback(this.OnGetAllDungeonChallengeTimesResetNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.InstanceDetailShouldRefresh, new Callback(this.InstanceDetailShouldRefresh));
		EventDispatcher.RemoveListener(DungeonManagerEvent.InstanceDataHaveChange, new Callback(this.InstanceDataHaveChange));
		EventDispatcher.RemoveListener(EventNames.OnGetAllDungeonChallengeTimesResetNty, new Callback(this.OnGetAllDungeonChallengeTimesResetNty));
	}

	private void OpenCPC()
	{
		base.get_transform().GetComponent<RectTransform>().SetAsLastSibling();
		ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
		if (this.currentInstanceType == 104)
		{
			changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.PVE, base.get_transform(), 0);
		}
		else if (this.currentInstanceType == 101)
		{
			changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.Normal, base.get_transform(), 0);
		}
		else if (this.currentInstanceType == 102)
		{
			changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.Elite, base.get_transform(), 0);
		}
		else if (this.currentInstanceType == 103)
		{
			changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.PVE, base.get_transform(), 0);
		}
	}

	public void RefreshUI()
	{
		this.RefreshUI(this.currentInstanceID);
	}

	public void RefreshUI(int instanceID)
	{
		this.currentInstanceID = instanceID;
		FuBenJiChuPeiZhi fuBenJiChuPeiZhi = DataReader<FuBenJiChuPeiZhi>.Get(instanceID);
		if (fuBenJiChuPeiZhi != null)
		{
			this.currentInstanceType = fuBenJiChuPeiZhi.type;
		}
		if (base.get_gameObject().get_activeSelf())
		{
			this.OpenCPC();
		}
		this.BtnClean.get_gameObject().SetActive(true);
		this.RefreshPetLimit();
		ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(instanceID);
		this.TextInstanceTitle.set_text(GameDataUtils.GetChineseContent(zhuXianPeiZhi.name, false));
		this.TextEnergy.set_text(zhuXianPeiZhi.expendVit.ToString());
		this.TextPower.set_text(zhuXianPeiZhi.suitCapabilitie.ToString());
		Icon icon = DataReader<Icon>.Get(zhuXianPeiZhi.backgroundPic);
		if (icon == null)
		{
			Debug.LogError("icon == null  " + zhuXianPeiZhi.backgroundPic);
		}
		ResourceManager.SetTexture(this.ImageMap, icon.icon);
		this.Condition1.get_gameObject().SetActive(false);
		this.Condition2.get_gameObject().SetActive(false);
		this.Condition3.get_gameObject().SetActive(false);
		List<int> star = zhuXianPeiZhi.star;
		DungeonInfo dungeonInfo = DungeonManager.Instance.GetDungeonInfo(instanceID);
		int star2 = dungeonInfo.star;
		for (int i = 0; i < star.get_Count(); i++)
		{
			int num = star.get_Item(i);
			DungeonStarLv dungeonStarLv = DataReader<DungeonStarLv>.Get(num);
			if (dungeonStarLv == null)
			{
				Debuger.Error("DungeonStarLv 不存在 starID id = " + num, new object[0]);
			}
			else if (i == 0)
			{
				this.Condition1.get_gameObject().SetActive(true);
				this.Condition1.get_transform().FindChild("TextContent").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(dungeonStarLv.introduction, false));
			}
			else if (i == 1)
			{
				this.Condition2.get_gameObject().SetActive(true);
				this.Condition2.get_transform().FindChild("TextContent").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(dungeonStarLv.introduction, false));
			}
			else if (i == 2)
			{
				this.Condition3.get_gameObject().SetActive(true);
				this.Condition3.get_transform().FindChild("TextContent").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(dungeonStarLv.introduction, false));
			}
		}
		if (star2 == 0)
		{
			this.ImageStar1_1.get_gameObject().SetActive(true);
			this.ImageStar1_2.get_gameObject().SetActive(false);
			this.ImageStar2_1.get_gameObject().SetActive(true);
			this.ImageStar2_2.get_gameObject().SetActive(false);
			this.ImageStar3_1.get_gameObject().SetActive(true);
			this.ImageStar3_2.get_gameObject().SetActive(false);
		}
		else if (star2 == 1)
		{
			this.ImageStar1_1.get_gameObject().SetActive(false);
			this.ImageStar1_2.get_gameObject().SetActive(true);
			this.ImageStar2_1.get_gameObject().SetActive(true);
			this.ImageStar2_2.get_gameObject().SetActive(false);
			this.ImageStar3_1.get_gameObject().SetActive(true);
			this.ImageStar3_2.get_gameObject().SetActive(false);
		}
		else if (star2 == 2)
		{
			this.ImageStar1_1.get_gameObject().SetActive(false);
			this.ImageStar1_2.get_gameObject().SetActive(true);
			this.ImageStar2_1.get_gameObject().SetActive(false);
			this.ImageStar2_2.get_gameObject().SetActive(true);
			this.ImageStar3_1.get_gameObject().SetActive(true);
			this.ImageStar3_2.get_gameObject().SetActive(false);
		}
		else if (star2 == 3)
		{
			this.ImageStar1_1.get_gameObject().SetActive(false);
			this.ImageStar1_2.get_gameObject().SetActive(true);
			this.ImageStar2_1.get_gameObject().SetActive(false);
			this.ImageStar2_2.get_gameObject().SetActive(true);
			this.ImageStar3_1.get_gameObject().SetActive(false);
			this.ImageStar3_2.get_gameObject().SetActive(true);
		}
		for (int j = 0; j < this.listDropItems.get_Count(); j++)
		{
			Object.Destroy(this.listDropItems.get_Item(j));
		}
		this.listDropItems.Clear();
		if (zhuXianPeiZhi.reward.get_Count() > 0)
		{
			string text = string.Empty;
			for (int k = 0; k < zhuXianPeiZhi.reward.get_Count(); k++)
			{
				if (zhuXianPeiZhi.reward.get_Item(k).key == EntityWorld.Instance.EntSelf.TypeID)
				{
					text = zhuXianPeiZhi.reward.get_Item(k).value;
				}
			}
			if (text != string.Empty)
			{
				string[] array = text.Split(new char[]
				{
					','
				});
				for (int l = 0; l < array.Length; l++)
				{
					GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("InstanceDropItem");
					instantiate2Prefab.get_transform().SetParent(this.Drop);
					this.listDropItems.Add(instantiate2Prefab);
					InstanceDropItem component = instantiate2Prefab.GetComponent<InstanceDropItem>();
					component.RefreshUI(int.Parse(array[l]));
					component.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickInstanceDropItem);
				}
			}
		}
		if (DataReader<FuBenJiChuPeiZhi>.Get(instanceID).type == 102)
		{
			this.TextChallengeTime.set_text(GameDataUtils.GetChineseContent(510029, false));
			this.TextTimeLast.get_gameObject().SetActive(true);
			string text2 = GameDataUtils.GetChineseContent(510104, false);
			text2 = text2.Replace("{s1}", "<color=red>" + dungeonInfo.remainingChallengeTimes.ToString() + "</color>");
			this.TextTimeLast.set_text(text2);
		}
		else
		{
			this.TextChallengeTime.set_text(GameDataUtils.GetChineseContent(510029, false));
			this.TextTimeLast.get_gameObject().SetActive(false);
		}
	}

	private void RefreshPetLimit()
	{
		int instanceLimitPetType = InstanceManagerUI.GetInstanceLimitPetType();
		if (instanceLimitPetType > 0)
		{
			this.m_goPetLimit.SetActive(true);
			this.m_lblPetLimitText.set_text("宠物限制");
			ResourceManager.SetSprite(this.m_spPetLimitIcon, PetManagerBase.GetPetLimitType(instanceLimitPetType));
		}
		else
		{
			this.m_goPetLimit.SetActive(false);
		}
	}

	private void OnClickBtnAddTime(GameObject sender)
	{
		DungeonManager.Instance.BuyChallengeTimes(this.currentInstanceID, delegate
		{
			this.RefreshUI();
		});
	}

	private void OnClickBtnAddEnergy(GameObject sender)
	{
		EnergyManager.Instance.BuyEnergy(delegate
		{
			this.RefreshUI();
		});
	}

	private void OnClickBtnEnter(GameObject sender)
	{
		if (this.currentInstanceType == 102)
		{
			EliteDungeonManager.Instance.SendEliteChallengeReq(this.CfgID);
			return;
		}
		ZhuXianPeiZhi zhuXianPeiZhi = DataReader<ZhuXianPeiZhi>.Get(this.currentInstanceID);
		if (zhuXianPeiZhi.minLv > EntityWorld.Instance.EntSelf.Lv)
		{
			string text = GameDataUtils.GetChineseContent(510114, false);
			text = text.Replace("{s1}", zhuXianPeiZhi.minLv.ToString());
			UIManagerControl.Instance.ShowToastText(text);
			return;
		}
		if (DungeonManager.Instance.GetDungeonInfo(this.currentInstanceID).remainingChallengeTimes == 0)
		{
			UIManagerControl.Instance.ShowToastText("当前挑战次数剩余0");
			this.OnClickBtnAddTime(null);
			return;
		}
		if (zhuXianPeiZhi.expendVit > EntityWorld.Instance.EntSelf.Energy)
		{
			UIManagerControl.Instance.ShowToastText("没有足够的体力");
			this.OnClickBtnAddEnergy(null);
			return;
		}
		if (InstanceManagerUI.IsPetLimit())
		{
			return;
		}
		DungeonManager.Instance.SendChallengeDungeonReq(this.currentInstanceID);
	}

	private void OnClickBtnClean(GameObject sender)
	{
		DungeonInfo dungeonInfo = DungeonManager.Instance.GetDungeonInfo(this.currentInstanceID);
		if (dungeonInfo.star < 3)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(510091, false));
		}
		else
		{
			MopUpDialogUI mopUpDialogUI = UIManagerControl.Instance.OpenUI("MopUpDialogUI", UINodesManager.NormalUIRoot, false, UIType.Pop) as MopUpDialogUI;
			mopUpDialogUI.RefreshUI(this.currentInstanceID);
		}
	}

	private void OnClickExit()
	{
	}

	private void OnClickInstanceDropItem(GameObject sender)
	{
		ItemTipUIViewModel.ShowItem(sender.GetComponent<InstanceDropItem>().equipID, null);
	}

	private void InstanceDetailShouldRefresh()
	{
		this.RefreshUI();
	}

	private void InstanceDataHaveChange()
	{
	}

	private void OnGetAllDungeonChallengeTimesResetNty()
	{
		this.RefreshUI();
	}
}
