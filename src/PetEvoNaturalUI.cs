using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetEvoNaturalUI : PetEvo
{
	private void Awake()
	{
		base.AwakeSelf();
	}

	protected override void OnEnable()
	{
		this.OffsetY = 200f;
		this.Spacing = 135f;
		base.OnEnable();
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
		return PetEvoGlobal.GetNaturalIds(PetBasicUIViewModel.PetID);
	}

	protected override void OnPlaySKill(GameObject go)
	{
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
			gameObject = ResourceManager.GetInstantiate2Prefab(WidgetName.PetEvoTalentCell);
		}
		gameObject.get_transform().SetParent(base.get_transform());
		gameObject.get_transform().set_localPosition(Vector3.get_zero() + new Vector3(0f, this.OffsetY - this.Spacing * (float)cellIndex, 0f));
		gameObject.get_transform().set_localRotation(Quaternion.get_identity());
		gameObject.get_transform().set_localScale(Vector3.get_one());
		gameObject.SetActive(true);
		gameObject.set_name(cellIndex.ToString());
	}

	protected override void SetOneSkill(int cellIndex, int talentId, int talentLv, int nextTalentLv)
	{
		Transform child = base.get_transform().GetChild(cellIndex);
		Text component = child.FindChild("texName").GetComponent<Text>();
		Text component2 = child.FindChild("texLv").GetComponent<Text>();
		Text component3 = child.FindChild("texNeedLv").GetComponent<Text>();
		Text component4 = child.FindChild("texDesc").GetComponent<Text>();
		Image component5 = child.FindChild("imgNatural").GetComponent<Image>();
		Image component6 = child.FindChild("imgMaterial").GetComponent<Image>();
		Text component7 = child.FindChild("texMaterial").GetComponent<Text>();
		ButtonCustom component8 = child.FindChild("btnLvUp").GetComponent<ButtonCustom>();
		string desc = PetEvoGlobal.GetDesc(talentId);
		if (PetEvoGlobal.IsEnoughStar(PetBasicUIViewModel.PetID, talentId))
		{
			component.set_text(PetEvoGlobal.GetName(talentId));
			component4.set_text(desc);
			component4.set_color(new Color32(165, 90, 65, 255));
			ImageColorMgr.SetImageColor(component5, false);
		}
		else
		{
			string text = string.Empty;
			int requireStar = PetEvoGlobal.GetRequireStar(PetBasicUIViewModel.PetID, talentId);
			PetConversion petConversion = DataReader<PetConversion>.Get(requireStar);
			if (petConversion != null)
			{
				text = string.Format(GameDataUtils.GetChineseContent(400018, false), petConversion.tianfuSuffix);
				component.set_text(PetEvoGlobal.GetName(talentId) + text);
			}
			component4.set_text(TextColorMgr.FilterColor(desc));
			component4.set_color(Color.get_gray());
			ImageColorMgr.SetImageColor(component5, true);
		}
		component3.get_gameObject().SetActive(false);
		component2.get_gameObject().SetActive(false);
		component8.get_gameObject().SetActive(false);
		component6.get_gameObject().SetActive(false);
		component7.get_gameObject().SetActive(false);
	}

	protected override void OnDownTip(GameObject go)
	{
	}

	protected override void OnUpTip(GameObject go)
	{
	}
}
