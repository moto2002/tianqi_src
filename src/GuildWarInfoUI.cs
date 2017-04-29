using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildWarInfoUI : UIBase
{
	public enum GuildWarInfoBtnState
	{
		MyGuildWarInfoBtn,
		GuildWarBattleInfo
	}

	private ButtonCustom battleInfoBtn;

	private ButtonCustom myGuildInfoBtn;

	private GameObject myGuildMemberInfoPanel;

	private GameObject guildWarBattleInfoPanel;

	private ListPool warInfoItemsListPool;

	private GuildWarInfoUI.GuildWarInfoBtnState currentBtnState;

	private List<Transform> myGuildMemberList;

	private List<Transform> enemyGuildMemberList;

	private List<Transform> resourceTransList;

	private Text resourceInfoText;

	private int currentResourceID;

	private int m_fxID;

	private RectTransform mFlagSelf;

	private GetMemberInGuildWarRes currentResourceInfo;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.isClick = true;
		this.isEndNav = false;
		this.alpha = 0.7f;
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mFlagSelf = base.FindTransform("FlagSelf").GetComponent<RectTransform>();
		this.myGuildMemberInfoPanel = base.FindTransform("MyGuildMemberInfoPanel").get_gameObject();
		this.guildWarBattleInfoPanel = base.FindTransform("GuildWarBattleInfoPanel").get_gameObject();
		this.battleInfoBtn = base.FindTransform("BattleInfoBtn").GetComponent<ButtonCustom>();
		this.battleInfoBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTabBtn);
		this.myGuildInfoBtn = base.FindTransform("MyGuildInfoBtn").GetComponent<ButtonCustom>();
		this.myGuildInfoBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTabBtn);
		base.FindTransform("GoToResourceBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGoToResourceBtn);
		this.resourceInfoText = base.FindTransform("ResourceInfoText").GetComponent<Text>();
		this.warInfoItemsListPool = base.FindTransform("WarInfoItemsListPool").GetComponent<ListPool>();
		this.warInfoItemsListPool.SetItem("GuildWarInfoItem");
		this.resourceTransList = new List<Transform>();
		for (int i = 0; i < 5; i++)
		{
			Transform transform = base.FindTransform("Resource" + (i + 1));
			if (transform != null)
			{
				this.resourceTransList.Add(transform);
				transform.FindChild("ResourceButton").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickResourceBtn);
			}
		}
		this.myGuildMemberList = new List<Transform>();
		for (int j = 0; j < 3; j++)
		{
			Transform transform2 = base.FindTransform("MyGuildMemberList").FindChild("GuildWarResourceMemberItem" + (j + 1));
			if (transform2 != null)
			{
				this.myGuildMemberList.Add(transform2);
			}
		}
		this.enemyGuildMemberList = new List<Transform>();
		for (int k = 0; k < 3; k++)
		{
			Transform transform3 = base.FindTransform("EnemyGuildMemberList").FindChild("GuildWarResourceMemberItem" + (k + 1));
			if (transform3 != null)
			{
				this.enemyGuildMemberList.Add(transform3);
			}
		}
		this.resourceInfoText.set_text(string.Empty);
		this.RefreshGuildNameLeft();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		FXSpineManager.Instance.DeleteSpine(this.m_fxID, true);
		this.warInfoItemsListPool.Clear();
		this.currentBtnState = GuildWarInfoUI.GuildWarInfoBtnState.GuildWarBattleInfo;
		this.RefreshUI();
		this.UpdateAllMineLiveData();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.warInfoItemsListPool.Clear();
		FXSpineManager.Instance.DeleteSpine(this.m_fxID, true);
	}

	private void Update()
	{
		this.mFlagSelf.set_anchoredPosition(RadarManager.Instance.GetSelfPosInMap(RadarManager.size_mapImage_guildwar));
		this.mFlagSelf.set_localEulerAngles(new Vector3(0f, 0f, RadarManager.Instance.GetSelfRotationZ()));
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetMyGuildMembersInGuildWar, new Callback(this.OnGetMyGuildMembersInGuildWar));
		EventDispatcher.AddListener<GetMemberInGuildWarRes>(EventNames.OnUpdateCurrentResourceInfo, new Callback<GetMemberInGuildWarRes>(this.OnUpdateCurrentResourceInfo));
		EventDispatcher.AddListener(GuildWarManagerEvent.UpdateAllMineLiveData, new Callback(this.UpdateAllMineLiveData));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetMyGuildMembersInGuildWar, new Callback(this.OnGetMyGuildMembersInGuildWar));
		EventDispatcher.RemoveListener<GetMemberInGuildWarRes>(EventNames.OnUpdateCurrentResourceInfo, new Callback<GetMemberInGuildWarRes>(this.OnUpdateCurrentResourceInfo));
		EventDispatcher.RemoveListener(GuildWarManagerEvent.UpdateAllMineLiveData, new Callback(this.UpdateAllMineLiveData));
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void OnClickTabBtn(GameObject go)
	{
		if (go == this.myGuildInfoBtn.get_gameObject())
		{
			this.currentBtnState = GuildWarInfoUI.GuildWarInfoBtnState.MyGuildWarInfoBtn;
		}
		else if (go == this.battleInfoBtn.get_gameObject())
		{
			this.currentBtnState = GuildWarInfoUI.GuildWarInfoBtnState.GuildWarBattleInfo;
		}
		this.RefreshUI();
	}

	private void OnClickGoToResourceBtn(GameObject go)
	{
		if (this.currentResourceID <= 0)
		{
			return;
		}
		GuildWarManager.Instance.NavToMine(this.currentResourceID);
		this.Show(false);
	}

	private void OnClickResourceBtn(GameObject go)
	{
		FXSpineManager.Instance.DeleteSpine(this.m_fxID, true);
		int num = this.resourceTransList.FindIndex((Transform a) => a.FindChild("ResourceButton").get_gameObject() == go);
		if (num >= 0)
		{
			this.currentResourceID = num + 1;
			this.m_fxID = FXSpineManager.Instance.PlaySpine(115, this.resourceTransList.get_Item(num), "GuildWarInfoUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.RefreshGuildBattleInfoPanel(this.currentResourceID);
		}
	}

	private void RefreshUI()
	{
		this.RefreshUIByType(this.currentBtnState);
		this.RefreshBtnByType(this.currentBtnState);
	}

	private void RefreshUIByType(GuildWarInfoUI.GuildWarInfoBtnState type)
	{
		FXSpineManager.Instance.DeleteSpine(this.m_fxID, true);
		if (type == GuildWarInfoUI.GuildWarInfoBtnState.GuildWarBattleInfo)
		{
			int num = GuildWarManager.Instance.LastBattleNo;
			if (GuildWarManager.Instance.LastBattleNo <= 0 || GuildWarManager.Instance.LastBattleNo > 5)
			{
				num = 3;
			}
			this.OnClickResourceBtn(this.resourceTransList.get_Item(num - 1).FindChild("ResourceButton").get_gameObject());
			if (!this.guildWarBattleInfoPanel.get_activeSelf())
			{
				this.guildWarBattleInfoPanel.SetActive(true);
			}
			if (this.myGuildMemberInfoPanel.get_activeSelf())
			{
				this.myGuildMemberInfoPanel.SetActive(false);
			}
		}
		else if (type == GuildWarInfoUI.GuildWarInfoBtnState.MyGuildWarInfoBtn)
		{
			this.RefreshMyGuildMemberPanel();
			if (this.guildWarBattleInfoPanel.get_activeSelf())
			{
				this.guildWarBattleInfoPanel.SetActive(false);
			}
			if (!this.myGuildMemberInfoPanel.get_activeSelf())
			{
				this.myGuildMemberInfoPanel.SetActive(true);
			}
		}
	}

	private void RefreshBtnByType(GuildWarInfoUI.GuildWarInfoBtnState type)
	{
		if (type == GuildWarInfoUI.GuildWarInfoBtnState.GuildWarBattleInfo)
		{
			this.SetBtnLightAndDim(this.myGuildInfoBtn.get_gameObject(), "y_fenye4", false);
			this.SetBtnLightAndDim(this.battleInfoBtn.get_gameObject(), "y_fenye3", true);
		}
		else if (type == GuildWarInfoUI.GuildWarInfoBtnState.MyGuildWarInfoBtn)
		{
			this.SetBtnLightAndDim(this.myGuildInfoBtn.get_gameObject(), "y_fenye3", true);
			this.SetBtnLightAndDim(this.battleInfoBtn.get_gameObject(), "y_fenye4", false);
		}
	}

	private void SetBtnLightAndDim(GameObject go, string btnIcon, bool isLight)
	{
		ResourceManager.SetSprite(go.get_transform().GetComponent<Image>(), ResourceManager.GetIconSprite(btnIcon));
		go.get_transform().FindChild("btnText").GetComponent<Text>().set_color((!isLight) ? new Color(1f, 0.843137264f, 0.549019635f) : new Color(1f, 0.980392158f, 0.9019608f));
	}

	private void RefreshMyGuildMemberPanel()
	{
		GuildWarManager.Instance.SendGetMemberInGuildReq(-1);
	}

	private void UpdateMyGuildMemberList()
	{
		this.warInfoItemsListPool.Clear();
		if (GuildWarManager.Instance.MyGuildMemberInSceneList != null && GuildWarManager.Instance.MyGuildMemberInSceneList.get_Count() > 0)
		{
			this.warInfoItemsListPool.Create(GuildWarManager.Instance.MyGuildMemberInSceneList.get_Count(), delegate(int index)
			{
				if (index < GuildWarManager.Instance.MyGuildMemberInSceneList.get_Count() && index < this.warInfoItemsListPool.Items.get_Count())
				{
					GuildWarInfoItem component = this.warInfoItemsListPool.Items.get_Item(index).GetComponent<GuildWarInfoItem>();
					if (component != null)
					{
						GuildMemberInfoInGuildWarScene memberInfo = GuildWarManager.Instance.MyGuildMemberInSceneList.get_Item(index);
						component.UpdateItem(memberInfo, index + 1);
					}
				}
			});
		}
	}

	private void RefreshGuildNameLeft()
	{
		bool isMyGuildInSceneLeft = GuildWarManager.Instance.IsMyGuildInSceneLeft;
		if (GuildWarManager.Instance.EnemyGuildWarResourceInfo != null && GuildWarManager.Instance.MyGuildWarResourceInfo != null)
		{
			base.FindTransform("GuildNameTextL").GetComponent<Text>().set_text((!isMyGuildInSceneLeft) ? TextColorMgr.GetColor(GuildWarManager.Instance.EnemyGuildWarResourceInfo.GuildName, "FF4141", string.Empty) : TextColorMgr.GetColor(GuildWarManager.Instance.MyGuildWarResourceInfo.GuildName, "3FCB19", string.Empty));
			base.FindTransform("GuildNameTextR").GetComponent<Text>().set_text(isMyGuildInSceneLeft ? TextColorMgr.GetColor(GuildWarManager.Instance.EnemyGuildWarResourceInfo.GuildName, "FF4141", string.Empty) : TextColorMgr.GetColor(GuildWarManager.Instance.MyGuildWarResourceInfo.GuildName, "3FCB19", string.Empty));
			for (int i = 0; i < this.resourceTransList.get_Count(); i++)
			{
				if (i != 2)
				{
					Transform transform = this.resourceTransList.get_Item(i).FindChild("ResourceButton");
					if (!(transform != null) || transform.get_childCount() > 1)
					{
					}
				}
			}
		}
	}

	private void UpdateMineInfoTitleText(int resourceID)
	{
		GuildWarManager.MineState mineState = GuildWarManager.Instance.GetMineState(resourceID);
		if (this.resourceInfoText != null)
		{
			if (GuildWarManager.Instance.GuildWarResourceBriefDic != null && GuildWarManager.Instance.GuildWarResourceBriefDic.ContainsKey(resourceID) && mineState != GuildWarManager.MineState.NoData && mineState != GuildWarManager.MineState.None)
			{
				string text = (mineState != GuildWarManager.MineState.Enemy) ? GuildWarManager.Instance.MyGuildName : GuildWarManager.Instance.EnemyGuildName;
				this.resourceInfoText.set_text(GameDataUtils.GetChineseContent(DataReader<JunTuanZhanCaiJi>.Get(resourceID).Name, false) + " " + text);
			}
			else
			{
				this.resourceInfoText.set_text(GameDataUtils.GetChineseContent(DataReader<JunTuanZhanCaiJi>.Get(resourceID).Name, false) + " 无主据点");
			}
		}
	}

	private void UpdateResourceBattleInfo(int ResourceID)
	{
		int i = 0;
		int j = 0;
		if (this.currentResourceInfo != null && this.currentResourceInfo.inResourceId > 0)
		{
			this.UpdateMineInfoTitleText(ResourceID);
			int num = (this.currentResourceInfo.myMembersInfo == null) ? 0 : this.currentResourceInfo.myMembersInfo.get_Count();
			int num2 = (this.currentResourceInfo.faceMembersInfo == null) ? 0 : this.currentResourceInfo.faceMembersInfo.get_Count();
			while (i < num)
			{
				MemberInGuildScene memberInfo = this.currentResourceInfo.myMembersInfo.get_Item(i);
				this.UpdateGuildMemberInfo(this.myGuildMemberList.get_Item(i), memberInfo);
				i++;
			}
			while (j < num2)
			{
				MemberInGuildScene memberInfo2 = this.currentResourceInfo.faceMembersInfo.get_Item(j);
				this.UpdateGuildMemberInfo(this.enemyGuildMemberList.get_Item(j), memberInfo2);
				j++;
			}
		}
		for (int k = i; k < 3; k++)
		{
			if (k < this.myGuildMemberList.get_Count())
			{
				this.UpdateGuildMemberInfo(this.myGuildMemberList.get_Item(k), null);
			}
		}
		for (int l = j; l < 3; l++)
		{
			if (l < this.enemyGuildMemberList.get_Count())
			{
				this.UpdateGuildMemberInfo(this.enemyGuildMemberList.get_Item(l), null);
			}
		}
	}

	private void UpdateMineDataByID(int resourceID = 1)
	{
		GuildWarManager.MineState mineState = GuildWarManager.Instance.GetMineState(resourceID);
		if (GuildWarManager.Instance.GuildWarResourceBriefDic != null && GuildWarManager.Instance.GuildWarResourceBriefDic.ContainsKey(resourceID))
		{
			int myMemberCount = GuildWarManager.Instance.GuildWarResourceBriefDic[resourceID].myMemberCount;
			int faceMemberCount = GuildWarManager.Instance.GuildWarResourceBriefDic[resourceID].faceMemberCount;
			Text component = base.FindTransform("Resource" + resourceID).FindChild("ResourceTitle").FindChild("ResourceTitleL").GetComponent<Text>();
			if (component != null)
			{
				component.set_text(myMemberCount + string.Empty);
			}
			Text component2 = base.FindTransform("Resource" + resourceID).FindChild("ResourceTitle").FindChild("ResourceTitleR").GetComponent<Text>();
			if (component2 != null)
			{
				component2.set_text(faceMemberCount + string.Empty);
			}
			Image component3 = base.FindTransform("Resource" + resourceID).FindChild("ResourceButton").GetComponent<Image>();
			if (component3 != null)
			{
				ResourceManager.SetCodeSprite(component3, this.GetMinePointImageName(mineState, resourceID));
			}
			if (resourceID != 3)
			{
				Transform transform = base.FindTransform("Resource" + resourceID).FindChild("ResourceButton").FindChild("ResourceText");
				if (transform != null)
				{
					this.SetMinePointText(transform, mineState);
				}
			}
		}
	}

	private void UpdateAllMineLiveData()
	{
		for (int i = 1; i <= 5; i++)
		{
			this.UpdateMineDataByID(i);
		}
	}

	private void RefreshGuildBattleInfoPanel(int resourceID = 1)
	{
		this.currentResourceID = resourceID;
		GuildWarManager.Instance.SendGetMemberInGuildReq(resourceID);
	}

	private void UpdateGuildMemberInfo(Transform parent, MemberInGuildScene memberInfo)
	{
		if (parent != null)
		{
			Text component = parent.FindChild("RoleName").GetComponent<Text>();
			Text component2 = parent.FindChild("RoleLV").GetComponent<Text>();
			Image component3 = parent.FindChild("RoleIcon").FindChild("RoleIconImg").GetComponent<Image>();
			if (memberInfo == null)
			{
				if (component != null)
				{
					component.set_text("暂无");
				}
				if (component2 != null)
				{
					component2.set_text("0");
				}
				if (component3 != null)
				{
					component3.set_enabled(false);
				}
			}
			else
			{
				if (component != null)
				{
					component.set_text(memberInfo.memberInfo.name);
				}
				if (component2 != null)
				{
					component2.set_text("Lv" + memberInfo.memberInfo.lv);
				}
				if (component3 != null)
				{
					component3.set_enabled(true);
					ResourceManager.SetSprite(component3, UIUtils.GetRoleSmallIcon(memberInfo.memberInfo.career));
				}
			}
		}
	}

	private string GetMinePointImageName(GuildWarManager.MineState mineState, int resourceID = 1)
	{
		if (mineState != GuildWarManager.MineState.My)
		{
			if (mineState != GuildWarManager.MineState.Enemy)
			{
				if (resourceID != 3)
				{
					return "gg_ziyuandian";
				}
				return "zj_ziyuandian";
			}
			else
			{
				if (resourceID != 3)
				{
					return "df_ziyuandian";
				}
				return "zj_ziyuandian_1";
			}
		}
		else
		{
			if (resourceID != 3)
			{
				return "zf_ziyuandian";
			}
			return "zj_ziyuandian_2";
		}
	}

	private void SetMinePointText(Transform mineTextTrans, GuildWarManager.MineState mineState)
	{
		if (mineTextTrans == null)
		{
			return;
		}
		Gradient component = mineTextTrans.GetComponent<Gradient>();
		Outline component2 = mineTextTrans.GetComponent<Outline>();
		if (mineState == GuildWarManager.MineState.Enemy)
		{
			if (component != null)
			{
				component.topColor = new Color(1f, 0.8666667f, 0.847058833f);
				component.bottomColor = new Color(1f, 0.4f, 0.454901963f);
			}
			if (component2 != null)
			{
				component2.set_effectColor(new Color(0.6431373f, 0.141176477f, 0.211764708f));
			}
		}
		else if (mineState == GuildWarManager.MineState.My)
		{
			if (component != null)
			{
				component.topColor = new Color(0.921568632f, 1f, 0.858823538f);
				component.bottomColor = new Color(127f, 1f, 0.3647059f);
			}
			if (component2 != null)
			{
				component2.set_effectColor(new Color(0.227450982f, 0.5294118f, 0.109803922f));
			}
		}
		else
		{
			if (component != null)
			{
				component.topColor = new Color(1f, 0.9882353f, 0.8156863f);
				component.bottomColor = new Color(1f, 0.9607843f, 0.333333343f);
			}
			if (component2 != null)
			{
				component2.set_effectColor(new Color(0.623529434f, 0.380392164f, 0.145098045f));
			}
		}
	}

	private void OnGetMyGuildMembersInGuildWar()
	{
		this.UpdateMyGuildMemberList();
	}

	private void OnUpdateCurrentResourceInfo(GetMemberInGuildWarRes down)
	{
		this.currentResourceInfo = down;
		if (down == null)
		{
			return;
		}
		if (down.inResourceId == this.currentResourceID)
		{
			this.UpdateResourceBattleInfo(this.currentResourceID);
		}
	}
}
