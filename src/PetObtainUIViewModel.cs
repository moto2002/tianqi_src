using Foundation.Core;
using GameData;
using System;
using UnityEngine;

public class PetObtainUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_PetStar = "PetStar";

		public const string Attr_PetName = "PetName";

		public const string Attr_TipName = "TipName";

		public const string Attr_ShowTip = "ShowTip";

		public const string Event_OnBtnComfirmClick = "OnBtnComfirmClick";
	}

	public static PetObtainUIViewModel Instance;

	private SpriteRenderer _PetStar;

	private string _PetName;

	private string _TipName;

	private bool _ShowTip;

	public SpriteRenderer PetStar
	{
		get
		{
			return this._PetStar;
		}
		set
		{
			this._PetStar = value;
			base.NotifyProperty("PetStar", value);
		}
	}

	public string PetName
	{
		get
		{
			return this._PetName;
		}
		set
		{
			this._PetName = value;
			base.NotifyProperty("PetName", value);
		}
	}

	public string TipName
	{
		get
		{
			return this._TipName;
		}
		set
		{
			this._TipName = value;
			base.NotifyProperty("TipName", value);
		}
	}

	public bool ShowTip
	{
		get
		{
			return this._ShowTip;
		}
		set
		{
			this._ShowTip = value;
			base.NotifyProperty("ShowTip", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		PetObtainUIViewModel.Instance = this;
	}

	public void OnBtnComfirmClick()
	{
		if (!PetManager.Instance.CheckObtainPetNty(false))
		{
			PetObtainUIView.Instance.Show(false);
			if (PetManager.Instance.mPetObtainUIFinishedCallback != null)
			{
				PetManager.Instance.mPetObtainUIFinishedCallback.Invoke();
			}
		}
	}

	public void SetPetInfo(int petId, int obtain_star, int decompose_star, bool exist, bool replace, string petName)
	{
		Pet pet = DataReader<Pet>.Get(petId);
		if (pet != null)
		{
			PetObtainUIView.Instance.SetBackground(pet.petType);
			this.PetName = PetManager.GetPetName(pet, obtain_star);
			this.PetStar = PetManager.GetPetQualityIcon(obtain_star);
			if (exist)
			{
				this.ShowTip = true;
				if (!replace)
				{
					this.TipName = string.Format(GameDataUtils.GetChineseContent(500116, false), petName, PetManager.GetReturnFragment(pet, decompose_star));
				}
				else
				{
					this.TipName = string.Format(GameDataUtils.GetChineseContent(500117, false), petName, PetManager.GetReturnFragment(pet, decompose_star));
				}
			}
			else
			{
				this.ShowTip = false;
			}
		}
	}
}
