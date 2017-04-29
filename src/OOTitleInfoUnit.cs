using Foundation.Core;
using System;

public class OOTitleInfoUnit : ObservableObject
{
	public class Names
	{
		public const string Attr_Name = "Name";

		public const string Attr_Selected = "Selected";
	}

	private int _TitleId;

	private string _Name;

	private bool _Selected;

	public int TitleId
	{
		get
		{
			return this._TitleId;
		}
		set
		{
			this._TitleId = value;
		}
	}

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

	public bool Selected
	{
		get
		{
			return this._Selected;
		}
		set
		{
			this._Selected = value;
			base.NotifyProperty("Selected", value);
			if (value)
			{
				this.OnChooseUp();
			}
		}
	}

	public void OnChooseUp()
	{
		Debuger.Error("OnChooseUp", new object[0]);
		TitleUIViewModel.Instance.SelectedTitleId = this.TitleId;
	}
}
