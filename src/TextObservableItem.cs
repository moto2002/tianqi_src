using Foundation.Core;
using System;

public class TextObservableItem : ObservableObject
{
	private string content;

	public string Content
	{
		get
		{
			return this.content;
		}
		set
		{
			this.content = value;
		}
	}
}
