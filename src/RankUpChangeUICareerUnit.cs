using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RankUpChangeUICareerUnit : BaseUIBehaviour
{
	protected Image RankUpChangeUICareerUnitImage;

	protected Text RankUpChangeUICareerUnitImageName;

	protected int i = -1;

	protected Action<int> clickAction;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.RankUpChangeUICareerUnitImage = base.FindTransform("RankUpChangeUICareerUnitImage").GetComponent<Image>();
		this.RankUpChangeUICareerUnitImageName = base.FindTransform("RankUpChangeUICareerUnitImageName").GetComponent<Text>();
		this.RankUpChangeUICareerUnitImage.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickCareerBtn);
	}

	public void SetData(int index, int name, Action<int> clickCallback)
	{
		this.i = index;
		this.clickAction = clickCallback;
		this.SetName(GameDataUtils.GetChineseContent(name, false));
	}

	protected void SetName(string text)
	{
		this.RankUpChangeUICareerUnitImageName.set_text(text);
	}

	public void SetClickState(bool isClick)
	{
		ResourceManager.SetSprite(this.RankUpChangeUICareerUnitImage, ResourceManager.GetIconSprite((!isClick) ? "bt_fenleianniu_3" : "bt_fenleianniu_1"));
		this.RankUpChangeUICareerUnitImage.SetNativeSize();
	}

	protected void OnClickCareerBtn(GameObject go)
	{
		if (this.clickAction != null)
		{
			this.clickAction.Invoke(this.i);
		}
	}
}
