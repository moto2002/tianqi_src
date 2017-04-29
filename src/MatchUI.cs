using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class MatchUI : UIBase
{
	public Text Title;

	public Text ButtonText;

	public Text TimeNum;

	public ButtonCustom Canle;

	public ButtonCustom OkBtn;

	public Transform TimeTran;

	public Image TipsImage;

	private Action CanlecallBack;

	private Action OkcallBack;

	private Action EndBack;

	public int time = 30;

	private bool orderAdd = true;

	private void Awake()
	{
		ButtonCustom expr_06 = this.Canle;
		expr_06.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_06.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickCanle));
		ButtonCustom expr_2D = this.OkBtn;
		expr_2D.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_2D.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickOk));
	}

	protected override void OnEnable()
	{
		base.get_transform().SetAsLastSibling();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void OnClickOk(GameObject go)
	{
		if (this.OkcallBack != null)
		{
			this.OkcallBack.Invoke();
		}
		this.CanlecallBack = null;
		this.EndBack = null;
	}

	private void OnClickCanle(GameObject go)
	{
		if (this.CanlecallBack != null)
		{
			this.CanlecallBack.Invoke();
		}
		this.OkcallBack = null;
		this.EndBack = null;
		this.CloseMatch();
	}

	private void CallBack(bool isEnd)
	{
		if (this.EndBack != null && isEnd)
		{
			this.EndBack.Invoke();
		}
		this.CloseMatch();
	}

	public void SetData(int max, bool order, Action End)
	{
		this.Title.set_text(GameDataUtils.GetChineseContent(513644, false));
		this.EndBack = End;
		this.time = max;
		this.orderAdd = order;
		base.SetMask(0.87f, true, false);
		this.Canle.get_gameObject().SetActive(false);
		this.OkBtn.get_gameObject().SetActive(false);
		ResourceManager.SetSprite(this.TipsImage, ResourceManager.GetIconSprite("zhengzaipipei"));
		base.StartCoroutine(this.StartNum());
	}

	public void SetDataCanle(int max, bool order, Action Close, Action End)
	{
		this.Title.set_text(GameDataUtils.GetChineseContent(513644, false));
		this.EndBack = End;
		this.CanlecallBack = Close;
		this.time = max;
		this.orderAdd = order;
		base.SetMask(0.87f, true, false);
		this.OkBtn.get_gameObject().SetActive(false);
		ResourceManager.SetSprite(this.TipsImage, ResourceManager.GetIconSprite("zhengzaipipei"));
		base.StartCoroutine(this.StartNum());
	}

	public void SetDataOkAndCanle(int max, bool order, Action Close, Action Ok, Action End, string OkName)
	{
		this.EndBack = End;
		this.CanlecallBack = Close;
		this.OkcallBack = Ok;
		this.Title.set_text(GameDataUtils.GetChineseContent(513644, false));
		ResourceManager.SetSprite(this.TipsImage, ResourceManager.GetIconSprite("zhengzaipipei"));
		this.time = max;
		this.orderAdd = order;
		base.SetMask(0.87f, true, false);
		base.StartCoroutine(this.StartNum());
		this.OkBtn.get_transform().FindChild("Text").GetComponent<Text>().set_text(OkName);
		this.OkBtn.get_gameObject().SetActive(true);
	}

	public void CloseMatchUI()
	{
		this.CloseMatch();
	}

	[DebuggerHidden]
	private IEnumerator StartNum()
	{
		MatchUI.<StartNum>c__Iterator3C <StartNum>c__Iterator3C = new MatchUI.<StartNum>c__Iterator3C();
		<StartNum>c__Iterator3C.<>f__this = this;
		return <StartNum>c__Iterator3C;
	}

	private void CloseMatch()
	{
		UIManagerControl.Instance.UnLoadUIPrefab("MatchUI");
	}
}
