using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamTargetItem : BaseUIBehaviour
{
	private bool isInit;

	private int dungeonType;

	private List<int> challegeIDList;

	private DuiWuMuBiao teamTargetCfg;

	private GameObject selectImgObj;

	private Text targetText;

	private bool selected;

	public bool Selected
	{
		get
		{
			return this.selected;
		}
		set
		{
			this.selected = value;
			if (this.selectImgObj.get_activeSelf() != this.selected)
			{
				this.selectImgObj.SetActive(this.selected);
			}
		}
	}

	public DuiWuMuBiao TeamTargetCfgData
	{
		get
		{
			return this.teamTargetCfg;
		}
		set
		{
			this.teamTargetCfg = value;
		}
	}

	public int DungeonType
	{
		get
		{
			return this.dungeonType;
		}
		set
		{
			this.dungeonType = value;
		}
	}

	public List<int> DungeonParams
	{
		get
		{
			if (this.challegeIDList == null)
			{
				this.challegeIDList = new List<int>();
			}
			return this.challegeIDList;
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.targetText = base.FindTransform("TargetText").GetComponent<Text>();
		this.selectImgObj = base.FindTransform("SelectImg").get_gameObject();
		this.isInit = true;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	public void UpdateUI(DuiWuMuBiao targetCfg)
	{
		if (targetCfg == null)
		{
			return;
		}
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.TeamTargetCfgData = targetCfg;
		this.dungeonType = this.teamTargetCfg.Type;
		this.challegeIDList = this.teamTargetCfg.FuBen;
		this.targetText.set_text(GameDataUtils.GetChineseContent(this.teamTargetCfg.Set, false));
		this.Selected = false;
	}
}
