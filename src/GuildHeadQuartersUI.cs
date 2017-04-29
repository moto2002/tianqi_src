using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuildHeadQuartersUI : UIBase
{
	private ButtonCustom guildInfoBtn;

	private ButtonCustom guildMemberBtn;

	private ButtonCustom otherGuildBtn;

	private ButtonCustom quitBtn;

	private ButtonCustom managerBtn;

	private ButtonCustom guildUpgradeBtn;

	private Slider guildProgressSlider;

	private GuildUIState currentState;

	private Transform listViewGuildMemberContair;

	private Transform listViewGuildLogContair;

	private ListPool otherGuildsListPool;

	private Image guildImageIcon;

	private GameObject guildMemberRoot;

	private GameObject guildLogRoot;

	private GameObject otherGuildRoot;

	private int currentPage = 1;

	private bool haseRequire;

	private Text guildMemberNumText;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.isClick = true;
		this.alpha = 0.7f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.managerBtn = base.FindTransform("BtnManager").GetComponent<ButtonCustom>();
		this.managerBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickManagerGuildBtn);
		this.guildUpgradeBtn = base.FindTransform("BtnLevelUp").GetComponent<ButtonCustom>();
		this.guildUpgradeBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildLevelUpBtn);
		this.quitBtn = base.FindTransform("BtnQuit").GetComponent<ButtonCustom>();
		this.quitBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickQuitGuildBtn);
		this.guildInfoBtn = base.FindTransform("InfoBtn").GetComponent<ButtonCustom>();
		this.guildInfoBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTab);
		this.guildMemberBtn = base.FindTransform("MemberBtn").GetComponent<ButtonCustom>();
		this.guildMemberBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTab);
		this.otherGuildBtn = base.FindTransform("OtherGuildBtn").GetComponent<ButtonCustom>();
		this.otherGuildBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTab);
		this.guildImageIcon = base.FindTransform("ImageIcon").GetComponent<Image>();
		this.listViewGuildMemberContair = base.FindTransform("ListViewGuildMemberInfo").FindChild("Contair");
		this.listViewGuildLogContair = base.FindTransform("ListViewGuildLogInfo").FindChild("Contair");
		this.otherGuildsListPool = base.FindTransform("ListViewOtherGuildInfo").FindChild("Contair").GetComponent<ListPool>();
		this.otherGuildsListPool.Clear();
		this.guildLogRoot = base.FindTransform("GuildLogRoot").get_gameObject();
		this.guildMemberRoot = base.FindTransform("GuildMemberRoot").get_gameObject();
		this.otherGuildRoot = base.FindTransform("OtherGuildRoot").get_gameObject();
		this.guildProgressSlider = base.FindTransform("NextLevelTip").GetComponent<Slider>();
		this.guildMemberNumText = base.FindTransform("guildMemberNumText").GetComponent<Text>();
		base.FindTransform("guildTitleDescBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGuildTitleDescBtn);
		this.guildInfoBtn.get_transform().FindChild("btnText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515027, false));
		this.guildMemberBtn.get_transform().FindChild("btnText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(515031, false));
		base.FindTransform("ListViewOtherGuildInfo").GetComponent<ScrollRectCustom>().onDrag = delegate(PointerEventData data)
		{
			float verticalNormalizedPosition = base.FindTransform("ListViewOtherGuildInfo").GetComponent<ScrollRectCustom>().verticalNormalizedPosition;
			if (verticalNormalizedPosition <= 0f && !this.haseRequire)
			{
				if (GuildManager.Instance.CanNotRequire)
				{
					return;
				}
				this.haseRequire = true;
				this.currentPage++;
				this.OnSendGetOtherGuildsListReq();
			}
		};
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.currentState = GuildUIState.GuildLog;
		this.OnClickTab(this.guildInfoBtn.get_gameObject());
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void OnClickCloseBtn(GameObject go)
	{
		this.OnDissolveGuildRes();
	}

	protected override void OnClickMaskAction()
	{
		this.OnDissolveGuildRes();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateGuildLogList, new Callback(this.OnUpdateGuildLogList));
		EventDispatcher.AddListener(EventNames.UpdateGuildMemberList, new Callback(this.OnUpdateGuildMemberList));
		EventDispatcher.AddListener(EventNames.UpdateGuildInfo, new Callback(this.OnUpdateGuildMemberList));
		EventDispatcher.AddListener(EventNames.OnDissolveGuildRes, new Callback(this.OnDissolveGuildRes));
		EventDispatcher.AddListener(EventNames.OnUpgradeGuildRes, new Callback(this.OnUpgradeGuildRes));
		EventDispatcher.AddListener(EventNames.OnGuildInfoChangeNty, new Callback(this.OnGuildInfoChangeNty));
		EventDispatcher.AddListener(EventNames.UpdateGuildInfoList, new Callback(this.OnUpdateOtherGuildsList));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateGuildLogList, new Callback(this.OnUpdateGuildLogList));
		EventDispatcher.RemoveListener(EventNames.UpdateGuildMemberList, new Callback(this.OnUpdateGuildMemberList));
		EventDispatcher.RemoveListener(EventNames.UpdateGuildInfo, new Callback(this.OnUpdateGuildMemberList));
		EventDispatcher.RemoveListener(EventNames.OnDissolveGuildRes, new Callback(this.OnDissolveGuildRes));
		EventDispatcher.RemoveListener(EventNames.OnUpgradeGuildRes, new Callback(this.OnUpgradeGuildRes));
		EventDispatcher.RemoveListener(EventNames.OnGuildInfoChangeNty, new Callback(this.OnGuildInfoChangeNty));
		EventDispatcher.RemoveListener(EventNames.UpdateGuildInfoList, new Callback(this.OnUpdateOtherGuildsList));
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			GuildManager.Instance.OwnApplicationGuilds = null;
			base.ReleaseSelf(true);
		}
	}

	private void RefreshUI()
	{
		this.RefreshGuildBtns();
		if (this.currentState == GuildUIState.GuildMember)
		{
			this.guildMemberRoot.SetActive(true);
			this.guildLogRoot.SetActive(false);
			this.otherGuildRoot.SetActive(false);
		}
		else if (this.currentState == GuildUIState.GuildLog)
		{
			this.guildMemberRoot.SetActive(false);
			this.guildLogRoot.SetActive(true);
			this.otherGuildRoot.SetActive(false);
			this.UpdateGuildInfoUI();
		}
		else if (this.currentState == GuildUIState.OtherGuild)
		{
			this.guildMemberRoot.SetActive(false);
			this.guildLogRoot.SetActive(false);
			this.otherGuildRoot.SetActive(true);
		}
	}

	private void RefreshGuildBtns()
	{
		bool active = GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.LevelUpGuild);
		this.managerBtn.get_gameObject().SetActive(active);
		if (GuildManager.Instance.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.Chairman)
		{
			this.managerBtn.get_gameObject().SetActive(true);
			this.quitBtn.get_gameObject().SetActive(false);
		}
		else
		{
			this.quitBtn.get_gameObject().SetActive(true);
			this.managerBtn.get_gameObject().SetActive(false);
		}
	}

	private void SetBtnLightAndDim(GameObject go, string btnIcon, bool isLight)
	{
		ResourceManager.SetSprite(go.get_transform().GetComponent<Image>(), ResourceManager.GetIconSprite(btnIcon));
		go.get_transform().FindChild("btnText").GetComponent<Text>().set_color((!isLight) ? new Color(1f, 0.843137264f, 0.549019635f) : new Color(1f, 0.980392158f, 0.9019608f));
	}

	private void UpdateGuildInfoUI()
	{
		if (GuildManager.Instance.MyGuildnfo == null)
		{
			return;
		}
		bool flag = GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.LevelUpGuild);
		GongHuiDengJi gongHuiDengJi = null;
		if (DataReader<GongHuiDengJi>.Contains(GuildManager.Instance.MyGuildnfo.lv + 1))
		{
			gongHuiDengJi = DataReader<GongHuiDengJi>.Get(GuildManager.Instance.MyGuildnfo.lv + 1);
		}
		if (gongHuiDengJi == null)
		{
			flag = false;
		}
		if (flag)
		{
			long num = BackpackManager.Instance.OnGetGoodCount(gongHuiDengJi.gold);
			flag = (num >= (long)gongHuiDengJi.num);
		}
		GongHuiDengJi gongHuiDengJi2 = null;
		if (DataReader<GongHuiDengJi>.Contains(GuildManager.Instance.MyGuildnfo.lv))
		{
			gongHuiDengJi2 = DataReader<GongHuiDengJi>.Get(GuildManager.Instance.MyGuildnfo.lv);
			ResourceManager.SetSprite(this.guildImageIcon, GameDataUtils.GetIcon(gongHuiDengJi2.icon));
		}
		Image component = base.FindTransform("guildMoneyIcon").GetComponent<Image>();
		int key = 0;
		if (gongHuiDengJi != null)
		{
			key = gongHuiDengJi.gold;
		}
		else if (gongHuiDengJi2 != null)
		{
			key = gongHuiDengJi2.gold;
		}
		if (DataReader<Items>.Contains(key))
		{
			Items items = DataReader<Items>.Get(key);
			ResourceManager.SetSprite(component, GameDataUtils.GetIcon(items.icon));
		}
		base.FindTransform("BtnLevelUp").get_gameObject().SetActive(flag);
		this.SetGuildFundProgressBar();
	}

	private void SetGuildFundProgressBar()
	{
		if (GuildManager.Instance.MyGuildnfo == null)
		{
			return;
		}
		int lv = GuildManager.Instance.MyGuildnfo.lv;
		GongHuiDengJi gongHuiDengJi = null;
		if (DataReader<GongHuiDengJi>.Contains(lv + 1))
		{
			gongHuiDengJi = DataReader<GongHuiDengJi>.Get(lv + 1);
		}
		long num;
		if (gongHuiDengJi == null)
		{
			num = (long)GuildManager.Instance.MyGuildnfo.guildFund;
			this.guildProgressSlider.get_transform().FindChild("NextSuccessRatio").GetComponent<Text>().set_text(num.ToString());
			this.guildProgressSlider.set_value(1f);
			base.FindTransform("BtnLevelUp").get_gameObject().SetActive(false);
			return;
		}
		num = BackpackManager.Instance.OnGetGoodCount(gongHuiDengJi.gold);
		this.guildProgressSlider.get_transform().FindChild("NextSuccessRatio").GetComponent<Text>().set_text(num.ToString() + "/" + gongHuiDengJi.num.ToString());
		float value = ((float)num / ((float)gongHuiDengJi.num * 1f) < 1f) ? ((float)num / ((float)gongHuiDengJi.num * 1f)) : 1f;
		this.guildProgressSlider.set_value(value);
	}

	private void OnSendGetGuildLogListReq()
	{
		GuildManager.Instance.SendGetGuildLogReq(0);
	}

	private void OnSendGetGuildMemberListReq()
	{
		GuildManager.Instance.SendGetGuildInfoReq();
	}

	private void OnSendGetOtherGuildsListReq()
	{
		if (this.currentPage <= 0)
		{
			this.currentPage = 1;
		}
		int fromIndex = (this.currentPage - 1) * 10;
		int toIndex = this.currentPage * 10 - 1;
		GuildManager.Instance.SendQueryGuildInfoReq(fromIndex, toIndex, false);
	}

	private void RefreshGuildLogPanel()
	{
		List<GuildLogTrace> guildLogList = GuildManager.Instance.GuildLogList;
		int i;
		for (i = 0; i < guildLogList.get_Count(); i++)
		{
			if (this.listViewGuildLogContair.get_childCount() > i)
			{
				GameObject gameObject = this.listViewGuildLogContair.GetChild(i).get_gameObject();
				if (gameObject != null && gameObject.GetComponent<GuildLogItem>() != null)
				{
					gameObject.SetActive(true);
					gameObject.GetComponent<GuildLogItem>().RefreshUI(guildLogList.get_Item(i));
				}
			}
			else
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GuildLogItem");
				instantiate2Prefab.set_name("guildLogItem_" + i);
				GuildLogItem component = instantiate2Prefab.GetComponent<GuildLogItem>();
				instantiate2Prefab.get_transform().SetParent(this.listViewGuildLogContair);
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
				component.RefreshUI(guildLogList.get_Item(i));
			}
		}
		for (int j = i; j < this.listViewGuildLogContair.get_childCount(); j++)
		{
			GameObject gameObject2 = this.listViewGuildLogContair.GetChild(j).get_gameObject();
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
	}

	private void RefreshGuildMemberPanel()
	{
		List<MemberInfo> myGuildMemberList = GuildManager.Instance.MyGuildMemberList;
		if (myGuildMemberList == null)
		{
			return;
		}
		int i;
		for (i = 0; i < myGuildMemberList.get_Count(); i++)
		{
			if (this.listViewGuildMemberContair.get_childCount() > i)
			{
				GameObject gameObject = this.listViewGuildMemberContair.GetChild(i).get_gameObject();
				if (gameObject != null && gameObject.GetComponent<GuildMemberItem>() != null)
				{
					gameObject.SetActive(true);
					gameObject.GetComponent<GuildMemberItem>().RefreshUI(myGuildMemberList.get_Item(i));
				}
			}
			else
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GuildMemberItem");
				instantiate2Prefab.set_name("guildMemberItem_" + i);
				GuildMemberItem component = instantiate2Prefab.GetComponent<GuildMemberItem>();
				instantiate2Prefab.get_transform().SetParent(this.listViewGuildMemberContair);
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
				component.RefreshUI(myGuildMemberList.get_Item(i));
			}
		}
		for (int j = i; j < this.listViewGuildMemberContair.get_childCount(); j++)
		{
			GameObject gameObject2 = this.listViewGuildMemberContair.GetChild(j).get_gameObject();
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
		this.UpdateGuildMemberNumText();
	}

	private void RefreshOtherGuildList(List<GuildBriefInfo> otherGuilds)
	{
		this.otherGuildsListPool.Clear();
		if (otherGuilds != null && otherGuilds.get_Count() > 0)
		{
			this.otherGuildsListPool.Create(otherGuilds.get_Count(), delegate(int index)
			{
				if (index < otherGuilds.get_Count() && index < this.otherGuildsListPool.Items.get_Count())
				{
					GuildRankInfoItem component = this.otherGuildsListPool.Items.get_Item(index).GetComponent<GuildRankInfoItem>();
					if (component != null)
					{
						component.UpdateItem(otherGuilds.get_Item(index));
					}
				}
			});
		}
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

	private void OnClickTab(GameObject go)
	{
		if (go == this.guildInfoBtn.get_gameObject())
		{
			this.SetBtnLightAndDim(go, "y_fenye3", true);
			this.SetBtnLightAndDim(this.guildMemberBtn.get_gameObject(), "y_fenye4", false);
			this.SetBtnLightAndDim(this.otherGuildBtn.get_gameObject(), "y_fenye4", false);
			this.currentState = GuildUIState.GuildLog;
			this.OnSendGetGuildLogListReq();
		}
		else if (go == this.guildMemberBtn.get_gameObject())
		{
			this.SetBtnLightAndDim(go, "y_fenye3", true);
			this.SetBtnLightAndDim(this.guildInfoBtn.get_gameObject(), "y_fenye4", false);
			this.SetBtnLightAndDim(this.otherGuildBtn.get_gameObject(), "y_fenye4", false);
			this.currentState = GuildUIState.GuildMember;
			this.OnSendGetGuildMemberListReq();
		}
		else if (go == this.otherGuildBtn.get_gameObject())
		{
			this.SetBtnLightAndDim(go, "y_fenye3", true);
			this.SetBtnLightAndDim(this.guildInfoBtn.get_gameObject(), "y_fenye4", false);
			this.SetBtnLightAndDim(this.guildMemberBtn.get_gameObject(), "y_fenye4", false);
			this.currentState = GuildUIState.OtherGuild;
			base.FindTransform("ListViewOtherGuildInfo").GetComponent<ScrollRectCustom>().verticalNormalizedPosition = 1f;
			this.currentPage = 1;
			this.OnSendGetOtherGuildsListReq();
		}
	}

	private void OnClickManagerGuildBtn(GameObject go)
	{
		if (GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.NoticeSetting))
		{
			UIManagerControl.Instance.OpenUI("GuildSettingUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		}
	}

	private void OnClickGuildLevelUpBtn(GameObject go)
	{
		if (!GuildManager.Instance.CheckMemberHasPrivilege(GuildPrivilegeState.LevelUpGuild))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515010, false));
			return;
		}
		GongHuiDengJi gongHuiDengJi = DataReader<GongHuiDengJi>.Get(GuildManager.Instance.MyGuildnfo.lv + 1);
		if (gongHuiDengJi == null)
		{
			return;
		}
		string content = string.Format(GameDataUtils.GetChineseContent(515024, false), gongHuiDengJi.num);
		string chineseContent = GameDataUtils.GetChineseContent(621264, false);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(chineseContent, content, null, delegate
		{
			GuildManager.Instance.SendUpgradeGuildReq();
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void OnClickQuitGuildBtn(GameObject go)
	{
		if (GuildManager.Instance.MyMemberInfo.title.get_Item(0) == MemberTitleType.MTT.Chairman)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(515025, false));
			return;
		}
		string tipContentByKey = GuildManager.Instance.GetTipContentByKey("quit");
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), tipContentByKey, null, delegate
		{
			GuildManager.Instance.SendGuildExitReq();
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void OnClickGuildTitleDescBtn(GameObject go)
	{
		SpecialInstanceDescUI.Open(UINodesManager.MiddleUIRoot, 515108, 515109);
	}

	private void OnUpdateGuildLogList()
	{
		this.RefreshUI();
		this.RefreshGuildLogPanel();
	}

	private void OnUpdateGuildMemberList()
	{
		this.RefreshUI();
		this.RefreshGuildMemberPanel();
	}

	private void OnUpdateOtherGuildsList()
	{
		this.RefreshUI();
		int num = (GuildManager.Instance.OwnApplicationGuilds == null) ? 0 : GuildManager.Instance.OwnApplicationGuilds.get_Count();
		this.currentPage = num / 9;
		this.RefreshOtherGuildList(GuildManager.Instance.GetOtherGuildInfoList());
	}

	private void OnUpgradeGuildRes()
	{
		this.RefreshUI();
		this.UpdateGuildInfoUI();
	}

	private void OnGuildInfoChangeNty()
	{
		if (this.currentState == GuildUIState.GuildMember)
		{
			this.RefreshGuildMemberPanel();
		}
	}

	private void OnDissolveGuildRes()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}
}
