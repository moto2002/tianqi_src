using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PetTaskFormationUIView : UIBase
{
	public static PetTaskFormationUIView Instance;

	private ListPool mInfoItemListPool;

	private ListPool mFormationItemListPool;

	private Text m_lblTaskTitle;

	private Text m_lblSuccessRate;

	private GameObject m_goRewardItem;

	private long m_taskuid;

	public int m_task_quality;

	public List<int> m_petIds = new List<int>();

	private GameObject m_goRewardItemCache;

	private int rate_arg_base;

	private int rate_arg_quality;

	private int rate_arg_limit;

	private int rate_arg_nofit;

	private int rate_arg_basefit;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = false;
		this.isInterruptStick = true;
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		PetTaskFormationUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.InitAllArgs();
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mInfoItemListPool = base.FindTransform("InfoItemList").GetComponent<ListPool>();
		this.mInfoItemListPool.SetItem("PTF_InfoItem");
		this.mInfoItemListPool.Create(6, null);
		this.mFormationItemListPool = base.FindTransform("FormationItemList").GetComponent<ListPool>();
		this.mFormationItemListPool.SetItem("PTF_FormationItem");
		this.mFormationItemListPool.Create(3, null);
		this.m_lblTaskTitle = base.FindTransform("TaskTitle").GetComponent<Text>();
		this.m_lblSuccessRate = base.FindTransform("SuccessRate").GetComponent<Text>();
		this.m_goRewardItem = base.FindTransform("RewardItem").get_gameObject();
		base.FindTransform("ButtonBegin").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickButtonBegin));
		base.FindTransform("BtnClose").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickBtnClose));
		base.FindTransform("BtnDesc").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickBtnDesc));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.m_lblSuccessRate.set_text(string.Format("成功率: {0}%", 0));
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.m_petIds.Clear();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			PetTaskFormationUIView.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void OnClickButtonBegin()
	{
		PetTaskManager.Instance.SendPickUpPetTask(this.m_taskuid, this.m_petIds);
	}

	private void OnClickBtnDesc()
	{
		SpecialInstanceDescUI.Open(UINodesManager.TopUIRoot, 513700, 513701);
	}

	private void OnClickBtnClose()
	{
		PetTaskFormationUIView.Close();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	public void RefreshUI(long taskuid, int itemId)
	{
		this.m_taskuid = taskuid;
		if (this.m_goRewardItemCache == null)
		{
			ItemShow.ShowItem(this.m_goRewardItem.get_transform(), itemId, -1L, false, null, 2001);
		}
		else
		{
			ItemShow.SetItem(this.m_goRewardItemCache, itemId, -1L, false, null, 2001);
		}
		this.mFormationItemListPool.Create(3, delegate(int index)
		{
			if (index < this.mFormationItemListPool.Items.get_Count())
			{
				PetFormationPet component = this.mFormationItemListPool.Items.get_Item(index).GetComponent<PetFormationPet>();
				component.SetItem(null, null, false);
			}
		});
		PetTaskInfo pti = PetTaskManager.Instance.GetPetTaskInfo(taskuid);
		if (pti == null)
		{
			Debug.LogError("PetTaskInfo no exist, uid = " + taskuid);
			return;
		}
		ChongWuRenWuPinZhi chongWuRenWuPinZhi = DataReader<ChongWuRenWuPinZhi>.Get(pti.taskId);
		if (chongWuRenWuPinZhi == null)
		{
			Debug.LogError("GameData.ChongWuRenWuPeiZhi no exist, id = " + pti.taskId);
			return;
		}
		this.m_lblTaskTitle.set_text(GameDataUtils.GetChineseContent(pti.taskName, false) + "    任务时间: " + TimeConverter.GetTime(chongWuRenWuPinZhi.time, TimeFormat.HHMM_Chinese_MNoZero));
		this.m_task_quality = chongWuRenWuPinZhi.petquality;
		this.mInfoItemListPool.Create(6, delegate(int index)
		{
			if (index < this.mInfoItemListPool.Items.get_Count())
			{
				PTF_InfoItem component = this.mInfoItemListPool.Items.get_Item(index).GetComponent<PTF_InfoItem>();
				if (index < 3 && index < pti.monsterId.get_Count())
				{
					component.SetItemToMonster(pti.monsterId.get_Item(index));
				}
				else if (index < 6 && index - 3 < pti.petId.get_Count())
				{
					component.SetItemToPet(pti.petId.get_Item(index - 3), 1);
				}
			}
		});
	}

	public void SendPetFormation(List<long> listPets)
	{
		UIManagerControl.Instance.HideUI("PetSelectUI");
		this.m_petIds.Clear();
		for (int i = 0; i < listPets.get_Count(); i++)
		{
			PetInfo petInfo = PetManager.Instance.GetPetInfo(listPets.get_Item(i));
			if (petInfo != null)
			{
				this.m_petIds.Add(petInfo.petId);
			}
		}
		for (int j = 3; j < this.mInfoItemListPool.Items.get_Count(); j++)
		{
			PTF_InfoItem component = this.mInfoItemListPool.Items.get_Item(j).GetComponent<PTF_InfoItem>();
			component.SetMatch(this.m_petIds.Contains(component.m_petId));
		}
		PetTaskInfo petTaskInfo = PetTaskManager.Instance.GetPetTaskInfo(this.m_taskuid);
		if (petTaskInfo == null)
		{
			return;
		}
		int num = this.CalSuccessRate(petTaskInfo);
		this.m_lblSuccessRate.set_text(string.Format("成功率: {0}%", num));
		for (int k = 0; k < this.mFormationItemListPool.Items.get_Count(); k++)
		{
			if (k < this.m_petIds.get_Count())
			{
				Pet dataPet = DataReader<Pet>.Get(this.m_petIds.get_Item(k));
				PetInfo petInfoById = PetManager.Instance.GetPetInfoById(this.m_petIds.get_Item(k));
				PetFormationPet component2 = this.mFormationItemListPool.Items.get_Item(k).GetComponent<PetFormationPet>();
				if (component2 != null)
				{
					component2.SetItem(petInfoById, dataPet, false);
				}
			}
			else
			{
				PetFormationPet component3 = this.mFormationItemListPool.Items.get_Item(k).GetComponent<PetFormationPet>();
				if (component3 != null)
				{
					component3.SetItem(null, null, false);
				}
			}
		}
	}

	public bool IsRecommend(int petId)
	{
		PetTaskInfo petTaskInfo = PetTaskManager.Instance.GetPetTaskInfo(this.m_taskuid);
		if (petTaskInfo == null)
		{
			Debug.LogError("PetTaskInfo no exist, uid = " + this.m_taskuid);
			return false;
		}
		return petTaskInfo.petId.Contains(petId);
	}

	public static void Close()
	{
		if (PetTaskFormationUIView.Instance != null)
		{
			UIType uiType = PetTaskFormationUIView.Instance.uiType;
			PetTaskFormationUIView.Instance.Show(false);
			UIStackManager.Instance.PopUIPrevious(uiType);
		}
	}

	private int CalSuccessRate(PetTaskInfo pti)
	{
		ChongWuRenWuPinZhi chongWuRenWuPinZhi = DataReader<ChongWuRenWuPinZhi>.Get(pti.taskId);
		if (chongWuRenWuPinZhi == null)
		{
			Debug.LogError("GameData.ChongWuRenWuPeiZhi no exist, id = " + pti.taskId);
			return 0;
		}
		int num = 0;
		for (int i = 0; i < this.m_petIds.get_Count(); i++)
		{
			if (pti.petId.Contains(this.m_petIds.get_Item(i)))
			{
				PetInfo petInfoById = PetManager.Instance.GetPetInfoById(this.m_petIds.get_Item(i));
				int num2 = this.rate_arg_base + (petInfoById.star - chongWuRenWuPinZhi.petquality) * this.rate_arg_quality;
				num += Mathf.Max(num2, this.rate_arg_basefit);
			}
			else
			{
				num += this.rate_arg_nofit;
			}
		}
		return Mathf.Clamp(num, 0, this.rate_arg_limit);
	}

	private void InitAllArgs()
	{
		this.rate_arg_base = this.GetInt("base");
		this.rate_arg_quality = this.GetInt("quality");
		this.rate_arg_limit = this.GetInt("limit");
		this.rate_arg_nofit = this.GetInt("nofit");
		this.rate_arg_basefit = this.GetInt("basefit");
	}

	private int GetInt(string key)
	{
		ChongWuRenWuPeiZhi chongWuRenWuPeiZhi = DataReader<ChongWuRenWuPeiZhi>.Get(key);
		if (chongWuRenWuPeiZhi != null && chongWuRenWuPeiZhi.value.get_Count() > 0)
		{
			return int.Parse(GameDataUtils.SplitString4Dot0(chongWuRenWuPeiZhi.value.get_Item(0)));
		}
		return 0;
	}
}
