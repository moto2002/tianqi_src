using Package;
using System;
using XNetwork;

public class GuildSkillManager : BaseSubSystemManager
{
	private GetGuildSkillRes skillData;

	protected static GuildSkillManager instance;

	public static GuildSkillManager Instance
	{
		get
		{
			if (GuildSkillManager.instance == null)
			{
				GuildSkillManager.instance = new GuildSkillManager();
			}
			return GuildSkillManager.instance;
		}
	}

	protected GuildSkillManager()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<GetGuildSkillRes>(new NetCallBackMethod<GetGuildSkillRes>(this.OnGetGuildSkillRes));
		NetworkManager.AddListenEvent<UpGuildSkillRes>(new NetCallBackMethod<UpGuildSkillRes>(this.OnUpGuildSkillRes));
	}

	public override void Release()
	{
	}

	public void SendGetGuildSkillReq()
	{
		NetworkManager.Send(new GetGuildSkillReq(), ServerType.Data);
	}

	public void SendUpGuildSkillReq(int skillId)
	{
		NetworkManager.Send(new UpGuildSkillReq
		{
			skillId = skillId
		}, ServerType.Data);
	}

	public void OnGetGuildSkillRes(short state, GetGuildSkillRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.skillData = down;
			if (GuildSkillUI.Instance != null && GuildSkillUI.Instance.get_gameObject().get_activeSelf())
			{
				GuildSkillUI.Instance.RefreshUI();
			}
		}
	}

	public void OnUpGuildSkillRes(short state, UpGuildSkillRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (this.skillData != null && this.skillData.skillInfo != null)
			{
				for (int i = 0; i < this.skillData.skillInfo.get_Count(); i++)
				{
					if (this.skillData.skillInfo.get_Item(i).skillId == down.skillId)
					{
						this.skillData.skillInfo.get_Item(i).skillLv = down.skillLv;
					}
				}
			}
			if (GuildSkillUI.Instance != null && GuildSkillUI.Instance.get_gameObject().get_activeSelf())
			{
				GuildSkillUI.Instance.RefreshUIById(down.skillId, down.skillLv);
			}
		}
	}

	public GetGuildSkillRes GetSkillData()
	{
		return this.skillData;
	}
}
