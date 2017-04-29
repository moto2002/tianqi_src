using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AttrUtility
{
	protected const string Display1Language = "{0}%";

	protected const int Display2Level1LanguageID = 14;

	protected const int Display2Level2LanguageID = 15;

	protected const int Display2Level3LanguageID = 16;

	public static string GetAttrName(AttrType type)
	{
		int num = 0;
		switch (type)
		{
		case AttrType.PveAtk:
			num = 1304;
			goto IL_4E1;
		case AttrType.PvpAtk:
			num = 1305;
			goto IL_4E1;
		case AttrType.HitRatio:
			num = 1306;
			goto IL_4E1;
		case AttrType.DodgeRatio:
			num = 1307;
			goto IL_4E1;
		case AttrType.CritRatio:
			num = 1308;
			goto IL_4E1;
		case AttrType.DecritRatio:
			num = 1309;
			goto IL_4E1;
		case AttrType.CritHurtAddRatio:
			num = 1310;
			goto IL_4E1;
		case AttrType.ParryRatio:
			num = 1311;
			goto IL_4E1;
		case AttrType.DeparryRatio:
			num = 1312;
			goto IL_4E1;
		case AttrType.ParryHurtDeRatio:
			num = 1313;
			goto IL_4E1;
		case (AttrType)1314:
		case (AttrType)1321:
		case (AttrType)1322:
		case (AttrType)1327:
		case (AttrType)1328:
			IL_A8:
			switch (type)
			{
			case AttrType.SkillNmlDmgScale:
				num = 501;
				goto IL_4E1;
			case AttrType.SkillNmlDmgAddAmend:
				num = 502;
				goto IL_4E1;
			case (AttrType)503:
			case (AttrType)504:
			case (AttrType)505:
			case (AttrType)506:
			case (AttrType)507:
			case (AttrType)508:
			case (AttrType)509:
			case (AttrType)510:
				IL_F4:
				switch (type)
				{
				case AttrType.Lv:
					num = 1001;
					goto IL_4E1;
				case AttrType.Exp:
					num = 1004;
					goto IL_4E1;
				case AttrType.ExpLmt:
					num = 1005;
					goto IL_4E1;
				case AttrType.Energy:
					num = 1002;
					goto IL_4E1;
				case AttrType.EnergyLmt:
					num = 1003;
					goto IL_4E1;
				case AttrType.Hp:
					num = 1006;
					goto IL_4E1;
				case AttrType.Fighting:
					num = 1007;
					goto IL_4E1;
				case AttrType.Diamond:
					num = 1008;
					goto IL_4E1;
				case AttrType.Gold:
					num = 1009;
					goto IL_4E1;
				case AttrType.VipLv:
					num = 1010;
					goto IL_4E1;
				case AttrType.RechargeDiamond:
					num = 1011;
					goto IL_4E1;
				case AttrType.Honor:
					num = 1012;
					goto IL_4E1;
				case AttrType.CompetitiveCurrency:
					num = 1013;
					goto IL_4E1;
				case (AttrType)1014:
					IL_13C:
					switch (type)
					{
					case AttrType.MoveSpeed:
						num = 101;
						goto IL_4E1;
					case AttrType.ActSpeed:
						num = 102;
						goto IL_4E1;
					case AttrType.Affinity:
						num = 103;
						goto IL_4E1;
					case (AttrType)104:
					case (AttrType)105:
						IL_165:
						switch (type)
						{
						case AttrType.WaterBuffAddProbAddAmend:
							num = 1221;
							goto IL_4E1;
						case (AttrType)1222:
						case (AttrType)1223:
							IL_181:
							switch (type)
							{
							case AttrType.ThunderBuffAddProbAddAmend:
								num = 1231;
								goto IL_4E1;
							case (AttrType)1232:
							case (AttrType)1233:
								IL_19D:
								switch (type)
								{
								case AttrType.BuffMoveSpeedMulPosAmend:
									num = 707;
									goto IL_4E1;
								case (AttrType)708:
									IL_1B5:
									if (type == AttrType.Atk)
									{
										num = 201;
										goto IL_4E1;
									}
									if (type == AttrType.AtkMulAmend)
									{
										num = 202;
										goto IL_4E1;
									}
									if (type == AttrType.HpLmt)
									{
										num = 301;
										goto IL_4E1;
									}
									if (type != AttrType.Defence)
									{
										goto IL_4E1;
									}
									num = 601;
									goto IL_4E1;
								case AttrType.BuffActSpeedMulPosAmend:
									num = 709;
									goto IL_4E1;
								}
								goto IL_1B5;
							case AttrType.ThunderBuffDurTimeAddAmend:
								num = 1234;
								goto IL_4E1;
							}
							goto IL_19D;
						case AttrType.WaterBuffDurTimeAddAmend:
							num = 1224;
							goto IL_4E1;
						}
						goto IL_181;
					case AttrType.ActPoint:
						num = 106;
						goto IL_4E1;
					case AttrType.ActPointLmt:
						num = 107;
						goto IL_4E1;
					case AttrType.ActPointRecoverSpeedAmend:
						num = 108;
						goto IL_4E1;
					}
					goto IL_165;
				case AttrType.SkillPoint:
					num = 1015;
					goto IL_4E1;
				}
				goto IL_13C;
			case AttrType.SkillHolyDmgScaleBOMaxHp:
				num = 512;
				goto IL_4E1;
			case AttrType.SkillHolyDmgScaleBOCurHp:
				num = 511;
				goto IL_4E1;
			case AttrType.SuckBloodScale:
				num = 513;
				goto IL_4E1;
			case AttrType.SkillTreatScaleBOAtk:
				num = 514;
				goto IL_4E1;
			case AttrType.SkillTreatScaleBOHpLmt:
				num = 515;
				goto IL_4E1;
			case AttrType.SkillIgnoreDefenceHurt:
				num = 516;
				goto IL_4E1;
			}
			goto IL_F4;
		case AttrType.HurtAddRatio:
			num = 1315;
			goto IL_4E1;
		case AttrType.HurtDeRatio:
			num = 1316;
			goto IL_4E1;
		case AttrType.PveHurtAddRatio:
			num = 1317;
			goto IL_4E1;
		case AttrType.PveHurtDeRatio:
			num = 1318;
			goto IL_4E1;
		case AttrType.PvpHurtAddRatio:
			num = 1319;
			goto IL_4E1;
		case AttrType.PvpHurtDeRatio:
			num = 1320;
			goto IL_4E1;
		case AttrType.DefMulAmend:
			num = 1323;
			goto IL_4E1;
		case AttrType.HpLmtMulAmend:
			num = 1324;
			goto IL_4E1;
		case AttrType.PveAtkMulAmend:
			num = 1325;
			goto IL_4E1;
		case AttrType.PvpAtkMulAmend:
			num = 1326;
			goto IL_4E1;
		case AttrType.OnlineTime:
			num = 1329;
			goto IL_4E1;
		case AttrType.VpLmt:
			num = 1330;
			goto IL_4E1;
		case AttrType.VpLmtMulAmend:
			num = 1331;
			goto IL_4E1;
		case AttrType.VpResume:
			num = 1332;
			goto IL_4E1;
		case AttrType.VpAtk:
			num = 1333;
			goto IL_4E1;
		case AttrType.VpAtkMulAmend:
			num = 1334;
			goto IL_4E1;
		case AttrType.Vp:
			num = 1335;
			goto IL_4E1;
		case AttrType.IdleVpResume:
			num = 1336;
			goto IL_4E1;
		case AttrType.HealIncreasePercent:
			num = 1337;
			goto IL_4E1;
		case AttrType.ExpAddRate:
			num = 1338;
			goto IL_4E1;
		case AttrType.CritAddValue:
			num = 1339;
			goto IL_4E1;
		case AttrType.HpRestore:
			num = 1340;
			goto IL_4E1;
		case AttrType.Reputation:
			num = 1341;
			goto IL_4E1;
		}
		goto IL_A8;
		IL_4E1:
		ChineseData chineseData = DataReader<ChineseData>.Get(num);
		if (chineseData == null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Could not found lang config, langId: ",
				num,
				", AttrType: ",
				type
			}));
			return string.Empty + num;
		}
		return chineseData.content;
	}

	public static string GetAttrName(int typeNum)
	{
		return AttrUtility.GetAttrName((AttrType)typeNum);
	}

	protected static int GetAttrValueDisplayMode(AttrType type)
	{
		switch (type)
		{
		case AttrType.PveAtk:
			return 0;
		case AttrType.PvpAtk:
			return 0;
		case AttrType.HitRatio:
			return 1;
		case AttrType.DodgeRatio:
			return 1;
		case AttrType.CritRatio:
			return 1;
		case AttrType.DecritRatio:
			return 1;
		case AttrType.CritHurtAddRatio:
			return 1;
		case AttrType.ParryRatio:
			return 1;
		case AttrType.DeparryRatio:
			return 1;
		case AttrType.ParryHurtDeRatio:
			return 1;
		case (AttrType)1314:
		case (AttrType)1321:
		case (AttrType)1322:
		case (AttrType)1327:
		case (AttrType)1328:
			IL_A6:
			switch (type)
			{
			case AttrType.SkillNmlDmgScale:
				return 1;
			case AttrType.SkillNmlDmgAddAmend:
				return 0;
			case (AttrType)503:
			case (AttrType)504:
			case (AttrType)505:
			case (AttrType)506:
			case (AttrType)507:
			case (AttrType)508:
			case (AttrType)509:
			case (AttrType)510:
				IL_F2:
				switch (type)
				{
				case AttrType.Lv:
					return 0;
				case AttrType.Exp:
					return 2;
				case AttrType.ExpLmt:
					return 2;
				case AttrType.Energy:
					return 0;
				case AttrType.EnergyLmt:
					return 0;
				case AttrType.Hp:
					return 2;
				case AttrType.Fighting:
					return 2;
				case AttrType.Diamond:
					return 0;
				case AttrType.Gold:
					return 2;
				case AttrType.VipLv:
					return 0;
				case AttrType.RechargeDiamond:
					return 0;
				case AttrType.Honor:
					return 0;
				case AttrType.CompetitiveCurrency:
					return 0;
				case (AttrType)1014:
					IL_13A:
					switch (type)
					{
					case AttrType.MoveSpeed:
						return 0;
					case AttrType.ActSpeed:
						return 0;
					case AttrType.Affinity:
						return 0;
					case (AttrType)104:
					case (AttrType)105:
						IL_163:
						switch (type)
						{
						case AttrType.WaterBuffAddProbAddAmend:
							return 0;
						case (AttrType)1222:
						case (AttrType)1223:
							IL_17F:
							switch (type)
							{
							case AttrType.ThunderBuffAddProbAddAmend:
								return 0;
							case (AttrType)1232:
							case (AttrType)1233:
								IL_19B:
								switch (type)
								{
								case AttrType.BuffMoveSpeedMulPosAmend:
									return 0;
								case (AttrType)708:
									IL_1B3:
									if (type == AttrType.Atk)
									{
										return 0;
									}
									if (type == AttrType.AtkMulAmend)
									{
										return 1;
									}
									if (type == AttrType.HpLmt)
									{
										return 0;
									}
									if (type != AttrType.Defence)
									{
										return 0;
									}
									return 0;
								case AttrType.BuffActSpeedMulPosAmend:
									return 0;
								}
								goto IL_1B3;
							case AttrType.ThunderBuffDurTimeAddAmend:
								return 0;
							}
							goto IL_19B;
						case AttrType.WaterBuffDurTimeAddAmend:
							return 0;
						}
						goto IL_17F;
					case AttrType.ActPoint:
						return 0;
					case AttrType.ActPointLmt:
						return 0;
					case AttrType.ActPointRecoverSpeedAmend:
						return 1;
					}
					goto IL_163;
				case AttrType.SkillPoint:
					return 0;
				}
				goto IL_13A;
			case AttrType.SkillHolyDmgScaleBOMaxHp:
				return 1;
			case AttrType.SkillHolyDmgScaleBOCurHp:
				return 1;
			case AttrType.SuckBloodScale:
				return 1;
			case AttrType.SkillTreatScaleBOAtk:
				return 1;
			case AttrType.SkillTreatScaleBOHpLmt:
				return 1;
			case AttrType.SkillIgnoreDefenceHurt:
				return 1;
			}
			goto IL_F2;
		case AttrType.HurtAddRatio:
			return 1;
		case AttrType.HurtDeRatio:
			return 1;
		case AttrType.PveHurtAddRatio:
			return 1;
		case AttrType.PveHurtDeRatio:
			return 1;
		case AttrType.PvpHurtAddRatio:
			return 1;
		case AttrType.PvpHurtDeRatio:
			return 1;
		case AttrType.DefMulAmend:
			return 1;
		case AttrType.HpLmtMulAmend:
			return 1;
		case AttrType.PveAtkMulAmend:
			return 1;
		case AttrType.PvpAtkMulAmend:
			return 1;
		case AttrType.OnlineTime:
			return 0;
		case AttrType.VpLmt:
			return 0;
		case AttrType.VpLmtMulAmend:
			return 1;
		case AttrType.VpResume:
			return 0;
		case AttrType.VpAtk:
			return 0;
		case AttrType.VpAtkMulAmend:
			return 1;
		case AttrType.Vp:
			return 0;
		case AttrType.IdleVpResume:
			return 0;
		case AttrType.HealIncreasePercent:
			return 1;
		case AttrType.ExpAddRate:
			return 1;
		case AttrType.CritAddValue:
			return 0;
		case AttrType.HpRestore:
			return 0;
		case AttrType.Reputation:
			return 0;
		}
		goto IL_A6;
	}

	public static int GetAttrValueDisplayMode(int typeNum)
	{
		return AttrUtility.GetAttrValueDisplayMode((AttrType)typeNum);
	}

	public static string GetAttrValueDisplay(AttrType type, int value)
	{
		int attrValueDisplayMode = AttrUtility.GetAttrValueDisplayMode(type);
		if (attrValueDisplayMode != 1)
		{
			if (attrValueDisplayMode != 2)
			{
				return value.ToString();
			}
			int num = 0;
			if ((double)value >= 100000000.0)
			{
				double num2 = (double)value / 100000000.0;
				if (int.TryParse(num2.ToString(), ref num))
				{
					return string.Format(GameDataUtils.GetChineseContent(15, false), num.ToString());
				}
				return string.Format(GameDataUtils.GetChineseContent(15, false), Math.Round(num2, 2).ToString("F2"));
			}
			else
			{
				if ((double)value < 100000.0)
				{
					return value.ToString();
				}
				double num2 = (double)value / 10000.0;
				if (int.TryParse(num2.ToString(), ref num))
				{
					return string.Format(GameDataUtils.GetChineseContent(14, false), num.ToString());
				}
				return string.Format(GameDataUtils.GetChineseContent(14, false), Math.Round(num2, 1).ToString("F1"));
			}
		}
		else
		{
			int num3 = 0;
			if (int.TryParse(((double)value * 0.1).ToString(), ref num3))
			{
				return string.Format("{0}%", num3.ToString());
			}
			return string.Format("{0}%", Math.Round((double)value * 0.1, 1).ToString("F1"));
		}
	}

	public static string GetAttrValueDisplay(int typeNum, int value)
	{
		return AttrUtility.GetAttrValueDisplay((AttrType)typeNum, value);
	}

	public static string GetAttrValueDisplay(AttrType type, long value)
	{
		int attrValueDisplayMode = AttrUtility.GetAttrValueDisplayMode(type);
		if (attrValueDisplayMode != 1)
		{
			if (attrValueDisplayMode != 2)
			{
				return ((int)value).ToString();
			}
			long num = 0L;
			if ((double)value >= 1000000000000.0)
			{
				double num2 = (double)value / 1000000000000.0;
				if (long.TryParse(num2.ToString(), ref num))
				{
					return string.Format(GameDataUtils.GetChineseContent(16, false), num.ToString());
				}
				return string.Format(GameDataUtils.GetChineseContent(16, false), Math.Round(num2, 2).ToString("F2"));
			}
			else if ((double)value >= 100000000.0)
			{
				double num2 = (double)value / 100000000.0;
				if (long.TryParse(num2.ToString(), ref num))
				{
					return string.Format(GameDataUtils.GetChineseContent(15, false), num.ToString());
				}
				return string.Format(GameDataUtils.GetChineseContent(15, false), Math.Round(num2, 2).ToString("F2"));
			}
			else
			{
				if ((double)value < 100000.0)
				{
					return value.ToString();
				}
				double num2 = (double)value / 10000.0;
				if (long.TryParse(num2.ToString(), ref num))
				{
					return string.Format(GameDataUtils.GetChineseContent(14, false), num.ToString());
				}
				return string.Format(GameDataUtils.GetChineseContent(14, false), Math.Round(num2, 1).ToString("F1"));
			}
		}
		else
		{
			int num3 = 0;
			if (int.TryParse(((double)value * 0.1).ToString(), ref num3))
			{
				return string.Format("{0}%", num3.ToString());
			}
			return string.Format("{0}%", Math.Round((double)value * 0.1, 1).ToString("F1"));
		}
	}

	public static string GetAttrValueDisplay(int typeNum, long value)
	{
		return AttrUtility.GetAttrValueDisplay((AttrType)typeNum, value);
	}

	public static string GetAttrValueDisplay(int typeNum, int value, string hyphen)
	{
		return string.Format("{0}{1}", hyphen, AttrUtility.GetAttrValueDisplay((AttrType)typeNum, value));
	}

	public static string GetAttrValueDisplay(int typeNum, long value, string hyphen)
	{
		return string.Format("{0}{1}", hyphen, AttrUtility.GetAttrValueDisplay((AttrType)typeNum, value));
	}

	public static string GetAddAttrValueDisplay(int typeNum, int value)
	{
		return AttrUtility.GetAttrValueDisplay(typeNum, value, "+");
	}

	public static string GetAddAttrValueDisplay(int typeNum, long value)
	{
		return AttrUtility.GetAttrValueDisplay(typeNum, value, "+");
	}

	public static string GetColonAttrValueDisplay(int typeNum, int value)
	{
		return AttrUtility.GetAttrValueDisplay(typeNum, value, ":");
	}

	public static string GetColonAttrValueDisplay(int typeNum, long value)
	{
		return AttrUtility.GetAttrValueDisplay(typeNum, value, ":");
	}

	public static string GetDesc(AttrType type, int value, string hyphen, string typeColor, string valueColor, bool isHyphenAccordWithType)
	{
		if (isHyphenAccordWithType)
		{
			return string.Format("{0}{1}", TextColorMgr.GetColor(string.Format("{0}{1}", AttrUtility.GetAttrName(type), hyphen), typeColor, string.Empty), TextColorMgr.GetColor(AttrUtility.GetAttrValueDisplay(type, value), valueColor, string.Empty));
		}
		return string.Format("{0}{1}", TextColorMgr.GetColor(AttrUtility.GetAttrName(type), typeColor, string.Empty), TextColorMgr.GetColor(string.Format("{0}{1}", hyphen, AttrUtility.GetAttrValueDisplay(type, value)), valueColor, string.Empty));
	}

	public static string GetDesc(int typeNum, int value, string hyphen, string typeColor, string valueColor, bool isHyphenAccordWithType)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, hyphen, typeColor, valueColor, isHyphenAccordWithType);
	}

	public static string GetDesc(AttrType type, long value, string hyphen, string typeColor, string valueColor, bool isHyphenAccordWithType)
	{
		if (isHyphenAccordWithType)
		{
			return string.Format("{0}{1}", TextColorMgr.GetColor(string.Format("{0}{1}", AttrUtility.GetAttrName(type), hyphen), typeColor, string.Empty), TextColorMgr.GetColor(AttrUtility.GetAttrValueDisplay(type, value), valueColor, string.Empty));
		}
		return string.Format("{0}{1}", TextColorMgr.GetColor(AttrUtility.GetAttrName(type), typeColor, string.Empty), TextColorMgr.GetColor(string.Format("{0}{1}", hyphen, AttrUtility.GetAttrValueDisplay(type, value)), valueColor, string.Empty));
	}

	public static string GetDesc(int typeNum, long value, string hyphen, string typeColor, string valueColor, bool isHyphenAccordWithType)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, hyphen, typeColor, valueColor, isHyphenAccordWithType);
	}

	public static string GetStandardDesc(AttrType type, int value)
	{
		return AttrUtility.GetDesc(type, value, ": ", string.Empty, string.Empty, true);
	}

	public static string GetStandardDesc(int typeNum, int value)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ": ", string.Empty, string.Empty, true);
	}

	public static string GetStandardDesc(AttrType type, long value)
	{
		return AttrUtility.GetDesc(type, value, ": ", string.Empty, string.Empty, true);
	}

	public static string GetStandardDesc(int typeNum, long value)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ": ", string.Empty, string.Empty, true);
	}

	public static string GetStandardDesc(AttrType type, int value, string valueColor)
	{
		return AttrUtility.GetDesc(type, value, ": ", string.Empty, valueColor, true);
	}

	public static string GetStandardDesc(int typeNum, int value, string valueColor)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ": ", string.Empty, valueColor, true);
	}

	public static string GetStandardDesc(AttrType type, long value, string valueColor)
	{
		return AttrUtility.GetDesc(type, value, ": ", string.Empty, valueColor, true);
	}

	public static string GetStandardDesc(int typeNum, long value, string valueColor)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ": ", string.Empty, valueColor, true);
	}

	public static string GetStandardDesc(AttrType type, int value, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc(type, value, ":" + ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)), string.Empty, valueColor, true);
	}

	public static string GetStandardDesc(int typeNum, int value, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ":" + ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)), string.Empty, valueColor, true);
	}

	public static string GetStandardDesc(AttrType type, long value, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc(type, value, ":" + ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)), string.Empty, valueColor, true);
	}

	public static string GetStandardDesc(int typeNum, long value, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ":" + ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)), string.Empty, valueColor, true);
	}

	public static string GetStandardDesc(AttrType type, int value, string typeColor, string valueColor)
	{
		return AttrUtility.GetDesc(type, value, ": ", typeColor, valueColor, true);
	}

	public static string GetStandardDesc(int typeNum, int value, string typeColor, string valueColor)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ": ", typeColor, valueColor, true);
	}

	public static string GetStandardDesc(AttrType type, long value, string typeColor, string valueColor)
	{
		return AttrUtility.GetDesc(type, value, ": ", typeColor, valueColor, true);
	}

	public static string GetStandardDesc(int typeNum, long value, string typeColor, string valueColor)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ": ", typeColor, valueColor, true);
	}

	public static string GetStandardDesc(AttrType type, int value, string typeColor, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc(type, value, ":" + ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)), typeColor, valueColor, true);
	}

	public static string GetStandardDesc(int typeNum, int value, string typeColor, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ":" + ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)), typeColor, valueColor, true);
	}

	public static string GetStandardDesc(AttrType type, long value, string typeColor, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc(type, value, ":" + ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)), typeColor, valueColor, true);
	}

	public static string GetStandardDesc(int typeNum, long value, string typeColor, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ":" + ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)), typeColor, valueColor, true);
	}

	public static string GetStandardAddDesc(AttrType type, int value)
	{
		return AttrUtility.GetDesc(type, value, " +", string.Empty, string.Empty, false);
	}

	public static string GetStandardAddDesc(int typeNum, int value)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, " +", string.Empty, string.Empty, false);
	}

	public static string GetStandardAddDesc(AttrType type, long value)
	{
		return AttrUtility.GetDesc(type, value, " +", string.Empty, string.Empty, false);
	}

	public static string GetStandardAddDesc(int typeNum, long value)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, " +", string.Empty, string.Empty, false);
	}

	public static string GetStandardAddDesc(AttrType type, int value, string valueColor)
	{
		return AttrUtility.GetDesc(type, value, " +", string.Empty, valueColor, false);
	}

	public static string GetStandardAddDesc(int typeNum, int value, string valueColor)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, " +", string.Empty, valueColor, false);
	}

	public static string GetStandardAddDesc(AttrType type, long value, string valueColor)
	{
		return AttrUtility.GetDesc(type, value, " +", string.Empty, valueColor, false);
	}

	public static string GetStandardAddDesc(int typeNum, long value, string valueColor)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, " +", string.Empty, valueColor, false);
	}

	public static string GetStandardAddDesc(AttrType type, int value, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc(type, value, ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)) + "+", string.Empty, valueColor, false);
	}

	public static string GetStandardAddDesc(int typeNum, int value, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)) + "+", string.Empty, valueColor, false);
	}

	public static string GetStandardAddDesc(AttrType type, long value, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc(type, value, ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)) + "+", string.Empty, valueColor, false);
	}

	public static string GetStandardAddDesc(int typeNum, long value, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)) + "+", string.Empty, valueColor, false);
	}

	public static string GetStandardAddDesc(AttrType type, int value, string typeColor, string valueColor)
	{
		return AttrUtility.GetDesc(type, value, " +", typeColor, valueColor, false);
	}

	public static string GetStandardAddDesc(int typeNum, int value, string typeColor, string valueColor)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, " +", typeColor, valueColor, false);
	}

	public static string GetStandardAddDesc(AttrType type, long value, string typeColor, string valueColor)
	{
		return AttrUtility.GetDesc(type, value, " +", typeColor, valueColor, false);
	}

	public static string GetStandardAddDesc(int typeNum, long value, string typeColor, string valueColor)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, " +", typeColor, valueColor, false);
	}

	public static string GetStandardAddDesc(AttrType type, int value, string typeColor, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc(type, value, ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)) + "+", typeColor, valueColor, false);
	}

	public static string GetStandardAddDesc(int typeNum, int value, string typeColor, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)) + "+", typeColor, valueColor, false);
	}

	public static string GetStandardAddDesc(AttrType type, long value, string typeColor, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc(type, value, ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)) + "+", typeColor, valueColor, false);
	}

	public static string GetStandardAddDesc(int typeNum, long value, string typeColor, string valueColor, int blankNum)
	{
		return AttrUtility.GetDesc((AttrType)typeNum, value, ((blankNum <= 0) ? string.Empty : new string(' ', blankNum)) + "+", typeColor, valueColor, false);
	}

	public static string GetExpValueStr(long value)
	{
		return AttrUtility.GetAttrValueDisplay(AttrType.Exp, value);
	}

	public static string GetGoldValueStr(long value)
	{
		return AttrUtility.GetAttrValueDisplay(AttrType.Gold, value);
	}

	public static List<string> GetAllStandardDesc(int dataID)
	{
		List<string> list = new List<string>();
		if (!DataReader<Attrs>.Contains(dataID))
		{
			return list;
		}
		Attrs attrs = DataReader<Attrs>.Get(dataID);
		int num = (attrs.attrs.get_Count() >= attrs.values.get_Count()) ? attrs.values.get_Count() : attrs.attrs.get_Count();
		for (int i = 0; i < num; i++)
		{
			list.Add(AttrUtility.GetStandardDesc(attrs.attrs.get_Item(i), attrs.values.get_Item(i)));
		}
		return list;
	}

	public static List<string> GetAllStandardAddDesc(int dataID)
	{
		List<string> list = new List<string>();
		if (!DataReader<Attrs>.Contains(dataID))
		{
			return list;
		}
		Attrs attrs = DataReader<Attrs>.Get(dataID);
		int num = (attrs.attrs.get_Count() >= attrs.values.get_Count()) ? attrs.values.get_Count() : attrs.attrs.get_Count();
		for (int i = 0; i < num; i++)
		{
			list.Add(AttrUtility.GetStandardAddDesc(attrs.attrs.get_Item(i), attrs.values.get_Item(i)));
		}
		return list;
	}
}
