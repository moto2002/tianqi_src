using System;
using System.Text;

public class UnicodeEscaper
{
	public static string Escape(string s)
	{
		StringBuilder stringBuilder = new StringBuilder();
		byte[] bytes = Encoding.get_Unicode().GetBytes(s);
		for (int i = 0; i < bytes.Length; i += 2)
		{
			stringBuilder.Append("\\u");
			stringBuilder.Append(bytes[i + 1].ToString("X2"));
			stringBuilder.Append(bytes[i].ToString("X2"));
		}
		return stringBuilder.ToString();
	}

	public static string UnEscape(string s)
	{
		if (s == null)
		{
			return null;
		}
		if (s.get_Length() == 0)
		{
			return string.Empty;
		}
		if (s.get_Length() % 6 != 0)
		{
			return s;
		}
		byte[] array = new byte[s.get_Length() / 3];
		for (int i = 0; i < s.get_Length(); i += 6)
		{
			int num = i / 6 * 2;
			array[num] = Convert.ToByte(s.Substring(i + 4, 2), 16);
			array[num + 1] = Convert.ToByte(s.Substring(i + 2, 2), 16);
		}
		return Encoding.get_Unicode().GetString(array);
	}

	public static string UnEscape2(string s)
	{
		if (s == null)
		{
			return null;
		}
		if (s.get_Length() == 0)
		{
			return string.Empty;
		}
		if (s.get_Length() % 6 != 0)
		{
			return s;
		}
		byte[] array = new byte[s.get_Length() / 3];
		for (int i = 0; i < s.get_Length(); i += 6)
		{
			int num = i / 6 * 2;
			array[num] = Convert.ToByte(s.Substring(i + 4, 2), 16);
			array[num + 1] = Convert.ToByte(s.Substring(i + 2, 2), 16);
		}
		return Encoding.get_Unicode().GetString(array);
	}
}
