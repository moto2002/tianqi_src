using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PetTaskItem : BaseUIBehaviour
{
	private GameObject m_goButtonOK;

	private GameObject m_goButtonReward;

	private Image m_spStatusIcon;

	private Image m_spQualityIcon;

	private Text m_lblTaskName;

	private Text m_lblTaskReward;

	public long m_taskuid;

	private PetTaskInfo m_pti;

	private int m_itemId;

	private ListPool mItemListPool;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mItemListPool = base.FindTransform("ItemList").GetComponent<ListPool>();
		this.mItemListPool.SetItem("ItemShow");
		this.m_goButtonOK = base.FindTransform("ButtonOK").get_gameObject();
		this.m_goButtonReward = base.FindTransform("ButtonReward").get_gameObject();
		this.m_spStatusIcon = base.FindTransform("StatusIcon").GetComponent<Image>();
		this.m_spQualityIcon = base.FindTransform("QualityIcon").GetComponent<Image>();
		this.m_lblTaskName = base.FindTransform("TaskName").GetComponent<Text>();
		this.m_lblTaskReward = base.FindTransform("TaskReward").GetComponent<Text>();
		base.FindTransform("ButtonOK").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickButtonOK));
		base.FindTransform("ButtonReward").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickButtonReward));
	}

	private void Update()
	{
		this.RefreshTime(null);
	}

	private void OnClickButtonOK()
	{
		UIManagerControl.Instance.OpenUI("PetTaskFormationUI", UINodesManager.NormalUIRoot, true, UIType.FullScreen);
		PetTaskFormationUIView.Instance.RefreshUI(this.m_taskuid, this.m_itemId);
	}

	private void OnClickButtonReward()
	{
		PetTaskManager.Instance.SendGetPetTaskReward(this.m_taskuid);
	}

	public void Refresh(PetTaskInfo pti)
	{
		this.m_pti = pti;
		this.m_taskuid = pti.idx;
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		if (this.m_pti == null)
		{
			return;
		}
		ChongWuRenWuPinZhi chongWuRenWuPinZhi = DataReader<ChongWuRenWuPinZhi>.Get(pti.taskId);
		if (chongWuRenWuPinZhi == null)
		{
			Debug.LogError("GameData.ChongWuRenWuPinZhi no exist, id = " + pti.taskId);
			return;
		}
		base.get_transform().set_name(pti.taskId + "_quality" + chongWuRenWuPinZhi.petquality);
		if (this.m_goButtonOK != null)
		{
			this.m_goButtonOK.SetActive(pti.Status == PetTaskInfo.PetTaskStatus.UnPickUp);
		}
		if (this.m_goButtonReward != null)
		{
			this.m_goButtonReward.SetActive(pti.Status == PetTaskInfo.PetTaskStatus.achieve);
		}
		if (this.m_spStatusIcon != null)
		{
			if (pti.Status == PetTaskInfo.PetTaskStatus.undone)
			{
				this.m_spStatusIcon.get_gameObject().SetActive(true);
				ResourceManager.SetIconSprite(this.m_spStatusIcon, "yinzhang_2");
				this.m_spStatusIcon.get_transform().set_localScale(Vector3.get_one());
				this.m_spStatusIcon.SetNativeSize();
			}
			else if (pti.Status == PetTaskInfo.PetTaskStatus.receive)
			{
				this.m_spStatusIcon.get_gameObject().SetActive(true);
				ResourceManager.SetIconSprite(this.m_spStatusIcon, "font_yiwancheng");
				this.m_spStatusIcon.get_transform().set_localScale(Vector3.get_one() * 0.8f);
				this.m_spStatusIcon.SetNativeSize();
			}
			else
			{
				this.m_spStatusIcon.get_gameObject().SetActive(false);
			}
		}
		ResourceManager.SetIconSprite(this.m_spQualityIcon, chongWuRenWuPinZhi.art);
		this.RefreshTime(chongWuRenWuPinZhi);
		this.SetRewards(chongWuRenWuPinZhi, pti);
	}

	public void RefreshTime(ChongWuRenWuPinZhi dataRWPZ)
	{
		if (this.m_pti == null)
		{
			return;
		}
		if (this.m_pti.Status == PetTaskInfo.PetTaskStatus.UnPickUp)
		{
			if (dataRWPZ != null)
			{
				this.m_lblTaskName.set_text(GameDataUtils.GetChineseContent(this.m_pti.taskName, false) + "(" + TimeConverter.GetTime(dataRWPZ.time, TimeFormat.HHMM_Chinese_MNoZero) + ")");
			}
			return;
		}
		if (this.m_pti.Status == PetTaskInfo.PetTaskStatus.undone)
		{
			int remainSecond = TimeManager.Instance.GetRemainSecond(this.m_pti.times);
			if (remainSecond > 0)
			{
				this.m_lblTaskName.set_text(GameDataUtils.GetChineseContent(this.m_pti.taskName, false) + "(剩余" + TimeConverter.GetTime(remainSecond, TimeFormat.HHMM_Chinese) + ")");
				return;
			}
		}
		this.m_lblTaskName.set_text(GameDataUtils.GetChineseContent(this.m_pti.taskName, false));
	}

	private void SetRewards(ChongWuRenWuPinZhi dataRWPZ, PetTaskInfo pti)
	{
		this.m_lblTaskReward.set_text(string.Empty);
		if (dataRWPZ.drop.get_Count() > 0)
		{
			int templateId = 0;
			long dropNum = DropUtil.GetDropNum(dataRWPZ.drop.get_Item(0), EntityWorld.Instance.EntSelf.Lv, ref templateId);
			this.m_lblTaskReward.set_text(string.Format("奖励: 人物经验+{0}", Utils.GetItemNum(templateId, dropNum)));
		}
		List<int> itemIds = new List<int>();
		List<long> itemNums = new List<long>();
		if (pti.rewards.get_Count() > 0)
		{
			this.m_itemId = pti.rewards.get_Item(0).cfgId;
			itemIds.Add(pti.rewards.get_Item(0).cfgId);
			itemNums.Add(pti.rewards.get_Item(0).count);
		}
		this.mItemListPool.Create(itemIds.get_Count(), delegate(int index)
		{
			if (index < this.mItemListPool.Items.get_Count() && index < itemIds.get_Count() && index < itemNums.get_Count())
			{
				ItemShow.SetItem(this.mItemListPool.Items.get_Item(index), itemIds.get_Item(index), itemNums.get_Item(index), false, UINodesManager.T2RootOfSpecial, 14000);
			}
		});
	}
}
