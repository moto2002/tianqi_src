using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetEvoSkillUI : PetEvo
{
	private void Awake()
	{
		base.AwakeSelf();
	}

	protected override void OnEnable()
	{
		this.OffsetY = 110f;
		this.Spacing = 255f;
		base.OnEnable();
		PetBasicUIViewModel.Instance.SetAttackNow();
	}

	protected override void InitCell(int cellIndex)
	{
		GameObject gameObject;
		if (cellIndex < base.get_transform().get_childCount())
		{
			gameObject = base.get_transform().GetChild(cellIndex).get_gameObject();
		}
		else
		{
			gameObject = ResourceManager.GetInstantiate2Prefab(WidgetName.PetEvoSkillCell);
		}
		gameObject.get_transform().SetParent(base.get_transform());
		gameObject.get_transform().set_localPosition(Vector3.get_zero() + new Vector3(0f, this.OffsetY - this.Spacing * (float)cellIndex, 0f));
		gameObject.get_transform().set_localRotation(Quaternion.get_identity());
		gameObject.get_transform().set_localScale(Vector3.get_one());
		gameObject.SetActive(true);
		gameObject.set_name(cellIndex.ToString());
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override List<int> GetCellIds()
	{
		return PetEvoGlobal.GetSkillIds(PetBasicUIViewModel.PetID);
	}

	protected override void OnPlaySKill(GameObject go)
	{
		int talentIdByName = base.GetTalentIdByName(go.get_transform().get_parent().get_name());
		ChongWuTianFu chongWuTianFuRow = PetEvoGlobal.GetChongWuTianFuRow(talentIdByName);
		if (chongWuTianFuRow.parameter.get_Count() > 0)
		{
			int num = chongWuTianFuRow.parameter.get_Item(0);
			Debug.LogError("SkillId=" + num);
			this.PlayPetSKill(num);
		}
		else
		{
			Debug.LogError("技能ID不存在, row.parameter[0]");
		}
	}

	protected override void SetOneSkill(int cellIndex, int talentId, int talentLv, int nextTalentLv)
	{
		Transform child = base.get_transform().GetChild(cellIndex);
		child.FindChild("texName").GetComponent<Text>().set_text(PetEvoGlobal.GetName(talentId));
		child.FindChild("texLv").GetComponent<Text>().set_text("Lv" + talentLv);
		if (PetEvoGlobal.GetOnePet(PetBasicUIViewModel.PetID) == null)
		{
			base.Locked(child, talentId);
		}
		else if (PetEvoGlobal.IsMaxSkillLv(talentId, talentLv))
		{
			base.MaxLv(child);
		}
		else if (PetEvoGlobal.IsSkillCanLvUp(PetBasicUIViewModel.PetID, talentId, nextTalentLv))
		{
			base.CanLvUp(child, talentId, nextTalentLv);
		}
		else
		{
			base.CanNotLvUp(child, talentId, nextTalentLv);
		}
		this.SetSkillDesc(cellIndex, talentId);
	}

	private void PlayPetSKill(int SkillId)
	{
		EventDispatcher.Broadcast("UIManagerControl.HideTipsUI");
		if (SkillId <= 0)
		{
			return;
		}
		CurrenciesUIViewModel.Show(false);
		PetBasicUIView.Instance.SetRawImageModelLayer(true, true, false);
		ModelDisplayManager.ShowSkill(PetBasicUIViewModel.PetModelUID, SkillId, delegate
		{
			CurrenciesUIViewModel.Show(true);
			PetBasicUIView.Instance.SetRawImageModelLayer(false, true, false);
		});
	}

	private void SetSkillDesc(int cellIndex, int skillId)
	{
		Transform child = base.get_transform().GetChild(cellIndex);
		int skillLv = PetEvoGlobal.GetSkillLv(PetBasicUIViewModel.PetID, skillId);
		int num = skillLv + 1;
		ChongWuTianFu chongWuTianFuRow = PetEvoGlobal.GetChongWuTianFuRow(skillId);
		string rowId = chongWuTianFuRow.lvRuleId + "_" + skillLv;
		TianFuDengJiGuiZe tianFuDengJiGuiZeRow = PetEvoGlobal.GetTianFuDengJiGuiZeRow(rowId);
		if (tianFuDengJiGuiZeRow != null)
		{
			int num2 = 0;
			if (tianFuDengJiGuiZeRow.describeValue.get_Count() > 0)
			{
				num2 = (int)tianFuDengJiGuiZeRow.describeValue.get_Item(0);
			}
			if (chongWuTianFuRow.describe != 0)
			{
				string chineseContent = GameDataUtils.GetChineseContent(chongWuTianFuRow.describe, false);
				child.FindChild("texSkillDescCurr").GetComponent<Text>().set_text(string.Format(chineseContent, num2));
			}
			if (PetEvoGlobal.IsMaxSkillLv(skillId, skillLv))
			{
				child.FindChild("texSkillDescNext").GetComponent<Text>().set_text("该技能已提升到满级");
			}
			else
			{
				string rowId2 = chongWuTianFuRow.lvRuleId + "_" + num;
				TianFuDengJiGuiZe tianFuDengJiGuiZeRow2 = PetEvoGlobal.GetTianFuDengJiGuiZeRow(rowId2);
				string chineseContent2 = GameDataUtils.GetChineseContent(500110, false);
				int num3 = 0;
				if (tianFuDengJiGuiZeRow2.describeValue.get_Count() > 0)
				{
					num3 = (int)tianFuDengJiGuiZeRow2.describeValue.get_Item(0);
				}
				child.FindChild("texSkillDescNext").GetComponent<Text>().set_text(string.Format(chineseContent2, num2, num3));
			}
		}
		else
		{
			rowId = chongWuTianFuRow.lvRuleId + "_" + 1;
			tianFuDengJiGuiZeRow = PetEvoGlobal.GetTianFuDengJiGuiZeRow(rowId);
			int num4 = 0;
			if (tianFuDengJiGuiZeRow != null && tianFuDengJiGuiZeRow.describeValue.get_Count() > 0)
			{
				num4 = (int)tianFuDengJiGuiZeRow.describeValue.get_Item(0);
			}
			if (chongWuTianFuRow.describe != 0)
			{
				string chineseContent3 = GameDataUtils.GetChineseContent(chongWuTianFuRow.describe, false);
				child.FindChild("texSkillDescCurr").GetComponent<Text>().set_text(string.Format(chineseContent3, num4));
			}
			child.FindChild("texSkillDescNext").GetComponent<Text>().set_text("该技能已提升到满级");
		}
	}
}
