using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamStartFightReplyUI : UIBase
{
	private Action leftBtnAction;

	private Action rightBtnAction;

	private Action closeBtnAction;

	private ButtonCustom leftBtn;

	private ButtonCustom rightBtn;

	private Text headTitleText;

	private Text coolDownText;

	private Text tipContentText;

	private Text leftBtnText;

	private Text rightBtnText;

	private List<Transform> teamMemberTransList;

	public string HeadTitleText
	{
		get
		{
			return this.headTitleText.get_text();
		}
		set
		{
			this.headTitleText.set_text(value);
		}
	}

	public string CoolDownText
	{
		get
		{
			return this.coolDownText.get_text();
		}
		set
		{
			this.coolDownText.set_text(value);
		}
	}

	public string TipContentText
	{
		get
		{
			return this.tipContentText.get_text();
		}
		set
		{
			this.tipContentText.set_text(value);
		}
	}

	public string LeftBtnContent
	{
		get
		{
			return this.leftBtnText.get_text();
		}
		set
		{
			this.leftBtnText.set_text(value);
		}
	}

	public string RightBtnContent
	{
		get
		{
			return this.rightBtnText.get_text();
		}
		set
		{
			this.rightBtnText.set_text(value);
		}
	}

	public bool LeftBtnVisibility
	{
		get
		{
			return this.leftBtn.get_gameObject().get_activeSelf();
		}
		set
		{
			if (this.leftBtn.get_gameObject().get_activeSelf() == value)
			{
				return;
			}
			this.leftBtn.get_gameObject().SetActive(value);
		}
	}

	public bool RightBtnVisibility
	{
		get
		{
			return this.rightBtn.get_gameObject().get_activeSelf();
		}
		set
		{
			if (this.rightBtn.get_gameObject().get_activeSelf() == value)
			{
				return;
			}
			this.rightBtn.get_gameObject().SetActive(value);
		}
	}

	public bool LeftBtnCanClick
	{
		set
		{
			this.leftBtn.set_enabled(value);
			Image component = this.leftBtn.get_transform().FindChild("BtnLeftBg").GetComponent<Image>();
			string spriteName = (!value) ? "button_gray_1" : "button_yellow_1";
			ResourceManager.SetIconSprite(component, spriteName);
		}
	}

	public bool RightBtnCanClick
	{
		set
		{
			this.rightBtn.set_enabled(value);
			Image component = this.rightBtn.get_transform().FindChild("BtnRightBg").GetComponent<Image>();
			string spriteName = (!value) ? "button_gray_1" : "button_yellow_1";
			ResourceManager.SetIconSprite(component, spriteName);
		}
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.alpha = 0.7f;
		this.isClick = false;
		this.isMask = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.coolDownText = base.FindTransform("CoolDown").GetComponent<Text>();
		this.tipContentText = base.FindTransform("TipContent").GetComponent<Text>();
		this.leftBtnText = base.FindTransform("BtnLeftText").GetComponent<Text>();
		this.rightBtnText = base.FindTransform("BtnRightText").GetComponent<Text>();
		this.headTitleText = base.FindTransform("Header").GetComponent<Text>();
		this.leftBtn = base.FindTransform("BtnLeft").GetComponent<ButtonCustom>();
		this.leftBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickLeftBtn);
		this.rightBtn = base.FindTransform("BtnRight").GetComponent<ButtonCustom>();
		this.rightBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRightBtn);
		this.teamMemberTransList = new List<Transform>();
		int num = 1;
		while ((long)num <= 3L)
		{
			Transform transform = base.FindTransform("TeamStarFightReplyItem" + num);
			if (transform != null)
			{
				this.teamMemberTransList.Add(transform);
			}
			num++;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.LeftBtnCanClick = true;
		this.RightBtnCanClick = true;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateMemberStartFightState, new Callback(this.UpdateMemberStartFightState));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateMemberStartFightState, new Callback(this.UpdateMemberStartFightState));
	}

	protected override void ReleaseSelf(bool destroy)
	{
		base.ReleaseSelf(destroy);
	}

	private void OnClickLeftBtn(GameObject go)
	{
		if (this.leftBtnAction != null)
		{
			this.leftBtnAction.Invoke();
		}
	}

	private void OnClickRightBtn(GameObject go)
	{
		if (this.rightBtnAction != null)
		{
			this.rightBtnAction.Invoke();
		}
	}

	protected override void OnClickCloseBtn(GameObject go)
	{
		if (this.closeBtnAction != null)
		{
			this.closeBtnAction.Invoke();
		}
		base.OnClickCloseBtn(go);
	}

	public void ShowAsOkAndCancel(string title, string content = "", Action leftBtnAction = null, Action rightBtnAction = null, Action closeBtnAction = null, string leftBtnContent = "取 消", string rightBtnContent = "确 定")
	{
		this.LeftBtnVisibility = true;
		this.RightBtnVisibility = true;
		this.LeftBtnContent = leftBtnContent;
		this.RightBtnContent = rightBtnContent;
		this.HeadTitleText = title;
		this.TipContentText = content;
		this.leftBtnAction = leftBtnAction;
		this.rightBtnAction = rightBtnAction;
		this.closeBtnAction = closeBtnAction;
		this.ShowRoleMemberListState();
	}

	public void ShowAsOk(string title, string content = "", Action rightBtnAction = null, string rightBtnContent = "确 定", Action closeBtnAction = null)
	{
		this.LeftBtnVisibility = false;
		this.RightBtnVisibility = true;
		this.RightBtnContent = rightBtnContent;
		this.HeadTitleText = title;
		this.TipContentText = content;
		this.leftBtnAction = null;
		this.rightBtnAction = rightBtnAction;
		this.closeBtnAction = closeBtnAction;
		this.ShowRoleMemberListState();
	}

	public void ShowRoleMemberListState()
	{
		List<TeamMemberStartFightReplyState> teamMemberStartFightReplyStateList = TeamBasicManager.Instance.teamMemberStartFightReplyStateList;
		int i = 0;
		if (TeamBasicManager.Instance.MyTeamData != null && TeamBasicManager.Instance.MyTeamData.TeamRoleList != null)
		{
			while (i < TeamBasicManager.Instance.MyTeamData.TeamRoleList.get_Count())
			{
				MemberResume member = TeamBasicManager.Instance.MyTeamData.TeamRoleList.get_Item(i);
				bool flag = member.roleId == TeamBasicManager.Instance.MyTeamData.LeaderID;
				bool isAgree = false;
				if (teamMemberStartFightReplyStateList != null)
				{
					int num = teamMemberStartFightReplyStateList.FindIndex((TeamMemberStartFightReplyState a) => a.RoleID == member.roleId);
					if (num >= 0)
					{
						isAgree = true;
					}
				}
				if (flag)
				{
					isAgree = true;
				}
				this.SetMemebrItem(i, (int)member.career, member.name, member.level, isAgree, flag);
				i++;
			}
		}
		int num2 = i;
		while ((long)num2 < 3L)
		{
			if (num2 >= this.teamMemberTransList.get_Count())
			{
				return;
			}
			this.teamMemberTransList.get_Item(num2).get_gameObject().SetActive(false);
			num2++;
		}
	}

	private void SetMemebrItem(int index, int carrer, string roleName, int roleLv, bool isAgree, bool isCaptain)
	{
		if (index >= this.teamMemberTransList.get_Count())
		{
			return;
		}
		Transform transform = this.teamMemberTransList.get_Item(index);
		transform.get_gameObject().SetActive(true);
		Image component = transform.FindChild("ImageIcon").GetComponent<Image>();
		if (component != null)
		{
			ResourceManager.SetSprite(component, UIUtils.GetRoleSmallIcon(carrer));
		}
		transform.FindChild("RoleName").GetComponent<Text>().set_text(roleName);
		transform.FindChild("RoleLv").GetComponent<Text>().set_text(roleLv + "级");
		transform.FindChild("PreparedImg").get_gameObject().SetActive(isAgree);
		transform.FindChild("NotPreparedImg").get_gameObject().SetActive(!isAgree);
		transform.FindChild("ImageLeaderIcon").get_gameObject().SetActive(isCaptain);
	}

	private void UpdateMemberStartFightState()
	{
		this.ShowRoleMemberListState();
	}
}
