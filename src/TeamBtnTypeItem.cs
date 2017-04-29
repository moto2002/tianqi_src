using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamBtnTypeItem : BaseUIBehaviour
{
	private bool isInit;

	private Transform teamFirstBtn;

	private List<Transform> teamSecondBtnList;

	private DuiWuMuBiao m_teamTargetCfg;

	private int dungeonType;

	private List<int> challegeIDList;

	private TeamSecondBtnTypeItem lastSecondBtnTypeItem;

	private bool selected;

	private bool isShowSecond;

	private Color32 COLOR_LIGHT = new Color32(255, 250, 230, 255);

	private Color32 COLOR_LIGHT_NO = new Color32(255, 215, 140, 255);

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
				this.SetBtnLightAndDim("fenleianniu_1", true);
			}
			else
			{
				this.SetBtnLightAndDim("fenleianniu_2", false);
			}
		}
	}

	public bool IsShowSecond
	{
		get
		{
			return this.isShowSecond;
		}
		set
		{
			this.isShowSecond = value;
			if (this.isShowSecond)
			{
				this.SetSecondTypeBtns();
			}
			else
			{
				this.HideAllSecondTypeBtns();
			}
		}
	}

	public Transform TeamFirstBtn
	{
		get
		{
			return this.teamFirstBtn;
		}
	}

	public int DungeonType
	{
		get
		{
			return this.dungeonType;
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
		this.teamFirstBtn = base.FindTransform("TeamFirstBtn");
		this.teamSecondBtnList = new List<Transform>();
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
		DuiWuMuBiao duiWuMuBiao = DataReader<DuiWuMuBiao>.Get(id);
		this.m_teamTargetCfg = duiWuMuBiao;
		this.dungeonType = duiWuMuBiao.Type;
		this.teamFirstBtn.FindChild("btnText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(duiWuMuBiao.Word, false));
		this.SetSecondTypeBtns();
		this.Selected = false;
		this.IsShowSecond = false;
	}

	public bool SetSelectTeamTargetID(int teamTargetCfgID)
	{
		if (this.m_teamTargetCfg != null && DataReader<DuiWuMuBiao>.Contains(teamTargetCfgID))
		{
			DuiWuMuBiao duiWuMuBiao = DataReader<DuiWuMuBiao>.Get(teamTargetCfgID);
			if (duiWuMuBiao.Type == this.m_teamTargetCfg.Type && duiWuMuBiao.Group == this.m_teamTargetCfg.Group)
			{
				for (int i = 0; i < this.teamSecondBtnList.get_Count(); i++)
				{
					TeamSecondBtnTypeItem component = this.teamSecondBtnList.get_Item(i).GetComponent<TeamSecondBtnTypeItem>();
					if (component != null && component.TeamTargetCfgID == teamTargetCfgID)
					{
						this.OnClickSecondBtn(this.teamSecondBtnList.get_Item(i).get_gameObject());
					}
				}
				return true;
			}
		}
		return false;
	}

	private void SetBtnLightAndDim(string btnIcon, bool isLight)
	{
		if (this.teamFirstBtn != null)
		{
			ResourceManager.SetSprite(this.teamFirstBtn.FindChild("btnImg").GetComponent<Image>(), ResourceManager.GetIconSprite(btnIcon));
			this.teamFirstBtn.get_transform().FindChild("btnText").GetComponent<Text>().set_color((!isLight) ? this.COLOR_LIGHT_NO : this.COLOR_LIGHT);
		}
	}

	private void SetSecondTypeBtns()
	{
		if (this.m_teamTargetCfg != null)
		{
			List<DuiWuMuBiao> list = DataReader<DuiWuMuBiao>.DataList.FindAll((DuiWuMuBiao a) => a.Type == this.m_teamTargetCfg.Type && a.label != 1 && this.m_teamTargetCfg.Group == a.Group);
			List<DuiWuMuBiao> list2 = new List<DuiWuMuBiao>();
			if (list != null && list.get_Count() > 0)
			{
				for (int i = 0; i < list.get_Count(); i++)
				{
					DuiWuMuBiao duiWuMuBiao = list.get_Item(i);
					if (duiWuMuBiao != null && duiWuMuBiao.Lv <= EntityWorld.Instance.EntSelf.Lv)
					{
						list2.Add(duiWuMuBiao);
					}
				}
			}
			int j = 0;
			if (list2 != null && list2.get_Count() > 0)
			{
				while (j < list2.get_Count())
				{
					DuiWuMuBiao duiWuMuBiao2 = list2.get_Item(j);
					if (j < this.teamSecondBtnList.get_Count())
					{
						Transform transform = this.teamSecondBtnList.get_Item(j);
						transform.get_gameObject().SetActive(true);
						transform.GetComponent<TeamSecondBtnTypeItem>().UpdateUI(duiWuMuBiao2.Id);
					}
					else
					{
						GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("TeamSecondTypeBtn");
						instantiate2Prefab.set_name("TeamSecondTypeBtn" + duiWuMuBiao2.Id);
						instantiate2Prefab.get_transform().SetParent(base.get_transform());
						instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
						this.teamSecondBtnList.Add(instantiate2Prefab.get_transform());
						TeamSecondBtnTypeItem component = instantiate2Prefab.GetComponent<TeamSecondBtnTypeItem>();
						component.UpdateUI(duiWuMuBiao2.Id);
						component.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSecondBtn);
						if (j == 0)
						{
							this.lastSecondBtnTypeItem = component;
							component.Selected = true;
							this.DungeonParams = this.lastSecondBtnTypeItem.DungeonParams;
						}
						else
						{
							component.Selected = false;
						}
					}
					j++;
				}
			}
			for (int k = j; k < this.teamSecondBtnList.get_Count(); k++)
			{
				GameObject gameObject = this.teamSecondBtnList.get_Item(k).get_gameObject();
				if (gameObject != null && gameObject.get_activeSelf())
				{
					gameObject.SetActive(false);
				}
			}
		}
	}

	private void HideAllSecondTypeBtns()
	{
		for (int i = 0; i < this.teamSecondBtnList.get_Count(); i++)
		{
			Transform transform = this.teamSecondBtnList.get_Item(i);
			if (transform != null && transform.get_gameObject().get_activeSelf())
			{
				transform.get_gameObject().SetActive(false);
			}
		}
	}

	private void OnClickSecondBtn(GameObject go)
	{
		TeamSecondBtnTypeItem component = go.GetComponent<TeamSecondBtnTypeItem>();
		if (this.lastSecondBtnTypeItem != null)
		{
			this.lastSecondBtnTypeItem.Selected = false;
		}
		if (component != null)
		{
			component.Selected = true;
			this.lastSecondBtnTypeItem = component;
			this.DungeonParams = this.lastSecondBtnTypeItem.DungeonParams;
		}
		EventDispatcher.Broadcast<TeamSecondBtnTypeItem>(EventNames.OnClickTeamTargetSecondBtn, component);
	}
}
