using Foundation.Core;
using System;

public class OOButtonToggle2SubUI : ObservableObject
{
	public class Names
	{
		public const string Attr_Name = "Name";

		public const string Attr_IsTip = "IsTip";

		public const string Attr_IsToggleOn = "IsToggleOn";
	}

	public int ToggleIndex;

	public Action<int> Action2CallBack;

	private string _Name;

	private bool _IsTip;

	private bool _IsToggleOn;

	public string Name
	{
		get
		{
			return this._Name;
		}
		set
		{
			this._Name = value;
			base.NotifyProperty("Name", value);
		}
	}

	public bool IsTip
	{
		get
		{
			return this._IsTip;
		}
		set
		{
			this._IsTip = value;
			base.NotifyProperty("IsTip", value);
		}
	}

	public bool IsToggleOn
	{
		get
		{
			return this._IsToggleOn;
		}
		set
		{
			this._IsToggleOn = value;
			base.NotifyProperty("IsToggleOn", value);
			if (value && this.Action2CallBack != null)
			{
				this.Action2CallBack.Invoke(this.ToggleIndex);
			}
		}
	}

	public void SetIsToggleOn(bool isOn)
	{
		this._IsToggleOn = isOn;
		base.NotifyProperty("IsToggleOn", isOn);
	}
}
