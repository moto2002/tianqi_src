using System;
using System.Runtime.InteropServices;

public abstract class WordFilter
{
	public class ChineseStringUtility
	{
		internal const int LOCALE_SYSTEM_DEFAULT = 2048;

		internal const int LCMAP_SIMPLIFIED_CHINESE = 33554432;

		internal const int LCMAP_TRADITIONAL_CHINESE = 67108864;

		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int LCMapString(int Locale, int dwMapFlags, string lpSrcStr, int cchSrc, [Out] string lpDestStr, int cchDest);

		public static string ToSimplified(string source)
		{
			string text = new string(' ', source.get_Length());
			WordFilter.ChineseStringUtility.LCMapString(2048, 33554432, source, source.get_Length(), text, source.get_Length());
			return text;
		}

		public static string ToTraditional(string source)
		{
			string text = new string(' ', source.get_Length());
			WordFilter.ChineseStringUtility.LCMapString(2048, 67108864, source, source.get_Length(), text, source.get_Length());
			return text;
		}
	}

	public static string[] s_filters;

	public static bool filter(string content, out string result_str, int filter_deep = 1, bool check_only = false, bool bTrim = false, string replace_str = "*")
	{
		string text = content;
		if (bTrim)
		{
			text = text.Trim();
		}
		result_str = text;
		if (WordFilter.s_filters == null)
		{
			WordFilter.s_filters = XUtility.GetConfigTxt("Word", ".txt").Trim().Replace("\r\n", string.Empty).Split(new char[]
			{
				'|'
			}, 1);
		}
		bool result = false;
		for (int i = 0; i < WordFilter.s_filters.Length; i++)
		{
			string text2 = WordFilter.s_filters[i];
			string text3 = text2.Replace(replace_str, string.Empty);
			if (text3.get_Length() != 0)
			{
				bool flag = true;
				while (flag)
				{
					int num = -1;
					int num2 = -1;
					int j = 0;
					while (j < text3.get_Length())
					{
						string text4 = text3.Substring(j, 1);
						if (!(text4 == replace_str))
						{
							if (num2 + 1 >= text.get_Length())
							{
								flag = false;
								break;
							}
							int num3 = text.IndexOf(text4, num2 + 1, 5);
							if (num3 == -1)
							{
								flag = false;
								break;
							}
							if (j > 0 && num3 - num2 > filter_deep + 1)
							{
								flag = false;
								break;
							}
							num2 = num3;
							if (num == -1)
							{
								num = num3;
							}
							j++;
						}
					}
					if (flag)
					{
						if (check_only)
						{
							return true;
						}
						result = true;
						string text5 = text.Substring(0, num);
						for (int k = num; k <= num2; k++)
						{
							text5 += replace_str;
						}
						string text6 = text.Substring(num2 + 1);
						text = text5 + text6;
					}
				}
			}
		}
		result_str = text;
		return result;
	}
}
