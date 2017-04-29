using Package;
using System;
using XNetwork;

public class ExperienceInstanceManager
{
	private static ExperienceInstanceManager instance;

	public static ExperienceInstanceManager Instance
	{
		get
		{
			if (ExperienceInstanceManager.instance == null)
			{
				ExperienceInstanceManager.instance = new ExperienceInstanceManager();
			}
			return ExperienceInstanceManager.instance;
		}
	}

	protected ExperienceInstanceManager()
	{
		this.AddInstanceListeners();
	}

	public void AddInstanceListeners()
	{
		EventDispatcher.AddListener<bool>(LocalInstanceEvent.LocalInstanceFinish, new Callback<bool>(this.ExperienceInstanceFinish));
		NetworkManager.AddListenEvent<FirstBattleEndRes>(new NetCallBackMethod<FirstBattleEndRes>(this.OnExperienceBattleFinishRes));
	}

	public void RemoveInstanceListeners()
	{
		EventDispatcher.RemoveListener<bool>(LocalInstanceEvent.LocalInstanceFinish, new Callback<bool>(this.ExperienceInstanceFinish));
		NetworkManager.RemoveListenEvent<FirstBattleEndRes>(new NetCallBackMethod<FirstBattleEndRes>(this.OnExperienceBattleFinishRes));
	}

	public void SendExperienceBattleFinish()
	{
		NetworkManager.Send(new FirstBattleEndReq(), ServerType.Data);
	}

	protected void OnExperienceBattleFinishRes(short state, FirstBattleEndRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
	}

	public void EnterExperienceInstance()
	{
		InstanceManager.ChangeInstanceManager(ExperienceInstance.Instance, false);
		EventDispatcher.Broadcast(ExperienceInstanceManagerEvent.ExperienceInstanceEnd);
	}

	private void ExperienceInstanceFinish(bool isWin)
	{
		if (InstanceManager.CurrentInstanceType != ExperienceInstance.Instance.Type)
		{
			return;
		}
		InstanceManager.InstanceWin();
	}
}
