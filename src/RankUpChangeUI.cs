using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUpChangeUI : UIBase
{
	protected ListPool careerListPool;

	protected List<RankUpChangeUICareerUnit> careerUnitList = new List<RankUpChangeUICareerUnit>();

	protected Text RankUpChangeUIAttrTextTitle0;

	protected Text RankUpChangeUIAttrText0;

	protected Text RankUpChangeUIAttrTextTitle1;

	protected Text RankUpChangeUIAttrText1;

	protected Text RankUpChangeUIAttrTextTitle2;

	protected Text RankUpChangeUIAttrText2;

	protected Image RankUpChangeUISkillFG;

	protected Text RankUpChangeUISkillText;

	protected ScrollRectCustom RankUpChangeUIStageUnitSR;

	protected ListPool stageListPool;

	protected List<RankUpChangeUIStageUnit> stageUnitList = new List<RankUpChangeUIStageUnit>();

	protected GameObject RankUpChangeUIStageUnitLeftArrow;

	protected GameObject RankUpChangeUIStageUnitRightArrow;

	protected Transform RankUpChangeUIPreviewSlot;

	protected RankUpPreviewCell rankUpPreviewCell;

	protected GameObject RankUpChangeUISuccessSign;

	protected int typeID;

	protected int defaultCareer;

	protected int defaultStage;

	protected int curCareer;

	protected int curStage;

	protected int curSkillID;

	protected int TypeID
	{
		get
		{
			return this.typeID;
		}
		set
		{
			this.typeID = value;
		}
	}

	public int DefaultCareer
	{
		get
		{
			return this.defaultCareer;
		}
		set
		{
			this.defaultCareer = value;
		}
	}

	public int DefaultStage
	{
		get
		{
			return this.defaultStage;
		}
		set
		{
			this.defaultStage = value;
		}
	}

	protected int DefauleCareerIndex
	{
		get
		{
			if (!DataReader<JobIndex>.Contains(this.TypeID))
			{
				return 0;
			}
			JobIndex jobIndex = DataReader<JobIndex>.Get(this.TypeID);
			for (int i = 0; i < jobIndex.AdvancedJobId.get_Count(); i++)
			{
				if (jobIndex.AdvancedJobId.get_Item(i) == this.DefaultCareer)
				{
					return i;
				}
			}
			return 0;
		}
	}

	protected int DefauleStageIndex
	{
		get
		{
			if (!DataReader<AdvancedJob>.Contains(this.DefaultCareer))
			{
				return 0;
			}
			AdvancedJob advancedJob = DataReader<AdvancedJob>.Get(this.DefaultCareer);
			for (int i = 0; i < advancedJob.stageId.get_Count(); i++)
			{
				if (advancedJob.stageId.get_Item(i) == this.DefaultStage)
				{
					return i;
				}
			}
			return 0;
		}
	}

	public int CurCareer
	{
		get
		{
			return this.curCareer;
		}
		set
		{
			this.curCareer = value;
		}
	}

	public int CurStage
	{
		get
		{
			return this.curStage;
		}
		set
		{
			this.curStage = value;
		}
	}

	public int CurSkillID
	{
		get
		{
			return this.curSkillID;
		}
		set
		{
			this.curSkillID = value;
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.careerListPool = base.FindTransform("RankUpChangeUICareerUnitList").GetComponent<ListPool>();
		this.careerListPool.SetItem("RankUpChangeUICareerUnit");
		this.RankUpChangeUIAttrTextTitle0 = base.FindTransform("RankUpChangeUIAttrTextTitle0").GetComponent<Text>();
		this.RankUpChangeUIAttrText0 = base.FindTransform("RankUpChangeUIAttrText0").GetComponent<Text>();
		this.RankUpChangeUIAttrTextTitle1 = base.FindTransform("RankUpChangeUIAttrTextTitle1").GetComponent<Text>();
		this.RankUpChangeUIAttrText1 = base.FindTransform("RankUpChangeUIAttrText1").GetComponent<Text>();
		this.RankUpChangeUIAttrTextTitle2 = base.FindTransform("RankUpChangeUIAttrTextTitle2").GetComponent<Text>();
		this.RankUpChangeUIAttrText2 = base.FindTransform("RankUpChangeUIAttrText2").GetComponent<Text>();
		this.RankUpChangeUISkillFG = base.FindTransform("RankUpChangeUISkillFG").GetComponent<Image>();
		this.RankUpChangeUISkillText = base.FindTransform("RankUpChangeUISkillText").GetComponent<Text>();
		this.RankUpChangeUIStageUnitSR = base.FindTransform("RankUpChangeUIStageUnitSR").GetComponent<ScrollRectCustom>();
		this.stageListPool = base.FindTransform("RankUpChangeUIStageUnitList").GetComponent<ListPool>();
		this.stageListPool.SetItem("RankUpChangeUIStageUnit");
		this.RankUpChangeUIStageUnitLeftArrow = base.FindTransform("RankUpChangeUIStageUnitLeftArrow").get_gameObject();
		this.RankUpChangeUIStageUnitRightArrow = base.FindTransform("RankUpChangeUIStageUnitRightArrow").get_gameObject();
		this.RankUpChangeUIPreviewSlot = base.FindTransform("RankUpChangeUIPreviewSlot");
		this.RankUpChangeUISuccessSign = base.FindTransform("RankUpChangeUISuccessSign").get_gameObject();
		base.FindTransform("RankUpChangeUICloseImage").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCloseBtn);
		this.RankUpChangeUISkillFG.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickIconBtn);
		this.RankUpChangeUIStageUnitSR.OnPageChanged = new Action<int>(this.UpdateStageBtn);
		this.RankUpChangeUIStageUnitLeftArrow.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickStageLeftBtn);
		this.RankUpChangeUIStageUnitRightArrow.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickStageRightBtn);
		if (DataReader<GlobalParams>.Contains("advancedJobText"))
		{
			string value = DataReader<GlobalParams>.Get("advancedJobText").value;
			string[] array = value.Split(new char[]
			{
				';'
			});
			if (array.Length > 2)
			{
				base.FindTransform("RankUpChangeUIAttrTitleText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(int.Parse(array[0]), false));
				base.FindTransform("RankUpChangeUISkillTitleText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(int.Parse(array[1]), false));
				base.FindTransform("RankUpChangeUIStageTitleText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(int.Parse(array[2]), false));
			}
		}
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		EventDispatcher.Broadcast<bool>(RankUpChangeUIEvent.ShowUI, true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		EventDispatcher.Broadcast<bool>(RankUpChangeUIEvent.ShowUI, false);
	}

	public void SetData(int theTypeID, int theDefaultCareer, int theCurState, List<int> completedTask)
	{
		this.TypeID = theTypeID;
		this.DefaultCareer = theDefaultCareer;
		this.DefaultStage = theCurState;
		if (!DataReader<JobIndex>.Contains(theTypeID))
		{
			return;
		}
		JobIndex jobIndex = DataReader<JobIndex>.Get(theTypeID);
		WaitUI.OpenUI(5000u);
		this.InitCareerUnit(jobIndex.AdvancedJobId, delegate
		{
			WaitUI.CloseUI(0u);
			this.OnClickCareerBtn(this.DefauleCareerIndex);
		});
	}

	protected void InitCareerUnit(List<int> careerDataList, Action callback = null)
	{
		this.careerUnitList.Clear();
		this.careerListPool.Create(careerDataList.get_Count(), delegate(int index)
		{
			if (index < careerDataList.get_Count() && index < this.careerListPool.Items.get_Count())
			{
				RankUpChangeUICareerUnit component = this.careerListPool.Items.get_Item(index).GetComponent<RankUpChangeUICareerUnit>();
				if (!DataReader<AdvancedJob>.Contains(careerDataList.get_Item(index)))
				{
					return;
				}
				AdvancedJob advancedJob = DataReader<AdvancedJob>.Get(careerDataList.get_Item(index));
				component.SetData(index, advancedJob.name, new Action<int>(this.OnClickCareerBtn));
				this.careerUnitList.Add(component);
			}
			if (index == careerDataList.get_Count() - 1 && callback != null)
			{
				callback.Invoke();
			}
		});
	}

	protected void SetCareer(int index)
	{
		if (!DataReader<JobIndex>.Contains(this.TypeID))
		{
			return;
		}
		JobIndex jobIndex = DataReader<JobIndex>.Get(this.TypeID);
		int num = (index >= 0) ? ((index <= jobIndex.AdvancedJobId.get_Count() - 1) ? index : (jobIndex.AdvancedJobId.get_Count() - 1)) : 0;
		this.CurCareer = jobIndex.AdvancedJobId.get_Item(num);
		for (int i = 0; i < this.careerUnitList.get_Count(); i++)
		{
			this.careerUnitList.get_Item(i).SetClickState(i == num);
		}
		if (!DataReader<AdvancedJob>.Contains(this.CurCareer))
		{
			return;
		}
		AdvancedJob advancedJob = DataReader<AdvancedJob>.Get(this.CurCareer);
		this.SetPreview(advancedJob.advanced1Model);
		this.SetSuccessSign();
		this.SetAttrText(advancedJob.attrsDelta);
		this.SetSkill(advancedJob.passiveSkill, advancedJob.description);
	}

	protected void SetPreview(int theModelID)
	{
		if (this.rankUpPreviewCell != null && this.rankUpPreviewCell.get_gameObject() != null)
		{
			Object.Destroy(this.rankUpPreviewCell.get_gameObject());
		}
		this.rankUpPreviewCell = RankUpPreviewManager.Instance.GetPreview(this.RankUpChangeUIPreviewSlot);
		this.rankUpPreviewCell.Bind(this);
		this.rankUpPreviewCell.SetData(theModelID);
	}

	protected void SetSuccessSign()
	{
		if (this.IsAllRankUpFinish() || this.CurCareer < this.DefaultCareer)
		{
			if (!this.RankUpChangeUISuccessSign.get_activeSelf())
			{
				this.RankUpChangeUISuccessSign.SetActive(true);
			}
		}
		else if (this.RankUpChangeUISuccessSign.get_activeSelf())
		{
			this.RankUpChangeUISuccessSign.SetActive(false);
		}
	}

	protected void SetAttrText(int attrID)
	{
		if (!DataReader<Attrs>.Contains(attrID))
		{
			return;
		}
		Attrs attrs = DataReader<Attrs>.Get(attrID);
		int num = (attrs.attrs.get_Count() >= attrs.values.get_Count()) ? attrs.values.get_Count() : attrs.attrs.get_Count();
		if (num == 0)
		{
			return;
		}
		XDict<int, long> xDict = new XDict<int, long>();
		for (int i = 0; i < num; i++)
		{
			if (xDict.ContainsKey(attrs.attrs.get_Item(i)))
			{
				XDict<int, long> xDict2;
				XDict<int, long> expr_76 = xDict2 = xDict;
				int key;
				int expr_85 = key = attrs.attrs.get_Item(i);
				long num2 = xDict2[key];
				expr_76[expr_85] = num2 + (long)attrs.values.get_Item(i);
			}
			else
			{
				xDict.Add(attrs.attrs.get_Item(i), (long)attrs.values.get_Item(i));
			}
		}
		if (xDict.Count < 3)
		{
			return;
		}
		this.RankUpChangeUIAttrTextTitle0.set_text(AttrUtility.GetAttrName(xDict.ElementKeyAt(0)));
		this.RankUpChangeUIAttrText0.set_text(AttrUtility.GetAddAttrValueDisplay(xDict.ElementKeyAt(0), xDict.ElementValueAt(0)));
		this.RankUpChangeUIAttrTextTitle1.set_text(AttrUtility.GetAttrName(xDict.ElementKeyAt(1)));
		this.RankUpChangeUIAttrText1.set_text(AttrUtility.GetAddAttrValueDisplay(xDict.ElementKeyAt(1), xDict.ElementValueAt(1)));
		this.RankUpChangeUIAttrTextTitle2.set_text(AttrUtility.GetAttrName(xDict.ElementKeyAt(2)));
		this.RankUpChangeUIAttrText2.set_text(AttrUtility.GetAddAttrValueDisplay(xDict.ElementKeyAt(2), xDict.ElementValueAt(2)));
	}

	protected void SetSkill(int theSkillID, int describeID)
	{
		if (!DataReader<Skill>.Contains(theSkillID))
		{
			return;
		}
		this.CurSkillID = theSkillID;
		ResourceManager.SetSprite(this.RankUpChangeUISkillFG, GameDataUtils.GetIcon(DataReader<Skill>.Get(theSkillID).icon));
		this.RankUpChangeUISkillText.set_text(GameDataUtils.GetChineseContent(describeID, false));
	}

	protected void InitStage(Action callback = null)
	{
		if (!DataReader<AdvancedJob>.Contains(this.CurCareer))
		{
			return;
		}
		AdvancedJob careerData = DataReader<AdvancedJob>.Get(this.CurCareer);
		this.stageUnitList.Clear();
		this.RankUpChangeUIStageUnitSR.OnHasBuilt = delegate
		{
			if (callback != null)
			{
				callback.Invoke();
			}
		};
		this.stageListPool.Create(careerData.stageId.get_Count(), delegate(int index)
		{
			if (index < careerData.stageId.get_Count() && index < this.stageListPool.Items.get_Count())
			{
				RankUpChangeUIStageUnit component = this.stageListPool.Items.get_Item(index).GetComponent<RankUpChangeUIStageUnit>();
				if (!DataReader<StageInfo>.Contains(careerData.stageId.get_Item(index)))
				{
					return;
				}
				StageInfo stageInfo = DataReader<StageInfo>.Get(careerData.stageId.get_Item(index));
				RankUpChangeStageState stageState = this.GetStageState(careerData.stageId.get_Item(index));
				XDict<int, bool> xDict = new XDict<int, bool>();
				int num = (stageInfo.taskid.get_Count() >= stageInfo.description.get_Count()) ? stageInfo.description.get_Count() : stageInfo.taskid.get_Count();
				for (int i = 0; i < num; i++)
				{
					if (xDict.ContainsKey(stageInfo.description.get_Item(i)))
					{
						xDict[stageInfo.description.get_Item(i)] = this.GetIsTaskFinished(stageState, stageInfo.taskid.get_Item(i));
					}
					else
					{
						xDict.Add(stageInfo.description.get_Item(i), this.GetIsTaskFinished(stageState, stageInfo.taskid.get_Item(i)));
					}
				}
				component.SetData(stageInfo.title, xDict, stageState);
				this.stageUnitList.Add(component);
			}
		});
	}

	protected RankUpChangeStageState GetStageState(int stage)
	{
		if (this.IsAllRankUpFinish())
		{
			return RankUpChangeStageState.Done;
		}
		if (this.CurCareer > this.DefaultCareer)
		{
			return RankUpChangeStageState.None;
		}
		if (this.CurCareer < this.defaultCareer)
		{
			return RankUpChangeStageState.Done;
		}
		if (stage > this.DefaultStage)
		{
			return RankUpChangeStageState.None;
		}
		if (stage < this.DefaultStage)
		{
			return RankUpChangeStageState.Done;
		}
		return RankUpChangeStageState.Doing;
	}

	protected bool GetIsTaskFinished(RankUpChangeStageState stageState, int taskID)
	{
		return stageState == RankUpChangeStageState.Done || (stageState != RankUpChangeStageState.None && RankUpChangeManager.Instance.IsTaskFinished(taskID));
	}

	protected bool IsAllRankUpFinish()
	{
		if (!DataReader<JobIndex>.Contains(this.TypeID))
		{
			return false;
		}
		JobIndex jobIndex = DataReader<JobIndex>.Get(this.TypeID);
		if (jobIndex.AdvancedJobId.get_Count() == 0)
		{
			return false;
		}
		if (this.DefaultCareer != jobIndex.AdvancedJobId.get_Item(jobIndex.AdvancedJobId.get_Count() - 1))
		{
			return false;
		}
		if (!DataReader<AdvancedJob>.Contains(this.DefaultCareer))
		{
			return false;
		}
		AdvancedJob advancedJob = DataReader<AdvancedJob>.Get(this.DefaultCareer);
		if (advancedJob.stageId.get_Count() == 0)
		{
			return false;
		}
		if (this.DefaultStage != advancedJob.stageId.get_Item(advancedJob.stageId.get_Count() - 1))
		{
			return false;
		}
		if (!DataReader<StageInfo>.Contains(this.DefaultStage))
		{
			return false;
		}
		StageInfo stageInfo = DataReader<StageInfo>.Get(this.DefaultStage);
		if (stageInfo.taskid.get_Count() == 0)
		{
			return false;
		}
		for (int i = 0; i < stageInfo.taskid.get_Count(); i++)
		{
			if (!RankUpChangeManager.Instance.IsTaskFinished(stageInfo.taskid.get_Item(i)))
			{
				return false;
			}
		}
		return true;
	}

	protected void SetStage(int index)
	{
		this.RankUpChangeUIStageUnitSR.Move2Index(index, true);
		this.UpdateStageBtn();
	}

	protected void OnClickCareerBtn(int index)
	{
		this.SetCareer(index);
		WaitUI.OpenUI(5000u);
		this.InitStage(delegate
		{
			WaitUI.CloseUI(0u);
			this.SetStage((this.CurCareer != this.DefaultCareer) ? 0 : this.DefauleStageIndex);
		});
	}

	protected void OnClickIconBtn(GameObject go)
	{
		SkillDetailUI skillDetailUI = UIManagerControl.Instance.OpenUI("SkillDetailUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as SkillDetailUI;
		if (skillDetailUI)
		{
			skillDetailUI.SetData(this.CurSkillID);
		}
	}

	protected void OnClickStageLeftBtn(GameObject go)
	{
		this.RankUpChangeUIStageUnitSR.Move2Previous();
		this.UpdateStageBtn();
	}

	protected void OnClickStageRightBtn(GameObject go)
	{
		this.RankUpChangeUIStageUnitSR.Move2Next();
		this.UpdateStageBtn();
	}

	protected void UpdateStageBtn(int currentIndex)
	{
		this.UpdateStageBtn();
	}

	protected void UpdateStageBtn()
	{
		if (this.RankUpChangeUIStageUnitSR.CurrentPageIndex <= 0)
		{
			if (this.RankUpChangeUIStageUnitLeftArrow.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitLeftArrow.SetActive(false);
			}
		}
		else if (!this.RankUpChangeUIStageUnitLeftArrow.get_activeSelf())
		{
			this.RankUpChangeUIStageUnitLeftArrow.SetActive(true);
		}
		if (this.RankUpChangeUIStageUnitSR.CurrentPageIndex >= this.RankUpChangeUIStageUnitSR.GetPageNum() - 1)
		{
			if (this.RankUpChangeUIStageUnitRightArrow.get_activeSelf())
			{
				this.RankUpChangeUIStageUnitRightArrow.SetActive(false);
			}
		}
		else if (!this.RankUpChangeUIStageUnitRightArrow.get_activeSelf())
		{
			this.RankUpChangeUIStageUnitRightArrow.SetActive(true);
		}
	}

	private void OnApplicationPause(bool isPause)
	{
		if (this.rankUpPreviewCell)
		{
			this.rankUpPreviewCell.DoOnApplicationPause();
		}
		TimerHeap.AddTimer(2000u, 0, delegate
		{
			if (this != null && base.get_gameObject() != null && base.get_gameObject().get_activeInHierarchy() && this.rankUpPreviewCell)
			{
				this.rankUpPreviewCell.DoOnApplicationPause();
			}
		});
	}
}
