using Foundation.Core.Databinding;
using GameData;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RankUpUI : UIBase
{
	protected const int RankUpFxID = 0;

	protected static readonly Vector3 RankUpUIPreviewLeftTweenOffset = new Vector3(255f, 0f, 0f);

	protected static readonly Vector3 RankUpUIPreviewRightTweenOffset = new Vector3(-255f, 0f, 0f);

	protected GameObject RankUpUIPreviewLeft;

	protected Transform RankUpUIPreviewLeftSlot;

	protected BaseTweenPostion RankUpUIPreviewLeftTween;

	protected RankUpPreviewCell RankUpPreviewLeftCell;

	protected Transform RankUpUIPreviewRightSlot;

	protected BaseTweenPostion RankUpUIPreviewRightTween;

	protected RankUpPreviewCell RankUpPreviewRightCell;

	protected GameObject RankUpUIPreviewMiddle;

	protected Text RankUpUIPreviewMiddleAttrText;

	protected GameObject RankUpUITip;

	protected GameObject RankUpUIAcceptTaskBtn;

	protected GameObject RankUpUITaskStateText;

	protected GameObject RankUpUICommitTaskBtn;

	protected Transform RankUpUIFxSlot;

	protected int rankUpFxUID;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.RankUpUIPreviewLeft = base.FindTransform("RankUpUIPreviewLeft").get_gameObject();
		this.RankUpUIPreviewLeftSlot = base.FindTransform("RankUpUIPreviewLeftSlot");
		this.RankUpUIPreviewLeftTween = this.RankUpUIPreviewLeftSlot.GetComponent<BaseTweenPostion>();
		this.RankUpUIPreviewRightSlot = base.FindTransform("RankUpUIPreviewRightSlot");
		this.RankUpUIPreviewRightTween = this.RankUpUIPreviewRightSlot.GetComponent<BaseTweenPostion>();
		this.RankUpUIPreviewMiddle = base.FindTransform("RankUpUIPreviewMiddle").get_gameObject();
		this.RankUpUIPreviewMiddleAttrText = base.FindTransform("RankUpUIPreviewMiddleAttrText").GetComponent<Text>();
		this.RankUpUITip = base.FindTransform("RankUpUITip").get_gameObject();
		this.RankUpUIAcceptTaskBtn = base.FindTransform("RankUpUIAcceptTaskBtn").get_gameObject();
		this.RankUpUITaskStateText = base.FindTransform("RankUpUITaskStateText").get_gameObject();
		this.RankUpUICommitTaskBtn = base.FindTransform("RankUpUICommitTaskBtn").get_gameObject();
		this.RankUpUIFxSlot = base.FindTransform("RankUpUIFxSlot");
		ButtonCustom expr_107 = base.FindTransform("RankUpUICloseBtn").GetComponent<ButtonCustom>();
		expr_107.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_107.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnCloseBtnClick));
		ButtonCustom expr_133 = this.RankUpUIAcceptTaskBtn.GetComponent<ButtonCustom>();
		expr_133.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_133.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnAcceptTaskBtnClick));
		ButtonCustom expr_15F = this.RankUpUICommitTaskBtn.GetComponent<ButtonCustom>();
		expr_15F.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_15F.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnCommitTaskBtnClick));
		base.FindTransform("RankUpUIBGTitleName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(0, false));
		this.RankUpUITip.GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(0, false));
		base.FindTransform("RankUpUIAcceptTaskBtnText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(0, false));
		base.FindTransform("RankUpUICommitTaskBtnText").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(0, false));
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void SetData(int rank, RankUpUIState state)
	{
	}

	public void RankUp()
	{
		this.ResetPreview();
		this.ShowMiddle(false);
		this.ShowTip(false);
		this.ShowAcceptTaskBtn(false);
		this.ShowTaskStateText(false);
		this.ShowCommitTaskBtn(false);
		this.RankUpUIPreviewLeftTween.MoveTo(RankUpUI.RankUpUIPreviewLeftTweenOffset, 0.5f, delegate
		{
			if (this.RankUpUIPreviewLeft.get_activeSelf())
			{
				this.RankUpUIPreviewLeft.SetActive(false);
			}
		});
		this.RankUpUIPreviewRightTween.MoveTo(RankUpUI.RankUpUIPreviewRightTweenOffset, 0.5f);
		this.rankUpFxUID = FXSpineManager.Instance.PlaySpine(0, this.RankUpUIFxSlot, "RankUpUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	protected void ResetPreview()
	{
		if (!this.RankUpUIPreviewLeft.get_activeSelf())
		{
			this.RankUpUIPreviewLeft.SetActive(true);
		}
		this.RankUpUIPreviewLeftTween.Reset(false, true);
		this.RankUpUIPreviewRightTween.Reset(false, true);
	}

	protected void SetLeftPreview(int leftModelID)
	{
		this.SetPreview(this.RankUpPreviewLeftCell, this.RankUpUIPreviewLeftSlot, leftModelID);
	}

	protected void SetRightPreview(int rightModelID)
	{
		this.SetPreview(this.RankUpPreviewRightCell, this.RankUpUIPreviewRightSlot, rightModelID);
	}

	protected void SetPreview(RankUpPreviewCell rankUpPreviewCell, Transform rankUpPreviewCellSlot, int theModelID)
	{
		if (rankUpPreviewCell != null && rankUpPreviewCell.get_gameObject() != null)
		{
			Object.Destroy(rankUpPreviewCell.get_gameObject());
		}
		rankUpPreviewCell = RankUpPreviewManager.Instance.GetPreview(rankUpPreviewCellSlot);
		rankUpPreviewCell.SetData(theModelID);
	}

	protected void ShowMiddle(bool isShow)
	{
		if (this.RankUpUIPreviewMiddle.get_activeSelf() != isShow)
		{
			this.RankUpUIPreviewMiddle.SetActive(isShow);
		}
	}

	protected string GetAttrText(int attrID)
	{
		if (!DataReader<Attrs>.Contains(attrID))
		{
			return string.Empty;
		}
		Attrs attrs = DataReader<Attrs>.Get(attrID);
		int num = (attrs.attrs.get_Count() >= attrs.values.get_Count()) ? attrs.values.get_Count() : attrs.attrs.get_Count();
		if (num == 0)
		{
			return string.Empty;
		}
		XDict<int, long> xDict = new XDict<int, long>();
		for (int i = 0; i < num; i++)
		{
			if (xDict.ContainsKey(attrs.attrs.get_Item(i)))
			{
				XDict<int, long> xDict2;
				XDict<int, long> expr_80 = xDict2 = xDict;
				int key;
				int expr_8F = key = attrs.attrs.get_Item(i);
				long num2 = xDict2[key];
				expr_80[expr_8F] = num2 + (long)attrs.values.get_Item(i);
			}
			else
			{
				xDict.Add(attrs.attrs.get_Item(i), (long)attrs.values.get_Item(i));
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(AttrUtility.GetDesc(xDict.Keys.get_Item(0), xDict.Values.get_Item(0), " +", "fffae6", "f95f4f", false));
		for (int j = 1; j < xDict.Count; j++)
		{
			stringBuilder.Append("\n");
			stringBuilder.Append(AttrUtility.GetDesc(xDict.Keys.get_Item(j), xDict.Values.get_Item(j), " +", "fffae6", "f95f4f", false));
		}
		return stringBuilder.ToString();
	}

	protected void SetAttrText(string text)
	{
		this.RankUpUIPreviewMiddleAttrText.set_text(text);
	}

	protected void ShowTip(bool isShow)
	{
		if (this.RankUpUITip.get_activeSelf() != isShow)
		{
			this.RankUpUITip.SetActive(isShow);
		}
	}

	protected void ShowAcceptTaskBtn(bool isShow)
	{
		if (this.RankUpUIAcceptTaskBtn.get_activeSelf() != isShow)
		{
			this.RankUpUIAcceptTaskBtn.SetActive(isShow);
		}
	}

	protected void ShowTaskStateText(bool isShow)
	{
		if (this.RankUpUITaskStateText.get_activeSelf() != isShow)
		{
			this.RankUpUITaskStateText.SetActive(isShow);
		}
	}

	protected void ShowCommitTaskBtn(bool isShow)
	{
		if (this.RankUpUICommitTaskBtn.get_activeSelf() != isShow)
		{
			this.RankUpUICommitTaskBtn.SetActive(isShow);
		}
	}

	protected void OnCloseBtnClick(GameObject go)
	{
		this.Show(false);
	}

	protected void OnAcceptTaskBtnClick(GameObject go)
	{
		EventDispatcher.Broadcast(RankUpManagerEvent.AcceptTask);
	}

	protected void OnCommitTaskBtnClick(GameObject go)
	{
		EventDispatcher.Broadcast(RankUpManagerEvent.CommitTask);
	}
}
