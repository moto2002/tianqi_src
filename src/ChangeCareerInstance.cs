using GameData;
using Package;
using System;
using System.Collections.Generic;

public class ChangeCareerInstance : BattleInstanceParent<object>
{
	public static readonly int ChangeCareerInstanceDataID = 110001;

	private static ChangeCareerInstance instance;

	public static ChangeCareerInstance Instance
	{
		get
		{
			if (ChangeCareerInstance.instance == null)
			{
				ChangeCareerInstance.instance = new ChangeCareerInstance();
			}
			return ChangeCareerInstance.instance;
		}
	}

	public override int InstanceDataID
	{
		get
		{
			return ChangeCareerInstance.ChangeCareerInstanceDataID;
		}
		set
		{
		}
	}

	protected ChangeCareerInstance()
	{
		base.Type = InstanceType.ChangeCareer;
	}

	public override void SendClientLoadDoneReq(int sceneID)
	{
		InstanceManager.SendClientLoadDoneResp(0, new BattleLoadInfo
		{
			unloadRoleCount = 0,
			loadTimeout = 0
		});
	}

	public override void LoadSceneStart(int lastSceneID, int nextSceneID)
	{
		LocalBattleHandler.Instance.SetData(new List<int>());
		LocalInstanceHandler.Instance.SetData(base.InstanceData, 0);
	}

	public override void PlayOpeningCG(int timeline, Action onPlayCGEnd)
	{
		if (onPlayCGEnd != null)
		{
			onPlayCGEnd.Invoke();
		}
	}

	public override void PlayEndingCG(int timeline, Action onPlayCGEnd)
	{
		if (onPlayCGEnd != null)
		{
			onPlayCGEnd.Invoke();
		}
	}

	public override void ShowWinPose()
	{
		UIManagerControl.Instance.HideUI("BattleUI");
		EventDispatcher.Broadcast("GuideManager.InstanceWin");
	}

	public override void SetTime()
	{
		base.SetTime();
		BattleTimeManager.Instance.ClientSetBattleTime(base.InstanceData.time);
	}

	public override void SetCommonLogic()
	{
		LocalInstanceHandler.Instance.Start();
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		InstanceManager.StopAllClientAI(true);
		SceneLoadedUIManager.Instance.IsFromBattleClick = true;
		SceneLoadedUISetting.CurrentType = "SHOW_CHANGE_CAREER_UI";
		UIManagerControl.Instance.HideUI("BattleUI");
		this.ShowMessageChangeCareerDialog();
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
		InstanceManager.StopAllClientAI(true);
		SceneLoadedUIManager.Instance.IsFromBattleClick = true;
		SceneLoadedUISetting.CurrentType = "SHOW_CHANGE_CAREER_UI";
		UIManagerControl.Instance.HideUI("BattleUI");
		InstanceManager.SimulateExitField();
		ChangeCareerManager.Instance.SendOutChallengeNty();
	}

	public override void ShowBattleUI()
	{
		if (base.InstanceResult != null)
		{
			return;
		}
		BattleUI battleUI = LinkNavigationManager.OpenBattleUI();
		battleUI.BtnQuitAction = delegate
		{
			UIManagerControl.Instance.OpenUI("GlobalBattleDialogUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			GlobalBattleDialogUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetNoticeText(100), GameDataUtils.GetChineseContent(511999, false), delegate
			{
				LinkNavigationManager.OpenBattleUI();
			}, delegate
			{
				ChangeCareerInstanceManager.Instance.IsQuit = true;
				LocalInstanceHandler.Instance.Finish(false);
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_yellow_1", null, true);
		};
		battleUI.ResetAllInstancePart();
		battleUI.ShowBattleTimeUI(true);
		battleUI.IsPauseCheck = false;
		battleUI.IsInAuto = (base.InstanceData.autoFight == 0);
	}

	private void ShowMessageChangeCareerDialog()
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(ChangeCareerInstanceManager.Instance.dst_profession);
		if (zhuanZhiJiChuPeiZhi == null)
		{
			return;
		}
		if (ChangeCareerManager.Instance.IsCareerChanged(zhuanZhiJiChuPeiZhi.job))
		{
			return;
		}
		DialogBoxUIViewModel.Instance.ShowAsOKCancel("转职确认", string.Format("是否转职为{0}", GameDataUtils.GetChineseContent(zhuanZhiJiChuPeiZhi.jobName, false)), delegate
		{
			ChangeCareerInstanceManager.Instance.IsWinWithChange = false;
			this.ExitInstance();
		}, delegate
		{
			ChangeCareerInstanceManager.Instance.IsWinWithChange = true;
			this.ExitInstance();
		}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", UINodesManager.T3RootOfSpecial, true, true);
		DialogBoxUIView.Instance.isClick = false;
	}

	private void ExitInstance()
	{
		InstanceManager.SimulateExitField();
		ChangeCareerManager.Instance.SendOutChallengeNty();
	}
}
