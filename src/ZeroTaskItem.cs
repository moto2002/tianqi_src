using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZeroTaskItem : MonoBehaviour
{
	public Action<BaseTask> EventHandler;

	private Text mTxTitle;

	private Image mImgTask;

	private Text mTxTarget;

	private Text mTxDesc;

	private GameObject mRewardPanel;

	private GameObject mGoFinish;

	private Text mTxButton;

	private Image mImgButton;

	private ButtonCustom mButton;

	private BaseTask mTask;

	private LingChengRenWuZuPeiZhi mData;

	private List<GameObject> mRewardList;

	public int GroupId
	{
		get;
		private set;
	}

	private void Awake()
	{
		this.mRewardList = new List<GameObject>();
		this.mTxTitle = UIHelper.GetText(base.get_transform(), "View/Title/txTitle");
		this.mImgTask = UIHelper.GetImage(base.get_transform(), "View/TaskImg");
		this.mTxTarget = UIHelper.GetText(base.get_transform(), "View/Target/txTarget");
		this.mTxDesc = UIHelper.GetText(base.get_transform(), "View/Target/txDesc");
		this.mRewardPanel = UIHelper.GetObject(base.get_transform(), "View/Rewards/Grid");
		this.mGoFinish = UIHelper.GetObject(base.get_transform(), "View/imgFinish");
		this.mButton = UIHelper.GetCustomButton(base.get_transform(), "View/BtnGet");
		this.mTxButton = UIHelper.GetText(this.mButton.get_transform(), "Text");
		this.mImgButton = this.mButton.GetComponent<Image>();
		this.mButton.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButton);
	}

	private void OnClickButton(GameObject go)
	{
		if (this.EventHandler != null && this.mTask != null)
		{
			this.EventHandler.Invoke(this.mTask);
		}
	}

	private void RefreshTask(bool canClick)
	{
		string chineseContent = GameDataUtils.GetChineseContent(this.mTask.Data.dramaIntroduce, false);
		this.mTxTitle.set_text(string.Format(MainTaskManager.Instance.ZERO_COLOR[this.mData.color], chineseContent));
		this.SetImg(this.mTask.Task.extParams.get_Item(1));
		this.mTxTarget.set_text(string.Format("任务目标（{0}/{1}）", this.mTask.Task.extParams.get_Item(5), this.mTask.Task.extParams.get_Item(3)));
		this.mTxDesc.set_text(MainTaskItem.GetTaskTarget(this.mTask, false, string.Empty));
		this.ClearReward();
		for (int i = 0; i < this.mData.reward.get_Count(); i++)
		{
			this.CreateRewards(this.mData.reward.get_Item(i).key, (long)this.mData.reward.get_Item(i).value);
		}
		List<DiaoLuo> dataList = DataReader<DiaoLuo>.DataList;
		for (int j = 0; j < this.mData.rewardId.get_Count(); j++)
		{
			this.CreateDropRewards(dataList, this.mData.rewardId.get_Item(j));
		}
		this.SetButton(canClick);
	}

	private void CreateDropRewards(List<DiaoLuo> diaoluos, int dropId)
	{
		int lv = EntityWorld.Instance.EntSelf.Lv;
		for (int i = 0; i < diaoluos.get_Count(); i++)
		{
			DiaoLuo diaoLuo = diaoluos.get_Item(i);
			if (diaoLuo.ruleId == dropId)
			{
				if (diaoLuo.minLv == diaoLuo.maxLv && diaoLuo.minLv == 0)
				{
					this.CreateRewards(diaoLuo.goodsId, diaoLuo.minNum);
				}
				else if (diaoLuo.minLv == diaoLuo.maxLv && lv == diaoLuo.minLv)
				{
					this.CreateRewards(diaoLuo.goodsId, diaoLuo.minNum);
				}
				else if (diaoLuo.minLv < diaoLuo.maxLv && lv >= diaoLuo.minLv && lv < diaoLuo.maxLv)
				{
					this.CreateRewards(diaoLuo.goodsId, diaoLuo.minNum);
				}
			}
		}
	}

	private void SetImg(int bankId)
	{
		LingChengRenWuKuPeiZhi lingChengRenWuKuPeiZhi = DataReader<LingChengRenWuKuPeiZhi>.Get(bankId);
		if (lingChengRenWuKuPeiZhi != null)
		{
			ResourceManager.SetSprite(this.mImgTask, GameDataUtils.GetIcon(lingChengRenWuKuPeiZhi.taskPic));
		}
	}

	private void SetButton(bool canClick)
	{
		if (MainTaskManager.Instance.ZeroTaskTimes <= 0)
		{
			if (this.mTask.Task.extParams.get_Item(4) == 5)
			{
				this.mGoFinish.SetActive(true);
				this.mButton.get_gameObject().SetActive(false);
			}
			else
			{
				this.mGoFinish.SetActive(false);
				this.mButton.get_gameObject().SetActive(true);
				this.mTxButton.set_text(GameDataUtils.GetChineseContent(310032, false));
				ResourceManager.SetSprite(this.mImgButton, ResourceManager.GetIconSprite("button_gray_1"));
			}
		}
		else
		{
			switch (this.mTask.Task.extParams.get_Item(4))
			{
			case 1:
				this.mGoFinish.SetActive(false);
				this.mButton.get_gameObject().SetActive(true);
				this.mTxButton.set_text(GameDataUtils.GetChineseContent(310032, false));
				ResourceManager.SetSprite(this.mImgButton, ResourceManager.GetIconSprite("button_gray_1"));
				break;
			case 2:
				this.mGoFinish.SetActive(false);
				this.mButton.get_gameObject().SetActive(true);
				this.mTxButton.set_text(GameDataUtils.GetChineseContent(310011, false));
				ResourceManager.SetSprite(this.mImgButton, ResourceManager.GetIconSprite((!canClick) ? "button_gray_1" : "button_yellow_1"));
				break;
			case 3:
				this.mGoFinish.SetActive(false);
				this.mButton.get_gameObject().SetActive(true);
				this.mTxButton.set_text(GameDataUtils.GetChineseContent(310031, false));
				ResourceManager.SetSprite(this.mImgButton, ResourceManager.GetIconSprite("button_orange_1"));
				break;
			case 4:
				this.mGoFinish.SetActive(false);
				this.mButton.get_gameObject().SetActive(true);
				this.mTxButton.set_text(GameDataUtils.GetChineseContent(310012, false));
				ResourceManager.SetSprite(this.mImgButton, ResourceManager.GetIconSprite("button_yellow_1"));
				break;
			case 5:
				this.mGoFinish.SetActive(true);
				this.mButton.get_gameObject().SetActive(false);
				break;
			}
		}
	}

	private void ClearReward()
	{
		for (int i = 0; i < this.mRewardList.get_Count(); i++)
		{
			this.mRewardList.get_Item(i).set_name("Unused");
			this.mRewardList.get_Item(i).SetActive(false);
		}
	}

	private GameObject GetUnusedItem()
	{
		for (int i = 0; i < this.mRewardList.get_Count(); i++)
		{
			if (this.mRewardList.get_Item(i).get_name() == "Unused")
			{
				return this.mRewardList.get_Item(i);
			}
		}
		return null;
	}

	private GameObject CreateRewards(int id, long value)
	{
		GameObject go = this.GetUnusedItem();
		if (go == null)
		{
			go = ResourceManager.GetInstantiate2Prefab("TaskRewardItem");
			go.GetComponent<Button>().get_onClick().AddListener(delegate
			{
				int num = int.Parse(go.get_name());
				if (num != 1)
				{
					ItemTipUIViewModel.ShowItem(num, null);
				}
			});
			UGUITools.SetParent(this.mRewardPanel, go, false);
			this.mRewardList.Add(go);
		}
		go.set_name(id.ToString());
		ResourceManager.SetSprite(go.GetComponent<Image>(), GameDataUtils.GetItemFrame(id));
		ResourceManager.SetSprite(go.get_transform().FindChild("Image").GetComponent<Image>(), GameDataUtils.GetItemIcon(id));
		go.get_transform().FindChild("Text").GetComponent<Text>().set_text(Utils.SwitchChineseNumber(value, 1));
		Items items = DataReader<Items>.Get(id);
		if (items == null || items.step <= 0)
		{
			go.get_transform().FindChild("ItemStep").get_gameObject().SetActive(false);
		}
		else
		{
			go.get_transform().FindChild("ItemStep").get_gameObject().SetActive(true);
			go.get_transform().FindChild("ItemStep").FindChild("ItemStepText").GetComponent<Text>().set_text(string.Format(GameDataUtils.GetChineseContent(505023, false), items.step));
		}
		go.SetActive(true);
		return go;
	}

	public void SetData(BaseTask task, bool canClick)
	{
		this.mTask = task;
		this.mData = DataReader<LingChengRenWuZuPeiZhi>.Get(task.Task.extParams.get_Item(2));
		if (this.mTask != null && this.mData != null)
		{
			this.RefreshTask(canClick);
		}
		else
		{
			Debug.Log("<color=red>Error:</color>零城任务配表数据为空!!!");
		}
	}

	public void SetUnused()
	{
		base.get_gameObject().set_name("Unused");
		base.get_gameObject().SetActive(false);
	}
}
