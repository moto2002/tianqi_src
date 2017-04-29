using Foundation.Core;
using System;
using UnityEngine;

public class SettingUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string SystemSetting01On = "SystemSetting01On";

		public const string PushSettingOn = "PushSettingOn";

		public const string IsGuideOn = "IsGuideOn";

		public const string IsPostProcessOn = "IsPostProcessOn";

		public const string PP_MotionBlurOn = "PP_MotionBlurOn";

		public const string PP_BloomOn = "PP_BloomOn";

		public const string PeopleNum = "PeopleNum";

		public const string PeopleNumAmount = "PeopleNumAmount";

		public const string IsLOD1On = "IsLOD1On";

		public const string IsLOD2On = "IsLOD2On";

		public const string IsLOD3On = "IsLOD3On";

		public const string IsResolutionOff = "IsResolutionOff";

		public const string ResolutionOn1 = "ResolutionOn1";

		public const string ResolutionOn2 = "ResolutionOn2";

		public const string ResolutionOn3 = "ResolutionOn3";

		public const string AAOn0 = "AAOn0";

		public const string AAOn2 = "AAOn2";

		public const string AAOn4 = "AAOn4";

		public const string AAOn8 = "AAOn8";

		public const string IsMusicOn = "IsMusicOn";

		public const string IsSoundOn = "IsSoundOn";

		public const string IsHeadInfoOn = "IsHeadInfoOn";

		public const string IsManNumOn = "IsManNumOn";

		public const string OnBtnClose = "OnBtnClose";
	}

	public static SettingUIViewModel Instance;

	private bool _SystemSetting01On = true;

	private bool _PushSettingOn;

	private bool _IsGuideOn;

	private bool _IsPostProcessOn;

	private bool _PP_MotionBlurOn;

	private bool _PP_BloomOn;

	private bool _IsManNumOn;

	private bool _IsResolutionOff;

	private bool _ResolutionOn1;

	private bool _ResolutionOn2;

	private bool _ResolutionOn3;

	private bool _AAOn0;

	private bool _AAOn2;

	private bool _AAOn4;

	private bool _AAOn8;

	private int _PeopleNum;

	private float _PeopleNumAmount;

	private bool _IsLOD1On;

	private bool _IsLOD2On;

	private bool _IsLOD3On;

	private bool _IsMusicOn;

	private bool _IsSoundOn;

	private bool _IsHeadInfoOn;

	public bool SystemSetting01On
	{
		get
		{
			return this._SystemSetting01On;
		}
		set
		{
			this._SystemSetting01On = value;
			base.NotifyProperty("SystemSetting01On", value);
		}
	}

	public bool PushSettingOn
	{
		get
		{
			return this._PushSettingOn;
		}
		set
		{
			this._PushSettingOn = value;
			base.NotifyProperty("PushSettingOn", value);
		}
	}

	public bool IsGuideOn
	{
		get
		{
			return this._IsGuideOn;
		}
		set
		{
			this._IsGuideOn = value;
			base.NotifyProperty("IsGuideOn", value);
		}
	}

	public bool IsPostProcessOn
	{
		get
		{
			return this._IsPostProcessOn;
		}
		set
		{
			this._IsPostProcessOn = value;
			base.NotifyProperty("IsPostProcessOn", value);
		}
	}

	public bool PP_MotionBlurOn
	{
		get
		{
			return this._PP_MotionBlurOn;
		}
		set
		{
			this._PP_MotionBlurOn = value;
			base.NotifyProperty("PP_MotionBlurOn", value);
		}
	}

	public bool PP_BloomOn
	{
		get
		{
			return this._PP_BloomOn;
		}
		set
		{
			this._PP_BloomOn = value;
			base.NotifyProperty("PP_BloomOn", value);
		}
	}

	public bool IsManNumOn
	{
		get
		{
			return this._IsManNumOn;
		}
		set
		{
			this._IsManNumOn = value;
			base.NotifyProperty("IsManNumOn", value);
		}
	}

	public bool IsResolutionOff
	{
		get
		{
			return this._IsResolutionOff;
		}
		set
		{
			this._IsResolutionOff = value;
			base.NotifyProperty("IsResolutionOff", value);
		}
	}

	public bool ResolutionOn1
	{
		get
		{
			return this._ResolutionOn1;
		}
		set
		{
			this._ResolutionOn1 = value;
			base.NotifyProperty("ResolutionOn1", value);
		}
	}

	public bool ResolutionOn2
	{
		get
		{
			return this._ResolutionOn2;
		}
		set
		{
			this._ResolutionOn2 = value;
			base.NotifyProperty("ResolutionOn2", value);
		}
	}

	public bool ResolutionOn3
	{
		get
		{
			return this._ResolutionOn3;
		}
		set
		{
			this._ResolutionOn3 = value;
			base.NotifyProperty("ResolutionOn3", value);
		}
	}

	public bool AAOn0
	{
		get
		{
			return this._AAOn0;
		}
		set
		{
			this._AAOn0 = value;
			base.NotifyProperty("AAOn0", value);
		}
	}

	public bool AAOn2
	{
		get
		{
			return this._AAOn2;
		}
		set
		{
			this._AAOn2 = value;
			base.NotifyProperty("AAOn2", value);
		}
	}

	public bool AAOn4
	{
		get
		{
			return this._AAOn4;
		}
		set
		{
			this._AAOn4 = value;
			base.NotifyProperty("AAOn4", value);
		}
	}

	public bool AAOn8
	{
		get
		{
			return this._AAOn8;
		}
		set
		{
			this._AAOn8 = value;
			base.NotifyProperty("AAOn8", value);
		}
	}

	public int PeopleNum
	{
		get
		{
			return this._PeopleNum;
		}
		set
		{
			this._PeopleNum = value;
			base.NotifyProperty("PeopleNum", value);
		}
	}

	public float PeopleNumAmount
	{
		get
		{
			return this._PeopleNumAmount;
		}
		set
		{
			this._PeopleNumAmount = value;
			base.NotifyProperty("PeopleNumAmount", value);
			this.PeopleNum = (int)(100f * value);
		}
	}

	public bool IsLOD1On
	{
		get
		{
			return this._IsLOD1On;
		}
		set
		{
			this._IsLOD1On = value;
			base.NotifyProperty("IsLOD1On", value);
		}
	}

	public bool IsLOD2On
	{
		get
		{
			return this._IsLOD2On;
		}
		set
		{
			this._IsLOD2On = value;
			base.NotifyProperty("IsLOD2On", value);
		}
	}

	public bool IsLOD3On
	{
		get
		{
			return this._IsLOD3On;
		}
		set
		{
			this._IsLOD3On = value;
			base.NotifyProperty("IsLOD3On", value);
		}
	}

	public bool IsMusicOn
	{
		get
		{
			return this._IsMusicOn;
		}
		set
		{
			this._IsMusicOn = value;
			base.NotifyProperty("IsMusicOn", value);
		}
	}

	public bool IsSoundOn
	{
		get
		{
			return this._IsSoundOn;
		}
		set
		{
			this._IsSoundOn = value;
			base.NotifyProperty("IsSoundOn", value);
		}
	}

	public bool IsHeadInfoOn
	{
		get
		{
			return this._IsHeadInfoOn;
		}
		set
		{
			this._IsHeadInfoOn = value;
			base.NotifyProperty("IsHeadInfoOn", value);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		SettingUIViewModel.Instance = this;
	}

	private void OnEnable()
	{
		this.SystemSetting01On = true;
		this.PushSettingOn = false;
		this.IsGuideOn = SystemConfig.IsGuideSystemOn;
		this.IsPostProcessOn = SystemConfig.IsPostProcessOn;
		this.PP_MotionBlurOn = SystemConfig.PP_MotionBlurOn;
		this.PP_BloomOn = SystemConfig.PP_BloomOn;
		float num = (float)GameLevelManager.GameLevelVariable.PeopleNum;
		float num2 = 100f;
		this.PeopleNumAmount = num / num2;
		this.GetLOD();
		this.GetResolution();
		this.GetAA();
		this.IsMusicOn = SystemConfig.IsMusicOn;
		this.IsSoundOn = SystemConfig.IsMusicOn;
		this.IsHeadInfoOn = SystemConfig.IsHeadInfoOn;
		this.IsManNumOn = SystemConfig.IsManNumOn;
		this.GetManNum();
	}

	private void OnDisable()
	{
		SystemConfig.IsGuideSystemOn = this.IsGuideOn;
		if (!SystemConfig.IsGuideSystemOn)
		{
			GuideManager.Instance.ReleaseAllLocks();
		}
		SystemConfig.IsPostProcessOn = this.IsPostProcessOn;
		SystemConfig.PP_MotionBlurOn = this.PP_MotionBlurOn;
		SystemConfig.PP_BloomOn = this.PP_BloomOn;
		this.SetLOD();
		this.SetResolution();
		this.SetAA();
		SoundManager.Instance.TurnOnOff2Music(this.IsMusicOn);
		SoundManager.Instance.TurnOnOff2Sound(this.IsMusicOn);
		SystemConfig.IsHeadInfoOn = this.IsHeadInfoOn;
		SystemConfig.IsManNumOn = this.IsManNumOn;
		this.SetManNum();
		SettingManager.Instance.SaveSetting();
	}

	private void GetLOD()
	{
		this.IsLOD1On = false;
		this.IsLOD2On = false;
		this.IsLOD3On = false;
		int lODLEVEL = GameLevelManager.GameLevelVariable.LODLEVEL;
		if (lODLEVEL != 200)
		{
			if (lODLEVEL != 250)
			{
				if (lODLEVEL == 300)
				{
					this.IsLOD1On = true;
				}
			}
			else
			{
				this.IsLOD2On = true;
			}
		}
		else
		{
			this.IsLOD3On = true;
		}
	}

	private void SetLOD()
	{
		int num = 0;
		if (this.IsLOD1On)
		{
			num = 300;
		}
		else if (this.IsLOD2On)
		{
			num = 250;
		}
		else if (this.IsLOD3On)
		{
			num = 200;
		}
		if (num > 0)
		{
			GameLevelManager.SetGameQuality(num, false);
		}
	}

	private void GetResolution()
	{
		this.IsResolutionOff = SystemConfig.IsSetHardwareResolutionOn;
		if (this.IsResolutionOff)
		{
			int rESOLUTION_WIDTH = SystemConfig.RESOLUTION_WIDTH;
			if (rESOLUTION_WIDTH != 960)
			{
				if (rESOLUTION_WIDTH != 1280)
				{
					if (rESOLUTION_WIDTH == 1920)
					{
						this.ResolutionOn1 = true;
					}
				}
				else
				{
					this.ResolutionOn2 = true;
				}
			}
			else
			{
				this.ResolutionOn3 = true;
			}
		}
	}

	private void SetResolution()
	{
		SystemConfig.IsSetHardwareResolutionOn = this.IsResolutionOff;
		if (this.IsResolutionOff)
		{
			int num = 0;
			if (this.ResolutionOn1)
			{
				num = 1920;
			}
			else if (this.ResolutionOn2)
			{
				num = 1280;
			}
			else if (this.ResolutionOn3)
			{
				num = 960;
			}
			if (num > 0)
			{
				SystemConfig.RESOLUTION_WIDTH = num;
			}
		}
		UIUtils.SetHardwareResolution();
		EventDispatcher.Broadcast("ControlStick.HardwareResolutionChange");
		TimerHeap.AddTimer(1000u, 0, delegate
		{
			EventDispatcher.Broadcast("ControlStick.HardwareResolutionChange");
		});
	}

	private void GetAA()
	{
		this.AAOn0 = false;
		this.AAOn2 = false;
		this.AAOn4 = false;
		this.AAOn8 = false;
		int antiAliasing = QualitySettings.get_antiAliasing();
		switch (antiAliasing)
		{
		case 0:
			this.AAOn0 = true;
			return;
		case 1:
		case 3:
			IL_3C:
			if (antiAliasing != 8)
			{
				return;
			}
			this.AAOn8 = true;
			return;
		case 2:
			this.AAOn2 = true;
			return;
		case 4:
			this.AAOn4 = true;
			return;
		}
		goto IL_3C;
	}

	private void SetAA()
	{
		int antiAliasing = 0;
		if (this.AAOn0)
		{
			antiAliasing = 0;
		}
		else if (this.AAOn2)
		{
			antiAliasing = 2;
		}
		else if (this.AAOn4)
		{
			antiAliasing = 4;
		}
		else if (this.AAOn8)
		{
			antiAliasing = 8;
		}
		QualitySettings.set_antiAliasing(antiAliasing);
	}

	private void GetManNum()
	{
		if (this.IsManNumOn)
		{
			this.PeopleNum = 50;
		}
		else
		{
			this.PeopleNum = 0;
		}
	}

	private void SetManNum()
	{
		if (this.IsManNumOn)
		{
			GameLevelManager.GameLevelVariable.PeopleNum = 50;
		}
		else
		{
			GameLevelManager.GameLevelVariable.PeopleNum = 0;
			ActorVisibleManager.Instance.ClearAVC();
		}
	}

	public void OnBtnClose()
	{
		UIManagerControl.Instance.HideUI("SettingUI");
	}
}
