using Foundation.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_UpgradeUnits = "UpgradeUnits";

		public const string Attr_FunctionName = "FunctionName";

		public const string Event_OnBtnComfirmClick = "OnBtnComfirmClick";
	}

	private static LevelUpUIViewModel m_instance;

	private SpriteRenderer _FunctionName;

	public ObservableCollection<OOLevelUpUnit> UpgradeUnits = new ObservableCollection<OOLevelUpUnit>();

	private List<LevelUpUnitData> m_datas;

	private int m_indexRolling;

	private bool IsRolling;

	private uint m_timerId;

	public static LevelUpUIViewModel Instance
	{
		get
		{
			if (LevelUpUIViewModel.m_instance == null)
			{
				LevelUpUIViewModel.Open();
			}
			return LevelUpUIViewModel.m_instance;
		}
	}

	public SpriteRenderer FunctionName
	{
		get
		{
			return this._FunctionName;
		}
		set
		{
			this._FunctionName = value;
			base.NotifyProperty("FunctionName", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		LevelUpUIViewModel.m_instance = this;
	}

	public void OnBtnComfirmClick()
	{
		if (!this.StopRolling())
		{
			LevelUpUIView.Instance.Show(false);
		}
	}

	public void ShowActorUpgrade(List<LevelUpUnitData> units)
	{
		LevelUpUIViewModel.Open();
		SoundManager.PlayUI(10013, false);
		this.FunctionName = ResourceManager.GetIconSprite("font_gongxishengji");
		this.m_datas = units;
		this.SetUpgradeUnits();
	}

	private void SetUpgradeUnits()
	{
		this.UpgradeUnits.Clear();
		for (int i = 0; i < this.m_datas.get_Count(); i++)
		{
			LevelUpUnitData levelUpUnitData = this.m_datas.get_Item(i);
			OOLevelUpUnit oOLevelUpUnit = new OOLevelUpUnit();
			oOLevelUpUnit.AttBegin = levelUpUnitData.BeginStr;
			oOLevelUpUnit.AttBegin1 = levelUpUnitData.Begin.ToString();
			oOLevelUpUnit.AttEnd = new Vector3(levelUpUnitData.Begin, levelUpUnitData.End, -1f);
			oOLevelUpUnit.BGVisibility = true;
			this.UpgradeUnits.Add(oOLevelUpUnit);
		}
		this.m_indexRolling = -1;
		this.IsRolling = true;
		this.RollingNext();
	}

	public void RollingNext()
	{
		this.m_indexRolling++;
		int ssindex = this.m_indexRolling;
		if (ssindex < this.UpgradeUnits.Count && ssindex < this.m_datas.get_Count())
		{
			TimerHeap.DelTimer(this.m_timerId);
			this.m_timerId = TimerHeap.AddTimer(220u, 0, delegate
			{
				OOLevelUpUnit oOLevelUpUnit = this.UpgradeUnits[ssindex];
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
		while (num < this.UpgradeUnits.Count && num < this.m_datas.get_Count())
		{
			OOLevelUpUnit oOLevelUpUnit = this.UpgradeUnits[num];
			oOLevelUpUnit.AttEnd = new Vector3(this.m_datas.get_Item(num).Begin, this.m_datas.get_Item(num).End, 0f);
			num++;
		}
		return true;
	}

	private static void Open()
	{
		UIManagerControl.Instance.OpenUI("LevelUpUI", UINodesManager.TopUIRoot, false, UIType.NonPush);
	}
}
