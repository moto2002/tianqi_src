using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorMgr
{
	public class HSVId
	{
		public const int Normal = 0;

		public const int WhiteBlack = 6;
	}

	public static void SetImageColor(Image image, bool isWhiteBlack)
	{
		if (isWhiteBlack)
		{
			ImageColorMgr.SetImageColor(image, 6);
		}
		else
		{
			ImageColorMgr.SetImageColor(image, 0);
		}
	}

	private static void SetImageColor(Image image, int hsvId)
	{
		if (image == null)
		{
			return;
		}
		if (hsvId != 0)
		{
			if (hsvId == 6)
			{
				image.set_color(new Color(image.get_color().r, image.get_color().g, 0f, image.get_color().a));
			}
		}
		else
		{
			image.set_color(new Color(image.get_color().r, image.get_color().g, 1f, image.get_color().a));
		}
	}
}
