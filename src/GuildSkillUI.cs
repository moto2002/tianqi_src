using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildSkillUI : UIBase
{
	protected const int Display2Level1LanguageID = 14;

	public static GuildSkillUI Instance;

	private GridLayoutGroup m_skilllist;

	private Text m_nowLv;

	private Text m_nextLv;

	private Text m_nowAttrs;

	private Text m_nextAttrs;

	private Text m_cost;

	private Text m_Name;

	private Text m_attrsName;

	private int CurrentSkillId;

	private Text m_attribution;

	private GameObject m_upBtn;

	private GameObject m_msg;

	private GameObject m_msgMax;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		GuildSkillUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.FindTransform("BtnUp").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickUp);
		base.FindTransform("CloseBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickClose);
		this.m_upBtn = base.FindTransform("BtnUp").get_gameObject();
		this.m_msg = base.FindTransform("TextMsg").get_gameObject();
		this.m_msgMax = base.FindTransform("TextMsgMax").get_gameObject();
		this.m_skilllist = base.FindTransform("AttListSR").FindChild("AttList").GetComponent<GridLayoutGroup>();
		this.m_nowLv = base.FindTransform("TextNowLvValue").GetComponent<Text>();
		this.m_nextLv = base.FindTransform("TextNextLvValue").GetComponent<Text>();
		this.m_nowAttrs = base.FindTransform("TextNowAttrsValue").GetComponent<Text>();
		this.m_nextAttrs = base.FindTransform("TextNextAttrsValue").GetComponent<Text>();
		this.m_cost = base.FindTransform("BtnUp").FindChild("TextCost").GetComponent<Text>();
		this.m_attribution = base.FindTransform("TextAttribution").GetComponent<Text>();
		this.m_Name = base.FindTransform("TextStr").GetComponent<Text>();
		this.m_attrsName = base.FindTransform("TextAttrsName").GetComponent<Text>();
	}

	protected override void InitUI()
	{
		base.InitUI();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		GuildSkillManager.Instance.SendGetGuildSkillReq();
		this.m_attribution.set_text(GuildManager.Instance.MyMemberInfo.contribution.ToString());
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.OnClickClose(null);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			GuildSkillUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGuildBuildRes, new Callback(this.UpdateAttribution));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGuildBuildRes, new Callback(this.UpdateAttribution));
	}

	private void OnClickUp(GameObject go)
	{
		if (this.CurrentSkillId > 0)
		{
			GuildSkillManager.Instance.SendUpGuildSkillReq(this.CurrentSkillId);
		}
	}

	private void OnClickClose(GameObject go)
	{
		this.Show(false);
	}

	public void RefreshUI()
	{
		this.ClearScroll();
		GetGuildSkillRes skillData = GuildSkillManager.Instance.GetSkillData();
		if (skillData.skillInfo != null)
		{
			for (int i = 0; i < skillData.skillInfo.get_Count(); i++)
			{
				this.AddScrollCell(i, skillData.skillInfo.get_Item(i).skillId, skillData.skillInfo.get_Item(i).skillLv);
				if (i == 0)
				{
					this.UpdateInfoPanel(skillData.skillInfo.get_Item(i).skillId, skillData.skillInfo.get_Item(i).skillLv);
				}
			}
		}
		this.m_attribution.set_text(GuildManager.Instance.MyMemberInfo.contribution.ToString());
	}

	public void RefreshUIById(int skillId, int skilllv)
	{
		for (int i = 0; i < this.m_skilllist.get_transform().get_childCount(); i++)
		{
			if (this.m_skilllist.get_transform().GetChild(i).get_name() == skillId.ToString())
			{
				this.m_skilllist.get_transform().GetChild(i).get_transform().FindChild("ImageBG").get_transform().FindChild("Lv").GetComponent<Text>().set_text(string.Format("Lv {0}", skilllv.ToString()));
				break;
			}
		}
		this.UpdateInfoPanel(skillId, skilllv);
		this.m_attribution.set_text(GuildManager.Instance.MyMemberInfo.contribution.ToString());
	}

	private void ClearScroll()
	{
		for (int i = 0; i < this.m_skilllist.get_transform().get_childCount(); i++)
		{
			this.m_skilllist.get_transform().GetChild(i).get_gameObject().SetActive(false);
		}
	}

	private void OnClickSkill(GameObject go)
	{
		this.ResetSelectState();
		go.get_transform().get_parent().get_transform().FindChild("ImageSelect").get_gameObject().SetActive(true);
		int num = int.Parse(go.get_transform().get_parent().get_name());
		GetGuildSkillRes skillData = GuildSkillManager.Instance.GetSkillData();
		if (skillData.skillInfo != null)
		{
			for (int i = 0; i < skillData.skillInfo.get_Count(); i++)
			{
				if (skillData.skillInfo.get_Item(i).skillId == num)
				{
					this.UpdateInfoPanel(num, skillData.skillInfo.get_Item(i).skillLv);
					break;
				}
			}
		}
	}

	private void AddScrollCell(int index, int skillId, int skillLv)
	{
		Transform transform = this.m_skilllist.get_transform().FindChild(skillId.ToString());
		if (transform != null)
		{
			transform.get_gameObject().SetActive(true);
			transform.get_transform().FindChild("ImageBG").get_transform().FindChild("Lv").GetComponent<Text>().set_text(string.Format("Lv {0}", skillLv.ToString()));
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GuildAttrsItem");
			instantiate2Prefab.get_transform().SetParent(this.m_skilllist.get_transform(), false);
			instantiate2Prefab.set_name(skillId.ToString());
			instantiate2Prefab.get_gameObject().SetActive(true);
			instantiate2Prefab.get_transform().FindChild("ImageBG").get_transform().FindChild("Lv").GetComponent<Text>().set_text(string.Format("Lv {0}", skillLv.ToString()));
			LegionSkill legionSkill = DataReader<LegionSkill>.Get(skillId);
			string chineseContent = GameDataUtils.GetChineseContent(legionSkill.name, false);
			instantiate2Prefab.get_transform().FindChild("ImageBG").get_transform().FindChild("Name").GetComponent<Text>().set_text(chineseContent);
			instantiate2Prefab.get_transform().FindChild("ImageRect").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSkill);
			ResourceManager.SetSprite(instantiate2Prefab.get_transform().FindChild("ImageIcon").GetComponent<Image>(), GameDataUtils.GetIcon(legionSkill.icon));
			if (index == 0)
			{
				instantiate2Prefab.get_transform().FindChild("ImageSelect").get_gameObject().SetActive(true);
			}
		}
	}

	private string NumChange(int types, int values)
	{
		if (types != 0)
		{
			return Math.Round((double)values / 10.0, 1).ToString("F1") + "%";
		}
		long num = 0L;
		if ((double)values < 10000.0)
		{
			return values.ToString();
		}
		double num2 = (double)values / 10000.0;
		if (long.TryParse(num2.ToString(), ref num))
		{
			return string.Format(GameDataUtils.GetChineseContent(14, false), num.ToString());
		}
		return string.Format(GameDataUtils.GetChineseContent(14, false), Math.Round(num2, 2).ToString("F2"));
	}

	private void UpdateInfoPanel(int skillId = 0, int skilllv = 0)
	{
		List<LegionSkill> dataList = DataReader<LegionSkill>.DataList;
		if (dataList == null)
		{
			return;
		}
		this.CurrentSkillId = skillId;
		string text = "0";
		string text2 = "Max";
		string text3 = "Max";
		string text4 = string.Empty;
		int num = 0;
		bool flag = true;
		int num2 = skilllv + 1;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i) != null && dataList.get_Item(i).level == skilllv && dataList.get_Item(i).id == skillId)
			{
				int attrs = dataList.get_Item(i).attrs;
				Attrs attrs2 = DataReader<Attrs>.Get(attrs);
				if (attrs2 != null && attrs2.values != null)
				{
					text = this.NumChange(dataList.get_Item(i).percentage, attrs2.values.get_Item(0));
					text4 = GameDataUtils.GetChineseContent(attrs2.attrs.get_Item(0), false);
				}
			}
			else if (dataList.get_Item(i) != null && dataList.get_Item(i).level == num2 && dataList.get_Item(i).id == skillId)
			{
				int attrs3 = dataList.get_Item(i).attrs;
				Attrs attrs4 = DataReader<Attrs>.Get(attrs3);
				if (attrs4 != null && attrs4.values != null)
				{
					text2 = this.NumChange(dataList.get_Item(i).percentage, attrs4.values.get_Item(0));
					text3 = num2.ToString();
					flag = false;
					text4 = GameDataUtils.GetChineseContent(attrs4.attrs.get_Item(0), false);
					num = dataList.get_Item(i).cost;
				}
			}
		}
		this.m_Name.set_text(text4);
		this.m_attrsName.set_text("+" + text);
		this.m_nowLv.set_text("Lv." + skilllv.ToString());
		this.m_nextAttrs.set_text("+" + text2);
		this.m_nextLv.set_text("Lv." + text3);
		this.m_cost.set_text("x" + num.ToString());
		this.m_msg.SetActive(false);
		if (!flag)
		{
			string value = DataReader<GongHuiXinXi>.Get("SkillLevelLimited").value;
			string[] array = value.Split(new char[]
			{
				';'
			});
			for (int j = 0; j < array.Length; j++)
			{
				string text5 = array[j];
				string[] array2 = text5.Split(new char[]
				{
					':'
				});
				int num3 = int.Parse(array2[0]);
				int num4 = int.Parse(array2[1]);
				if (GuildManager.Instance.MyGuildnfo.lv == num3 && skilllv >= num4)
				{
					this.m_upBtn.SetActive(false);
					this.m_msgMax.SetActive(false);
					this.m_msg.SetActive(true);
					return;
				}
			}
		}
		this.m_upBtn.SetActive(!flag);
		this.m_msgMax.SetActive(flag);
	}

	private void ResetSelectState()
	{
		for (int i = 0; i < this.m_skilllist.get_transform().get_childCount(); i++)
		{
			Transform child = this.m_skilllist.get_transform().GetChild(i);
			child.get_transform().FindChild("ImageSelect").get_gameObject().SetActive(false);
		}
	}

	private void UpdateAttribution()
	{
		this.m_attribution.set_text(GuildManager.Instance.MyMemberInfo.contribution.ToString());
	}
}
