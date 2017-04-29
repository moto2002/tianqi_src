using Foundation.Core;
using GameData;
using System;
using UnityEngine;

public class OOPetEXPUnit : ObservableObject
{
	public class Names
	{
		public const string Attr_ItemFrame = "ItemFrame";

		public const string Attr_ItemIcon = "ItemIcon";

		public const string Attr_ItemNum = "ItemNum";

		public const string Attr_BtnUseEnable = "BtnUseEnable";

		public const string Attr_Checked = "Checked";

		public const string Event_OnBtnUseClick = "OnBtnUseClick";

		public const string Event_OnBtnUseDown = "OnBtnUseDown";

		public const string Event_OnBtnUseUp = "OnBtnUseUp";

		public const string Event_OnBtnItemUp = "OnBtnItemUp";
	}

	public int ItemId;

	private SpriteRenderer _ItemFrame;

	private SpriteRenderer _ItemIcon;

	private string _ItemNum;

	private bool _BtnUseEnable;

	private bool _Checked;

	public static bool IsCancelDown;

	private static float _FirstRealtimeSinceStartup;

	private static float _LastRealtimeSinceStartup;

	private static int m_currentRaiseTimes;

	private int _raiseTime;

	private int _raiseTimeSpeed;

	public SpriteRenderer ItemFrame
	{
		get
		{
			return this._ItemFrame;
		}
		set
		{
			this._ItemFrame = value;
			base.NotifyProperty("ItemFrame", value);
		}
	}

	public SpriteRenderer ItemIcon
	{
		get
		{
			return this._ItemIcon;
		}
		set
		{
			this._ItemIcon = value;
			base.NotifyProperty("ItemIcon", value);
		}
	}

	public string ItemNum
	{
		get
		{
			return this._ItemNum;
		}
		set
		{
			this._ItemNum = value;
			base.NotifyProperty("ItemNum", value);
		}
	}

	public bool BtnUseEnable
	{
		get
		{
			return this._BtnUseEnable;
		}
		set
		{
			this._BtnUseEnable = value;
			base.NotifyProperty("BtnUseEnable", value);
		}
	}

	public bool Checked
	{
		get
		{
			return this._Checked;
		}
		set
		{
			this._Checked = value;
			base.NotifyProperty("Checked", value);
		}
	}

	private static float FirstRealtimeSinceStartup
	{
		get
		{
			return OOPetEXPUnit._FirstRealtimeSinceStartup;
		}
		set
		{
			OOPetEXPUnit._FirstRealtimeSinceStartup = value;
		}
	}

	private static float LastRealtimeSinceStartup
	{
		get
		{
			return OOPetEXPUnit._LastRealtimeSinceStartup;
		}
		set
		{
			OOPetEXPUnit._LastRealtimeSinceStartup = value;
		}
	}

	private int raiseTime
	{
		get
		{
			if (this._raiseTime == 0)
			{
				this._raiseTime = DataReader<CChongWuSheZhi>.Get("raiseTime").num;
			}
			return this._raiseTime;
		}
	}

	private int raiseTimeSpeed
	{
		get
		{
			if (this._raiseTimeSpeed == 0)
			{
				this._raiseTimeSpeed = DataReader<CChongWuSheZhi>.Get("raiseTimeSpeed").num;
			}
			return this._raiseTimeSpeed;
		}
	}

	public void OnBtnUseClick()
	{
		this.DoUse();
	}

	public void OnBtnUseDown(GameObject go)
	{
		OOPetEXPUnit.LastRealtimeSinceStartup = Time.get_realtimeSinceStartup();
		if (OOPetEXPUnit.FirstRealtimeSinceStartup == 0f)
		{
			OOPetEXPUnit.IsCancelDown = false;
			OOPetEXPUnit.FirstRealtimeSinceStartup = Time.get_realtimeSinceStartup();
			this.DoUse();
		}
		else
		{
			if (OOPetEXPUnit.IsCancelDown)
			{
				return;
			}
			if (this.IsNextTimeReach())
			{
				this.OnBtnUseClick();
				OOPetEXPUnit.m_currentRaiseTimes++;
			}
		}
	}

	public void OnBtnUseUp()
	{
		this.ResetAll();
		PetBasicUIViewModel.Instance.RemoveMoreEXPBarAnimationAction();
	}

	public void OnBtnItemUp()
	{
		PetBasicUIViewModel.Instance.SetEXPItemSelected(this.ItemId, true);
	}

	private void ResetAll()
	{
		OOPetEXPUnit.FirstRealtimeSinceStartup = 0f;
		OOPetEXPUnit.LastRealtimeSinceStartup = 0f;
		OOPetEXPUnit.m_currentRaiseTimes = 0;
	}

	private bool IsNextTimeReach()
	{
		float num = OOPetEXPUnit.LastRealtimeSinceStartup - OOPetEXPUnit.FirstRealtimeSinceStartup;
		return num > this.GetTotalNextTime();
	}

	private float GetTotalNextTime()
	{
		float num = 0f;
		int num2 = OOPetEXPUnit.m_currentRaiseTimes + 1;
		int raiseTime = this.raiseTime;
		int raiseTimeSpeed = this.raiseTimeSpeed;
		int num3 = 0;
		for (int i = 1; i <= raiseTime; i++)
		{
			int num4 = i * raiseTimeSpeed;
			num4 = Mathf.Min(num4, raiseTime);
			num3 += num4;
			if (num2 < num3)
			{
				int num5 = num3 - num2;
				float num6 = 1f / (float)num4;
				float num7 = (float)num5 * num6;
				return num + num7;
			}
			num += 1f;
			if (num2 == num3)
			{
				return num;
			}
			if (num4 == raiseTime)
			{
				break;
			}
		}
		int num8 = num2 - num3;
		float num9 = 1f / (float)raiseTime;
		float num10 = (float)num8 * num9;
		return num + num10;
	}

	private void DoUse()
	{
		if (BackpackManager.Instance.OnGetGoodCount(this.ItemId) > 0L)
		{
			PetManager.Instance.SendUpgradeLevel(PetBasicUIViewModel.PetUID, this.ItemId, 1);
		}
		else
		{
			OOPetEXPUnit.IsCancelDown = true;
			UIManagerControl.Instance.OpenSourceReferenceUI(this.ItemId, null);
		}
	}
}
