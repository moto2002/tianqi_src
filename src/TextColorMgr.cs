using GameData;
using System;
using UnityEngine;

public class TextColorMgr
{
	public class RGB
	{
		public const int A1 = 301;

		public const int A2 = 302;

		public const int A3 = 303;

		public const int A4 = 304;

		public const int A5 = 305;

		public const int A6 = 306;

		public const int A7 = 307;

		public const int A8 = 308;

		public const int A9 = 309;

		public const int A10 = 310;

		public const int A11 = 311;

		public const int A12 = 312;

		public const int B1 = 401;

		public const int B2 = 402;

		public const int B3 = 403;

		public const int B4 = 404;

		public const int B5 = 405;

		public const int White = 301;

		private const string COC_Yellow_Light = "f9d092";

		private const string COC_Yellow_Bright = "ffd451";

		private const string COC_Yellow_Dark = "b19066";

		private const string COC_Blue = "5fd0ff";

		private const string COC_Purple = "adc6ff";

		private const string COC_Black = "000000";

		private const string COC_Red = "ff0000";

		private const string COC_NORMAL = "ffffff";

		private const string COC_Orange = "ff7d4b";

		public const int Yellow_Light = 1000001;

		public const int Yellow_Bright = 1000002;

		public const int Yellow_Dark = 1000003;

		public const int Blue = 1000004;

		public const int Purple = 1000005;

		public const int Black = 1000006;

		public const int Red = 1000007;

		public const int NORMAL = 1000008;

		public const int Orange = 1000009;

		public static string GetRGB(int rgbId)
		{
			string result = string.Empty;
			switch (rgbId)
			{
			case 1000001:
				result = "f9d092";
				break;
			case 1000002:
				result = "ffd451";
				break;
			case 1000003:
				result = "b19066";
				break;
			case 1000004:
				result = "5fd0ff";
				break;
			case 1000005:
				result = "adc6ff";
				break;
			case 1000006:
				result = "000000";
				break;
			case 1000007:
				result = "ff0000";
				break;
			case 1000008:
				result = "ffffff";
				break;
			case 1000009:
				result = "ff7d4b";
				break;
			default:
				if (rgbId == 0)
				{
					result = "ffffff";
				}
				else
				{
					WenZiRGB wenZiRGB = DataReader<WenZiRGB>.Get(rgbId);
					result = ((wenZiRGB == null) ? "ffffff" : wenZiRGB.rgb);
				}
				break;
			}
			return result;
		}
	}

	public static Color32 EffectColor2Outline = new Color32(2, 5, 64, 128);

	public static Color32 EffectColor2Shadow = new Color32(1, 14, 21, 128);

	private static string ColorPrefix = "<color";

	private static string ColorSuffix = "</color>";

	public static string GetColorByID(string text, int rgbId)
	{
		return TextColorMgr.GetTextWithColor(text, TextColorMgr.RGB.GetRGB(rgbId));
	}

	public static string GetColorByID(string text, int rgbId, float alpha)
	{
		return TextColorMgr.GetTextWithColor(text, TextColorMgr.RGB.GetRGB(rgbId) + TextColorMgr.GetAlpha(alpha));
	}

	public static string GetColorByQuality(string text, int quality)
	{
		return TextColorMgr.GetColorByID(text, quality);
	}

	public static string GetColorByQuality(string text, int quality, float alpha)
	{
		return TextColorMgr.GetColorByID(text, quality, alpha);
	}

	public static string GetColor(string text, string color, string alpha = "")
	{
		if (string.IsNullOrEmpty(color))
		{
			return text;
		}
		return TextColorMgr.GetTextWithColor(text, color + alpha);
	}

	public static string FilterColor(string content)
	{
		if (string.IsNullOrEmpty(content))
		{
			return content;
		}
		int num = 0;
		while (num + TextColorMgr.ColorSuffix.get_Length() <= content.get_Length())
		{
			string text = content.Substring(num, TextColorMgr.ColorSuffix.get_Length());
			if (text.Equals(TextColorMgr.ColorSuffix))
			{
				content = content.ReplaceFirst(text, string.Empty, 0);
			}
			else
			{
				num++;
			}
		}
		int num2 = 0;
		while (num2 + TextColorMgr.ColorPrefix.get_Length() <= content.get_Length())
		{
			string text2 = content.Substring(num2, TextColorMgr.ColorPrefix.get_Length());
			if (text2.Equals(TextColorMgr.ColorPrefix))
			{
				for (int i = num2 + TextColorMgr.ColorPrefix.get_Length(); i <= content.get_Length(); i++)
				{
					string text3 = content.Substring(i, 1);
					if (text3.Equals(">"))
					{
						content = content.Remove(num2, i - num2 + 1);
						break;
					}
				}
			}
			else
			{
				num2++;
			}
		}
		return content;
	}

	private static string GetTextWithColor(string text, string color)
	{
		return string.Concat(new string[]
		{
			"<color=#",
			color,
			">",
			text,
			"</color>"
		});
	}

	private static string GetAlpha(float alpha)
	{
		return ((int)Mathf.Min(255f, 255f * alpha)).ToString("x2");
	}
}
