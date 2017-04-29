using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuildUI : UIBase
{
	private GameObject GuildBossTipsObj;

	private Dictionary<GuildUIState, ButtonCustom> BtnGuildUIDic;

	private Dictionary<GuildUIState, Transform> GuildTransformDic;

	private GuildUIState lastState;

	private GuildUIState currentState;

	private List<int> joinLvMinList;

	private int currentRoleMinLv;

	private bool isSearch;

	private int currentPage = 1;

	private bool haseRequire;

	private Text guildNameText;

	private Text guildLvText;

	private Text guildFightingText;

	private Text myGuildNoticeText;

	private Text guildAttributionText;

	private Text guildPositionText;

	private Text guildMemberNumText;

	private Text activityStateText;

	private uint t;

	private bool IsClickArrow;

	private bool isSelected;

	private bool onlyShowCanJoin;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		this.joinLvMinList = new List<int>();
		string value = DataReader<GongHuiXinXi>.Get("Level Limits").value;
		string[] array = value.Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			this.joinLvMinList.Add(int.Parse(array[i]));
		}
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.BtnGuildUIDic = new Dictionary<GuildUIState, ButtonCustom>();
		ButtonCustom component = base.FindTransform("CreateBtn").GetComponent<ButtonCustom>();
		ButtonCustom component2 = base.FindTransform("JoinBtn").GetComponent<ButtonCustom>();
		this.BtnGuildUIDic.Add(GuildUIState.GuildCreate, component);
		this.BtnGuildUIDic.Add(GuildUIState.GuildJoin, component2);
		using (Dictionary<GuildUIState, ButtonCustom>.ValueCollection.Enumerator enumerator = this.BtnGuildUIDic.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ButtonCustom current = enumerator.get_Current();
				ButtonCustom expr_6C = current;
				expr_6C.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_6C.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickTabBtn));
			}
		}
		this.GuildTransformDic = new Dictionary<GuildUIState, Transform>();
		Transform transform = base.get_transform().FindChild("NoGuildPanel").FindChild("CreateGuildPanel");
		Transform transform2 = base.get_transform().FindChild("NoGuildPanel").FindChild("JoinGuildPanel");
		this.GuildTransformDic.Add(GuildUIState.GuildCreate, transform);
		this.GuildTransformDic.Add(GuildUIState.GuildJoin, transform2);
		this.GuildBossTipsObj = base.FindTransform("GuildBossTips").get_gameObject();
		base.FindTransform("BtnTitleHide").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickShowOrHideTitle);
		base.FindTransform("BtnOK").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCreateGuildBtn);
		base.FindTransform("BtnQuery").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickQueryGuild);
		base.FindTransform("InfoBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenHeadQuartersBtn);
		base.FindTransform("BtnShop").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenShopBtn);
		base.FindTransform("BtnBuild").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenBuildBtn);
		base.FindTransform("BtnActivity").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenActivityBtn);
		base.FindTransform("BtnGuildBoss").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenGuildBossBtn);
		base.FindTransform("BtnGuildStove").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildStoveBtn);
		base.FindTransform("BtnGuildWar").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenGuildWarVSUI);
		base.FindTransform("BtnSkill").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenGuildSkillUI);
		base.FindTransform("BtnGuildStorage").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildStorageBtn);
		base.FindTransform("selectBtnRegion").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickLvDownBtn);
		base.FindTransform("selectMaskImg").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickLvDownBtn);
		base.FindTransform("BtnSettingInvite").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSettingInviteBtn);
		base.FindTransform("BtnShowOnlyCanJoin").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickShowOnlyCanJoinBtn);
		this.guildNameText = base.FindTransform("guildInfoNameText").GetComponent<Text>();
		this.guildNameText.set_text(string.Empty);
		this.guildFightingText = base.FindTransform("guildFightingText").GetComponent<Text>();
		this.guildFightingText.set_text(string.Empty);
		this.myGuildNoticeText = base.FindTransform("myGuildNoticeText").GetComponent<Text>();
		this.myGuildNoticeText.set_text(string.Empty);
		this.guildAttributionText = base.FindTransform("guildAttribution").GetComponent<Text>();
		this.guildAttributionText.set_text(string.Empty);
		this.guildPositionText = base.FindTransform("guildPosition").GetComponent<Text>();
		this.activityStateText = base.FindTransform("ActivityStateText").GetComponent<Text>();
		this.guildLvText = base.FindTransform("guildlvText").GetComponent<Text>();
		this.guildMemberNumText = base.FindTransform("guildMemberNumText").GetComponent<Text>();
		base.FindTransform("ListViewGuildInfo").GetComponent<ScrollRectCustom>().onDrag = delegate(PointerEventData data)
		{
			float verticalNormalizedPosition = base.FindTransform("ListViewGuildInfo").GetComponent<ScrollRectCustom>().verticalNormalizedPosition;
			if (verticalNormalizedPosition <= 0f && !this.haseRequire)
			{
				if (GuildManager.Instance.CanNotRequire)
				{
					return;
				}
				this.haseRequire = true;
				this.currentPage++;
				this.OnSendQueryGuildInfo();
			}
		};
		InputField component3 = this.GuildTransformDic.get_Item(GuildUIState.GuildCreate).FindChild("guildNoticeText").GetComponent<InputField>();
		string value = DataReader<GongHuiXinXi>.Get("manifesto").value;
		int characterLimit = (int)float.Parse(value);
		component3.set_characterLimit(characterLimit);
		this.SetTextName();
		this.UpdateStateInfo();
		this.OnGuildWarStepNty();
		this.UpdateGuildBossTipsState();
	}

	private void SetTextName()
	{
		base.FindTransform("JoinBtn").FindChild("btnText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515032, false));
		base.FindTransform("CreateBtn").FindChild("btnText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515033, false));
		base.FindTransform("JoinGuildPanel").FindChild("BtnOKName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515034, false));
		base.FindTransform("JoinGuildPanel").FindChild("InputField").GetComponent<InputField>().get_placeholder().GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515035, false));
		base.FindTransform("JoinGuildPanel").FindChild("NoGuildTipText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515106, false));
		base.FindTransform("CreateGuildPanel").FindChild("title").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515033, false));
		base.FindTransform("CreateGuildPanel").FindChild("guildName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515036, false));
		base.FindTransform("CreateGuildPanel").FindChild("guildNotice").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515037, false));
		base.FindTransform("CreateGuildPanel").FindChild("guildRoleMinLV").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515038, false));
		base.FindTransform("CreateGuildPanel").FindChild("guildNameText").GetComponent<InputField>().get_placeholder().GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515039, false));
		base.FindTransform("CreateGuildPanel").FindChild("guildNoticeText").GetComponent<InputField>().get_placeholder().GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515040, false));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110040), string.Empty, delegate
		{
			if (this.isSearch && this.currentState == GuildUIState.GuildJoin)
			{
				if (GuildManager.Instance.OwnApplicationGuilds != null && GuildManager.Instance.OwnApplicationGuilds.get_Count() > 9)
				{
					GuildManager.Instance.CanNotRequire = false;
				}
				InputField component2 = this.GuildTransformDic.get_Item(GuildUIState.GuildJoin).FindChild("InputField").GetComponent<InputField>();
				component2.set_text(string.Empty);
				this.OnUpdateGuildInfoList();
			}
			else
			{
				UIStackManager.Instance.PopUIPrevious(base.uiType);
			}
		}, false);
		this.isSelected = false;
		this.OnClickSettingInviteBtn(null);
		this.onlyShowCanJoin = false;
		base.FindTransform("BtnShowCanJoinGuildSelect").get_gameObject().SetActive(this.onlyShowCanJoin);
		InputField component = this.GuildTransformDic.get_Item(GuildUIState.GuildJoin).FindChild("InputField").GetComponent<InputField>();
		component.set_text(string.Empty);
		base.FindTransform("ListViewGuildInfo").GetComponent<ScrollRectCustom>().verticalNormalizedPosition = 1f;
		this.ResetUI();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			TimerHeap.DelTimer(this.t);
			GuildManager.Instance.OwnApplicationGuilds = null;
			GuildManager.Instance.SearchApplicationGuilds = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateGuildInfoList, new Callback(this.OnUpdateGuildInfoList));
		EventDispatcher.AddListener(EventNames.UpdateSearchGuildList, new Callback(this.OnUpdateSearchGuildList));
		EventDispatcher.AddListener(EventNames.OnGuildInfoNty, new Callback(this.OnGuildInfoNty));
		EventDispatcher.AddListener(EventNames.OnDissolveGuildRes, new Callback(this.OnDissolveGuildRes));
		EventDispatcher.AddListener(EventNames.UpdateGuildInfo, new Callback(this.OnUpdateGuildInfo));
		EventDispatcher.AddListener(EventNames.OnGuildSettingNty, new Callback(this.OnUpdateGuildNotice));
		EventDispatcher.AddListener(EventNames.OnUpgradeGuildRes, new Callback(this.OnUpgradeGuildRes));
		EventDispatcher.AddListener(EventNames.OnGuildInfoChangeNty, new Callback(this.OnGuildInfoChangeNty));
		EventDispatcher.AddListener(EventNames.UpdateGuildMemberList, new Callback(this.OnUpdateGuildMemberList));
		EventDispatcher.AddListener(EventNames.OnGuildWarStepNty, new Callback(this.OnGuildWarStepNty));
		EventDispatcher.AddListener(EventNames.OnGuildBossStatusNty, new Callback(this.UpdateGuildBossTipsState));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateGuildInfoList, new Callback(this.OnUpdateGuildInfoList));
		EventDispatcher.RemoveListener(EventNames.UpdateSearchGuildList, new Callback(this.OnUpdateSearchGuildList));
		EventDispatcher.RemoveListener(EventNames.OnGuildInfoNty, new Callback(this.OnGuildInfoNty));
		EventDispatcher.RemoveListener(EventNames.OnDissolveGuildRes, new Callback(this.OnDissolveGuildRes));
		EventDispatcher.RemoveListener(EventNames.UpdateGuildInfo, new Callback(this.OnUpdateGuildInfo));
		EventDispatcher.RemoveListener(EventNames.OnGuildSettingNty, new Callback(this.OnUpdateGuildNotice));
		EventDispatcher.RemoveListener(EventNames.OnUpgradeGuildRes, new Callback(this.OnUpgradeGuildRes));
		EventDispatcher.RemoveListener(EventNames.OnGuildInfoChangeNty, new Callback(this.OnGuildInfoChangeNty));
		EventDispatcher.RemoveListener(EventNames.UpdateGuildMemberList, new Callback(this.OnUpdateGuildMemberList));
		EventDispatcher.RemoveListener(EventNames.OnGuildWarStepNty, new Callback(this.OnGuildWarStepNty));
		EventDispatcher.RemoveListener(EventNames.OnGuildBossStatusNty, new Callback(this.UpdateGuildBossTipsState));
	}

	private void OnClickTabBtn(GameObject go)
	{
		string name = go.get_name();
		if (name != null)
		{
			if (GuildUI.<>f__switch$map16 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(2);
				dictionary.Add("JoinBtn", 0);
				dictionary.Add("CreateBtn", 1);
				GuildUI.<>f__switch$map16 = dictionary;
			}
			int num;
			if (GuildUI.<>f__switch$map16.TryGetValue(name, ref num))
			{
				if (num != 0)
				{
					if (num == 1)
					{
						this.currentState = GuildUIState.GuildCreate;
					}
				}
				else
				{
					this.currentState = GuildUIState.GuildJoin;
				}
			}
		}
		if (this.currentState == this.lastState)
		{
			return;
		}
		this.SetBtnLightAndDim(go, "fenleianniu_1", true);
		if (this.BtnGuildUIDic.ContainsKey(this.lastState))
		{
			this.SetBtnLightAndDim(this.BtnGuildUIDic.get_Item(this.lastState).get_gameObject(), "fenleianniu_2", false);
		}
		this.RefreshUIState(this.currentState);
		this.lastState = this.currentState;
	}

	private void OnClickQueryGuild(GameObject go)
	{
		InputField component = this.GuildTransformDic.get_Item(GuildUIState.GuildJoin).FindChild("InputField").GetComponent<InputField>();
		if (string.IsNullOrEmpty(component.get_text()))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(506030, false));
			return;
		}
		if (this.onlyShowCanJoin)
		{
			this.onlyShowCanJoin = false;
			base.FindTransform("BtnShowCanJoinGuildSelect").get_gameObject().SetActive(false);
		}
		GuildManager.Instance.SendSearchGuildInfoReq(component.get_text(), 1, false);
	}

	private void OnClickCreateGuildBtn(GameObject go)
	{
		InputField guildNameText = this.GuildTransformDic.get_Item(GuildUIState.GuildCreate).FindChild("guildNameText").GetComponent<InputField>();
		InputField guildNoticeText = this.GuildTransformDic.get_Item(GuildUIState.GuildCreate).FindChild("guildNoticeText").GetComponent<InputField>();
		if (string.IsNullOrEmpty(guildNameText.get_text()))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(506030, false));
			return;
		}
		string empty = string.Empty;
		if (WordFilter.filter(guildNameText.get_text(), out empty, 1, false, false, "*"))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515026, false));
			return;
		}
		if (WordFilter.filter(guildNoticeText.get_text(), out empty, 1, false, false, "*"))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515026, false));
			return;
		}
		GongHuiDengJi gongHuiDengJi = DataReader<GongHuiDengJi>.Get(1);
		string chineseContent = GameDataUtils.GetChineseContent(621264, false);
		string text = GameDataUtils.GetChineseContent(506001, false);
		text = string.Format(text, gongHuiDengJi.num);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(chineseContent, text, null, delegate
		{
			GuildManager.Instance.SendSetUpGuildReq(guildNameText.get_text(), this.currentRoleMinLv, !this.isSelected, guildNoticeText.get_text());
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void OnClickOpenHeadQuartersBtn(GameObject go)
	{
		if (MySceneManager.Instance.IsCurrentGuildWarCityScene)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515069, false));
			return;
		}
		GuildHeadQuartersUI guildHeadQuartersUI = UIManagerControl.Instance.OpenUI("GuildHeadQuartersUI", UINodesManager.NormalUIRoot, false, UIType.Pop) as GuildHeadQuartersUI;
		guildHeadQuartersUI.get_transform().SetAsLastSibling();
	}

	private void OnClickOpenShopBtn(GameObject go)
	{
		LinkNavigationManager.OpenGuildMarketUI();
	}

	private void OnClickOpenBuildBtn(GameObject go)
	{
		if (MySceneManager.Instance.IsCurrentGuildWarCityScene)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515069, false));
			return;
		}
		GuildConstructUI guildConstructUI = UIManagerControl.Instance.OpenUI("GuildConstructUI", UINodesManager.NormalUIRoot, false, UIType.NonPush) as GuildConstructUI;
		guildConstructUI.get_transform().SetAsLastSibling();
	}

	private void OnClickOpenGuildWarVSUI(GameObject go)
	{
		if (MySceneManager.Instance.IsCurrentGuildWarCityScene)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515069, false));
			return;
		}
		GuildWarVSInfoUI guildWarVSInfoUI = UIManagerControl.Instance.OpenUI("GuildWarVSInfoUI", UINodesManager.NormalUIRoot, false, UIType.NonPush) as GuildWarVSInfoUI;
		guildWarVSInfoUI.get_transform().SetAsLastSibling();
	}

	private void OnClickOpenActivityBtn(GameObject go)
	{
		if (MySceneManager.Instance.IsCurrentGuildWarCityScene)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515069, false));
			return;
		}
		UIManagerControl.Instance.OpenUI("GuildActivityCenterUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}

	private void OnClickOpenGuildBossBtn(GameObject go)
	{
		if (MySceneManager.Instance.IsCurrentGuildWarCityScene)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515069, false));
			return;
		}
		UIManagerControl.Instance.OpenUI("GuildBossUI", UINodesManager.NormalUIRoot, false, UIType.NonPush);
	}

	private void OnClickGuildStoveBtn(GameObject go)
	{
		if (MySceneManager.Instance.IsCurrentGuildWarCityScene)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515069, false));
			return;
		}
		UIManagerControl.Instance.OpenUI("GuildStoveUI", UINodesManager.NormalUIRoot, false, UIType.NonPush);
	}

	private void OnClickGuildStorageBtn(GameObject go)
	{
		UIManagerControl.Instance.OpenUI("GuildStorageUI", UINodesManager.NormalUIRoot, false, UIType.NonPush);
	}

	private void OnClickOpenGuildSkillUI(GameObject go)
	{
		if (MySceneManager.Instance.IsCurrentGuildWarCityScene)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515069, false));
			return;
		}
		UIManagerControl.Instance.OpenUI("GuildSkillUI", UINodesManager.NormalUIRoot, false, UIType.NonPush);
	}

	private void OnClickLvDownBtn(GameObject go)
	{
		this.IsClickArrow = !this.IsClickArrow;
		this.GuildTransformDic.get_Item(GuildUIState.GuildCreate).FindChild("selectList").get_gameObject().SetActive(this.IsClickArrow);
	}

	private void OnClickSettingInviteBtn(GameObject go)
	{
		this.isSelected = !this.isSelected;
		base.FindTransform("BtnSettingSelect").get_gameObject().SetActive(this.isSelected);
	}

	private void OnClickShowOnlyCanJoinBtn(GameObject go)
	{
		this.onlyShowCanJoin = !this.onlyShowCanJoin;
		base.FindTransform("BtnShowCanJoinGuildSelect").get_gameObject().SetActive(this.onlyShowCanJoin);
		InputField component = this.GuildTransformDic.get_Item(GuildUIState.GuildJoin).FindChild("InputField").GetComponent<InputField>();
		component.set_text(string.Empty);
		this.isSearch = false;
		GuildManager.Instance.OwnApplicationGuilds = null;
		this.currentPage = 1;
		this.OnSendQueryGuildInfo();
	}

	private void OnClickSelectRoleMinLV(GameObject go)
	{
		if (go.GetComponent<GuildButtonItem>() != null)
		{
			this.currentRoleMinLv = go.GetComponent<GuildButtonItem>().LV;
		}
		this.GuildTransformDic.get_Item(GuildUIState.GuildCreate).FindChild("guildRoleMinLVText").GetComponent<Text>().set_text(this.currentRoleMinLv.ToString());
		this.IsClickArrow = false;
		this.GuildTransformDic.get_Item(GuildUIState.GuildCreate).FindChild("selectList").get_gameObject().SetActive(this.IsClickArrow);
	}

	private void OnSendQueryGuildInfo()
	{
		if (this.currentPage <= 0)
		{
			this.currentPage = 1;
		}
		int fromIndex = (this.currentPage - 1) * 10;
		int toIndex = this.currentPage * 10 - 1;
		if (this.isSearch)
		{
			InputField component = this.GuildTransformDic.get_Item(GuildUIState.GuildJoin).FindChild("InputField").GetComponent<InputField>();
			GuildManager.Instance.SendSearchGuildInfoReq(component.get_text(), this.currentPage, false);
		}
		else
		{
			GuildManager.Instance.SendQueryGuildInfoReq(fromIndex, toIndex, this.onlyShowCanJoin);
		}
	}

	private void OnClickShowOrHideTitle(GameObject go)
	{
		bool flag = !GuildManager.Instance.IsHideTitle;
		base.FindTransform("BtnHide").get_gameObject().SetActive(flag);
		GuildManager.Instance.SendGuildTitleSetReq(flag);
	}

	private void ResetUI()
	{
		if (GuildManager.Instance.MyGuildnfo == null)
		{
			this.currentState = GuildUIState.GuildJoin;
			base.FindTransform("HaveGuildPanel").get_gameObject().SetActive(false);
			base.FindTransform("NoGuildPanel").get_gameObject().SetActive(true);
			using (Dictionary<GuildUIState, Transform>.Enumerator enumerator = this.GuildTransformDic.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<GuildUIState, Transform> current = enumerator.get_Current();
					if (current.get_Value().get_gameObject().get_activeSelf())
					{
						current.get_Value().get_gameObject().SetActive(false);
					}
				}
			}
			using (Dictionary<GuildUIState, ButtonCustom>.Enumerator enumerator2 = this.BtnGuildUIDic.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<GuildUIState, ButtonCustom> current2 = enumerator2.get_Current();
					this.SetBtnLightAndDim(current2.get_Value().get_gameObject(), "fenleianniu_2", false);
				}
			}
			if (this.BtnGuildUIDic.ContainsKey(this.currentState))
			{
				this.SetBtnLightAndDim(this.BtnGuildUIDic.get_Item(this.currentState).get_gameObject(), "fenleianniu_1", true);
			}
			this.lastState = this.currentState;
			this.RefreshUIState(this.currentState);
		}
		else
		{
			this.currentState = GuildUIState.GuildInfo;
			base.FindTransform("NoGuildPanel").get_gameObject().SetActive(false);
			base.FindTransform("HaveGuildPanel").get_gameObject().SetActive(true);
			this.OnReqGuildInfo();
		}
	}

	private void RefreshUIState(GuildUIState state)
	{
		this.isSearch = false;
		if (this.GuildTransformDic.ContainsKey(this.lastState))
		{
			this.GuildTransformDic.get_Item(this.lastState).get_gameObject().SetActive(false);
		}
		if (this.GuildTransformDic.ContainsKey(state))
		{
			this.GuildTransformDic.get_Item(state).get_gameObject().SetActive(true);
		}
		if (state != GuildUIState.GuildCreate)
		{
			if (state == GuildUIState.GuildJoin)
			{
				this.OnGetGuildInfoListReq();
			}
		}
		else
		{
			this.RefreshCreateGuildPanel();
		}
	}

	private void SetBtnLightAndDim(GameObject go, string btnIcon, bool isLight)
	{
		ResourceManager.SetSprite(go.get_transform().GetComponent<Image>(), ResourceManager.GetIconSprite(btnIcon));
		go.get_transform().FindChild("btnText").GetComponent<Text>().set_color((!isLight) ? new Color(1f, 0.843137264f, 0.549019635f) : new Color(1f, 0.980392158f, 0.9019608f));
	}

	private void RefreshCreateGuildPanel()
	{
		this.IsClickArrow = false;
		this.currentRoleMinLv = this.joinLvMinList.get_Item(0);
		this.GuildTransformDic.get_Item(GuildUIState.GuildCreate).FindChild("selectList").get_gameObject().SetActive(this.IsClickArrow);
		this.GuildTransformDic.get_Item(GuildUIState.GuildCreate).FindChild("guildRoleMinLVText").GetComponent<Text>().set_text(this.currentRoleMinLv.ToString());
		this.GuildTransformDic.get_Item(GuildUIState.GuildCreate).FindChild("guildNameText").GetComponent<InputField>().set_text(string.Empty);
		this.GuildTransformDic.get_Item(GuildUIState.GuildCreate).FindChild("guildNoticeText").GetComponent<InputField>().set_text(string.Empty);
		GongHuiDengJi gongHuiDengJi = DataReader<GongHuiDengJi>.Get(1);
		string text = "x" + gongHuiDengJi.num.ToString();
		Items items = DataReader<Items>.Get(gongHuiDengJi.gold);
		if (items != null)
		{
			int id;
			if (gongHuiDengJi.gold < 13)
			{
				id = items.littleIcon;
			}
			else
			{
				id = items.icon;
			}
			Image component = base.FindTransform("CostIcon").GetComponent<Image>();
			ResourceManager.SetSprite(component, GameDataUtils.GetIcon(id));
		}
		if ((long)gongHuiDengJi.num > BackpackManager.Instance.OnGetGoodCount(gongHuiDengJi.gold))
		{
			text = string.Format("<color=#ff0000>{0}</color>", text);
		}
		this.GuildTransformDic.get_Item(GuildUIState.GuildCreate).FindChild("CostRegion").FindChild("CostNum").GetComponent<Text>().set_text(text);
		Transform transform = this.GuildTransformDic.get_Item(GuildUIState.GuildCreate).FindChild("selectList").FindChild("ListselectList").FindChild("Contair");
		if (transform.get_childCount() <= 0)
		{
			for (int i = 0; i < this.joinLvMinList.get_Count(); i++)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GuildButtonItem");
				instantiate2Prefab.set_name("guildItem_" + i);
				GuildButtonItem component2 = instantiate2Prefab.GetComponent<GuildButtonItem>();
				instantiate2Prefab.get_transform().SetParent(transform);
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
				instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelectRoleMinLV);
				component2.Refresh(this.joinLvMinList.get_Item(i));
			}
		}
	}

	private void OnGetGuildInfoListReq()
	{
		this.GuildTransformDic.get_Item(GuildUIState.GuildJoin).FindChild("InputField").GetComponent<InputField>().set_text(string.Empty);
		this.OnSendQueryGuildInfo();
	}

	private void RefreshJoinGuildPanel(List<GuildBriefInfo> guildList)
	{
		Transform transform = this.GuildTransformDic.get_Item(GuildUIState.GuildJoin).FindChild("ListViewGuildInfo").FindChild("Contair");
		int i = 0;
		if (guildList != null && guildList.get_Count() > 0)
		{
			GameObject gameObject = this.GuildTransformDic.get_Item(GuildUIState.GuildJoin).FindChild("NoGuildTipText").get_gameObject();
			if (gameObject != null && gameObject.get_activeSelf())
			{
				gameObject.SetActive(false);
			}
			while (i < guildList.get_Count())
			{
				if (transform.get_childCount() > i)
				{
					GameObject gameObject2 = transform.GetChild(i).get_gameObject();
					gameObject2.SetActive(true);
					GuildInfoItem component = gameObject2.GetComponent<GuildInfoItem>();
					if (component != null)
					{
						component.RefreshUI(guildList.get_Item(i), true);
					}
				}
				else
				{
					GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GuildInfoItem");
					instantiate2Prefab.SetActive(true);
					instantiate2Prefab.set_name("guildItem_" + i);
					GuildInfoItem component2 = instantiate2Prefab.GetComponent<GuildInfoItem>();
					instantiate2Prefab.get_transform().SetParent(transform);
					instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
					component2.RefreshUI(guildList.get_Item(i), true);
				}
				i++;
			}
		}
		else
		{
			GameObject gameObject3 = this.GuildTransformDic.get_Item(GuildUIState.GuildJoin).FindChild("NoGuildTipText").get_gameObject();
			if (gameObject3 != null && !gameObject3.get_activeSelf())
			{
				gameObject3.SetActive(true);
			}
		}
		for (int j = i; j < transform.get_childCount(); j++)
		{
			GameObject gameObject4 = transform.GetChild(j).get_gameObject();
			gameObject4.SetActive(false);
		}
		this.haseRequire = false;
	}

	private void OnReqGuildInfo()
	{
		GuildManager.Instance.SendGetGuildInfoReq();
	}

	private void RefreshGuildInfoPanel()
	{
		this.guildNameText.set_text(GuildManager.Instance.MyGuildnfo.name);
		this.guildLvText.set_text(GuildManager.Instance.MyGuildnfo.lv.ToString());
		this.myGuildNoticeText.set_text(GuildManager.Instance.MyGuildnfo.notice);
		this.guildFightingText.set_text(GuildManager.Instance.MyGuildnfo.totalFighting.ToString());
		this.guildPositionText.set_text(GuildManager.Instance.GetTitleName(GuildManager.Instance.MyMemberInfo.title.get_Item(0)));
		this.guildAttributionText.set_text(GameDataUtils.GetChineseContent(515053, false) + "：" + GuildManager.Instance.MyMemberInfo.contribution.ToString());
		base.FindTransform("BtnTitleHide").FindChild("BtnHide").get_gameObject().SetActive(GuildManager.Instance.IsHideTitle);
		this.UpdateGuildMemberNumText();
	}

	private void UpdateGuildMemberNumText()
	{
		int lv = GuildManager.Instance.MyGuildnfo.lv;
		int num = GuildManager.Instance.MyGuildnfo.memberSize;
		GongHuiDengJi gongHuiDengJi = DataReader<GongHuiDengJi>.Get(lv);
		if (gongHuiDengJi != null)
		{
			num = gongHuiDengJi.member;
		}
		this.guildMemberNumText.set_text(GuildManager.Instance.MyGuildnfo.memberSize + "/" + num);
	}

	protected void UpdateStateInfo()
	{
		string[] times = DataReader<GongHuiXinXi>.Get("QuestionTime").value.Split(new char[]
		{
			','
		});
		string[] array = times[0].Split(new char[]
		{
			':'
		});
		int num = int.Parse((!array[0].StartsWith("0")) ? array[0] : array[0].Substring(1));
		int num2 = int.Parse((!array[1].StartsWith("0")) ? array[1] : array[1].Substring(1));
		DateTime dateTimeStart = new DateTime(DateTime.get_Now().get_Year(), DateTime.get_Now().get_Month(), DateTime.get_Now().get_Day(), num, num2, 0);
		string[] array2 = times[1].Split(new char[]
		{
			':'
		});
		int num3 = int.Parse((!array2[0].StartsWith("0")) ? array2[0] : array2[0].Substring(1));
		int num4 = int.Parse((!array2[1].StartsWith("0")) ? array2[1] : array2[1].Substring(1));
		DateTime dateTimeEnd = new DateTime(DateTime.get_Now().get_Year(), DateTime.get_Now().get_Month(), DateTime.get_Now().get_Day(), num3, num4, 0);
		this.activityStateText.set_text(string.Empty);
		this.t = TimerHeap.AddTimer(0u, 2000, delegate
		{
			if (dateTimeStart <= DateTime.get_Now() && DateTime.get_Now() <= dateTimeEnd)
			{
				this.activityStateText.set_text("竞答：进行中");
			}
			else
			{
				this.activityStateText.set_text(string.Format("竞答：{0}--{1}", times[0], times[1]));
			}
		});
	}

	protected void UpdateGuildBossTipsState()
	{
		bool isCallGuildBoss = GuildBossManager.Instance.IsCallGuildBoss;
		if (this.GuildBossTipsObj.get_activeSelf() != isCallGuildBoss)
		{
			this.GuildBossTipsObj.SetActive(isCallGuildBoss);
		}
	}

	private void OnGuildInfoNty()
	{
		this.ResetUI();
	}

	private void OnDissolveGuildRes()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	private void OnUpdateSearchGuildList()
	{
		this.isSearch = true;
		this.currentPage = GuildManager.Instance.GetApplicationGuilds(false, true).get_Count() / 9;
		this.RefreshJoinGuildPanel(GuildManager.Instance.GetApplicationGuilds(false, true));
	}

	private void OnUpdateGuildInfoList()
	{
		this.isSearch = false;
		this.currentPage = GuildManager.Instance.GetApplicationGuilds(false, false).get_Count() / 10;
		this.RefreshJoinGuildPanel(GuildManager.Instance.GetApplicationGuilds(false, false));
	}

	private void OnUpdateGuildInfo()
	{
		this.RefreshGuildInfoPanel();
	}

	private void OnUpdateGuildNotice()
	{
		this.myGuildNoticeText.set_text(GuildManager.Instance.MyGuildnfo.notice);
	}

	private void OnUpgradeGuildRes()
	{
		this.RefreshGuildInfoPanel();
	}

	private void OnGuildInfoChangeNty()
	{
		this.RefreshUIState(this.currentState);
		this.RefreshGuildInfoPanel();
	}

	private void OnUpdateGuildMemberList()
	{
		this.UpdateGuildMemberNumText();
	}

	private void OnGuildWarStepNty()
	{
		string text = string.Empty;
		if (GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.HALF_MATCH2_END && GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.FINAL_MATCH_END)
		{
			text = GuildWarManager.Instance.GetGuildWarOpenTime(4, "FFEB4B");
		}
		else if (GuildWarManager.Instance.GuildWarTimeStep >= GuildWarTimeStep.GWTS.HALF_MATCH1_END && GuildWarManager.Instance.GuildWarTimeStep < GuildWarTimeStep.GWTS.HALF_MATCH2_END)
		{
			text = GuildWarManager.Instance.GetGuildWarOpenTime(3, "FFEB4B");
		}
		else
		{
			text = GuildWarManager.Instance.GetGuildWarOpenTime(2, "FFEB4B");
		}
		base.FindTransform("GuildWarOpenTimeText").GetComponent<Text>().set_text(text);
	}
}
