using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInstanceTips : UIBase
{
	private Action endCallBack;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void SetInit(DefendFightTips type, Action callBack = null)
	{
		SoundManager.PlayUI(10038, false);
		this.endCallBack = callBack;
		string spriteName = string.Empty;
		string spriteName2 = string.Empty;
		string spriteName3 = string.Empty;
		if (type != DefendFightTips.BossEnter)
		{
			if (type != DefendFightTips.TaskAchieve)
			{
				spriteName = "rwwqd";
				spriteName2 = "rwwqicon";
				spriteName3 = "rwwqz";
			}
			else
			{
				spriteName = "rwwqd";
				spriteName2 = "rwwqicon";
				spriteName3 = "rwwqz";
			}
		}
		else
		{
			spriteName = "qdlrd";
			spriteName2 = "qdlricon";
			spriteName3 = "qdlrz";
		}
		ResourceManager.SetSprite(base.FindTransform("ImageBG1").GetComponent<Image>(), ResourceManager.GetCodeSprite(spriteName));
		ResourceManager.SetSprite(base.FindTransform("ImageBG2").GetComponent<Image>(), ResourceManager.GetCodeSprite(spriteName2));
		ResourceManager.SetSprite(base.FindTransform("ImageBG3").GetComponent<Image>(), ResourceManager.GetCodeSprite(spriteName3));
		base.GetComponent<Animator>().Play("DefendEnterAnim", 0, 0f);
	}

	public void EndCallBack()
	{
		if (this.endCallBack != null)
		{
			this.endCallBack.Invoke();
		}
		this.Show(false);
	}
}
