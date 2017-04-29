using System;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInstanceDifficultyItem : MonoBehaviour
{
	public delegate void DelegateObj(GameObject go);

	protected const string selectColor = "fffae6";

	protected const string notSelectColor = "ffd78c";

	protected Image btnImage;

	protected Text btnName;

	protected Text btnLevel;

	protected ButtonCustom btn;

	protected SpecialInstanceDifficultyItem.DelegateObj btnClickCallBack;

	protected string text;

	protected int selectFxUID;

	private void Awake()
	{
		this.btnImage = base.GetComponent<Image>();
		this.btnName = base.get_transform().FindChild("BtnName").GetComponent<Text>();
		this.btnLevel = base.get_transform().FindChild("BtnLevel").GetComponent<Text>();
		this.btn = base.GetComponent<ButtonCustom>();
		this.btn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnBtnClick);
	}

	public void Init(SpecialInstanceDifficultyItem.DelegateObj callback)
	{
		this.btnClickCallBack = callback;
	}

	public void SetText(string theText)
	{
		this.text = theText;
	}

	public void SetLevel(string theText)
	{
		this.btnLevel.set_text(theText);
	}

	public void SetButtonEnable(bool isEnable)
	{
		if (isEnable)
		{
			this.btn.set_enabled(true);
			ImageColorMgr.SetImageColor(this.btnImage, false);
		}
		else
		{
			this.btn.set_enabled(false);
			ImageColorMgr.SetImageColor(this.btnImage, true);
		}
	}

	public void SetSelect(bool isSelect)
	{
		if (isSelect)
		{
			ResourceManager.SetSprite(this.btnImage, ResourceManager.GetIconSprite("fenleianniu_1"));
			this.btnName.set_fontSize(28);
			this.btnName.set_text(TextColorMgr.GetColor(this.text, "fffae6", string.Empty));
		}
		else
		{
			ResourceManager.SetSprite(this.btnImage, ResourceManager.GetIconSprite("fenleianniu_2"));
			this.btnName.set_fontSize(26);
			this.btnName.set_text(TextColorMgr.GetColor(this.text, "ffd78c", string.Empty));
		}
	}

	public void OnBtnClick(GameObject go)
	{
		if (this.btnClickCallBack != null)
		{
			this.btnClickCallBack(go);
		}
	}
}
