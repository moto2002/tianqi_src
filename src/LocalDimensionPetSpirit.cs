using Package;
using System;
using System.Collections.Generic;

public class LocalDimensionPetSpirit : LocalDimensionSpirit
{
	protected bool isSummoned;

	public MapObjInfo initialData;

	protected bool isSummonMonopolize;

	public BattleSkillInfo summonSkillInfo;

	public float existTime;

	public uint sleepTimer;

	public float summonPoint;

	public float resummonPoint;

	public int manualSkillBound;

	public BattleSkillInfo manualSkillInfo;

	public int fuseRitualSkillBound;

	public BattleSkillInfo fuseRitualSkillInfo;

	public List<int> currentSummonOwnerAttr = new List<int>();

	public List<int> currentCheckOwnerAttr = new List<int>();

	public List<int> currentCheckSelfAttr = new List<int>();

	public List<int> talentIDs = new List<int>();

	public List<int> summonOwnerAttrChangeData = new List<int>();

	public XDict<int, int> checkOwnerAttrChangeData = new XDict<int, int>();

	public XDict<int, int> checkSelfAttrChangeData = new XDict<int, int>();

	public bool IsSummoned
	{
		get
		{
			return this.isSummoned;
		}
		set
		{
			this.isSummoned = value;
		}
	}

	public bool IsSummonMonopolize
	{
		get
		{
			return this.isSummonMonopolize;
		}
		set
		{
			this.isSummonMonopolize = value;
		}
	}
}
