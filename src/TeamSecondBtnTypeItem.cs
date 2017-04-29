using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamSecondBtnTypeItem : BaseUIBehaviour
{
	private bool isInit;

	private int dungeonType;

	private List<int> challegeIDList;

	private Text btnText;

	private Image btnImg;

	private Color selectColor;

	private Color notSelectColor;

	public int TeamTargetCfgID;

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
			if (this.selected)
			{
				this.SetBtnLightAndDim(true);
			}
			else
			{
				this.SetBtnLightAndDim(false);
			}
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
		set
		{
			this.challegeIDList = value;
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.btnImg = base.FindTransform("btnImg").GetComponent<Image>();
		this.btnText = base.FindTransform("btnText").GetComponent<Text>();
		this.notSelectColor = new Color(0.470588237f, 0.3137255f, 0.235294119f);
		this.selectColor = new Color(1f, 0.980392158f, 0.9019608f);
		this.isInit = true;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	public void UpdateUI(int id)
	{
		if (!DataReader<DuiWuMuBiao>.Contains(id))
		{
			return;
		}
		this.TeamTargetCfgID = id;
		DuiWuMuBiao duiWuMuBiao = DataReader<DuiWuMuBiao>.Get(id);
		this.dungeonType = duiWuMuBiao.Type;
		this.challegeIDList = duiWuMuBiao.FuBen;
		this.btnText.set_text(GameDataUtils.GetChineseContent(duiWuMuBiao.Word, false));
	}

	private void SetBtnLightAndDim(bool isLight)
	{
		if (base.get_transform() != null)
		{
			this.btnText.set_color((!isLight) ? this.notSelectColor : this.selectColor);
			float num = (!isLight) ? 0.5f : 1f;
			this.btnImg.set_color(new Color(this.btnImg.get_color().r, this.btnImg.get_color().b, this.btnImg.get_color().g, num));
		}
	}
}
