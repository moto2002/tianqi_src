using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DamageCalItemLevel3 : BaseUIBehaviour
{
	public Image ImageHead;

	public Image ImageProgressFront;

	public Image ImageProgressBack;

	public Text TextNumLevel3;

	public Text TextAmout;

	public Text TextName;

	private float maxWidth = 370f;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.ImageHead = base.FindTransform("ImageHead").GetComponent<Image>();
		this.ImageProgressFront = base.FindTransform("ImageProgressFront").GetComponent<Image>();
		this.ImageProgressBack = base.FindTransform("ImageProgressBack").GetComponent<Image>();
		this.TextNumLevel3 = base.FindTransform("TextNumLevel3").GetComponent<Text>();
		this.TextAmout = base.FindTransform("TextAmout").GetComponent<Text>();
		this.TextName = base.FindTransform("TextName").GetComponent<Text>();
	}

	public void SetUI(DamageCalModel model, float total)
	{
		ResourceManager.SetSprite(this.ImageHead, GameDataUtils.GetIcon(model.iconId));
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
		this.TextNumLevel3.set_text(text);
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
}
