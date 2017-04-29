using GameData;
using Package;
using System;

public class LocalBattleBuffCalculatorHandler
{
	protected static XDict<GameData.AttrType, long> GetBuffCasterTempAttr(Buff buffData, EntityParent caster, int fromSkillLevel, XDict<GameData.AttrType, BattleSkillAttrAdd> casterTempAttrsChange)
	{
		XDict<GameData.AttrType, long> xDict = new XDict<GameData.AttrType, long>();
		if ((caster.IsEntitySelfType || caster.IsEntityPlayerType || caster.IsEntityPetType) && buffData.rolePropId.get_Count() > 0 && fromSkillLevel > 0)
		{
			for (int i = 0; i < buffData.rolePropId.get_Count(); i++)
			{
				if (buffData.rolePropId.get_Item(i).key == fromSkillLevel)
				{
					Attrs attrs = DataReader<Attrs>.Get(buffData.rolePropId.get_Item(i).value);
					if (attrs != null)
					{
						for (int j = 0; j < attrs.attrs.get_Count(); j++)
						{
							xDict.Add((GameData.AttrType)attrs.attrs.get_Item(j), (long)attrs.values.get_Item(j));
						}
					}
					break;
				}
			}
		}
		else if (buffData.propId > 0)
		{
			Attrs attrs2 = DataReader<Attrs>.Get(buffData.propId);
			if (attrs2 != null)
			{
				for (int k = 0; k < attrs2.attrs.get_Count(); k++)
				{
					xDict.Add((GameData.AttrType)attrs2.attrs.get_Item(k), (long)attrs2.values.get_Item(k));
				}
			}
		}
		if (casterTempAttrsChange != null)
		{
			for (int l = 0; l < casterTempAttrsChange.Count; l++)
			{
				if (xDict.ContainsKey(casterTempAttrsChange.Keys.get_Item(l)))
				{
					xDict[casterTempAttrsChange.Keys.get_Item(l)] = (long)((double)(xDict[casterTempAttrsChange.Keys.get_Item(l)] + (long)casterTempAttrsChange.Values.get_Item(l).addiAdd) * (1.0 + (double)casterTempAttrsChange.Values.get_Item(l).multiAdd * 0.001));
				}
			}
		}
		return xDict;
	}

	protected static XDict<GameData.AttrType, long> GetBuffTargetTempAttr(Buff buffData, EntityParent target, int fromSkillLevel, XDict<GameData.AttrType, BattleSkillAttrAdd> targetTempAttrsChange)
	{
		XDict<GameData.AttrType, long> xDict = new XDict<GameData.AttrType, long>();
		if ((target.IsEntitySelfType || target.IsEntityPlayerType || target.IsEntityPetType) && buffData.roleTargetPropId.get_Count() > 0 && fromSkillLevel > 0)
		{
			for (int i = 0; i < buffData.roleTargetPropId.get_Count(); i++)
			{
				if (buffData.roleTargetPropId.get_Item(i).key == fromSkillLevel)
				{
					Attrs attrs = DataReader<Attrs>.Get(buffData.roleTargetPropId.get_Item(i).value);
					if (attrs != null)
					{
						for (int j = 0; j < attrs.attrs.get_Count(); j++)
						{
							xDict.Add((GameData.AttrType)attrs.attrs.get_Item(j), (long)attrs.values.get_Item(j));
						}
					}
					break;
				}
			}
		}
		else if (buffData.targetPropId > 0)
		{
			Attrs attrs2 = DataReader<Attrs>.Get(buffData.targetPropId);
			if (attrs2 != null)
			{
				for (int k = 0; k < attrs2.attrs.get_Count(); k++)
				{
					xDict.Add((GameData.AttrType)attrs2.attrs.get_Item(k), (long)attrs2.values.get_Item(k));
				}
			}
		}
		if (targetTempAttrsChange != null)
		{
			for (int l = 0; l < targetTempAttrsChange.Count; l++)
			{
				if (xDict.ContainsKey(targetTempAttrsChange.Keys.get_Item(l)))
				{
					xDict[targetTempAttrsChange.Keys.get_Item(l)] = (long)((double)(xDict[targetTempAttrsChange.Keys.get_Item(l)] + (long)targetTempAttrsChange.Values.get_Item(l).addiAdd) * (1.0 + (double)targetTempAttrsChange.Values.get_Item(l).multiAdd * 0.001));
				}
			}
		}
		return xDict;
	}
}
