using Foundation.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCareerSuccessUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_ChangeCareerUnits = "ChangeCareerUnits";

		public const string Attr_CareerPic = "CareerPic";

		public const string Attr_CareerNameBg = "CareerNameBg";

		public const string Attr_CareerName = "CareerName";

		public const string Event_OnBtnConfirmClick = "OnBtnConfirmClick";
	}

	private static ChangeCareerSuccessUIViewModel m_instance;

	private SpriteRenderer _CareerPic;

	private SpriteRenderer m_CareerNameBg;

	private SpriteRenderer _CareerName;

	public ObservableCollection<OOLevelUpUnit> ChangeCareerUnits = new ObservableCollection<OOLevelUpUnit>();

	private List<LevelUpUnitData> m_datas;

	private int m_indexRolling;

	private bool IsRolling;

	private uint m_timerId;

	public static ChangeCareerSuccessUIViewModel Instance
	{
		get
		{
			if (ChangeCareerSuccessUIViewModel.m_instance == null)
			{
				ChangeCareerSuccessUIViewModel.Open();
			}
			return ChangeCareerSuccessUIViewModel.m_instance;
		}
	}

	public SpriteRenderer CareerPic
	{
		get
		{
			return this._CareerPic;
		}
		set
		{
			this._CareerPic = value;
			base.NotifyProperty("CareerPic", value);
		}
	}

	public SpriteRenderer CareerNameBg
	{
		get
		{
			return this.m_CareerNameBg;
		}
		set
		{
			this.m_CareerNameBg = value;
			base.NotifyProperty("CareerNameBg", value);
		}
	}

	public SpriteRenderer CareerName
	{
		get
		{
			return this._CareerName;
		}
		set
		{
			this._CareerName = value;
			base.NotifyProperty("CareerName", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		ChangeCareerSuccessUIViewModel.m_instance = this;
	}

	public void OnBtnConfirmClick()
	{
		if (!this.StopRolling())
		{
			ChangeCareerSuccessUIView.Instance.Show(false);
		}
	}

	public void ShowChangeCareer(List<LevelUpUnitData> units, int profession)
	{
		ChangeCareerSuccessUIViewModel.Open();
		SoundManager.PlayUI(10013, false);
		this.CareerPic = UIUtils.GetRoleSelfChangeCareerImage();
		this.CareerNameBg = UIUtils.GetRoleSelfNameBg();
		this.CareerName = UIUtils.GetRoleSelfName();
		this.m_datas = units;
		this.SetChangeCareerUnits();
	}

	private void SetChangeCareerUnits()
	{
		this.ChangeCareerUnits.Clear();
		for (int i = 0; i < this.m_datas.get_Count(); i++)
		{
			LevelUpUnitData levelUpUnitData = this.m_datas.get_Item(i);
			OOLevelUpUnit oOLevelUpUnit = new OOLevelUpUnit();
			oOLevelUpUnit.AttBegin = levelUpUnitData.BeginStr;
			oOLevelUpUnit.AttBegin1 = levelUpUnitData.Begin.ToString();
			oOLevelUpUnit.AttEnd = new Vector3(levelUpUnitData.Begin, levelUpUnitData.End, -1f);
			oOLevelUpUnit.BGVisibility = true;
			this.ChangeCareerUnits.Add(oOLevelUpUnit);
		}
		this.m_indexRolling = -1;
		this.IsRolling = true;
		this.RollingNext();
	}

	public void RollingNext()
	{
		this.m_indexRolling++;
		int ssindex = this.m_indexRolling;
		if (ssindex < this.ChangeCareerUnits.Count && ssindex < this.m_datas.get_Count())
		{
			TimerHeap.DelTimer(this.m_timerId);
			this.m_timerId = TimerHeap.AddTimer(220u, 0, delegate
			{
				OOLevelUpUnit oOLevelUpUnit = this.ChangeCareerUnits[ssindex];
				oOLevelUpUnit.AttEnd = new Vector3(this.m_datas.get_Item(ssindex).Begin, this.m_datas.get_Item(ssindex).End, 1f);
			});
		}
		else
		{
			this.IsRolling = false;
		}
	}

	public bool StopRolling()
	{
		if (!this.IsRolling)
		{
			return false;
		}
		TimerHeap.DelTimer(this.m_timerId);
		this.IsRolling = false;
		int num = 0;
		while (num < this.ChangeCareerUnits.Count && num < this.m_datas.get_Count())
		{
			OOLevelUpUnit oOLevelUpUnit = this.ChangeCareerUnits[num];
			oOLevelUpUnit.AttEnd = new Vector3(this.m_datas.get_Item(num).Begin, this.m_datas.get_Item(num).End, 0f);
			num++;
		}
		return true;
	}

	private static void Open()
	{
		UIManagerControl.Instance.OpenUI("ChangeCareerSuccessUI", UINodesManager.TopUIRoot, false, UIType.NonPush);
	}
}
