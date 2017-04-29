using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RankUpChangePreviewUI : UIBase
{
	protected Transform RankUpChangePreviewUISlot;

	protected RankUpPreviewCell rankUpPreviewCell;

	protected void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.RankUpChangePreviewUISlot = base.FindTransform("RankUpChangePreviewUISlot");
		if (DataReader<GlobalParams>.Contains("advancedJobText"))
		{
			string value = DataReader<GlobalParams>.Get("advancedJobText").value;
			string[] array = value.Split(new char[]
			{
				';'
			});
			base.FindTransform("RankUpChangePreviewUIBtnImageName").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(int.Parse(array[5]), false));
		}
		base.FindTransform("RankUpChangePreviewUIBtn").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickConfirmBtn);
	}

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void SetData(int theModelID)
	{
		this.SetPreview(theModelID);
	}

	protected void SetPreview(int theModelID)
	{
		if (this.rankUpPreviewCell != null && this.rankUpPreviewCell.get_gameObject() != null)
		{
			Object.Destroy(this.rankUpPreviewCell.get_gameObject());
		}
		this.rankUpPreviewCell = RankUpPreviewManager.Instance.GetPreview(this.RankUpChangePreviewUISlot);
		this.rankUpPreviewCell.Bind(this);
		this.rankUpPreviewCell.SetData(theModelID);
	}

	protected void OnClickConfirmBtn(GameObject go)
	{
		this.Show(false);
	}

	private void OnApplicationPause(bool isPause)
	{
		if (this.rankUpPreviewCell)
		{
			this.rankUpPreviewCell.DoOnApplicationPause();
		}
		TimerHeap.AddTimer(2000u, 0, delegate
		{
			if (this != null && base.get_gameObject() != null && base.get_gameObject().get_activeInHierarchy() && this.rankUpPreviewCell)
			{
				this.rankUpPreviewCell.DoOnApplicationPause();
			}
		});
	}
}
