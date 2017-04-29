using EntitySubSystem;
using GameData;
using Package;
using System;
using System.Collections.Generic;

public class LocalBattleHandler : ILocalBattleData
{
	protected const float UpdateInterval = 0.1f;

	protected XDict<long, XDict<int, BuffState>> buffStateTable = new XDict<long, XDict<int, BuffState>>();

	protected XDict<long, List<int>> intervalBuffList = new XDict<long, List<int>>();

	protected XDict<long, List<int>> removeBuffList = new XDict<long, List<int>>();

	protected List<int> globalBuff = new List<int>();

	private object thisLock = new object();

	protected static LocalBattleHandler instance;

	protected bool hasInit;

	protected bool isEnable;

	protected bool isEnableCalculate = true;

	protected float totalDeltaTime;

	protected XDict<long, FuseState> fuseStateTable = new XDict<long, FuseState>();

	protected XDict<long, XDict<int, long>> skillStateTable = new XDict<long, XDict<int, long>>();

	public static LocalBattleHandler Instance
	{
		get
		{
			if (LocalBattleHandler.instance == null)
			{
				LocalBattleHandler.instance = new LocalBattleHandler();
			}
			return LocalBattleHandler.instance;
		}
	}

	protected bool HasInit
	{
		get
		{
			return this.hasInit;
		}
		set
		{
			this.hasInit = value;
		}
	}

	protected bool IsEnable
	{
		get
		{
			return this.isEnable;
		}
		set
		{
			this.isEnable = value;
		}
	}

	public bool IsEnableCalculate
	{
		get
		{
			return this.isEnableCalculate;
		}
		set
		{
			this.isEnableCalculate = value;
		}
	}

	public void AddGlobalBuff(EntityParent target)
	{
		EntityParent entSelf = EntityWorld.Instance.EntSelf;
		for (int i = 0; i < this.globalBuff.get_Count(); i++)
		{
			Buff buffDataByID = LocalAgent.GetBuffDataByID(this.globalBuff.get_Item(i));
			if (this.CheckAddGlobalBuff(buffDataByID, entSelf, target))
			{
				this.InitGlobalBuff(buffDataByID, entSelf, target);
			}
		}
	}

	protected bool CheckAddGlobalBuff(Buff buffData, EntityParent caster, EntityParent target)
	{
		if (buffData == null)
		{
			return false;
		}
		if (caster == null)
		{
			return false;
		}
		if (target == null)
		{
			return false;
		}
		if (!EntityWorld.Instance.TargetTypeFilter<EntityParent>(target, caster, buffData.globalTarget))
		{
			return false;
		}
		if (!BattleCalculator.CalculateAddBuff(caster.BattleBaseAttrs, target.BattleBaseAttrs, (double)buffData.buffProp, 0))
		{
			return false;
		}
		XDict<int, BuffState> buffListByEntityID = this.GetBuffListByEntityID(target.ID);
		if (buffListByEntityID == null)
		{
			return false;
		}
		for (int i = 0; i < buffListByEntityID.Count; i++)
		{
			if (LocalAgent.GetBuffDataByID(buffListByEntityID.ElementKeyAt(i)).resist.Contains(buffData.type))
			{
				return false;
			}
		}
		for (int j = 0; j < buffListByEntityID.Count; j++)
		{
			if (LocalAgent.GetBuffDataByID(buffListByEntityID.ElementKeyAt(j)).resistId.Contains(buffData.id))
			{
				return false;
			}
		}
		List<int> list = new List<int>();
		for (int k = 0; k < buffListByEntityID.Count; k++)
		{
			if (LocalAgent.GetBuffDataByID(buffListByEntityID.ElementKeyAt(k)).cover.Contains(buffData.type))
			{
				list.Add(buffListByEntityID.ElementKeyAt(k));
			}
		}
		for (int l = 0; l < list.get_Count(); l++)
		{
			this.AppRemoveBuff(list.get_Item(l), target.ID);
		}
		list.Clear();
		for (int m = 0; m < buffListByEntityID.Count; m++)
		{
			if (buffData.delete.Contains(LocalAgent.GetBuffDataByID(buffListByEntityID.ElementKeyAt(m)).type))
			{
				list.Add(buffListByEntityID.ElementKeyAt(m));
			}
		}
		for (int n = 0; n < list.get_Count(); n++)
		{
			this.AppRemoveBuff(list.get_Item(n), target.ID);
		}
		if (LocalAgent.CheckBuffByTargetIDAndBuffID(target.ID, buffData.id))
		{
			int overlayModeId = buffData.overlayModeId;
			if (overlayModeId != 1 && overlayModeId != 2)
			{
				this.AppRemoveBuff(buffData.id, target.ID);
			}
		}
		return true;
	}

	protected void InitGlobalBuff(Buff buffData, EntityParent caster, EntityParent target)
	{
		double num = BattleCalculator.CalculateBuffTime(caster.BattleBaseAttrs.GetBuffCtrlAttrs(0), target.BattleBaseAttrs.GetBuffCtrlAttrs(0), (double)buffData.time);
		if (LocalAgent.CheckBuffByTargetIDAndBuffID(target.ID, buffData.id))
		{
			int overlayModeId = buffData.overlayModeId;
			if (overlayModeId != 1)
			{
				if (overlayModeId == 2)
				{
					this.buffStateTable[target.ID][buffData.id].removeLeftTime = num;
				}
			}
			else
			{
				this.buffStateTable[target.ID][buffData.id].removeLeftTime += num;
			}
			return;
		}
		BuffState buffState = new BuffState();
		buffState.isBlock = false;
		buffState.isCommunicateMix = true;
		buffState.isGlobalBuff = true;
		buffState.casterID = caster.ID;
		buffState.fromSkillID = 0;
		buffState.fromSkillLevel = 0;
		buffState.fromSkillAttrChange = null;
		buffState.intervalDefaultTime = (float)buffData.interval;
		buffState.intervalLeftTime = (float)buffData.interval;
		buffState.removeLeftTime = num;
		if (!this.buffStateTable.ContainsKey(target.ID))
		{
			this.buffStateTable.Add(target.ID, new XDict<int, BuffState>());
		}
		this.buffStateTable[target.ID].Add(buffData.id, buffState);
		this.HandleBuff(buffData, caster, target, 0, buffState.fromSkillLevel, buffState.fromSkillAttrChange, true);
		AddBuffAnnouncer.Announce(LocalAgent.GetEntityByID(target.ID), buffData.id);
		LocalBattleProtocolSimulator.SendAddBuff(caster.ID, target.ID, buffData.id, (int)num);
	}

	public void AppAddBuff(int buffID, EntityParent caster, long targetID, int fromSkillID, int elementType = 0, bool isCommunicateMix = false)
	{
		Buff buffDataByID = LocalAgent.GetBuffDataByID(buffID);
		EntityParent entityByID = LocalAgent.GetEntityByID(targetID);
		if (this.CheckBuff(buffDataByID, caster, entityByID, fromSkillID, elementType, isCommunicateMix))
		{
			this.InitBuff(buffDataByID, caster, entityByID, fromSkillID, elementType, isCommunicateMix);
		}
	}

	protected bool CheckBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillID, int elementType, bool isCommunicateMix = false)
	{
		if (buffData == null)
		{
			return false;
		}
		if (caster == null)
		{
			return false;
		}
		if (target == null)
		{
			return false;
		}
		if (LocalAgent.GetSpiritIsDead(target, isCommunicateMix))
		{
			return false;
		}
		if (!target.IsFighting)
		{
			return false;
		}
		if (!BattleCalculator.CalculateAddBuff(caster.BattleBaseAttrs, target.BattleBaseAttrs, (double)buffData.buffProp, elementType))
		{
			return false;
		}
		XDict<int, BuffState> buffListByEntityID = this.GetBuffListByEntityID(target.ID);
		if (buffListByEntityID == null)
		{
			return false;
		}
		for (int i = 0; i < buffListByEntityID.Count; i++)
		{
			if (LocalAgent.GetBuffDataByID(buffListByEntityID.ElementKeyAt(i)).resist.Contains(buffData.type))
			{
				return false;
			}
		}
		for (int j = 0; j < buffListByEntityID.Count; j++)
		{
			if (LocalAgent.GetBuffDataByID(buffListByEntityID.ElementKeyAt(j)).resistId.Contains(buffData.id))
			{
				return false;
			}
		}
		List<int> list = new List<int>();
		for (int k = 0; k < buffListByEntityID.Count; k++)
		{
			if (LocalAgent.GetBuffDataByID(buffListByEntityID.ElementKeyAt(k)).cover.Contains(buffData.type))
			{
				list.Add(buffListByEntityID.ElementKeyAt(k));
			}
		}
		for (int l = 0; l < buffListByEntityID.Count; l++)
		{
			if (LocalAgent.GetBuffDataByID(buffListByEntityID.ElementKeyAt(l)).coverId.Contains(buffData.id))
			{
				list.Add(buffListByEntityID.ElementKeyAt(l));
			}
		}
		for (int m = 0; m < list.get_Count(); m++)
		{
			this.AppRemoveBuff(list.get_Item(m), target.ID);
		}
		list.Clear();
		for (int n = 0; n < buffListByEntityID.Count; n++)
		{
			if (buffData.delete.Contains(LocalAgent.GetBuffDataByID(buffListByEntityID.ElementKeyAt(n)).type))
			{
				list.Add(buffListByEntityID.ElementKeyAt(n));
			}
		}
		for (int num = 0; num < list.get_Count(); num++)
		{
			this.AppRemoveBuff(list.get_Item(num), target.ID);
		}
		if (LocalAgent.CheckBuffByTargetIDAndBuffID(target.ID, buffData.id))
		{
			int overlayModeId = buffData.overlayModeId;
			if (overlayModeId != 1 && overlayModeId != 2)
			{
				this.AppRemoveBuff(buffData.id, target.ID);
			}
		}
		return true;
	}

	protected void InitBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillID, int elementType, bool isCommunicateMix = false)
	{
		double num = BattleCalculator.CalculateBuffTime(caster.BattleBaseAttrs.GetBuffCtrlAttrs(elementType), target.BattleBaseAttrs.GetBuffCtrlAttrs(elementType), (double)buffData.time);
		if (LocalAgent.CheckBuffByTargetIDAndBuffID(target.ID, buffData.id))
		{
			int overlayModeId = buffData.overlayModeId;
			if (overlayModeId != 1)
			{
				if (overlayModeId == 2)
				{
					this.buffStateTable[target.ID][buffData.id].removeLeftTime = num;
				}
			}
			else
			{
				this.buffStateTable[target.ID][buffData.id].removeLeftTime += num;
			}
			return;
		}
		BuffState buffState = new BuffState();
		buffState.isBlock = false;
		buffState.isCommunicateMix = isCommunicateMix;
		buffState.isGlobalBuff = false;
		buffState.casterID = caster.ID;
		buffState.fromSkillID = fromSkillID;
		buffState.fromSkillLevel = caster.GetSkillLevelByID(fromSkillID);
		buffState.fromSkillAttrChange = caster.GetSkillAttrChangeByID(fromSkillID);
		buffState.intervalDefaultTime = (float)buffData.interval;
		buffState.intervalLeftTime = (float)buffData.interval;
		buffState.removeLeftTime = num;
		if (!this.buffStateTable.ContainsKey(target.ID))
		{
			this.buffStateTable.Add(target.ID, new XDict<int, BuffState>());
		}
		this.buffStateTable[target.ID].Add(buffData.id, buffState);
		this.HandleBuff(buffData, caster, target, fromSkillID, buffState.fromSkillLevel, buffState.fromSkillAttrChange, isCommunicateMix);
		AddBuffAnnouncer.Announce(LocalAgent.GetEntityByID(target.ID), buffData.id);
		LocalBattleProtocolSimulator.SendAddBuff(caster.ID, target.ID, buffData.id, (int)num);
	}

	protected void HandleBuff(Buff buffData, EntityParent caster, EntityParent target, int fromSkillID, int fromSkillLevel, XDict<GameData.AttrType, BattleSkillAttrAdd> fromSkillAttrChange, bool isCommunicateMix = false)
	{
		if (buffData.tempSkill > 0)
		{
			this.AddSkill(target.ID, 0, buffData.tempSkill, 1);
		}
		switch (buffData.type)
		{
		case 1:
			if (buffData.immediateEffect == 1)
			{
				LocalBattleBuffCalculatorDamageHandler.HandleBuff(buffData, caster, target, fromSkillID, fromSkillLevel, fromSkillAttrChange, isCommunicateMix);
			}
			break;
		case 2:
			if (buffData.immediateEffect == 1)
			{
				LocalBattleBuffCalculatorTreatHandler.HandleBuff(buffData, caster, target, fromSkillID, fromSkillLevel, fromSkillAttrChange, isCommunicateMix);
			}
			break;
		case 3:
			LocalBattleBuffTauntHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 4:
			LocalBattleBuffFixHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 5:
			LocalBattleBuffStaticHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 6:
			LocalBattleBuffShiftHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 7:
			LocalBattleBuffChangeAttrsHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 8:
			LocalBattleBuffSuperArmorHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 9:
			if (buffData.immediateEffect == 1)
			{
				LocalBattleBuffDrainHandler.HandleBuff(buffData, caster, target, fromSkillID, fromSkillLevel, fromSkillAttrChange, isCommunicateMix);
			}
			break;
		case 10:
			LocalBattleBuffIgnoreFormulaHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 11:
			LocalBattleBuffCloseRendererHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 12:
			LocalBattleBuffMoveCastHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 13:
			LocalBattleBuffFilterHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 14:
			LocalBattleBuffDizzyHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 15:
			LocalBattleBuffUnconspicuousHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 16:
			LocalBattleBuffAuraHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 17:
			LocalBattleBuffWeakHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 18:
			LocalBattleBuffIncurableHandler.HandleBuff(buffData, caster, target, fromSkillLevel);
			break;
		case 19:
			if (buffData.immediateEffect == 1)
			{
				LocalBattleBuffCalculatorTreat2Handler.HandleBuff(buffData, caster, target, fromSkillID, fromSkillLevel, fromSkillAttrChange, isCommunicateMix);
			}
			break;
		}
	}

	protected void IntervalHandleBuff(int buffID, long targetID)
	{
		EntityParent entityByID = LocalAgent.GetEntityByID(targetID);
		if (entityByID == null)
		{
			return;
		}
		XDict<int, BuffState> buffListByEntityID = this.GetBuffListByEntityID(targetID);
		if (buffListByEntityID == null)
		{
			return;
		}
		if (buffListByEntityID.Count == 0)
		{
			return;
		}
		if (!buffListByEntityID.ContainsKey(buffID))
		{
			return;
		}
		EntityParent entityByID2 = LocalAgent.GetEntityByID(this.buffStateTable[targetID][buffID].casterID);
		if (entityByID2 == null)
		{
			return;
		}
		Buff buffDataByID = LocalAgent.GetBuffDataByID(buffID);
		BuffType type = (BuffType)buffDataByID.type;
		switch (type)
		{
		case BuffType.Aura:
			entityByID.GetSkillManager().ClientCastSkillByID(buffDataByID.tempSkill);
			return;
		case BuffType.Weak:
		case BuffType.Incurable:
			IL_83:
			if (type == BuffType.Damage)
			{
				LocalBattleBuffCalculatorDamageHandler.IntervalBuff(buffDataByID, entityByID2, entityByID, this.buffStateTable[targetID][buffID].fromSkillID, this.buffStateTable[targetID][buffID].fromSkillLevel, this.buffStateTable[targetID][buffID].fromSkillAttrChange, this.buffStateTable[targetID][buffID].isCommunicateMix);
				return;
			}
			if (type == BuffType.Theat)
			{
				LocalBattleBuffCalculatorTreatHandler.IntervalBuff(buffDataByID, entityByID2, entityByID, this.buffStateTable[targetID][buffID].fromSkillID, this.buffStateTable[targetID][buffID].fromSkillLevel, this.buffStateTable[targetID][buffID].fromSkillAttrChange, this.buffStateTable[targetID][buffID].isCommunicateMix);
				return;
			}
			if (type != BuffType.Drain)
			{
				return;
			}
			LocalBattleBuffDrainHandler.IntervalBuff(buffDataByID, entityByID2, entityByID, this.buffStateTable[targetID][buffID].fromSkillID, this.buffStateTable[targetID][buffID].fromSkillLevel, this.buffStateTable[targetID][buffID].fromSkillAttrChange, this.buffStateTable[targetID][buffID].isCommunicateMix);
			return;
		case BuffType.Theat2:
			LocalBattleBuffCalculatorTreat2Handler.IntervalBuff(buffDataByID, entityByID2, entityByID, this.buffStateTable[targetID][buffID].fromSkillID, this.buffStateTable[targetID][buffID].fromSkillLevel, this.buffStateTable[targetID][buffID].fromSkillAttrChange, this.buffStateTable[targetID][buffID].isCommunicateMix);
			return;
		}
		goto IL_83;
	}

	public void AppClearBuff(long targetID)
	{
		List<int> list = new List<int>();
		if (this.buffStateTable.ContainsKey(targetID))
		{
			for (int i = 0; i < this.buffStateTable[targetID].Count; i++)
			{
				list.Add(this.buffStateTable[targetID].ElementKeyAt(i));
			}
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			this.AppRemoveBuff(list.get_Item(j), targetID);
		}
	}

	protected void AppRemoveBuff(int buffID, long targetID)
	{
		if (LocalAgent.CheckBuffByTargetIDAndBuffID(targetID, buffID))
		{
			this.OnLeaveBuff(buffID, targetID);
		}
	}

	protected void OnLeaveBuff(int buffID, long targetID)
	{
		XDict<int, BuffState> buffListByEntityID = this.GetBuffListByEntityID(targetID);
		if (!buffListByEntityID.ContainsKey(buffID))
		{
			return;
		}
		this.KillBuff(buffID, buffListByEntityID[buffID].casterID, targetID, buffListByEntityID[buffID].fromSkillLevel, buffListByEntityID[buffID].isCommunicateMix);
		this.buffStateTable[targetID].Remove(buffID);
		LocalBattleProtocolSimulator.SendRemoveBuff(targetID, buffID);
	}

	protected void KillBuff(int buffID, long casterID, long targetID, int fromSkillLevel, bool isCommunicateMix)
	{
		Buff buffDataByID = LocalAgent.GetBuffDataByID(buffID);
		EntityParent entityByID = LocalAgent.GetEntityByID(casterID);
		EntityParent entityByID2 = LocalAgent.GetEntityByID(targetID);
		switch (buffDataByID.type)
		{
		case 1:
			LocalBattleBuffCalculatorDamageHandler.KillBuff(buffID, casterID, targetID, fromSkillLevel, isCommunicateMix);
			break;
		case 2:
			LocalBattleBuffCalculatorTreatHandler.KillBuff(buffID, casterID, targetID, fromSkillLevel, isCommunicateMix);
			break;
		case 3:
			LocalBattleBuffTauntHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 4:
			LocalBattleBuffFixHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 5:
			LocalBattleBuffStaticHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 6:
			LocalBattleBuffShiftHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 7:
			LocalBattleBuffChangeAttrsHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 8:
			LocalBattleBuffSuperArmorHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 9:
			LocalBattleBuffDrainHandler.KillBuff(buffID, casterID, targetID, fromSkillLevel, isCommunicateMix);
			break;
		case 10:
			LocalBattleBuffIgnoreFormulaHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 11:
			LocalBattleBuffCloseRendererHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 12:
			LocalBattleBuffMoveCastHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 13:
			LocalBattleBuffFilterHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 14:
			LocalBattleBuffDizzyHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 15:
			LocalBattleBuffUnconspicuousHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 16:
			LocalBattleBuffAuraHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 17:
			LocalBattleBuffWeakHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 18:
			LocalBattleBuffIncurableHandler.KillBuff(buffDataByID, entityByID, entityByID2, fromSkillLevel, isCommunicateMix);
			break;
		case 19:
			LocalBattleBuffCalculatorTreat2Handler.KillBuff(buffID, casterID, targetID, fromSkillLevel, isCommunicateMix);
			break;
		}
		if (buffDataByID.tempSkill > 0)
		{
			LocalBattleProtocolSimulator.SendRemoveSkill(targetID, buffDataByID.tempSkill);
		}
	}

	protected void UpdateBuff(float deltaTime)
	{
		object obj = this.thisLock;
		lock (obj)
		{
			if (this.buffStateTable.Count != 0)
			{
				this.removeBuffList.Clear();
				this.intervalBuffList.Clear();
				for (int i = 0; i < this.buffStateTable.Count; i++)
				{
					long key = this.buffStateTable.ElementKeyAt(i);
					XDict<int, BuffState> xDict = this.buffStateTable.ElementValueAt(i);
					for (int j = 0; j < xDict.Count; j++)
					{
						int num = xDict.ElementKeyAt(j);
						BuffState buffState = xDict.ElementValueAt(j);
						if (!buffState.isBlock)
						{
							buffState.removeLeftTime -= (double)(deltaTime * 1000f);
							if (buffState.removeLeftTime <= 0.0 && !buffState.isGlobalBuff)
							{
								if (!this.removeBuffList.ContainsKey(key))
								{
									this.removeBuffList.Add(key, new List<int>());
								}
								this.removeBuffList[key].Add(num);
							}
							else if (buffState.intervalDefaultTime > 0f)
							{
								buffState.intervalLeftTime -= deltaTime * 1000f;
								if (buffState.intervalLeftTime <= 0f)
								{
									buffState.intervalLeftTime = buffState.intervalDefaultTime;
									if (!this.intervalBuffList.ContainsKey(key))
									{
										this.intervalBuffList.Add(key, new List<int>());
									}
									this.intervalBuffList[key].Add(num);
								}
							}
						}
					}
				}
				for (int k = 0; k < this.removeBuffList.Count; k++)
				{
					long targetID = this.removeBuffList.ElementKeyAt(k);
					List<int> list = this.removeBuffList.ElementValueAt(k);
					for (int l = 0; l < list.get_Count(); l++)
					{
						this.AppRemoveBuff(list.get_Item(l), targetID);
					}
				}
				for (int m = 0; m < this.intervalBuffList.Count; m++)
				{
					long targetID2 = this.intervalBuffList.ElementKeyAt(m);
					List<int> list2 = this.intervalBuffList.ElementValueAt(m);
					for (int n = 0; n < list2.get_Count(); n++)
					{
						this.IntervalHandleBuff(list2.get_Item(n), targetID2);
					}
				}
			}
		}
	}

	public XDict<int, BuffState> GetBuffListByEntityID(long id)
	{
		if (!this.buffStateTable.ContainsKey(id))
		{
			this.buffStateTable.Add(id, new XDict<int, BuffState>());
		}
		return this.buffStateTable[id];
	}

	public bool CheckBuffByTargetIDAndBuffID(long targetID, int buffID)
	{
		return this.buffStateTable.ContainsKey(targetID) && this.buffStateTable[targetID].ContainsKey(buffID);
	}

	public bool CheckBuffTypeContainOther(Buff buffData, long targetID)
	{
		if (!this.buffStateTable.ContainsKey(targetID))
		{
			return false;
		}
		for (int i = 0; i < this.buffStateTable[targetID].Count; i++)
		{
			if (this.buffStateTable[targetID].ElementKeyAt(i) != buffData.id)
			{
				Buff buffDataByID = LocalAgent.GetBuffDataByID(this.buffStateTable[targetID].ElementKeyAt(i));
				if (buffDataByID.type == buffData.type)
				{
					return true;
				}
			}
		}
		return false;
	}

	public void SetGlobalBuff(List<int> buffList)
	{
		if (buffList == null)
		{
			return;
		}
		if (buffList.get_Count() == 0)
		{
			return;
		}
		this.globalBuff.Clear();
		this.globalBuff.AddRange(buffList);
	}

	public List<ClientDrvBuffInfo> MakeClientDrvBuffInfo(long id)
	{
		List<ClientDrvBuffInfo> list = new List<ClientDrvBuffInfo>();
		XDict<int, BuffState> buffListByEntityID = this.GetBuffListByEntityID(id);
		for (int i = 0; i < buffListByEntityID.Count; i++)
		{
			list.Add(new ClientDrvBuffInfo
			{
				casterId = buffListByEntityID.ElementValueAt(i).casterID,
				buffId = buffListByEntityID.ElementKeyAt(i),
				skillId = buffListByEntityID.ElementValueAt(i).fromSkillID
			});
		}
		return list;
	}

	public void Init()
	{
		if (this.HasInit)
		{
			return;
		}
		this.HasInit = true;
		this.ResetData();
		this.AddListener();
	}

	public void Release()
	{
		this.RemoveListener();
		this.ResetData();
		this.HasInit = false;
	}

	protected void AddListener()
	{
	}

	protected void RemoveListener()
	{
	}

	public void ResetData()
	{
		this.IsEnable = false;
		this.IsEnableCalculate = true;
		this.ResetGlobalBuff();
		this.ResetBattleTable();
		this.ResetEffectTable();
		this.ResetBuffTable();
		this.ResetFuseTable();
	}

	protected void ResetGlobalBuff()
	{
		this.globalBuff.Clear();
	}

	protected void ResetBattleTable()
	{
		this.skillStateTable.Clear();
	}

	protected void ResetEffectTable()
	{
	}

	protected void ResetBuffTable()
	{
		this.removeBuffList.Clear();
		for (int i = 0; i < this.buffStateTable.Count; i++)
		{
			long key = this.buffStateTable.ElementKeyAt(i);
			XDict<int, BuffState> xDict = this.buffStateTable.ElementValueAt(i);
			for (int j = 0; j < xDict.Count; j++)
			{
				if (!this.removeBuffList.ContainsKey(key))
				{
					this.removeBuffList.Add(key, new List<int>());
				}
				this.removeBuffList[key].Add(xDict.ElementKeyAt(j));
			}
		}
		for (int k = 0; k < this.removeBuffList.Count; k++)
		{
			long targetID = this.removeBuffList.ElementKeyAt(k);
			List<int> list = this.removeBuffList.ElementValueAt(k);
			for (int l = 0; l < list.get_Count(); l++)
			{
				this.AppRemoveBuff(list.get_Item(l), targetID);
			}
		}
		this.removeBuffList.Clear();
	}

	protected void ResetFuseTable()
	{
		List<long> list = new List<long>();
		for (int i = 0; i < this.fuseStateTable.Count; i++)
		{
			TimerHeap.DelTimer(this.fuseStateTable.ElementValueAt(i).timerID);
			list.Add(this.fuseStateTable.ElementKeyAt(i));
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			this.AppDeFuse(list.get_Item(j));
		}
		this.fuseStateTable.Clear();
	}

	public void SetData(List<int> buffList = null)
	{
		this.ResetData();
		this.IsEnable = true;
		this.IsEnableCalculate = true;
		if (buffList != null)
		{
			this.SetGlobalBuff(buffList);
		}
	}

	public void Update(float deltaTime)
	{
		if (InstanceManager.IsServerBattle)
		{
			return;
		}
		this.totalDeltaTime += deltaTime;
		if (this.totalDeltaTime < 0.1f)
		{
			return;
		}
		this.totalDeltaTime -= 0.1f;
		this.UpdateBuff(0.1f);
		LocalBattleRecoverVpHandler.UpdateVp(0.1f);
	}

	public void SetLogicPause()
	{
		this.IsEnableCalculate = false;
	}

	public void SetLogicResume()
	{
		this.IsEnableCalculate = true;
	}

	public void HandleAddEffect(long casterID, int skillID, int effectID, List<EffectTargetInfo> effectTargetInfos, XPoint basePoint, int effectUniqueID, bool isCommunicateMix = false)
	{
		this.HandleTriggerEffect(casterID, skillID, effectID, effectTargetInfos, basePoint, effectUniqueID, true, isCommunicateMix);
	}

	public void HandleUpdateEffect(long casterID, int skillID, int effectID, List<EffectTargetInfo> effectTargetInfos, XPoint basePoint, int effectUniqueID, bool isCommunicateMix = false)
	{
		this.HandleTriggerEffect(casterID, skillID, effectID, effectTargetInfos, basePoint, effectUniqueID, false, isCommunicateMix);
	}

	protected void HandleTriggerEffect(long casterID, int skillID, int effectID, List<EffectTargetInfo> effectTargetInfos, XPoint basePoint, int effectUniqueID, bool isAddEffect = true, bool isCommunicateMix = false)
	{
		Effect effectDataByID = LocalAgent.GetEffectDataByID(effectID);
		EntityParent entityByID = LocalAgent.GetEntityByID(casterID);
		List<long> list = new List<long>();
		for (int i = 0; i < effectTargetInfos.get_Count(); i++)
		{
			list.Add(effectTargetInfos.get_Item(i).targetId);
		}
		if (isAddEffect)
		{
			if (list.get_Count() > 0 && LocalAgent.GetEntityUsable(entityByID, isCommunicateMix))
			{
				int num = BattleCalculator.CalculateEffectCasterActPoint(entityByID.BattleBaseAttrs, (double)effectDataByID.casterPoint);
				entityByID.SetValue(GameData.AttrType.ActPoint, entityByID.TryAddValue(GameData.AttrType.ActPoint, (long)num), true);
			}
			if (isCommunicateMix && effectDataByID.type1 != 0 && effectDataByID.type1 != 1 && effectDataByID.type1 != 2)
			{
				GlobalBattleNetwork.Instance.SendClientDriveBattleEffectDamage(casterID, 0L, (!LocalAgent.GetEntityUsable(entityByID, isCommunicateMix)) ? 0L : entityByID.Hp, 0L, 0L, skillID, effectID, true, false, null, null, basePoint, new List<long>(), string.Empty, true);
			}
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			EntityParent entityByID2 = LocalAgent.GetEntityByID(list.get_Item(j));
			if (LocalAgent.GetEntityUsable(entityByID2, isCommunicateMix))
			{
				entityByID2.SetValue(GameData.AttrType.ActPoint, entityByID2.TryAddValue(GameData.AttrType.ActPoint, (long)effectDataByID.targetPoint), true);
			}
		}
		switch (effectDataByID.type1)
		{
		case 1:
			LocalBattleEffectCalculatorDamageHandler.AppDamage(effectDataByID, entityByID, list, basePoint, skillID, isAddEffect, isCommunicateMix);
			break;
		case 2:
			LocalBattleEffectCalculatorTreatHandler.AppTheat(effectDataByID, entityByID, list, basePoint, skillID, isAddEffect, isCommunicateMix);
			break;
		case 3:
			LocalBattleEffectSummonMonsterHandler.AppSummonMonster(effectDataByID, entityByID, basePoint, isCommunicateMix);
			break;
		case 4:
			LocalBattleEffectReliveHandler.AppRelive(list, isCommunicateMix);
			break;
		case 5:
			LocalBattleEffectDrainHandler.AppDrain(effectDataByID, entityByID, list, isCommunicateMix);
			break;
		case 7:
			this.AppFuse(entityByID, skillID, isCommunicateMix);
			break;
		case 8:
			LocalBattleEffectSummonPetHandler.AppSummonPet(effectDataByID, entityByID, skillID, isCommunicateMix);
			break;
		case 9:
			LocalBattleEffectPetManualSkillHandler.AppManualSkill(entityByID, list, skillID, isCommunicateMix);
			break;
		case 10:
			LocalBattleEffectBlinkHandler.AppBlink(effectDataByID, entityByID, basePoint, isCommunicateMix);
			break;
		case 11:
			LocalBattleEffectHitHandler.AppHit(effectDataByID, entityByID, list, basePoint, isAddEffect);
			break;
		}
		for (int k = 0; k < list.get_Count(); k++)
		{
			for (int l = 0; l < effectDataByID.addLoopBuff.get_Count(); l++)
			{
				this.AppAddBuff(effectDataByID.addLoopBuff.get_Item(l), entityByID, list.get_Item(k), skillID, effectDataByID.element, isCommunicateMix);
			}
			if (isAddEffect)
			{
				for (int m = 0; m < effectDataByID.addBuff.get_Count(); m++)
				{
					this.AppAddBuff(effectDataByID.addBuff.get_Item(m), entityByID, list.get_Item(k), skillID, effectDataByID.element, isCommunicateMix);
				}
				for (int n = 0; n < effectDataByID.removeBuff.get_Count(); n++)
				{
					this.AppRemoveBuff(effectDataByID.removeBuff.get_Item(n), list.get_Item(k));
				}
			}
			if (entityByID != null)
			{
				int skillLevelByID = entityByID.GetSkillLevelByID(skillID);
				for (int num2 = 0; num2 < effectDataByID.gradeAddLoopBuffId.get_Count(); num2++)
				{
					if (effectDataByID.gradeAddLoopBuffId.get_Item(num2).key == skillLevelByID)
					{
						this.AppAddBuff(effectDataByID.gradeAddLoopBuffId.get_Item(num2).value, entityByID, list.get_Item(k), skillID, effectDataByID.element, isCommunicateMix);
					}
				}
				if (isAddEffect)
				{
					for (int num3 = 0; num3 < effectDataByID.gradeAddBuffId.get_Count(); num3++)
					{
						if (effectDataByID.gradeAddBuffId.get_Item(num3).key == skillLevelByID)
						{
							this.AppAddBuff(effectDataByID.gradeAddBuffId.get_Item(num3).value, entityByID, list.get_Item(k), skillID, effectDataByID.element, isCommunicateMix);
						}
					}
				}
			}
		}
	}

	public void AppFuse(EntityParent caster, int skillID, bool isCommunicateMix)
	{
		if (caster == null)
		{
			return;
		}
		if (caster.BattleBaseAttrs == null)
		{
			return;
		}
		XDict<int, LocalDimensionPetSpirit> petSpiritByOwnerID = LocalAgent.GetPetSpiritByOwnerID(caster.ID);
		if (petSpiritByOwnerID == null)
		{
			return;
		}
		if (petSpiritByOwnerID.Count == 0)
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < petSpiritByOwnerID.Count; i++)
		{
			if (petSpiritByOwnerID.ElementValueAt(i).fuseRitualSkillInfo != null)
			{
				if (petSpiritByOwnerID.ElementValueAt(i).fuseRitualSkillInfo.skillId == skillID)
				{
					num = petSpiritByOwnerID.ElementKeyAt(i);
					LocalAgent.RemovePetSummonRitualSkill(caster.ID, petSpiritByOwnerID.ElementValueAt(i));
					break;
				}
			}
		}
		List<EntityParent> values = EntityWorld.Instance.GetEntities<EntityPet>().Values;
		EntityPet entityPet = null;
		for (int j = 0; j < values.get_Count(); j++)
		{
			if (values.get_Item(j).IsFighting && values.get_Item(j).OwnerListIdx == num && values.get_Item(j).OwnerID == caster.ID)
			{
				entityPet = (values.get_Item(j) as EntityPet);
				break;
			}
		}
		if (entityPet == null)
		{
			return;
		}
		FuseState fuseState = new FuseState();
		fuseState.modelID = caster.ModelID;
		fuseState.skill.Clear();
		using (Dictionary<int, int>.Enumerator enumerator = caster.GetSkillPairPart().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.get_Current();
				fuseState.skill.Add(current.get_Key(), current.get_Value());
			}
		}
		using (List<int>.Enumerator enumerator2 = caster.GetSkillSinglePart().GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				int current2 = enumerator2.get_Current();
				fuseState.skill.AddValue(current2);
			}
		}
		fuseState.petID = entityPet.ID;
		LocalAgent.AppClearBuff(caster.ID);
		LocalAgent.AppClearBuff(entityPet.ID);
		this.SetFuseAttrs(caster, entityPet, fuseState);
		Pet pet = DataReader<Pet>.Get(entityPet.TypeID);
		for (int k = 0; k < pet.fuseSkill.get_Count(); k++)
		{
			if (pet.fuseSkill.get_Item(k).key == caster.TypeID)
			{
				List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
				list.Add(new KeyValuePair<int, int>(1, pet.fuseNormalSkill));
				list.Add(new KeyValuePair<int, int>(11, pet.fuseSkill.get_Item(k).value));
				LocalBattleProtocolSimulator.SendFit(caster.ID, entityPet.ID, pet.fuseModle, list, 1000 * (int)(entityPet.FuseTime + (caster as EntitySelf).TotalFuseTimePlus));
				caster.SetValue(GameData.AttrType.MoveSpeed, DataReader<AvatarModel>.Get(pet.fuseModle).speed, true);
				break;
			}
		}
		fuseState.timerID = TimerHeap.AddTimer((uint)(1000f * (entityPet.FuseTime + (caster as EntitySelf).TotalFuseTimePlus)), 0, delegate
		{
			this.AppDeFuse(caster.ID);
		});
		this.fuseStateTable.Add(caster.ID, fuseState);
	}

	protected void AppDeFuse(long casterID)
	{
		LocalAgent.AppClearBuff(casterID);
		this.AppClearFuse(casterID, false);
	}

	public void AppClearFuse(long targetID, bool isDeadDefuse)
	{
		EntityParent entityByID = LocalAgent.GetEntityByID(targetID);
		if (entityByID == null)
		{
			return;
		}
		if (entityByID.BattleBaseAttrs == null)
		{
			return;
		}
		if (!this.fuseStateTable.ContainsKey(targetID))
		{
			return;
		}
		FuseState fuseState = this.fuseStateTable[targetID];
		TimerHeap.DelTimer(fuseState.timerID);
		if (!isDeadDefuse)
		{
			LocalBattleProtocolSimulator.SendExitFit(targetID, fuseState.petID, fuseState.modelID, fuseState.skill);
		}
		this.ResetFuseAttrs(targetID, 0L, fuseState);
		entityByID.SetValue(GameData.AttrType.MoveSpeed, DataReader<AvatarModel>.Get(fuseState.modelID).speed, true);
		this.fuseStateTable.Remove(targetID);
	}

	protected void SetFuseAttrs(EntityParent caster, EntityParent target, FuseState state)
	{
	}

	protected void ResetFuseAttrs(long casterID, long curHp, FuseState state)
	{
	}

	public void HandleUseSkill(long casterID, long targetID, int skillID, bool isCommunicateMix = false)
	{
		Skill skillDataByID = LocalAgent.GetSkillDataByID(skillID);
		if (skillDataByID == null)
		{
			return;
		}
		EntityParent entityByID = LocalAgent.GetEntityByID(casterID);
		if (entityByID == null)
		{
			return;
		}
		entityByID.SetValue(GameData.AttrType.ActPoint, entityByID.TryAddValue(GameData.AttrType.ActPoint, (long)(skillDataByID.actionPoint + entityByID.GetSkillActionPointVariationByType(skillDataByID.skilltype))), true);
		if (isCommunicateMix)
		{
			GlobalBattleNetwork.Instance.SendClientDriveBattleSkill(casterID, targetID, skillID);
		}
		if (!this.skillStateTable.ContainsKey(casterID))
		{
			this.skillStateTable.Add(casterID, new XDict<int, long>());
		}
		if (!this.skillStateTable[casterID].ContainsKey(skillID))
		{
			this.skillStateTable[casterID].Add(skillID, targetID);
		}
		else
		{
			this.skillStateTable[casterID][skillID] = targetID;
		}
		EntityParent entityByID2 = LocalAgent.GetEntityByID(targetID);
		UseSkillAnnouncer.Announce(entityByID, entityByID2, skillID);
		BacameSkillTargetAnnouncer.Announce(entityByID2, entityByID, skillID);
	}

	public void HandleCancelUseSkill(long casterID, int skillID)
	{
		if (this.skillStateTable.ContainsKey(casterID) && this.skillStateTable[casterID].ContainsKey(skillID))
		{
			this.skillStateTable[casterID].Remove(skillID);
		}
	}

	public void AddSkill(long targetID, int skillIndex, int skillID, int skillLevel = 1)
	{
		LocalBattleProtocolSimulator.SendAddSkill(targetID, skillIndex, skillID, skillLevel);
	}

	public void RemoveSkill(long targetID, int skillID)
	{
		if (LocalAgent.GetEntityByID(targetID).IsFuse && this.fuseStateTable.ContainsKey(targetID))
		{
			this.fuseStateTable[targetID].skill.Remove(skillID);
		}
		LocalBattleProtocolSimulator.SendRemoveSkill(targetID, skillID);
	}
}
