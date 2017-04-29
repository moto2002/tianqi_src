using Foundation.Core;
using GameData;
using System;

public class TitleUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_TitleInfoUnits = "TitleInfoUnits";

		public const string Attr_ConditionDesc = "ConditionDesc";

		public const string Attr_LimitTimeDesc = "LimitTimeDesc";

		public const string Attr_BonusesDesc1 = "BonusesDesc1";

		public const string Attr_BonusesDesc2 = "BonusesDesc2";

		public const string Attr_BonusesDesc3 = "BonusesDesc3";

		public const string Attr_BonusesDesc4 = "BonusesDesc4";

		public const string Event_OnBtnComfirmUp = "OnBtnComfirmUp";
	}

	public class ConditionType
	{
		public const int VIP = 1;

		public const int GuildBattle = 2;

		public const int Arena = 3;

		public const int FirstPunchBag = 4;

		public const int GuildMember = 5;
	}

	private static TitleUIViewModel m_instance;

	private int _SelectedTitleId;

	private string _ConditionDesc;

	private string _LimitTimeDesc;

	private string _BonusesDesc;

	public ObservableCollection<OOTitleInfoUnit> TitleInfoUnits = new ObservableCollection<OOTitleInfoUnit>();

	public static TitleUIViewModel Instance
	{
		get
		{
			return TitleUIViewModel.m_instance;
		}
	}

	public int SelectedTitleId
	{
		get
		{
			return this._SelectedTitleId;
		}
		set
		{
			this._SelectedTitleId = value;
			ChengHao chengHao = DataReader<ChengHao>.Get(value);
			if (chengHao != null)
			{
				this.ConditionDesc = this.GetCondition(chengHao);
				if (chengHao.duration == 3)
				{
					this.LimitTimeDesc = GameDataUtils.GetChineseContent(502047, false);
				}
				else
				{
					this.LimitTimeDesc = "00:00:00";
				}
				this.BonusesDesc = GameDataUtils.GetChineseContent(chengHao.gainIntroduction, false);
			}
		}
	}

	public string ConditionDesc
	{
		get
		{
			return this._ConditionDesc;
		}
		set
		{
			this._ConditionDesc = value;
			base.NotifyProperty("ConditionDesc", value);
		}
	}

	public string LimitTimeDesc
	{
		get
		{
			return this._LimitTimeDesc;
		}
		set
		{
			this._LimitTimeDesc = value;
			base.NotifyProperty("LimitTimeDesc", value);
		}
	}

	public string BonusesDesc
	{
		get
		{
			return this._BonusesDesc;
		}
		set
		{
			this._BonusesDesc = value;
			base.NotifyProperty("BonusesDesc1", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		TitleUIViewModel.m_instance = this;
	}

	private void OnEnable()
	{
		this.RefreshTitleInfoUnits();
	}

	public void OnBtnComfirmUp()
	{
		Debuger.Error("OnBtnComfirmUp", new object[0]);
		this.ShowUI(false);
		TitleManager.Instance.SendReplaceCurrTiltle(this.SelectedTitleId);
	}

	private void RefreshTitleInfoUnits()
	{
	}

	private string GetCondition(ChengHao dataCH)
	{
		return string.Empty;
	}

	private void ShowUI(bool isShow)
	{
		base.get_gameObject().GetComponent<TitleUIView>().Show(isShow);
	}
}
