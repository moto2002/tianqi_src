using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCalItemLevel2 : BaseUIBehaviour
{
	public Image ImageHead;

	public ButtonCustom BtnOpenLevel2;

	public Image ImageProgressFront;

	public Image ImageProgressBack;

	public Text TextNumLevel2;

	public Transform Level3;

	public Text TextAmout;

	public Image ImageOpen;

	public Image ImageClose;

	public List<DamageCalItemLevel3> listChildren = new List<DamageCalItemLevel3>();

	public List<DamageCalItemLevel3> unuseChildren = new List<DamageCalItemLevel3>();

	public DamageCalModel treeItem;

	public ListView listView;

	public Text TextName;

	private float maxWidth = 370f;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.ImageHead = base.FindTransform("ImageHead").GetComponent<Image>();
		this.BtnOpenLevel2 = base.FindTransform("BtnOpenLevel2").GetComponent<ButtonCustom>();
		this.TextNumLevel2 = base.FindTransform("TextNumLevel2").GetComponent<Text>();
		this.Level3 = base.FindTransform("Level3");
		this.ImageProgressFront = base.FindTransform("ImageProgressFront").GetComponent<Image>();
		this.ImageProgressBack = base.FindTransform("ImageProgressBack").GetComponent<Image>();
		this.TextAmout = base.FindTransform("TextAmout").GetComponent<Text>();
		this.ImageOpen = base.FindTransform("ImageOpen").GetComponent<Image>();
		this.TextName = base.FindTransform("TextName").GetComponent<Text>();
		this.ImageClose = base.FindTransform("ImageClose").GetComponent<Image>();
	}

	public void SetUI(DamageCalModel model, float total)
	{
		ResourceManager.SetSprite(this.ImageHead, GameDataUtils.GetIcon(model.iconId));
		this.TextNumLevel2.set_text(model.amount.ToString());
		if (model.amount == 1L)
		{
			this.TextAmout.get_gameObject().SetActive(false);
		}
		else
		{
			this.TextAmout.get_gameObject().SetActive(true);
			this.TextAmout.set_text("x" + model.amount);
		}
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
		string text = string.Empty;
		if (model.damage > 0L)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"<color=#FE583A>",
				model.damage,
				"</color>"
			});
			if (model.heal > 0L)
			{
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"/<color=#5CF606>",
					model.heal,
					"</color>"
				});
			}
		}
		else
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"<color=#5CF606>",
				model.heal,
				"</color>"
			});
		}
		this.TextNumLevel2.set_text(text);
		this.TextName.set_text(model.name);
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

	public DamageCalItemLevel3 SetChild(Transform parent, float y, int leftOrRight)
	{
		DamageCalItemLevel3 damageCalItemLevel;
		if (this.unuseChildren.get_Count() > 0)
		{
			damageCalItemLevel = this.unuseChildren.get_Item(0);
			this.unuseChildren.RemoveAt(0);
		}
		else
		{
			string name = (leftOrRight != 0) ? "DamageCalItemLevel3Right" : "DamageCalItemLevel3Left";
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(name);
			damageCalItemLevel = instantiate2Prefab.GetComponent<DamageCalItemLevel3>();
			damageCalItemLevel.get_transform().SetParent(parent);
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
			this.listChildren.Add(damageCalItemLevel);
		}
		damageCalItemLevel.get_gameObject().SetActive(true);
		damageCalItemLevel.get_transform().GetComponent<RectTransform>().set_localPosition(new Vector3(0f, y, 0f));
		return damageCalItemLevel;
	}
}
