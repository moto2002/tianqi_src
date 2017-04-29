using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class PetEvoGlobal
{
	private static Pet GetPetRow(int petId)
	{
		return DataReader<Pet>.Get(petId);
	}

	public static int GetNeedQulity(int petId, int talentId)
	{
		Pet petRow = PetEvoGlobal.GetPetRow(petId);
		List<int> talent = petRow.talent;
		List<int> talentStart = petRow.talentStart;
		int num = 0;
		using (List<int>.Enumerator enumerator = talent.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				if (current == talentId)
				{
					return talentStart.get_Item(num);
				}
				num++;
			}
		}
		return -1;
	}

	public static ChongWuTianFu GetChongWuTianFuRow(int talentId)
	{
		return DataReader<ChongWuTianFu>.Get(talentId);
	}

	public static int GetEffect(int talentId)
	{
		return PetEvoGlobal.GetChongWuTianFuRow(talentId).effect;
	}

	public static string GetDesc(int talentId)
	{
		ChongWuTianFu chongWuTianFuRow = PetEvoGlobal.GetChongWuTianFuRow(talentId);
		return GameDataUtils.GetChineseContent(chongWuTianFuRow.describe, false);
	}

	public static SpriteRenderer GetSprite(int talentId)
	{
		string picture = PetEvoGlobal.GetChongWuTianFuRow(talentId).picture;
		return ResourceManager.GetIconSprite(picture);
	}

	public static string GetName(int talentId)
	{
		int name = PetEvoGlobal.GetChongWuTianFuRow(talentId).name;
		return GameDataUtils.GetChineseContent(name, false);
	}

	public static TianFuDengJiGuiZe GetTianFuDengJiGuiZeRow(string rowId)
	{
		if (DataReader<TianFuDengJiGuiZe>.Contains(rowId))
		{
			return DataReader<TianFuDengJiGuiZe>.Get(rowId);
		}
		return null;
	}

	public static TianFuDengJiGuiZe GetMaterial(int talentId, int talentLv)
	{
		string rowId = PetEvoGlobal.GetChongWuTianFuRow(talentId).lvRuleId.ToString() + "_" + talentLv.ToString();
		return PetEvoGlobal.GetTianFuDengJiGuiZeRow(rowId);
	}

	public static SpriteRenderer GetMaterialSprite(int talentId, int talentLv)
	{
		int materialId = PetEvoGlobal.GetMaterialId(talentId, talentLv);
		Items items = DataReader<Items>.Get(materialId);
		return GameDataUtils.GetIcon(items.littleIcon);
	}

	public static int GetMaterialId(int talentId, int talentLv)
	{
		return PetEvoGlobal.GetMaterial(talentId, talentLv).item.get_Item(0);
	}

	public static int GetMaterialNum(int talentId, int talentLv)
	{
		return PetEvoGlobal.GetMaterial(talentId, talentLv).num.get_Item(0);
	}

	public static int GetMaxTalentLv(int talentId)
	{
		int lvRuleId = PetEvoGlobal.GetChongWuTianFuRow(talentId).lvRuleId;
		int num = 0;
		using (List<TianFuDengJiGuiZe>.Enumerator enumerator = DataReader<TianFuDengJiGuiZe>.DataList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TianFuDengJiGuiZe current = enumerator.get_Current();
				if (current.lvRule.IndexOf(lvRuleId.ToString()) != -1)
				{
					num++;
				}
			}
		}
		return num;
	}

	public static bool IsMaxSkillLv(int talentId, int talentLv)
	{
		int lvRuleId = PetEvoGlobal.GetChongWuTianFuRow(talentId).lvRuleId;
		int num = talentLv + 1;
		string rowId = lvRuleId.ToString() + "_" + num.ToString();
		return PetEvoGlobal.GetTianFuDengJiGuiZeRow(rowId) == null;
	}

	public static long GetHaveMaterailCount(int talentId, int talentLv)
	{
		int materialId = PetEvoGlobal.GetMaterialId(talentId, talentLv);
		return BackpackManager.Instance.OnGetGoodCount(materialId);
	}

	public static bool IsEnoughMaterail(int talentId, int talentLv)
	{
		long haveMaterailCount = PetEvoGlobal.GetHaveMaterailCount(talentId, talentLv);
		int materialNum = PetEvoGlobal.GetMaterialNum(talentId, talentLv);
		return haveMaterailCount >= (long)materialNum;
	}

	public static TianFuShuXing GetTianFuShuXingRow(string attributeId)
	{
		return DataReader<TianFuShuXing>.Get(attributeId);
	}

	public static int GetAttributeTemplateID(int attributeId, int lv)
	{
		string attributeId2 = attributeId.ToString() + "_" + lv;
		return PetEvoGlobal.GetTianFuShuXingRow(attributeId2).attributeTemplateID;
	}

	public static List<int> GetAttributeTemplateIDs(List<int> parameter, int lv)
	{
		List<int> list = new List<int>();
		using (List<int>.Enumerator enumerator = parameter.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				int attributeTemplateID = PetEvoGlobal.GetAttributeTemplateID(current, lv);
				list.Add(attributeTemplateID);
			}
		}
		return list;
	}

	public static List<int> GetNaturalIds(int petId)
	{
		return PetEvoGlobal.GetIds(petId, 2);
	}

	public static List<int> GetSkillIds(int petId)
	{
		return PetEvoGlobal.GetIds(petId, 1);
	}

	private static List<int> GetIds(int petId, int type)
	{
		List<int> list = new List<int>();
		Pet petRow = PetEvoGlobal.GetPetRow(petId);
		List<int> talent = petRow.talent;
		using (List<int>.Enumerator enumerator = talent.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				ChongWuTianFu chongWuTianFuRow = PetEvoGlobal.GetChongWuTianFuRow(current);
				if (chongWuTianFuRow.type == type)
				{
					list.Add(current);
				}
			}
		}
		return list;
	}

	public static PetInfo GetOnePet(int petId)
	{
		return PetManager.Instance.GetPetInfoById(petId);
	}

	public static int GetPetStar(int petId)
	{
		PetInfo onePet = PetEvoGlobal.GetOnePet(petId);
		if (onePet != null)
		{
			return onePet.star;
		}
		return 0;
	}

	public static int GetPetLv(int petId)
	{
		PetInfo onePet = PetEvoGlobal.GetOnePet(petId);
		return (onePet == null) ? 0 : onePet.lv;
	}

	public static int GetPetQuality(int petId)
	{
		PetInfo onePet = PetEvoGlobal.GetOnePet(petId);
		if (onePet != null)
		{
			return onePet.quality;
		}
		return 0;
	}

	public static int GetSkillLv(int petId, int talentId)
	{
		PetInfo onePet = PetEvoGlobal.GetOnePet(petId);
		if (onePet == null || onePet.petTalents == null)
		{
			return 1;
		}
		using (List<PetTalent>.Enumerator enumerator = onePet.petTalents.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PetTalent current = enumerator.get_Current();
				if (current.talentId == talentId)
				{
					return current.talentLv;
				}
			}
		}
		return 1;
	}

	public static int GetRequireStar(int petId, int talentId)
	{
		Pet petRow = PetEvoGlobal.GetPetRow(petId);
		for (int i = 0; i < petRow.talent.get_Count(); i++)
		{
			if (petRow.talent.get_Item(i) == talentId)
			{
				return petRow.talentStart.get_Item(i);
			}
		}
		return -1;
	}

	public static bool IsEnoughStar(int petId, int talentId)
	{
		return PetEvoGlobal.GetPetStar(petId) >= PetEvoGlobal.GetRequireStar(petId, talentId);
	}

	public static bool IsSkillCanLvUp(int petId, int talentId, int talentLv)
	{
		return PetEvoGlobal.IsEnoughStar(petId, talentId) && PetEvoGlobal.IsSkillLvLessThanPetLv(petId, talentId);
	}

	public static bool IsSkillLvLessThanPetLv(int petId, int talentId)
	{
		return PetEvoGlobal.GetSkillLv(petId, talentId) < PetEvoGlobal.GetPetLv(petId);
	}

	public static List<int> GetTalentIds(int petId, int effect)
	{
		List<int> list = new List<int>();
		PetInfo onePet = PetEvoGlobal.GetOnePet(petId);
		if (onePet == null || onePet.petTalents == null)
		{
			return list;
		}
		using (List<PetTalent>.Enumerator enumerator = onePet.petTalents.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PetTalent current = enumerator.get_Current();
				ChongWuTianFu chongWuTianFuRow = PetEvoGlobal.GetChongWuTianFuRow(current.talentId);
				if (chongWuTianFuRow != null)
				{
					if (chongWuTianFuRow.effect == effect)
					{
						list.Add(current.talentId);
					}
				}
			}
		}
		return list;
	}

	public static List<PetAttribute> ManualSkill(int petId)
	{
		List<PetAttribute> list = new List<PetAttribute>();
		List<int> talentIds = PetEvoGlobal.GetTalentIds(petId, 11);
		List<int> talentIds2 = PetEvoGlobal.GetTalentIds(petId, 15);
		List<int> talentIds3 = PetEvoGlobal.GetTalentIds(petId, 21);
		int num = -1;
		if (talentIds.get_Count() == 0)
		{
			return list;
		}
		int num2 = 0;
		while (num2 < talentIds.get_Count() && num == -1)
		{
			ChongWuTianFu chongWuTianFuRow = PetEvoGlobal.GetChongWuTianFuRow(talentIds.get_Item(num2));
			int skillLv = PetEvoGlobal.GetSkillLv(petId, talentIds.get_Item(num2));
			for (int i = 0; i < talentIds3.get_Count(); i++)
			{
				ChongWuTianFu chongWuTianFuRow2 = PetEvoGlobal.GetChongWuTianFuRow(talentIds3.get_Item(i));
				if (chongWuTianFuRow2.parameter.get_Item(0) == talentIds.get_Item(num2))
				{
					num = chongWuTianFuRow2.parameter2.get_Item(0);
				}
			}
			int num3 = 0;
			while (num3 < talentIds2.get_Count() && num == -1)
			{
				ChongWuTianFu chongWuTianFuRow3 = PetEvoGlobal.GetChongWuTianFuRow(talentIds2.get_Item(num3));
				if (chongWuTianFuRow3.parameter.get_Item(0) == talentIds.get_Item(num2))
				{
					num = chongWuTianFuRow3.parameter2.get_Item(0);
				}
				num3++;
			}
			if (num == -1)
			{
				num = chongWuTianFuRow.parameter.get_Item(0);
			}
			list.Add(new PetAttribute
			{
				petSkillId = num,
				petSkillLv = skillLv,
				petSkillSlot = chongWuTianFuRow.parameter2.get_Item(1),
				roleSkillId = chongWuTianFuRow.parameter2.get_Item(0),
				roleSkillLv = 1
			});
			num2++;
		}
		return list;
	}

	public static List<PetAttribute> GetSkills10(int petId)
	{
		return PetEvoGlobal.GetSkills(petId, 10);
	}

	public static List<PetAttribute> GetSkills11(int petId)
	{
		return PetEvoGlobal.GetSkills(petId, 11);
	}

	public static List<PetAttribute> GetSkills12(int petId)
	{
		return PetEvoGlobal.GetSkills(petId, 12);
	}

	public static List<PetAttribute> GetSkills13(int petId)
	{
		return PetEvoGlobal.GetSkills(petId, 13);
	}

	public static List<PetAttribute> GetSkills14(int petId)
	{
		return PetEvoGlobal.GetSkills(petId, 14);
	}

	public static List<PetAttribute> GetSkills15(int petId)
	{
		return PetEvoGlobal.GetSkills(petId, 15);
	}

	public static List<PetAttribute> GetSkills(int petId, int effect)
	{
		List<PetAttribute> list = new List<PetAttribute>();
		PetInfo onePet = PetEvoGlobal.GetOnePet(petId);
		if (onePet == null || onePet.petTalents == null)
		{
			return list;
		}
		using (List<PetTalent>.Enumerator enumerator = onePet.petTalents.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PetTalent current = enumerator.get_Current();
				ChongWuTianFu chongWuTianFuRow = PetEvoGlobal.GetChongWuTianFuRow(current.talentId);
				int skillLv = PetEvoGlobal.GetSkillLv(petId, current.talentId);
				if (chongWuTianFuRow != null)
				{
					if (skillLv != 0)
					{
						if (chongWuTianFuRow.effect == effect)
						{
							if (chongWuTianFuRow.effect == 10)
							{
								List<int> talentIds = PetEvoGlobal.GetTalentIds(petId, 15);
								List<int> talentIds2 = PetEvoGlobal.GetTalentIds(petId, 21);
								int num = -1;
								if (talentIds2.get_Count() > 0)
								{
									num = talentIds2.get_Item(0);
								}
								else if (talentIds.get_Count() > 0)
								{
									num = talentIds.get_Item(0);
								}
								if (num != -1)
								{
									ChongWuTianFu chongWuTianFuRow2 = PetEvoGlobal.GetChongWuTianFuRow(num);
									int num2 = chongWuTianFuRow2.parameter.get_Item(0);
									if (num2 == current.talentId)
									{
										for (int i = 0; i < chongWuTianFuRow2.parameter2.get_Count(); i++)
										{
											list.Add(new PetAttribute
											{
												petSkillId = chongWuTianFuRow2.parameter2.get_Item(i),
												petSkillLv = skillLv,
												petSkillSlot = chongWuTianFuRow.parameter2.get_Item(i)
											});
										}
										continue;
									}
								}
								for (int j = 0; j < chongWuTianFuRow.parameter.get_Count(); j++)
								{
									list.Add(new PetAttribute
									{
										petSkillId = chongWuTianFuRow.parameter.get_Item(j),
										petSkillLv = skillLv,
										petSkillSlot = chongWuTianFuRow.parameter2.get_Item(j)
									});
								}
							}
							else if (chongWuTianFuRow.effect == 12)
							{
								PetAttribute petAttribute = new PetAttribute
								{
									templateIds = PetEvoGlobal.GetAttributeTemplateIDs(chongWuTianFuRow.parameter, skillLv)
								};
								list.Add(petAttribute);
							}
							else if (chongWuTianFuRow.effect == 13)
							{
								PetAttribute petAttribute2 = new PetAttribute
								{
									templateIds = chongWuTianFuRow.parameter,
									templateIds2 = PetEvoGlobal.GetAttributeTemplateIDs(chongWuTianFuRow.parameter2, skillLv)
								};
								list.Add(petAttribute2);
							}
							else if (chongWuTianFuRow.effect == 14)
							{
								PetAttribute petAttribute3 = new PetAttribute
								{
									templateIds = chongWuTianFuRow.parameter,
									templateIds2 = PetEvoGlobal.GetAttributeTemplateIDs(chongWuTianFuRow.parameter2, skillLv)
								};
								list.Add(petAttribute3);
							}
							else
							{
								Debug.LogError("!!! row.effect=" + chongWuTianFuRow.effect);
							}
						}
					}
				}
			}
		}
		return list;
	}

	public static List<string> GetAttrTexts(int propertyType)
	{
		List<string> list = new List<string>();
		List<int> attrs = PetEvoGlobal.GetAttrRow(propertyType).attrs;
		List<int> values = PetEvoGlobal.GetAttrRow(propertyType).values;
		for (int i = 0; i < attrs.get_Count(); i++)
		{
			list.Add(AttrUtility.GetStandardAddDesc(attrs.get_Item(i), values.get_Item(i)));
		}
		return list;
	}

	public static Attrs GetAttrRow(int typeId)
	{
		return DataReader<Attrs>.Get(typeId);
	}

	private static bool IsOneSkillCanLvUp(int petId, int talentId, int talentLv, int nextTalentLv)
	{
		return !PetEvoGlobal.IsMaxSkillLv(talentId, talentLv) && PetEvoGlobal.IsSkillCanLvUp(petId, talentId, nextTalentLv) && PetManager.Instance.GetSkillPoint() > 0;
	}

	public static bool IsSkillsCanLvUp(int petId)
	{
		List<int> skillIds = PetEvoGlobal.GetSkillIds(petId);
		using (List<int>.Enumerator enumerator = skillIds.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				int skillLv = PetEvoGlobal.GetSkillLv(petId, current);
				int nextTalentLv = skillLv + 1;
				if (PetEvoGlobal.IsOneSkillCanLvUp(petId, current, skillLv, nextTalentLv))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool IsCurrPetCanSkillLvUp()
	{
		return PetEvoGlobal.IsSkillsCanLvUp(PetBasicUIViewModel.PetID);
	}

	public static bool IsHavePetCanSkillLvUp()
	{
		using (Dictionary<long, PetInfo>.Enumerator enumerator = PetManager.Instance.MaplistPet.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<long, PetInfo> current = enumerator.get_Current();
				int petId = current.get_Value().petId;
				if (PetEvoGlobal.IsSkillsCanLvUp(petId))
				{
					return true;
				}
			}
		}
		return false;
	}
}
