using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TeamJoinUI : UIBase
{
	private const int numPerPage = 10;

	private int currentPage = 1;

	private bool haseRequire;

	private ListPool btnListPool;

	private Text noTeamTipText;

	private ButtonCustom btnQuickEnter;

	private TeamBtnTypeItem lastTeamBtnTypeItem;

	private TeamBtnTypeItem currentBtnTypeItem;

	public int selectTeamTargetID;

	private uint quickBtnTimerID;

	private bool isAddQuickTimeEnd;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.SetMask(0.7f, true, true);
	}

	protected override void InitUI()
	{
		base.InitUI();
		base.FindTransform("BtnRefresh").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRefresh);
		base.FindTransform("BtnCreate").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCreateBtn);
		this.btnQuickEnter = base.FindTransform("BtnQuick").GetComponent<ButtonCustom>();
		this.btnQuickEnter.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnQuick);
		base.FindTransform("BtnQuick").FindChild("Text").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(50735, false));
		base.FindTransform("BtnCreate").FindChild("Text").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(50726, false));
		base.FindTransform("BtnRefresh").FindChild("Text").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(505183, false));
		this.noTeamTipText = base.FindTransform("NoTeamTipText").GetComponent<Text>();
		this.btnListPool = base.FindTransform("TeamFirstTypeTab").GetComponent<ListPool>();
		base.FindTransform("ListViewTeamInfo").GetComponent<ScrollRectCustom>().onDrag = delegate(PointerEventData data)
		{
			float verticalNormalizedPosition = base.FindTransform("ListViewTeamInfo").GetComponent<ScrollRectCustom>().verticalNormalizedPosition;
			if (verticalNormalizedPosition <= 0f && !this.haseRequire)
			{
				if (TeamBasicManager.Instance.CanNotRequire)
				{
					return;
				}
				this.haseRequire = true;
				this.currentPage++;
				if (this.currentBtnTypeItem != null)
				{
					this.OnSendQueryTeamInfo(this.currentBtnTypeItem.DungeonType, this.currentBtnTypeItem.DungeonParams);
				}
			}
		};
		this.btnListPool.Clear();
		this.noTeamTipText.set_text(GameDataUtils.GetChineseContent(516128, false));
		if (this.noTeamTipText != null && this.noTeamTipText.get_gameObject().get_activeSelf())
		{
			this.noTeamTipText.get_gameObject().SetActive(false);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateQueryTeamBaseInfoList, new Callback(this.RefreshUI));
		EventDispatcher.AddListener(EventNames.CreateTeamSuccess, new Callback(this.OnCreateTeamSuccess));
		EventDispatcher.AddListener<TeamSecondBtnTypeItem>(EventNames.OnClickTeamTargetSecondBtn, new Callback<TeamSecondBtnTypeItem>(this.OnClickTeamTargetSecondBtn));
		EventDispatcher.AddListener(EventNames.OnQuickEnterTeamRes, new Callback(this.OnQuickEnterTeamRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateQueryTeamBaseInfoList, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener(EventNames.CreateTeamSuccess, new Callback(this.OnCreateTeamSuccess));
		EventDispatcher.RemoveListener<TeamSecondBtnTypeItem>(EventNames.OnClickTeamTargetSecondBtn, new Callback<TeamSecondBtnTypeItem>(this.OnClickTeamTargetSecondBtn));
		EventDispatcher.RemoveListener(EventNames.OnQuickEnterTeamRes, new Callback(this.OnQuickEnterTeamRes));
	}

	protected override void OnEnable()
	{
		this.SetQuickBtnState(true);
		this.currentPage = 1;
		this.RefreshLeftBtnList();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		TeamBasicManager.Instance.QueryTeamList = null;
		this.btnListPool.Clear();
		TimerHeap.DelTimer(this.quickBtnTimerID);
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void RefreshLeftBtnList()
	{
		this.btnListPool.Clear();
		List<DuiWuMuBiao> teamTargetCfgList = TeamBasicManager.Instance.GetTeamTargetFirstTypeCfgList();
		if (teamTargetCfgList != null && teamTargetCfgList.get_Count() > 0)
		{
			this.btnListPool.Create(teamTargetCfgList.get_Count(), delegate(int index)
			{
				if (index < teamTargetCfgList.get_Count() && index < this.btnListPool.Items.get_Count())
				{
					TeamBtnTypeItem component = this.btnListPool.Items.get_Item(index).get_transform().GetComponent<TeamBtnTypeItem>();
					if (component != null)
					{
						component.UpdateUI(teamTargetCfgList.get_Item(index).Id);
						component.TeamFirstBtn.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickTabBtn);
						bool flag = component.SetSelectTeamTargetID(this.selectTeamTargetID);
						if (flag)
						{
							this.OnClickTabBtn(component.TeamFirstBtn.get_gameObject());
						}
					}
				}
			});
		}
	}

	private void RefreshUI()
	{
		if (TeamBasicManager.Instance.QueryTeamList != null)
		{
			this.currentPage = TeamBasicManager.Instance.QueryTeamList.get_Count() / 10;
			this.RefreshTeamInfoList(TeamBasicManager.Instance.QueryTeamList);
		}
	}

	private void RefreshTeamInfoList(List<TeamBaseInfo> teamList)
	{
		Transform transform = base.FindTransform("ListViewTeamInfo").FindChild("Contair");
		int i = 0;
		if (teamList != null && teamList.get_Count() > 0)
		{
			if (this.noTeamTipText != null && this.noTeamTipText.get_gameObject().get_activeSelf())
			{
				this.noTeamTipText.get_gameObject().SetActive(false);
			}
			while (i < teamList.get_Count())
			{
				if (transform.get_childCount() > i)
				{
					GameObject gameObject = transform.GetChild(i).get_gameObject();
					if (gameObject != null && gameObject.GetComponent<TeamInfoItem>() != null)
					{
						gameObject.SetActive(true);
						gameObject.GetComponent<TeamInfoItem>().RefreshUI(teamList.get_Item(i));
					}
				}
				else
				{
					GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("TeamInfoItem");
					instantiate2Prefab.set_name("teamItem_" + i);
					TeamInfoItem component = instantiate2Prefab.GetComponent<TeamInfoItem>();
					instantiate2Prefab.get_transform().SetParent(transform);
					instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
					component.RefreshUI(teamList.get_Item(i));
				}
				i++;
			}
		}
		else if (this.noTeamTipText != null && !this.noTeamTipText.get_gameObject().get_activeSelf())
		{
			this.noTeamTipText.get_gameObject().SetActive(true);
		}
		for (int j = i; j < transform.get_childCount(); j++)
		{
			GameObject gameObject2 = transform.GetChild(j).get_gameObject();
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
		this.haseRequire = false;
	}

	private void OnClickTeamTargetSecondBtn(TeamSecondBtnTypeItem teamSecondItem)
	{
		if (teamSecondItem != null)
		{
			this.currentPage = 1;
			if (TeamBasicManager.Instance.QueryTeamList != null)
			{
				TeamBasicManager.Instance.QueryTeamList.Clear();
			}
			this.OnSendQueryTeamInfo(teamSecondItem.DungeonType, teamSecondItem.DungeonParams);
		}
	}

	private void SetQuickBtnState(bool isEnable = true)
	{
		this.btnQuickEnter.set_enabled(isEnable);
		ImageColorMgr.SetImageColor(this.btnQuickEnter.get_transform().GetComponent<Image>(), !isEnable);
	}

	private void OnClickTabBtn(GameObject go)
	{
		TeamBtnTypeItem component = go.get_transform().get_parent().GetComponent<TeamBtnTypeItem>();
		if (component == null)
		{
			return;
		}
		if (this.lastTeamBtnTypeItem != null)
		{
			this.lastTeamBtnTypeItem.Selected = false;
		}
		component.Selected = true;
		component.IsShowSecond = !component.IsShowSecond;
		this.currentBtnTypeItem = component;
		this.lastTeamBtnTypeItem = component;
		this.currentPage = 1;
		if (TeamBasicManager.Instance.QueryTeamList != null)
		{
			TeamBasicManager.Instance.QueryTeamList.Clear();
		}
		this.OnSendQueryTeamInfo(this.currentBtnTypeItem.DungeonType, this.currentBtnTypeItem.DungeonParams);
	}

	private void OnClickRefresh(GameObject go)
	{
		if (TeamBasicManager.Instance.QueryTeamList != null)
		{
			TeamBasicManager.Instance.QueryTeamList.Clear();
		}
		if (this.currentBtnTypeItem != null)
		{
			this.OnSendQueryTeamInfo(this.currentBtnTypeItem.DungeonType, this.currentBtnTypeItem.DungeonParams);
		}
	}

	private void OnClickBtnQuick(GameObject go)
	{
		if (this.currentBtnTypeItem != null)
		{
			TeamBasicManager.Instance.SendQuickEnterTeamReq((DungeonType.ENUM)this.currentBtnTypeItem.DungeonType, this.currentBtnTypeItem.DungeonParams);
			this.SetQuickBtnState(false);
			this.isAddQuickTimeEnd = false;
			this.quickBtnTimerID = TimerHeap.AddTimer(2000u, 0, delegate
			{
				this.SetQuickBtnState(true);
				TimerHeap.DelTimer(this.quickBtnTimerID);
				this.isAddQuickTimeEnd = true;
			});
		}
	}

	private void OnSendQueryTeamInfo(int type = 0, List<int> param = null)
	{
		this.currentPage = ((this.currentPage > 0) ? this.currentPage : 1);
		if (type <= 1)
		{
			type = 100;
		}
		TeamBasicManager.Instance.SendFindTeamInfoReq(this.currentPage, (DungeonType.ENUM)type, param);
	}

	private void OnClickCreateBtn(GameObject go)
	{
		if (TeamBasicManager.Instance.MyTeamData == null && this.currentBtnTypeItem != null)
		{
			int num = this.currentBtnTypeItem.DungeonType;
			if (num <= 1)
			{
				num = 100;
			}
			int systemID = 0;
			DuiWuMuBiao teamTargetCfg = TeamBasicManager.Instance.GetTeamTargetCfg((DungeonType.ENUM)num, this.currentBtnTypeItem.DungeonParams);
			if (teamTargetCfg != null)
			{
				systemID = teamTargetCfg.SystemId;
			}
			TeamBasicManager.Instance.OnMakeTeamByDungeonType((DungeonType.ENUM)num, this.currentBtnTypeItem.DungeonParams, systemID);
		}
	}

	private void OnCreateTeamSuccess()
	{
		this.Show(false);
	}

	private void OnQuickEnterTeamRes()
	{
		if (this.isAddQuickTimeEnd)
		{
			this.SetQuickBtnState(true);
		}
	}
}
