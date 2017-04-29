using System;
using UnityEngine;

public class W_BoxBGr : PropertyAttribute
{
	public int BoxHeight = 170;

	public int BoxWidth = 200;

	public string text;

	public int LineX = 1;

	public int LineY = 15;

	public W_BoxBGr(string text, int BoxHeight, int BoxWidth)
	{
		this.BoxHeight = BoxHeight;
		this.BoxWidth = BoxWidth;
		this.text = text;
	}

	public W_BoxBGr(string text, int BoxHeight, int BoxWidth, int LineX, int LineY)
	{
		this.BoxHeight = BoxHeight;
		this.BoxWidth = BoxWidth;
		this.text = text;
		this.LineX = LineX;
		this.LineY = LineY;
	}
}
