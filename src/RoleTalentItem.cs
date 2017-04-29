using GameData;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoleTalentItem : MonoBehaviour
{
	public int m_id;

	public int m_row;

	public int m_column;

	private GameObject m_goSelected;

	private Image m_spIcon;

	private Text m_lblLevel;

	private GameObject m_goArrowL;

	private GameObject m_goArrowR;

	private GameObject m_goArrowB;

	private GameObject m_goArrowRB;

	private int m_fx_selected;

	private int m_fx_activation;

	private int m_fx_levelup;

	private int m_fx_reset;

	private void Awake()
	{
		this.m_goSelected = base.get_transform().FindChild("Button").FindChild("Selected").get_gameObject();
		this.m_spIcon = base.get_transform().FindChild("Button").FindChild("Icon").GetComponent<Image>();
		this.m_lblLevel = base.get_transform().FindChild("Level").GetComponent<Text>();
		this.m_goArrowL = base.get_transform().FindChild("ArrowL").get_gameObject();
		this.m_goArrowR = base.get_transform().FindChild("ArrowR").get_gameObject();
		this.m_goArrowB = base.get_transform().FindChild("ArrowB").get_gameObject();
		this.m_goArrowRB = base.get_transform().FindChild("ArrowRB").get_gameObject();
		base.get_transform().FindChild("Button").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickButton));
	}

	private void OnDisable()
	{
		this.DeleteSpineOfSelected();
		this.DeleteSpineOfActivation();
		this.DeleteSpineOfLevelUp();
		this.DeleteSpineOfReset();
	}

	private void OnClickButton()
	{
		RoleTalentUIView.Instance.RefreshTalentInfo(this.m_id);
	}

	public void SetInfo(Talent dataTalent)
	{
		this.HideAllArrow();
		this.SetSelected(false);
		this.m_id = dataTalent.id;
		this.SetIcon(dataTalent.icon, RoleTalentManager.Instance.IsActivation(dataTalent.id));
	}

	public void SetSelected(bool isShow)
	{
		this.m_goSelected.SetActive(isShow);
		if (isShow)
		{
			this.AddSpineOfSelected();
		}
		else
		{
			this.DeleteSpineOfSelected();
		}
	}

	public void SetArrow(int next_row, int next_column)
	{
		if (this.m_row == next_row)
		{
			if (this.m_column < next_column)
			{
				this.SetArrowR(true);
			}
			else
			{
				this.SetArrowL(true);
			}
		}
		else if (this.m_row < next_row)
		{
			if (this.m_column < next_column)
			{
				this.SetArrowRB(true);
			}
			else if (this.m_column == next_column)
			{
				this.SetArrowB(true);
			}
		}
	}

	public void SetLevel(int level, int maxlevel)
	{
		this.m_lblLevel.set_text(level + "/" + maxlevel);
	}

	private void SetIcon(int id, bool isActivation)
	{
		ResourceManager.SetSprite(this.m_spIcon, GameDataUtils.GetIcon(id));
		ImageColorMgr.SetImageColor(this.m_spIcon, !isActivation);
	}

	private void HideAllArrow()
	{
		this.SetArrowL(false);
		this.SetArrowR(false);
		this.SetArrowB(false);
		this.SetArrowRB(false);
	}

	private void SetArrowL(bool isShow)
	{
		this.m_goArrowL.SetActive(isShow);
	}

	private void SetArrowR(bool isShow)
	{
		this.m_goArrowR.SetActive(isShow);
	}

	private void SetArrowB(bool isShow)
	{
		this.m_goArrowB.SetActive(isShow);
	}

	private void SetArrowRB(bool isShow)
	{
		this.m_goArrowRB.SetActive(isShow);
	}

	private void AddSpineOfSelected()
	{
		this.m_fx_selected = FXSpineManager.Instance.ReplaySpine(this.m_fx_selected, 3021, base.get_transform(), "RoleTalentUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void DeleteSpineOfSelected()
	{
		FXSpineManager.Instance.DeleteSpine(this.m_fx_selected, true);
	}

	public void AddSpineOfActivation()
	{
		this.m_fx_activation = FXSpineManager.Instance.ReplaySpine(this.m_fx_activation, 3022, base.get_transform(), "RoleTalentUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void DeleteSpineOfActivation()
	{
		FXSpineManager.Instance.DeleteSpine(this.m_fx_activation, true);
	}

	public void AddSpineOfLevelUp()
	{
		this.m_fx_levelup = FXSpineManager.Instance.ReplaySpine(this.m_fx_levelup, 3023, base.get_transform(), "RoleTalentUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void DeleteSpineOfLevelUp()
	{
		FXSpineManager.Instance.DeleteSpine(this.m_fx_levelup, true);
	}

	public void AddSpineOfReset()
	{
		this.m_fx_reset = FXSpineManager.Instance.ReplaySpine(this.m_fx_reset, 3024, base.get_transform(), "RoleTalentUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void DeleteSpineOfReset()
	{
		FXSpineManager.Instance.DeleteSpine(this.m_fx_reset, true);
	}
}
