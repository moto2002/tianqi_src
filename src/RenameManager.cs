using Package;
using System;
using XNetwork;

internal class RenameManager : BaseSubSystemManager
{
	private static RenameManager instance;

	public static RenameManager Instance
	{
		get
		{
			if (RenameManager.instance == null)
			{
				RenameManager.instance = new RenameManager();
			}
			return RenameManager.instance;
		}
	}

	private RenameManager()
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
		NetworkManager.AddListenEvent<RoleReNameRes>(new NetCallBackMethod<RoleReNameRes>(this.OnRoleReNameRes));
	}

	public void SendRoleReNameReq(string newName)
	{
		NetworkManager.Send(new RoleReNameReq
		{
			newName = newName
		}, ServerType.Data);
	}

	public void OnRoleReNameRes(short state, RoleReNameRes msg = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (msg == null)
		{
			return;
		}
		string newName = msg.newName;
		EntityWorld.Instance.EntSelf.Name = newName;
		HeadInfoManager.Instance.SetName(1, EntityWorld.Instance.EntSelf.ID, EntityWorld.Instance.EntSelf.Lv, newName);
		EventDispatcher.Broadcast(EventNames.OnGetRoleAttrChangedNty);
	}
}
