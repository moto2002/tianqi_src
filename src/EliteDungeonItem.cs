using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EliteDungeonItem : BaseUIBehaviour
{
	private EliteDataInfo dataInfo;

	private Image bossIcon;

	private GameObject closeMask;

	private int challengeLv;

	private Image stepImg;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.bossIcon = base.FindTransform("BossIcon").GetComponent<Image>();
		base.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenDetail);
		this.closeMask = base.FindTransform("CloseMask").get_gameObject();
		this.stepImg = base.FindTransform("stepImg").GetComponent<Image>();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	public void RefreshUI(EliteDataInfo eliteDataInfo)
	{
		this.dataInfo = eliteDataInfo;
		Icon icon = DataReader<Icon>.Get(this.dataInfo.BossIconID);
		if (icon != null)
		{
			ResourceManager.SetSprite(this.bossIcon, ResourceManager.GetIconSprite(icon.icon));
		}
		base.FindTransform("bossName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(this.dataInfo.BossID, false));
		int key = this.dataInfo.cfgIDList.get_Item(0);
		JingYingFuBenPeiZhi jingYingFuBenPeiZhi = DataReader<JingYingFuBenPeiZhi>.Get(key);
		if (jingYingFuBenPeiZhi != null)
		{
			this.challengeLv = jingYingFuBenPeiZhi.level;
			ResourceManager.SetSprite(this.stepImg, ResourceManager.GetIconSprite("duanshu_" + jingYingFuBenPeiZhi.step));
		}
		bool active = !eliteDataInfo.isOpen || EntityWorld.Instance.EntSelf.Lv < this.challengeLv;
		this.closeMask.SetActive(active);
		this.closeMask.get_transform().FindChild("NotOpenText1").GetComponent<Text>().set_text(string.Concat(new object[]
		{
			GameDataUtils.GetChineseContent(506011, false),
			GameDataUtils.GetChineseContent(508009, false),
			GameDataUtils.GetChineseContent(510022, false),
			"\nLV：",
			this.challengeLv
		}));
	}

	private void OnClickOpenDetail(GameObject go)
	{
		if (EntityWorld.Instance.EntSelf.Lv < this.challengeLv)
		{
			UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(505029, false), this.challengeLv));
			return;
		}
		if (this.dataInfo != null && this.dataInfo.isOpen)
		{
			WaitUI.OpenUI(0u);
			EliteInstanceDetailUI.IsOpenFromStack = false;
			EliteInstanceDetailUI eliteInstanceDetailUI = UIManagerControl.Instance.OpenUI("EliteInstanceDetailUI", UINodesManager.NormalUIRoot, false, UIType.Pop) as EliteInstanceDetailUI;
			eliteInstanceDetailUI.OnRefreshDifficultyList(this.dataInfo);
		}
		else if (this.dataInfo != null && !this.dataInfo.isOpen)
		{
			if (this.dataInfo.TaskID > 0 && !MainTaskManager.Instance.IsFinishedTask(this.dataInfo.TaskID))
			{
				RenWuPeiZhi renWuPeiZhi = DataReader<RenWuPeiZhi>.Get(this.dataInfo.TaskID);
				if (renWuPeiZhi != null)
				{
					UIManagerControl.Instance.ShowToastText(string.Format("系统未开放, [{0}]任务未完成", GameDataUtils.GetChineseContent(renWuPeiZhi.dramaIntroduce, false)));
				}
				else
				{
					UIManagerControl.Instance.ShowToastText("系统未开放, 依赖任务未完成");
				}
			}
		}
		else
		{
			UIManagerControl.Instance.ShowToastText("找不到精英副本数据");
		}
	}
}
