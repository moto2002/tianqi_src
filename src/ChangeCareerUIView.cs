using System;
using UnityEngine;

public class ChangeCareerUIView : UIBase
{
	private const float CAREER_LEFT_POS1 = -500f;

	private const float CAREER_LEFT_POS2 = -615f;

	private const float CAREER_RIGHT_POS1 = 405f;

	private const float CAREER_RIGHT_POS2 = 515f;

	private const float CAREERPic_LEFT_POS1 = 34f;

	private const float CAREERPic_LEFT_POS2 = -86f;

	private const float CAREERPic_RIGHT_POS1 = 4f;

	private const float CAREERPic_RIGHT_POS2 = 113f;

	public static ChangeCareerUIView Instance;

	private ChangeCareerInfo m_info1;

	private ChangeCareerInfo m_info2;

	private ChangeCareerInfo m_currentInfo;

	private Transform mBackgroundImage;

	private bool m_isIn = true;

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.isClick = false;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		ChangeCareerUIView.Instance = this;
		this.m_info1 = base.get_transform().FindChild("Layer1Info").get_gameObject().AddComponent<ChangeCareerInfo>();
		this.m_info2 = base.get_transform().FindChild("Layer2Info").get_gameObject().AddComponent<ChangeCareerInfo>();
		this.mBackgroundImage = base.FindTransform("BackgroundImage");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110039), string.Empty, delegate
		{
			if (this.m_isIn)
			{
				this.Show(false);
				UIStackManager.Instance.PopUIPrevious(base.uiType);
			}
			else
			{
				this.ResetAll();
			}
		}, false);
		this.ResetAll();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			ChangeCareerUIView.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void ResetAll()
	{
		this.m_isIn = true;
		this.ShowAnimation(true);
		this.m_currentInfo = null;
		int changeCareerID = ChangeCareerManager.GetChangeCareerID(0);
		if (changeCareerID > 0)
		{
			this.m_info1.SetCareerPos(-500f, 34f);
			this.m_info1.SetCareerButtonClick(true);
			if (ChangeCareerManager.Instance.GetTaskFirstTime() || ChangeCareerManager.Instance.IsCareerChangedOrSelectedChange(changeCareerID))
			{
				this.m_info1.SetCareerButtonBlack(false);
			}
			else
			{
				this.m_info1.SetCareerButtonBlack(true);
			}
			this.m_info1.profession = changeCareerID;
			this.m_info1.Show(true, false);
			this.m_info1.ShowAnimation(false);
		}
		int changeCareerID2 = ChangeCareerManager.GetChangeCareerID(1);
		if (changeCareerID2 > 0)
		{
			this.m_info2.SetCareerPos(405f, 4f);
			this.m_info2.SetCareerButtonClick(true);
			if (ChangeCareerManager.Instance.GetTaskFirstTime() || ChangeCareerManager.Instance.IsCareerChangedOrSelectedChange(changeCareerID2))
			{
				this.m_info2.SetCareerButtonBlack(false);
			}
			else
			{
				this.m_info2.SetCareerButtonBlack(true);
			}
			this.m_info2.profession = changeCareerID2;
			this.m_info2.Show(true, false);
			this.m_info2.ShowAnimation(false);
		}
	}

	private void ShowAnimation(bool isShow)
	{
		Animator component = base.GetComponent<Animator>();
		component.set_enabled(isShow);
		if (isShow)
		{
			component.Play("open", 0, 0f);
		}
	}

	public void RefreshTaskInfo()
	{
		if (this.m_currentInfo != null)
		{
			this.m_currentInfo.RefreshTaskInfo();
		}
	}

	public void SwitchCurrentInfo()
	{
		if (this.m_currentInfo == this.m_info1)
		{
			this.SetCurrentInfo(this.m_info2);
		}
		else
		{
			this.SetCurrentInfo(this.m_info1);
		}
	}

	public void SetCurrentInfo(int profession)
	{
		if (this.m_info1.profession == profession)
		{
			this.SetCurrentInfo(this.m_info1);
		}
		else if (this.m_info2.profession == profession)
		{
			this.SetCurrentInfo(this.m_info2);
		}
	}

	public void SetCurrentInfo(ChangeCareerInfo info)
	{
		this.ShowAnimation(false);
		this.m_isIn = false;
		this.m_info1.Show(false, false);
		this.m_info2.Show(false, false);
		if (info == this.m_info1)
		{
			this.m_info1.SetCareerPos(-615f, -86f);
			this.m_info1.SetCareerButtonClick(false);
			this.m_info1.SetCareerButtonBlack(false);
			this.mBackgroundImage.set_localRotation(Quaternion.Euler(0f, 180f, 0f));
		}
		else
		{
			this.m_info2.SetCareerPos(515f, 113f);
			this.m_info2.SetCareerButtonClick(false);
			this.m_info2.SetCareerButtonBlack(false);
			this.mBackgroundImage.set_localRotation(Quaternion.get_identity());
		}
		this.m_currentInfo = info;
		this.m_currentInfo.Show(true, true);
		this.m_currentInfo.ShowAnimation(true);
		this.m_currentInfo.RefreshAll();
	}
}
