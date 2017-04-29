using System;
using UnityEngine;

public class W_HeaderAttribute : PropertyAttribute
{
	public string headerText;

	public string text;

	public int FontSize;

	public Color HeaderColor = Color.get_gray();

	public W_HeaderAttribute(string header)
	{
		this.headerText = header;
	}

	public W_HeaderAttribute(string header, string text)
	{
		this.headerText = header;
		this.text = text;
	}

	public W_HeaderAttribute(string header, string text, int FontSize)
	{
		this.headerText = header;
		this.text = text;
		this.FontSize = FontSize;
	}

	public W_HeaderAttribute(string header, string text, int FontSize, string HeaderColor)
	{
		this.headerText = header;
		this.text = text;
		this.FontSize = FontSize;
		if (HeaderColor == "RedColor")
		{
			this.HeaderColor = Color.get_red();
		}
		else if (HeaderColor == "WhiteColor")
		{
			this.HeaderColor = Color.get_white();
		}
		else if (HeaderColor == "YellowColor")
		{
			this.HeaderColor = Color.get_yellow();
		}
		else if (HeaderColor == "BlackColor")
		{
			this.HeaderColor = Color.get_black();
		}
		else if (HeaderColor == "GreenColor")
		{
			this.HeaderColor = Color.get_green();
		}
		else if (HeaderColor == "BlueColor")
		{
			this.HeaderColor = Color.get_blue();
		}
	}
}
