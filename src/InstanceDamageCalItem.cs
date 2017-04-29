using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceDamageCalItem : BaseUIBehaviour
{
	public Image ImageHead;

	public Text TextNameLevel1;

	public Text TextNumLevel1;

	public Image ImageProgressBack;

	public Image ImageProgressFront;

	public ButtonCustom BtnOpenLevel1;

	public Transform Level2;

	public Text TextAmout;

	public Image ImageOpen;

	public Image ImageBG;

	public Image ImageClose;

	public List<DamageCalItemLevel2> listChildren = new List<DamageCalItemLevel2>();

	public List<DamageCalItemLevel2> unuseChildren = new List<DamageCalItemLevel2>();

	public DamageCalModel treeItem;

	public ListView listView;

	private float maxWidth = 390f;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TextNameLevel1 = base.FindTransform("TextNameLevel1").GetComponent<Text>();
		this.ImageHead = base.FindTransform("ImageHead").GetComponent<Image>();
		this.TextNumLevel1 = base.FindTransform("TextNumLevel1").GetComponent<Text>();
		this.ImageProgressBack = base.FindTransform("ImageProgressBack").GetComponent<Image>();
		this.ImageProgressFront = base.FindTransform("ImageProgressFront").GetComponent<Image>();
		this.BtnOpenLevel1 = base.FindTransform("BtnOpenLevel1").GetComponent<ButtonCustom>();
		this.Level2 = base.FindTransform("Level2");
		this.TextAmout = base.FindTransform("TextAmout").GetComponent<Text>();
		this.ImageOpen = base.FindTransform("ImageOpen").GetComponent<Image>();
		this.ImageClose = base.FindTransform("ImageClose").GetComponent<Image>();
	}

	public void SetUI(DamageCalModel model, float total)
	{
		ResourceManager.SetSprite(this.ImageHead, GameDataUtils.GetIcon(model.iconId));
		this.TextNameLevel1.set_text(model.name);
		string text = string.Empty;
		string text2 = string.Empty;
		if (model.damage != 0L)
		{
			text = "<color=#FE583A>" + model.damage + "</color>";
		}
		if (text.get_Length() == 0)
		{
			if (model.heal != 0L)
			{
				text2 = "<color=#5CF606>" + model.heal + "</color>";
			}
		}
		else if (model.heal != 0L)
		{
			text2 = "/<color=#5CF606>" + model.heal + "</color>";
		}
		this.TextNumLevel1.set_text(text + text2);
		if (model.amount == 1L)
		{
			this.TextAmout.get_gameObject().SetActive(false);
		}
		else
		{
			this.TextAmout.get_gameObject().SetActive(true);
			this.TextAmout.set_text("x" + model.amount);
		}
		float num = (float)model.damage / total;
		float num2 = (float)model.heal / total;
		if (num < 0.03f && num != 0f)
		{
			num = 0.03f;
		}
		if (num2 < 0.03f && num2 != 0f)
		{
			num2 = 0.03f;
		}
		Vector2 sizeDelta = this.ImageProgressFront.GetComponent<RectTransform>().get_sizeDelta();
		sizeDelta.x = num * this.maxWidth;
		this.ImageProgressFront.GetComponent<RectTransform>().set_sizeDelta(sizeDelta);
		sizeDelta = this.ImageProgressBack.GetComponent<RectTransform>().get_sizeDelta();
		sizeDelta.x = (num + num2) * this.maxWidth;
		this.ImageProgressBack.GetComponent<RectTransform>().set_sizeDelta(sizeDelta);
		if (model.open)
		{
			this.ImageOpen.get_gameObject().SetActive(false);
			this.ImageClose.get_gameObject().SetActive(true);
		}
		else
		{
			this.ImageOpen.get_gameObject().SetActive(true);
			this.ImageClose.get_gameObject().SetActive(false);
		}
		if (model.ModelObjType != GameObjectType.ENUM.Monster && model.ModelObjType != GameObjectType.ENUM.Pet)
		{
			if (model.ModelObjType != GameObjectType.ENUM.Role)
			{
				if (model.ModelObjType == GameObjectType.ENUM.OtherType)
				{
				}
			}
		}
	}

	public void ResetAll()
	{
		for (int i = 0; i < this.listChildren.get_Count(); i++)
		{
			this.listChildren.get_Item(i).get_gameObject().SetActive(false);
		}
		this.unuseChildren.Clear();
		this.unuseChildren.AddRange(this.listChildren);
	}

	public DamageCalItemLevel2 SetChild(Transform parent, float y, int leftOrRight)
	{
		DamageCalItemLevel2 damageCalItemLevel;
		if (this.unuseChildren.get_Count() > 0)
		{
			damageCalItemLevel = this.unuseChildren.get_Item(0);
			this.unuseChildren.RemoveAt(0);
		}
		else
		{
			string name = (leftOrRight != 0) ? "DamageCalItemLevel2Right" : "DamageCalItemLevel2Left";
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(name);
			damageCalItemLevel = instantiate2Prefab.GetComponent<DamageCalItemLevel2>();
			this.listChildren.Add(damageCalItemLevel);
			damageCalItemLevel.get_transform().SetParent(parent);
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
		}
		damageCalItemLevel.get_gameObject().SetActive(true);
		damageCalItemLevel.get_transform().GetComponent<RectTransform>().set_localPosition(new Vector3(0f, y, 0f));
		return damageCalItemLevel;
	}
}
