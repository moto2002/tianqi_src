using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolygenSytem : MonoBehaviour
{
	private Text m_lblPetMainType;

	private PolygenFrontCtrl mPolygenFrontCtrl;

	public void Init()
	{
		PetBasicUIView.Instance.FindTransform("PetAttrTypes1").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(645019, false));
		PetBasicUIView.Instance.FindTransform("PetAttrTypes2").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(645018, false));
		PetBasicUIView.Instance.FindTransform("PetAttrTypes3").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(645017, false));
		PetBasicUIView.Instance.FindTransform("PetAttrTypes4").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(645022, false));
		PetBasicUIView.Instance.FindTransform("PetAttrTypes5").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(645021, false));
		PetBasicUIView.Instance.FindTransform("PetAttrTypes6").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(645020, false));
		this.mPolygenFrontCtrl = PetBasicUIView.Instance.FindTransform("PolygenFront").GetComponent<PolygenFrontCtrl>();
		this.m_lblPetMainType = PetBasicUIView.Instance.FindTransform("PetMainType").GetComponent<Text>();
	}

	public void SetValues(List<float> attrValues)
	{
		this.mPolygenFrontCtrl._value1 = attrValues.get_Item(0);
		this.mPolygenFrontCtrl._value2 = attrValues.get_Item(1);
		this.mPolygenFrontCtrl._value3 = attrValues.get_Item(2);
		this.mPolygenFrontCtrl._value4 = attrValues.get_Item(3);
		this.mPolygenFrontCtrl._value5 = attrValues.get_Item(4);
		this.mPolygenFrontCtrl._value6 = attrValues.get_Item(5);
		this.mPolygenFrontCtrl.RefreshMesh();
	}

	public void SetType(string text)
	{
		this.m_lblPetMainType.set_text(text);
	}
}
