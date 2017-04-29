using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EliteDifficultItem : BaseUIBehaviour
{
	private Text btnText;

	private Image btnImg;

	private Image btnSelectImg;

	private Color selectColor;

	private Color notSelectColor;

	private bool isInit;

	public int EliteCfgID;

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
			this.SetBtnLightAndDim(this.selected);
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
		this.btnSelectImg = base.FindTransform("btnSelectImg").GetComponent<Image>();
		this.btnText = base.FindTransform("btnText").GetComponent<Text>();
		this.selectColor = new Color(0.4f, 0.156862751f, 0.03137255f);
		this.notSelectColor = new Color(0.2509804f, 0.113725491f, 0.05490196f);
		this.isInit = true;
	}

	private void SetBtnLightAndDim(bool isSelect)
	{
		if (this.btnSelectImg != null)
		{
			this.btnSelectImg.get_gameObject().SetActive(isSelect);
		}
		if (this.btnText != null)
		{
			this.btnText.set_color((!isSelect) ? this.notSelectColor : this.selectColor);
		}
	}

	public void RefreshUI(int eliteCfgID)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		if (DataReader<JingYingFuBenPeiZhi>.Contains(eliteCfgID))
		{
			this.EliteCfgID = eliteCfgID;
			JingYingFuBenPeiZhi jingYingFuBenPeiZhi = DataReader<JingYingFuBenPeiZhi>.Get(eliteCfgID);
			int id = 505093 + jingYingFuBenPeiZhi.rank;
			this.btnText.set_text(GameDataUtils.GetChineseContent(id, false));
			bool flag = EliteDungeonManager.Instance.CheckCopyIsOpen(eliteCfgID);
			ImageColorMgr.SetImageColor(this.btnImg, !flag);
			this.Selected = false;
		}
	}
}
