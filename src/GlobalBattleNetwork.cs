using Package;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XNetwork;

public class GlobalBattleNetwork
{
	public enum CowardCheckType
	{
		Time,
		Count
	}

	private static GlobalBattleNetwork instance;

	protected int serverVitalSendCount;

	protected int serverVitalReceiveCount;

	protected bool isServerEnableWithCowardCheck = true;

	protected GlobalBattleNetwork.CowardCheckType curCowardCheckType = GlobalBattleNetwork.CowardCheckType.Count;

	protected bool isCowardTimeChecking;

	protected uint cowardTimeCheckTimer;

	protected uint cowardTimeCheckDelay = 1000u;

	protected int cowardCountCheckDefinite = 300;

	public static GlobalBattleNetwork Instance
	{
		get
		{
			if (GlobalBattleNetwork.instance == null)
			{
				GlobalBattleNetwork.instance = new GlobalBattleNetwork();
			}
			return GlobalBattleNetwork.instance;
		}
	}

	protected int ServerVitalSendCount
	{
		get
		{
			return this.serverVitalSendCount;
		}
		set
		{
			if (ClientGMManager.Instance.BattleLog)
			{
				Debug.LogError("ServerVitalSendCount: " + value);
			}
			this.serverVitalSendCount = value;
			GlobalBattleNetwork.CowardCheckType cowardCheckType = this.CurCowardCheckType;
			if (cowardCheckType != GlobalBattleNetwork.CowardCheckType.Time)
			{
				if (cowardCheckType == GlobalBattleNetwork.CowardCheckType.Count)
				{
					this.CowardCheckIsServerEnable(value - this.cowardCountCheckDefinite, this.ServerVitalReceiveCount, true);
				}
			}
			else if (!this.IsCowardTimeChecking)
			{
				this.IsCowardTimeChecking = true;
				this.cowardTimeCheckTimer = TimerHeap.AddTimer(this.cowardTimeCheckDelay, 0, delegate
				{
					this.CowardCheckIsServerEnable(value, this.ServerVitalReceiveCount, true);
				});
			}
		}
	}

	protected int ServerVitalReceiveCount
	{
		get
		{
			return this.serverVitalReceiveCount;
		}
		set
		{
			if (ClientGMManager.Instance.BattleLog)
			{
				Debug.LogError("ServerVitalReceiveCount: " + value);
			}
			this.serverVitalReceiveCount = ((value <= this.ServerVitalSendCount) ? value : this.ServerVitalSendCount);
			this.CowardCheckIsServerEnable(this.ServerVitalSendCount, value, false);
		}
	}

	public bool IsServerEnable
	{
		get
		{
			if (SystemConfig.IsDisableServerProBattle)
			{
				return true;
			}
			if (ClientGMManager.Instance.BattleSwitch00)
			{
				return this.IsServerEnableWithCowardCheck;
			}
			return this.IsServerEnableWithStrongCheck;
		}
	}

	public bool IsServerEnableWithStrongCheck
	{
		get
		{
			return this.ServerVitalSendCount <= this.ServerVitalReceiveCount;
		}
	}

	public bool IsServerEnableWithCowardCheck
	{
		get
		{
			return this.isServerEnableWithCowardCheck;
		}
		protected set
		{
			if (ClientGMManager.Instance.BattleLog)
			{
				Debug.LogError("IsServerEnableWithCowardCheck: " + value);
			}
			this.isServerEnableWithCowardCheck = value;
		}
	}

	public GlobalBattleNetwork.CowardCheckType CurCowardCheckType
	{
		get
		{
			return this.curCowardCheckType;
		}
		set
		{
			this.curCowardCheckType = value;
		}
	}

	protected bool IsCowardTimeChecking
	{
		get
		{
			return this.isCowardTimeChecking;
		}
		set
		{
			if (ClientGMManager.Instance.BattleLog)
			{
				Debug.LogError("IsCowardTimeChecking: " + value);
			}
			this.isCowardTimeChecking = value;
		}
	}

	protected GlobalBattleNetwork()
	{
	}

	public void Init()
	{
		NetworkManager.AddListenEvent<RoleMoveRes>(new NetCallBackMethod<RoleMoveRes>(this.OnRoleMoveRes));
		NetworkManager.AddListenEvent<RolePreciseMoveToRes>(new NetCallBackMethod<RolePreciseMoveToRes>(this.OnRolePreciseMoveToRes));
		NetworkManager.AddListenEvent<BattleNty>(new NetCallBackMethod<BattleNty>(this.OnBattleNty));
		NetworkManager.AddListenEvent<UseSkillRes>(new NetCallBackMethod<UseSkillRes>(this.OnUseSkillRes));
		NetworkManager.AddListenEvent<CancelUseSkillRes>(new NetCallBackMethod<CancelUseSkillRes>(this.OnCancelUseSkillRes));
		NetworkManager.AddListenEvent<AddEffectRes>(new NetCallBackMethod<AddEffectRes>(this.OnAddEffectRes));
		NetworkManager.AddListenEvent<UpdateEffectRes>(new NetCallBackMethod<UpdateEffectRes>(this.OnUpdateEffectRes));
		NetworkManager.AddListenEvent<AssaultRes>(new NetCallBackMethod<AssaultRes>(this.OnAssaultRes));
		NetworkManager.AddListenEvent<EndFitActionRes>(new NetCallBackMethod<EndFitActionRes>(this.OnEndFitActionRes));
		NetworkManager.AddListenEvent<ClientDrvBattleCalcActPointRes>(new NetCallBackMethod<ClientDrvBattleCalcActPointRes>(this.OnClientDrvBattleCalcActPointRes));
		NetworkManager.AddListenEvent<ClientDrvBattleEffectDmgReportRes>(new NetCallBackMethod<ClientDrvBattleEffectDmgReportRes>(this.OnClientDrvBattleEffectDmgReportRes));
		NetworkManager.AddListenEvent<ClientDrvBattleBuffDmgReportRes>(new NetCallBackMethod<ClientDrvBattleBuffDmgReportRes>(this.OnClientDrvBattleBuffDmgReportRes));
		NetworkManager.AddListenEvent<ClientDrvBattleVerifyInfoNty>(new NetCallBackMethod<ClientDrvBattleVerifyInfoNty>(this.OnClientDrvBattleVerifyInfoNty));
		NetworkManager.AddListenEvent<BattleReconnCacheEndNty>(new NetCallBackMethod<BattleReconnCacheEndNty>(this.OnBattleReconnectCacheEndNty));
	}

	public void Release()
	{
		NetworkManager.RemoveListenEvent<RoleMoveRes>(new NetCallBackMethod<RoleMoveRes>(this.OnRoleMoveRes));
		NetworkManager.RemoveListenEvent<RolePreciseMoveToRes>(new NetCallBackMethod<RolePreciseMoveToRes>(this.OnRolePreciseMoveToRes));
		NetworkManager.RemoveListenEvent<BattleNty>(new NetCallBackMethod<BattleNty>(this.OnBattleNty));
		NetworkManager.RemoveListenEvent<UseSkillRes>(new NetCallBackMethod<UseSkillRes>(this.OnUseSkillRes));
		NetworkManager.RemoveListenEvent<CancelUseSkillRes>(new NetCallBackMethod<CancelUseSkillRes>(this.OnCancelUseSkillRes));
		NetworkManager.RemoveListenEvent<AddEffectRes>(new NetCallBackMethod<AddEffectRes>(this.OnAddEffectRes));
		NetworkManager.RemoveListenEvent<UpdateEffectRes>(new NetCallBackMethod<UpdateEffectRes>(this.OnUpdateEffectRes));
		NetworkManager.RemoveListenEvent<AssaultRes>(new NetCallBackMethod<AssaultRes>(this.OnAssaultRes));
		NetworkManager.RemoveListenEvent<EndFitActionRes>(new NetCallBackMethod<EndFitActionRes>(this.OnEndFitActionRes));
		NetworkManager.RemoveListenEvent<ClientDrvBattleCalcActPointRes>(new NetCallBackMethod<ClientDrvBattleCalcActPointRes>(this.OnClientDrvBattleCalcActPointRes));
		NetworkManager.RemoveListenEvent<ClientDrvBattleEffectDmgReportRes>(new NetCallBackMethod<ClientDrvBattleEffectDmgReportRes>(this.OnClientDrvBattleEffectDmgReportRes));
		NetworkManager.RemoveListenEvent<ClientDrvBattleBuffDmgReportRes>(new NetCallBackMethod<ClientDrvBattleBuffDmgReportRes>(this.OnClientDrvBattleBuffDmgReportRes));
		NetworkManager.RemoveListenEvent<ClientDrvBattleVerifyInfoNty>(new NetCallBackMethod<ClientDrvBattleVerifyInfoNty>(this.OnClientDrvBattleVerifyInfoNty));
		NetworkManager.RemoveListenEvent<BattleReconnCacheEndNty>(new NetCallBackMethod<BattleReconnCacheEndNty>(this.OnBattleReconnectCacheEndNty));
		this.ResetData();
	}

	public void ResetData()
	{
		this.ServerVitalSendCount = 0;
		this.ServerVitalReceiveCount = 0;
		this.IsServerEnableWithCowardCheck = true;
		this.IsCowardTimeChecking = false;
		TimerHeap.DelTimer(this.cowardTimeCheckTimer);
	}

	public void SetData()
	{
		this.ResetData();
	}

	protected T ByteToObject<T>(byte[] buffer)
	{
		T result;
		using (MemoryStream memoryStream = new MemoryStream(buffer, 4, buffer.Length - 4))
		{
			try
			{
				result = Serializer.Deserialize<T>(memoryStream);
			}
			catch (Exception ex)
			{
				Debuger.Error(ex.get_Message(), new object[0]);
				throw;
			}
		}
		return result;
	}

	protected bool CheckIsSelf(long id)
	{
		return EntityWorld.Instance.EntSelf != null && id == EntityWorld.Instance.EntSelf.ID;
	}

	protected bool CheckIsClientDrive(long id)
	{
		return EntityWorld.Instance.GetEntityByID(id) != null && EntityWorld.Instance.GetEntityByID(id).IsClientDrive;
	}

	protected void OnRoleMoveRes(short state, RoleMoveRes down = null)
	{
		this.ServerVitalReceiveCount++;
	}

	protected void OnRolePreciseMoveToRes(short state, RolePreciseMoveToRes down = null)
	{
		this.ServerVitalReceiveCount++;
	}

	protected void OnBattleNty(short state, BattleNty down = null)
	{
		if (state != 0 || down == null)
		{
			Debuger.Error(string.Concat(new object[]
			{
				"OnBattleNty Error: ",
				state,
				" ",
				down.ToString()
			}), new object[0]);
			return;
		}
		for (int i = 0; i < down.actions.get_Count(); i++)
		{
			BattleAction battleAction = down.actions.get_Item(i);
			switch (battleAction.actType)
			{
			case 0:
			{
				Debuger.Info("UseSkill", new object[0]);
				BattleAction_UseSkill arg = this.ByteToObject<BattleAction_UseSkill>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_UseSkill, bool>(BattleActionEvent.UseSkill, arg, true);
				break;
			}
			case 1:
				Debuger.Info("CancelUseSkill", new object[0]);
				break;
			case 2:
			{
				Debuger.Info("AttrChanged", new object[0]);
				BattleAction_AttrChanged arg2 = this.ByteToObject<BattleAction_AttrChanged>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_AttrChanged, bool>(BattleActionEvent.AttrChanged, arg2, true);
				break;
			}
			case 3:
			{
				Debuger.Info("Bleed", new object[0]);
				BattleAction_Bleed arg3 = this.ByteToObject<BattleAction_Bleed>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_Bleed, bool>(BattleActionEvent.Bleed, arg3, true);
				break;
			}
			case 4:
			{
				Debuger.Info("Treat", new object[0]);
				BattleAction_Treat arg4 = this.ByteToObject<BattleAction_Treat>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_Treat, bool>(BattleActionEvent.Treat, arg4, true);
				break;
			}
			case 5:
			{
				Debuger.Info("UpdateEffect", new object[0]);
				BattleAction_UpdateEffect arg5 = this.ByteToObject<BattleAction_UpdateEffect>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_UpdateEffect, bool>(BattleActionEvent.UpdateEffect, arg5, true);
				break;
			}
			case 6:
			{
				Debuger.Info("RemoveEffect", new object[0]);
				BattleAction_RemoveEffect arg6 = this.ByteToObject<BattleAction_RemoveEffect>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_RemoveEffect, bool>(BattleActionEvent.RemoveEffect, arg6, true);
				break;
			}
			case 7:
			{
				Debuger.Info("Relive", new object[0]);
				BattleAction_Relive arg7 = this.ByteToObject<BattleAction_Relive>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_Relive, bool>(BattleActionEvent.Relive, arg7, true);
				break;
			}
			case 8:
			{
				Debuger.Info("PetEnterBattleField", new object[0]);
				BattleAction_PetEnterField arg8 = this.ByteToObject<BattleAction_PetEnterField>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_PetEnterField, bool>(BattleActionEvent.PetEnterBattleField, arg8, true);
				break;
			}
			case 9:
			{
				Debuger.Info("PetLeaveBattleField", new object[0]);
				BattleAction_PetLeaveField arg9 = this.ByteToObject<BattleAction_PetLeaveField>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_PetLeaveField, bool>(BattleActionEvent.PetLeaveBattleField, arg9, true);
				break;
			}
			case 10:
			{
				Debuger.Info("Fit", new object[0]);
				BattleAction_Fit arg10 = this.ByteToObject<BattleAction_Fit>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_Fit, bool>(BattleActionEvent.Fit, arg10, true);
				break;
			}
			case 11:
			{
				Debuger.Info("ExitFit", new object[0]);
				BattleAction_ExitFit arg11 = this.ByteToObject<BattleAction_ExitFit>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_ExitFit, bool>(BattleActionEvent.ExitFit, arg11, true);
				break;
			}
			case 12:
			{
				Debuger.Info("AddBuff", new object[0]);
				BattleAction_AddBuff arg12 = this.ByteToObject<BattleAction_AddBuff>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_AddBuff, bool>(BattleActionEvent.AddBuff, arg12, true);
				break;
			}
			case 13:
			{
				Debuger.Info("UpdateBuff", new object[0]);
				BattleAction_UpdateBuff arg13 = this.ByteToObject<BattleAction_UpdateBuff>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_UpdateBuff, bool>(BattleActionEvent.UpdateBuff, arg13, true);
				break;
			}
			case 14:
			{
				Debuger.Info("RemoveBuff", new object[0]);
				BattleAction_RemoveBuff arg14 = this.ByteToObject<BattleAction_RemoveBuff>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_RemoveBuff, bool>(BattleActionEvent.RemoveBuff, arg14, true);
				break;
			}
			case 15:
			{
				Debuger.Info("SuckBlood", new object[0]);
				BattleAction_SuckBlood arg15 = this.ByteToObject<BattleAction_SuckBlood>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_SuckBlood, bool>(BattleActionEvent.SuckBlood, arg15, true);
				break;
			}
			case 16:
			{
				Debuger.Info("LegalizeHp", new object[0]);
				BattleAction_LegalizeHp arg16 = this.ByteToObject<BattleAction_LegalizeHp>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_LegalizeHp, bool>(BattleActionEvent.LegalizeHp, arg16, true);
				break;
			}
			case 17:
			{
				Debuger.Info("AddSkill", new object[0]);
				BattleAction_AddSkill arg17 = this.ByteToObject<BattleAction_AddSkill>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_AddSkill, bool>(BattleActionEvent.AddSkill, arg17, true);
				break;
			}
			case 18:
			{
				Debuger.Info("RemoveSkill", new object[0]);
				BattleAction_RemoveSkill arg18 = this.ByteToObject<BattleAction_RemoveSkill>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_RemoveSkill, bool>(BattleActionEvent.RemoveSkill, arg18, true);
				break;
			}
			case 19:
			{
				Debuger.Info("AddFilter", new object[0]);
				BattleAction_AddFilter arg19 = this.ByteToObject<BattleAction_AddFilter>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_AddFilter, bool>(BattleActionEvent.AddFilter, arg19, true);
				break;
			}
			case 20:
			{
				Debuger.Info("RemoveFilter", new object[0]);
				BattleAction_RemoveFilter arg20 = this.ByteToObject<BattleAction_RemoveFilter>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_RemoveFilter, bool>(BattleActionEvent.RemoveFilter, arg20, true);
				break;
			}
			case 21:
			{
				Debuger.Info("Teleport", new object[0]);
				BattleAction_Teleport arg21 = this.ByteToObject<BattleAction_Teleport>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_Teleport, bool>(BattleActionEvent.Teleport, arg21, true);
				break;
			}
			case 50:
			{
				Debuger.Info("Fixed", new object[0]);
				BattleAction_Fix arg22 = this.ByteToObject<BattleAction_Fix>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_Fix, bool>(BattleActionEvent.Fix, arg22, true);
				break;
			}
			case 51:
			{
				Debuger.Info("Relaxed", new object[0]);
				BattleAction_EndFix arg23 = this.ByteToObject<BattleAction_EndFix>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndFix, bool>(BattleActionEvent.EndFix, arg23, true);
				break;
			}
			case 52:
			{
				Debuger.Info("Static", new object[0]);
				BattleAction_Static arg24 = this.ByteToObject<BattleAction_Static>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_Static, bool>(BattleActionEvent.Static, arg24, true);
				break;
			}
			case 53:
			{
				Debuger.Info("EndStatic", new object[0]);
				BattleAction_EndStatic arg25 = this.ByteToObject<BattleAction_EndStatic>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndStatic, bool>(BattleActionEvent.EndStatic, arg25, true);
				break;
			}
			case 54:
			{
				Debuger.Info("Taunt", new object[0]);
				BattleAction_Taunt arg26 = this.ByteToObject<BattleAction_Taunt>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_Taunt, bool>(BattleActionEvent.Taunt, arg26, true);
				break;
			}
			case 55:
			{
				Debuger.Info("EndTaunt", new object[0]);
				BattleAction_EndTaunt arg27 = this.ByteToObject<BattleAction_EndTaunt>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndTaunt, bool>(BattleActionEvent.EndTaunt, arg27, true);
				break;
			}
			case 56:
			{
				Debuger.Info("SuperArmor", new object[0]);
				BattleAction_SuperArmor arg28 = this.ByteToObject<BattleAction_SuperArmor>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_SuperArmor, bool>(BattleActionEvent.SuperArmor, arg28, true);
				break;
			}
			case 57:
			{
				Debuger.Info("EndSuperArmor", new object[0]);
				BattleAction_EndSuperArmor arg29 = this.ByteToObject<BattleAction_EndSuperArmor>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndSuperArmor, bool>(BattleActionEvent.EndSuperArmor, arg29, true);
				break;
			}
			case 58:
			{
				Debuger.Info("IgnoreDmgFormula", new object[0]);
				BattleAction_IgnoreDmgFormula arg30 = this.ByteToObject<BattleAction_IgnoreDmgFormula>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_IgnoreDmgFormula, bool>(BattleActionEvent.IgnoreFormula, arg30, true);
				break;
			}
			case 59:
			{
				Debuger.Info("EndIgnoreDmgFormula", new object[0]);
				BattleAction_EndIgnoreDmgFormula arg31 = this.ByteToObject<BattleAction_EndIgnoreDmgFormula>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndIgnoreDmgFormula, bool>(BattleActionEvent.EndIgnoreFormula, arg31, true);
				break;
			}
			case 60:
			{
				Debuger.Info("CloseRenderer", new object[0]);
				BattleAction_CloseRenderer arg32 = this.ByteToObject<BattleAction_CloseRenderer>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_CloseRenderer, bool>(BattleActionEvent.CloseRenderer, arg32, true);
				break;
			}
			case 61:
			{
				Debuger.Info("EndCloseRenderer", new object[0]);
				BattleAction_EndCloseRenderer arg33 = this.ByteToObject<BattleAction_EndCloseRenderer>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndCloseRenderer, bool>(BattleActionEvent.EndCloseRenderer, arg33, true);
				break;
			}
			case 62:
			{
				Debuger.Info("Stun", new object[0]);
				BattleAction_Stun arg34 = this.ByteToObject<BattleAction_Stun>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_Stun, bool>(BattleActionEvent.Dizzy, arg34, true);
				break;
			}
			case 63:
			{
				Debuger.Info("EndStun", new object[0]);
				BattleAction_EndStun arg35 = this.ByteToObject<BattleAction_EndStun>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndStun, bool>(BattleActionEvent.EndDizzy, arg35, true);
				break;
			}
			case 64:
			{
				Debuger.Info("MoveCast", new object[0]);
				BattleAction_MoveCast arg36 = this.ByteToObject<BattleAction_MoveCast>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_MoveCast, bool>(BattleActionEvent.MoveCast, arg36, true);
				break;
			}
			case 65:
			{
				Debuger.Info("EndMoveCast", new object[0]);
				BattleAction_EndMoveCast arg37 = this.ByteToObject<BattleAction_EndMoveCast>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndMoveCast, bool>(BattleActionEvent.EndMoveCast, arg37, true);
				break;
			}
			case 67:
			{
				Debuger.Info("EndFitAction", new object[0]);
				BattleAction_EndFitAction arg38 = this.ByteToObject<BattleAction_EndFitAction>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndFitAction, bool>(BattleActionEvent.EndFitAction, arg38, true);
				break;
			}
			case 68:
			{
				Debuger.Info("Assault", new object[0]);
				BattleAction_Assault battleAction_Assault = this.ByteToObject<BattleAction_Assault>(battleAction.argBuf);
				Debuger.Info(string.Concat(new object[]
				{
					"Assault:",
					battleAction_Assault.soldierId,
					" ",
					PosDirUtility.ToDetailString(battleAction_Assault.toPos)
				}), new object[0]);
				if (battleAction_Assault.soldierId == EntityWorld.Instance.EntSelf.ID)
				{
					Debug.LogError("Fuck2");
				}
				EventDispatcher.Broadcast<BattleAction_Assault, bool>(BattleActionEvent.Assault, battleAction_Assault, true);
				break;
			}
			case 69:
			{
				Debuger.Info("EndAssault", new object[0]);
				BattleAction_EndAssault battleAction_EndAssault = this.ByteToObject<BattleAction_EndAssault>(battleAction.argBuf);
				Debuger.Info("EndAssault:" + PosDirUtility.ToDetailString(battleAction_EndAssault.pos), new object[0]);
				if (battleAction_EndAssault.soldierId == EntityWorld.Instance.EntSelf.ID)
				{
					Debug.LogError("Fuck3");
				}
				EventDispatcher.Broadcast<BattleAction_EndAssault, bool>(BattleActionEvent.EndAssault, battleAction_EndAssault, true);
				break;
			}
			case 71:
			{
				Debuger.Info("EndKnock", new object[0]);
				BattleAction_EndKnock battleAction_EndKnock = this.ByteToObject<BattleAction_EndKnock>(battleAction.argBuf);
				if (battleAction_EndKnock.soldierId == EntityWorld.Instance.EntSelf.ID)
				{
					Debug.LogError("Fuck4");
				}
				EventDispatcher.Broadcast<BattleAction_EndKnock, bool>(BattleActionEvent.EndKnock, battleAction_EndKnock, true);
				break;
			}
			case 73:
			{
				Debuger.Info("EndSkillManage", new object[0]);
				BattleAction_EndSkillManage battleAction_EndSkillManage = this.ByteToObject<BattleAction_EndSkillManage>(battleAction.argBuf);
				if (battleAction_EndSkillManage.soldierId == EntityWorld.Instance.EntSelf.ID)
				{
					Debug.LogError("Fuck5");
				}
				EventDispatcher.Broadcast<BattleAction_EndSkillManage, bool>(BattleActionEvent.EndSkillManage, battleAction_EndSkillManage, true);
				break;
			}
			case 75:
			{
				Debuger.Info("EndLoading", new object[0]);
				BattleAction_EndLoading arg39 = this.ByteToObject<BattleAction_EndLoading>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndLoading, bool>(BattleActionEvent.EndLoading, arg39, true);
				break;
			}
			case 77:
			{
				Debuger.Info("EndSkillPress", new object[0]);
				BattleAction_EndSkillPress arg40 = this.ByteToObject<BattleAction_EndSkillPress>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndSkillPress, bool>(BattleActionEvent.EndSkillPress, arg40, true);
				break;
			}
			case 78:
			{
				Debuger.Info("MakeDead", new object[0]);
				BattleAction_MakeDead arg41 = this.ByteToObject<BattleAction_MakeDead>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_MakeDead, bool>(BattleActionEvent.MakeDead, arg41, true);
				break;
			}
			case 79:
			{
				Debuger.Info("AtkProof", new object[0]);
				BattleAction_AtkProof arg42 = this.ByteToObject<BattleAction_AtkProof>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_AtkProof, bool>(BattleActionEvent.AtkProof, arg42, true);
				break;
			}
			case 80:
			{
				Debuger.Info("EndAtkProof", new object[0]);
				BattleAction_EndAtkProof arg43 = this.ByteToObject<BattleAction_EndAtkProof>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndAtkProof, bool>(BattleActionEvent.EndAtkProof, arg43, true);
				break;
			}
			case 81:
			{
				Debuger.Info("NewBatchNty", new object[0]);
				BattleAction_NewBatchNty arg44 = this.ByteToObject<BattleAction_NewBatchNty>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_NewBatchNty, bool>(BattleActionEvent.NewBatchNty, arg44, true);
				break;
			}
			case 82:
			{
				Debuger.Info("AllLoadDoneNty", new object[0]);
				BattleAction_AllLoadDoneNty arg45 = this.ByteToObject<BattleAction_AllLoadDoneNty>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_AllLoadDoneNty, bool>(BattleActionEvent.AllLoadDoneNty, arg45, true);
				break;
			}
			case 83:
			{
				Debuger.Info("Weak", new object[0]);
				BattleAction_Weak arg46 = this.ByteToObject<BattleAction_Weak>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_Weak, bool>(BattleActionEvent.Weak, arg46, true);
				break;
			}
			case 84:
			{
				Debuger.Info("EndWeak", new object[0]);
				BattleAction_EndWeak arg47 = this.ByteToObject<BattleAction_EndWeak>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndWeak, bool>(BattleActionEvent.EndWeak, arg47, true);
				break;
			}
			case 85:
			{
				Debuger.Info("ChangeCamp", new object[0]);
				BattleAction_ChangeCamp arg48 = this.ByteToObject<BattleAction_ChangeCamp>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_ChangeCamp, bool>(BattleActionEvent.ChangeCamp, arg48, true);
				break;
			}
			case 86:
			{
				Debuger.Info("Incurable", new object[0]);
				BattleAction_Incurable arg49 = this.ByteToObject<BattleAction_Incurable>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_Incurable, bool>(BattleActionEvent.Incurable, arg49, true);
				break;
			}
			case 87:
			{
				Debuger.Info("EndIncurable", new object[0]);
				BattleAction_EndIncurable arg50 = this.ByteToObject<BattleAction_EndIncurable>(battleAction.argBuf);
				EventDispatcher.Broadcast<BattleAction_EndIncurable, bool>(BattleActionEvent.EndIncurable, arg50, true);
				break;
			}
			}
		}
	}

	protected void OnUseSkillRes(short state, UseSkillRes down = null)
	{
		this.ServerVitalReceiveCount++;
		if (state == 0)
		{
			return;
		}
		Debuger.Error("OnUseSkillRes: " + state, new object[0]);
		StateManager.Instance.StateShow(state, 0);
	}

	protected void OnCancelUseSkillRes(short state, CancelUseSkillRes down = null)
	{
		if (!SystemConfig.IsShowBattleState)
		{
			return;
		}
		if (state == 0)
		{
			return;
		}
		StateManager.Instance.StateShow(state, 0);
	}

	protected void OnAddEffectRes(short state, AddEffectRes down = null)
	{
		if (!SystemConfig.IsShowBattleState)
		{
			return;
		}
		if (state == 0)
		{
			return;
		}
		StateManager.Instance.StateShow(state, 0);
	}

	protected void OnUpdateEffectRes(short state, UpdateEffectRes down = null)
	{
		if (!SystemConfig.IsShowBattleState)
		{
			return;
		}
		if (state == 0)
		{
			return;
		}
		StateManager.Instance.StateShow(state, 0);
	}

	protected void OnAssaultRes(short state, AssaultRes down = null)
	{
		this.ServerVitalReceiveCount++;
	}

	protected void OnEndFitActionRes(short state, EndFitActionRes down = null)
	{
		if (!SystemConfig.IsShowBattleState)
		{
			return;
		}
		if (state == 0)
		{
			return;
		}
		StateManager.Instance.StateShow(state, 0);
	}

	protected void OnClientDrvBattleCalcActPointRes(short state, ClientDrvBattleCalcActPointRes down = null)
	{
		if (!SystemConfig.IsShowBattleState)
		{
			return;
		}
		if (state == 0)
		{
			return;
		}
		StateManager.Instance.StateShow(state, 0);
	}

	protected void OnClientDrvBattleEffectDmgReportRes(short state, ClientDrvBattleEffectDmgReportRes down = null)
	{
		if (!SystemConfig.IsShowBattleState)
		{
			return;
		}
		if (state == 0)
		{
			return;
		}
		StateManager.Instance.StateShow(state, 0);
	}

	protected void OnClientDrvBattleBuffDmgReportRes(short state, ClientDrvBattleBuffDmgReportRes down = null)
	{
		if (!SystemConfig.IsShowBattleState)
		{
			return;
		}
		if (state == 0)
		{
			return;
		}
		StateManager.Instance.StateShow(state, 0);
	}

	protected void OnClientDrvBattleVerifyInfoNty(short state, ClientDrvBattleVerifyInfoNty down = null)
	{
	}

	protected void OnBattleReconnectCacheEndNty(short state, BattleReconnCacheEndNty down = null)
	{
		Debug.Log("OnBattleReconnectCacheEndNty");
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		if (!down.isRoleManaging)
		{
			EntityWorld.Instance.EntSelf.IsSynchronizingServerBattle = false;
		}
	}

	public void SendMove(float theX, float theY)
	{
		if (InstanceManager.IsServerBattle)
		{
			this.ServerVitalSendCount++;
		}
		NetworkManager.Send(new RoleMoveReq
		{
			toPos = new Pos
			{
				x = theX,
				y = theY
			}
		}, ServerType.Data);
	}

	public void SendPreciseMove(float theX, float theY)
	{
		if (InstanceManager.IsServerBattle)
		{
			this.ServerVitalSendCount++;
		}
		NetworkManager.Send(new RolePreciseMoveToReq
		{
			toPos = new Pos
			{
				x = theX,
				y = theY
			}
		}, ServerType.Data);
	}

	public void SendUseSkill(long casterID, int skillID, long targetID, bool isSkillNeedTrustee, Vector3 casterPos, Vector3 casterDir)
	{
		switch (InstanceManager.CurrentCommunicationType)
		{
		case CommunicationType.Client:
			LocalBattleHandler.Instance.HandleUseSkill(casterID, targetID, skillID, false);
			break;
		case CommunicationType.Server:
			if (this.CheckIsSelf(casterID))
			{
				Debug.Log(string.Concat(new object[]
				{
					"上报 casterID: ",
					casterID,
					" skillID: ",
					skillID,
					" targetID: ",
					targetID,
					" willManaged: ",
					isSkillNeedTrustee,
					" Dir: (",
					casterDir.x * 100f,
					", ",
					casterDir.z * 100f,
					") "
				}));
				this.ServerVitalSendCount++;
				NetworkManager.Send(new UseSkillReq
				{
					targetId = targetID,
					skillId = skillID,
					willManaged = (!isSkillNeedTrustee) ? 0 : 1,
					pos = new Pos
					{
						x = casterPos.x * 100f,
						y = casterPos.z * 100f
					},
					vector = new Vector2
					{
						x = casterDir.x * 100f,
						y = casterDir.z * 100f
					}
				}, ServerType.Data);
			}
			break;
		case CommunicationType.Mixed:
		case CommunicationType.MixedEx:
			LocalBattleHandler.Instance.HandleUseSkill(casterID, targetID, skillID, true);
			break;
		}
	}

	public void SendCancelUseSkill(long casterID, int skillID)
	{
		switch (InstanceManager.CurrentCommunicationType)
		{
		case CommunicationType.Client:
			LocalBattleHandler.Instance.HandleCancelUseSkill(casterID, skillID);
			break;
		case CommunicationType.Server:
			Debuger.Error(string.Concat(new object[]
			{
				"SendCancelUseSkill: ",
				casterID,
				" skillID: ",
				skillID
			}), new object[0]);
			if (this.CheckIsSelf(casterID))
			{
				NetworkManager.Send(new CancelUseSkillReq
				{
					skillId = skillID
				}, ServerType.Data);
			}
			break;
		case CommunicationType.Mixed:
		case CommunicationType.MixedEx:
			LocalBattleHandler.Instance.HandleCancelUseSkill(casterID, skillID);
			break;
		}
	}

	public void SendAddEffect(long casterID, int skillID, int effectID, List<EffectTargetInfo> targetInfos, int effectUniqueID, XPoint basePoint, bool isClientHandle)
	{
		switch (InstanceManager.CurrentCommunicationType)
		{
		case CommunicationType.Client:
			LocalBattleHandler.Instance.HandleAddEffect(casterID, skillID, effectID, targetInfos, basePoint, effectUniqueID, false);
			break;
		case CommunicationType.Server:
			if (this.CheckIsSelf(casterID) || isClientHandle)
			{
				AddEffectReq addEffectReq = new AddEffectReq();
				addEffectReq.casterId = casterID;
				addEffectReq.effectId = effectID;
				addEffectReq.uniqueId = (long)effectUniqueID;
				addEffectReq.skillId = skillID;
				if (basePoint == null)
				{
					addEffectReq.pos = new Pos();
					addEffectReq.vector = new Vector2();
				}
				else
				{
					addEffectReq.pos = new Pos
					{
						x = basePoint.position.x * 100f,
						y = basePoint.position.z * 100f
					};
					addEffectReq.vector = new Vector2
					{
						x = (basePoint.rotation * Vector3.get_forward()).x,
						y = (basePoint.rotation * Vector3.get_forward()).z
					};
				}
				addEffectReq.targets.AddRange(targetInfos);
				NetworkManager.Send(addEffectReq, ServerType.Data);
			}
			break;
		case CommunicationType.Mixed:
		case CommunicationType.MixedEx:
			LocalBattleHandler.Instance.HandleAddEffect(casterID, skillID, effectID, targetInfos, basePoint, effectUniqueID, true);
			break;
		}
	}

	public void SendUpdateEffect(long casterID, int skillID, int effectID, List<EffectTargetInfo> targetInfos, int effectUniqueID, XPoint basePoint, bool isClientHandle)
	{
		switch (InstanceManager.CurrentCommunicationType)
		{
		case CommunicationType.Client:
			LocalBattleHandler.Instance.HandleUpdateEffect(casterID, skillID, effectID, targetInfos, basePoint, effectUniqueID, false);
			break;
		case CommunicationType.Server:
			if (this.CheckIsSelf(casterID) || isClientHandle)
			{
				UpdateEffectReq updateEffectReq = new UpdateEffectReq();
				updateEffectReq.casterId = casterID;
				updateEffectReq.uniqueId = (long)effectUniqueID;
				updateEffectReq.targets.AddRange(targetInfos);
				if (basePoint == null)
				{
					updateEffectReq.pos = new Pos();
					updateEffectReq.vector = new Vector2();
				}
				else
				{
					updateEffectReq.pos = new Pos
					{
						x = basePoint.position.x * 100f,
						y = basePoint.position.z * 100f
					};
					updateEffectReq.vector = new Vector2
					{
						x = (basePoint.rotation * Vector3.get_forward()).x,
						y = (basePoint.rotation * Vector3.get_forward()).z
					};
				}
				NetworkManager.Send(updateEffectReq, ServerType.Data);
			}
			break;
		case CommunicationType.Mixed:
		case CommunicationType.MixedEx:
			LocalBattleHandler.Instance.HandleUpdateEffect(casterID, skillID, effectID, targetInfos, basePoint, effectUniqueID, true);
			break;
		}
	}

	public void SendAssault(long casterID, Vector3 pos, Vector3 dir, Vector3 endPosition)
	{
		CommunicationType currentCommunicationType = InstanceManager.CurrentCommunicationType;
		if (currentCommunicationType == CommunicationType.Server)
		{
			Debuger.Info(casterID + " SendAssault: " + pos.ToString(), new object[0]);
			this.ServerVitalSendCount++;
			NetworkManager.Send(new AssaultReq
			{
				curPos = new Pos
				{
					x = pos.x * 100f,
					y = pos.z * 100f
				},
				curVector = new Vector2
				{
					x = dir.x,
					y = dir.z
				},
				toPos = new Pos
				{
					x = endPosition.x * 100f,
					y = endPosition.z * 100f
				}
			}, ServerType.Data);
		}
	}

	public void SendEndAssault(long casterID, Vector3 casterPos, Vector3 casterDir)
	{
		CommunicationType currentCommunicationType = InstanceManager.CurrentCommunicationType;
		if (currentCommunicationType == CommunicationType.Server)
		{
			Debuger.Info(string.Concat(new object[]
			{
				casterID,
				" SendEndAssault: ",
				casterPos.x * 100f,
				" ",
				casterPos.z * 100f
			}), new object[0]);
			NetworkManager.Send(new EndAssaultReq
			{
				pos = new Pos
				{
					x = casterPos.x * 100f,
					y = casterPos.z * 100f
				},
				vector = new Vector2
				{
					x = casterDir.x,
					y = casterDir.z
				}
			}, ServerType.Data);
		}
	}

	public void SendEndKnock(long targetID, Vector3 targetPos, Vector3 targetDir, int uniqueID)
	{
		CommunicationType currentCommunicationType = InstanceManager.CurrentCommunicationType;
		if (currentCommunicationType == CommunicationType.Server)
		{
			Debuger.Info(string.Concat(new object[]
			{
				"SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSendEndKnock: ",
				targetPos.x * 100f,
				" ",
				targetPos.z * 100f,
				" ",
				uniqueID
			}), new object[0]);
			NetworkManager.Send(new EndKnockReq
			{
				soldierId = targetID,
				pos = new Pos
				{
					x = targetPos.x * 100f,
					y = targetPos.z * 100f
				},
				vector = new Vector2
				{
					x = targetDir.x,
					y = targetDir.z
				},
				mgrSn = uniqueID
			}, ServerType.Data);
		}
	}

	public void SendEndSkillManage(long targetID, Vector3 targetPos, Vector3 targetDir, int uniqueID)
	{
		CommunicationType currentCommunicationType = InstanceManager.CurrentCommunicationType;
		if (currentCommunicationType == CommunicationType.Server)
		{
			Debug.Log(string.Concat(new object[]
			{
				"----------------------targetID: ",
				targetID,
				" targetCurPos(",
				targetPos.x * 100f,
				",",
				targetPos.z * 100f,
				") targetDir(",
				targetDir.x,
				",",
				targetDir.z,
				") ",
				uniqueID
			}));
			NetworkManager.Send(new EndSkillManageReq
			{
				managedId = targetID,
				pos = new Pos
				{
					x = targetPos.x * 100f,
					y = targetPos.z * 100f
				},
				vector = new Vector2
				{
					x = targetDir.x,
					y = targetDir.z
				},
				mgrSn = uniqueID
			}, ServerType.Data);
		}
	}

	public void SendEndFusing(long casterID)
	{
		CommunicationType currentCommunicationType = InstanceManager.CurrentCommunicationType;
		if (currentCommunicationType == CommunicationType.Server)
		{
			if (this.CheckIsSelf(casterID))
			{
				NetworkManager.Send(typeof(EndFitActionReq), null, ServerType.Data);
			}
		}
	}

	public void SendEndRepeatSkill(long casterID, int skillID)
	{
		CommunicationType currentCommunicationType = InstanceManager.CurrentCommunicationType;
		if (currentCommunicationType == CommunicationType.Server)
		{
			if (this.CheckIsSelf(casterID))
			{
				NetworkManager.Send(new EndSkillPressReq
				{
					skillId = skillID
				}, ServerType.Data);
			}
		}
	}

	public void SendClientDriveBattleSkill(long casterID, long targetID, int skillID)
	{
	}

	public void SendClientDriveBattleEffectDamage(long casterID, long targetID, long casterHp, long targetHp, long hpChangeValue, int skillID, int effectID, bool isNeedCalCasterActPoint, bool isNeedCalTargetVp, List<ClientDrvBuffInfo> casterBuffInfo, List<ClientDrvBuffInfo> targetBuffInfo, XPoint basePoint, List<long> randomIndexes, string testStr, bool isMixExForceSend = false)
	{
		CommunicationType currentCommunicationType = InstanceManager.CurrentCommunicationType;
		if (currentCommunicationType != CommunicationType.Mixed)
		{
			if (currentCommunicationType != CommunicationType.MixedEx)
			{
				return;
			}
			if (!isMixExForceSend)
			{
				return;
			}
		}
		ClientDrvBattleEffectDmgReportReq clientDrvBattleEffectDmgReportReq = new ClientDrvBattleEffectDmgReportReq();
		clientDrvBattleEffectDmgReportReq.fromId = casterID;
		clientDrvBattleEffectDmgReportReq.toId = targetID;
		clientDrvBattleEffectDmgReportReq.skillId = skillID;
		clientDrvBattleEffectDmgReportReq.effectId = effectID;
		clientDrvBattleEffectDmgReportReq.calcCasterActPoint = isNeedCalCasterActPoint;
		clientDrvBattleEffectDmgReportReq.calcTargetVp = isNeedCalTargetVp;
		if (casterBuffInfo != null && casterBuffInfo.get_Count() > 0)
		{
			clientDrvBattleEffectDmgReportReq.fromBuffInfos.AddRange(casterBuffInfo);
		}
		if (targetBuffInfo != null && targetBuffInfo.get_Count() > 0)
		{
			clientDrvBattleEffectDmgReportReq.toBuffInfos.AddRange(targetBuffInfo);
		}
		clientDrvBattleEffectDmgReportReq.realId = EntityWorld.Instance.EntSelf.ID;
		if (basePoint != null)
		{
			clientDrvBattleEffectDmgReportReq.basePos = new Pos
			{
				x = basePoint.position.x * 100f,
				y = basePoint.position.z * 100f
			};
			clientDrvBattleEffectDmgReportReq.baseVec = new Vector2
			{
				x = (basePoint.rotation * Vector3.get_forward()).x,
				y = (basePoint.rotation * Vector3.get_forward()).z
			};
		}
		clientDrvBattleEffectDmgReportReq.vertifyInfo = new BattleVertifyInfo();
		clientDrvBattleEffectDmgReportReq.vertifyInfo.casterHp = casterHp;
		clientDrvBattleEffectDmgReportReq.vertifyInfo.targetHp = targetHp;
		clientDrvBattleEffectDmgReportReq.vertifyInfo.randIndex.AddRange(randomIndexes);
		clientDrvBattleEffectDmgReportReq.vertifyInfo.testStr = string.Concat(new object[]
		{
			"13_",
			testStr,
			"_",
			hpChangeValue
		});
		BattleCalculator.RecordPacket(clientDrvBattleEffectDmgReportReq);
		NetworkManager.Send(clientDrvBattleEffectDmgReportReq, ServerType.Data);
	}

	public void SendClientDriveBattleBuffDamage(long casterID, long targetID, long casterHp, long targetHp, long hpChangeValue, int buffID, bool isNeedCalTargetVp, List<ClientDrvBuffInfo> casterBuffInfo, List<ClientDrvBuffInfo> targetBuffInfo, List<long> randomIndexes, string testStr)
	{
		CommunicationType currentCommunicationType = InstanceManager.CurrentCommunicationType;
		if (currentCommunicationType == CommunicationType.Mixed)
		{
			ClientDrvBattleBuffDmgReportReq clientDrvBattleBuffDmgReportReq = new ClientDrvBattleBuffDmgReportReq();
			clientDrvBattleBuffDmgReportReq.fromId = casterID;
			clientDrvBattleBuffDmgReportReq.toId = targetID;
			clientDrvBattleBuffDmgReportReq.buffId = buffID;
			clientDrvBattleBuffDmgReportReq.calcTargetVp = isNeedCalTargetVp;
			if (casterBuffInfo != null && casterBuffInfo.get_Count() > 0)
			{
				clientDrvBattleBuffDmgReportReq.fromBuffInfos.AddRange(casterBuffInfo);
			}
			if (targetBuffInfo != null && targetBuffInfo.get_Count() > 0)
			{
				clientDrvBattleBuffDmgReportReq.toBuffInfos.AddRange(targetBuffInfo);
			}
			clientDrvBattleBuffDmgReportReq.realId = EntityWorld.Instance.EntSelf.ID;
			clientDrvBattleBuffDmgReportReq.vertifyInfo = new BattleVertifyInfo();
			clientDrvBattleBuffDmgReportReq.vertifyInfo.casterHp = casterHp;
			clientDrvBattleBuffDmgReportReq.vertifyInfo.targetHp = targetHp;
			clientDrvBattleBuffDmgReportReq.vertifyInfo.randIndex.AddRange(randomIndexes);
			clientDrvBattleBuffDmgReportReq.vertifyInfo.testStr = string.Concat(new object[]
			{
				"13_",
				testStr,
				"_",
				hpChangeValue
			});
			BattleCalculator.RecordPacket(clientDrvBattleBuffDmgReportReq);
			NetworkManager.Send(clientDrvBattleBuffDmgReportReq, ServerType.Data);
		}
	}

	public void SendClientDriveWeakState(long targetID, bool isWeak)
	{
	}

	public void SendClientDrvBattleDeathNty(long targetID)
	{
		CommunicationType currentCommunicationType = InstanceManager.CurrentCommunicationType;
		if (currentCommunicationType == CommunicationType.MixedEx)
		{
			NetworkManager.Send(new ClientDrvBattleDeathNty
			{
				soldierId = targetID
			}, ServerType.Data);
		}
	}

	protected void CowardCheckIsServerEnable(int sendCount, int receiveCount, bool isSendCheck)
	{
		if (isSendCheck)
		{
			this.IsServerEnableWithCowardCheck = (sendCount <= receiveCount);
		}
		else if (this.ServerVitalSendCount <= this.ServerVitalReceiveCount)
		{
			this.IsServerEnableWithCowardCheck = true;
		}
		GlobalBattleNetwork.CowardCheckType cowardCheckType = this.CurCowardCheckType;
		if (cowardCheckType != GlobalBattleNetwork.CowardCheckType.Time)
		{
			if (cowardCheckType != GlobalBattleNetwork.CowardCheckType.Count)
			{
			}
		}
		else if (this.IsServerEnableWithCowardCheck)
		{
			this.IsCowardTimeChecking = false;
			TimerHeap.DelTimer(this.cowardTimeCheckTimer);
		}
	}
}
