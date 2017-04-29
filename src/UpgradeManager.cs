using Package;
using System;
using UnityEngine;
using XNetwork;

public class UpgradeManager : BaseSubSystemManager
{
	public class MEventNames
	{
		public const string RoleSelfLevelUp = "UpgradeManager.RoleSelfLevelUp";
	}

	private static UpgradeManager instance;

	public static UpgradeManager Instance
	{
		get
		{
			if (UpgradeManager.instance == null)
			{
				UpgradeManager.instance = new UpgradeManager();
			}
			return UpgradeManager.instance;
		}
	}

	private UpgradeManager()
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
		NetworkManager.AddListenEvent<RoleAfterLevelUpNty>(new NetCallBackMethod<RoleAfterLevelUpNty>(this.OnGetRoleAfterLevelUpNty));
		NetworkManager.AddListenEvent<RoleLevelUpBcast>(new NetCallBackMethod<RoleLevelUpBcast>(this.OnGetRoleLevelUpBcast));
	}

	private void OnGetRoleLevelUpBcast(short state, RoleLevelUpBcast down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			EntityParent entityByID = EntityWorld.Instance.GetEntityByID(down.roleId);
			if (entityByID != null && entityByID.IsEntityCityPlayerType)
			{
				entityByID.Lv = down.newLv;
			}
		}
	}

	private void OnGetRoleAfterLevelUpNty(short state, RoleAfterLevelUpNty down = null)
	{
		if (EntityWorld.Instance != null && EntityWorld.Instance.EntSelf != null)
		{
			EntityWorld.Instance.EntSelf.Lv = down.newLv;
		}
		Debug.Log("主角升级:<color=#ffffff>" + down.newLv + "</color>");
		EventDispatcher.Broadcast("UpgradeManager.RoleSelfLevelUp");
		EventDispatcher.Broadcast<int>("GuideManager.LevelUp", down.newLv);
		SDKManager.Instance.SubmitExtendData(null, SDKManager.SubmitTypeLevelup, SDKManager.GetSubmitData());
	}

	private void DoGetRoleAfterLevelUpNty(RoleAfterLevelUpNty down)
	{
	}
}
