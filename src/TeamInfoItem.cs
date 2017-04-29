using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfoItem : BaseUIBehaviour
{
	private Text joinLvText;

	private ButtonCustom btnJoin;

	private List<Transform> memberTransformList;

	private TeamBaseInfo myTeamBaseInfo;

	private bool isInit;

	private bool isInvited;

	private float espacTime;

	private int second;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.joinLvText = base.FindTransform("JoinLvText").GetComponent<Text>();
		this.btnJoin = base.FindTransform("BtnJoin").GetComponent<ButtonCustom>();
		this.btnJoin.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickJoinBtn);
		this.memberTransformList = new List<Transform>();
		for (int i = 1; i < 4; i++)
		{
			Transform transform = base.FindTransform("member" + i);
			if (!(transform == null))
			{
				this.memberTransformList.Add(transform);
			}
		}
		this.isInit = true;
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int, int>(EventNames.UpdateJoinInTeamCoolDown, new Callback<int, int>(this.UpdateJoinInTeamCoolDown));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int, int>(EventNames.UpdateJoinInTeamCoolDown, new Callback<int, int>(this.UpdateJoinInTeamCoolDown));
	}

	public void RefreshUI(TeamBaseInfo teamBaseInfo)
	{
		if (!this.isInit)
		{
			this.InitUI();
		}
		this.myTeamBaseInfo = teamBaseInfo;
		if (teamBaseInfo != null)
		{
			this.joinLvText.set_text(string.Concat(new object[]
			{
				teamBaseInfo.minLv,
				"-",
				teamBaseInfo.maxLv,
				GameDataUtils.GetChineseContent(301042, false)
			}));
			int i;
			for (i = 0; i < teamBaseInfo.memberResume.get_Count(); i++)
			{
				this.memberTransformList.get_Item(i).FindChild("HaveMember").get_gameObject().SetActive(true);
				this.memberTransformList.get_Item(i).FindChild("NoMember").get_gameObject().SetActive(false);
				Image component = this.memberTransformList.get_Item(i).FindChild("HaveMember").FindChild("ImageIcon").GetComponent<Image>();
				ResourceManager.SetSprite(component, UIUtils.GetRoleSmallIcon((int)teamBaseInfo.memberResume.get_Item(i).career));
				component.SetNativeSize();
				Text component2 = this.memberTransformList.get_Item(i).FindChild("HaveMember").FindChild("MemberName").GetComponent<Text>();
				component2.set_text(teamBaseInfo.memberResume.get_Item(i).name);
				Text component3 = this.memberTransformList.get_Item(i).FindChild("HaveMember").FindChild("MemberFighting").GetComponent<Text>();
				component3.set_text(teamBaseInfo.memberResume.get_Item(i).fighting + string.Empty);
			}
			int num = i;
			while ((long)num < 3L)
			{
				this.memberTransformList.get_Item(num).FindChild("HaveMember").get_gameObject().SetActive(false);
				this.memberTransformList.get_Item(num).FindChild("NoMember").get_gameObject().SetActive(true);
				num++;
			}
			int num2 = TeamBasicManager.Instance.GetJoinTeamTime(this.myTeamBaseInfo.teamId);
			num2 /= 1000;
			if (num2 > 0)
			{
				this.second = num2;
				base.FindTransform("BtnQueryName").GetComponent<Text>().set_text(this.second.ToString());
				this.isInvited = true;
				this.OnUpdateItemButtonState(true);
			}
			else
			{
				this.isInvited = false;
				this.OnUpdateItemButtonState(false);
			}
		}
	}

	private void OnClickJoinBtn(GameObject go)
	{
		if (this.myTeamBaseInfo != null)
		{
			if ((long)this.myTeamBaseInfo.memberResume.get_Count() >= 3L)
			{
				string chineseContent = GameDataUtils.GetChineseContent(516102, false);
				UIManagerControl.Instance.ShowToastText(chineseContent);
				return;
			}
			TeamBasicManager.Instance.SendAppointJoinTeamInfoReq(this.myTeamBaseInfo.teamId);
		}
	}

	private void UpdateJoinInTeamCoolDown(int teamID, int CoolDown)
	{
		if (this.myTeamBaseInfo != null && this.myTeamBaseInfo.teamId == teamID && CoolDown > 0)
		{
			this.second = CoolDown;
			base.FindTransform("BtnQueryName").GetComponent<Text>().set_text(this.second.ToString());
			this.isInvited = true;
			this.OnUpdateItemButtonState(true);
		}
	}

	private void Update()
	{
		if (!this.isInvited)
		{
			return;
		}
		if (this.second > 0)
		{
			this.espacTime += Time.get_deltaTime();
			if (this.espacTime > 1f)
			{
				this.espacTime -= (float)((int)this.espacTime);
				this.second--;
				if (base.FindTransform("BtnQueryName") == null)
				{
					return;
				}
				base.FindTransform("BtnQueryName").GetComponent<Text>().set_text(this.second.ToString());
			}
		}
		else
		{
			this.StopTimer();
		}
	}

	private void OnUpdateItemButtonState(bool invited)
	{
		if (invited)
		{
			base.FindTransform("BtnJoin").GetComponent<ButtonCustom>().set_enabled(false);
			ImageColorMgr.SetImageColor(base.FindTransform("BtnJoin").GetComponent<Image>(), true);
		}
		else
		{
			base.FindTransform("BtnJoin").GetComponent<ButtonCustom>().set_enabled(true);
			ImageColorMgr.SetImageColor(base.FindTransform("BtnJoin").GetComponent<Image>(), false);
			base.FindTransform("BtnQueryName").GetComponent<Text>().set_text("申请加入");
		}
	}

	private void StopTimer()
	{
		this.isInvited = false;
		this.OnUpdateItemButtonState(false);
		TeamBasicManager.Instance.RemoveJoinTeamCDTime(this.myTeamBaseInfo.teamId);
	}
}
