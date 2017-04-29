using Foundation.Core;
using System;

public class InputUnitViewModel : ViewModelBase
{
	public class Names
	{
		public const string Attr_Input = "Input";

		public const string Attr_Title = "Title";

		public const string Attr_BtnText = "BtnText";

		public const string Event_OnBtnCommitUp = "OnBtnCommitUp";
	}

	public Action Action2Callback;

	private string _Input;

	private string _Title;

	private string _BtnText;

	public string Input
	{
		get
		{
			return this._Input;
		}
		set
		{
			this._Input = value;
			base.NotifyProperty("Input", value);
		}
	}

	public string Title
	{
		get
		{
			return this._Title;
		}
		set
		{
			this._Title = value;
			base.NotifyProperty("Title", this._Title);
		}
	}

	public string BtnText
	{
		get
		{
			return this._BtnText;
		}
		set
		{
			this._BtnText = value;
			base.NotifyProperty("BtnText", value);
		}
	}

	private void OnDisable()
	{
		this.Input = string.Empty;
	}

	public void OnBtnCommitUp()
	{
		Debuger.Error("OnBtnCommitUp", new object[0]);
		if (this.Action2Callback != null)
		{
			this.Action2Callback.Invoke();
		}
	}
}
