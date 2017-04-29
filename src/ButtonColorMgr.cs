using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorMgr
{
	public class BTN
	{
		public const string ORANGE = "button_orange_1";

		public const string YELLOW = "button_yellow_1";

		public const string GRAY = "button_gray_1";

		public const string XUANXIANG = "xuanxiang_button";
	}

	public const int Blue = 1;

	public const int Green = 2;

	public const int ButtonPrivate = 101;

	public static SpriteRenderer GetButton(string name)
	{
		return ResourceManager.GetIconSprite(name);
	}

	public static SpriteRenderer GetButton(int color)
	{
		SpriteRenderer result = null;
		if (color != 1)
		{
			if (color != 2)
			{
				if (color == 101)
				{
					result = ResourceManager.GetIconSprite("zhanghaokuang");
				}
			}
			else
			{
				result = ResourceManager.GetIconSprite("xqan");
			}
		}
		else
		{
			result = ResourceManager.GetIconSprite("xinanniu");
		}
		return result;
	}

	public static void SetButtonEnable(bool bEnable, ButtonCustom buttonCustom, Image bgUp, Image bgDown)
	{
		buttonCustom.set_enabled(bEnable);
		if (bEnable)
		{
			ImageColorMgr.SetImageColor(bgUp, false);
			ImageColorMgr.SetImageColor(bgDown, false);
		}
		else
		{
			ImageColorMgr.SetImageColor(bgUp, true);
			ImageColorMgr.SetImageColor(bgDown, true);
		}
	}
}
