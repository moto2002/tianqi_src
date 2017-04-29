using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetEvoTip : UIBase
{
	private const int attrLineCount = 4;

	public void Init(int talentId)
	{
		int skillLv = PetEvoGlobal.GetSkillLv(PetBasicUIViewModel.PetID, talentId);
		int num = skillLv + 1;
		Text component = base.get_transform().FindChild("texName").GetComponent<Text>();
		Text component2 = base.get_transform().FindChild("texLv").GetComponent<Text>();
		Text component3 = base.get_transform().FindChild("texDesc").GetComponent<Text>();
		Text component4 = base.get_transform().FindChild("texLvNext").GetComponent<Text>();
		Text component5 = base.get_transform().FindChild("texDescNext").GetComponent<Text>();
		ChongWuTianFu chongWuTianFuRow = PetEvoGlobal.GetChongWuTianFuRow(talentId);
		int describe = chongWuTianFuRow.describe;
		string chineseContent = GameDataUtils.GetChineseContent(describe, false);
		string rowId = chongWuTianFuRow.lvRuleId + "_" + skillLv;
		List<float> describeValue = PetEvoGlobal.GetTianFuDengJiGuiZeRow(rowId).describeValue;
		component.set_text(PetEvoGlobal.GetName(talentId));
		component2.set_text("当前");
		if (describe != 0)
		{
			component3.set_text(this.ReplaceDesc(chineseContent, describeValue));
		}
		else
		{
			component3.set_text(string.Empty);
		}
		if (PetEvoGlobal.IsMaxSkillLv(talentId, skillLv))
		{
			component4.set_text("该技能已提升到满级");
			component5.set_text(string.Empty);
		}
		else
		{
			component4.set_text("下一级");
			string rowId2 = chongWuTianFuRow.lvRuleId + "_" + num;
			describeValue = PetEvoGlobal.GetTianFuDengJiGuiZeRow(rowId2).describeValue;
			if (describe != 0)
			{
				component5.set_text(this.ReplaceDesc(chineseContent, describeValue));
			}
			else
			{
				component5.set_text(string.Empty);
			}
		}
	}

	private string ReplaceDesc(string origin, List<float> describeValue)
	{
		string text = origin;
		for (int i = 0; i < describeValue.get_Count(); i++)
		{
			string text2 = "{" + i + "}";
			string text3 = describeValue.get_Item(i).ToString();
			text = text.Replace(text2, text3);
		}
		return text;
	}

	private void SetTexName(Text texName, int talentId, int talentLv)
	{
		if (talentLv == 0)
		{
			texName.set_text(PetEvoGlobal.GetName(talentId));
		}
		else
		{
			texName.set_text(string.Concat(new object[]
			{
				PetEvoGlobal.GetName(talentId),
				" (LV.",
				talentLv,
				")"
			}));
		}
	}

	private void SetTexDesc(Text texDesc, int talentId, int talentLv)
	{
		texDesc.set_text(PetEvoGlobal.GetDesc(talentId));
	}

	private void SetAttr(int talentId, int talentLv, List<string>[] vals)
	{
		Text[] array = new Text[4];
		Text[] array2 = new Text[4];
		for (int i = 0; i < 4; i++)
		{
			array[i] = base.get_transform().FindChild("texLv" + i).GetComponent<Text>();
			array2[i] = base.get_transform().FindChild("texAttr" + i).GetComponent<Text>();
			array[i].set_text(string.Empty);
			array2[i].set_text(string.Empty);
		}
		array[0].set_text("当前等级");
		int num = 0;
		using (List<string>.Enumerator enumerator = vals[0].GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.get_Current();
				array2[num].set_text(current);
				num++;
			}
		}
		array[num].set_text("下一等级");
		using (List<string>.Enumerator enumerator2 = vals[1].GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				string current2 = enumerator2.get_Current();
				array2[num].set_text(current2);
				num++;
			}
		}
	}

	private List<string> GetAttrVals(int talentId, int talentLv)
	{
		Debug.LogError(string.Concat(new object[]
		{
			"talentId=",
			talentId,
			" talentLv=",
			talentLv
		}));
		int maxTalentLv = PetEvoGlobal.GetMaxTalentLv(talentId);
		if (talentLv == 0)
		{
			List<string> list = new List<string>();
			list.Add("无");
			return list;
		}
		if (talentLv > maxTalentLv)
		{
			List<string> list = new List<string>();
			list.Add("已达最大等级");
			return list;
		}
		ChongWuTianFu chongWuTianFuRow = PetEvoGlobal.GetChongWuTianFuRow(talentId);
		Debug.LogError("effect=" + chongWuTianFuRow.effect);
		if (chongWuTianFuRow.effect == 12)
		{
			string attributeId = chongWuTianFuRow.parameter.get_Item(0) + "_" + talentLv;
			int attributeTemplateID = PetEvoGlobal.GetTianFuShuXingRow(attributeId).attributeTemplateID;
			return PetEvoGlobal.GetAttrTexts(attributeTemplateID);
		}
		if (chongWuTianFuRow.effect == 13)
		{
			string text = chongWuTianFuRow.parameter2.get_Item(0) + "_" + talentLv;
			Debug.LogError("attributeId=" + text);
			int attributeTemplateID2 = PetEvoGlobal.GetTianFuShuXingRow(text).attributeTemplateID;
			Debug.LogError("attributeTemplateID=" + attributeTemplateID2);
			return PetEvoGlobal.GetAttrTexts(attributeTemplateID2);
		}
		if (chongWuTianFuRow.effect == 14)
		{
			string text2 = chongWuTianFuRow.parameter2.get_Item(0) + "_" + talentLv;
			Debug.LogError("attributeId=" + text2);
			int attributeTemplateID3 = PetEvoGlobal.GetTianFuShuXingRow(text2).attributeTemplateID;
			Debug.LogError("attributeTemplateID=" + attributeTemplateID3);
			return PetEvoGlobal.GetAttrTexts(attributeTemplateID3);
		}
		string rowId = chongWuTianFuRow.lvRuleId + "_" + talentLv;
		int describe = PetEvoGlobal.GetTianFuDengJiGuiZeRow(rowId).describe;
		if (describe == 0)
		{
			List<string> list = new List<string>();
			list.Add(string.Empty);
			return list;
		}
		string chineseContent = GameDataUtils.GetChineseContent(describe, false);
		string[] array = chineseContent.Split(new char[]
		{
			'\n'
		});
		List<string> list2 = new List<string>();
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text3 = array2[i];
			list2.Add(text3);
		}
		return list2;
	}
}
