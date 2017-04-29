using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadInfoUnit : BaseUIBehaviour
{
	private const int NUM_ARGs = 5;

	private const string BloodBar_Red = "xiaoguaixuetiao2";

	private const string BloodBar_Green = "cwxuetiao";

	private const string BloodBar_Bg = "xiaoguaixuetiao1";

	private const string BloodBar2_Red01 = "fight_bloodframe_1";

	private const string BloodBar2_Red02 = "fight_bloodframe_3";

	private const string BloodBar2_Green01 = "fight_greenframe_1";

	private const string BloodBar2_Green02 = "fight_greenframe_3";

	private const string BloodBar2_Highlight = "fight_bloodframe_2";

	private const float InfoPosY1 = 2f;

	private const float InfoPosY2 = 30f;

	private const float HEIGHT_NAME = 26f;

	private const float HEIGHT_GUILDTITLE = 26f;

	private const float HEIGHT_TITLE = 28f;

	private const int BG_BIG_X = 50;

	private const int BG_SIZE_HEIGHT_TITLE = 36;

	private const int BG_SIZE_HEIGHT_GUILDTITLE = 30;

	public long uuid;

	private int _actorType = 1;

	private Text m_lblName;

	private Image m_spNameBG;

	private Text m_lblTitle;

	private Image m_spTitleBG;

	private Text m_lblGuildTitle;

	private Image m_spGuildTitleBG;

	private Image m_spTitleIcon;

	private Image m_spCommonIcon;

	private int bloodBarType;

	private Image m_spBloodBar1Bg;

	private Image m_spBloodBar1Fg;

	private Image m_spBloodBar2Bg1;

	private Image m_spBloodBar2Bg2;

	private Image m_spBloodBar2Fg;

	private RectTransform m_nodeInfo;

	private float height_dynamic_name = 26f;

	private float height_dynamic_guildtitle;

	public int actorType
	{
		get
		{
			return this._actorType;
		}
		set
		{
			this._actorType = value;
			this.ShowByScene(MySceneManager.Instance.CurSceneID);
			if (this._actorType == 33 || this._actorType == 42)
			{
				HeadInfoManager.Instance.SetBloodBarType(this.uuid, 12);
			}
			else if (this._actorType == 5 || this._actorType == 41 || this._actorType == 32)
			{
				HeadInfoManager.Instance.SetBloodBarType(this.uuid, 11);
			}
			else if (this.actorType == 6 || this.actorType == 61)
			{
				HeadInfoManager.Instance.SetBloodBarType(this.uuid, 1);
			}
			else
			{
				HeadInfoManager.Instance.SetBloodBarType(this.uuid, 2);
			}
		}
	}

	public void RefreshAll()
	{
		HeadInfoManager.HeadInfoData data = HeadInfoManager.Instance.GetData(this.uuid);
		if (data == null)
		{
			return;
		}
		this.ShowName(data.Show(data.isShowName));
		this.SetAndShowTitle(data.Show(data.isShowTitle), data.titleId);
		this.SetAndShowGuildTitle(data.Show(data.isShowGuildTitle), data.guildTitle);
		this.SetAndShowCommonIcon(data.Show(data.isShowCommonIcon), data.commonIcon);
		this.SetAndShowBloodBar(data.ShowBloodBar(false), data.bloodBarType);
		this.SetName(data.name);
		this.SetBloodBar(data.bloodFillAmount);
		this.SetBloodBarSize(data.bloodBarSize);
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_lblName = base.FindTransform("Name").GetComponent<Text>();
		this.m_spNameBG = base.FindTransform("NameBG").GetComponent<Image>();
		this.m_spNameBG.get_rectTransform().set_sizeDelta(Vector2.get_zero());
		this.m_lblTitle = base.FindTransform("Title").GetComponent<Text>();
		this.m_spTitleBG = base.FindTransform("TitleBG").GetComponent<Image>();
		this.m_lblGuildTitle = base.FindTransform("GuildTitle").GetComponent<Text>();
		this.m_spGuildTitleBG = base.FindTransform("GuildTitleBG").GetComponent<Image>();
		this.m_spTitleIcon = base.FindTransform("TitleIcon").GetComponent<Image>();
		this.m_spCommonIcon = base.FindTransform("ComIcon").GetComponent<Image>();
		this.m_spBloodBar1Bg = base.FindTransform("BloodBar1Bg").GetComponent<Image>();
		this.m_spBloodBar1Fg = base.FindTransform("BloodBar1Fg").GetComponent<Image>();
		this.m_spBloodBar2Bg1 = base.FindTransform("BloodBar2Bg1").GetComponent<Image>();
		this.m_spBloodBar2Bg2 = base.FindTransform("BloodBar2Bg2").GetComponent<Image>();
		this.m_spBloodBar2Fg = base.FindTransform("BloodBar2Fg").GetComponent<Image>();
		this.m_nodeInfo = (base.FindTransform("Info") as RectTransform);
	}

	private void OnEnable()
	{
		if (this.m_myTransform != null)
		{
			this.m_myTransform.get_gameObject().SetActive(true);
		}
	}

	private bool IsWidgetIsNull()
	{
		return this.m_nodeInfo == null || this.m_lblName == null || this.m_spNameBG == null || this.m_spTitleIcon == null || this.m_lblTitle == null || this.m_spBloodBar1Bg == null || this.m_spBloodBar1Fg == null || this.m_spBloodBar2Bg1 == null || this.m_spBloodBar2Bg2 == null || this.m_spBloodBar2Fg == null || this.m_lblGuildTitle == null || this.m_spCommonIcon == null;
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.OnEnterScene));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.OnEnterScene));
	}

	private void OnEnterScene(int sceneId)
	{
		this.ShowByScene(sceneId);
	}

	private void ShowByScene(int sceneId)
	{
		if (MySceneManager.IsMainScene(sceneId))
		{
			this.ShowAsMainScene();
		}
		else
		{
			InstanceType currentInstanceType = InstanceManager.CurrentInstanceType;
			if (currentInstanceType == InstanceType.DungeonNormal || currentInstanceType == InstanceType.DungeonElite || currentInstanceType == InstanceType.ChangeCareer)
			{
				this.ShowAsInstanceType1();
			}
			else if (currentInstanceType == InstanceType.DungeonMutiPeople || currentInstanceType == InstanceType.GuildWar || currentInstanceType == InstanceType.Defence || currentInstanceType == InstanceType.Bounty || currentInstanceType == InstanceType.WildBossMulti)
			{
				this.ShowAsInstanceType2();
			}
			else if (currentInstanceType == InstanceType.Arena || currentInstanceType == InstanceType.GangFight)
			{
				this.ShowAsInstanceType4();
			}
			else if (currentInstanceType == InstanceType.Hook)
			{
				this.ShowAsInstanceType5();
			}
			else
			{
				this.ShowAsInstanceType3();
			}
		}
	}

	private void ShowAsMainScene()
	{
		HeadInfo headInfo = DataReader<HeadInfo>.Get(this.actorType);
		if (headInfo != null && headInfo.MainScene.get_Count() >= 5)
		{
			HeadInfoManager.HeadInfoData data = HeadInfoManager.Instance.GetData(this.uuid);
			HeadInfoManager.Instance.ShowName(this.uuid, headInfo.MainScene.get_Item(0) == 1, data, this);
			HeadInfoManager.Instance.ShowTitle(this.uuid, headInfo.MainScene.get_Item(1) == 1, data, this);
			HeadInfoManager.Instance.ShowBloodBarByScene(this.uuid, headInfo.MainScene.get_Item(2) == 1, data, this);
			HeadInfoManager.Instance.ShowGuildTitle(this.uuid, headInfo.MainScene.get_Item(3) == 1, data, this);
			HeadInfoManager.Instance.ShowCommonIcon(this.uuid, headInfo.MainScene.get_Item(4) == 1, data, this);
		}
		else
		{
			this.HideAll(this.uuid);
		}
	}

	private void ShowAsInstanceType1()
	{
		HeadInfo headInfo = DataReader<HeadInfo>.Get(this.actorType);
		if (headInfo != null && headInfo.InstanceType1.get_Count() >= 5)
		{
			HeadInfoManager.Instance.ShowName(this.uuid, headInfo.InstanceType1.get_Item(0) == 1);
			HeadInfoManager.Instance.ShowTitle(this.uuid, headInfo.InstanceType1.get_Item(1) == 1);
			HeadInfoManager.Instance.ShowBloodBarByScene(this.uuid, headInfo.InstanceType1.get_Item(2) == 1);
			HeadInfoManager.Instance.ShowGuildTitle(this.uuid, headInfo.InstanceType1.get_Item(3) == 1);
			HeadInfoManager.Instance.ShowCommonIcon(this.uuid, headInfo.InstanceType1.get_Item(4) == 1);
		}
		else
		{
			this.HideAll(this.uuid);
		}
	}

	private void ShowAsInstanceType2()
	{
		HeadInfo headInfo = DataReader<HeadInfo>.Get(this.actorType);
		if (headInfo != null && headInfo.InstanceType2.get_Count() >= 5)
		{
			HeadInfoManager.Instance.ShowName(this.uuid, headInfo.InstanceType2.get_Item(0) == 1);
			HeadInfoManager.Instance.ShowTitle(this.uuid, headInfo.InstanceType2.get_Item(1) == 1);
			HeadInfoManager.Instance.ShowBloodBarByScene(this.uuid, headInfo.InstanceType2.get_Item(2) == 1);
			HeadInfoManager.Instance.ShowGuildTitle(this.uuid, headInfo.InstanceType2.get_Item(3) == 1);
			HeadInfoManager.Instance.ShowCommonIcon(this.uuid, headInfo.InstanceType2.get_Item(4) == 1);
		}
		else
		{
			this.HideAll(this.uuid);
		}
	}

	private void ShowAsInstanceType3()
	{
		HeadInfo headInfo = DataReader<HeadInfo>.Get(this.actorType);
		if (headInfo != null && headInfo.InstanceType3.get_Count() >= 5)
		{
			HeadInfoManager.Instance.ShowName(this.uuid, headInfo.InstanceType3.get_Item(0) == 1);
			HeadInfoManager.Instance.ShowTitle(this.uuid, headInfo.InstanceType3.get_Item(1) == 1);
			HeadInfoManager.Instance.ShowBloodBarByScene(this.uuid, headInfo.InstanceType3.get_Item(2) == 1);
			HeadInfoManager.Instance.ShowGuildTitle(this.uuid, headInfo.InstanceType3.get_Item(3) == 1);
			HeadInfoManager.Instance.ShowCommonIcon(this.uuid, headInfo.InstanceType3.get_Item(4) == 1);
		}
		else
		{
			this.HideAll(this.uuid);
		}
	}

	private void ShowAsInstanceType4()
	{
		HeadInfo headInfo = DataReader<HeadInfo>.Get(this.actorType);
		if (headInfo != null && headInfo.InstanceType4.get_Count() >= 5)
		{
			HeadInfoManager.Instance.ShowName(this.uuid, headInfo.InstanceType4.get_Item(0) == 1);
			HeadInfoManager.Instance.ShowTitle(this.uuid, headInfo.InstanceType4.get_Item(1) == 1);
			HeadInfoManager.Instance.ShowBloodBarByScene(this.uuid, headInfo.InstanceType4.get_Item(2) == 1);
			HeadInfoManager.Instance.ShowGuildTitle(this.uuid, headInfo.InstanceType4.get_Item(3) == 1);
			HeadInfoManager.Instance.ShowCommonIcon(this.uuid, headInfo.InstanceType4.get_Item(4) == 1);
		}
		else
		{
			this.HideAll(this.uuid);
		}
	}

	private void ShowAsInstanceType5()
	{
		HeadInfo headInfo = DataReader<HeadInfo>.Get(this.actorType);
		if (headInfo != null && headInfo.InstanceType4.get_Count() >= 5)
		{
			HeadInfoManager.Instance.ShowName(this.uuid, headInfo.InstanceType5.get_Item(0) == 1);
			HeadInfoManager.Instance.ShowTitle(this.uuid, headInfo.InstanceType5.get_Item(1) == 1);
			HeadInfoManager.Instance.ShowBloodBarByScene(this.uuid, headInfo.InstanceType5.get_Item(2) == 1);
			HeadInfoManager.Instance.ShowGuildTitle(this.uuid, headInfo.InstanceType5.get_Item(3) == 1);
			HeadInfoManager.Instance.ShowCommonIcon(this.uuid, headInfo.InstanceType5.get_Item(4) == 1);
		}
		else
		{
			this.HideAll(this.uuid);
		}
	}

	public void ShowName(bool isShow)
	{
		if (HeadInfoManager.IsUpdateLockOn)
		{
			return;
		}
		if (this.IsWidgetIsNull())
		{
			return;
		}
		this.DoShowName(isShow);
		this.height_dynamic_name = ((!isShow) ? 0f : 26f);
		this.SetPosGuildTitle();
		this.SetPosTitle();
	}

	public void SetName(string name)
	{
		if (HeadInfoManager.IsUpdateLockOn)
		{
			return;
		}
		if (this.IsWidgetIsNull())
		{
			return;
		}
		this.DoSetName(this.GetName(name));
	}

	public void SetAndShowTitle(bool isShow, int titleId)
	{
		if (HeadInfoManager.IsUpdateLockOn)
		{
			return;
		}
		if (this.IsWidgetIsNull())
		{
			return;
		}
		if (isShow && titleId > 0)
		{
			ChengHao chengHao = DataReader<ChengHao>.Get(titleId);
			if (chengHao != null)
			{
				if (chengHao.displayWay == 1)
				{
					this.SetTitle(chengHao.displayWay, GameDataUtils.GetChineseContent(chengHao.icon, false), null);
				}
				else if (chengHao.displayWay == 2)
				{
					this.SetTitle(chengHao.displayWay, string.Empty, GameDataUtils.GetIcon(chengHao.icon));
				}
			}
		}
		else
		{
			this.m_lblTitle.set_enabled(false);
			this.m_spTitleBG.set_enabled(false);
			this.m_spTitleIcon.set_enabled(false);
		}
	}

	public void SetAndShowGuildTitle(bool isShow, string guildTitle)
	{
		if (HeadInfoManager.IsUpdateLockOn)
		{
			return;
		}
		if (this.IsWidgetIsNull())
		{
			return;
		}
		if (string.IsNullOrEmpty(guildTitle))
		{
			isShow = false;
		}
		this.m_lblGuildTitle.set_enabled(isShow);
		this.m_spGuildTitleBG.set_enabled(isShow);
		this.height_dynamic_guildtitle = ((!isShow) ? 0f : 26f);
		if (isShow)
		{
			this.SetPosTitle();
			this.SetGuildTitle(guildTitle);
		}
	}

	public void ShowCommonIcon(bool isShow, int iconId)
	{
		if (HeadInfoManager.IsUpdateLockOn)
		{
			return;
		}
		if (this.IsWidgetIsNull())
		{
			return;
		}
		if (iconId <= 0)
		{
			isShow = false;
		}
		this.m_spCommonIcon.set_enabled(isShow);
	}

	public void SetAndShowCommonIcon(bool isShow, int iconId)
	{
		if (HeadInfoManager.IsUpdateLockOn)
		{
			return;
		}
		if (this.IsWidgetIsNull())
		{
			return;
		}
		if (iconId <= 0)
		{
			isShow = false;
		}
		if (isShow)
		{
			this.m_spCommonIcon.set_enabled(true);
			this.SetCommonIcon(GameDataUtils.GetIcon(iconId));
		}
		else
		{
			this.m_spCommonIcon.set_enabled(false);
		}
	}

	public void SetAndShowBloodBar(bool isShow, int type)
	{
		if (HeadInfoManager.IsUpdateLockOn)
		{
			return;
		}
		if (this.IsWidgetIsNull())
		{
			return;
		}
		if (!isShow)
		{
			this.ShowBloodBar1(false);
			this.ShowBloodBar2(false);
		}
		else if (type == 1 || type == 2)
		{
			this.ShowBloodBar1(true);
			this.ShowBloodBar2(false);
		}
		else if (type == 11 || type == 12)
		{
			this.ShowBloodBar1(false);
			this.ShowBloodBar2(true);
		}
		if (this.bloodBarType == type)
		{
			this.bloodBarType = type;
		}
		else
		{
			this.bloodBarType = type;
			this.SetBloodBarImage();
		}
		this.SetInfoPos(isShow);
	}

	public void SetBloodBar(float fillAmount)
	{
		if (HeadInfoManager.IsUpdateLockOn)
		{
			return;
		}
		if (this.IsWidgetIsNull())
		{
			return;
		}
		this.m_spBloodBar1Fg.set_fillAmount(fillAmount);
		this.m_spBloodBar2Fg.set_fillAmount(fillAmount);
	}

	public void SetBloodBarSize(List<int> bloodBarSize)
	{
		if (HeadInfoManager.IsUpdateLockOn)
		{
			return;
		}
		if (this.IsWidgetIsNull())
		{
			return;
		}
		if (bloodBarSize != null && bloodBarSize.get_Count() >= 2)
		{
			this.SetBloodBarSize(false, bloodBarSize.get_Item(0), bloodBarSize.get_Item(1));
		}
		else
		{
			this.SetBloodBarSize(true, 0, 0);
		}
	}

	public void ResetAll()
	{
		this.uuid = 0L;
		if (this.IsWidgetIsNull())
		{
			return;
		}
		this.ResetName();
		this.ResetTitle();
		this.ResetGuildTitle();
		this.ResetBloodBar();
	}

	private void HideAll(long uuid)
	{
		HeadInfoManager.Instance.ShowName(uuid, false);
		HeadInfoManager.Instance.ShowTitle(uuid, false);
		HeadInfoManager.Instance.ShowGuildTitle(uuid, false);
		HeadInfoManager.Instance.ShowBloodBarByScene(uuid, false);
	}

	private void ResetName()
	{
		this.DoSetName(string.Empty);
		this.ShowName(false);
	}

	private string GetName(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return name;
		}
		string result = string.Empty;
		int actorType = this.actorType;
		switch (actorType)
		{
		case 1:
			result = TextColorMgr.GetColorByID(name, 101);
			break;
		case 2:
			name = TextColorMgr.FilterColor(name);
			result = TextColorMgr.GetColorByID(name, 108);
			break;
		case 3:
			result = TextColorMgr.GetColorByID(name, 105);
			break;
		case 4:
			name = TextColorMgr.FilterColor(name);
			result = TextColorMgr.GetColorByID(name, 109);
			break;
		case 5:
			result = TextColorMgr.GetColorByID(name, 106);
			break;
		case 6:
			name = TextColorMgr.FilterColor(name);
			result = TextColorMgr.GetColorByID(name, 110);
			break;
		case 7:
			result = TextColorMgr.GetColorByID(name, 111);
			break;
		case 8:
			name = TextColorMgr.FilterColor(name);
			result = TextColorMgr.GetColorByID(name, 112);
			break;
		default:
			if (actorType != 21)
			{
				if (actorType != 31)
				{
					if (actorType != 41)
					{
						if (actorType != 61)
						{
							result = name;
						}
						else
						{
							result = TextColorMgr.GetColorByID(name, 106);
						}
					}
					else
					{
						result = TextColorMgr.GetColorByID(name, 106);
					}
				}
				else
				{
					result = TextColorMgr.GetColorByID(name, 107);
				}
			}
			else
			{
				result = TextColorMgr.GetColorByID(name, 103);
			}
			break;
		}
		return result;
	}

	private void DoSetName(string name)
	{
		this.m_lblName.set_text(name);
		this.m_lblName.get_rectTransform().set_sizeDelta(new Vector2(this.m_lblName.get_rectTransform().get_sizeDelta().x, this.m_lblName.get_preferredHeight() + 10f));
		if (string.IsNullOrEmpty(this.m_lblName.get_text()))
		{
			this.m_spNameBG.get_rectTransform().set_sizeDelta(Vector2.get_zero());
		}
		else
		{
			this.m_spNameBG.get_rectTransform().set_sizeDelta(new Vector2(this.m_lblName.get_preferredWidth() + 50f, this.m_lblName.get_preferredHeight() + 10f));
		}
	}

	private void DoShowName(bool isShow)
	{
		this.m_lblName.set_enabled(isShow);
		this.m_spNameBG.set_enabled(isShow);
	}

	private void ResetTitle()
	{
		this.m_lblTitle.set_text(string.Empty);
		this.m_lblTitle.set_enabled(false);
		this.m_spTitleBG.set_enabled(false);
		ResourceManager.SetSprite(this.m_spTitleIcon, ResourceManagerBase.GetNullSprite());
		this.m_spTitleIcon.set_enabled(false);
	}

	private string GetFinalTitle(string title)
	{
		string result = string.Empty;
		int actorType = this.actorType;
		switch (actorType)
		{
		case 1:
			result = TextColorMgr.GetColorByID(title, 102);
			return result;
		case 2:
		case 4:
			IL_29:
			if (actorType != 21)
			{
				return result;
			}
			result = TextColorMgr.GetColorByID(title, 104);
			return result;
		case 3:
			return result;
		case 5:
			return result;
		}
		goto IL_29;
	}

	private void SetTitle(int displayWay, string title, SpriteRenderer titleIcon)
	{
		if (displayWay == 1)
		{
			this.m_lblTitle.set_enabled(true);
			this.m_spTitleBG.set_enabled(true);
			this.m_spTitleIcon.set_enabled(false);
			this.m_lblTitle.set_text(this.GetFinalTitle(title));
			if (string.IsNullOrEmpty(this.m_lblTitle.get_text()))
			{
				this.m_spTitleBG.get_rectTransform().set_sizeDelta(Vector2.get_zero());
			}
			else
			{
				this.m_spTitleBG.get_rectTransform().set_sizeDelta(new Vector2(this.m_lblTitle.get_preferredWidth() + 50f, 36f));
			}
			this.SetPosTitle();
		}
		else if (displayWay == 2)
		{
			ResourceManager.SetSprite(this.m_spTitleIcon, titleIcon);
			if (titleIcon != null)
			{
				this.m_lblTitle.set_enabled(false);
				this.m_spTitleBG.set_enabled(false);
				this.m_spTitleIcon.set_enabled(true);
				this.m_spTitleIcon.SetNativeSize();
				this.SetPosTitle();
			}
			else
			{
				this.m_lblTitle.set_enabled(true);
				this.m_spTitleBG.set_enabled(true);
				this.m_spTitleIcon.set_enabled(false);
				this.m_lblTitle.set_text("ICON." + titleIcon);
			}
		}
	}

	private void ResetGuildTitle()
	{
		this.m_lblGuildTitle.set_text(string.Empty);
		this.m_lblGuildTitle.set_enabled(false);
		this.m_spGuildTitleBG.set_enabled(false);
	}

	private void SetGuildTitle(string guildTitle)
	{
		this.m_lblGuildTitle.set_text(guildTitle);
		if (string.IsNullOrEmpty(guildTitle))
		{
			this.m_spGuildTitleBG.get_rectTransform().set_sizeDelta(Vector2.get_zero());
		}
		else
		{
			this.m_spGuildTitleBG.get_rectTransform().set_sizeDelta(new Vector2(this.m_lblGuildTitle.get_preferredWidth() + 50f, 30f));
		}
	}

	private void ResetCommonIcon()
	{
		ResourceManager.SetSprite(this.m_spCommonIcon, ResourceManagerBase.GetNullSprite());
		this.m_spCommonIcon.set_enabled(false);
	}

	private void SetCommonIcon(SpriteRenderer spr)
	{
		ResourceManager.SetSprite(this.m_spCommonIcon, spr);
		this.m_spCommonIcon.SetNativeSize();
		RectTransform rectTransform = this.m_spCommonIcon.get_rectTransform();
		rectTransform.set_anchoredPosition(new Vector2(rectTransform.get_anchoredPosition().x, this.m_spCommonIcon.get_sprite().get_rect().get_height() / 2f + 15f));
	}

	private void SetInfoPos(bool bloodShow)
	{
		if (this.IsWidgetIsNull())
		{
			return;
		}
		if (!bloodShow)
		{
			this.m_nodeInfo.set_anchoredPosition(new Vector2(0f, 2f));
		}
		else
		{
			this.m_nodeInfo.set_anchoredPosition(new Vector2(0f, 30f));
		}
	}

	private void ResetBloodBar()
	{
		if (this.IsWidgetIsNull())
		{
			return;
		}
		this.SetBloodBarSize(true, 0, 0);
		this.SetAndShowBloodBar(false, 0);
		this.SetBloodBarNull();
	}

	private void SetBloodBarImage()
	{
		if (this.IsWidgetIsNull())
		{
			return;
		}
		if (this.bloodBarType == 1)
		{
			ResourceManager.SetSprite(this.m_spBloodBar1Bg, ResourceManager.GetIconSprite("xiaoguaixuetiao1"));
			ResourceManager.SetSprite(this.m_spBloodBar1Fg, ResourceManager.GetIconSprite("xiaoguaixuetiao2"));
		}
		else if (this.bloodBarType == 2)
		{
			ResourceManager.SetSprite(this.m_spBloodBar1Bg, ResourceManager.GetIconSprite("xiaoguaixuetiao1"));
			ResourceManager.SetSprite(this.m_spBloodBar1Fg, ResourceManager.GetIconSprite("cwxuetiao"));
		}
		else if (this.bloodBarType == 11)
		{
			ResourceManager.SetSprite(this.m_spBloodBar2Bg2, ResourceManager.GetIconSprite("fight_bloodframe_2"));
			ResourceManager.SetSprite(this.m_spBloodBar2Bg1, ResourceManager.GetIconSprite("fight_bloodframe_1"));
			ResourceManager.SetSprite(this.m_spBloodBar2Fg, ResourceManager.GetIconSprite("fight_bloodframe_3"));
		}
		else if (this.bloodBarType == 12)
		{
			ResourceManager.SetSprite(this.m_spBloodBar2Bg2, ResourceManager.GetIconSprite("fight_bloodframe_2"));
			ResourceManager.SetSprite(this.m_spBloodBar2Bg1, ResourceManager.GetIconSprite("fight_greenframe_1"));
			ResourceManager.SetSprite(this.m_spBloodBar2Fg, ResourceManager.GetIconSprite("fight_greenframe_3"));
		}
	}

	private void SetBloodBarSize(bool nativeSize, int width = 0, int height = 0)
	{
		if (this.IsWidgetIsNull())
		{
			return;
		}
		this.m_spBloodBar1Bg.SetNativeSize();
		this.m_spBloodBar1Fg.SetNativeSize();
		if (width != 0 && height != 0)
		{
			this.m_spBloodBar1Bg.get_rectTransform().set_sizeDelta(new Vector2((float)width, (float)height));
			this.m_spBloodBar1Fg.get_rectTransform().set_sizeDelta(new Vector2((float)width, (float)height));
		}
		else if (width != 0)
		{
			this.m_spBloodBar1Bg.get_rectTransform().set_sizeDelta(new Vector2((float)width, this.m_spBloodBar1Bg.get_rectTransform().get_sizeDelta().y));
			this.m_spBloodBar1Fg.get_rectTransform().set_sizeDelta(new Vector2((float)width, this.m_spBloodBar1Fg.get_rectTransform().get_sizeDelta().y));
		}
		else if (height != 0)
		{
			this.m_spBloodBar1Bg.get_rectTransform().set_sizeDelta(new Vector2(this.m_spBloodBar1Bg.get_rectTransform().get_sizeDelta().x, (float)height));
			this.m_spBloodBar1Fg.get_rectTransform().set_sizeDelta(new Vector2(this.m_spBloodBar1Fg.get_rectTransform().get_sizeDelta().x, (float)height));
		}
	}

	private void SetBloodBarNull()
	{
		this.SetBloodBar1Null();
		this.SetBloodBar2Null();
	}

	private void ShowBloodBar1(bool isShow)
	{
		if (HeadInfoManager.IsUpdateLockOn)
		{
			return;
		}
		if (this.IsWidgetIsNull())
		{
			return;
		}
		this.m_spBloodBar1Bg.set_enabled(isShow);
		this.m_spBloodBar1Fg.set_enabled(isShow);
	}

	private void ShowBloodBar2(bool isShow)
	{
		if (HeadInfoManager.IsUpdateLockOn)
		{
			return;
		}
		if (this.IsWidgetIsNull())
		{
			return;
		}
		this.m_spBloodBar2Bg1.set_enabled(isShow);
		this.m_spBloodBar2Bg2.set_enabled(isShow);
		this.m_spBloodBar2Fg.set_enabled(isShow);
	}

	private void SetBloodBar1Null()
	{
		if (this.IsWidgetIsNull())
		{
			return;
		}
		ResourceManager.SetSprite(this.m_spBloodBar1Bg, ResourceManagerBase.GetNullSprite());
		ResourceManager.SetSprite(this.m_spBloodBar1Fg, ResourceManagerBase.GetNullSprite());
	}

	private void SetBloodBar2Null()
	{
		if (this.IsWidgetIsNull())
		{
			return;
		}
		ResourceManager.SetSprite(this.m_spBloodBar2Bg1, ResourceManagerBase.GetNullSprite());
		ResourceManager.SetSprite(this.m_spBloodBar2Fg, ResourceManagerBase.GetNullSprite());
		ResourceManager.SetSprite(this.m_spBloodBar2Bg2, ResourceManagerBase.GetNullSprite());
	}

	private void SetPosName()
	{
		if (this.IsWidgetIsNull())
		{
			return;
		}
	}

	private void SetPosGuildTitle()
	{
		if (this.IsWidgetIsNull())
		{
			return;
		}
		this.m_spGuildTitleBG.get_rectTransform().set_anchoredPosition(new Vector2(this.m_spGuildTitleBG.get_rectTransform().get_anchoredPosition().x, this.height_dynamic_name));
	}

	private void SetPosTitle()
	{
		if (this.IsWidgetIsNull())
		{
			return;
		}
		RectTransform rectTransform = this.m_spTitleIcon.get_transform() as RectTransform;
		rectTransform.set_anchoredPosition(new Vector2(rectTransform.get_anchoredPosition().x, this.height_dynamic_name + this.height_dynamic_guildtitle - 12f));
		this.m_spTitleBG.get_rectTransform().set_anchoredPosition(new Vector2(this.m_spTitleBG.get_rectTransform().get_anchoredPosition().x, this.height_dynamic_name + this.height_dynamic_guildtitle));
	}
}
