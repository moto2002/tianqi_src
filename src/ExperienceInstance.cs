using Package;
using System;
using System.Collections.Generic;

public class ExperienceInstance : BattleInstanceParent<object>
{
	protected static readonly int ExperienceInstanceDataID = 10000;

	private static ExperienceInstance instance;

	public static ExperienceInstance Instance
	{
		get
		{
			if (ExperienceInstance.instance == null)
			{
				ExperienceInstance.instance = new ExperienceInstance();
			}
			return ExperienceInstance.instance;
		}
	}

	public override int InstanceDataID
	{
		get
		{
			return ExperienceInstance.ExperienceInstanceDataID;
		}
		set
		{
		}
	}

	protected ExperienceInstance()
	{
		base.Type = InstanceType.Experience;
	}

	public override void LoadSceneStart(int lastSceneID, int nextSceneID)
	{
		LocalBattleHandler.Instance.SetData(new List<int>());
		LocalInstanceHandler.Instance.SetData(base.InstanceData, 0);
	}

	public override void SendClientLoadDoneReq(int sceneID)
	{
		InstanceManager.SendClientLoadDoneResp(0, new BattleLoadInfo
		{
			unloadRoleCount = 0,
			loadTimeout = 0
		});
	}

	public override void PlayOpeningCG(int timeline, Action onPlayCGEnd)
	{
		if (InstanceManager.IsCurrentInstanceWithTask)
		{
			TimelineGlobal.Init(timeline, 0, onPlayCGEnd);
		}
		else if (onPlayCGEnd != null)
		{
			onPlayCGEnd.Invoke();
		}
	}

	public override void PlayEndingCG(int timeline, Action onPlayCGEnd)
	{
		if (InstanceManager.IsCurrentInstanceWithTask)
		{
			TimelineGlobal.Init(timeline, 1, onPlayCGEnd);
		}
		else
		{
			FXManager.Instance.DeleteFX(DungeonManager.Instance.TaskEffectFxID);
			DungeonManager.Instance.TaskEffectInstanceID = 0;
			if (onPlayCGEnd != null)
			{
				onPlayCGEnd.Invoke();
			}
		}
	}

	public override void SetCommonLogic()
	{
		LocalInstanceHandler.Instance.Start();
	}

	public override void ShowWinUI()
	{
		base.ShowWinUI();
		InstanceManager.SimulateExitField();
		EventDispatcher.Broadcast(ExperienceInstanceManagerEvent.ExperienceInstanceEnd);
	}

	public override void ShowLoseUI()
	{
		base.ShowLoseUI();
	}
}
