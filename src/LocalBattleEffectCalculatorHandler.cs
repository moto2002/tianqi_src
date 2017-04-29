using GameData;
using Package;
using System;

public class LocalBattleEffectCalculatorHandler
{
	public static XDict<GameData.AttrType, long> GetEffectCasterTempAttr(Effect effectData, EntityParent caster, int fromSkillID)
	{
		XDict<GameData.AttrType, long> xDict = new XDict<GameData.AttrType, long>();
		int skillLevelByID = caster.GetSkillLevelByID(fromSkillID);
		XDict<GameData.AttrType, BattleSkillAttrAdd> skillAttrChangeByID = caster.GetSkillAttrChangeByID(fromSkillID);
		if ((caster.IsEntitySelfType || caster.IsEntityPlayerType || caster.IsEntityPetType) && effectData.rolePropId.get_Count() > 0 && skillLevelByID > 0)
		{
			for (int i = 0; i < effectData.rolePropId.get_Count(); i++)
			{
				if (effectData.rolePropId.get_Item(i).key == skillLevelByID)
				{
					Attrs attrs = DataReader<Attrs>.Get(effectData.rolePropId.get_Item(i).value);
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
		else if (effectData.propId > 0)
		{
			Attrs attrs2 = DataReader<Attrs>.Get(effectData.propId);
			if (attrs2 != null)
			{
				for (int k = 0; k < attrs2.attrs.get_Count(); k++)
				{
					xDict.Add((GameData.AttrType)attrs2.attrs.get_Item(k), (long)attrs2.values.get_Item(k));
				}
			}
		}
		if (skillAttrChangeByID != null)
		{
			for (int l = 0; l < skillAttrChangeByID.Count; l++)
			{
				if (xDict.ContainsKey(skillAttrChangeByID.Keys.get_Item(l)))
				{
					xDict[skillAttrChangeByID.Keys.get_Item(l)] = (long)((double)(xDict[skillAttrChangeByID.Keys.get_Item(l)] + (long)skillAttrChangeByID.Values.get_Item(l).addiAdd) * (1.0 + (double)skillAttrChangeByID.Values.get_Item(l).multiAdd * 0.001));
				}
			}
		}
		return xDict;
	}
}
