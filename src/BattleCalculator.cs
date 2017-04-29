using GameData;
using Package;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class BattleCalculator
{
	public class DamageResult
	{
		protected LongCoder longCoder = new LongCoder();

		protected BooleanCoder booleanCoder = new BooleanCoder();

		protected bool isMiss;

		protected bool isParry;

		protected bool isCrit;

		protected long damage;

		protected long lifesteal;

		public int parryRandomIndex;

		public int critRandomIndex;

		public int damageRandomIndex;

		public bool IsMiss
		{
			get
			{
				return this.booleanCoder.Decode(this.isMiss);
			}
			set
			{
				this.isMiss = this.booleanCoder.Encode(value);
			}
		}

		public bool IsParry
		{
			get
			{
				return this.booleanCoder.Decode(this.isParry);
			}
			set
			{
				this.isParry = this.booleanCoder.Encode(value);
			}
		}

		public bool IsCrit
		{
			get
			{
				return this.booleanCoder.Decode(this.isCrit);
			}
			set
			{
				this.isCrit = this.booleanCoder.Encode(value);
			}
		}

		public long Damage
		{
			get
			{
				return this.longCoder.Decode(this.damage);
			}
			set
			{
				this.damage = this.longCoder.Encode(value);
			}
		}

		public long Lifesteal
		{
			get
			{
				return this.longCoder.Decode(this.lifesteal);
			}
			set
			{
				this.lifesteal = this.longCoder.Encode(value);
			}
		}
	}

	protected static Dictionary<string, long> battleConstParams = new Dictionary<string, long>();

	public static BattleCalculatorRandom serverRandom = new BattleCalculatorRandom();

	protected static Dictionary<string, List<object>> packetRecord = new Dictionary<string, List<object>>();

	protected static StringBuilder sb = new StringBuilder();

	protected static string key = string.Empty;

	public static void Init()
	{
		List<BattleConstParams> dataList = DataReader<BattleConstParams>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (!BattleCalculator.battleConstParams.ContainsKey(dataList.get_Item(i).key))
			{
				BattleCalculator.battleConstParams.Add(dataList.get_Item(i).key, (long)dataList.get_Item(i).value);
			}
		}
	}

	public static void Release()
	{
		BattleCalculator.battleConstParams.Clear();
	}

	public static void SetData(long randomNum, int count)
	{
		BattleCalculator.ResetPacketRecord();
		BattleCalculator.serverRandom.ResetSeed(randomNum, count);
	}

	protected static bool CheckHit(BattleBaseAttrs caster, BattleBaseAttrs target, StringBuilder sb = null)
	{
		int num = Random.Range(0, 1000);
		if (sb != null)
		{
			sb.Append("A: " + num + "\n");
		}
		return num < Math.Max(500, Math.Min(1000, caster.HitRatio - target.DodgeRatio));
	}

	protected static bool CheckParry(BattleBaseAttrs caster, BattleBaseAttrs target, out int randomIndex, StringBuilder sb = null)
	{
		int num = BattleCalculator.serverRandom.Next(0, 1000, out randomIndex);
		if (sb != null)
		{
			sb.Append("C: " + num + "\n");
		}
		return num < Math.Max(0, Math.Min(400, target.ParryRatio - caster.DeparryRatio));
	}

	protected static double GetFinalParryAddRatio(BattleBaseAttrs target, bool isParry)
	{
		return (!isParry) ? 1.0 : ((double)Math.Max(200L, 1000L - BattleCalculator.battleConstParams.get_Item("base_parry_sub_dmg") - (long)target.ParryHurtDeRatio) * 0.001);
	}

	protected static bool CheckCrit(BattleBaseAttrs caster, BattleBaseAttrs target, out int randomIndex, StringBuilder sb = null)
	{
		int num = BattleCalculator.serverRandom.Next(0, 1000, out randomIndex);
		if (sb != null)
		{
			sb.Append("B: " + num + "\n");
		}
		return num < Math.Max(0, Math.Min(400, caster.CritRatio - target.DecritRatio));
	}

	protected static double GetFinalCritAddRatio(BattleBaseAttrs caster, bool isCrit)
	{
		return (!isCrit) ? 1.0 : ((double)(BattleCalculator.battleConstParams.get_Item("base_crt_dmg_addi") + (long)caster.CritHurtAddRatio) * 0.001);
	}

	protected static int GetCritAddDamage(BattleBaseAttrs caster, bool isCrit)
	{
		return (!isCrit) ? 0 : caster.CritAddValue;
	}

	protected static double GetFinalAttack(BattleBaseAttrs caster, bool isTargetPlayer)
	{
		return (!isTargetPlayer) ? ((double)caster.Atk * (1.0 + (double)caster.AtkMulAmend * 0.001) + (double)caster.PveAtk * (1.0 + (double)caster.PveAtkMulAmend * 0.001)) : ((double)caster.Atk * (1.0 + (double)caster.AtkMulAmend * 0.001) + (double)caster.PvpAtk * (1.0 + (double)caster.PvpAtkMulAmend * 0.001));
	}

	protected static double GetFinalDefence(BattleBaseAttrs target)
	{
		return (double)target.Defence * (1.0 + (double)target.DefMulAmend * 0.001);
	}

	protected static double GetTempDamage(BattleBaseAttrs caster, BattleBaseAttrs target, bool isTargetPlayer)
	{
		double finalAttack = BattleCalculator.GetFinalAttack(caster, isTargetPlayer);
		double finalDefence = BattleCalculator.GetFinalDefence(target);
		return Math.Max(0.2 * finalAttack, Math.Min(finalAttack - finalDefence, 0.8 * finalAttack + Math.Max(0.0, 0.2 * finalAttack - finalDefence) / (double)Math.Max(1, caster.Lv - target.Lv)));
	}

	protected static double GetHolyDamage(BattleBaseAttrs caster, BattleBaseAttrs target, XDict<GameData.AttrType, long> casterTempAttrs)
	{
		return (double)caster.TryAddValue(GameData.AttrType.SkillHolyDmgScaleBOCurHp, casterTempAttrs) * 0.001 * (double)target.Hp + (double)caster.TryAddValue(GameData.AttrType.SkillHolyDmgScaleBOMaxHp, casterTempAttrs) * 0.001 * (double)target.RealHpLmt;
	}

	protected static double GetIgnoreTargetDamage(BattleBaseAttrs caster, bool isTargetPlayer, XDict<GameData.AttrType, long> casterTempAttrs)
	{
		if (isTargetPlayer)
		{
			return ((double)caster.Atk * (1.0 + (double)caster.AtkMulAmend * 0.001) + (double)caster.PvpAtk * (1.0 + (double)caster.PvpAtkMulAmend * 0.001)) * (double)caster.TryAddValue(GameData.AttrType.SkillIgnoreDefenceHurt, casterTempAttrs) * 0.001;
		}
		return ((double)caster.Atk * (1.0 + (double)caster.AtkMulAmend * 0.001) + (double)caster.PveAtk * (1.0 + (double)caster.PveAtkMulAmend * 0.001)) * (double)caster.TryAddValue(GameData.AttrType.SkillIgnoreDefenceHurt, casterTempAttrs) * 0.001;
	}

	public static BattleCalculator.DamageResult CalculateDamage(BattleBaseAttrs caster, BattleBaseAttrs target, bool isCasterPlayer, bool isTargetPlayer, XDict<GameData.AttrType, long> casterTempAttrs, XDict<GameData.AttrType, long> targetTempAttrs)
	{
		FileStream fileStream = null;
		StreamWriter streamWriter = null;
		StringBuilder stringBuilder = null;
		if (ClientApp.Instance.isShowFightLog)
		{
			string text = Application.get_dataPath() + "/../BattleLog.txt";
			try
			{
				fileStream = new FileStream(text, 4, 3);
				streamWriter = new StreamWriter(fileStream, Encoding.get_UTF8());
				stringBuilder = new StringBuilder();
			}
			catch (Exception ex)
			{
				Debug.LogError("Can't Write Log File!\n" + ex.get_Message());
				return null;
			}
			stringBuilder.Append("Caster: \n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.Atk, caster.Atk) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.Defence, caster.Defence) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.PveAtk, caster.PveAtk) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.PvpAtk, caster.PvpAtk) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.HitRatio, caster.HitRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.CritRatio, caster.CritRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.CritHurtAddRatio, caster.CritHurtAddRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.DeparryRatio, caster.DeparryRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.SuckBloodScale, caster.SuckBloodScale) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.HurtAddRatio, caster.HurtAddRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.PveHurtAddRatio, caster.PveHurtAddRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.PvpHurtAddRatio, caster.PvpHurtAddRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.AtkMulAmend, caster.AtkMulAmend) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.PveAtkMulAmend, caster.PveAtkMulAmend) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.PvpAtkMulAmend, caster.PvpAtkMulAmend) + "\n");
			stringBuilder.Append("Target: \n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.Defence, target.Defence) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.HpLmt, target.HpLmt) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.DodgeRatio, target.DodgeRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.DecritRatio, target.DecritRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.ParryRatio, target.ParryRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.ParryHurtDeRatio, target.ParryHurtDeRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.HurtDeRatio, target.ParryHurtDeRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.PveHurtDeRatio, target.PveHurtDeRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.PvpHurtDeRatio, target.PvpHurtDeRatio) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.DefMulAmend, target.DefMulAmend) + "\n");
			stringBuilder.Append(AttrUtility.GetStandardDesc(GameData.AttrType.HpLmtMulAmend, target.HpLmtMulAmend) + "\n");
			stringBuilder.Append("Calculate: \n");
		}
		BattleCalculator.DamageResult damageResult = new BattleCalculator.DamageResult();
		if (BattleCalculator.CheckHit(caster, target, stringBuilder))
		{
			if (ClientApp.Instance.isShowFightLog)
			{
				stringBuilder.Append("IsHit: " + true + "\n");
			}
			bool flag = BattleCalculator.CheckParry(caster, target, out damageResult.parryRandomIndex, stringBuilder);
			double finalParryAddRatio = BattleCalculator.GetFinalParryAddRatio(target, flag);
			if (ClientApp.Instance.isShowFightLog)
			{
				stringBuilder.Append("IsParry: " + flag + "\n");
			}
			bool flag2 = BattleCalculator.CheckCrit(caster, target, out damageResult.critRandomIndex, stringBuilder);
			double finalCritAddRatio = BattleCalculator.GetFinalCritAddRatio(caster, flag2);
			if (ClientApp.Instance.isShowFightLog)
			{
				stringBuilder.Append("IsCrit: " + flag2 + "\n");
			}
			double tempDamage = BattleCalculator.GetTempDamage(caster, target, isTargetPlayer);
			double holyDamage = BattleCalculator.GetHolyDamage(caster, target, casterTempAttrs);
			double ignoreTargetDamage = BattleCalculator.GetIgnoreTargetDamage(caster, isTargetPlayer, casterTempAttrs);
			double num = (double)BattleCalculator.GetCritAddDamage(caster, flag2);
			List<double> list = new List<double>();
			list.Add(tempDamage * (double)caster.TryAddValue(GameData.AttrType.SkillNmlDmgScale, casterTempAttrs) * 0.001 + (double)caster.TryAddValue(GameData.AttrType.SkillNmlDmgAddAmend, casterTempAttrs));
			list.Add(finalCritAddRatio);
			list.Add(finalParryAddRatio);
			list.Add((!isCasterPlayer || !isTargetPlayer) ? Math.Max(0.0, (1000.0 + (double)caster.PveHurtAddRatio - (double)target.PveHurtDeRatio) * 0.001) : Math.Max(0.0, (1000.0 + (double)caster.PvpHurtAddRatio - (double)target.PvpHurtDeRatio) * 0.001));
			list.Add(Math.Max(0.0, (double)(1000 + caster.HurtAddRatio - target.HurtDeRatio) * 0.001));
			double num2 = BattleCalculator.GetMultipleResult(list) + holyDamage + ignoreTargetDamage + num;
			double num3 = Math.Max(1.0, num2 * (double)BattleCalculator.serverRandom.Next(9500, 10500, out damageResult.damageRandomIndex) * 9.9999997473787516E-05);
			double num4 = BattleCalculator.CalculateLifeSteal(caster, (int)num3);
			damageResult.IsMiss = false;
			damageResult.IsCrit = flag2;
			damageResult.IsParry = flag;
			damageResult.Damage = (long)num3;
			damageResult.Lifesteal = (long)num4;
			if (ClientApp.Instance.isShowFightLog)
			{
				stringBuilder.Append("TempDamage: " + tempDamage + "\n");
				stringBuilder.Append("HolyDamage: " + holyDamage + "\n");
				stringBuilder.Append("SkillNmlDmgScale: " + caster.TryAddValue(GameData.AttrType.SkillNmlDmgScale, casterTempAttrs) + "\n");
				stringBuilder.Append("SkillNmlDmgAddAmend: " + caster.TryAddValue(GameData.AttrType.SkillNmlDmgAddAmend, casterTempAttrs) + "\n");
				stringBuilder.Append("FixFinalDamage: " + num3 + "\n");
				stringBuilder.Append("Lifesteal: " + num4 + "\n");
			}
		}
		else
		{
			damageResult.parryRandomIndex = 0;
			damageResult.critRandomIndex = 0;
			damageResult.damageRandomIndex = 0;
			damageResult.IsMiss = true;
			damageResult.IsParry = false;
			damageResult.IsCrit = false;
			damageResult.Damage = 0L;
			damageResult.Lifesteal = 0L;
			if (ClientApp.Instance.isShowFightLog)
			{
				stringBuilder.Append("IsHit: " + false + "\n");
			}
		}
		if (ClientApp.Instance.isShowFightLog)
		{
			stringBuilder.Append("================================================================\n\n");
			Debug.LogError(stringBuilder.ToString());
			streamWriter.Write(stringBuilder.ToString());
			streamWriter.Close();
			fileStream.Close();
		}
		return damageResult;
	}

	protected static double CalculateLifeSteal(BattleBaseAttrs caster, int finalDamage)
	{
		return (double)caster.SuckBloodScale * 0.001 * (double)finalDamage;
	}

	public static int CalculateKillRecover(BattleBaseAttrs source, BattleBaseAttrs target)
	{
		return 0;
	}

	public static int CalculateWeak(BattleBaseAttrs caster, XDict<GameData.AttrType, long> casterTempAttrs)
	{
		return (int)((double)caster.TryAddValue(GameData.AttrType.VpAtk, casterTempAttrs) * (1.0 + (double)caster.VpAtkMulAmend * 0.001));
	}

	public static long CalculateTreatment(BattleBaseAttrs caster, BattleBaseAttrs target, bool isTargetPlayer, XDict<GameData.AttrType, long> casterTempAttrs, XDict<GameData.AttrType, long> targetTempAttrs)
	{
		return (long)(BattleCalculator.GetFinalAttack(caster, isTargetPlayer) * (double)caster.TryAddValue(GameData.AttrType.SkillTreatScaleBOAtk, casterTempAttrs) * 0.001 * (1.0 + (double)target.HealIncreasePercent * 0.001) + (double)(target.RealHpLmt * caster.TryAddValue(GameData.AttrType.SkillTreatScaleBOHpLmt, casterTempAttrs)) * 0.001 * (1.0 + (double)target.HealIncreasePercent * 0.001));
	}

	public static long CalculateTreat2ment(BattleBaseAttrs target, XDict<GameData.AttrType, long> targetTempAttrs)
	{
		return target.TryAddValue(GameData.AttrType.HpRestore, targetTempAttrs);
	}

	public static int CalculateEffectCasterActPoint(BattleBaseAttrs caster, double effectCasterActPoint)
	{
		return (int)(effectCasterActPoint * (1.0 + (double)caster.ActPointRecoverSpeedAmend * 0.001));
	}

	public static bool CalculateAddBuff(BattleBaseAttrs caster, BattleBaseAttrs target, double baseAddProb, int elementType)
	{
		double num = Math.Min(Math.Max(0.0, baseAddProb + (double)caster.GetBuffCtrlAttrs(elementType).AddProbAddAmend), 1000.0);
		return (double)Random.Range(0, 1000) < num;
	}

	public static double CalculateBuffTime(BuffCtrlAttrs casterAttrs, BuffCtrlAttrs targetAttrs, double baseTimeProb)
	{
		return Math.Max(0.0, baseTimeProb + (double)casterAttrs.DurTimeAddAmend + (double)targetAttrs.DurTimeAddAmend);
	}

	protected static double GetMultipleResult(List<double> multipleFactor)
	{
		if (multipleFactor.get_Count() == 0)
		{
			return 0.0;
		}
		if (multipleFactor.get_Count() == 1)
		{
			return multipleFactor.get_Item(0);
		}
		double num = multipleFactor.get_Item(0);
		double num2 = multipleFactor.get_Item(1);
		for (int i = 2; i < multipleFactor.get_Count(); i++)
		{
			num2 *= multipleFactor.get_Item(i);
			num2 *= 100000.0;
			num2 = Math.Floor(num2);
			num2 *= 1E-05;
		}
		return num * num2;
	}

	protected static void ResetPacketRecord()
	{
		BattleCalculator.packetRecord.Clear();
	}

	public static void RecordPacket(ClientDrvBattleEffectDmgReportReq packet)
	{
		if (packet.vertifyInfo.randIndex.get_Count() < 3)
		{
			return;
		}
		BattleCalculator.sb.Clear();
		for (int i = 0; i < packet.vertifyInfo.randIndex.get_Count(); i++)
		{
			BattleCalculator.sb.Append(packet.vertifyInfo.randIndex.get_Item(i));
			if (i < packet.vertifyInfo.randIndex.get_Count() - 1)
			{
				BattleCalculator.sb.Append("_");
			}
		}
		BattleCalculator.key = BattleCalculator.sb.ToString();
		if (!BattleCalculator.packetRecord.ContainsKey(BattleCalculator.key))
		{
			BattleCalculator.packetRecord.Add(BattleCalculator.key, new List<object>());
		}
		BattleCalculator.packetRecord.get_Item(BattleCalculator.key).Add(packet);
	}

	public static void RecordPacket(ClientDrvBattleBuffDmgReportReq packet)
	{
		if (packet.vertifyInfo.randIndex.get_Count() < 3)
		{
			return;
		}
		BattleCalculator.sb.Clear();
		for (int i = 0; i < packet.vertifyInfo.randIndex.get_Count(); i++)
		{
			BattleCalculator.sb.Append(packet.vertifyInfo.randIndex.get_Item(i));
			if (i < packet.vertifyInfo.randIndex.get_Count() - 1)
			{
				BattleCalculator.sb.Append("_");
			}
		}
		BattleCalculator.key = BattleCalculator.sb.ToString();
		if (!BattleCalculator.packetRecord.ContainsKey(BattleCalculator.key))
		{
			BattleCalculator.packetRecord.Add(BattleCalculator.key, new List<object>());
		}
		BattleCalculator.packetRecord.get_Item(BattleCalculator.key).Add(packet);
	}

	public static object GetRecordPacket(int count, List<int> randIndex)
	{
		BattleCalculator.sb.Clear();
		for (int i = 0; i < randIndex.get_Count(); i++)
		{
			BattleCalculator.sb.Append(randIndex.get_Item(i));
			if (i < randIndex.get_Count() - 1)
			{
				BattleCalculator.sb.Append("_");
			}
		}
		BattleCalculator.key = BattleCalculator.sb.ToString();
		Debug.LogError("GetRecordPacket: " + BattleCalculator.key);
		if (!BattleCalculator.packetRecord.ContainsKey(BattleCalculator.key))
		{
			return null;
		}
		int num = (BattleCalculator.serverRandom.MaxIndex % 3 != 0) ? (count / BattleCalculator.serverRandom.MaxIndex / 3) : (count / BattleCalculator.serverRandom.MaxIndex);
		if (BattleCalculator.packetRecord.get_Item(BattleCalculator.key).get_Count() <= num)
		{
			Debug.LogError(string.Concat(new object[]
			{
				count,
				" ",
				BattleCalculator.serverRandom.MaxIndex,
				" ",
				num,
				" - ",
				BattleCalculator.packetRecord.get_Item(BattleCalculator.key).get_Count()
			}));
			return null;
		}
		return BattleCalculator.packetRecord.get_Item(BattleCalculator.key).get_Item(num);
	}
}
