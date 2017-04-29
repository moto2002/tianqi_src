using GameData;
using Package;
using System;
using XEngineActor;

public class CheckPlayerInfoManager : BaseSubSystemManager
{
	private EntityParent selectEntityParent;

	private bool isAfterTeamCreateTeam;

	public static CheckPlayerInfoManager instance;

	public EntityParent SelectEntityParent
	{
		get
		{
			return this.selectEntityParent;
		}
		set
		{
			this.selectEntityParent = value;
		}
	}

	public bool IsAfterTeamCreateTeam
	{
		get
		{
			return this.isAfterTeamCreateTeam;
		}
		set
		{
			this.isAfterTeamCreateTeam = value;
		}
	}

	public static CheckPlayerInfoManager Instance
	{
		get
		{
			if (CheckPlayerInfoManager.instance == null)
			{
				CheckPlayerInfoManager.instance = new CheckPlayerInfoManager();
			}
			return CheckPlayerInfoManager.instance;
		}
	}

	private CheckPlayerInfoManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener<Actor>(CameraEvent.SelectModel, new Callback<Actor>(this.onSelectModelCallback));
	}

	protected void onSelectModelCallback(Actor actor)
	{
		if (actor is ActorCityPlayer)
		{
			this.SelectEntityParent = (actor as ActorParent).GetEntity();
			if (this.SelectEntityParent.ID != EntityWorld.Instance.EntSelf.ID)
			{
				LinkNavigationManager.OpenCheckPlayerInfoUI();
			}
		}
		else if (actor is ActorNPC)
		{
			(actor as ActorNPC).OnSeleted();
		}
	}

	protected void OnSeleteNPC(int npcId)
	{
		MainTaskManager.Instance.OpenTalkUI(DataReader<NPC>.Get(npcId).word, true, null, npcId);
	}

	public void invitePlayer(bool isHaveTeam, bool isLeader, bool isTeamFull, int teamMinLevel, int teamMaxLevel, Action action)
	{
		if (!isHaveTeam)
		{
			TeamBasicManager.Instance.SendOrganizeTeamReq(DungeonType.ENUM.Team, null, 0);
		}
		else if (isLeader)
		{
			this.currentPlayerInviteLogic(isTeamFull, teamMinLevel, teamMaxLevel, action);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516124, false));
		}
	}

	private void currentPlayerInviteLogic(bool isTeamFull, int teamMinLevel, int teamMaxLevel, Action action)
	{
		if (isTeamFull)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516102, false));
		}
		else if (CheckPlayerInfoManager.Instance.SelectEntityParent.Lv < teamMinLevel || CheckPlayerInfoManager.Instance.SelectEntityParent.Lv > teamMaxLevel)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(516114, false));
		}
		else
		{
			this.sendInvitePlayerLogic(action);
		}
	}

	private void sendInvitePlayerLogic(Action action)
	{
		if (CheckPlayerInfoManager.Instance.SelectEntityParent != null)
		{
			if (action != null)
			{
				action.Invoke();
			}
			TeamBasicManager.Instance.SendInvitePartnerReq(CheckPlayerInfoManager.Instance.SelectEntityParent.ID);
		}
	}
}
