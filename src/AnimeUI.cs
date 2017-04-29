using Foundation.Core.Databinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class AnimeUI : UIBase
{
	public enum TalkType
	{
		NpcTalk,
		TaskTalk,
		BeginTurnTalk,
		EndeTurnTalk
	}

	private List<int> animeIds;

	private int index;

	private CanvasGroup imgs;

	private bool isAlpha;

	private AnimeUI.TalkType talkType;

	private Action AnimeEndCallBack;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		ButtonCustom expr_10 = base.FindTransform("Skip").GetComponent<ButtonCustom>();
		expr_10.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_10.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickSkip));
		ButtonCustom expr_41 = base.FindTransform("Next").GetComponent<ButtonCustom>();
		expr_41.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_41.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickNext));
		ButtonCustom expr_72 = base.FindTransform("Pre").GetComponent<ButtonCustom>();
		expr_72.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_72.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickPre));
		this.imgs = base.FindTransform("bg").GetComponent<CanvasGroup>();
		this.imgs.set_alpha(0f);
		AnimeUI.TalkType talkType = this.talkType;
		if (talkType != AnimeUI.TalkType.BeginTurnTalk)
		{
			if (talkType != AnimeUI.TalkType.EndeTurnTalk)
			{
			}
		}
		this.index = -1;
		this.UpdateTexture(true);
		base.StartCoroutine(this.UpdateAlpha(true));
	}

	private void OnClickPre(GameObject go)
	{
		this.UpdateTexture(false);
	}

	private void OnClickNext(GameObject go)
	{
		this.UpdateTexture(true);
	}

	private void OnClickSkip(GameObject go)
	{
		base.StartCoroutine(this.UpdateAlpha(false));
	}

	[DebuggerHidden]
	private IEnumerator UpdateAlpha(bool Fadin)
	{
		AnimeUI.<UpdateAlpha>c__Iterator4A <UpdateAlpha>c__Iterator4A = new AnimeUI.<UpdateAlpha>c__Iterator4A();
		<UpdateAlpha>c__Iterator4A.Fadin = Fadin;
		<UpdateAlpha>c__Iterator4A.<$>Fadin = Fadin;
		<UpdateAlpha>c__Iterator4A.<>f__this = this;
		return <UpdateAlpha>c__Iterator4A;
	}

	public void UpdateTexture(bool order)
	{
		if (order)
		{
			this.index++;
		}
		else
		{
			this.index--;
		}
		if (this.animeIds != null && this.animeIds.get_Count() <= this.index)
		{
			base.StartCoroutine(this.UpdateAlpha(false));
			return;
		}
		if (this.index <= 0)
		{
			this.index = 0;
			base.FindTransform("Pre").get_gameObject().SetActive(false);
		}
		else
		{
			base.FindTransform("Pre").get_gameObject().SetActive(true);
		}
		ResourceManager.SetCodeTexture(base.FindTransform("bg").GetComponent<RawImage>(), this.animeIds.get_Item(this.index).ToString());
	}

	private void CloseUI()
	{
		UIManagerControl.Instance.UnLoadUIPrefab("AnimeUI");
		if (this.AnimeEndCallBack != null)
		{
			this.AnimeEndCallBack.Invoke();
		}
		this.animeIds = null;
	}

	internal void AddCallBack(AnimeUI.TalkType type, Action CallBack)
	{
		this.talkType = type;
		this.AnimeEndCallBack = CallBack;
	}
}
